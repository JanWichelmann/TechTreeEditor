using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert eine tote Einheit im Technologiebaum.
	/// Diese wird niemals gerendert.
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ID: #{ID}, Name: {Name}")]
	public class TechTreeProjectile : TechTreeUnit, IUpgradeable
	{
		#region Variablen

		/// <summary>
		/// Die zugehörige Tracking-Einheit.
		/// </summary>
		public TechTreeEyeCandy TrackingUnit { get; set; }

		/// <summary>
		/// Die direkte Weiterentwicklung dieses Elements.
		/// </summary>
		public TechTreeUnit Successor { get; set; }

		/// <summary>
		/// Die Technologie, die dieses Element weiterentwickelt.
		/// </summary>
		public TechTreeResearch SuccessorResearch { get; set; }

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Toten-Element.
		/// </summary>
		public TechTreeProjectile()
			: base()
		{
			// Nichts zu tun
		}

		#endregion Funktionen

		#region Funktionen

		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// Pixelbreite des Unterbaums berechnen
			int pixelWidth = TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

			// Ist ein Nachfolger vorhanden?
			// TODO verkürzen
			if(Successor != null)
			{
				// Senkrechte Linie nach unten zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.Lines);
				{
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + RenderControl.BOX_SPACE_VERT); // Oben
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT); // Unten
				}
				GL.End();

				// Hilfsvariablen
				position.Y += RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT;
				int childPixelWidth = 0;
				Point dotFirstChild = position;
				Point dotLastChild = position;
				bool dotFirstChildSet = false;
				int childYOffset = 0;

				// Nachfolger zeichnen
				Successor.Draw(position, ageOffsets, parentAgeOffset + 1);

				// Pixel-Breite berechnen
				childPixelWidth = Successor.TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

				// Aktuelle waagerechte Verbindungslinien-Position bestimmen
				dotLastChild = new Point(position.X + childPixelWidth / 2, position.Y);

				// Ggf. erste waagerechte Verbindungslinien-Position merken
				if(!dotFirstChildSet)
				{
					dotFirstChild = dotLastChild;
					dotFirstChildSet = true;
				}

				// Versatz des Nachfolger-Elements nach unten wegen des Zeitalter-Offsets berechnen
				childYOffset = dotLastChild.Y + Math.Max(ageOffsets[Successor.Age] - parentAgeOffset - 1, 0) * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT);

				// Senkrechte Linie darauf zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.Lines);
				{
					GL.Vertex2(dotLastChild.X, dotLastChild.Y); // Oben
					GL.Vertex2(dotLastChild.X, childYOffset + RenderControl.BOX_SPACE_VERT); // Unten
				}
				GL.End();
			}
		}

		public override void CalculateTreeBounds(ref List<int> ageCounts)
		{
			// Zähler für eigenes Zeitalter inkrementieren
			++ageCounts[Age];

			// Baumbreite muss berechnet werden
			TreeWidth = 0;

			// Ist ein Nachfolgerelement vorhanden?
			if(Successor != null)
			{
				// Baumbreite addieren
				Successor.CalculateTreeBounds(ref ageCounts);
				TreeWidth += Successor.TreeWidth;
			}

			// Die Baumbreite muss mindestens 1 sein
			if(TreeWidth == 0)
				TreeWidth = 1;
		}

		public override List<TechTreeElement> GetChildren()
		{
			// Nachfolgeelement zurückgeben
            List<TechTreeElement> childElements = new List<TechTreeElement>();
			if(Successor != null)
				childElements.Add(Successor);
			return childElements;
		}

		public override List<TechTreeElement> GetVisibleChildren()
		{
			// Nachfolgeelement zurückgeben
			List<TechTreeElement> childElements = new List<TechTreeElement>();
			if(Successor != null)
				childElements.Add(Successor);
			return childElements;
		}

		public override void RemoveChild(TechTreeElement child)
		{
			// Nachfolge-Element?
			if(Successor == child)
			{
				Successor = null;
				SuccessorResearch = null;
			}
		}

		public override void DrawDependencies()
		{
			// Ggf. Linie zur Weiterentwicklungs-Technologie zeichnen
			if(SuccessorResearch != null)
				RenderControl.DrawLine(this, SuccessorResearch, Color.Red, true);
		}

		public override int ToXml(System.Xml.XmlWriter writer, Dictionary<TechTreeElement, int> elementIDs, int lastID)
		{
			// ID generieren
			int myID = ++lastID;
			elementIDs[this] = myID;

			// Toten-ID abrufen
			int deadUnitID = -1;
			if(DeadUnit != null)
			{
				if(!elementIDs.ContainsKey(DeadUnit))
					lastID = DeadUnit.ToXml(writer, elementIDs, lastID);
				deadUnitID = elementIDs[DeadUnit];
			}

			// Tracking-ID abrufen
			int trackUnitID = -1;
			if(TrackingUnit != null)
			{
				if(!elementIDs.ContainsKey(TrackingUnit))
					lastID = TrackingUnit.ToXml(writer, elementIDs, lastID);
				trackUnitID = elementIDs[TrackingUnit];
			}

			// Nachfolger-ID abrufen
			int successorID = -1;
			if(Successor != null)
			{
				if(!elementIDs.ContainsKey(Successor))
					lastID = Successor.ToXml(writer, elementIDs, lastID);
				successorID = elementIDs[Successor];
			}

			// Nachfolger-Technologie-ID abrufen
			int successorResearchID = -1;
			if(SuccessorResearch != null)
			{
				if(!elementIDs.ContainsKey(SuccessorResearch))
					lastID = SuccessorResearch.ToXml(writer, elementIDs, lastID);
				successorResearchID = elementIDs[SuccessorResearch];
			}

			// Element-Anfangstag schreiben
			writer.WriteStartElement("element");
			{
				// ID generieren und schreiben
				writer.WriteAttributeNumber("id", myID);

				// Elementtyp schreiben
				writer.WriteAttributeString("type", Type);

				// Interne Werte schreiben
				writer.WriteElementNumber("age", Age);
				writer.WriteElementNumber("id", ID);
				writer.WriteElementNumber("flags", (int)Flags);
				writer.WriteElementNumber("shadow", ShadowElement);

				// Toten-ID schreiben
				writer.WriteElementNumber("deadunit", deadUnitID);

				// Tracking-ID schreiben
				writer.WriteElementNumber("trackunit", trackUnitID);

				// Nachfolger-ID schreiben
				writer.WriteElementNumber("successor", successorID);

				// Nachfolger-Technologie-ID schreiben
				writer.WriteElementNumber("successorresearch", successorResearchID);
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
			int id = (int)element.Element("trackunit");
			if(id >= 0)
				this.TrackingUnit = (TechTreeEyeCandy)previousElements[id];
			id = (int)element.Element("successor");
			if(id >= 0)
				this.Successor = (TechTreeUnit)previousElements[id];
			id = (int)element.Element("successorresearch");
			if(id >= 0)
				this.SuccessorResearch = (TechTreeResearch)previousElements[id];
		}

		/// <summary>
		/// Zählt die Referenzen zu dem angegebenen Element.
		/// </summary>
		/// <param name="element">Das zu zählende Element.</param>
		/// <returns></returns>
		public override int CountReferencesToElement(TechTreeElement element)
		{
			// Oberklassen zählen lassen
			int counter = base.CountReferencesToElement(element);

			// Zählen
			if(TrackingUnit == element) ++counter;
			if(Successor == element) ++counter;
			if(SuccessorResearch == element) ++counter;

			// Fertig
			return counter;
		}

		#endregion Funktionen

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeProjectile"; }
		}

		#endregion Eigenschaften
	}
}