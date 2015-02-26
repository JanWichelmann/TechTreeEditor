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
		/// <param name="dat">Die DAT-Datei, aus der ggf. Daten ausgelesen werden können.</param>
		/// <param name="langFiles">Das Language-Datei-Objekt zum Auslesen von Stringdaten.</param>
		public override void FromXml(XElement element, Dictionary<int, TechTreeElement> previousElements, GenieLibrary.GenieFile dat, GenieLibrary.LanguageFileWrapper langFiles)
		{
			// Elemente der Oberklasse lesen
			base.FromXml(element, previousElements, dat, langFiles);

			// Werte einlesen
			int id = (int)element.Element("deadunit");
			if(id >= 0)
				this.DeadUnit = (TechTreeUnit)previousElements[id];

			// DAT-Einheit laden
			DATUnit = dat.Civs[1].Units[ID];

			// Name laden
			Name = langFiles.GetString(DATUnit.LanguageDLLName) + " [" + DATUnit.Name1.TrimEnd('\0') + "]";
		}

		/// <summary>
		/// Erstellt für alle Kindelemente die Texturen.
		/// </summary>
		/// <param name="textureFunc">Die Textur-Generierungsfunktion.</param>
		public override void CreateIconTextures(Func<string, short, int> textureFunc)
		{
			// Schon ein Icon vorhanden? => Abbrechen
			if(IconTextureID > 0)
				return;

			// Icon erstellen
			IconTextureID = textureFunc(Type, DATUnit.IconID);

			// Funktion in der Basisklasse aufrufen
			base.CreateIconTextures(textureFunc);
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
