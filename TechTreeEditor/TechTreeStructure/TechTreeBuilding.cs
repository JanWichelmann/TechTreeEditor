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
	/// Definiert ein Gebäude-Element im Technologiebaum.
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ID: #{ID}, Name: {Name}")]
	public class TechTreeBuilding : TechTreeUnit, IChildrenContainer, IUpgradeable
	{
		#region Variablen

		/// <summary>
		/// Die in diesem Gebäude enthaltenen Kindelemente.
		/// Dies sind Elemente, die in diesem Gebäude entwickelt oder erschaffen werden können, samt deren Button-IDs.
		/// Die Button-IDs sind nicht zwingend eindeutig!
		/// </summary>
		public List<Tuple<byte, TechTreeElement>> Children { get; private set; }

		/// <summary>
		/// Die direkte Weiterentwicklung dieses Elements.
		/// </summary>
		public TechTreeUnit Successor { get; set; }

		/// <summary>
		/// Die Technologie, die dieses Element weiterentwickelt.
		/// </summary>
		public TechTreeResearch SuccessorResearch { get; set; }

		/// <summary>
		/// Die Technologie, die dieses Element freischaltet.
		/// Nicht zu verwechseln mit der Nachfolger-Technologie! Diese Variable sollte nur bei "Stammeinheiten" gesetzt sein, da die nachfolgenden i.d.R. Weiterentwicklungen sind.
		/// </summary>
		public TechTreeResearch EnablerResearch { get; set; }

		/// <summary>
		/// Die Gebäude-Varianten, die bei den jeweiligen Zeitalter-Upgrades gezeigt werden (Grafiksets).
		/// </summary>
		public Dictionary<int, TechTreeBuilding> AgeUpgrades { get; private set; }

		/// <summary>
		/// Die Projektil-Einheit.
		/// </summary>
		public TechTreeProjectile ProjectileUnit { get; set; }

		/// <summary>
		/// Die Projektil-Duplikations-Einheit.
		/// </summary>
		public TechTreeProjectile ProjectileDuplicationUnit { get; set; }

		/// <summary>
		/// Die Stack-Einheit.
		/// Diese wird direkt auf der eigentlichen Einheit platziert und reagiert auch auf Klicks. Wird beim Dorfzentrum benutzt.
		/// </summary>
		public TechTreeBuilding StackUnit { get; set; }

		/// <summary>
		/// Die Head-Einheit.
		/// Dies ist die Einheit, an der die Annex-Einheiten angeheftet werden.
		/// </summary>
		public TechTreeBuilding HeadUnit { get; set; }

		/// <summary>
		/// Die Ein/Auspack-Einheit.
		/// </summary>
		public TechTreeUnit TransformUnit { get; set; }

		/// <summary>
		/// Die an dieses Gebäude angehefteten Untergebäude und deren Versatzwerte.
		/// Wird beim Dorfzentrum benutzt.
		/// Diese Liste darf maximal 4 Elemente enthalten.
		/// </summary>
		public List<Tuple<TechTreeBuilding, float, float>> AnnexUnits { get; private set; }

		/// <summary>
		/// Die Button-ID im Dorfbewohner-Bau-Menü. Wird nur bei Elternelementen benutzt.
		/// </summary>
		public int ButtonID { get; set; }

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt ein neues TechTree-Gebäude-Element.
		/// </summary>
		/// <param name="age">Das Zeitalter, ab dem das Element verfügbar ist.</param>
		public TechTreeBuilding()
			: base()
		{
			// Objekte erstellen
			Children = new List<Tuple<byte, TechTreeElement>>();
			AgeUpgrades = new Dictionary<int, TechTreeBuilding>();
			AnnexUnits = new List<Tuple<TechTreeBuilding, float, float>>();
			StandardElement = false;
		}

		/// <summary>
		/// Zeichnet den Unterbaum des Gebäudes an der angegebenen Position.
		/// </summary>
		/// <param name="position">Die Zeichenposition des gesamten Baum-Rechtecks. Es wird die obere linke Ecke angegeben.</param>
		/// <param name="ageOffsets">Eine Liste mit den Zeitalter-Offset-Daten. Diese wird in dieser Funktion nicht verändert.</param>
		/// <param name="lastAge">Das Zeitalter-Offset des übergeordneten Elements.</param>
		protected override void DrawChildren(Point position, List<int> ageOffsets, int parentAgeOffset)
		{
			// Pixelbreite des Unterbaums berechnen
			int pixelWidth = TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

			// Sind Kinder vorhanden?
			if(Children.Count != 0 || Successor != null)
			{
				// Senkrechte Linie nach unten zeichnen
				GL.Color3(Color.Black);
				GL.Begin(BeginMode.Lines);
				{
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + RenderControl.BOX_SPACE_VERT); // Oben
					GL.Vertex2(position.X + (pixelWidth / 2), position.Y + RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT); // Unten
				}
				GL.End();

				// Hilfsvariablen
				position.Y += RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT;
				int childPixelWidth = 0;
				Point dotFirstChild = position;
				Point dotLastChild = position;
				bool dotFirstChildSet = false;
				int childYOffset = 0;

				// Ist ein Nachfolger vorhanden?
				if(Successor != null)
				{
					// Nachfolger zeichnen
					Successor.Draw(position, ageOffsets, parentAgeOffset + 1);

					// Pixel-Breite berechnen
					childPixelWidth = Successor.TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

					// Aktuelle waagerechte Verbindungslinien-Position bestimmen
					dotLastChild = new Point(position.X + childPixelWidth / 2, position.Y);

					// Ggf. erste waagerechte Verbindungslinien-Position merken
					if(!dotFirstChildSet)
					{
						dotFirstChild = dotLastChild;
						dotFirstChildSet = true;
					}

					// Versatz des Nachfolger-Elements nach unten wegen des Zeitalter-Offsets berechnen
					childYOffset = dotLastChild.Y + Math.Max(ageOffsets[Successor.Age] - parentAgeOffset - 1, 0) * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT);

					// Senkrechte Linie darauf zeichnen
					GL.Color3(Color.Black);
					GL.Begin(BeginMode.Lines);
					{
						GL.Vertex2(dotLastChild.X, dotLastChild.Y); // Oben
						GL.Vertex2(dotLastChild.X, childYOffset + RenderControl.BOX_SPACE_VERT); // Unten
					}
					GL.End();

					// Position erhöhen
					position.X += childPixelWidth;
				}

				// Kinder zeichnen
				foreach(var child in Children)
				{
					// Kind versetzt zeichnen
					child.Item2.Draw(position, ageOffsets, parentAgeOffset + 1);

					// Pixel-Breite berechnen
					childPixelWidth = child.Item2.TreeWidth * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_HORI);

					// Aktuelle waagerechte Verbindungslinien-Position bestimmen
					dotLastChild = new Point(position.X + childPixelWidth / 2, position.Y);

					// Ggf. erste waagerechte Verbindungslinien-Position merken
					if(!dotFirstChildSet)
					{
						dotFirstChild = dotLastChild;
						dotFirstChildSet = true;
					}

					// Versatz des Kind-Elements nach unten wegen des Zeitalter-Offsets berechnen
					childYOffset = dotLastChild.Y + Math.Max(ageOffsets[child.Item2.Age] - parentAgeOffset - 1, 0) * (RenderControl.BOX_BOUNDS + 2 * RenderControl.BOX_SPACE_VERT);

					// Senkrechte Linie darauf zeichnen
					GL.Color3(Color.Black);
					GL.Begin(BeginMode.Lines);
					{
						GL.Vertex2(dotLastChild.X, dotLastChild.Y); // Oben
						GL.Vertex2(dotLastChild.X, childYOffset + RenderControl.BOX_SPACE_VERT); // Unten
					}
					GL.End();

					// Button-Nummer anzeigen
					GL.Color3(Color.FromArgb(255, 210, 102));
					RenderControl.DrawString(child.Item1.ToString(), dotLastChild.X - (child.Item1 < 10 ? 4 : 7), dotLastChild.Y + RenderControl.BOX_SPACE_VERT / 2);

					// Position erhöhen
					position.X += childPixelWidth;
				}

				// Waagerechte Verbindungslinie zeichnen
				GL.Color3(Color.Black);
				GL.Begin(BeginMode.Lines);
				{
					GL.Vertex2(dotFirstChild.X, dotFirstChild.Y); // Oben
					GL.Vertex2(dotLastChild.X + 1, dotLastChild.Y); // Unten (+ 1, da sonst ein Pixel am Ende fehlt)
				}
				GL.End();
			}
		}

		/// <summary>
		/// Zeichnet Pfeile zu den Abhängigkeiten dieses Elements.
		/// </summary>
		public override void DrawDependencies()
		{
			// Basisklassen-Funktion aufrufen
			base.DrawDependencies();

			// Ggf. Linie zur Weiterentwicklungs-Technologie zeichnen
			if(SuccessorResearch != null)
				RenderControl.DrawLine(this, SuccessorResearch, Color.Red, true);

			// Ggf. Pfeil von der Freischaltungs-Technologie zeichnen
			if(EnablerResearch != null)
				RenderControl.DrawArrow(EnablerResearch, this, Color.DarkGreen, true);

			// Ggf. Linien zu den Gebäude-Abhängigkeiten zeichnen
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

			// Baumbreite muss berechnet werden
			TreeWidth = 0;

			// Abmessungen des Baums bestimmen
			if(Children.Count > 0)
			{
				// Die Breite des Baums ist die Summe der Breite der Kinder
				List<int> ageCountsCopy = new List<int>(ageCounts);
				List<int> childAgeCounts = null;
				foreach(var child in Children)
				{
					// Abmessungen des Unterbaums berechnen
					childAgeCounts = new List<int>(ageCountsCopy);
					child.Item2.CalculateTreeBounds(ref childAgeCounts);

					// Unterbaumbreite zur Gesamtbreite hinzuaddieren
					TreeWidth += child.Item2.TreeWidth;

					// Zurückgegebene Zeitalter-Werte abgleichen => es wird immer das Maximum pro Zeitalter genommen
					ageCounts = ageCounts.Zip(childAgeCounts, (a1, a2) => Math.Max(a1, a2)).ToList();
				}
			}

			// Ist ein Nachfolgerelement vorhanden?
			if(Successor != null)
			{
				// Baumbreite addieren
				Successor.CalculateTreeBounds(ref ageCounts);
				TreeWidth += Successor.TreeWidth;
			}

			// Die Baumbreite muss mindestens 1 sein
			if(TreeWidth == 0)
				TreeWidth = 1;
		}

		/// <summary>
		/// Ruft eine Liste mit den Kindelementen ab.
		/// </summary>
		/// <returns></returns>
		public override List<TechTreeElement> GetChildren()
		{
			// Kinder und Nachfolgeelement zurückgeben
			List<TechTreeElement> childElements = Children.Select(c => c.Item2).ToList();
			if(Successor != null)
				childElements.Add(Successor);
			childElements.AddRange(AgeUpgrades.Values);
			return childElements;
		}

		/// <summary>
		/// Ruft eine Liste mit den sichtbaren (= gerenderten) Kindelementen ab.
		/// Dies wird zur schöneren Umsetzung der Filterfunktion benutzt, damit Schattenelemente nicht als Kind betrachtet werden.
		/// </summary>
		/// <returns></returns>
		public override List<TechTreeElement> GetVisibleChildren()
		{
			// Kinder und Nachfolgeelement zurückgeben
			List<TechTreeElement> childElements = Children.Select(c => c.Item2).ToList();
			if(Successor != null)
				childElements.Add(Successor);
			return childElements;
		}

		/// <summary>
		/// Gibt das übergebene Element frei, falls es diesem Element untergeordnet sein sollte.
		/// </summary>
		/// <param name="child">Das freizugebende Element.</param>
		/// <returns></returns>
		public override void RemoveChild(TechTreeElement child)
		{
			// Nachfolge-Element?
			if(Successor == child)
			{
				Successor = null;
				SuccessorResearch = null;
			}

			// Kind-Element?
			Children.RemoveAll(c => c.Item2 == child);
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

			// Alt. TechTree-Elternelement-ID abrufen
			int altNTTParentID = -1;
			if(AlternateNewTechTreeParentElement != null)
			{
				if(!elementIDs.ContainsKey(AlternateNewTechTreeParentElement))
					lastID = AlternateNewTechTreeParentElement.ToXml(writer, elementIDs, lastID);
				altNTTParentID = elementIDs[AlternateNewTechTreeParentElement];
			}

			// Toten-ID abrufen
			int deadUnitID = -1;
			if(DeadUnit != null)
			{
				if(!elementIDs.ContainsKey(DeadUnit))
					lastID = DeadUnit.ToXml(writer, elementIDs, lastID);
				deadUnitID = elementIDs[DeadUnit];
			}

			// Nachfolger-ID abrufen
			int successorID = -1;
			if(Successor != null)
			{
				if(!elementIDs.ContainsKey(Successor))
					lastID = Successor.ToXml(writer, elementIDs, lastID);
				successorID = elementIDs[Successor];
			}

			// Nachfolger-Technologie-ID abrufen
			int successorResearchID = -1;
			if(SuccessorResearch != null)
			{
				if(!elementIDs.ContainsKey(SuccessorResearch))
					lastID = SuccessorResearch.ToXml(writer, elementIDs, lastID);
				successorResearchID = elementIDs[SuccessorResearch];
			}

			// Freischalt-Technologie-ID abrufen
			int enablerResearchID = -1;
			if(EnablerResearch != null)
			{
				if(!elementIDs.ContainsKey(EnablerResearch))
					lastID = EnablerResearch.ToXml(writer, elementIDs, lastID);
				enablerResearchID = elementIDs[EnablerResearch];
			}

			// Projektil-Einheit-ID abrufen
			int projectileUnitID = -1;
			if(ProjectileUnit != null)
			{
				if(!elementIDs.ContainsKey(ProjectileUnit))
					lastID = ProjectileUnit.ToXml(writer, elementIDs, lastID);
				projectileUnitID = elementIDs[ProjectileUnit];
			}

			// Projektil-Duplikations-Einheit-ID abrufen
			int projectileDuplUnitID = -1;
			if(ProjectileDuplicationUnit != null)
			{
				if(!elementIDs.ContainsKey(ProjectileDuplicationUnit))
					lastID = ProjectileDuplicationUnit.ToXml(writer, elementIDs, lastID);
				projectileDuplUnitID = elementIDs[ProjectileDuplicationUnit];
			}

			// Stack-Einheit-ID abrufen
			int stackUnitID = -1;
			if(StackUnit != null)
			{
				if(!elementIDs.ContainsKey(StackUnit))
					lastID = StackUnit.ToXml(writer, elementIDs, lastID);
				stackUnitID = elementIDs[StackUnit];
			}

			// Head-Einheit-ID abrufen
			int headUnitID = -1;
			if(HeadUnit != null)
			{
				if(!elementIDs.ContainsKey(HeadUnit))
					lastID = HeadUnit.ToXml(writer, elementIDs, lastID);
				headUnitID = elementIDs[HeadUnit];
			}

			// Ein/Auspack-Einheit-ID abrufen
			int transformUnitID = -1;
			if(TransformUnit != null)
			{
				if(!elementIDs.ContainsKey(TransformUnit))
					lastID = TransformUnit.ToXml(writer, elementIDs, lastID);
				transformUnitID = elementIDs[TransformUnit];
			}

			// Kind-IDs abrufen (Schlüssel: ID, Wert: Button)
			Dictionary<int, byte> childIDs = new Dictionary<int, byte>();
			Children.ForEach(c =>
			{
				if(!elementIDs.ContainsKey(c.Item2))
					lastID = c.Item2.ToXml(writer, elementIDs, lastID);
				childIDs.Add(elementIDs[c.Item2], c.Item1);
			});

			// Gebäude-Abhängigkeits-IDs abrufen
			List<Tuple<int, int>> buildingDepIDs = new List<Tuple<int, int>>();
			foreach(var d in BuildingDependencies)
			{
				if(!elementIDs.ContainsKey(d.Key))
					lastID = d.Key.ToXml(writer, elementIDs, lastID);
				buildingDepIDs.Add(new Tuple<int, int>(elementIDs[d.Key], d.Value ? 1 : 0));
			}

			// Age-Upgrade-IDs abrufen
			List<Tuple<int, int>> ageUpgradeIDs = new List<Tuple<int, int>>();
			foreach(var au in AgeUpgrades)
			{
				if(!elementIDs.ContainsKey(au.Value))
					lastID = au.Value.ToXml(writer, elementIDs, lastID);
				ageUpgradeIDs.Add(new Tuple<int, int>(au.Key, elementIDs[au.Value]));
			}

			// Annex-IDs abrufen (ID, Verschiebung X/Y)
			List<Tuple<int, float, float>> annexIDs = new List<Tuple<int, float, float>>();
			foreach(var a in AnnexUnits)
			{
				if(!elementIDs.ContainsKey(a.Item1))
					lastID = a.Item1.ToXml(writer, elementIDs, lastID);
				annexIDs.Add(new Tuple<int, float, float>(elementIDs[a.Item1], a.Item2, a.Item3));
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
				writer.WriteElementNumber("standard", StandardElement);

				// Button-ID schreiben
				writer.WriteElementNumber("button", ButtonID);

				// Alt. TechTree-Elternelement-ID schreiben
				writer.WriteElementNumber("alternatetechtreeparent", altNTTParentID);

				// Toten-ID schreiben
				writer.WriteElementNumber("deadunit", deadUnitID);

				// Nachfolger-ID schreiben
				writer.WriteElementNumber("successor", successorID);

				// Nachfolger-Technologie-ID schreiben
				writer.WriteElementNumber("successorresearch", successorResearchID);

				// Freischalt-Technologie-ID schreiben
				writer.WriteElementNumber("enablerresearch", enablerResearchID);

				// Projektil-IDs schreiben
				writer.WriteElementNumber("projunit", projectileUnitID);
				writer.WriteElementNumber("projduplunit", projectileDuplUnitID);

				// Sonstige IDs schreiben
				writer.WriteElementNumber("stackunit", stackUnitID);
				writer.WriteElementNumber("headunit", headUnitID);
				writer.WriteElementNumber("transunit", transformUnitID);

				// Kinder schreiben
				writer.WriteStartElement("children");
				{
					foreach(var c in childIDs)
					{
						// Button-ID schreiben
						writer.WriteStartElement("child");
						writer.WriteAttributeNumber("button", c.Value);
						writer.WriteNumber(c.Key);
						writer.WriteEndElement();
					}
				}
				writer.WriteEndElement();

				// Abhängigkeiten schreiben
				writer.WriteStartElement("buildingdependencies");
				{
					buildingDepIDs.ForEach(d =>
					{
						// Abhängigkeitsanzahl und Abhängigkeits-ID schreiben
						writer.WriteStartElement("dependency");
						writer.WriteAttributeNumber("depcount", d.Item2);
						writer.WriteNumber(d.Item1);
						writer.WriteEndElement();
					});
				}
				writer.WriteEndElement();

				// Zeitalter-Upgrades schreiben
				writer.WriteStartElement("ageupgrades");
				{
					ageUpgradeIDs.ForEach(d =>
					{
						// Zeitalter und Gebäude-ID schreiben
						writer.WriteStartElement("upgrade");
						writer.WriteAttributeNumber("age", d.Item1);
						writer.WriteNumber(d.Item2);
						writer.WriteEndElement();
					});
				}
				writer.WriteEndElement();

				// Annex-Einheiten schreiben
				writer.WriteStartElement("annexes");
				{
					annexIDs.ForEach(a =>
					{
						// Zeitalter und Gebäude-ID schreiben
						writer.WriteStartElement("annex");
						writer.WriteAttributeNumber("x", a.Item2);
						writer.WriteAttributeNumber("y", a.Item3);
						writer.WriteNumber(a.Item1);
						writer.WriteEndElement();
					});
				}
				writer.WriteEndElement();

				// Fähigkeiten schreiben
				writer.WriteStartElement("abilities");
				{
					// Fähigkeiten schreiben
					Abilities.ForEach(a => a.ToXml(writer, elementIDs));
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

			// Werte einlesen
			StandardElement = (bool)element.Element("standard");
			ButtonID = (int)element.Element("button");
			int id = (int)element.Element("successor");
			if(id >= 0)
				this.Successor = (TechTreeBuilding)previousElements[id];
			id = (int)element.Element("successorresearch");
			if(id >= 0)
				this.SuccessorResearch = (TechTreeResearch)previousElements[id];
			id = (int)element.Element("enablerresearch");
			if(id >= 0)
				this.EnablerResearch = (TechTreeResearch)previousElements[id];
			id = (int)element.Element("projunit");
			if(id >= 0)
				this.ProjectileUnit = (TechTreeProjectile)previousElements[id];
			id = (int)element.Element("projduplunit");
			if(id >= 0)
				this.ProjectileDuplicationUnit = (TechTreeProjectile)previousElements[id];
			id = (int)element.Element("stackunit");
			if(id >= 0)
				this.StackUnit = (TechTreeBuilding)previousElements[id];
			id = (int)element.Element("headunit");
			if(id >= 0)
				this.HeadUnit = (TechTreeBuilding)previousElements[id];
			id = (int)element.Element("transunit");
			if(id >= 0)
				this.TransformUnit = (TechTreeBuilding)previousElements[id];

			// Kinder einlesen
			Children = new List<Tuple<byte, TechTreeElement>>();
			foreach(XElement child in element.Element("children").Descendants("child"))
				Children.Add(new Tuple<byte, TechTreeElement>((byte)(uint)child.Attribute("button"), previousElements[(int)child]));

			// Gebäude-Abhängigkeiten einlesen
			BuildingDependencies = new Dictionary<TechTreeBuilding, bool>();
			foreach(XElement dep in element.Element("buildingdependencies").Descendants("dependency"))
				BuildingDependencies.Add((TechTreeBuilding)previousElements[(int)dep], ((int)dep.Attribute("depcount") > 0));

			// Zeitalter-Upgrades einlesen
			AgeUpgrades = new Dictionary<int, TechTreeBuilding>();
			foreach(XElement au in element.Element("ageupgrades").Descendants("upgrade"))
				AgeUpgrades.Add((int)au.Attribute("age"), (TechTreeBuilding)previousElements[(int)au]);

			// Annex-Einheiten einlesen
			AnnexUnits = new List<Tuple<TechTreeBuilding, float, float>>();
			foreach(XElement a in element.Element("annexes").Descendants("annex"))
				AnnexUnits.Add(new Tuple<TechTreeBuilding, float, float>((TechTreeBuilding)previousElements[(int)a], (float)a.Attribute("x"), (float)a.Attribute("y")));
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
			counter += Children.Count(c => c.Item2 == element);
			if(Successor == element) ++counter;
			if(SuccessorResearch == element) ++counter;
			if(EnablerResearch == element) ++counter;
			if(ProjectileUnit == element) ++counter;
			if(ProjectileDuplicationUnit == element) ++counter;
			if(StackUnit == element) ++counter;
			if(HeadUnit == element) ++counter;
			if(TransformUnit == element) ++counter;
			foreach(var upgr in AgeUpgrades)
				if(upgr.Value == element) ++counter;
			foreach(var annex in AnnexUnits)
				if(annex.Item1 == element) ++counter;

			// Fertig
			return counter;
		}

		#endregion Funktionen

		#region Eigenschaften

		/// <summary>
		/// Ruft den den Elementtyp (= Klassennamen) ab.
		/// </summary>
		public override string Type
		{
			get { return "TechTreeBuilding"; }
		}

		/// <summary>
		/// Ruft ab, ob dieses Gebäude ein Standard-Element ist (d.h. im Standard-Spiel errichtet werden kann), oder legt dies fest.
		/// </summary>
		public bool StandardElement
		{
			get { return _standardElement; }
			set { _standardElement = value; }
		}

		#endregion Eigenschaften
	}
}