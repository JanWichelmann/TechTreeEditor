using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

// TODO: Datenstruktur komplett umschreiben, am besten Ober-/Unterklassen-Modell zur Unterscheidung von Gebäuden, Einheiten und Technologien.
// Die Struktur muss den regulären Techtree möglichst sauber und zeichenbar wiedergeben können, erstmal keine Gedanken zur Generierung machen.
// Weiterentwicklungen und zugehörige Technologien müssen irgendwie sinnvoll implementiert werden.
// Wie werden die Kulturunterschiede gehandhabt? => Einfach "Deaktivierung" von Einheiten/Technologien? => DAT-Struktur erstmal nicht beachten, Konvertierung kommt später.
// Kultur-Unterschiede betrifft ja primär erstmal Civ-Boni, d.h. automatische Technologien. Wenn man die hier komplett rauslässt, sollte das erstmal keine Probleme machen.
// Tote Einheiten / Annex-Einheiten usw. nicht vergessen! Die als "Schattenobjekte" hinterlegen. Derartige Parameter falls möglich von der DAT lösen, um absolute ID-Freiheit zu bekommen.

namespace X2AddOnTechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert ein allgemeines Techtree-Element.
	/// </summary>
	public abstract class TechTreeElement
	{
		#region Variablen

		#region Öffentlich

		/// <summary>
		/// Das Zeitalter, ab dem das Element verfügbar ist, 0-basiert.
		/// </summary>
		public int Age { get; set; }

		/// <summary>
		/// Die DAT-ID des Elements.
		/// Diese ID muss nicht zwangsläufig in der generierten Ergebnis-DAT verwendet werden.
		/// Sie dient lediglich dazu, ein Element während der Laufzeit effizient referenzieren zu können.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Der Anzeigename des Elements.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Die ID der Icon-Textur.
		/// </summary>
		public int IconTextureID { get; set; }

		/// <summary>
		/// Die Breite des Unterbaums. Kann mithilfe der CalculateTreeBounds-Methode aktualisiert werden.
		/// </summary>
		public int TreeWidth { get; set; }

		/// <summary>
		/// Gibt an, ob das Feld aktuell ausgewählt ist. Wird lediglich als Hilfsvariable beim Zeichnen abgefragt.
		/// </summary>
		public bool Selected { get; set; }

		/// <summary>
		/// Gibt an, ob das Feld aktuell überfahren ist. Wird lediglich als Hilfsvariable beim Zeichnen abgefragt.
		/// </summary>
		public bool Hovered { get; set; }

		/// <summary>
		/// Gibt an, ob dieses Element ein "Schattenelement" ist, d.h. zwar referenziert, aber im Baum nicht angezeigt wird.
		/// </summary>
		public bool ShadowElement { get; set; }

		#endregion Öffentlich

		#region Geschützt

		/// <summary>
		/// Gibt an, ob die gecachten Werte gültig sind.
		/// </summary>
		protected bool _cacheValid = false;

		/// <summary>
		/// Die Positionswerte des Element-Kästchens.
		/// </summary>
		protected Rectangle _cacheBoxPosition;

		#endregion Privat

		#endregion Variablen

		#region Abstrakte Funktionen

		/// <summary>
		/// Zeichnet den Unterbaum des Elements an der angegebenen Position.
		/// </summary>
		/// <param name="position">Die Zeichenposition des gesamten Baum-Rechtecks. Es wird die obere linke Ecke angegeben.</param>
		/// <param name="ageOffsets">Eine Liste mit den Zeitalter-Offset-Daten. Diese wird in dieser Funktion nicht verändert.</param>
		/// <param name="lastAge">Das Zeitalter-Offset des übergeordneten Elements.</param>
		protected abstract void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset);

		/// <summary>
		/// Berechnet die rekursiv die maximale Breite des Unterbaums sowie die Tiefe der jeweiligen Zeitalter-Elemente.
		/// </summary>
		/// <param name="ageCounts">Die aktuelle Liste mit den Tiefen der Elemente pro Zeitalter.</param>
		public abstract void CalculateTreeBounds(ref List<int> ageCounts);

		/// <summary>
		/// Ruft eine Liste mit den Kindelementen ab. Dies wird zur Umsetzung von Such-Rekursionen u.ä. benutzt.
		/// </summary>
		/// <returns></returns>
		protected abstract List<TechTreeElement> GetChildren();

		/// <summary>
		/// Zeichnet Pfeile zu den Abhängigkeiten dieses Elements. Sollte nur nach der Draw-Methode aufgerufen werden, damit die Elementpositionen vorberechnet und gecached sind.
		/// </summary>
		public abstract void DrawDependencies();

		/// <summary>
		/// Konvertiert die enthaltenen Daten in ein XML-Format und schreibt sie in den übergebenen XmlWriter.
		/// Die generierte Element-ID wird dabei zurückgegeben.
		/// </summary>
		/// <param name="writer">Der XmlWriter, in den die Daten geschrieben werden sollen.</param>
		/// <param name="elementIDs">Die Liste mit den den TechTree-Elementen zugeordneten IDs.</param>
		/// <param name="lastID">Die letzte vergebene ID. Muss als Referenz übergeben werden, da diese Zahl stetig inkrementiert wird.</param>
		public abstract int ToXml(XmlWriter writer, Dictionary<TechTreeElement, int> elementIDs, int lastID);

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		protected TechTreeElement()
		{
			// Standardwerte setzen
			Age = 0;
			TreeWidth = 1;
			IconTextureID = 0;
			Selected = false;
			Hovered = false;
			ShadowElement = false;
		}

		/// <summary>
		/// Zeichnet das Element und den Unterbaum an der angegebenen Position.
		/// </summary>
		/// <param name="position">Die Zeichenposition des gesamten Baum-Rechtecks. Es wird die obere linke Ecke angegeben.</param>
		/// <param name="ageOffsets">Eine Liste mit den Zeitalter-Offset-Daten. Diese wird in dieser Funktion nicht verändert.</param>
		/// <param name="lastAge">Das Zeitalter-Offset des übergeordneten Elements.</param>
		public void Draw(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// Falls das Eltern-Zeitalter-Offset nicht dem aktuellen Zeitalter entspricht, entsprechend vertikal verschieben
			while(parentAgeOffset < ageOffsets[Age])
			{
				// Offset erhöhen
				++parentAgeOffset;

				// Position um ein Kästchen nach unten verschieben
				position.Y += RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT;
			}

			// Pixelbreite des Unterbaums berechnen
			int pixelWidth = TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

			// Eigene Box in die Mitte zeichnen
			{
				// Ggf. gecachten Positionswert aktualisieren
				if(!_cacheValid)
				{
					_cacheBoxPosition = new Rectangle(position.X + (pixelWidth - RenderControl.BOX_BOUNDS) / 2, position.Y + RenderControl.BOX_SPACE_VERT, RenderControl.BOX_BOUNDS, RenderControl.BOX_BOUNDS);
					_cacheValid = true;
				}

				// Farbe setzen und Textur laden
				GL.Color4(Color.White);
				GL.BindTexture(TextureTarget.Texture2D, IconTextureID);

				// Zur kürzeren Zeichenfunktion die Matrix verschieben
				GL.PushMatrix();
				GL.Translate(_cacheBoxPosition.X, _cacheBoxPosition.Y, 0);

				// Zeichnen
				GL.Begin(PrimitiveType.Quads);
				{
					GL.TexCoord2(0.0, 0.0); GL.Vertex2(0, 0); // Oben links
					GL.TexCoord2(1.0, 0.0); GL.Vertex2(RenderControl.BOX_BOUNDS, 0); // Oben rechts
					GL.TexCoord2(1.0, 1.0); GL.Vertex2(RenderControl.BOX_BOUNDS, RenderControl.BOX_BOUNDS); // Unten rechts
					GL.TexCoord2(0.0, 1.0); GL.Vertex2(0, RenderControl.BOX_BOUNDS); // Unten links
				}
				GL.End();

				// Textur entladen
				GL.BindTexture(TextureTarget.Texture2D, 0);

				// Falls Element überfahren, einen Auswahlrahmen zeichnen
				if(Selected)
				{
					GL.Color4(Color.Red);
					GL.LineWidth(2);
					GL.Begin(PrimitiveType.LineLoop);
					{
						GL.Vertex2(0, 0); // Oben links
						GL.Vertex2(RenderControl.BOX_BOUNDS, 0); // Oben rechts
						GL.Vertex2(RenderControl.BOX_BOUNDS, RenderControl.BOX_BOUNDS); // Unten rechts
						GL.Vertex2(0, RenderControl.BOX_BOUNDS); // Unten links
					}
					GL.End();
					GL.LineWidth(1);
				}
				else if(Hovered)
				{
					// Falls Element ausgewählt, einen Auswahlrahmen zeichnen
					GL.Color4(Color.Gold);
					GL.LineWidth(2);
					GL.Begin(PrimitiveType.LineLoop);
					{
						GL.Vertex2(0, 0); // Oben links
						GL.Vertex2(RenderControl.BOX_BOUNDS, 0); // Oben rechts
						GL.Vertex2(RenderControl.BOX_BOUNDS, RenderControl.BOX_BOUNDS); // Unten rechts
						GL.Vertex2(0, RenderControl.BOX_BOUNDS); // Unten links
					}
					GL.End();
					GL.LineWidth(1);
				}

				// Matrix wiederherstellen
				GL.PopMatrix();
			}

			// Unterbaum zeichnen
			DrawChildren(position, ageOffsets, parentAgeOffset);
		}

		/// <summary>
		/// Sucht rekursiv das Element, dessen Kästchen den angegebenen Bildpunkt enthält.
		/// </summary>
		/// <param name="point">Der zu suchende Bildpunkt.</param>
		/// <returns></returns>
		public TechTreeElement FindBox(Point point)
		{
			// Enthält das aktuelle Element den gegebenen Punkt?
			if(_cacheBoxPosition.Contains(point))
				return this;

			// Rekursiv in den Kind-Elementen suchen
			TechTreeElement res = null;
			foreach(TechTreeElement child in GetChildren())
			{
				if((res = child.FindBox(point)) != null)
					break;
			}

			// Fertig
			return res;
		}

		/// <summary>
		/// Sucht rekursiv das Element mit der gegebenen ID.
		/// </summary>
		/// <param name="id">Die zu suchende ID.</param>
		/// <returns></returns>
		public TechTreeElement FindElementWithID(int id)
		{
			// Ist dieses Element das gesuchte?
			if(ID == id)
				return this;

			// Rekursiv in den Kind-Elementen suchen
			TechTreeElement res = null;
			foreach(TechTreeElement child in GetChildren())
			{
				if((res = child.FindElementWithID(id)) != null)
					break;
			}

			// Fertig
			return res;
		}

		/// <summary>
		/// Sucht rekursiv das Element mit der gegebenen ID und dem gegebenen Typen.
		/// </summary>
		/// <param name="id">Die zu suchende ID.</param>
		/// <param name="type">Der Typ des zu suchenden Elements.</param>
		/// <returns></returns>
		public TechTreeElement FindElementWithID(int id, Type type)
		{
			// Ist dieses Element das gesuchte?
			if(ID == id && this.GetType() == type)
				return this;

			// Rekursiv in den Kind-Elementen suchen
			TechTreeElement res = null;
			foreach(TechTreeElement child in GetChildren())
			{
				if((res = child.FindElementWithID(id, type)) != null)
					break;
			}

			// Fertig
			return res;
		}

		/// <summary>
		/// Sucht rekursiv das Element mit dem gegebenen Typen. Es wird das erste tiefste Element zurückgegeben.
		/// </summary>
		/// <param name="type">Der Typ des zu suchenden Elements.</param>
		/// <returns></returns>
		public TechTreeElement FindElementWithType(Type type)
		{
			// Rekursiv in den Kind-Elementen suchen
			TechTreeElement res = null;
			foreach(TechTreeElement child in GetChildren())
			{
				if(child.GetType() == type)
					if((res = child.FindElementWithType(type)) != null)
						break;
			}

			// Dieses Element ist das gesuchte
			if(res == null)
				return this;

			// Fertig
			return res;
		}

		/// <summary>
		/// Liest das aktuelle Element aus dem angegebenen XElement.
		/// </summary>
		/// <param name="element">Das XElement, aus dem das Element erstellt werden soll.</param>
		/// <param name="previousElements">Die bereits eingelesenen Vorgängerelemente, von denen dieses Element abhängig sein kann.</param>
		/// <param name="dat">Die DAT-Datei, aus der ggf. Daten ausgelesen werden können.</param>
		/// <param name="langFiles">Das Language-Datei-Objekt zum Auslesen von Stringdaten.</param>
		public virtual void FromXml(XElement element, Dictionary<int, TechTreeElement> previousElements, GenieLibrary.GenieFile dat, GenieLibrary.LanguageFileWrapper langFiles)
		{
			// Werte einlesen
			Age = (int)element.Element("age");
			ID = (int)element.Element("id");
			Name = (string)element.Element("name");
			ShadowElement = (bool)element.Element("shadow");
		}

		/// <summary>
		/// Erstellt ein leeres TechTree-Element basierend auf dem übergebenen Typ.
		/// </summary>
		/// <param name="type">Der Elementtyp als String. Dies ist einfach der zugehörige Klassenname.</param>
		public static TechTreeElement CreateFromType(string type)
		{
			// Nach Typen vorgehen
			// Als Zeitalter wird ggf. ein Dummy-Wert übergeben
			switch(type)
			{
				case "TechTreeBuilding":
					return new TechTreeBuilding();
				case "TechTreeCreatable":
					return new TechTreeCreatable();
				case "TechTreeDead":
					return new TechTreeDead();
				case "TechTreeEyeCandy":
					return new TechTreeEyeCandy();
				case "TechTreeResearch":
					return new TechTreeResearch();
				default:
					throw new ArgumentException("Ungültiger Elementtyp.");
			}
		}

		/// <summary>
		/// Erstellt für alle Kindelemente die Texturen.
		/// </summary>
		/// <param name="textureFunc">Die Textur-Generierungsfunktion.</param>
		public virtual void CreateIconTextures(Func<string, short, int> textureFunc)
		{
			// Aufruf für alle Kindelemente durchführen
			GetChildren().ForEach(c => c.CreateIconTextures(textureFunc));
		}

		#endregion Funktionen

		#region Eigenschaften

		/// <summary>
		/// Ruft die gecachten Positionswerte des Element-Kästchens ab.
		/// </summary>
		public Rectangle CacheBoxPosition
		{
			get { return _cacheBoxPosition; }
		}

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public virtual string Type
		{
			get { return "TechTreeElement"; }
		}

		#endregion
	}
}
