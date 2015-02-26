using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Linq;
using System.Text;

namespace X2AddOnTechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert eine tote Einheit im Technologiebaum.
	/// Diese wird niemals gerendert.
	/// </summary>
	public class TechTreeDead : TechTreeUnit
	{
		#region Variablen

		#endregion

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

		#endregion

		#region Funktionen

		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// Nichts zu zeichnen
			throw new NotSupportedException("Tote Einheiten können nicht gezeichnet werden!");
		}

		public override void CalculateTreeBounds(ref List<int> ageCounts)
		{
			// Nichts zu zeichnen
			throw new NotSupportedException("Tote Einheiten können nicht gezeichnet werden!");
		}

		protected override List<TechTreeElement> GetChildren()
		{
			// Keine Kinder vorhanden
			return new List<TechTreeElement>();
		}

		public override void DrawDependencies()
		{
			// Nichts zu zeichnen
			throw new NotSupportedException("Tote Einheiten können nicht gezeichnet werden!");
		}

		public override int ToXml(System.Xml.XmlWriter writer, Dictionary<TechTreeElement, int> elementIDs, int lastID)
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
			}
			writer.WriteEndElement();

			// ID zurückgeben
			return lastID;
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeDead"; }
		}

		#endregion
	}
}
