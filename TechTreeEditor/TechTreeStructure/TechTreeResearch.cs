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
	/// Definiert ein Technologie-Element im Technologiebaum.
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ID: #{ID}, Name: {Name}")]
	public class TechTreeResearch : TechTreeElement
	{
		#region Variablen

		/// <summary>
		/// Die Technologie-Abhängigkeiten dieses Elements. Die Zahl gibt an, wie viele Abhängigkeiten *zusätzlich* erfüllt sein müssen. -1 steht für alle Abhängigkeiten.
		/// </summary>
		public Dictionary<TechTreeResearch, int> Dependencies { get; private set; }

		/// <summary>
		/// Die Technologien, die an demselben (Button-)Slot wie diese Technologie liegen und direkt von ihr abhängig sind.
		/// </summary>
		public List<TechTreeResearch> Successors { get; private set; }

		/// <summary>
		/// Die Technologie-Effekte.
		/// </summary>
		public List<TechEffect> Effects { get; private set; }

		/// <summary>
		/// Die zugehörige DAT-Technologie.
		/// </summary>
		public GenieLibrary.DataElements.Research DATResearch { get; set; }

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Technologie-Element.
		/// </summary>
		public TechTreeResearch()
			: base()
		{
			// Objekte erstellen
			Dependencies = new Dictionary<TechTreeResearch, int>();
			Successors = new List<TechTreeResearch>();
			Effects = new List<TechEffect>();

			// Technologien sind in jedem Fall Standard-Elemente
			_standardElement = true;
		}

		/// <summary>
		/// Zeichnet den Unterbaum der Technologie an der angegebenen Position.
		/// </summary>
		/// <param name="position">Die Zeichenposition des gesamten Baum-Rechtecks. Es wird die obere linke Ecke angegeben.</param>
		/// <param name="ageOffsets">Eine Liste mit den Zeitalter-Offset-Daten. Diese wird in dieser Funktion nicht verändert.</param>
		/// <param name="parentAgeOffset">Das Zeitalter-Offset des übergeordneten Elements.</param>
		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// (Breiten-)Mittelpunkt dieses Elements berechnen
			int pixelWidth = TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

			// Sind Nachfolger vorhanden?
			if(Successors.Count != 0)
			{
				// Senkrechte Linie nach unten zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.Lines);
				{
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + RenderControl.BOX_SPACE_VERT); // Oben
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT); // Unten
				}
				GL.End();

				// Nachfolger zeichnen
				position.Y += RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT;
				int successorPixelWidth = 0;
				Point dotFirstSuccessor = position;
				Point dotLastSuccessor = position;
				bool dotFirstSuccessorSet = false;
				int successorYOffset = 0;
				foreach(TechTreeResearch successor in Successors)
				{
					// Nachfolger versetzt zeichnen
					successor.Draw(position, ageOffsets, parentAgeOffset + 1);

					// Pixel-Breite berechnen
					successorPixelWidth = successor.TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

					// Aktuelle waagerechte Verbindungslinien-Position bestimmen
					dotLastSuccessor = new Point(position.X + successorPixelWidth / 2, position.Y);

					// Ggf. erste waagerechte Verbindungslinien-Position merken
					if(!dotFirstSuccessorSet)
					{
						dotFirstSuccessor = dotLastSuccessor;
						dotFirstSuccessorSet = true;
					}

					// Versatz des Nachfolger-Elements nach unten wegen des Zeitalter-Offsets berechnen
					successorYOffset = dotLastSuccessor.Y + Math.Max(ageOffsets[successor.Age] - parentAgeOffset - 1, 0) * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT);

					// Senkrechte Linie darauf zeichnen
					GL.Color3(Color.Black);
					GL.Begin(PrimitiveType.Lines);
					{
						GL.Vertex2(dotLastSuccessor.X, dotLastSuccessor.Y); // Oben
						GL.Vertex2(dotLastSuccessor.X, successorYOffset + RenderControl.BOX_SPACE_VERT); // Unten
					}
					GL.End();

					// Position erhöhen
					position.X += successorPixelWidth;
				}

				// Waagerechte Verbindungslinie zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.Lines);
				{
					GL.Vertex2(dotFirstSuccessor.X, dotFirstSuccessor.Y); // Oben
					GL.Vertex2(dotLastSuccessor.X + 1, dotLastSuccessor.Y); // Unten (+ 1, da sonst ein Pixel am Ende fehlt)
				}
				GL.End();
			}
		}

		/// <summary>
		/// Zeichnet Pfeile zu den Abhängigkeiten dieses Elements.
		/// </summary>
		public override void DrawDependencies()
		{
			// Linie zu jedem Element zeichnen
			foreach(var dep in Dependencies)
				RenderControl.DrawLine(this, dep.Key, Color.Red, true);
			foreach(var dep in BuildingDependencies)
				RenderControl.DrawLine(this, dep.Key, Color.Blue, true);
		}

		/// <summary>
		/// Berechnet die rekursiv die maximale Breite des Unterbaums sowie die Tiefe der jeweiligen Zeitalter-Elemente.
		/// </summary>
		/// <param name="ageCounts">Die aktuelle Liste mit den Tiefen der Elemente pro Zeitalter.</param>
		public override void CalculateTreeBounds(ref List<int> ageCounts)
		{
			// Zähler für eigenes Zeitalter inkrementieren
			++ageCounts[Age];

			// Abmessungen des Baums bestimmen
			if(Successors.Count == 0)
				TreeWidth = 1;
			else
			{
				// Die Breite des Baums ist die Summe der Breite der Nachfolger
				TreeWidth = 0;
				List<int> ageCountsCopy = new List<int>(ageCounts);
				List<int> succAgeCounts = null;
				foreach(TechTreeResearch succ in Successors)
				{
					// Abmessungen des Unterbaums berechnen
					succAgeCounts = new List<int>(ageCountsCopy);
					succ.CalculateTreeBounds(ref succAgeCounts);

					// Unterbaumbreite zur Gesamtbreite hinzuaddieren
					TreeWidth += succ.TreeWidth;

					// Zurückgegebene Zeitalter-Werte abgleichen => es wird immer das Maximum pro Zeitalter genommen
					ageCounts = ageCounts.Zip(succAgeCounts, (a1, a2) => Math.Max(a1, a2)).ToList();
				}
			}
		}

		/// <summary>
		/// Ruft eine Liste mit den Kindelementen ab.
		/// </summary>
		/// <returns></returns>
		public override List<TechTreeElement> GetChildren()
		{
			// Nachfolger zurückgeben
			return new List<TechTreeElement>(Successors);
		}

		/// <summary>
		/// Ruft eine Liste mit den sichtbaren (= gerenderten) Kindelementen ab.
		/// Dies wird zur schöneren Umsetzung der Filterfunktion benutzt, damit Schattenelemente nicht als Kind betrachtet werden.
		/// </summary>
		/// <returns></returns>
		public override List<TechTreeElement> GetVisibleChildren()
		{
			// Nachfolger zurückgeben
			return new List<TechTreeElement>(Successors);
		}

		/// <summary>
		/// Gibt das übergebene Element frei, falls es diesem Element untergeordnet sein sollte.
		/// </summary>
		/// <param name="child">Das freizugebende Element.</param>
		/// <returns></returns>
		public override void RemoveChild(TechTreeElement child)
		{
			// Kind-Element?
			Successors.RemoveAll(s => s == child);
		}

		/// <summary>
		/// Konvertiert die enthaltenen Daten in ein XML-Format und schreibt sie in den übergebenen XmlWriter.
		/// </summary>
		/// <param name="writer">Der XmlWriter, in den die Daten geschrieben werden sollen.</param>
		/// <param name="elementIDs">Die Liste mit den den TechTree-Elementen zugeordneten IDs.</param>
		/// <param name="lastID">Die letzte vergebene ID.</param>
		public override int ToXml(XmlWriter writer, Dictionary<TechTreeElement, int> elementIDs, int lastID)
		{
			// ID generieren
			int myID = ++lastID;
			elementIDs[this] = myID;

			// Nachfolger-IDs abrufen
			List<int> succIDs = new List<int>();
			Successors.ForEach(s =>
			{
				if(!elementIDs.ContainsKey(s))
					lastID = s.ToXml(writer, elementIDs, lastID);
				succIDs.Add(elementIDs[s]);
			});

			// Abhängigkeits-IDs abrufen
			List<KeyValuePair<int, int>> depIDs = new List<KeyValuePair<int, int>>();
			foreach(var d in Dependencies)
			{
				if(!elementIDs.ContainsKey(d.Key))
					lastID = d.Key.ToXml(writer, elementIDs, lastID);
				depIDs.Add(new KeyValuePair<int, int>(elementIDs[d.Key], d.Value));
			}

			// Gebäude-Abhängigkeits-IDs abrufen
			List<KeyValuePair<int, int>> buildingDepIDs = new List<KeyValuePair<int, int>>();
			foreach(var d in BuildingDependencies)
			{
				if(!elementIDs.ContainsKey(d.Key))
					lastID = d.Key.ToXml(writer, elementIDs, lastID);
				buildingDepIDs.Add(new KeyValuePair<int, int>(elementIDs[d.Key], d.Value ? 1 : 0));
			}

			// Sicherstellen, dass alle von den Effekten referenzierten Elemente IDs haben
			Effects.ForEach(eff =>
			{
				if(eff.Element != null)
					if(!elementIDs.ContainsKey(eff.Element))
						lastID = eff.Element.ToXml(writer, elementIDs, lastID);
				if(eff.DestinationElement != null)
					if(!elementIDs.ContainsKey(eff.DestinationElement))
						lastID = eff.DestinationElement.ToXml(writer, elementIDs, lastID);
			});

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

				// Nachfolger schreiben
				writer.WriteStartElement("successors");
				{
					foreach(int s in succIDs)
						writer.WriteElementNumber("successor", s);
				}
				writer.WriteEndElement();

				// Abhängigkeiten schreiben
				writer.WriteStartElement("dependencies");
				{
					depIDs.ForEach(d =>
					{
						// Abhängigkeitsanzahl und Abhängigkeits-ID schreiben
						writer.WriteStartElement("dependency");
						writer.WriteAttributeNumber("depcount", d.Value);
						writer.WriteNumber(d.Key);
						writer.WriteEndElement();
					});
				}
				writer.WriteEndElement();
				writer.WriteStartElement("buildingdependencies");
				{
					buildingDepIDs.ForEach(d =>
					{
						// Abhängigkeitsanzahl und Abhängigkeits-ID schreiben
						writer.WriteStartElement("dependency");
						writer.WriteAttributeNumber("depcount", d.Value);
						writer.WriteNumber(d.Key);
						writer.WriteEndElement();
					});
				}
				writer.WriteEndElement();

				// Effekte schreiben
				writer.WriteStartElement("effects");
				{
					Effects.ForEach(eff => eff.ToXml(writer, elementIDs));
				}
				writer.WriteEndElement();
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

			// Nachfolger einlesen
			Successors = new List<TechTreeResearch>();
			foreach(XElement succ in element.Element("successors").Descendants("successor"))
				Successors.Add((TechTreeResearch)previousElements[(int)succ]);

			// Abhängigkeiten einlesen
			Dependencies = new Dictionary<TechTreeResearch, int>();
			foreach(XElement dep in element.Element("dependencies").Descendants("dependency"))
				Dependencies.Add((TechTreeResearch)previousElements[(int)dep], (int)dep.Attribute("depcount"));

			// Gebäude-Abhängigkeiten einlesen
			BuildingDependencies = new Dictionary<TechTreeBuilding, bool>();
			foreach(XElement dep in element.Element("buildingdependencies").Descendants("dependency"))
				BuildingDependencies.Add((TechTreeBuilding)previousElements[(int)dep], ((int)dep.Attribute("depcount") > 0));

			// Effekte lesen
			Effects = new List<TechEffect>();
			foreach(XElement eff in element.Element("effects").Descendants("effect"))
				Effects.Add(new TechEffect(eff, previousElements));

			// DAT-Einheit laden
			DATResearch = dat.Researches[ID];

			// Namen setzen
			UpdateName(langFiles);
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
			CreateIconTexture(textureFunc);

			// Funktion in der Basisklasse aufrufen
			base.CreateIconTextures(textureFunc);
		}

		/// <summary>
		/// Erstellt die Icon-Textur.
		/// </summary>
		/// <param name="textureFunc">Die Textur-Generierungsfunktion.</param>
		public override void CreateIconTexture(Func<string, short, int> textureFunc)
		{
			// Icon erstellen
			IconTextureID = textureFunc(Type, DATResearch.IconID);
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
			counter += Dependencies.Count(c => c.Key == element);
			counter += Successors.Count(c => c == element);

			// Referenzen in Effekten suchen
			counter += Effects.Count(eff => eff.Element == element || eff.DestinationElement == element);

			// Fertig
			return counter;
		}

		/// <summary>
		/// Generiert den Anzeige-Namen.
		/// </summary>
		/// <param name="langFiles">Das Language-Datei-Objekt zum Auslesen von Stringdaten.</param>
		public override void UpdateName(GenieLibrary.LanguageFileWrapper langFiles)
		{
			// Name setzen
			Name = langFiles.GetString(DATResearch.LanguageDLLName1) + " [ID " + ID.ToString() + ", " + DATResearch.Name.TrimEnd('\0') + "]";
		}

		/// <summary>
		/// Gibt eine Kopie dieser Technologie zurück. Texturen werden ebenfalls kopiert.
		/// </summary>
		/// <param name="cloneChildren">Gibt an, ob auch Kinder kopiert oder diese übersprungen werden sollen.</param>
		/// <param name="projectFile">Das zugrundeliegende Projekt.</param>
		/// <param name="textureFunc">Die Textur-Generierungsfunktion.</param>
		/// <returns></returns>
		public TechTreeResearch Clone(bool cloneChildren, TechTreeFile projectFile, Func<string, short, int> textureFunc)
		{
			// Klon-Objekt erstellen
			TechTreeResearch clone = (TechTreeResearch)projectFile.CreateElement("TechTreeResearch");

			// Flach kopierbare Elemente übertragen
			clone.Age = Age;
			clone.Flags = Flags;
			clone.Name = Name;
			clone.ShadowElement = ShadowElement;
			clone.TreeWidth = TreeWidth;

			// Abhängigkeitslisten kopieren
			clone.Dependencies = new Dictionary<TechTreeResearch, int>(Dependencies);
			clone.BuildingDependencies = new Dictionary<TechTreeBuilding, bool>(BuildingDependencies);

			// Effekte kopieren
			clone.Effects = new List<TechEffect>(Effects.Count);
			Effects.ForEach(eff => clone.Effects.Add(eff.Clone()));

			// DAT-Technologie kopieren
			GenieLibrary.DataElements.Research cloneDAT = (GenieLibrary.DataElements.Research)DATResearch.Clone();
			clone.DATResearch = cloneDAT;

			// Technologie in die DAT schreiben
			int newID = projectFile.BasicGenieFile.Researches.Count;
			projectFile.BasicGenieFile.Researches.Add(cloneDAT);
			clone.ID = newID;

			// Kinder kopieren
			if(cloneChildren)
			{
				clone.Successors = new List<TechTreeResearch>(Successors.Count);
				Successors.ForEach(succ => clone.Successors.Add(succ.Clone(cloneChildren, projectFile, textureFunc)));
			}
			else
				clone.Successors = new List<TechTreeResearch>();

			// Textur neu erstellen
			clone.IconTextureID = textureFunc(clone.Type, clone.DATResearch.IconID);

			// Fertig
			return clone;
		}

		#endregion Funktionen

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeResearch"; }
		}

		#endregion Eigenschaften
	}
}