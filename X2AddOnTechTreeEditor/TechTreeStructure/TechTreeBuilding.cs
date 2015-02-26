using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace X2AddOnTechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert ein Gebäude-Element im Technologiebaum.
	/// </summary>
	public class TechTreeBuilding : TechTreeUnit
	{
		#region Variablen

		/// <summary>
		/// Die in diesem Gebäude enthaltenen Kindelemente.
		/// Dies sind Elemente, die in diesem Gebäude entwickelt oder erschaffen werden können, samt deren Button-IDs.
		/// Die Button-IDs sind nicht zwingend eindeutig!
		/// </summary>
		public List<KeyValuePair<byte, TechTreeElement>> Children { get; private set; }

		/// <summary>
		/// Die direkte Weiterentwicklung dieses Elements.
		/// </summary>
		public TechTreeBuilding Successor { get; set; }

		/// <summary>
		/// Die Technologie, die dieses Element weiterentwickelt.
		/// </summary>
		public TechTreeResearch SuccessorResearch { get; set; }

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Gebäude-Element.
		/// </summary>
		/// <param name="age">Das Zeitalter, ab dem das Element verfügbar ist.</param>
		public TechTreeBuilding()
			: base()
		{
			// Objekte erstellen
			Children = new List<KeyValuePair<byte, TechTreeElement>>();
		}

		/// <summary>
		/// Zeichnet den Unterbaum des Gebäudes an der angegebenen Position.
		/// </summary>
		/// <param name="position">Die Zeichenposition des gesamten Baum-Rechtecks. Es wird die obere linke Ecke angegeben.</param>
		/// <param name="ageOffsets">Eine Liste mit den Zeitalter-Offset-Daten. Diese wird in dieser Funktion nicht verändert.</param>
		/// <param name="lastAge">Das Zeitalter-Offset des übergeordneten Elements.</param>
		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// Pixelbreite des Unterbaums berechnen
			int pixelWidth = TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

			// Sind Kinder vorhanden?
			if(Children.Count != 0)
			{
				// Senkrechte Linie nach unten zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.Lines);
				{
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + RenderControl.BOX_SPACE_VERT); // Oben
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT); // Unten
				}
				GL.End();

				// Kinder zeichnen
				position.Y += RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT;
				int childPixelWidth = 0;
				Point dotFirstChild = position;
				Point dotLastChild = position;
				bool dotFirstChildSet = false;
				int childYOffset = 0;
				foreach(var child in Children)
				{
					// Kind versetzt zeichnen
					child.Value.Draw(position, ageOffsets, parentAgeOffset + 1);

					// Pixel-Breite berechnen
					childPixelWidth = child.Value.TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

					// Aktuelle waagerechte Verbindungslinien-Position bestimmen
					dotLastChild = new Point(position.X + childPixelWidth / 2, position.Y);

					// Ggf. erste waagerechte Verbindungslinien-Position merken
					if(!dotFirstChildSet)
					{
						dotFirstChild = dotLastChild;
						dotFirstChildSet = true;
					}

					// Versatz des Kind-Elements nach unten wegen des Zeitalter-Offsets berechnen
					childYOffset = dotLastChild.Y + Math.Max(ageOffsets[child.Value.Age] - parentAgeOffset - 1, 0) * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT);

					// Senkrechte Linie darauf zeichnen
					GL.Color3(Color.Black);
					GL.Begin(PrimitiveType.Lines);
					{
						GL.Vertex2(dotLastChild.X, dotLastChild.Y); // Oben
						GL.Vertex2(dotLastChild.X, childYOffset + RenderControl.BOX_SPACE_VERT); // Unten
					}
					GL.End();

					// Position erhöhen
					position.X += childPixelWidth;
				}

				// Waagerechte Verbindungslinie zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.Lines);
				{
					GL.Vertex2(dotFirstChild.X, dotFirstChild.Y); // Oben
					GL.Vertex2(dotLastChild.X + 1, dotLastChild.Y); // Unten (+ 1, da sonst ein Pixel am Ende fehlt)
				}
				GL.End();
			}
		}

		/// <summary>
		/// Zeichnet Pfeile zu den Abhängigkeiten dieses Elements.
		/// </summary>
		public override void DrawDependencies()
		{
			// Ggf. Pfeil zur Weiterentwicklungs-Technologie zeichnen
			if(SuccessorResearch != null)
				RenderControl.DrawArrow(this, SuccessorResearch, Color.Red, true);
		}

		/// <summary>
		/// Berechnet die rekursiv die maximale Breite des Unterbaums sowie die Tiefe der jeweiligen Zeitalter-Elemente.
		/// </summary>
		/// <param name="ageCounts">Die aktuelle Liste mit den Tiefen der Elemente pro Zeitalter.</param>
		public override void CalculateTreeBounds(ref List<int> ageCounts)
		{
			// Zähler für eigenes Zeitalter inkrementieren
			++ageCounts[Age];

			// Abmessungen des Baums bestimmen
			if(Children.Count == 0)
				TreeWidth = 1;
			else
			{
				// Die Breite des Baums ist die Summe der Breite der Kinder
				TreeWidth = 0;
				List<int> ageCountsCopy = new List<int>(ageCounts);
				List<int> childAgeCounts = null;
				foreach(var child in Children)
				{
					// Abmessungen des Unterbaums berechnen
					childAgeCounts = new List<int>(ageCountsCopy);
					child.Value.CalculateTreeBounds(ref childAgeCounts);

					// Unterbaumbreite zur Gesamtbreite hinzuaddieren
					TreeWidth += child.Value.TreeWidth;

					// Zurückgegebene Zeitalter-Werte abgleichen => es wird immer das Maximum pro Zeitalter genommen
					ageCounts = ageCounts.Zip(childAgeCounts, (a1, a2) => Math.Max(a1, a2)).ToList();
				}
			}
		}

		/// <summary>
		/// Ruft eine Liste mit den Kindelementen ab.
		/// </summary>
		/// <returns></returns>
		protected override List<TechTreeElement> GetChildren()
		{
			// Kinder und Nachfolgeelement zurückgeben
			List<TechTreeElement> childElements = Children.Select(c => c.Value).ToList();
			if(Successor != null)
				childElements.Add(Successor);
			return childElements;
		}

		/// <summary>
		/// Konvertiert die enthaltenen Daten in ein XML-Format und schreibt sie in den übergebenen XmlWriter.
		/// </summary>
		/// <param name="writer">Der XmlWriter, in den die Daten geschrieben werden sollen.</param>
		/// <param name="elementIDs">Die Liste mit den den TechTree-Elementen zugeordneten IDs.</param>
		/// <param name="lastID">Die letzte vergebene ID. Muss als Referenz übergeben werden, da diese Zahl stetig inkrementiert wird.</param>
		public override int ToXml(XmlWriter writer, Dictionary<TechTreeElement, int> elementIDs, int lastID)
		{
			// Toten-ID abrufen
			int deadUnitID = -1;
			if(DeadUnit != null)
			{
				if(!elementIDs.ContainsKey(DeadUnit))
					deadUnitID = lastID = DeadUnit.ToXml(writer, elementIDs, lastID);
				else
					deadUnitID = elementIDs[DeadUnit];
			}

			// Nachfolger-ID abrufen
			int successorID = -1;
			if(Successor != null)
			{
				if(!elementIDs.ContainsKey(Successor))
					successorID = lastID = Successor.ToXml(writer, elementIDs, lastID);
				else
					successorID = elementIDs[Successor];
			}

			// Nachfolger-Technologie-ID abrufen
			int successorResearchID = -1;
			if(SuccessorResearch != null)
			{
				if(!elementIDs.ContainsKey(SuccessorResearch))
					successorResearchID = lastID = SuccessorResearch.ToXml(writer, elementIDs, lastID);
				else
					successorResearchID = elementIDs[SuccessorResearch];
			}

			// Kind-IDs abrufen (Schlüssel: ID, Wert: Button)
			Dictionary<int, byte> childIDs = new Dictionary<int, byte>();
			Children.ForEach(c =>
			{
				if(!elementIDs.ContainsKey(c.Value))
					childIDs.Add(lastID = c.Value.ToXml(writer, elementIDs, lastID), c.Key);
				else
					childIDs.Add(elementIDs[c.Value], c.Key);
			});

			// Element-Anfangstag schreiben
			writer.WriteStartElement("element");
			{
				// ID generieren und schreiben
				writer.WriteAttributeString("id", (++lastID).ToString());
				elementIDs.Add(this, lastID);

				// Elementtyp schreiben
				writer.WriteAttributeString("type", Type);

				// Interne Werte schreiben
				writer.WriteElementString("age", Age.ToString());
				writer.WriteElementString("id", ID.ToString());
				writer.WriteElementString("name", Name == null ? "" : Name);
				writer.WriteElementString("shadow", ShadowElement.ToString());

				// Toten-ID schreiben
				writer.WriteElementString("deadunit", deadUnitID.ToString());

				// Nachfolger-ID schreiben
				writer.WriteElementString("successor", successorID.ToString());

				// Nachfolger-Technologie-ID schreiben
				writer.WriteElementString("successorresearch", successorResearchID.ToString());

				// Kinder schreiben
				writer.WriteStartElement("children");
				{
					foreach(var c in childIDs)
					{
						// Button-ID schreiben
						writer.WriteStartElement("child");
						writer.WriteAttributeString("button", c.Value.ToString());
						writer.WriteString(c.Key.ToString());
						writer.WriteEndElement();
					}
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			// ID zurückgeben
			return lastID;
		}

		/// <summary>
		/// Liest das aktuelle Element aus dem angegebenen XElement.
		/// </summary>
		/// <param name="element">Das XElement, aus dem das Element erstellt werden soll.</param>
		/// <param name="previousElements">Die bereits eingelesenen Vorgängerelemente, von denen dieses Element abhängig sein kann.</param>
		/// <param name="dat">Die DAT-Datei, aus der ggf. Daten ausgelesen werden können.</param>
		/// <param name="langFiles">Das Language-Datei-Objekt zum Auslesen von Stringdaten.</param>
		public override void FromXml(XElement element, Dictionary<int, TechTreeElement> previousElements, GenieLibrary.GenieFile dat, GenieLibrary.LanguageFileWrapper langFiles)
		{
			// Elemente der Oberklasse lesen
			base.FromXml(element, previousElements, dat, langFiles);

			// Werte einlesen
			int id = (int)element.Element("successor");
			if(id >= 0)
				this.Successor = (TechTreeBuilding)previousElements[id];

			id = (int)element.Element("successorresearch");
			if(id >= 0)
				this.SuccessorResearch = (TechTreeResearch)previousElements[id];

			// Kinder einlesen
			Children = new List<KeyValuePair<byte, TechTreeElement>>();
			foreach(XElement child in element.Element("children").Descendants("child"))
				Children.Add(new KeyValuePair<byte, TechTreeElement>((byte)(uint)child.Attribute("parent"), previousElements[(int)child]));
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeBuilding"; }
		}

		#endregion
	}
}
