using System.Collections.Generic;
using System.Drawing;

namespace TechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert eine tote Einheit im Technologiebaum.
	/// Diese wird niemals gerendert.
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ID: #{ID}, Name: {Name}")]
	public class TechTreeDead : TechTreeUnit
	{
		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Toten-Element.
		/// </summary>
		public TechTreeDead()
			: base()
		{
			// Tote Einheiten sind immer verborgen
			ShadowElement = true;
		}

		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// Nichts zu tun
			return;
		}

		public override void CalculateTreeBounds(ref List<int> ageCounts)
		{
			// Zähler für eigenes Zeitalter inkrementieren
			++ageCounts[Age];

			// Der Baum hat immer Breite 1, das Element ist aber kein Standardelement
			TreeWidth = 1;
		}

		public override List<TechTreeElement> GetChildren()
		{
			// Keine Kinder vorhanden
			return new List<TechTreeElement>();
		}

		public override List<TechTreeElement> GetVisibleChildren()
		{
			// Keine Kinder vorhanden
			return new List<TechTreeElement>();
		}

		public override void RemoveChild(TechTreeElement child)
		{
			// Dieses Element hat keine Kinder
		}

		public override void DrawDependencies()
		{
			// Nichts zu tun
			return;
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

			// Ggf. in den Fähigkeiten referenzierte Einheiten schreiben
			foreach(UnitAbility ab in Abilities)
				if(ab.Unit != null && !elementIDs.ContainsKey(ab.Unit))
					lastID = ab.Unit.ToXml(writer, elementIDs, lastID);

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

				// Fähigkeiten schreiben
				writer.WriteStartElement("abilities");
				{
					// Fähigkeit schreiben
					Abilities.ForEach(a => a.ToXml(writer, elementIDs));
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			// ID zurückgeben
			return lastID;
		}

		#endregion Funktionen

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeDead"; }
		}

		#endregion Eigenschaften
	}
}