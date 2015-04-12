using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using X2AddOnTechTreeEditor.TechTreeStructure;

namespace X2AddOnTechTreeEditor
{
	/// <summary>
	/// Bietet Funktionen für das Importieren einer DAT-Datei in das eigene Format.
	/// </summary>
	public partial class ImportDATFile : Form
	{
		#region Variablen

		/// <summary>
		/// Gibt an, ob das Fenster geschlossen wird.
		/// </summary>
		private bool _formClosing = false;

		/// <summary>
		/// Gibt an, ob das Fenster geschlossen werden kann.
		/// </summary>
		private bool _formClosingAllowed = true;

		/// <summary>
		/// Die neue Projektdatei.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Ruft den Dateinamen der neu erstellten Projektdatei ab.
		/// </summary>
		public string ProjectFileName { get; private set; }

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public ImportDATFile()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Spaltentyp für UnitType-Werte setzen
			_selectionViewTypeColumn.ValueType = typeof(GenieLibrary.DataElements.Civ.Unit.UnitType);

			// ToolTip zuweisen
			_dllHelpBox.SetToolTip(_dllTextBox, "Hier sollen alle referenzierten Language-DLL-Dateien angegeben werden.\nDiese sollen nach Priorität aufsteigend sortiert sein, also zum Beispiel:\n\nLANGUAGE.DLL;language_x1.dll;language_x1_p1.dll");
			_dllHelpBox.InitialDelay = 0;
			_dllHelpBox.ReshowDelay = 0;
			_dllHelpBox.AutomaticDelay = 0;
			_dllHelpBox.ShowAlways = true;

			// Standard-Dialogrückgabestatus setzen
			this.DialogResult = DialogResult.Cancel;
		}

		#endregion Funktionen

		#region Ereignishandler

		private void _datButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openDATDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_datTextBox.Text = _openDATDialog.FileName;
			}
		}

		private void _dllButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openDLLDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_dllTextBox.Text = string.Join(";", _openDLLDialog.FileNames);
			}
		}

		private void _interfacDRSButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openInterfacDRSDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_interfacDRSTextBox.Text = _openInterfacDRSDialog.FileName;
			}
		}

		private void _loadDataButton_Click(object sender, EventArgs e)
		{
			// Sicherstellen, dass DAT-Datei angegeben wurde
			if(!File.Exists(_datTextBox.Text))
			{
				// Fehler
				MessageBox.Show("Fehler: Es wurde keine gültige DAT-Datei angegeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Fortschrittsleiste aktivieren und Mauszeiger ändern, Steuerelemente deaktivieren
			_loadProgressBar.Visible = true;
			this.Cursor = Cursors.WaitCursor;
			_datTextBox.Enabled = false;
			_datButton.Enabled = false;
			_dllTextBox.Enabled = false;
			_dllButton.Enabled = false;
			_interfacDRSTextBox.Enabled = false;
			_interfacDRSButton.Enabled = false;
			_loadDataButton.Enabled = false;

			// Lade-Funktion in separatem Thread ausführen, um Formular nicht zu blockieren
			Task.Factory.StartNew(() =>
			{
				// TechTree-Datei erstellen und DAT-Datei laden
				_projectFile = new TechTreeFile(new GenieLibrary.GenieFile(GenieLibrary.GenieFile.DecompressData(new IORAMHelper.RAMBuffer(_datTextBox.Text))));
				GenieLibrary.GenieFile dat = _projectFile.BasicGenieFile; // Kürzel

				// Wenn DLLs angegeben wurden, diese laden
				List<string> languageFileNames = _dllTextBox.Text.Split(';').ToList();
				languageFileNames.RemoveAll(fn => string.IsNullOrEmpty(fn));
				languageFileNames.Reverse();
				_projectFile.LanguageFilePaths = languageFileNames;

				// Interfac-DRS-Pfad merken
				_projectFile.InterfacDRSPath = _interfacDRSTextBox.Text;

				// Alle Einheiten aus den Völkern kopieren (erst Zivilisationen, dann Gaia, um etwaige Bugs zu vermeiden)
				Dictionary<int, GenieLibrary.DataElements.Civ.Unit> remainingUnits = new Dictionary<int, GenieLibrary.DataElements.Civ.Unit>(dat.UnitHeaders.Count);
				for(int c = dat.Civs.Count - 1; c >= 0; --c)
				{
					// Einheiten kopieren
					GenieLibrary.DataElements.Civ currC = dat.Civs[c];
					foreach(var unit in currC.Units)
						if(!remainingUnits.ContainsKey(unit.Key))
							remainingUnits.Add(unit.Key, unit.Value);
				}

				// Technologien kopieren
				Dictionary<int, GenieLibrary.DataElements.Research> remainingResearches = new Dictionary<int, GenieLibrary.DataElements.Research>(dat.Researches.Count);
				for(int r = 0; r < dat.Researches.Count; ++r)
					remainingResearches.Add(r, dat.Researches[r]);

				// Liste für die bereits entwickelten Technologien
				List<int> completedResearches = new List<int>(dat.Researches.Count);

				// Liste für die Technologie-IDs, die von Gebäuden getriggert werden
				Dictionary<int, List<TechTreeBuilding>> triggeredResearches = new Dictionary<int, List<TechTreeBuilding>>();

				// Alle Gebäude als Stammelemente schreiben, erschaffbare Einheiten aus Einheiten-Liste nehmen und Untereinheiten anhängen
				// Als IDs benutzen wir hier zuerst die DAT-IDs, diese werden dann später in eindeutige IDs umgesetzt
				List<TechTreeUnit> allNormalUnits = new List<TechTreeUnit>(); // Enthält alle auf "normale" Einheiten erstellten Zeiger. Dient lediglich zur Optimierung des Ergebnisses.
				List<TechTreeCreatable> creatableUnits = new List<TechTreeCreatable>(remainingUnits.Count);
				List<TechTreeBuilding> buildings = new List<TechTreeBuilding>(remainingUnits.Count);
				List<TechTreeUnit> otherUnits = new List<TechTreeUnit>(remainingUnits.Count); // Tote und Eye-Candy-Einheiten
				for(int u = remainingUnits.Count - 1; u >= 0; --u)
				{
					// Einheit abrufen
					GenieLibrary.DataElements.Civ.Unit currUnit = remainingUnits.ElementAt(u).Value;
					if(currUnit == null || currUnit.Type < GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable)
						continue;

					// Tote Einheit vorhanden?
					TechTreeUnit deadUnitElement = null;
					if(currUnit.DeadUnitID >= 0)
					{
						// Einheit bereits geladen?
						deadUnitElement = otherUnits.FirstOrDefault(un => un.ID == currUnit.DeadUnitID);
						if(deadUnitElement == null)
						{
							// Einheit erstellen
							if(remainingUnits.ContainsKey(currUnit.DeadUnitID))
							{
								// Einheit abrufen
								GenieLibrary.DataElements.Civ.Unit deadUnit = remainingUnits[currUnit.DeadUnitID];

								// Nach Typ unterscheiden
								if(deadUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.DeadFish)
									deadUnitElement = new TechTreeDead()
									{
										ID = deadUnit.ID1,
										DATUnit = deadUnit
									};
								else if(deadUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.EyeCandy)
									deadUnitElement = new TechTreeEyeCandy()
									{
										ID = deadUnit.ID1,
										DATUnit = deadUnit
									};

								// Wenn nun Einheit gefunden, die bei den toten Einheiten mit speichern
								if(deadUnitElement != null)
									otherUnits.Add(deadUnitElement);

								// TODO: "Mehrfachtod" erschaffbarer Einheiten (siehe z.B. Shaolin)

								// Einheit als gelöscht markieren
								remainingUnits[currUnit.DeadUnitID] = null;
							}
						}
					}

					// Tracking-Einheit vorhanden?
					TechTreeEyeCandy trackingUnitElement = null;
					if(currUnit.DeadFish.TrackingUnit >= 0)
					{
						// Einheit bereits geladen?
						trackingUnitElement = (TechTreeEyeCandy)otherUnits.FirstOrDefault(un => un.ID == currUnit.DeadFish.TrackingUnit);
						if(trackingUnitElement == null)
						{
							// Einheit erstellen
							if(remainingUnits.ContainsKey(currUnit.DeadFish.TrackingUnit))
							{
								// Einheit abrufen
								GenieLibrary.DataElements.Civ.Unit trackUnit = remainingUnits[currUnit.DeadFish.TrackingUnit];

								// Element erstellen
								trackingUnitElement = new TechTreeEyeCandy()
								{
									ID = trackUnit.ID1,
									DATUnit = trackUnit
								};

								// Einheit speichern
								otherUnits.Add(trackingUnitElement);

								// Einheit als gelöscht markieren
								remainingUnits[currUnit.DeadFish.TrackingUnit] = null;
							}
						}
					}

					// Projektil vorhanden?
					TechTreeProjectile projectileUnitElement = null;
					if(currUnit.Type50.ProjectileUnitID >= 0)
					{
						// Einheit bereits geladen?
						projectileUnitElement = (TechTreeProjectile)otherUnits.FirstOrDefault(un => un.ID == currUnit.Type50.ProjectileUnitID);
						if(projectileUnitElement == null)
						{
							// Einheit erstellen
							if(remainingUnits.ContainsKey(currUnit.Type50.ProjectileUnitID))
							{
								// Einheit abrufen
								GenieLibrary.DataElements.Civ.Unit projUnit = remainingUnits[currUnit.Type50.ProjectileUnitID];

								// Element erstellen
								projectileUnitElement = new TechTreeProjectile()
								{
									ID = projUnit.ID1,
									DATUnit = projUnit
								};

								// Einheit speichern
								otherUnits.Add(projectileUnitElement);

								// Einheit als gelöscht markieren
								remainingUnits[currUnit.Type50.ProjectileUnitID] = null;
							}
						}
					}

					// Projektil-Duplikation vorhanden?
					TechTreeProjectile projectileDuplUnitElement = null;
					if(currUnit.Creatable.AlternativeProjectileUnit >= 0)
					{
						// Einheit bereits geladen?
						projectileDuplUnitElement = (TechTreeProjectile)otherUnits.FirstOrDefault(un => un.ID == currUnit.Creatable.AlternativeProjectileUnit);
						if(projectileDuplUnitElement == null)
						{
							// Einheit erstellen
							if(remainingUnits.ContainsKey(currUnit.Creatable.AlternativeProjectileUnit))
							{
								// Einheit abrufen
								GenieLibrary.DataElements.Civ.Unit projDuplUnit = remainingUnits[currUnit.Creatable.AlternativeProjectileUnit];

								// Element erstellen
								projectileDuplUnitElement = new TechTreeProjectile()
								{
									ID = projDuplUnit.ID1,
									DATUnit = projDuplUnit
								};

								// Einheit speichern
								otherUnits.Add(projectileDuplUnitElement);

								// Einheit als gelöscht markieren
								remainingUnits[currUnit.Creatable.AlternativeProjectileUnit] = null;
							}
						}
					}

					// Nach Typen unterscheiden
					if(currUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable)
					{
						// Einheit hinzufügen
						TechTreeCreatable newUnit = new TechTreeCreatable()
						{
							Age = 0,
							ID = currUnit.ID1,
							DATUnit = currUnit,
							DeadUnit = deadUnitElement,
							StandardElement = (currUnit.Creatable.TrainLocationID > 0),
							ProjectileUnit = projectileUnitElement,
							ProjectileDuplicationUnit = projectileDuplUnitElement,
							TrackingUnit = trackingUnitElement,
							Flags = (currUnit.HideInEditor == 0 ? TechTreeElement.ElementFlags.ShowInEditor : TechTreeElement.ElementFlags.None)
						};
						creatableUnits.Add(newUnit);
						allNormalUnits.Add(newUnit);
					}
					else if(currUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.Building)
					{
						// Gebäude hinzufügen
						TechTreeBuilding building = new TechTreeBuilding()
						{
							Age = 0,
							ID = currUnit.ID1,
							DATUnit = currUnit,
							DeadUnit = deadUnitElement,
							StandardElement = (currUnit.Creatable.TrainLocationID > 0),
							ProjectileUnit = projectileUnitElement,
							ProjectileDuplicationUnit = projectileDuplUnitElement,
							ButtonID = currUnit.Creatable.ButtonID,
							Flags = (currUnit.HideInEditor == 0 ? TechTreeElement.ElementFlags.ShowInEditor : TechTreeElement.ElementFlags.None)
						};
						buildings.Add(building);
						allNormalUnits.Add(building);

						// Wenn das Gebäude eine Technologie triggert, diese merken
						if(currUnit.Building.ResearchID >= 0)
						{
							// Liste erstellt?
							if(!triggeredResearches.ContainsKey(currUnit.Building.ResearchID))
								triggeredResearches[currUnit.Building.ResearchID] = new List<TechTreeBuilding>();
							triggeredResearches[currUnit.Building.ResearchID].Add(building);
						}
					}

					// Einheit aus Einheiten-Liste nehmen
					remainingUnits.Remove(currUnit.ID1);
				}

				// Untereinheiten/Querverweise für Gebäude anlegen
				foreach(var currB in buildings)
				{
					// Stack-Einheit?
					if(currB.DATUnit.Building.StackUnitID >= 0)
					{
						// Einheit abrufen
						currB.StackUnit = (TechTreeBuilding)buildings.FirstOrDefault(b => b.ID == currB.DATUnit.Building.StackUnitID);
					}

					// Head-Einheit?
					if(currB.DATUnit.Building.HeadUnit >= 0)
					{
						// Einheit abrufen
						currB.HeadUnit = (TechTreeBuilding)buildings.FirstOrDefault(b => b.ID == currB.DATUnit.Building.HeadUnit);
					}

					// Transform-Einheit?
					if(currB.DATUnit.Building.TransformUnit >= 0)
					{
						// Einheit abrufen
						currB.TransformUnit = (TechTreeBuilding)buildings.FirstOrDefault(b => b.ID == currB.DATUnit.Building.TransformUnit);
					}

					// Annex-Einheiten?
					foreach(var annex in currB.DATUnit.Building.Annexes)
					{
						// Gültiger Annex?
						if(annex.UnitID >= 0)
						{
							// Annex hinzufügen
							TechTreeBuilding annexBuilding = (TechTreeBuilding)buildings.First(b => b.ID == annex.UnitID);
							currB.AnnexUnits.Add(new Tuple<TechTreeBuilding, float, float>(annexBuilding, annex.MisplacementX, annex.MisplacementY));

							// Annex-Gebäude als Schatten-Element markieren, damit es nicht als Elternelement gezeichnet wird
							annexBuilding.ShadowElement = true;
						}
					}
				}

				// Untereinheiten/Querverweise für erschaffbare Einheiten anlegen
				foreach(var currU in creatableUnits)
				{
					// DropSite-Einheiten?
					if(currU.DATUnit.Bird.DropSite1 >= 0)
					{
						// Einheit abrufen
						currU.DropSite1Unit = (TechTreeUnit)allNormalUnits.FirstOrDefault(u => u.ID == currU.DATUnit.Bird.DropSite1);
					}
					if(currU.DATUnit.Bird.DropSite2 >= 0)
					{
						// Einheit abrufen
						currU.DropSite2Unit = (TechTreeUnit)allNormalUnits.FirstOrDefault(u => u.ID == currU.DATUnit.Bird.DropSite2);
					}
				}

				// Den sonstigen Einheiten etwaige Untereinheiten zuweisen
				Dictionary<int, TechTreeUnit> otherUnitsLookup = otherUnits.ToDictionary(elem => elem.ID);
				foreach(var currUnit in otherUnits)
				{
					// Nur Projektile interessant
					if(currUnit.Type == "TechTreeProjectile")
					{
						// Tracking-Einheit vorhanden?
						TechTreeEyeCandy trackingUnitElement = null;
						if(currUnit.DATUnit.DeadFish.TrackingUnit >= 0)
						{
							// Einheit bereits geladen?
							if(otherUnitsLookup.ContainsKey(currUnit.DATUnit.DeadFish.TrackingUnit))
								trackingUnitElement = (TechTreeEyeCandy)otherUnitsLookup[currUnit.DATUnit.DeadFish.TrackingUnit];
							else
							{
								// Einheit erstellen
								if(remainingUnits.ContainsKey(currUnit.DATUnit.DeadFish.TrackingUnit))
								{
									// Einheit abrufen
									GenieLibrary.DataElements.Civ.Unit trackUnit = remainingUnits[currUnit.DATUnit.DeadFish.TrackingUnit];

									// Element erstellen
									trackingUnitElement = new TechTreeEyeCandy()
									{
										ID = trackUnit.ID1,
										DATUnit = trackUnit
									};

									// Einheit speichern
									otherUnitsLookup.Add(trackingUnitElement.ID, trackingUnitElement);

									// Einheit als gelöscht markieren
									remainingUnits[currUnit.DATUnit.DeadFish.TrackingUnit] = null;
								}
							}

							// Einheit zuweisen
							((TechTreeProjectile)currUnit).TrackingUnit = trackingUnitElement;
						}
					}
				}

				// Liste aufräumen
				remainingUnits = remainingUnits.Where(un => un.Value != null).ToDictionary(un => un.Key, un => un.Value);

				// Als Elternelemente kommen nun die aktuell in der Gebäude-Liste befindlichen Objekte infrage, die keine Schattenelemente sind
				List<TechTreeBuilding> parentBuildings = buildings.Where(b => !b.ShadowElement).ToList();

				// Technologien den Gebäuden zuweisen
				Dictionary<int, TechTreeResearch> researches = new Dictionary<int, TechTreeResearch>();
				Dictionary<int, GenieLibrary.DataElements.Research> shadowNodes = new Dictionary<int, GenieLibrary.DataElements.Research>();
				Dictionary<int, GenieLibrary.DataElements.Research> otherResearches = new Dictionary<int, GenieLibrary.DataElements.Research>();
				for(int r = remainingResearches.Count - 1; r >= 0; --r)
				{
					// Technologie abrufen
					GenieLibrary.DataElements.Research currRes = remainingResearches[r];

					// Hat die Technologie ein Gebäude?
					if(currRes.ResearchLocation > 0)
					{
						// Technologie-Objekt erstellen
						TechTreeResearch research = new TechTreeResearch()
						{
							Age = 0,
							ID = r,
							DATResearch = currRes
						};

						// Technologie dem Gebäude zuweisen
						TechTreeBuilding parentBuilding = buildings.FirstOrDefault(u => u.ID == currRes.ResearchLocation);
						if(parentBuilding != null)
							parentBuilding.Children.Add(new Tuple<byte, TechTreeElement>(currRes.ButtonID, research));

						// Technologie aus Rest-Liste entfernen, aber Objekt speichern
						remainingResearches.Remove(r);
						researches.Add(r, research);
					}
					else // Möglicherweise Shadow-Tech
					{
						// Prüfen, ob alle Abhängigkeiten Shadow-Elemente sind
						List<short> reqTechs = currRes.RequiredTechs.Where(dep => dep >= 0).ToList();
						if(reqTechs.Count > 0 && reqTechs.TrueForAll(dep => triggeredResearches.ContainsKey(dep)))
						{
							// Shadow-Node gefunden, merken
							shadowNodes.Add(r, currRes);

							// Technologie aus Rest-Liste entfernen
							remainingResearches.Remove(r);
						}
						else if(currRes.TechageID >= 0) // Die Technologie sollte zumindest einen Effekt haben
						{
							// Rest-Technologie, merken
							otherResearches.Add(r, currRes);

							// Technologie aus Rest-Liste entfernen
							remainingResearches.Remove(r);
						}
					}
				}

				// Solange noch Änderungen stattfinden, nach Shadow-Node-Technologien suchen
				int shadowCount = -1;
				while(shadowCount != shadowNodes.Count)
				{
					// Neue Anzahl speichern
					shadowCount = shadowNodes.Count;

					// Alle Technologien durchlaufen
					for(int r = remainingResearches.Count - 1; r >= 0; --r)
					{
						// Technologie existent?
						if(remainingResearches.ContainsKey(r))
						{
							// Technologie abrufen und auf Kandidat prüfen
							GenieLibrary.DataElements.Research currRes = remainingResearches[r];
							if(currRes.ResearchLocation <= 0)
							{
								// Prüfen, ob alle Abhängigkeiten Shadow-Elemente sind
								List<short> reqTechs = currRes.RequiredTechs.Where(dep => dep >= 0).ToList();
								if(reqTechs.Count > 0 && reqTechs.TrueForAll(dep => triggeredResearches.ContainsKey(dep) || shadowNodes.ContainsKey(dep)))
								{
									// Shadow-Node gefunden, merken
									shadowNodes.Add(r, currRes);

									// Technologie aus Rest-Liste entfernen
									remainingResearches.Remove(r);
								}
							}
						}
					}
				}

				// Zeitalter-Technologien parsen, um Schatten-Weiterentwicklungen bei den Gebäuden zu finden
				// Dunkle Zeit wird hier ignoriert => TODO: hardcoded...
				for(int age = 101; age <= 104; ++age)
				{
					// Technologie-Effekte durchlaufen
					foreach(var effect in dat.Techages[researches[age].DATResearch.TechageID].Effects)
					{
						// Upgrade-Effekte sind hier interessant
						if(effect.Type == 3)
						{
							// Gebäude suchen
							TechTreeBuilding baseBuilding = buildings.FirstOrDefault(b => b.ID == effect.A);
							TechTreeBuilding upgrBuilding = buildings.FirstOrDefault(b => b.ID == effect.B);
							if(baseBuilding != null && upgrBuilding != null)
							{
								// Upgrade-Gebäude dem Basis-Gebäude zuweisen samt der zugehörigen Zeitalter-Nummer
								baseBuilding.AgeUpgrades[age - 100] = upgrBuilding;

								// Upgrade-Gebäude als Schattenelement markieren
								upgrBuilding.ShadowElement = true;

								// Das Upgrade-Gebäude kann kein Elternelement mehr sein
								parentBuildings.Remove(upgrBuilding);
							}
						}
					}
				}

				// Erstellte Technologien korrekt verzweigen
				// Dafür Gebäude durchlaufen und nach Button-IDs vorgehen
				foreach(var currB in buildings)
				{
					// Technologien nach Buttons gruppieren (Button => List<TechTreeResearch>)
					foreach(var g in currB.Children.Where(c => c.Item2.Type == "TechTreeResearch").GroupBy(c => c.Item1).ToDictionary(el => el.Key, el => el.Select(elm => (TechTreeResearch)elm.Item2).ToList()))
					{
						// Technologie-Abhängigkeiten durchsuchen
						bool finish = false; // Abbruchvariable, wenn keine Änderung mehr passiert
						while(!finish)
						{
							// Wenn keine Änderung passiert, in der nächsten Runde abbrechen
							finish = true;

							// Technologien nacheinander durchgehen und nach Abhängigkeit suchen
							TechTreeResearch currR;
							for(int r = 0; r < g.Value.Count; ++r)
							{
								// Technologie abrufen
								currR = (TechTreeResearch)g.Value[r];

								// Abhängigkeiten suchen
								TechTreeResearch dep = g.Value.FirstOrDefault(re => re.DATResearch.RequiredTechs.Contains((short)currR.ID));
								if(dep != null && !currR.Successors.Contains(dep))
								{
									// Abhängigkeit gefunden, Element anhängen
									currR.Successors.Add(dep);

									// Technologie aus Gebäudeelement entfernen
									currB.Children.RemoveAll(el => el.Item2 == dep);

									// Es wurde eine Änderung durchgeführt
									finish = false;
								}
							}
						}
					}

					// Gebäude-Kinder nach Buttons sortieren
					currB.Children.Sort((a, b) => a.Item1.CompareTo(b.Item1));
				}

				// Den Technologien deren Abhängigkeiten zuweisen
				// Bei den Gebäudeabhängigkeiten wird aus Vereinfachungsgründen angenommen, dass maximal eine Unter-Abhängigkeit mit Tiefe 1 existiert; mehr wird aktuell nicht unterstützt.
				// Es können wegen dieser Vereinfachung bei gemoddeten DATs folglich schnell Ungenauigkeiten auftreten, die manuell gefixt werden müssen.
				foreach(var currRes in researches)
				{
					// Abhängigkeiten durchlaufen
					foreach(short depID in currRes.Value.DATResearch.RequiredTechs)
					{
						// Abhängigkeit definiert?
						if(depID > 0)
						{
							// Zeitalter-Abhängigkeit? => TODO: hardcoded...
							// Dunkle Zeit (105) braucht nicht berücksichtigt werden, Age ist eh standardmäßig 0
							if(depID >= 101 && depID <= 104)
							{
								// Zeitalter setzen
								if(currRes.Value.Age < depID - 100)
									currRes.Value.Age = depID - 100;
							}

							// Normale Technologie-Abhängigkeit?
							else if(researches.ContainsKey(depID) && !researches[depID].Successors.Contains(currRes.Value))
							{
								// Abhängigkeit zuweisen
								currRes.Value.Dependencies.Add(researches[depID]);
							}

							// Gebäude-Abhängigkeit?
							else
							{
								// Direkte Abhängigkeit?
								if(triggeredResearches.ContainsKey(depID))
								{
									// Gebäude-Abhängigkeit hinzufügen
									foreach(var b in triggeredResearches[depID])
										if(currRes.Value.DATResearch.ResearchLocation != b.ID && !b.ShadowElement)
											if(currRes.Value.BuildingDependencies.ContainsKey(b))
												currRes.Value.BuildingDependencies[b] = Math.Min(currRes.Value.BuildingDependencies[b], currRes.Value.DATResearch.RequiredTechCount - 1);
											else
												currRes.Value.BuildingDependencies[b] = currRes.Value.DATResearch.RequiredTechCount - 1;
								}

								// Shadow-Node?
								else if(shadowNodes.ContainsKey(depID))
								{
									// Node abrufen
									GenieLibrary.DataElements.Research shadowN = shadowNodes[depID];

									// Abhängigkeiten der Node durchgehen
									foreach(short nDep in shadowN.RequiredTechs)
									{
										// Abhängigkeit definiert?
										if(nDep > 0)
											if(triggeredResearches.ContainsKey(nDep))
											{
												// Gebäude-Abhängigkeit hinzufügen
												foreach(var b in triggeredResearches[nDep])
													if(currRes.Value.DATResearch.ResearchLocation != b.ID && !b.ShadowElement)
														if(currRes.Value.BuildingDependencies.ContainsKey(b))
															currRes.Value.BuildingDependencies[b] = Math.Min(currRes.Value.BuildingDependencies[b], shadowN.RequiredTechCount - 1);
														else
															currRes.Value.BuildingDependencies[b] = shadowN.RequiredTechCount - 1;
											}
											else if(shadowNodes.ContainsKey(nDep))
											{
												// Node abrufen
												GenieLibrary.DataElements.Research shadowN2 = shadowNodes[nDep];

												// Abhängigkeiten der Node durchgehen
												foreach(short nDep2 in shadowN2.RequiredTechs)
												{
													// Abhängigkeit definiert?
													if(nDep2 > 0)
														if(triggeredResearches.ContainsKey(nDep2))
														{
															// Gebäude-Abhängigkeit hinzufügen
															foreach(var b in triggeredResearches[nDep2])
																if(currRes.Value.DATResearch.ResearchLocation != b.ID && !b.ShadowElement)
																	if(currRes.Value.BuildingDependencies.ContainsKey(b))
																		currRes.Value.BuildingDependencies[b] = Math.Min(currRes.Value.BuildingDependencies[b], shadowN2.RequiredTechCount - 1);
																	else
																		currRes.Value.BuildingDependencies[b] = shadowN2.RequiredTechCount - 1;
														}
												}
											}
									}
								}
							}
						}
					}
				}

				// Einheiten suchen, die Gebäuden zugewiesen werden können
				Dictionary<int, Tuple<TechTreeCreatable, TechTreeBuilding>> assignableUnits = new Dictionary<int, Tuple<TechTreeCreatable, TechTreeBuilding>>(creatableUnits.Count);
				for(int u = creatableUnits.Count - 1; u >= 0; --u)
				{
					// Einheit abrufen
					TechTreeCreatable currUnit = creatableUnits[u];

					// Hat die Einheit ein Gebäude?
					if(currUnit.DATUnit.Creatable.TrainLocationID >= 0)
					{
						// Gebäude suchen
						TechTreeBuilding building = buildings.FirstOrDefault(b => b.ID == currUnit.DATUnit.Creatable.TrainLocationID);
						if(building != null)
						{
							// Einheit aus Gesamtliste nehmen und Referenz merken
							creatableUnits.RemoveAt(u);
							assignableUnits.Add(currUnit.ID, new Tuple<TechTreeCreatable, TechTreeBuilding>(currUnit, building));
						}
					}
				}

				// Weiterentwicklungen suchen
				List<Tuple<int, int, TechTreeResearch>> unitUpgradeEffects = new List<Tuple<int, int, TechTreeResearch>>();
				foreach(var currRes in researches)
				{
					// Technologie-Effekte durchlaufen
					foreach(var effect in dat.Techages[currRes.Value.DATResearch.TechageID].Effects)
					{
						// Upgrade-Effekte sind hier interessant
						if(effect.Type == 3)
						{
							// Gebäude suchen
							TechTreeBuilding baseBuilding = buildings.FirstOrDefault(b => b.ID == effect.A);
							TechTreeBuilding upgrBuilding = buildings.FirstOrDefault(b => b.ID == effect.B);
							if(baseBuilding != null && upgrBuilding != null && currRes.Value.DATResearch.Type != 2) // Zeitalter überspringen (wurden bereits behandelt)
							{
								// Ist schon Upgrade-Gebäude zugewiesen? => Welches Upgrade ist früher?
								upgrBuilding.Age = currRes.Value.Age;
								while(baseBuilding.Successor != null && baseBuilding.Successor.Age < upgrBuilding.Age)
								{
									// Upgrade kommt später
									baseBuilding = (TechTreeBuilding)baseBuilding.Successor;
								}

								// Gebäude zuweisen, die Upgrade-Technologie ist die aktuelle
								baseBuilding.Successor = upgrBuilding;
								baseBuilding.SuccessorResearch = currRes.Value;

								// Upgrade-Gebäude aus Elternelement-Liste nehmen
								if(parentBuildings.Contains(upgrBuilding))
									parentBuildings.Remove(upgrBuilding);
							}
							else
							{
								// Einheiten suchen
								var baseUnitRes = assignableUnits.FirstOrDefault(u => u.Key == effect.A);
								var upgrUnitRes = assignableUnits.FirstOrDefault(u => u.Key == effect.B);
								if(baseUnitRes.Value != null && upgrUnitRes.Value != null)
								{
									// Kombination in Upgrade-Liste schreiben
									unitUpgradeEffects.Add(new Tuple<int, int, TechTreeResearch>(baseUnitRes.Value.Item1.ID, upgrUnitRes.Value.Item1.ID, currRes.Value));
								}
							}
						}
					}
				}

				// Einheiten-Upgrade-Ketten auflösen
				List<Tuple<int, int, TechTreeResearch>> unitUpgradeEffectsCopy = new List<Tuple<int, int, TechTreeResearch>>(unitUpgradeEffects);
				foreach(int baseUnitID in unitUpgradeEffects.Where(u1 => !unitUpgradeEffects.Exists(u2 => u2.Item2 == u1.Item1)).Select(elem => elem.Item1).Distinct())
				{
					// Einheit abrufen
					TechTreeCreatable baseUnit = SortUnitUpgrades(unitUpgradeEffectsCopy, baseUnitID, assignableUnits);

					// Zeitalter der Einheit ist bis auf Weiteres dasselbe wie das des Elterngebäudes
					baseUnit.Age = assignableUnits[baseUnitID].Item2.Age;

					// Einheit ihrem Gebäude zuweisen
					assignableUnits[baseUnitID].Item2.Children.Add(new Tuple<byte, TechTreeElement>(baseUnit.DATUnit.Creatable.ButtonID, baseUnit));
				}

				// Freischalt-Technologie-Abhängigkeiten bestimmen
				foreach(var currRes in researches)
				{
					// Effekte durchlaufen
					foreach(var effect in dat.Techages[currRes.Value.DATResearch.TechageID].Effects)
					{
						// Aktivierungseffekt?
						if(effect.Type == 2 && effect.B == 1)
						{
							// Gebäude abrufen
							TechTreeBuilding building = buildings.FirstOrDefault(b => b.ID == effect.A);
							if(building != null)
							{
								// Technologie-Abhängigkeit definieren
								building.EnablerResearch = currRes.Value;
							}
							else if(assignableUnits.ContainsKey(effect.A))
							{
								// Einheit abrufen
								TechTreeCreatable unit = assignableUnits[effect.A].Item1;

								// Zeitalter festlegen
								unit.Age = Math.Max(unit.Age, currRes.Value.Age);

								// Technologie-Abhängigkeit definieren
								unit.EnablerResearch = currRes.Value;
							}
						}
					}
				}

				// Shadow-Nodes in die "Sonstige Technologien"-Liste schreiben, um ggf. falsch einsortierte Technologien zu finden
				foreach(var sn in shadowNodes)
					otherResearches[sn.Key] = sn.Value;

				// Sonstige Technologien durchlaufen und automatische Aktivierungen finden ("make avail"-Techs)
				foreach(var currRes in otherResearches)
				{
					// Effekte durchlaufen
					foreach(var effect in dat.Techages[currRes.Value.TechageID].Effects)
					{
						// Aktivierungseffekt?
						if(effect.Type == 2 && effect.B == 1)
						{
							// Gebäude abrufen
							TechTreeBuilding building = buildings.FirstOrDefault(b => b.ID == effect.A);
							if(building != null)
							{
								// Gebäude-Abhängigkeiten erstellen
								// Abhängigkeiten der Technologie durchgehen
								foreach(short dep in currRes.Value.RequiredTechs)
								{
									// Abhängigkeit definiert?
									if(dep > 0)

										// Zeitalter-Abhängigkeit? Dunkle Zeit kann hier ignoriert werden, da eh Age = 0
										if(dep >= 101 && dep <= 104)
										{
											// Gebäude-Zeitalter setzen
											building.Age = dep - 100;
										}

										// Gebäude-Abhängigkeit?
										else if(triggeredResearches.ContainsKey(dep))
										{
											// Gebäude-Abhängigkeit hinzufügen
											foreach(var b in triggeredResearches[dep])
												if(!b.ShadowElement)
													if(building.BuildingDependencies.ContainsKey(b))
														building.BuildingDependencies[b] = Math.Min(building.BuildingDependencies[b], currRes.Value.RequiredTechCount - 1);
													else
														building.BuildingDependencies[b] = currRes.Value.RequiredTechCount - 1;
										}
								}
							}
							else if(assignableUnits.ContainsKey(effect.A))
							{
								// Einheit abrufen
								TechTreeCreatable unit = assignableUnits[effect.A].Item1;

								// Abhängigkeiten der Technologie durchgehen
								foreach(short dep in currRes.Value.RequiredTechs)
								{
									// Abhängigkeit definiert?
									if(dep > 0)
									{
										// Zeitalter-Abhängigkeit? Dunkle Zeit kann hier ignoriert werden, da eh Age mindestens 0
										if(dep >= 101 && dep <= 104)
										{
											// Einheit-Zeitalter setzen
											unit.Age = Math.Max(unit.Age, dep - 100);
										}
									}
								}
							}
						}
					}
				}

				// Einheiten direkt ihrem Gebäude zuweisen, falls das noch nicht passiert ist
				foreach(var currUnit in assignableUnits)
				{
					// Einheit an ihr Gebäude anfügen
					if(!allNormalUnits.Exists(u => u.CountReferencesToElement(currUnit.Value.Item1) > 0))
						currUnit.Value.Item2.Children.Add(new Tuple<byte, TechTreeElement>(currUnit.Value.Item1.DATUnit.Creatable.ButtonID, currUnit.Value.Item1));
				}

				// Gebäude ggf. Elterneinheiten zuweisen, falls diese keine Bauarbeiter sind (hardcoded: #118)
				for(int b = parentBuildings.Count - 1; b >= 0; --b)
				{
					// Gebäude abrufen
					TechTreeBuilding building = parentBuildings[b];

					// Elterneinheit != Bauarbeiter?
					if(building.DATUnit.Creatable.TrainLocationID != 118)
						if(assignableUnits.ContainsKey(building.DATUnit.Creatable.TrainLocationID))
						{
							// Elterneinheit zuweisen
							assignableUnits[building.DATUnit.Creatable.TrainLocationID].Item1.Children.Add(new Tuple<byte, TechTreeElement>(building.DATUnit.Creatable.ButtonID, building));

							// Zeitalter ggf. übernehmen
							building.Age = Math.Max(building.Age, assignableUnits[building.DATUnit.Creatable.TrainLocationID].Item1.Age);

							// Gebäude aus Elternliste nehmen
							parentBuildings.RemoveAt(b);
						}
						else
						{
							// Elterngebäude suchen
							TechTreeBuilding parentBuilding = buildings.FirstOrDefault(pb => pb.ID == building.DATUnit.Creatable.TrainLocationID);
							if(parentBuilding != null)
							{
								// Elterneinheit zuweisen
								parentBuilding.Children.Add(new Tuple<byte, TechTreeElement>(building.DATUnit.Creatable.ButtonID, building));

								// Zeitalter ggf. übernehmen
								building.Age = Math.Max(building.Age, parentBuilding.Age);

								// Gebäude aus Elternliste nehmen
								parentBuildings.RemoveAt(b);
							}
						}
				}

				// Elternelemente zuweisen
				_projectFile.TechTreeParentElements = new List<TechTreeElement>(buildings);
				_projectFile.TechTreeParentElements.AddRange(otherUnitsLookup.Select(elem => elem.Value));
				_projectFile.TechTreeParentElements.Distinct();
				_projectFile.TechTreeParentElements.RemoveAll(p => _projectFile.TechTreeParentElements.Exists(p2 => p2 != p && p2.HasChild(p)));
				_projectFile.TechTreeParentElements.ForEach(elem =>
				{
					if(elem.GetType() == typeof(TechTreeBuilding) && elem.ShadowElement)
						((TechTreeBuilding)elem).StandardElement = false;
				});

				// Kultur-Konfigurationen berechnen
				List<TechTreeFile.CivTreeConfig> civTrees = new List<TechTreeFile.CivTreeConfig>();
				for(int c = 0; c < dat.Civs.Count; ++c)
				{
					// Kultur-Objekt abrufen
					GenieLibrary.DataElements.Civ currCiv = dat.Civs[c];
					TechTreeFile.CivTreeConfig currConf = new TechTreeFile.CivTreeConfig();
					civTrees.Add(currConf);

					// Gaia überspringen
					if(c == 0)
						continue;

					// Kultur-Techtree anwenden (Sperren usw.)
					foreach(var techEff in dat.Techages[currCiv.TechTreeID].Effects)
					{
						// Typen untersuchen
						switch(techEff.Type)
						{
							// Technologie sperren
							case 102:
								{
									// Gültige Technologie?
									if(techEff.D < 0)
										break;

									// Technologie abrufen
									GenieLibrary.DataElements.Research research = dat.Researches[(int)techEff.D];

									// Technologie-Typ ermitteln
									if(research.ResearchLocation > 0)
									{
										// Normale Technologie => Element suchen und sperren
										currConf.BlockedElements.Add(researches[(int)techEff.D]);
									}
									else
									{
										// Es könnte sich um eine make-avail Technologie handeln
										// Effekte der gesperrten Technologie nach Aktivierungen durchsuchen und diese sperren
										if(research.TechageID >= 0)
											foreach(var currSubEff in dat.Techages[research.TechageID].Effects)
											{
												// Aktivierungs-Effekt?
												if(currSubEff.Type == 2 && currSubEff.B == 1)
												{
													// Zugehörige Einheit suchen
													TechTreeUnit unit = allNormalUnits.FirstOrDefault(u => u.ID == currSubEff.A);
													if(unit != null)
													{
														// Einheit in Sperrliste schreiben
														currConf.BlockedElements.Add(unit);
													}
												}
											}
									}
								}
								break;
						}
					}
				}

				// Kultur-Konfigurationen zuweisen
				_projectFile.CivTrees = civTrees;

				// Fehlende Einheiten in Liste für Restauswahlfeld schreiben
				List<DataGridViewRow> rows = new List<DataGridViewRow>(remainingUnits.Count);
				foreach(var unit in remainingUnits)
				{
					// Element hinzufügen
					DataGridViewRow row = new DataGridViewRow();

					// Werte setzen
					row.Cells.AddRange(
						new DataGridViewCheckBoxCell() { Value = (unit.Value.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.EyeCandy || unit.Value.Class == 5) }, // EyeCandy+Fisch automatisch markieren
						new DataGridViewTextBoxCell() { Value = unit.Value.ID1.ToString() },
						new DataGridViewTextBoxCell() { Value = unit.Value.Name1.Trim() },
						new DataGridViewTextBoxCell() { Value = _projectFile.LanguageFileWrapper.GetString(unit.Value.LanguageDLLName) },
						new DataGridViewTextBoxCell() { Value = unit.Value.Type }
					);

					// Einheit erstellen
					TechTreeUnit unitElem = null;
					switch(unit.Value.Type)
					{
						case GenieLibrary.DataElements.Civ.Unit.UnitType.Building:
							unitElem = new TechTreeBuilding()
							{
								ID = unit.Key,
								DATUnit = unit.Value,
								StandardElement = false
							};
							break;

						case GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable:
							unitElem = new TechTreeCreatable()
							{
								ID = unit.Key,
								DATUnit = unit.Value,
								StandardElement = false
							};
							break;

						default:
							unitElem = new TechTreeEyeCandy()
							{
								ID = unit.Key,
								DATUnit = unit.Value
							};
							break;
					}

					// Element an Zeile anheften
					row.Tag = unitElem;

					// Zeile hinzufügen
					rows.Add(row);
				}
				foreach(var unit in creatableUnits)
				{
					// Element hinzufügen
					DataGridViewRow row = new DataGridViewRow();

					// Werte setzen
					row.Cells.AddRange(
						new DataGridViewCheckBoxCell() { Value = true },
						new DataGridViewTextBoxCell() { Value = unit.DATUnit.ID1.ToString() },
						new DataGridViewTextBoxCell() { Value = unit.DATUnit.Name1.Trim() },
						new DataGridViewTextBoxCell() { Value = _projectFile.LanguageFileWrapper.GetString(unit.DATUnit.LanguageDLLName) },
						new DataGridViewTextBoxCell() { Value = GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable }
					);

					// Element an Zeile anheften
					row.Tag = unit;

					// Zeile hinzufügen
					rows.Add(row);
				}

				// Wenn das Fenster noch offen ist, Steuerelement-Befehle in GUI-Thread ausführen
				if(!_formClosing)
					this.Invoke(new Action(() =>
					{
						// Importmenü deaktivieren und Mauszeiger zurücksetzen
						_loadProgressBar.Visible = false;
						_datTextBox.Enabled = false;
						_loadDataButton.Enabled = false;
						_datButton.Enabled = false;
						this.Cursor = Cursors.Default;

						// Auswahlmenü aktivieren
						_selectionGroupBox.Enabled = true;
						_finishButton.Enabled = true;

						// Fehlende Einheiten zur Auswahl bereitstellen
						_selectionView.SuspendLayout();
						_selectionView.Rows.Clear();
						_selectionView.Rows.AddRange(rows.ToArray());
						_selectionView.ResumeLayout();
					}));
			});
		}

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Ggf. sicherheitshalber fragen
			if(string.IsNullOrWhiteSpace(_datTextBox.Text) || MessageBox.Show("Wollen Sie dieses Fenster wirklich schließen und damit den Importvorgang abbrechen?", "Importvorgang abbrechen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void _selectionView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			// ID-Spalte sortieren
			if(e.Column.Index == 1)
			{
				// Zahlen sortieren
				e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
				e.Handled = true;
			}
		}

		private void ImportDATFile_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Ist das erlaubt?
			if(!_formClosingAllowed)
			{
				// Schließen verbieten
				MessageBox.Show("Bitte warten Sie auf den Abschluss des Vorgangs. Schließen dieses Fensters kann zu Instabilität führen.", "Importvorgang abbrechen", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}

			// Variable setzen, um ggf. asynchrone Aufgaben darüber zu informieren
			_formClosing = true;
		}

		private void _finishButton_Click(object sender, EventArgs e)
		{
			// Speicherfenster anzeigen
			if(_saveProjectDialog.ShowDialog() == DialogResult.OK)
			{
				// Steuerelemente sperren
				_selectionGroupBox.Enabled = false;
				_cancelButton.Enabled = false;
				_finishButton.Enabled = false;
				_formClosingAllowed = false;

				// Ladebalken anzeigen
				_finishProgressBar.Visible = true;
				this.Cursor = Cursors.WaitCursor;

				// Dateinamen merken
				ProjectFileName = _saveProjectDialog.FileName;

				// Fertigstellungs-Funktion in separatem Thread ausführen, um Formular nicht zu blockieren
				Task.Factory.StartNew(() =>
				{
					// Markierte Elemente als Elternelemente eintragen
					foreach(DataGridViewRow row in _selectionView.Rows)
					{
						// Zeile markiert?
						if((bool)row.Cells[0].Value == true)
						{
							// Element als Elternelement definieren
							_projectFile.TechTreeParentElements.Add((TechTreeElement)row.Tag);
						}
					}

					// Projektdatei speichern und neu laden, um Daten-Konsistenz sicherzustellen (Einfügen der Daten hat u.U. interne Variablen nicht korrekt gesetzt)
					_projectFile.WriteData(ProjectFileName);
					_projectFile = new TechTreeFile(ProjectFileName);

					// Sicherstellen, dass Elemente für alle Völker verfügbar sind
					// Elemente können weiterhin als "Gaia-Only" definiert sein, sie sind dennoch bis aus Weiteres für alle Völker verfügbar, das macht die Export-Funktion
					bool isGaia = false;
					int civCount = _projectFile.BasicGenieFile.Civs.Count;
					int count = 0;
					_projectFile.Where(elem => elem is TechTreeUnit).ForEach(u =>
					{
						// Gaia gesetzt?
						isGaia = _projectFile.BasicGenieFile.Civs[0].UnitPointers[u.ID] > 0;

						// Weitere Referenzen zählen
						count = 0;
						for(int i = 1; i < civCount; ++i)
						{
							// Einheit existiert?
							if(_projectFile.BasicGenieFile.Civs[i].UnitPointers[u.ID] > 0)
								++count;
						}

						// Nur-Gaia?
						if(isGaia && count == 0)
							u.Flags |= TechTreeElement.ElementFlags.GaiaOnly;
						if(count < civCount - 1) // In allen Kulturen definiert?
						{
							// Einheit muss in alle Zivilisationen kopiert werden
							for(int i = 0; i < civCount; ++i)
							{
								// Einheit nicht existent?
								if(_projectFile.BasicGenieFile.Civs[i].UnitPointers[u.ID] <= 0)
								{
									// Einheit setzen
									_projectFile.BasicGenieFile.Civs[i].UnitPointers[u.ID] = 1;
									_projectFile.BasicGenieFile.Civs[i].Units[u.ID] = ((TechTreeUnit)u).DATUnit;

									// Einheit als blockiert markieren
									if(i > 0 && !u.Flags.HasFlag(TechTreeElement.ElementFlags.GaiaOnly))
										_projectFile.CivTrees[i].BlockedElements.Add(u);
								}
							}
						}
					});

					// Effekte der importierten Technologien lesen
					// Dies geschieht erst hier, da evtl. noch weitere Einheiten für den Import markiert wurden, die von den Technologien referenziert werden
					List<TechTreeResearch> researches = _projectFile.Where(elem => elem.GetType() == typeof(TechTreeResearch)).Cast<TechTreeResearch>().ToList();
					List<TechTreeElement> units = _projectFile.Where(elem => elem is TechTreeUnit);
					foreach(var res in researches)
					{
						// Effekte durchgehen
						if(res.DATResearch.TechageID >= 0 && res.DATResearch.TechageID < _projectFile.BasicGenieFile.Techages.Count)
							_projectFile.BasicGenieFile.Techages[res.DATResearch.TechageID].Effects.ForEach(eff =>
							{
								// Effekt lesen
								TechEffect newE = new TechEffect();
								newE.Type = (TechEffect.EffectType)eff.Type;
								switch(newE.Type)
								{
									case TechEffect.EffectType.AttributeSet:
									case TechEffect.EffectType.AttributePM:
									case TechEffect.EffectType.AttributeMult:
										newE.Element = units.FirstOrDefault(u => u.ID == eff.A);
										newE.ClassID = eff.B;
										if(newE.Element == null && newE.ClassID < 0)
											newE = null;
										else
										{
											newE.ParameterID = eff.C;
											newE.Value = eff.D;
										}
										break;

									case TechEffect.EffectType.ResourceSetPM:
										newE.ParameterID = eff.A;
										newE.Mode = (TechEffect.EffectMode)eff.B;
										newE.Value = eff.D;
										break;

									case TechEffect.EffectType.ResourceMult:
										newE.ParameterID = eff.A;
										newE.Value = eff.D;
										break;

									case TechEffect.EffectType.ResearchCostSetPM:
										newE.Element = researches.FirstOrDefault(r => r.ID == eff.A);
										if(newE.Element == null)
											newE = null;
										else
										{
											newE.ParameterID = eff.B;
											newE.Mode = (TechEffect.EffectMode)eff.C;
											newE.Value = eff.D;
										}
										break;

									case TechEffect.EffectType.ResearchTimeSetPM:
										newE.Element = researches.FirstOrDefault(r => r.ID == eff.A);
										if(newE.Element == null)
											newE = null;
										else
										{
											newE.Mode = (TechEffect.EffectMode)eff.C;
											newE.Value = eff.D;
										}
										break;

									default:
										newE = null;
										break;
								}
								if(newE != null)
									res.Effects.Add(newE);
							});
					}

					// Projektdatei erneut speichern
					_projectFile.WriteData(ProjectFileName);

					// Fertig
					this.Invoke(new Action(() =>
					{
						// Fenster schließen
						_formClosingAllowed = true;
						this.DialogResult = DialogResult.OK;
						this.Close();
					}));
				});
			}
		}

		#endregion Ereignishandler

		#region Hilfsfunktionen für die Baumerstellung

		/// <summary>
		/// Löst rekursiv die Einheit-Upgrade-Ketten auf und weist der Einheiten ihre jeweiligen Nachfolger zu.
		/// </summary>
		/// <param name="upgrades">Die Upgrade-Paare.</param>
		/// <param name="currBaseUnitID">Die ID der aktuellen "Stamm-Einheit". Das ist die Einheit, die im aktuellen Durchlauf keine Vorgänger hat.</param>
		/// <param name="unitData">Die Liste, die den Einheiten-IDs die Einheiten-Objekte zuordnet.</param>
		private TechTreeCreatable SortUnitUpgrades(List<Tuple<int, int, TechTreeResearch>> upgrades, int currBaseUnitID, Dictionary<int, Tuple<TechTreeCreatable, TechTreeBuilding>> unitData)
		{
			// Die aktuelle Stamm-Einheit abrufen
			TechTreeCreatable baseUnit = unitData[currBaseUnitID].Item1;

			// Nachfolgeeinheiten bestimmen und alle Einträge mit der aktuellen Basiseinheit entfernen
			List<Tuple<int, TechTreeResearch>> children = new List<Tuple<int, TechTreeResearch>>();
			for(int i = upgrades.Count - 1; i >= 0; --i)
				if(upgrades[i].Item1 == currBaseUnitID)
				{
					// Nachfolger merken
					children.Add(new Tuple<int, TechTreeResearch>(upgrades[i].Item2, upgrades[i].Item3));

					// Element aus Upgrade-Liste löschen
					upgrades.RemoveAt(i);
				}

			// Alle Elemente herausnehmen, die einen Vorgänger haben, der nicht die aktuelle Einheit ist
			children.RemoveAll(c => upgrades.Exists(u => u.Item2 == c.Item1));

			// Existiert ein Kind?
			if(children.Count > 0)
			{
				// Rekursiver Aufruf, das Kind ist das nächste Stammelement
				// Es wird immer nur das erste Kind genommen, mehrere Nachfolger werden bis auf weiteres nicht zugelassen
				baseUnit.Successor = SortUnitUpgrades(upgrades, children[0].Item1, unitData);

				// Technologie zuweisen
				baseUnit.SuccessorResearch = children[0].Item2;

				// Zeitalter der Nachfolgeeinheit ist das Zeitalter der entwickelnden Technologie
				baseUnit.Successor.Age = children[0].Item2.Age;
			}

			// Stammeinheit zurückgeben
			return baseUnit;
		}

		#endregion Hilfsfunktionen für die Baumerstellung
	}
}