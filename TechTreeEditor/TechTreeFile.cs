using IORAMHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	/// <summary>
	/// Definiert eine TechTree-Datei, die alle Daten einer Instanz erhält.
	/// Elemente sollten nicht ohne Nutzung der in dieser Klasse enthaltenen Funktionen neu erstellt oder gelöscht werden, falls sie mit dem internen Baum verbunden sind.
	/// Inkorrektes Bearbeiten dieser Elemente führt evtl. zu fehlerhaften Einstellungen bei den CivBlocks.
	/// TODO: Ja, das ist hässlich...
	/// </summary>
	public class TechTreeFile
	{
		#region Variablen

		/// <summary>
		/// Die originale Basis-DAT-Datei. Diese darf nach der ersten Erstellung aus Kompatibilitätsgründen nicht mehr verändert werden.
		/// </summary>
		private GenieLibrary.GenieFile _basicGenieFile = null;

		/// <summary>
		/// Die Elternelemente des enthaltenen Technologiebaums.
		/// </summary>
		private List<TechTreeElement> _techTreeParentElements = null;

		/// <summary>
		/// Enthält alle Technologiebaumelemente in einer linearen Liste.
		/// Diese dient nur als Cache zur Optimierung von Suchoperationen, beim Speichern ist diese unerheblich!
		/// </summary>
		private List<TechTreeElement> _allElements = null;

		/// <summary>
		/// Die Pfade zu den Language-Dateien.
		/// </summary>
		private List<string> _languageFilePaths = null;

		/// <summary>
		/// Das Language-DLL-Wrapperobjekt.
		/// </summary>
		private GenieLibrary.LanguageFileWrapper _languageFileWrapper = null;

		/// <summary>
		/// Der Pfad zur Interface-DRS.
		/// </summary>
		private string _interfacDRSPath = "";

		/// <summary>
		/// Die Kultur-Baum-Konfigurationen.
		/// </summary>
		private List<CivTreeConfig> _civTrees = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// Erstellt eine neue leere TechTree-Datei.
		/// </summary>
		/// <param name="basicGenieFile">Die DAT-Datei, auf der die enthaltenen Definitionen basieren sollen.</param>
		public TechTreeFile(GenieLibrary.GenieFile basicGenieFile)
		{
			// Parameter speichern
			_basicGenieFile = basicGenieFile;

			// Elternelement-Liste erstellen
			_techTreeParentElements = new List<TechTreeElement>();

			// Sammelliste erstellen
			_allElements = new List<TechTreeElement>();

			// Kultur-Liste erstellen
			_civTrees = new List<CivTreeConfig>();
		}

		/// <summary>
		/// Konstruktor.
		/// Lädt die angegebene TechTree-Datei.
		/// </summary>
		/// <param name="filename">Die zu ladende TechTree-Datei.</param>
		public TechTreeFile(string filename)
		{
			// Datei laden
			ReadData(new FileStream(filename, FileMode.Open));
		}

		/// <summary>
		/// Liest die Daten aus dem übergebenen Stream.
		/// </summary>
		/// <param name="stream">Der Stream mit den zu lesenden Daten.</param>
		private void ReadData(FileStream stream)
		{
			// ZIP-Archiv aus Stream erstellen
			using(ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
			{
				// Projektdaten lesen
				using(Stream currFile = archive.GetEntry("project.xml").Open())
				{
					// XML-Format benutzen
					XDocument doc = XDocument.Load(currFile);
					XElement mainElement = doc.Element("project");

					// Language-Datei-Pfade lesen
					_languageFilePaths = new List<string>();
					foreach(XElement fileElement in mainElement.Element("languagefiles").Descendants("file"))
						_languageFilePaths.Add(fileElement.Value);

					// Interfac-DRS-Pfad lesen
					_interfacDRSPath = mainElement.Element("interfacdrs").Value;
				}

				// DLL-Handles erstellen
				_languageFileWrapper = new GenieLibrary.LanguageFileWrapper(_languageFilePaths.ToArray());

				// DAT-Datei laden
				using(Stream currFile = archive.GetEntry("dat.unz").Open())
				{
					_basicGenieFile = new GenieLibrary.GenieFile(new RAMBuffer(currFile));
				}

				// Technologiebaum lesen
				_techTreeParentElements = new List<TechTreeElement>();
				Dictionary<int, TechTreeElement> readElements = new Dictionary<int, TechTreeElement>();
				using(Stream currFile = archive.GetEntry("techtree.xml").Open())
				{
					// XML-Format benutzen
					XDocument doc = XDocument.Load(currFile);
					XElement mainElement = doc.Element("techtree");
					XElement elemList = mainElement.Element("elements");

					// Elemente oberflächlich einlesen
					// Ggf. bestehen Abhängigkeiten, die nicht Reihenfolgen-konform sind => Leere Elemente in die Liste schreiben
					foreach(XElement elem in elemList.Descendants("element"))
					{
						// Element zur Liste hinzufügen
						readElements.Add((int)elem.Attribute("id"), TechTreeElement.CreateFromType((string)elem.Attribute("type")));
					}

					// Elementinhalte lesen
					foreach(XElement elem in elemList.Descendants("element"))
					{
						// Element lesen
						readElements[(int)elem.Attribute("id")].FromXml(elem, readElements, _basicGenieFile, _languageFileWrapper);
					}

					// Elternelemente lesen
					XElement parentList = mainElement.Element("parents");
					foreach(XElement elem in mainElement.Descendants("parent"))
					{
						// Element zur Liste hinzufügen
						_techTreeParentElements.Add(readElements[(int)elem]);
					}

					// Vollständige flache Elementliste speichern
					_allElements = readElements.Select(e => e.Value).ToList();
				}

				// Kulturkonfigurationen lesen
				using(Stream currFile = archive.GetEntry("civconfig.xml").Open())
				{
					// XML-Format benutzen
					XDocument doc = XDocument.Load(currFile);
					XElement mainElement = doc.Element("civconfig");
					XElement subElement = mainElement.Element("civs");

					// Liste leer erstellen
					int count = subElement.Descendants("civ").Count();
					_civTrees = new List<CivTreeConfig>();
					for(int i = 0; i < count; ++i)
						_civTrees.Add(new CivTreeConfig());

					// Kulturen lesen
					foreach(XElement fileElement in subElement.Descendants("civ"))
					{
						// Kultur lesen
						_civTrees[(int)fileElement.Attribute("id")].FromXml(fileElement, readElements);
					}
				}
			}
		}

		/// <summary>
		/// Speichert die internen Daten in der angegebenen Datei.
		/// </summary>
		/// <param name="filename">Die Datei, in der die Daten gespeichert werden sollen.</param>
		public void WriteData(string filename)
		{
			// Dateistream erstellen
			using(FileStream stream = new FileStream(filename, FileMode.Create))
			{
				// ZIP-Archiv auf Stream erstellen
				using(ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create))
				{
					// Projektdaten schreiben
					using(Stream currFile = archive.CreateEntry("project.xml").Open())
					{
						// XML-Format benutzen
						XmlWriterSettings settings = new XmlWriterSettings();
						settings.Indent = true;
						using(XmlWriter writer = XmlWriter.Create(currFile, settings))
						{
							// Dokument-Element und Hauptelement schreiben
							writer.WriteStartDocument();
							writer.WriteStartElement("project");

							// Language-DLL-Pfade schreiben
							writer.WriteStartElement("languagefiles");
							{
								// Datei-Pfad einzeln schreiben
								foreach(string currDLLName in _languageFilePaths)
									writer.WriteElementString("file", currDLLName);
							}
							writer.WriteEndElement();

							// Interfac-DRS-Pfad schreiben
							writer.WriteElementString("interfacdrs", _interfacDRSPath);

							// Haupt- und Dokumentelement schließen
							writer.WriteEndElement();
							writer.WriteEndDocument();
						}
					}

					// DAT-Datei schreiben
					using(Stream currFile = archive.CreateEntry("dat.unz").Open())
					{
						_basicGenieFile.WriteData(currFile);
					}

					// Technologiebaumdaten schreiben
					Dictionary<TechTreeElement, int> elementIDs = new Dictionary<TechTreeElement, int>();
					using(Stream currFile = archive.CreateEntry("techtree.xml").Open())
					{
						// XML-Format benutzen
						XmlWriterSettings settings = new XmlWriterSettings();
						settings.Indent = true;
						using(XmlWriter writer = XmlWriter.Create(currFile, settings))
						{
							// Dokument-Element und Hauptelement schreiben
							writer.WriteStartDocument();
							writer.WriteStartElement("techtree");

							// Elemente schreiben
							writer.WriteStartElement("elements");
							{
								// Pro Element Unterbaum schreiben (aber alles auf einer Ebene)
								int lastID = 0;
								_techTreeParentElements.ForEach(p =>
								{
									if(!elementIDs.ContainsKey(p))
										lastID = p.ToXml(writer, elementIDs, lastID);
								});
							}
							writer.WriteEndElement();

							// Elternelemente schreiben
							writer.WriteStartElement("parents");
							{
								// Pro Elternelement ID schreiben
								_techTreeParentElements.ForEach(p => writer.WriteElementString("parent", elementIDs[p].ToString()));
							}
							writer.WriteEndElement();

							// Haupt- und Dokumentelement schließen
							writer.WriteEndElement();
							writer.WriteEndDocument();
						}
					}

					// Kultur-Konfigurationen schreiben
					using(Stream currFile = archive.CreateEntry("civconfig.xml").Open())
					{
						// XML-Format benutzen
						XmlWriterSettings settings = new XmlWriterSettings();
						settings.Indent = true;
						using(XmlWriter writer = XmlWriter.Create(currFile, settings))
						{
							// Dokument-Element und Hauptelement schreiben
							writer.WriteStartDocument();
							writer.WriteStartElement("civconfig");

							// Elemente schreiben
							writer.WriteStartElement("civs");
							{
								// Kultur schreiben
								for(int i = 0; i < _civTrees.Count; ++i)
								{
									// Elementbeginn schreiben
									writer.WriteStartElement("civ");

									// ID schreiben
									writer.WriteAttributeNumber("id", i);

									// Konfigurationen schreiben
									_civTrees[i].ToXml(writer, elementIDs);

									// Elementende schreiben
									writer.WriteEndElement();
								}
							}
							writer.WriteEndElement();

							// Haupt- und Dokumentelement schließen
							writer.WriteEndElement();
							writer.WriteEndDocument();
						}
					}
				}
			}
		}

		#endregion Funktionen

		#region Baumfunktionen

		/// <summary>
		/// Erstellt ein neues Element vom angegebenen Typ und gibt dieses zurück.
		/// </summary>
		/// <param name="type">Der Typ des zu erstellenden Elements.</param>
		/// <returns></returns>
		public TechTreeElement CreateElement(string type)
		{
			// Element erstellen
			TechTreeElement elem = TechTreeElement.CreateFromType(type);

			// Element in interne Liste schreiben
			_allElements.Add(elem);

			// Element zurückgeben
			return elem;
		}

		/// <summary>
		/// Vernichtet das angegebene Element und gibt im Erfolgsfall true zurück.
		/// Falls das Element im Baum noch referenziert wird, wird false zurückgegeben.
		/// </summary>
		/// <param name="element">Das zu löschende Element.</param>
		public bool DestroyElement(TechTreeElement element)
		{
			// Noch Referenzen vorhanden?
			// Das Element darf keine Kinder haben!
			if(element == null || element.HasChildren() || !_allElements.All(e => e.CountReferencesToElement(element) + (e.HasChild(element) ? -1 : 0) <= 0))
			{
				// Fehler
				return false;
			}

			// Alle verweisenden Technologie-Effekte ändern
			_allElements.ForEach(e =>
			{
				// Technologie?
				if(e.GetType() == typeof(TechTreeResearch))
					((TechTreeResearch)e).Effects.ForEach(eff =>
					{
						// Elementverweis löschen
						if(eff.Element == element)
							eff.Element = null;
						if(eff.DestinationElement == element)
							eff.DestinationElement = null;
					});
			});


			// Element suchen und als Kind entfernen
			_allElements.ForEach(e => e.RemoveChild(element));

			// Element aus Liste nehmen
			_allElements.Remove(element);
			_techTreeParentElements.Remove(element);

			// Fertig
			return true;
		}

		/// <summary>
		/// Verschiebt das angegebene Element um offset Zeitalter. Mit diesem verbundene Elemente werden dabei nicht beeinträchtigt.
		/// </summary>
		/// <param name="element">Das zu verschiebende Element.</param>
		/// <param name="offset">Der Betrag, um den das Element verschoben werden soll.</param>
		public void MoveElementAge(TechTreeElement element, int offset)
		{
			// Gültiges Element und Offset?
			if(element == null || element.Age + offset < 0 || element.Age + offset > 4) // TODO: hardcoded...
				return;

			// Vorgänger-Element bestimmen
			TechTreeElement parentElem = _allElements.Find(e => e.HasChild(element) && e != element);
			int minAge = (parentElem == null ? 0 : parentElem.Age);

			// Nachfolger-Element mit minimalem Zeitalter bestimmen
			int maxAge = (element.HasChildren() ? element.GetChildren().Min(e => e.Age) : 4); // TODO: hardcoded...

			// Neues Zeitalter berechnen
			int newAge = element.Age + offset;

			// Falls gültig, zuweisen
			if(minAge <= newAge && newAge <= maxAge)
				element.Age = newAge;
			else
				return;
		}

		/// <summary>
		/// Wendet die angegebenen Kultur-Konfiguration auf den gesamten Baum an.
		/// </summary>
		/// <param name="config">Die anzuwendenden Konfigurationen.</param>
		public void ApplyCivConfiguration(CivTreeConfig config)
		{
			// Alle Elemente durchlaufen
			foreach(TechTreeElement elem in _allElements)
			{
				// Flags bestimmen
				TechTreeElement.ElementFlags flags =
					(config.BlockedElements.Contains(elem) ? TechTreeElement.ElementFlags.Blocked : TechTreeElement.ElementFlags.None) |
					(config.FreeElements.Contains(elem) ? TechTreeElement.ElementFlags.Free : TechTreeElement.ElementFlags.None);

				// Alte Rendering-Flags löschen
				elem.Flags &= ~TechTreeElement.ElementFlags.RenderingFlags;

				// Flags setzen
				elem.Flags |= flags;
			}
		}

		/// <summary>
		/// Sucht das TechTreeElement mit den angegebenen Eigenschaften und gibt eine Referenz auf dieses zurück, oder null.
		/// </summary>
		/// <param name="id">Die ID des zu suchenden Elements.</param>
		/// <param name="type">Der Typ des zu suchenden Elements.</param>
		/// <returns></returns>
		public TechTreeElement FindTechTreeElement(int id, string type)
		{
			// Element suchen
			foreach(var currElem in _allElements)
				if(currElem.ID == id && currElem.Type == type)
					return currElem;

			// Nichts gefunden
			return null;
		}

		/// <summary>
		/// Ordnet das gegebene Element einem anderen unter.
		/// </summary>
		/// <param name="parent">Das Element, dem das andere untergeordnet werden soll.</param>
		/// <param name="child">Das unterzuordnende Element.</param>
		/// <param name="doAlternateAction">Gibt an, ob die zweitmögliche Aktion ausgeführt werden soll. Das sind:<para/>
		/// P-Gebäude -> C-Gebäude: Das C-Gebäude dem P-Gebäude als erschaffbare Einheit unterordnen, nicht als Nachfolger<para/>
		/// P-Einheit -> C-Einheit: Die C-Einheit der P-Einheit als erschaffbare Einheit unterordnen, nicht als Nachfolger</param>
		public void CreateElementLink(TechTreeElement parent, TechTreeElement child, bool doAlternateAction = false)
		{
			// Kind muss angegeben werden
			// Elternelement und Kind dürfen nicht identisch sein
			// Das Elternelement darf kein Kind des Kindelements sein
			if(child == null || parent == child || child.HasChild(parent))
				return;

			// Alte Verbindung löschen
			DeleteElementLink(child);

			// Wenn kein Elternteil angegeben ist, nichts weiter tun
			if(parent == null)
				return;

			// Nach Elementtypen entscheiden
			Type parentType = parent.GetType();
			Type childType = child.GetType();
			if(parentType == typeof(TechTreeBuilding))
			{
				// Element laden
				TechTreeBuilding parentBuilding = (TechTreeBuilding)parent;

				// Ist das Kind ein Gebäude?
				if(childType == typeof(TechTreeBuilding))
				{
					// Element laden
					TechTreeBuilding childBuilding = (TechTreeBuilding)child;

					// Gewünschte Aktion bestimmen
					if(doAlternateAction)
					{
						// Kind lediglich unterordnen
						// Die Button-ID wird standardmäßig auf 0 gesetzt
						parentBuilding.Children.Add(new Tuple<byte, TechTreeElement>(0, childBuilding));

						// Kindelemente nach Button-ID sortieren
						parentBuilding.Children.Sort((c1, c2) => c1.Item1.CompareTo(c2.Item1));
					}
					else
					{
						// Hat das Eltern-Gebäude schon einen Nachfolger? => Dem Kind unterordnen
						if(parentBuilding.Successor != null)
						{
							// Letzten Kind-Nachfolger suchen
							TechTreeBuilding lastChildSuccessor = childBuilding;
							while(lastChildSuccessor.Successor != null)
								lastChildSuccessor = (TechTreeBuilding)lastChildSuccessor.Successor;

							// Gebäude-Nachfolger dem letzten Nachfolger unterordnen
							lastChildSuccessor.Successor = parentBuilding.Successor;
							lastChildSuccessor.SuccessorResearch = parentBuilding.SuccessorResearch;

							// Eine Technologie ist nicht mehr zugewiesen
							parentBuilding.SuccessorResearch = null;
						}

						// Kind dem Gebäude als Nachfolger unterordnen
						parentBuilding.Successor = childBuilding;
					}

					// Element aus Stammelement-Liste löschen
					_techTreeParentElements.Remove(child);
				}
				else if(childType == typeof(TechTreeCreatable) || childType == typeof(TechTreeResearch))
				{
					// Element dem Gebäude als Kindelement unterordnen
					// Die Button-ID wird standardmäßig auf 0 gesetzt
					parentBuilding.Children.Add(new Tuple<byte, TechTreeElement>(0, child));

					// Kindelemente nach Button-ID sortieren
					parentBuilding.Children.Sort((c1, c2) => c1.Item1.CompareTo(c2.Item1));

					// Element aus Stammelement-Liste löschen
					_techTreeParentElements.Remove(child);
				}
			}
			else if(parentType == typeof(TechTreeCreatable))
			{
				// Element laden
				TechTreeCreatable parentCreatable = (TechTreeCreatable)parent;

				// Nach Kind-Typ unterscheiden
				if(childType == typeof(TechTreeCreatable))
				{
					// Element laden
					TechTreeCreatable childCreatable = (TechTreeCreatable)child;

					// Gewünschte Aktion bestimmen
					if(doAlternateAction)
					{
						// Kind lediglich unterordnen
						// Die Button-ID wird standardmäßig auf 0 gesetzt
						parentCreatable.Children.Add(new Tuple<byte, TechTreeElement>(0, childCreatable));

						// Kindelemente nach Button-ID sortieren
						parentCreatable.Children.Sort((c1, c2) => c1.Item1.CompareTo(c2.Item1));
					}
					else
					{
						// Hat das Eltern-Gebäude schon einen Nachfolger? => Dem Kind unterordnen
						if(parentCreatable.Successor != null)
						{
							// Letzten Kind-Nachfolger suchen
							TechTreeCreatable lastChildSuccessor = childCreatable;
							while(lastChildSuccessor.Successor != null)
								lastChildSuccessor = (TechTreeCreatable)lastChildSuccessor.Successor;

							// Gebäude-Nachfolger dem letzten Nachfolger unterordnen
							lastChildSuccessor.Successor = parentCreatable.Successor;
							lastChildSuccessor.SuccessorResearch = parentCreatable.SuccessorResearch;
						}

						// Kind dem Gebäude als Nachfolger unterordnen
						parentCreatable.Successor = childCreatable;

						// Eine Technologie ist noch nicht zugewiesen
						parentCreatable.SuccessorResearch = null;
					}

					// Element aus Stammelement-Liste löschen
					_techTreeParentElements.Remove(child);
				}
				else if(childType == typeof(TechTreeBuilding) || childType == typeof(TechTreeResearch))
				{
					// Element der Einheit als Kindelement unterordnen
					// Die Button-ID wird standardmäßig auf 0 gesetzt
					parentCreatable.Children.Add(new Tuple<byte, TechTreeElement>(0, child));

					// Kindelemente nach Button-ID sortieren
					parentCreatable.Children.Sort((c1, c2) => c1.Item1.CompareTo(c2.Item1));

					// Element aus Stammelement-Liste löschen
					_techTreeParentElements.Remove(child);
				}
			}
			else if(parentType == typeof(TechTreeResearch) && childType == typeof(TechTreeResearch))
			{
				// Kindtechnologie unterordnen
				((TechTreeResearch)parent).Successors.Add((TechTreeResearch)child);

				// Element aus Stammelement-Liste löschen
				_techTreeParentElements.Remove(child);
			}
		}

		/// <summary>
		/// Löscht die Unterordnung des gegebenen Elements und definiert es als Stammelement.
		/// </summary>
		/// <param name="element">Das zu befreiende Element.</param>
		public void DeleteElementLink(TechTreeElement element)
		{
			// Kind muss angegeben werden
			if(element == null)
				return;

			// Ist das Kind schon ein Stammelement? => Nichts zu tun
			if(_techTreeParentElements.Contains(element))
				return;

			// Elternelement des Kinds suchen und das Kind aus dessen Unterordnung entfernen
			_allElements.ForEach(a => a.RemoveChild(element));

			// Kind als Stammelement definieren
			_techTreeParentElements.Add(element);
		}

		/// <summary>
		/// Erstellt eine neue Freischalt-Abhängigkeit.
		/// </summary>
		/// <param name="baseElement">Das Basis-Element.</param>
		/// <param name="dependency">Die Technologie, die das Basis-Element freischalten soll.</param>
		public void CreateMakeAvailDependency(TechTreeElement baseElement, TechTreeResearch dependency)
		{
			// Typ des Hauptelements abrufen
			Type baseElemType = baseElement.GetType();

			// Hat die Basis-Einheit Vorgänger-Elemente?
			// Bei Technologien nicht relevant, da die ja nicht "weiterentwickelt" werden
			if(baseElemType != typeof(TechTreeResearch))
			{
				TechTreeElement parentElem = _allElements.Find(e => e.HasChild(baseElement) && e != baseElement);
				if(parentElem != null)
					if(parentElem.GetType() == typeof(TechTreeBuilding) && ((TechTreeBuilding)parentElem).Successor == baseElement)
						return;
					else if(parentElem.GetType() == typeof(TechTreeCreatable) && ((TechTreeCreatable)parentElem).Successor == baseElement)
						return;
			}

			// Abhängigkeit erstellen
			if(baseElemType == typeof(TechTreeBuilding))
				((TechTreeBuilding)baseElement).EnablerResearch = dependency;
			else if(baseElemType == typeof(TechTreeCreatable))
				((TechTreeCreatable)baseElement).EnablerResearch = dependency;
			else if(baseElemType == typeof(TechTreeResearch))
			{
				// Wenn noch keine Abhängigkeit existiert, Technologie-Abhängigkeit hinzufügen
				TechTreeResearch baseElemResearch = (TechTreeResearch)baseElement;
				if(!baseElemResearch.Dependencies.Contains(dependency))
					baseElemResearch.Dependencies.Add(dependency);
			}
		}

		/// <summary>
		/// Erstellt eine neue Upgrade-Abhängigkeit.
		/// </summary>
		/// <param name="baseElement">Das Basis-Element.</param>
		/// <param name="dependency">Die Technologie, die das Basis-Element weiterentwickeln soll.</param>
		public void CreateUpgradeDependency(TechTreeElement baseElement, TechTreeResearch dependency)
		{
			// Typ des Hauptelements abrufen
			Type baseElemType = baseElement.GetType();

			// Abhängigkeit erstellen
			if(baseElemType == typeof(TechTreeBuilding))
				((TechTreeBuilding)baseElement).SuccessorResearch = dependency;
			else if(baseElemType == typeof(TechTreeCreatable))
				((TechTreeCreatable)baseElement).SuccessorResearch = dependency;
		}

		/// <summary>
		/// Erstellt eine neue Gebäude-Abhängigkeit.
		/// </summary>
		/// <param name="baseElement">Das Basis-Element.</param>
		/// <param name="dependency">Das Gebäude, von dessen Bau das Basis-Element abhängen soll.</param>
		public void CreateBuildingDependency(TechTreeElement baseElement, TechTreeBuilding dependency)
		{
			// Typ des Hauptelements abrufen
			Type baseElemType = baseElement.GetType();

			// Abhängigkeit erstellen
			if(baseElemType == typeof(TechTreeBuilding))
			{
				// Element abrufen
				TechTreeBuilding baseElemBuilding = (TechTreeBuilding)baseElement;

				// Wenn Abhängigkeit noch nicht existiert, diese erstellen
				if(!baseElemBuilding.BuildingDependencies.ContainsKey(dependency))
					baseElemBuilding.BuildingDependencies.Add(dependency, false);
			}
			else if(baseElemType == typeof(TechTreeResearch))
			{
				// Element abrufen
				TechTreeResearch baseElemResearch = (TechTreeResearch)baseElement;

				// Wenn Abhängigkeit noch nicht existiert, diese erstellen
				if(!baseElemResearch.BuildingDependencies.ContainsKey(dependency))
					baseElemResearch.BuildingDependencies.Add(dependency, false);
			}
		}

		/// <summary>
		/// Löscht eine Abhängigkeit.
		/// </summary>
		/// <param name="baseElement">Das Basis-Element.</param>
		/// <param name="dependency">Das referenzierte Element.</param>
		public void DeleteDependency(TechTreeElement baseElement, TechTreeElement dependency)
		{
			// Typ der Elemente abrufen
			Type baseElemType = baseElement.GetType();
			Type depElemType = dependency.GetType();

			// Fallunterscheidung
			if(baseElemType == typeof(TechTreeBuilding))
			{
				// Element abrufen
				TechTreeBuilding baseElemBuilding = (TechTreeBuilding)baseElement;

				// Abhängigkeiten löschen
				if(baseElemBuilding.EnablerResearch == dependency)
					baseElemBuilding.EnablerResearch = null;
				if(baseElemBuilding.SuccessorResearch == dependency)
					baseElemBuilding.SuccessorResearch = null;
				if(depElemType == typeof(TechTreeBuilding))
					if(baseElemBuilding.BuildingDependencies.ContainsKey((TechTreeBuilding)dependency))
						baseElemBuilding.BuildingDependencies.Remove((TechTreeBuilding)dependency);
			}
			else if(baseElemType == typeof(TechTreeCreatable))
			{
				// Element abrufen
				TechTreeCreatable baseElemCreatable = (TechTreeCreatable)baseElement;

				// Abhängigkeiten löschen
				if(baseElemCreatable.EnablerResearch == dependency)
					baseElemCreatable.EnablerResearch = null;
				if(baseElemCreatable.SuccessorResearch == dependency)
					baseElemCreatable.SuccessorResearch = null;
			}
			else if(baseElemType == typeof(TechTreeResearch))
			{
				// Element abrufen
				TechTreeResearch baseElemResearch = (TechTreeResearch)baseElement;

				// Abhängigkeiten löschen
				if(depElemType == typeof(TechTreeBuilding))
				{
					if(baseElemResearch.BuildingDependencies.ContainsKey((TechTreeBuilding)dependency))
						baseElemResearch.BuildingDependencies.Remove((TechTreeBuilding)dependency);
				}
				else if(depElemType == typeof(TechTreeResearch))
				{
					if(baseElemResearch.Dependencies.Contains((TechTreeResearch)dependency))
						baseElemResearch.Dependencies.Remove((TechTreeResearch)dependency);
				}
			}
		}

		/// <summary>
		/// Gibt alle TechTree-Elemente zurück, die die gegebene Bedingung erfüllen.
		/// </summary>
		/// <param name="predicate">Eine Funktion, mit der jedes Element auf eine Bedingung überprüft wird.</param>
		/// <returns></returns>
		public List<TechTreeElement> Where(Func<TechTreeElement, bool> predicate)
		{
			// Elemente auswerten und zurückgeben
			return _allElements.Where(predicate).ToList();
		}

		#endregion Baumfunktionen

		#region Eigenschaften

		/// <summary>
		/// Ruft die zugrundeliegende Basis-DAT-Datei ab.
		/// </summary>
		public GenieLibrary.GenieFile BasicGenieFile
		{
			get { return _basicGenieFile; }
		}

		/// <summary>
		/// Ruft die Elternelemente des enthaltenen Technologiebaums ab oder setzt diese.
		/// Die Erstellung oder Löschung von Einheiten muss diesem Objekt mitgeteilt werden, da dieses auch die kulturspezifischen Einstellungen verwaltet!
		/// </summary>
		public List<TechTreeElement> TechTreeParentElements
		{
			get { return _techTreeParentElements; }
			set { _techTreeParentElements = value; }
		}

		/// <summary>
		/// Ruft das Language-DLL-Wrapperobjekt ab.
		/// </summary>
		public GenieLibrary.LanguageFileWrapper LanguageFileWrapper
		{
			get { return _languageFileWrapper; }
		}

		/// <summary>
		/// Die Language-Datei-Pfade.
		/// </summary>
		public List<string> LanguageFilePaths
		{
			get { return _languageFilePaths; }
			set
			{
				// Dateien laden
				_languageFilePaths = value;

				// DLL-Handles neu erstellen
				_languageFileWrapper = new GenieLibrary.LanguageFileWrapper(_languageFilePaths.ToArray());
			}
		}

		/// <summary>
		/// Der Interfac-DRS-Pfad.
		/// </summary>
		public string InterfacDRSPath
		{
			get { return _interfacDRSPath; }
			set { _interfacDRSPath = value; }
		}

		/// <summary>
		/// Ruft die Kultur-Konfigurationen ab.
		/// </summary>
		public List<CivTreeConfig> CivTrees
		{
			get { return _civTrees; }
			set { _civTrees = value; }
		}

		#endregion Eigenschaften

		#region Strukturen

		/// <summary>
		/// Verwaltet die spezifischen Element-Konfigurationen einer Kultur.
		/// </summary>
		public class CivTreeConfig
		{
			/// <summary>
			/// Die blockierten Elemente.
			/// </summary>
			public List<TechTreeElement> BlockedElements { get; private set; }

			/// <summary>
			/// Die freien Elemente.
			/// Dies sind Elemente, die automatisch entwickelt werden / verfügbar sind.
			/// </summary>
			public List<TechTreeResearch> FreeElements { get; private set; }

			/// <summary>
			/// Die zusätzlichen Civ-Boni.
			/// </summary>
			public List<TechEffect> Bonuses { get; private set; }

			/// <summary>
			/// Die Team-Boni.
			/// </summary>
			public List<TechEffect> TeamBonuses { get; private set; }

			/// <summary>
			/// Konstruktor.
			/// </summary>
			public CivTreeConfig()
			{
				// Objekte erstellen
				BlockedElements = new List<TechTreeElement>();
				FreeElements = new List<TechTreeResearch>();
				Bonuses = new List<TechEffect>();
				TeamBonuses = new List<TechEffect>();
			}

			/// <summary>
			/// Konvertiert die enthaltenen Daten in ein XML-Format und schreibt sie in den übergebenen XmlWriter.
			/// </summary>
			/// <param name="writer">Der XmlWriter, in den die Daten geschrieben werden sollen.</param>
			/// <param name="elementIDs">Die Liste mit den den TechTree-Elementen zugeordneten IDs.</param>
			public void ToXml(XmlWriter writer, Dictionary<TechTreeElement, int> elementIDs)
			{
				// Geblockte Elemente schreiben
				writer.WriteStartElement("blockedelements");
				{
					// Elemente schreiben
					BlockedElements.ForEach(be => writer.WriteElementNumber("element", elementIDs[be]));
				}
				writer.WriteEndElement();

				// Freie Elemente schreiben
				writer.WriteStartElement("freeelements");
				{
					// Elemente schreiben
					FreeElements.ForEach(fe => writer.WriteElementNumber("element", elementIDs[fe]));
				}
				writer.WriteEndElement();

				// Boni schreiben
				writer.WriteStartElement("bonuses");
				{
					// Effekte schreiben
					Bonuses.ForEach(be => be.ToXml(writer, elementIDs));
				}
				writer.WriteEndElement();

				// Team-Boni schreiben
				writer.WriteStartElement("teambonuses");
				{
					// Effekte schreiben
					TeamBonuses.ForEach(be => be.ToXml(writer, elementIDs));
				}
				writer.WriteEndElement();
			}

			/// <summary>
			/// Liest das aktuelle Element aus dem angegebenen XElement.
			/// </summary>
			/// <param name="element">Das XElement, aus dem das Element erstellt werden soll.</param>
			/// <param name="treeElements">Die bereits eingelesenen Baumelemente samt deren IDs.</param>
			public void FromXml(XElement element, Dictionary<int, TechTreeElement> treeElements)
			{
				// Geblockte Elemente einlesen
				BlockedElements = new List<TechTreeElement>();
				foreach(XElement dep in element.Element("blockedelements").Descendants("element"))
					BlockedElements.Add(treeElements[(int)dep]);

				// Freie Elemente einlesen
				FreeElements = new List<TechTreeResearch>();
				foreach(XElement dep in element.Element("freeelements").Descendants("element"))
					FreeElements.Add((TechTreeResearch)treeElements[(int)dep]);

				// Boni einlesen
				Bonuses = new List<TechEffect>();
				foreach(XElement dep in element.Element("bonuses").Descendants("effect"))
					Bonuses.Add(new TechEffect(dep, treeElements));

				// Team-Boni einlesen
				TeamBonuses = new List<TechEffect>();
				foreach(XElement dep in element.Element("teambonuses").Descendants("effect"))
					TeamBonuses.Add(new TechEffect(dep, treeElements));
			}
		}

		#endregion Strukturen
	}
}