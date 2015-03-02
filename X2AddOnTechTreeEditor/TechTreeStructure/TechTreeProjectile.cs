using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace X2AddOnTechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert eine tote Einheit im Technologiebaum.
	/// Diese wird niemals gerendert.
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ID: #{ID}, Name: {Name}")]
	public class TechTreeProjectile : TechTreeUnit
	{
		#region Variablen

		/// <summary>
		/// Die zugehörige Tracking-Einheit.
		/// </summary>
		public TechTreeEyeCandy TrackingUnit { get; set; }

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Toten-Element.
		/// </summary>
		public TechTreeProjectile()
			: base()
		{
			// Projektile sind immer verborgen
			ShadowElement = true;
		}

		#endregion

		#region Funktionen

		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// Nichts zu zeichnen
			throw new NotSupportedException("Projektile können nicht gezeichnet werden!");
		}

		public override void CalculateTreeBounds(ref List<int> ageCounts)
		{
			// Nichts zu zeichnen
			throw new NotSupportedException("Projektile können nicht gezeichnet werden!");
		}

		protected override List<TechTreeElement> GetChildren()
		{
			// Keine Kinder vorhanden
			return new List<TechTreeElement>();
		}

		public override void DrawDependencies()
		{
			// Nichts zu zeichnen
			throw new NotSupportedException("Projektile können nicht gezeichnet werden!");
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

			// Fertig
			return counter;
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeProjectile"; }
		}

		#endregion
	}
}
