﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using X2AddOnTechTreeEditor.TechTreeStructure;
using IORAMHelper;

namespace X2AddOnTechTreeEditor
{
	/// <summary>
	/// Bietet Funktionen für das Importieren einer DAT-Datei in das eigene Format.
	/// </summary>
	public partial class ExportDATFile : Form
	{
		#region Variablen

		/// <summary>
		/// Die zu exportierenden Projektdaten.
		/// </summary>
		TechTreeFile _projectFile = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private ExportDATFile()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Standard-Dialogrückgabestatus setzen
			this.DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// Erstellt ein neues Gebäude-Bearbeitungsfenster.
		/// </summary>
		/// <param name="projectFile">Das zugrundeliegende Projekt.</param>
		public ExportDATFile(TechTreeFile projectFile)
			: this()
		{
			// Parameter merken
			_projectFile = projectFile;
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

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Ggf. sicherheitshalber fragen
			if(string.IsNullOrWhiteSpace(_datTextBox.Text) || MessageBox.Show("Wollen Sie dieses Fenster wirklich schließen und damit den Exportvorgang abbrechen?", "Exportvorgang abbrechen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void _finishButton_Click(object sender, EventArgs e)
		{
			// Existiert die ausgewählte DAT-Datei?
			if(!File.Exists(_datTextBox.Text))
			{
				// Fehler
				MessageBox.Show("Fehler: Die angegebene Basis-DAT-Datei existiert nicht!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Speicherfenster anzeigen
			if(_saveDATDialog.ShowDialog() == DialogResult.OK)
			{
				// Steuerelemente sperren
				_datTextBox.Enabled = false;
				_datButton.Enabled = false;
				_finishButton.Enabled = false;

				// Ladebalken anzeigen
				_finishProgressBar.Visible = true;
				this.Cursor = Cursors.WaitCursor;

				// Fertigstellungs-Funktion in separatem Thread ausführen, um Formular nicht zu blockieren
				Task.Factory.StartNew(() =>
				{
					// Basis-DAT laden
					GenieLibrary.GenieFile exportDAT = new GenieLibrary.GenieFile(GenieLibrary.GenieFile.DecompressData(new IORAMHelper.RAMBuffer(_datTextBox.Text)));

					// Neue Grafikdaten zuweisen
					exportDAT.GraphicPointers = _projectFile.BasicGenieFile.GraphicPointers;
					exportDAT.Graphics = _projectFile.BasicGenieFile.Graphics;

					// Alle zu überschreibenden Daten löschen
					exportDAT.Civs.Clear();
					exportDAT.UnitHeaders.Clear();
					exportDAT.Researches.Clear();
					exportDAT.Techages.Clear();

					// Einheiten-ID-Verteilung erstellen
					Dictionary<TechTreeUnit, int> unitIDMap = new Dictionary<TechTreeUnit, int>();

					// Technologie-ID-Verteilung erstellen
					Dictionary<TechTreeResearch, int> researchIDMap = new Dictionary<TechTreeResearch, int>();

					// Alle Einheiten abrufen
					List<TechTreeUnit> units = _projectFile.Where(el => el is TechTreeUnit).Cast<TechTreeUnit>().ToList();

					// ID-geschützte Einheiten haben Vorrang, diesen zuerst IDs zuweisen
					for(int i = units.Count - 1; i >= 0; --i)
					{
						// Geschützt?
						if(units[i].Flags.HasFlag(TechTreeElement.ElementFlags.LockID))
						{
							// ID frei?
							if(!unitIDMap.ContainsValue(units[i].ID))
							{
								// ID vergeben
								unitIDMap[units[i]] = units[i].ID;

								// Einheit aus Liste nehmen
								units.RemoveAt(i);
							}
							else
							{
								// Nicht gut
								throw new Exception("Fehler: Doppelte als 'geschützt' markierte ID!");
							}
						}
					}

					// Verbleibende Standardeinheiten haben nun Priorität, um die KI-Skript-ID-Obergrenze zu vermeiden
					int lastID = 0;
					for(int i = units.Count - 1; i >= 0; --i)
					{
						// Standardeinheit?
						if((units[i].GetType() == typeof(TechTreeBuilding) && ((TechTreeBuilding)units[i]).StandardElement)
						|| (units[i].GetType() == typeof(TechTreeCreatable) && ((TechTreeCreatable)units[i]).StandardElement))
						{
							// Freie ID suchen
							while(unitIDMap.ContainsValue(lastID))
								++lastID;

							// Element einfügen
							unitIDMap[units[i]] = lastID;
							++lastID;

							// Einheit aus Liste nehmen
							units.RemoveAt(i);
						}
					}

					// Restliche Einheiten verteilen
					foreach(var unit in units)
					{
						// Freie ID suchen
						while(unitIDMap.ContainsValue(lastID))
							++lastID;

						// Element einfügen
						unitIDMap[unit] = lastID;
						++lastID;
					}

					// Technologien verteilen, zuerst die ID-geschützten
					List<TechTreeResearch> researches = _projectFile.Where(el => el.GetType() == typeof(TechTreeResearch)).Cast<TechTreeResearch>().ToList();
					for(int i = researches.Count - 1; i >= 0; --i)
					{
						// Geschützt?
						if(researches[i].Flags.HasFlag(TechTreeElement.ElementFlags.LockID))
						{
							// ID frei?
							if(!researchIDMap.ContainsValue(researches[i].ID))
							{
								// ID vergeben
								researchIDMap[researches[i]] = researches[i].ID;

								// Technologie aus Liste nehmen
								researches.RemoveAt(i);
							}
							else
							{
								// Nicht gut
								throw new Exception("Fehler: Doppelte als 'geschützt' markierte ID!");
							}
						}
					}

					// Restliche Technologien verteilen
					lastID = 0;
					foreach(var res in researches)
					{
						// Freie ID suchen
						while(researchIDMap.ContainsValue(lastID))
							++lastID;

						// Element einfügen
						researchIDMap[res] = lastID;
						++lastID;
					}

					// Technologie-Daten erstellen
					SortedDictionary<int, GenieLibrary.DataElements.Research> datResearches = new SortedDictionary<int, GenieLibrary.DataElements.Research>();
					foreach(var res in researchIDMap)
					{
						// Effekt erstellen
						GenieLibrary.DataElements.Techage newTechage = new GenieLibrary.DataElements.Techage();
						exportDAT.Techages.Add(newTechage);

						// Technologie einfügen
						GenieLibrary.DataElements.Research newResearch = new GenieLibrary.DataElements.Research()
						{
							FullTechMode = res.Key.DATResearch.FullTechMode,
							IconID = res.Key.DATResearch.IconID,
							LanguageDLLDescription = res.Key.DATResearch.LanguageDLLDescription,
							LanguageDLLHelp = res.Key.DATResearch.LanguageDLLHelp,
							LanguageDLLName1 = res.Key.DATResearch.LanguageDLLName1,
							LanguageDLLName2 = res.Key.DATResearch.LanguageDLLName2,
							Name = res.Key.DATResearch.Name,
							ResearchTime = res.Key.DATResearch.ResearchTime,
							ResourceCosts = res.Key.DATResearch.ResourceCosts,
							TechageID = (short)(exportDAT.Techages.Count - 1),
							Type = res.Key.DATResearch.Type,
							Unknown1 = res.Key.DATResearch.Unknown1
						};
						datResearches[res.Value] = newResearch;

						// Effekte erstellen
						if(res.Key.DATResearch.Name.Length > 30)
							newTechage.Name = res.Key.DATResearch.Name.Substring(0, 29) + "~\0";
						else
							newTechage.Name = res.Key.DATResearch.Name + "\0";
						CreateTechageEffects(newTechage, res.Key.Effects, unitIDMap, researchIDMap);
					}

					// Alle Gebäude suchen, von denen Elemente abhängen, und entsprechende Technologien definieren
					Dictionary<TechTreeBuilding, int> buildingDependencyResearchIDs = new Dictionary<TechTreeBuilding, int>();
					foreach(var building in _projectFile.Where(b => b.GetType() == typeof(TechTreeBuilding) && _projectFile.Where(el => el.BuildingDependencies.ContainsKey((TechTreeBuilding)b)).Count > 0))
					{
						// Freie ID suchen
						while(datResearches.ContainsKey(lastID))
							++lastID;

						// Technologie für Gebäude-Abhängigkeit erstellen
						GenieLibrary.DataElements.Research buildingDepResearch = new GenieLibrary.DataElements.Research()
						{
							Name = "#BDepTrigger: " + ((TechTreeBuilding)building).DATUnit.Name1,
							ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
							RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
							RequiredTechCount = 1, // Das ist wichtig, damit die Technologie nicht automatisch entwickelt wird
							LanguageDLLName1 = 7000,
							LanguageDLLName2 = 157000,
							LanguageDLLDescription = 8000,
							LanguageDLLHelp = 107000,
							Unknown1 = -1,
							TechageID = -1
						};
						buildingDepResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
						buildingDepResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
						buildingDepResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());

						// Element einfügen
						buildingDependencyResearchIDs[(TechTreeBuilding)building] = lastID;
						datResearches[lastID] = buildingDepResearch;
						++lastID;
					}

					// Technologie-Abhängigkeiten erstellen
					foreach(var res in researchIDMap)
					{
						// Abhängigkeiten erstellen
						CreateResearchDependencyStructure(res.Key, researchIDMap, buildingDependencyResearchIDs, datResearches);
					}

					// Leere Technologie für nicht vergebene DAT-IDs erstellen
					GenieLibrary.DataElements.Research emptyResearch = new GenieLibrary.DataElements.Research()
					{
						Name = "Empty\0",
						ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
						RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
						LanguageDLLName1 = 7000,
						LanguageDLLName2 = 157000,
						LanguageDLLDescription = 8000,
						LanguageDLLHelp = 107000,
						Unknown1 = -1,
						TechageID = -1
					};
					emptyResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
					emptyResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
					emptyResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());

					// Technologiedaten mit leeren Technologien in neue DAT kopieren
					int maxResearchID = datResearches.Keys.Max();
					for(int i = 0; i <= maxResearchID; ++i)
					{
						// Leere Technologie?
						if(datResearches.ContainsKey(i))
							exportDAT.Researches.Add(datResearches[i]);
						else
							exportDAT.Researches.Add(emptyResearch);
					}

					// Einheitenheader und -daten erstellen
					SortedDictionary<int, UnitIDContainer> unitData = new SortedDictionary<int, UnitIDContainer>();
					foreach(var unit in unitIDMap)
					{
						// Header erstellen
						UnitIDContainer newUnit = new UnitIDContainer()
						{
							BaseTreeElement = unit.Key,
							UnitHeader = new GenieLibrary.DataElements.UnitHeader()
							{
								Exists = 1,
								Commands = _projectFile.BasicGenieFile.UnitHeaders[unit.Key.ID].Commands
							},
							AnnexUnits = new List<Tuple<int, float, float>>(),
							ButtonID = -1,
							DeadUnitID = -1,
							HeadUnitID = -1,
							NeedsResearch = false,
							ProjectileDuplicationUnitID = -1,
							ProjectileUnitID = -1,
							StackUnitID = -1,
							TransformUnitID = -1,
							DropSite1UnitID = -1,
							DropSite2UnitID = -1,
							TrackingUnitID = -1,
							TrainParentID = -1
						};

						// Je nach Typ unterscheiden
						if(unit.Key.GetType() == typeof(TechTreeBuilding))
						{
							// Werte setzen
							TechTreeBuilding unitObj = (TechTreeBuilding)unit.Key;
							if(unitObj.DeadUnit != null)
								newUnit.DeadUnitID = unitIDMap[unitObj.DeadUnit];
							if(unitObj.HeadUnit != null)
								newUnit.HeadUnitID = unitIDMap[unitObj.HeadUnit];
							if(unitObj.ProjectileDuplicationUnit != null)
								newUnit.ProjectileDuplicationUnitID = unitIDMap[unitObj.ProjectileDuplicationUnit];
							if(unitObj.ProjectileUnit != null)
								newUnit.ProjectileUnitID = unitIDMap[unitObj.ProjectileUnit];
							if(unitObj.StackUnit != null)
								newUnit.StackUnitID = unitIDMap[unitObj.StackUnit];
							if(unitObj.TransformUnit != null)
								newUnit.TransformUnitID = unitIDMap[unitObj.TransformUnit];
							if(unitObj.EnablerResearch != null || _projectFile.Where(el => el.GetType() == typeof(TechTreeBuilding) && ((TechTreeBuilding)el).Successor == unitObj).Count > 0)
								newUnit.NeedsResearch = true;

							// Button-ID setzen: Hat die Einheit ein Elternelement, in dem sie produziert werden kann?
							TechTreeUnit parent = (TechTreeUnit)_projectFile.Where(el => el is IChildrenContainer && ((IChildrenContainer)el).Children.Exists(c => c.Item2 == unitObj)).FirstOrDefault();
							if(parent != null)
							{
								newUnit.TrainParentID = unitIDMap[parent];
								newUnit.ButtonID = ((IChildrenContainer)parent).Children.First(c => c.Item2 == unitObj).Item1;
							}
							else
								newUnit.ButtonID = unitObj.ButtonID;
						}
						else if(unit.Key.GetType() == typeof(TechTreeCreatable))
						{
							// Werte setzen
							TechTreeCreatable unitObj = (TechTreeCreatable)unit.Key;
							if(unitObj.DeadUnit != null)
								newUnit.DeadUnitID = unitIDMap[unitObj.DeadUnit];
							if(unitObj.ProjectileDuplicationUnit != null)
								newUnit.ProjectileDuplicationUnitID = unitIDMap[unitObj.ProjectileDuplicationUnit];
							if(unitObj.ProjectileUnit != null)
								newUnit.ProjectileUnitID = unitIDMap[unitObj.ProjectileUnit];
							if(unitObj.TrackingUnit != null)
								newUnit.TrackingUnitID = unitIDMap[unitObj.TrackingUnit];
							if(unitObj.DropSite1Unit != null)
								newUnit.DropSite1UnitID = unitIDMap[unitObj.DropSite1Unit];
							if(unitObj.DropSite2Unit != null)
								newUnit.DropSite2UnitID = unitIDMap[unitObj.DropSite2Unit];
							if(unitObj.EnablerResearch != null || _projectFile.Where(el => el.GetType() == typeof(TechTreeCreatable) && ((TechTreeCreatable)el).Successor == unitObj).Count > 0)
								newUnit.NeedsResearch = true;

							// Button-ID setzen: Hat die Einheit ein Elternelement, in dem sie produziert werden kann?
							TechTreeUnit parent = (TechTreeUnit)_projectFile.Where(el => el is IChildrenContainer && ((IChildrenContainer)el).Children.Exists(c => c.Item2 == unitObj)).FirstOrDefault();
							if(parent != null)
							{
								newUnit.TrainParentID = unitIDMap[parent];
								newUnit.ButtonID = ((IChildrenContainer)parent).Children.First(c => c.Item2 == unitObj).Item1;
							}
						}
						else if(unit.Key.GetType() == typeof(TechTreeProjectile))
						{
							// Werte setzen
							TechTreeProjectile unitObj = (TechTreeProjectile)unit.Key;
							if(unitObj.DeadUnit != null)
								newUnit.DeadUnitID = unitIDMap[unitObj.DeadUnit];
							if(unitObj.TrackingUnit != null)
								newUnit.TrackingUnitID = unitIDMap[unitObj.TrackingUnit];
						}

						// Einheitendaten merken
						unitData[unit.Value] = newUnit;

						// TODO: Sonstige Eigenschaften (Gebäude-Grafik-Weiterentwicklungen, Nachfolgertechnologie => Technologie-Abschnitt usw.)
						// TODO: Vererbung der Gebäude-/Button-ID bei Nachfolgeeinheiten
					}

					// Einheitenheader in neue DAT kopieren
					GenieLibrary.DataElements.UnitHeader emptyUnitHeader = new GenieLibrary.DataElements.UnitHeader()
					{
						Exists = 0,
						Commands = new List<GenieLibrary.DataElements.UnitHeader.UnitCommand>()
					};
					int maxUnitID = unitData.Keys.Max();
					for(int u = 0; u <= maxUnitID; ++u)
					{
						// Existiert der Header?
						if(unitData.ContainsKey(u))
							exportDAT.UnitHeaders.Add(unitData[u].UnitHeader);
						else
							exportDAT.UnitHeaders.Add(emptyUnitHeader);
					}

					// Zivilisationsdaten erstellen
					for(int c = 0; c < _projectFile.CivTrees.Count; ++c)
					{
						// Neue Kultur erstellen
						GenieLibrary.DataElements.Civ newCiv = new GenieLibrary.DataElements.Civ()
						{
							IconSet = _projectFile.BasicGenieFile.Civs[c].IconSet,
							Name = _projectFile.BasicGenieFile.Civs[c].Name,
							One = 1,
							Resources = _projectFile.BasicGenieFile.Civs[c].Resources,
							TechTreeID = (short)exportDAT.Techages.Count, // Wird als nächstes Element eingefügt, daher ID klar
							TeamBonusID = (short)(exportDAT.Techages.Count + 1), // Hier ebenso
							UnitPointers = new List<int>(),
							Units = new SortedList<int, GenieLibrary.DataElements.Civ.Unit>()
						};
						exportDAT.Civs.Add(newCiv);

						// Civ-Boni erstellen
						GenieLibrary.DataElements.Techage newBonusTechage = new GenieLibrary.DataElements.Techage();
						if(newCiv.Name.Length > 24)
							newBonusTechage.Name = "CivB: " + newCiv.Name.Substring(0, 23) + "~\0";
						else
							newBonusTechage.Name = "CivB: " + newCiv.Name + "\0";
						CreateTechageEffects(newBonusTechage, _projectFile.CivTrees[c].Bonuses, unitIDMap, researchIDMap);
						exportDAT.Techages.Add(newBonusTechage);

						// Team-Boni erstellen
						newBonusTechage = new GenieLibrary.DataElements.Techage();
						if(newCiv.Name.Length > 24)
							newBonusTechage.Name = "CivT: " + newCiv.Name.Substring(0, 23) + "~\0";
						else
							newBonusTechage.Name = "CivT: " + newCiv.Name + "\0";
						CreateTechageEffects(newBonusTechage, _projectFile.CivTrees[c].TeamBonuses, unitIDMap, researchIDMap);
						exportDAT.Techages.Add(newBonusTechage);

						// Einheiten erstellen
						for(int u = 0; u <= maxUnitID; ++u)
						{
							// Existiert die Einheit?
							if(!unitData.ContainsKey(u))
							{
								// Einheit als nicht-existent markieren
								newCiv.UnitPointers.Add(0);
								continue;
							}

							// Einheit existiert
							UnitIDContainer currUnit = unitData[u];
							newCiv.UnitPointers.Add(1);

							// DAT-Einheit erstellen und einfügen
							GenieLibrary.DataElements.Civ.Unit currDATUnit = (GenieLibrary.DataElements.Civ.Unit)_projectFile.BasicGenieFile.Civs[c].Units[currUnit.BaseTreeElement.ID].Clone();
							newCiv.Units[u] = currDATUnit;

							// IDs ggf. ergänzen
							if(currUnit.BaseTreeElement.GetType() == typeof(TechTreeBuilding))
							{
								currDATUnit.DeadUnitID = (short)currUnit.DeadUnitID;
								currDATUnit.Building.HeadUnit = (short)currUnit.HeadUnitID;
								currDATUnit.Building.StackUnitID = (short)currUnit.StackUnitID;
								currDATUnit.Building.TransformUnit = (short)currUnit.TransformUnitID;
								currDATUnit.Creatable.AlternativeProjectileUnit = (short)currUnit.ProjectileDuplicationUnitID;
								currDATUnit.Type50.ProjectileUnitID = (short)currUnit.ProjectileUnitID;
								currDATUnit.Creatable.ButtonID = (byte)currUnit.ButtonID;
								currDATUnit.Creatable.TrainLocationID = (short)currUnit.TrainParentID;
								currDATUnit.Enabled = (byte)(currUnit.NeedsResearch ? 0 : 1);
							}
							else if(currUnit.BaseTreeElement.GetType() == typeof(TechTreeCreatable))
							{
								currDATUnit.DeadUnitID = (short)currUnit.DeadUnitID;
								currDATUnit.Creatable.AlternativeProjectileUnit = (short)currUnit.ProjectileDuplicationUnitID;
								currDATUnit.Type50.ProjectileUnitID = (short)currUnit.ProjectileUnitID;
								currDATUnit.Creatable.ButtonID = (byte)currUnit.ButtonID;
								currDATUnit.Creatable.TrainLocationID = (short)currUnit.TrainParentID;
								currDATUnit.DeadFish.TrackingUnit = (short)currUnit.TrackingUnitID;
								currDATUnit.Bird.DropSite1 = (short)currUnit.DropSite1UnitID;
								currDATUnit.Bird.DropSite2 = (short)currUnit.DropSite2UnitID;
								currDATUnit.Enabled = (byte)(currUnit.NeedsResearch ? 0 : 1);
							}
							else if(currUnit.BaseTreeElement.GetType() == typeof(TechTreeProjectile))
							{
								currDATUnit.DeadUnitID = (short)currUnit.DeadUnitID;
								currDATUnit.DeadFish.TrackingUnit = (short)currUnit.TrackingUnitID;
							}
						}
					}

					// Neue DAT speichern
					RAMBuffer tmpBuffer = new RAMBuffer();
					exportDAT.WriteData(tmpBuffer);
					GenieLibrary.GenieFile.CompressData(tmpBuffer).Save(_saveDATDialog.FileName);

					// Fertig
					this.Invoke(new Action(() =>
					{
						// Fenster schließen
						this.DialogResult = DialogResult.OK;
						this.Close();
					}));
				});
			}
		}

		#endregion Ereignishandler

		#region Hilfsfunktionen

		/// <summary>
		/// Erstellt die Abhängigkeitsstruktur für die gegebene Technologie.
		/// </summary>
		/// <param name="research">Die Baum-Technologie, für die die Abhängigkeitsstruktur erstellt werden soll.</param>
		/// <param name="researchIDMap">Die Zuordnung von Baum-Technologien zu DAT-IDs.</param>
		/// <param name="buildingDependencyResearchIDs">Die IDs der von Gebäuden induzierten Technologien.</param>
		/// <param name="datResearches">Die bereits erstellten DAT-Technologien.</param>
		/// <returns></returns>
		private void CreateResearchDependencyStructure(TechTreeResearch research, Dictionary<TechTreeResearch, int> researchIDMap, Dictionary<TechTreeBuilding, int> buildingDependencyResearchIDs, SortedDictionary<int, GenieLibrary.DataElements.Research> datResearches)
		{
			// DAT-Technologie der aktuellen Technologie abrufen
			GenieLibrary.DataElements.Research datResearch = datResearches[researchIDMap[research]];

			// Slots ggf. leeren
			datResearch.RequiredTechCount = 0;
			datResearch.RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 });

			// Der erste Slot ist für das Zeitalter
			// TODO: Hardcoded...
			int slotNum = 0;
			if(research.Age > 0)
			{
				datResearch.RequiredTechs[slotNum++] = (short)(100 + research.Age);
				++datResearch.RequiredTechCount;
			}

			// Gebäude-Abhängigkeiten bekommen einen eigenen Knoten und landen dann im 2. Slot
			if(research.BuildingDependencies.Count > 0)
			{
				// Element erstellen
				GenieLibrary.DataElements.Research buildingDepMainNode = new GenieLibrary.DataElements.Research()
				{
					Name = "#BDepMainNode: " + research.DATResearch.Name,
					ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
					RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
					LanguageDLLName1 = 7000,
					LanguageDLLName2 = 157000,
					LanguageDLLDescription = 8000,
					LanguageDLLHelp = 107000,
					Unknown1 = -1,
					TechageID = -1
				};
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());

				// Abhängigkeiten einfügen
				// TODO: Die Anzahlen werden aktuell völlig ignoriert
				int depSlotNum = 0;
				foreach(var dep in research.BuildingDependencies)
				{
					// Maximal 6 Slots => TODO...
					if(depSlotNum >= 6)
						break;

					// Abhängigkeit erstellen
					buildingDepMainNode.RequiredTechs[depSlotNum++] = (short)buildingDependencyResearchIDs[(TechTreeBuilding)dep.Key];
				}

				// Freie ID suchen und Abhängigkeitsknoten erstellen
				int lastID = 0;
				while(datResearches.ContainsKey(lastID))
					++lastID;
				datResearches[lastID] = buildingDepMainNode;
				datResearch.RequiredTechs[slotNum++] = (short)lastID;
				++datResearch.RequiredTechCount;
			}

			// Elternelement suchen, es sollte kein Zeitalter sein
			// TODO: Hardcoded...
			TechTreeElement parent = _projectFile.Where(el => el.GetChildren().Contains(research) && (el.ID < 101 || el.ID > 105)).FirstOrDefault();
			if(parent != null && parent.GetType() == typeof(TechTreeResearch))
			{
				// Abhängigkeit erstellen
				datResearch.RequiredTechs[slotNum++] = (short)researchIDMap[(TechTreeResearch)parent];
				++datResearch.RequiredTechCount;
			}

			// Sonstige Technologie-Abhängigkeiten in die restlichen Slots packen
			foreach(var dep in research.Dependencies)
			{
				// Elternelement haben wir schon
				if(dep != parent)
				{
					// Slots voll?
					// TODO: Noch etwas zu wenig...
					if(slotNum >= 6)
						break;

					// Abhängigkeit erstellen
					datResearch.RequiredTechs[slotNum++] = (short)researchIDMap[dep];
					++datResearch.RequiredTechCount;
				}
			}
		}

		/// <summary>
		/// Erstellt die DAT-Effekte aus der angegebenen Effektliste und schreibt sie in das angegebene Techage-Objekt.
		/// </summary>
		/// <param name="techage">Das zu füllende Techage-Objekt.</param>
		/// <param name="effects">Die zu konvertierenden Effekte.</param>
		/// <param name="unitIDMap">Die Zuordnung von Einheiten und DAT-IDs.</param>
		/// <param name="researchIDMap">Die Zuordnung von Technologien und DAT-IDs.</param>
		private void CreateTechageEffects(GenieLibrary.DataElements.Techage techage, List<TechEffect> effects, Dictionary<TechTreeUnit, int> unitIDMap, Dictionary<TechTreeResearch, int> researchIDMap)
		{
			// Effektliste anlegen
			techage.Effects = new List<GenieLibrary.DataElements.Techage.TechageEffect>(effects.Count);
			foreach(TechEffect eff in effects)
			{
				// Nach Typ unterscheiden
				GenieLibrary.DataElements.Techage.TechageEffect newEff = new GenieLibrary.DataElements.Techage.TechageEffect();
				newEff.Type = (byte)eff.Type;
				switch(eff.Type)
				{
					case TechEffect.EffectType.AttributeSet:
					case TechEffect.EffectType.AttributePM:
					case TechEffect.EffectType.AttributeMult:
						if(eff.Element != null)
							newEff.A = (short)unitIDMap[(TechTreeUnit)eff.Element];
						newEff.B = eff.ClassID;
						newEff.C = eff.ParameterID;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.ResourceSetPM:
						newEff.A = eff.ParameterID;
						newEff.B = (short)eff.Mode;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.UnitEnableDisable:
						if(eff.Element != null)
							newEff.A = (short)unitIDMap[(TechTreeUnit)eff.Element];
						newEff.B = (short)eff.Mode;
						break;

					case TechEffect.EffectType.UnitUpgrade:
						if(eff.Element != null)
							newEff.A = (short)unitIDMap[(TechTreeUnit)eff.Element];
						if(eff.DestinationElement != null)
							newEff.B = (short)unitIDMap[(TechTreeUnit)eff.DestinationElement];
						break;

					case TechEffect.EffectType.ResourceMult:
						newEff.A = eff.ParameterID;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.ResearchCostSetPM:
						if(eff.Element != null)
							newEff.A = (short)researchIDMap[(TechTreeResearch)eff.Element];
						newEff.B = eff.ParameterID;
						newEff.C = (short)eff.Mode;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.ResearchDisable:
						if(eff.Element != null)
							newEff.D = (float)researchIDMap[(TechTreeResearch)eff.Element];
						break;

					case TechEffect.EffectType.ResearchTimeSetPM:
						if(eff.Element != null)
							newEff.A = (short)researchIDMap[(TechTreeResearch)eff.Element];
						newEff.C = (short)eff.Mode;
						newEff.D = eff.Value;
						break;
				}

				// Fertig, Effekt hinzufügen
				techage.Effects.Add(newEff);
			}
		}

		#endregion

		#region Strukturen

		/// <summary>
		/// Enthält alle für eine Einheit vorberechneten Eigenschaften/IDs.
		/// </summary>
		private struct UnitIDContainer
		{
			/// <summary>
			/// Das zugrundeliegende Baum-Element.
			/// </summary>
			public TechTreeUnit BaseTreeElement;

			/// <summary>
			/// Der Einheiten-Header.
			/// </summary>
			public GenieLibrary.DataElements.UnitHeader UnitHeader;

			/// <summary>
			/// Die ID der toten Einheit.
			/// </summary>
			public int DeadUnitID;

			/// <summary>
			/// Die ID der Projektil-Einheit.
			/// </summary>
			public int ProjectileUnitID;

			/// <summary>
			/// Die ID der Projektil-Duplikations-Einheit.
			/// </summary>
			public int ProjectileDuplicationUnitID;

			/// <summary>
			/// Die ID der Stack-Einheit.
			/// </summary>
			public int StackUnitID;

			/// <summary>
			/// Die ID der Head-Einheit.
			/// </summary>
			public int HeadUnitID;

			/// <summary>
			/// Die ID der Ein/Auspack-Einheit.
			/// </summary>
			public int TransformUnitID;

			/// <summary>
			/// Die an ein Gebäude angehefteten Untergebäude und deren Versatzwerte.
			/// Diese Liste darf maximal 4 Elemente enthalten.
			/// </summary>
			public List<Tuple<int, float, float>> AnnexUnits;

			/// <summary>
			/// Gibt an, ob diese Einheit automatisch verfügbar ist oder erst aktiviert werden muss.
			/// </summary>
			public bool NeedsResearch;

			/// <summary>
			/// Die ID der produzierenden Eltern-Einheit.
			/// </summary>
			public int TrainParentID;

			/// <summary>
			/// Die Button-ID der Einheit.
			/// </summary>
			public int ButtonID;

			/// <summary>
			/// Die ID der Tracking-Einheit.
			/// </summary>
			public int TrackingUnitID;

			/// <summary>
			/// Die ID der DropSite-1-Einheit.
			/// </summary>
			public int DropSite1UnitID;

			/// <summary>
			/// Die ID der DropSite-2-Einheit.
			/// </summary>
			public int DropSite2UnitID;
		}

		#endregion
	}
}