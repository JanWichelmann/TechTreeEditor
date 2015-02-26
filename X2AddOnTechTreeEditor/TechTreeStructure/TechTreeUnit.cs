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
	/// Definiert ein Einheit-Element im Technologiebaum.
	/// </summary>
	public abstract class TechTreeUnit : TechTreeElement
	{
		#region Variablen

		/// <summary>
		/// Die zugehörige DAT-Einheit.
		/// </summary>
		public GenieLibrary.DataElements.Civ.Unit DATUnit { get; set; }

		/// <summary>
		/// Die zugehörige tote Einheit.
		/// </summary>
		public TechTreeUnit DeadUnit { get; set; }

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Einheit-Element.
		/// </summary>
		public TechTreeUnit()
			: base()
		{

		}

		/// <summary>
		/// Liest das aktuelle Element aus dem angegebenen XElement.
		/// </summary>
		/// <param name="element">Das XElement, aus dem das Element erstellt werden soll.</param>
		/// <param name="previousElements">Die bereits eingelesenen Vorgängerelemente, von denen dieses Element abhängig sein kann.</param>
		public override void FromXml(XElement element, Dictionary<int, TechTreeElement> previousElements)
		{
			// Elemente der Oberklasse lesen
			base.FromXml(element, previousElements);

			// Werte einlesen
			int id = (int)element.Element("deadunit");
			if(id >= 0)
				this.DeadUnit = (TechTreeDead)previousElements[id];
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeUnit"; }
		}

		#endregion
	}
}
