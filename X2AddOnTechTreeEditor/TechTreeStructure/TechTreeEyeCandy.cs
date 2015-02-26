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
	/// Definiert eine Deko-Einheit im Technologiebaum.
	/// </summary>
	public class TechTreeEyeCandy : TechTreeUnit
	{
		#region Variablen

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Deko-Element.
		/// </summary>
		public TechTreeEyeCandy()
			: base()
		{
			
		}

		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// TODO
			throw new NotImplementedException();
		}

		public override void CalculateTreeBounds(ref List<int> ageCounts)
		{
			// TODO
			throw new NotImplementedException();
		}

		protected override List<TechTreeElement> GetChildren()
		{
			// Keine Kinder vorhanden
			return new List<TechTreeElement>();
		}

		public override void DrawDependencies()
		{
			// TODO
			throw new NotImplementedException();
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
			get { return "TechTreeEyeCandy"; }
		}

		#endregion
	}
}
