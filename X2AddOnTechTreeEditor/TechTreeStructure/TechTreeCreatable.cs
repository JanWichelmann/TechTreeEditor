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
	/// Definiert ein Erschaffbare-Einheit-Element im Technologiebaum.
	/// </summary>
	public class TechTreeCreatable : TechTreeUnit
	{
		#region Variablen

		/// <summary>
		/// Die direkte Weiterentwicklung dieses Elements.
		/// </summary>
		public TechTreeCreatable Successor { get; set; }

		/// <summary>
		/// Die Technologie, die dieses Element weiterentwickelt.
		/// </summary>
		public TechTreeResearch SuccessorResearch { get; set; }

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Erschaffbare-Einheit-Element.
		/// </summary>
		/// <param name="age">Das Zeitalter, ab dem das Element verfügbar ist.</param>
		public TechTreeCreatable()
			: base()
		{

		}

		/// <summary>
		/// Zeichnet den Unterbaum der Einheit an der angegebenen Position.
		/// </summary>
		/// <param name="position">Die Zeichenposition des gesamten Baum-Rechtecks. Es wird die obere linke Ecke angegeben.</param>
		/// <param name="ageOffsets">Eine Liste mit den Zeitalter-Offset-Daten. Diese wird in dieser Funktion nicht verändert.</param>
		/// <param name="lastAge">Das Zeitalter-Offset des übergeordneten Elements.</param>
		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// (Breiten-)Mittelpunkt dieses Elements berechnen
			int pixelWidth = (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI) / 2;

			// Ist ein Nachfolger vorhanden?
			if(Successor != null)
			{
				// Nachfolger zeichnen
				position.Y += RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT;
				Successor.Draw(position, ageOffsets, parentAgeOffset + 1);

				// Versatz des Nachfolger-Elements nach unten wegen des Zeitalter-Offsets berechnen
				int childYOffset = position.Y + Math.Max(ageOffsets[Successor.Age] - parentAgeOffset - 1, 0) * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT);

				// Senkrechte Verbindungslinie nach unten zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.Lines);
				{
					GL.Vertex2(position.X + pixelWidth, position.Y + RenderControl.BOX_BOUNDS + RenderControl.BOX_SPACE_VERT); // Oben
					GL.Vertex2(position.X + pixelWidth, childYOffset + RenderControl.BOX_SPACE_VERT); // Unten
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

			// Nachfolge-Element vorhanden?
			if(Successor == null)
			{
				// Der Baum ist genau 1 Element breit
				TreeWidth = 1;
			}
			else
			{
				// Die Breite des Baums ist die Breite des Unterbaums
				List<int> childAgeCounts = null;
				childAgeCounts = new List<int>(new List<int>(ageCounts));
				Successor.CalculateTreeBounds(ref childAgeCounts);

				// Unterbaumbreite zur Gesamtbreite hinzuaddieren
				TreeWidth = Successor.TreeWidth;

				// Zurückgegebene Zeitalter-Werte abgleichen => es wird immer das Maximum pro Zeitalter genommen
				ageCounts = ageCounts.Zip(childAgeCounts, (a1, a2) => Math.Max(a1, a2)).ToList();
			}
		}

		/// <summary>
		/// Ruft eine Liste mit den Kindelementen ab.
		/// </summary>
		/// <returns></returns>
		protected override List<TechTreeElement> GetChildren()
		{
			// Kinder zurückgeben
			if(Successor != null)
				return new List<TechTreeElement>(new TechTreeElement[] { Successor });
			return new List<TechTreeElement>();
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
				this.Successor = (TechTreeCreatable)previousElements[id];

			id = (int)element.Element("successorresearch");
			if(id >= 0)
				this.SuccessorResearch = (TechTreeResearch)previousElements[id];
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeCreatable"; }
		}

		#endregion
	}
}
