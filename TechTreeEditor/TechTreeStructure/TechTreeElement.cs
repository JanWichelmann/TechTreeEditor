using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace TechTreeEditor.TechTreeStructure
{
	/// <summary>
	/// Definiert ein allgemeines Techtree-Element.
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ID: #{ID}, Name: {Name}")]
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
		/// Die Element-Flags.
		/// </summary>
		public ElementFlags Flags { get; set; }

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
		/// Wird lediglich als Hilfsvariable beim Zeichnen abgefragt.
		/// </summary>
		public bool ShadowElement { get; set; }

		/// <summary>
		/// Enthält die Liste der Gebäude, von denen dieses Element abhängig ist. Der Boolean-Wert gibt an, ob noch ein weiteres Gebäude erforderlich ist.
		/// </summary>
		public Dictionary<TechTreeBuilding, bool> BuildingDependencies { get; protected set; }

		#endregion Öffentlich

		#region Geschützt

		/// <summary>
		/// Die Positionswerte des Element-Kästchens.
		/// </summary>
		protected Rectangle _cacheBoxPosition;

		/// <summary>
		/// Gibt an, ob das Element ein Standard-Element ist.
		/// </summary>
		protected bool _standardElement = false;

		#endregion Geschützt

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
		/// Erstellt die Icon-Textur.
		/// </summary>
		/// <param name="textureFunc">Die Textur-Generierungsfunktion.</param>
		public abstract void CreateIconTexture(Func<string, short, int> textureFunc);

		/// <summary>
		/// Ruft eine Liste mit den Kindelementen ab. Dies wird zur Umsetzung von Such-Rekursionen u.ä. benutzt.
		/// </summary>
		/// <returns></returns>
		public abstract List<TechTreeElement> GetChildren();

		/// <summary>
		/// Ruft eine Liste mit den sichtbaren (= gerenderten) Kindelementen ab.
		/// Dies wird zur schöneren Umsetzung der Filterfunktion benutzt, damit Schattenelemente nicht als Kind betrachtet werden.
		/// </summary>
		/// <returns></returns>
		public abstract List<TechTreeElement> GetVisibleChildren();

		/// <summary>
		/// Gibt das übergebene Element frei, falls es diesem Element untergeordnet sein sollte.
		/// </summary>
		/// <param name="child">Das freizugebende Element.</param>
		/// <returns></returns>
		public abstract void RemoveChild(TechTreeElement child);

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

		/// <summary>
		/// Aktualisiert den Element-Namen.
		/// </summary>
		/// <param name="langFiles">Das Language-Datei-Objekt zum Auslesen von Stringdaten.</param>
		public abstract void UpdateName(GenieLibrary.LanguageFileWrapper langFiles);

		#endregion Abstrakte Funktionen

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
			BuildingDependencies = new Dictionary<TechTreeBuilding, bool>();
		}

		/// <summary>
		/// Zeichnet das Element und den Unterbaum an der angegebenen Position.
		/// </summary>
		/// <param name="position">Die Zeichenposition des gesamten Baum-Rechtecks. Es wird die obere linke Ecke angegeben.</param>
		/// <param name="ageOffsets">Eine Liste mit den Zeitalter-Offset-Daten. Diese wird in dieser Funktion nicht verändert.</param>
		/// <param name="parentAgeOffset">Das Zeitalter-Offset des übergeordneten Elements.</param>
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
				// Gecachten Positionswert aktualisieren
				_cacheBoxPosition = new Rectangle(position.X + (pixelWidth - RenderControl.BOX_BOUNDS) / 2, position.Y + RenderControl.BOX_SPACE_VERT, RenderControl.BOX_BOUNDS, RenderControl.BOX_BOUNDS);

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

				// Flag-Icons zeichnen
				if((Flags & ElementFlags.ShowInEditor) == ElementFlags.ShowInEditor)
					RenderControl.DrawFlagOverlay(ElementFlags.ShowInEditor);
				if((Flags & ElementFlags.GaiaOnly) == ElementFlags.GaiaOnly)
					RenderControl.DrawFlagOverlay(ElementFlags.GaiaOnly);
				if((Flags & ElementFlags.LockID) == ElementFlags.LockID)
					RenderControl.DrawFlagOverlay(ElementFlags.LockID);
				if((Flags & ElementFlags.Blocked) == ElementFlags.Blocked)
					RenderControl.DrawFlagOverlay(ElementFlags.Blocked);
				if((Flags & ElementFlags.Free) == ElementFlags.Free)
					RenderControl.DrawFlagOverlay(ElementFlags.Free);

				// Textur entladen
				GL.BindTexture(TextureTarget.Texture2D, 0);

				// Falls Element überfahren, einen Auswahlrahmen zeichnen
				if(Selected)
				{
					GL.Color4(Color.FromArgb(128, 28, 28));
					GL.LineWidth(3);
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
					GL.Color4(Color.FromArgb(166, 94, 94));
					GL.LineWidth(3);
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
			Flags = (ElementFlags)(int)element.Element("flags");
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

				case "TechTreeProjectile":
					return new TechTreeProjectile();

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

		/// <summary>
		/// Gibt die Icon-Textur frei.
		/// </summary>
		public void FreeIconTexture()
		{
			// Textur löschen
			if(IconTextureID > 0)
			{
				GL.DeleteTexture(IconTextureID);
				IconTextureID = 0;
			}
		}

		/// <summary>
		/// Zählt die Referenzen zu dem angegebenen Element.
		/// </summary>
		/// <param name="element">Das zu zählende Element.</param>
		/// <returns></returns>
		public virtual int CountReferencesToElement(TechTreeElement element)
		{
			// Gebäude-Abhängigkeiten zählen
			int counter = 0;
			foreach(var dep in BuildingDependencies)
				if(dep.Key == element)
					++counter;
			return counter;
		}

		/// <summary>
		/// Prüft, ob das übergebene Element eines der Kindelemente dieses Elements ist.
		/// </summary>
		/// <param name="element">Das zu suchende Element.</param>
		/// <returns></returns>
		public bool HasChild(TechTreeElement element)
		{
			// Kind-Element?
			if(this == element)
				return true;

			// Rekursiver Aufruf
			return GetChildren().Exists(c => c.HasChild(element));
		}

		/// <summary>
		/// Prüft, ob eines der Kindelemente in seinem Namen einen der gegebenen Suchstrings enthält.
		/// </summary>
		/// <param name="searchStrings">Die zu suchenden Strings.</param>
		/// <param name="onlyVisibleElements">Optional. Gibt an, ob nur ssichtbare (= gerenderte) Elemente besucht werden sollen.</param>
		/// <returns></returns>
		public bool HasChildWithName(string[] searchStrings, bool onlyVisibleElements = false)
		{
			// Suchstring durchlaufen
			foreach(string search in searchStrings)
			{
				// Kind-Element?
				if(Name.Contains(search, StringComparison.OrdinalIgnoreCase))
					return true;

				// Rekursiver Aufruf
				bool found;
				if(onlyVisibleElements)
					found = GetVisibleChildren().Exists(c => c.HasChildWithName(searchStrings, true));
				else
					found = GetChildren().Exists(c => c.HasChildWithName(searchStrings));

				// Gefunden? => Abbrechen
				if(found)
					return true;
			}

			// Nichts gefunden
			return false;
		}

		/// <summary>
		/// Prüft, ob das übergebene Element Kindelemente hat.
		/// </summary>
		/// <returns></returns>
		public bool HasChildren()
		{
			// Kinder vorhanden?
			return GetChildren().Count > 0;
		}

		/// <summary>
		/// Gibt die String-Repräsentation dieses Elements zurück.
		/// Dies ist normalerweise der Wert der "Name"-Eigenschaft.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			// Namen zurückgeben
			return Name;
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

		#endregion Eigenschaften

		#region Enumerationen

		/// <summary>
		/// Definiert Element-spezifische Flags.
		/// </summary>
		[Flags]
		public enum ElementFlags : int
		{
			/// <summary>
			/// Kein Flag gesetzt.
			/// </summary>
			None = 0,

			/// <summary>
			/// Macht das Element im Editor verfügbar.
			/// </summary>
			ShowInEditor = 1,

			/// <summary>
			/// Schaltet das Element nur für den Spieler Gaia frei.
			/// </summary>
			GaiaOnly = 2,

			/// <summary>
			/// Schützt die ID des Elements. Diese wird beim Erstellen der neuen DAT beibehalten werden.
			/// </summary>
			LockID = 4,

			/// <summary>
			/// Markiert das Element in der aktuell ausgewählten Kultur als blockiert. Hat nur Auswirkungen auf das Rendering.
			/// </summary>
			Blocked = 8,

			/// <summary>
			/// Markiert das Element in der aktuell ausgewählten Kultur als kostenfrei. Hat nur Auswirkungen auf das Rendering.
			/// </summary>
			Free = 16,

			/// <summary>
			/// Definiert alle Flags, die ausschließlich für das Rendering relevant sind.
			/// </summary>
			RenderingFlags = 24
		}

		#endregion Enumerationen
	}
}