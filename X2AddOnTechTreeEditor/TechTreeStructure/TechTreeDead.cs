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
	[System.Diagnostics.DebuggerDisplay("ID: #{ID}, Name: {Name}")]
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

		public override List<TechTreeElement> GetChildren()
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
			// Nichts zu zeichnen
			throw new NotSupportedException("Tote Einheiten können nicht gezeichnet werden!");
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
