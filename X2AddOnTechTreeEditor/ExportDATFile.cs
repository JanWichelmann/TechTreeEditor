using System;
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

					// Listen der zu blockenden Technologie-IDs pro Kultur erstellen
					List<int>[] civBlockIDs = new List<int>[_projectFile.CivTrees.Count];
					for(int c = 0; c < _projectFile.CivTrees.Count; ++c)
						civBlockIDs[c] = new List<int>();

					// Einheiten-ID-Verteilung erstellen
					Dictionary<TechTreeUnit, int> unitIDMap = new Dictionary<TechTreeUnit, int>();

					// Technologie-ID-Verteilung erstellen
					Dictionary<TechTreeResearch, int> researchIDMap = new Dictionary<TechTreeResearch, int>();

					// Technologie-Effektdaten-ID-Verteilung erstellen
					SortedDictionary<int, GenieLibrary.DataElements.Techage> techages = new SortedDictionary<int, GenieLibrary.DataElements.Techage>();

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

					// Dunkle Zeit manuell einfügen
					// TODO: Hardcoded...
					TechTreeResearch darkAgeResearch = new TechTreeResearch()
					{
						DATResearch = _projectFile.BasicGenieFile.Researches[105]
					};
					darkAgeResearch.Effects.Add(new TechEffect() { Type = TechEffect.EffectType.ResourceSetPM, Mode = TechEffect.EffectMode.Set_Disable, ParameterID = 6, Value = 0 });
					darkAgeResearch.Effects.Add(new TechEffect() { Type = TechEffect.EffectType.ResourceSetPM, Mode = TechEffect.EffectMode.Set_Disable, ParameterID = 67, Value = 0 });
					darkAgeResearch.Effects.Add(new TechEffect() { Type = TechEffect.EffectType.ResourceSetPM, Mode = TechEffect.EffectMode.Set_Disable, ParameterID = 66, Value = 1 });
					researchIDMap.Add(darkAgeResearch, 105);

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
						// Technologie einfügen
						GenieLibrary.DataElements.Research newResearch = new GenieLibrary.DataElements.Research()
						{
							Civ = res.Key.DATResearch.Civ,
							FullTechMode = res.Key.DATResearch.FullTechMode,
							IconID = res.Key.DATResearch.IconID,
							LanguageDLLDescription = res.Key.DATResearch.LanguageDLLDescription,
							LanguageDLLHelp = res.Key.DATResearch.LanguageDLLHelp,
							LanguageDLLName1 = res.Key.DATResearch.LanguageDLLName1,
							LanguageDLLName2 = res.Key.DATResearch.LanguageDLLName2,
							Name = res.Key.DATResearch.Name,
							ResearchTime = res.Key.DATResearch.ResearchTime,
							ResourceCosts = res.Key.DATResearch.ResourceCosts,
							TechageID = (short)res.Value,
							Type = res.Key.DATResearch.Type,
							Unknown1 = res.Key.DATResearch.Unknown1
						};
						datResearches[res.Value] = newResearch;

						// Effekt erstellen
						GenieLibrary.DataElements.Techage newTechage = new GenieLibrary.DataElements.Techage();
						techages[res.Value] = newTechage;

						// Effekte erstellen
						if(res.Key.DATResearch.Name.Length > 30)
							newTechage.Name = res.Key.DATResearch.Name.Substring(0, 29) + "~\0";
						else
							newTechage.Name = res.Key.DATResearch.Name + "\0";
						CreateTechageEffects(newTechage, res.Key.Effects, unitIDMap, researchIDMap);
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
							if(unitObj.EnablerResearch != null || _projectFile.Where(el => el is IUpgradeable && ((IUpgradeable)el).Successor == unitObj).Count > 0)
								newUnit.NeedsResearch = true;

							// Standard-Gebäude?
							if(unitObj.StandardElement)
							{
								// Bauarbeiter wählen
								// TODO: Hardcoded...
								newUnit.TrainParentID = 118;
								newUnit.ButtonID = unitObj.ButtonID;
							}
							else
							{
								// Erstmal keine Button-Werte setzen
								newUnit.TrainParentID = -1;
								newUnit.ButtonID = 0;
							}
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
							if(unitObj.EnablerResearch != null)
								newUnit.NeedsResearch = true;

							// Erstmal keine Button-Werte setzen
							newUnit.TrainParentID = -1;
							newUnit.ButtonID = 0;
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
						else if(unit.Key.GetType() == typeof(TechTreeEyeCandy))
						{
							// Werte setzen
							TechTreeEyeCandy unitObj = (TechTreeEyeCandy)unit.Key;
							if(unitObj.DeadUnit != null)
								newUnit.DeadUnitID = unitIDMap[unitObj.DeadUnit];
						}

						// Einheitendaten merken
						unitData[unit.Value] = newUnit;
					}

					// Age-Upgrades durchlaufen und Upgrade-Effekte einrichten
					foreach(var unit in unitIDMap)
					{
						// Gebäude?
						TechTreeBuilding currB = unit.Key as TechTreeBuilding;
						if(currB != null)
						{
							// Upgrades erstellen
							foreach(var currAU in currB.AgeUpgrades)
							{
								// Effekt in jeweiligem Zeitalter erstellen
								// TODO: Hardcoded...
								techages[datResearches[100 + currAU.Key].TechageID].Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
								{
									Type = (byte)TechEffect.EffectType.UnitUpgrade,
									A = (short)unit.Value,
									B = (short)unitIDMap[currAU.Value]
								});

								// Einheit als aktivierungsbedürftig markieren
								UnitIDContainer auData = unitData[unitIDMap[currAU.Value]];
								auData.NeedsResearch = true;

								// Bauort und -button erben
								auData.ButtonID = unitData[unitIDMap[currB]].ButtonID;
								auData.TrainParentID = unitData[unitIDMap[currB]].TrainParentID;
							}
						}
					}

					// Einheiten durchlaufen und Kind-Elementen Orte und Buttons zuweisen
					Dictionary<int, int> upgradeConstraints = new Dictionary<int, int>(); // Die einzelnen Upgrade-Verbindungen, Format: B <- A
					foreach(var unit in _projectFile.Where(u => u is IChildrenContainer && u is TechTreeUnit))
					{
						// Kinder durchlaufen
						short unitID = (short)unitIDMap[(TechTreeUnit)unit];
						foreach(var child in ((IChildrenContainer)unit).Children)
						{
							// Technologie?
							TechTreeResearch currRes = child.Item2 as TechTreeResearch;
							TechTreeUnit currUnit = child.Item2 as TechTreeUnit;
							if(currRes != null)
							{
								// Button und Elternelement setzen
								GenieLibrary.DataElements.Research datRes = datResearches[researchIDMap[currRes]];
								datRes.ResearchLocation = unitID;
								datRes.ButtonID = child.Item1;

								// Rekursiv weiterreichen
								PassDownResearchLocations(currRes, researchIDMap, datResearches);
							}
							else if(currUnit != null) // Einheit?
							{
								// Button und Elternelement setzen
								UnitIDContainer currUnitD = unitData[unitIDMap[currUnit]];
								currUnitD.TrainParentID = unitID;
								currUnitD.ButtonID = child.Item1;

								// Alle Nachfolger durchlaufen und Werte weiterreichen
								if(currUnit is IUpgradeable)
								{
									int lastSuccID = unitIDMap[currUnit];
									TechTreeUnit currSuccessor = ((IUpgradeable)currUnit).Successor;
									while(currSuccessor != null)
									{
										// Upgrade merken
										int currID = unitIDMap[currSuccessor];
										upgradeConstraints[currID] = lastSuccID;

										// Einheitendaten abrufen
										UnitIDContainer currSuccD = unitData[currID];

										// Button-ID vererben
										if(currUnitD.ButtonID > 0 && currSuccD.ButtonID == 0)
											currSuccD.ButtonID = currUnitD.ButtonID;

										// Produktionsort-ID vererben
										if(currUnitD.TrainParentID > -1 && currSuccD.TrainParentID == -1)
											currSuccD.TrainParentID = currUnitD.TrainParentID;

										// Aktivierungszustand vererben
										if(currUnitD.NeedsResearch)
											currSuccD.NeedsResearch = currUnitD.NeedsResearch;

										// Nächster
										lastSuccID = currID;
										if(currSuccessor is IUpgradeable)
											currSuccessor = ((IUpgradeable)currSuccessor).Successor;
										else
											break;
									}
								}
							}
						}
					}

					// Button-IDs etc. ggf. von Elternelementen abwärts vererben
					foreach(var unit in _projectFile.TechTreeParentElements.Where(u => u is IUpgradeable && u is TechTreeUnit))
					{
						// Einheitendaten abrufen
						int unitID = unitIDMap[(TechTreeUnit)unit];
						UnitIDContainer unitD = unitData[unitID];

						// Alle Nachfolger durchlaufen und Werte weiterreichen
						TechTreeUnit currSuccessor = ((IUpgradeable)unit).Successor;
						int lastSuccID = unitID;
						while(currSuccessor != null)
						{
							// Upgrade merken
							int currID = unitIDMap[currSuccessor];
							upgradeConstraints[currID] = lastSuccID;

							// Einheitendaten abrufen
							UnitIDContainer currSuccD = unitData[unitIDMap[currSuccessor]];

							// Button-ID vererben
							if(unitD.ButtonID > 0 && currSuccD.ButtonID == 0)
								currSuccD.ButtonID = unitD.ButtonID;

							// Produktionsort-ID vererben
							if(unitD.TrainParentID > -1 && currSuccD.TrainParentID == -1)
								currSuccD.TrainParentID = unitD.TrainParentID;

							// Aktivierungszustand vererben
							if(unitD.NeedsResearch)
								currSuccD.NeedsResearch = unitD.NeedsResearch;

							// Nächster
							lastSuccID = currID;
							if(currSuccessor is IUpgradeable)
								currSuccessor = ((IUpgradeable)currSuccessor).Successor;
							else
								break;
						}
					}

					// Weiterentwicklungen für alle Einheiten erstellen
					List<TechTreeUnit> nonSuccessorUnits = unitIDMap.Keys.ToList();
					foreach(var unit in _projectFile.Where(u => u is IUpgradeable && u is TechTreeUnit))
					{
						// Ist eine Upgrade-Einheit gesetzt?
						TechTreeUnit succUnit = ((IUpgradeable)unit).Successor;
						TechTreeResearch succRes = ((IUpgradeable)unit).SuccessorResearch;
						if(succUnit != null)
						{
							// Die Einheit ist ein Upgrade und muss aktiviert werden
							nonSuccessorUnits.Remove(succUnit);
							int unitID = unitIDMap[succUnit];
							unitData[unitID].NeedsResearch = true;

							// Effekte erstellen für jede Entwicklungsstufe der Einheit
							int currID = unitID;
							List<GenieLibrary.DataElements.Techage.TechageEffect> effects = new List<GenieLibrary.DataElements.Techage.TechageEffect>();
							while(upgradeConstraints.ContainsKey(currID))
							{
								// ID, die zu dieser Einheit entwickelt werden kann, abrufen
								currID = upgradeConstraints[currID];

								// Upgrade-Effekt erstellen
								effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
								{
									Type = (byte)TechEffect.EffectType.UnitUpgrade,
									A = (short)currID,
									B = (short)unitID
								});
							}

							// Technologie-Upgrade oder automatisches Zeitalter-Upgrade?
							if(succRes == null && succUnit.Age > 0)
							{
								// Auto-Upgrade-Effekt zu Zeitalter-Technologie hinzufügen
								// TODO: Hardcoded...
								techages[datResearches[100 + succUnit.Age].TechageID].Effects.AddRange(effects);
							}
							else if(succRes != null)
							{
								// Upgrade-Effekt zu angegebener Technologie hinzufügen
								techages[datResearches[researchIDMap[succRes]].TechageID].Effects.AddRange(effects);
							}
						}
					}

					// Alle Elemente mit leerer Button-ID von ihrer Elterneinheit lösen
					foreach(var unitD in unitData)
						if(unitD.Value.ButtonID == 0)
							unitD.Value.TrainParentID = -1;
					foreach(var res in datResearches)
						if(res.Value.ButtonID == 0)
							res.Value.ResearchLocation = -1;

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
							TechageID = -1,
							ResearchLocation = -1,
							Civ = -1
						};
						buildingDepResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
						buildingDepResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
						buildingDepResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

						// Element einfügen
						buildingDependencyResearchIDs[(TechTreeBuilding)building] = lastID;

						// Element an Zeitalter-Upgrades weitervererben
						foreach(var au in ((TechTreeBuilding)building).AgeUpgrades)
							buildingDependencyResearchIDs[au.Value] = lastID;

						// Technologie merken
						datResearches[lastID] = buildingDepResearch;
						++lastID;
					}

					// Technologie-Abhängigkeiten erstellen
					foreach(var res in researchIDMap)
					{
						// Abhängigkeiten erstellen
						CreateResearchDependencyStructure(res.Key, researchIDMap, buildingDependencyResearchIDs, datResearches);
					}

					// Einheiten-Abhängigkeiten erstellen
					foreach(TechTreeUnit unit in nonSuccessorUnits)
					{
						// Abhängigkeiten erstellen
						if(CreateUnitDependencyStructure(unitIDMap[unit], unit, researchIDMap, buildingDependencyResearchIDs, datResearches, techages, civBlockIDs))
						{
							// Die Einheit muss aktiviert werden
							unitData[unitIDMap[unit]].NeedsResearch = true;
						}
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
					emptyResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					emptyResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					emptyResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

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

					// Leere Technologie-Effekt-Daten für nicht vergebene Techage-IDs erstellen
					GenieLibrary.DataElements.Techage emptyTechage = new GenieLibrary.DataElements.Techage()
					{
						Name = "Empty\0",
						Effects = new List<GenieLibrary.DataElements.Techage.TechageEffect>()
					};

					// Technologiedaten mit leeren Technologien in neue DAT kopieren
					int maxTechageID = techages.Keys.Max();
					for(int i = 0; i <= maxTechageID; ++i)
					{
						// Leere Technologie?
						if(techages.ContainsKey(i))
							exportDAT.Techages.Add(techages[i]);
						else
							exportDAT.Techages.Add(emptyTechage);
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
						// Kultur-Konfiguration abrufen
						TechTreeFile.CivTreeConfig currCivConfig = _projectFile.CivTrees[c];

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
						CreateTechageEffects(newBonusTechage, currCivConfig.Bonuses, unitIDMap, researchIDMap);
						exportDAT.Techages.Add(newBonusTechage);

						// Verfügbarkeits-Technologie-Blockierungen in Effekte umwandeln
						civBlockIDs[c].ForEach(cb => newBonusTechage.Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
						{
							Type = (byte)TechEffect.EffectType.ResearchDisable,
							D = (float)cb
						}));

						// Baum-Technologie-Blockierungen in Effekte umwandeln
						currCivConfig.BlockedElements.ForEach(res =>
						{
							// Technologie?
							if(res.GetType() == typeof(TechTreeResearch))
							{
								// Technologie blockieren
								newBonusTechage.Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
								{
									Type = (byte)TechEffect.EffectType.ResearchDisable,
									D = (float)researchIDMap[(TechTreeResearch)res]
								});
							}
						});

						// Freie Technologien in Effekte umwandeln
						foreach(TechTreeResearch freeRes in currCivConfig.FreeElements)
						{
							// Zeit auf 0 setzen
							int resID = researchIDMap[freeRes];
							newBonusTechage.Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
							{
								Type = (byte)TechEffect.EffectType.ResearchTimeSetPM,
								A = (short)resID,
								C = (short)TechEffect.EffectMode.Set_Disable,
								D = 0
							});

							// Einzelne Ressourcen auf 0 setzen
							foreach(var resource in datResearches[resID].ResourceCosts)
							{
								// Ressource benutzt? => Löschen
								if(resource.Enabled > 0)
									newBonusTechage.Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
									{
										Type = (byte)TechEffect.EffectType.ResearchCostSetPM,
										A = (short)resID,
										B = resource.Type,
										C = (short)TechEffect.EffectMode.Set_Disable,
										D = 0
									});
							}
						}

						// Team-Boni erstellen
						newBonusTechage = new GenieLibrary.DataElements.Techage();
						if(newCiv.Name.Length > 24)
							newBonusTechage.Name = "CivT: " + newCiv.Name.Substring(0, 23) + "~\0";
						else
							newBonusTechage.Name = "CivT: " + newCiv.Name + "\0";
						CreateTechageEffects(newBonusTechage, currCivConfig.TeamBonuses, unitIDMap, researchIDMap);
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
							currDATUnit.ID1 = currDATUnit.ID2 = currDATUnit.ID3 = (short)u;
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

								// Gebäude-Abhängigkeits-Technologie ggf. setzen
								currDATUnit.Building.ResearchID = (short)(buildingDependencyResearchIDs.ContainsKey((TechTreeBuilding)currUnit.BaseTreeElement) ? buildingDependencyResearchIDs[(TechTreeBuilding)currUnit.BaseTreeElement] : -1);

								// Annex-Werte einfügen
								for(int a = 0; a < 4; ++a)
								{
									// Fertig?
									if(a >= ((TechTreeBuilding)currUnit.BaseTreeElement).AnnexUnits.Count)
									{
										// Leeren Annex einfügen
										currDATUnit.Building.Annexes[a] = new GenieLibrary.DataElements.UnitTypes.Building.BuildingAnnex()
										{
											UnitID = -1,
											MisplacementX = 0,
											MisplacementY = 0
										};
									}
									else
									{
										// Wert einfügen
										var currAnnex = ((TechTreeBuilding)currUnit.BaseTreeElement).AnnexUnits[a];
										currDATUnit.Building.Annexes[a] = new GenieLibrary.DataElements.UnitTypes.Building.BuildingAnnex()
										{
											UnitID = (short)unitIDMap[currAnnex.Item1],
											MisplacementX = currAnnex.Item2,
											MisplacementY = currAnnex.Item3
										};
									}
								}
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
				int lastID = 0;
				int mainDepSlotNum = 0;
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
					TechageID = -1,
					Civ = -1,
					ResearchLocation = -1,
					RequiredTechCount = 1 // Ein Unter-Knoten muss true liefern
				};
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

				// Abhängigkeiten einfügen
				// Abhängigkeitsknoten für Gebäude ohne weitere Anzahl erstellen
				if(research.BuildingDependencies.Any(d => !d.Value))
				{
					// Element erstellen
					GenieLibrary.DataElements.Research buildingDepSubNode = new GenieLibrary.DataElements.Research()
					{
						Name = "#BDepSingleNode: " + research.DATResearch.Name,
						ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
						RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
						LanguageDLLName1 = 7000,
						LanguageDLLName2 = 157000,
						LanguageDLLDescription = 8000,
						LanguageDLLHelp = 107000,
						Unknown1 = -1,
						TechageID = -1,
						Civ = -1,
						ResearchLocation = -1,
						RequiredTechCount = 1 // Eines dieser Gebäude reicht
					};
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

					// Gebäude einfügen
					int depSlotNum = 0;
					foreach(var dep in research.BuildingDependencies)
					{
						// Maximal 6 Slots, das muss reichen
						if(depSlotNum >= 6)
							break;

						// Abhängigkeit erstellen
						if(!dep.Value)
							buildingDepSubNode.RequiredTechs[depSlotNum++] = (short)buildingDependencyResearchIDs[(TechTreeBuilding)dep.Key];
					}

					// Unterknoten speichern
					while(datResearches.ContainsKey(lastID))
						++lastID;
					datResearches[lastID] = buildingDepSubNode;
					buildingDepMainNode.RequiredTechs[mainDepSlotNum++] = (short)lastID;
				}
				// Abhängigkeitsknoten für Gebäude mit weiterer Anzahl erstellen
				if(research.BuildingDependencies.Any(d => d.Value))
				{
					// Element erstellen
					GenieLibrary.DataElements.Research buildingDepSubNode = new GenieLibrary.DataElements.Research()
					{
						Name = "#BDepMultiNode: " + research.DATResearch.Name,
						ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
						RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
						LanguageDLLName1 = 7000,
						LanguageDLLName2 = 157000,
						LanguageDLLDescription = 8000,
						LanguageDLLHelp = 107000,
						Unknown1 = -1,
						TechageID = -1,
						Civ = -1,
						ResearchLocation = -1,
						RequiredTechCount = 2 // Zwei dieser Gebäude benötigt
					};
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

					// Gebäude einfügen
					int depSlotNum = 0;
					foreach(var dep in research.BuildingDependencies)
					{
						// Maximal 6 Slots, das muss reichen
						if(depSlotNum >= 6)
							break;

						// Abhängigkeit erstellen
						if(dep.Value)
							buildingDepSubNode.RequiredTechs[depSlotNum++] = (short)buildingDependencyResearchIDs[(TechTreeBuilding)dep.Key];
					}

					// Unterknoten speichern
					while(datResearches.ContainsKey(lastID))
						++lastID;
					datResearches[lastID] = buildingDepSubNode;
					buildingDepMainNode.RequiredTechs[mainDepSlotNum++] = (short)lastID;
				}

				// Freie ID suchen und Abhängigkeitsknoten erstellen
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
					// Muss reichen
					if(slotNum >= 6)
						break;

					// Abhängigkeit erstellen
					datResearch.RequiredTechs[slotNum++] = (short)researchIDMap[dep];
					++datResearch.RequiredTechCount;
				}
			}
		}

		/// <summary>
		/// Erstellt die Abhängigkeitsstruktur für die gegebene Einheit und gibt true zurück, falls Abhängigkeiten gefunden wurden, sonst false, wenn die Einheit direkt verfügbar ist.
		/// </summary>
		/// <param name="unitID">Die ID der Einheit in der Ausgabe-DAT.</param>
		/// <param name="unit">Die Baum-Einheit, für die die Abhängigkeitsstruktur erstellt werden soll.</param>
		/// <param name="researchIDMap">Die Zuordnung von Baum-Technologien zu DAT-IDs.</param>
		/// <param name="buildingDependencyResearchIDs">Die IDs der von Gebäuden induzierten Technologien.</param>
		/// <param name="datResearches">Die bereits erstellten DAT-Technologien.</param>
		/// <param name="techages">Die Technologie-Effekt-Daten-Liste.</param>
		/// <param name="civBlockIDs">Die pro Kultur zu sperrenden Technologie-IDs.</param>
		/// <returns></returns>
		private bool CreateUnitDependencyStructure(int unitID, TechTreeUnit unit, Dictionary<TechTreeResearch, int> researchIDMap, Dictionary<TechTreeBuilding, int> buildingDependencyResearchIDs, SortedDictionary<int, GenieLibrary.DataElements.Research> datResearches, SortedDictionary<int, GenieLibrary.DataElements.Techage> techages, List<int>[] civBlockIDs)
		{
			// Nur erschaffbare Einheiten bzw. Gebäude sind hier interessant
			TechTreeBuilding unitB = unit as TechTreeBuilding;
			TechTreeCreatable unitC = unit as TechTreeCreatable;
			if(unitB == null && unitC == null)
				return false;

			// Kulturabhängige nötige Blockierungen der gleich erstellten Verfügbarkeits-Technologie erstellen
			List<short> blocks = new List<short>();
			List<short> avails = new List<short>();
			for(short c = 0; c < _projectFile.CivTrees.Count; ++c)
				if(_projectFile.CivTrees[c].BlockedElements.Contains(unit))
					blocks.Add(c);
				else
					avails.Add(c);

			// DAT-Technologie für Abhängigkeit erstellen
			GenieLibrary.DataElements.Research availResearch = new GenieLibrary.DataElements.Research()
			{
				Name = "#Avail: " + unit.DATUnit.Name1,
				Civ = -1,
				ResearchLocation = -1,
				ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
				RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
				RequiredTechCount = 0,
				LanguageDLLName1 = 7000,
				LanguageDLLName2 = 157000,
				LanguageDLLDescription = 8000,
				LanguageDLLHelp = 107000,
				Unknown1 = -1
			};
			availResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
			availResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
			availResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

			// Der erste Slot ist für das Zeitalter
			// TODO: Hardcoded...
			int slotNum = 0;
			if(unit.Age > 0)
			{
				availResearch.RequiredTechs[slotNum++] = (short)(100 + unit.Age);
				++availResearch.RequiredTechCount;
			}

			// Existiert eine freischaltende Technologie?
			if(unitB != null && unitB.EnablerResearch != null)
			{
				availResearch.RequiredTechs[slotNum++] = (short)researchIDMap[unitB.EnablerResearch];
				++availResearch.RequiredTechCount;
			}
			else if(unitC != null && unitC.EnablerResearch != null)
			{
				availResearch.RequiredTechs[slotNum++] = (short)researchIDMap[unitC.EnablerResearch];
				++availResearch.RequiredTechCount;
			}

			// Gebäude-Abhängigkeiten bekommen einen eigenen Knoten und landen dann im 2. Slot
			int lastID = 0; // Die letzte vergebene Technologie-ID
			if(unit.BuildingDependencies.Count > 0)
			{
				// Element erstellen
				int mainDepSlotNum = 0;
				GenieLibrary.DataElements.Research buildingDepMainNode = new GenieLibrary.DataElements.Research()
				{
					Name = "#BDepMainNode: " + unit.DATUnit.Name1,
					ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
					RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
					LanguageDLLName1 = 7000,
					LanguageDLLName2 = 157000,
					LanguageDLLDescription = 8000,
					LanguageDLLHelp = 107000,
					Unknown1 = -1,
					TechageID = -1,
					Civ = -1,
					ResearchLocation = -1,
					RequiredTechCount = 1 // Ein Unter-Knoten muss true liefern
				};
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
				buildingDepMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

				// Abhängigkeiten einfügen
				// Abhängigkeitsknoten für Gebäude ohne weitere Anzahl erstellen
				if(unit.BuildingDependencies.Any(d => !d.Value))
				{
					// Element erstellen
					GenieLibrary.DataElements.Research buildingDepSubNode = new GenieLibrary.DataElements.Research()
					{
						Name = "#BDepSingleNode: " + unit.DATUnit.Name1,
						ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
						RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
						LanguageDLLName1 = 7000,
						LanguageDLLName2 = 157000,
						LanguageDLLDescription = 8000,
						LanguageDLLHelp = 107000,
						Unknown1 = -1,
						TechageID = -1,
						Civ = -1,
						ResearchLocation = -1,
						RequiredTechCount = 1 // Eines dieser Gebäude reicht
					};
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

					// Gebäude einfügen
					int depSlotNum = 0;
					foreach(var dep in unit.BuildingDependencies)
					{
						// Maximal 6 Slots, das muss reichen
						if(depSlotNum >= 6)
							break;

						// Abhängigkeit erstellen
						if(!dep.Value)
							buildingDepSubNode.RequiredTechs[depSlotNum++] = (short)buildingDependencyResearchIDs[(TechTreeBuilding)dep.Key];
					}

					// Unterknoten speichern
					while(datResearches.ContainsKey(lastID))
						++lastID;
					datResearches[lastID] = buildingDepSubNode;
					buildingDepMainNode.RequiredTechs[mainDepSlotNum++] = (short)lastID;
				}
				// Abhängigkeitsknoten für Gebäude mit weiterer Anzahl erstellen
				if(unit.BuildingDependencies.Any(d => d.Value))
				{
					// Element erstellen
					GenieLibrary.DataElements.Research buildingDepSubNode = new GenieLibrary.DataElements.Research()
					{
						Name = "#BDepMultiNode: " + unit.DATUnit.Name1,
						ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
						RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
						LanguageDLLName1 = 7000,
						LanguageDLLName2 = 157000,
						LanguageDLLDescription = 8000,
						LanguageDLLHelp = 107000,
						Unknown1 = -1,
						TechageID = -1,
						Civ = -1,
						ResearchLocation = -1,
						RequiredTechCount = 2 // Zwei dieser Gebäude benötigt
					};
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					buildingDepSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

					// Gebäude einfügen
					int depSlotNum = 0;
					foreach(var dep in unit.BuildingDependencies)
					{
						// Maximal 6 Slots, das muss reichen
						if(depSlotNum >= 6)
							break;

						// Abhängigkeit erstellen
						if(dep.Value)
							buildingDepSubNode.RequiredTechs[depSlotNum++] = (short)buildingDependencyResearchIDs[(TechTreeBuilding)dep.Key];
					}

					// Unterknoten speichern
					while(datResearches.ContainsKey(lastID))
						++lastID;
					datResearches[lastID] = buildingDepSubNode;
					buildingDepMainNode.RequiredTechs[mainDepSlotNum++] = (short)lastID;
				}

				// Freie ID suchen und Abhängigkeitsknoten erstellen, falls benötigt
				if(buildingDepMainNode.RequiredTechs.Count(t => t > -1) > 0)
				{
					while(datResearches.ContainsKey(lastID))
						++lastID;
					datResearches[lastID] = buildingDepMainNode;
					availResearch.RequiredTechs[slotNum++] = (short)lastID;
					++availResearch.RequiredTechCount;
				}
			}

			// Gibt es überhaupt Abhängigkeiten?
			if(availResearch.RequiredTechCount > 0 || blocks.Count > 0)
			{
				// Neue DAT-Technologie speichern
				while(datResearches.ContainsKey(lastID))
					++lastID;
				datResearches[lastID] = availResearch;

				// Nur für eine Kultur?
				if(avails.Count == 1)
					availResearch.Civ = avails[0];
				else
					blocks.ForEach(c => civBlockIDs[c].Add(lastID));

				// Verfügbarkeit-Effekt erstellen
				availResearch.TechageID = (short)lastID;
				techages[lastID] = new GenieLibrary.DataElements.Techage()
				{
					Name = "Avail: " + (unit.DATUnit.Name1.Length <= 23 ? unit.DATUnit.Name1 : unit.DATUnit.Name1.Substring(0, 21) + "~\0"),
					Effects = new List<GenieLibrary.DataElements.Techage.TechageEffect>()
				};
				techages[lastID].Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
				{
					Type = (byte)TechEffect.EffectType.UnitEnableDisable,
					A = (short)unitID,
					B = (short)TechEffect.EffectMode.PM_Enable
				});

				// Nachfolger durchlaufen und schauen, ob nicht blockierte folgen (d.h. z.B. A ist blockiert ab 1. Zeitalter, aber B ab 3. Zeitalter verfügbar)
				TechTreeUnit currSuccessor = ((IUpgradeable)unit).Successor;
				TechTreeResearch currSuccRes = ((IUpgradeable)unit).SuccessorResearch;
				while(currSuccessor != null)
				{
					// Upgrade-Technologie-ID abrufen
					// TODO: Hardcoded [evtl. die Upgrade-Tech-ID merken, um Redundanz zu verhindern?]
					currSuccRes = ((IUpgradeable)currSuccessor).SuccessorResearch;
					int succResID = -1;
					if(currSuccRes != null)
						succResID = researchIDMap[currSuccRes];
					else if(currSuccessor.Age > 0)
						succResID = 100 + currSuccessor.Age;

					// Verfügbarkeits-Technologie erstellen
					GenieLibrary.DataElements.Research civAvailResearch = new GenieLibrary.DataElements.Research()
					{
						Name = "#CivAvail: " + unit.DATUnit.Name1,
						Civ = -1,
						ResearchLocation = -1,
						ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
						RequiredTechs = new List<short>(new short[] { (short)succResID, -1, -1, -1, -1, -1 }),
						RequiredTechCount = 1,
						LanguageDLLName1 = 7000,
						LanguageDLLName2 = 157000,
						LanguageDLLDescription = 8000,
						LanguageDLLHelp = 107000,
						Unknown1 = -1,
						TechageID = availResearch.TechageID
					};
					civAvailResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					civAvailResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					civAvailResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

					// Nachfolger freigegeben?
					avails.Clear();
					for(int i = blocks.Count - 1; i >= 0; --i)
						if(!_projectFile.CivTrees[blocks[i]].BlockedElements.Contains(currSuccessor))
						{
							// Technologie ist für diese Kultur verfügbar
							avails.Add(blocks[i]);

							// Einmalig freigeben reicht
							blocks.RemoveAt(i);
						}

					// Technologie benötigt?
					if(avails.Count > 0)
					{
						// Technologie erstellen
						while(datResearches.ContainsKey(lastID))
							++lastID;
						datResearches[lastID] = civAvailResearch;

						// Für alle anderen Kulturen Technologie blocken
						if(avails.Count == 1)
							civAvailResearch.Civ = avails[0];
						else
							for(int c = 0; c < civBlockIDs.Length; ++c)
								if(!avails.Contains((short)c))
									civBlockIDs[c].Add(lastID);
					}

					// Nächster
					if(currSuccessor is IUpgradeable)
						currSuccessor = ((IUpgradeable)currSuccessor).Successor;
					else
						break;
				}

				// Die Einheit muss aktiviert werden
				return true;
			}
			else
			{
				// Fertig, Einheit muss sonst nicht weiter aktiviert werden
				return false;
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
						else
							newEff.A = -1;
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
						else
							newEff.A = -1;
						newEff.B = (short)eff.Mode;
						break;

					case TechEffect.EffectType.UnitUpgrade:
						if(eff.Element != null)
							newEff.A = (short)unitIDMap[(TechTreeUnit)eff.Element];
						else
							newEff.A = -1;
						if(eff.DestinationElement != null)
							newEff.B = (short)unitIDMap[(TechTreeUnit)eff.DestinationElement];
						else
							newEff.B = -1;
						break;

					case TechEffect.EffectType.ResourceMult:
						newEff.A = eff.ParameterID;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.ResearchCostSetPM:
						if(eff.Element != null)
							newEff.A = (short)researchIDMap[(TechTreeResearch)eff.Element];
						else
							newEff.A = -1;
						newEff.B = eff.ParameterID;
						newEff.C = (short)eff.Mode;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.ResearchDisable:
						if(eff.Element != null)
							newEff.D = (float)researchIDMap[(TechTreeResearch)eff.Element];
						else
							newEff.D = -1.0f;
						break;

					case TechEffect.EffectType.ResearchTimeSetPM:
						if(eff.Element != null)
							newEff.A = (short)researchIDMap[(TechTreeResearch)eff.Element];
						else
							newEff.A = -1;
						newEff.C = (short)eff.Mode;
						newEff.D = eff.Value;
						break;
				}

				// Fertig, Effekt hinzufügen
				techage.Effects.Add(newEff);
			}
		}

		/// <summary>
		/// Verteilt die Technologie-Entwicklungsort-IDs rekursiv an etwaige abhängige Nachfolge-Technologien.
		/// </summary>
		/// <param name="research">Die Technologie, deren Eigenschaften an die Nachfolger verteilt werden sollen.</param>
		/// <param name="researchIDMap">Die Zuordnung von Technologien und DAT-IDs.</param>
		/// <param name="datResearches">Die erstellten Technologie-DAT-Objekte.</param>
		private void PassDownResearchLocations(TechTreeResearch research, Dictionary<TechTreeResearch, int> researchIDMap, SortedDictionary<int, GenieLibrary.DataElements.Research> datResearches)
		{
			// DAT-Technologie abrufen
			GenieLibrary.DataElements.Research datResearch = datResearches[researchIDMap[research]];

			// Kinder durchlaufen
			foreach(var childR in research.Successors)
			{
				// Dem Kind die Werte zuweisen
				GenieLibrary.DataElements.Research datChildR = datResearches[researchIDMap[childR]];
				datChildR.ButtonID = datResearch.ButtonID;
				datChildR.ResearchLocation = datResearch.ResearchLocation;

				// Rekursiver Aufruf
				PassDownResearchLocations(childR, researchIDMap, datResearches);
			}
		}

		#endregion

		#region Strukturen

		/// <summary>
		/// Enthält alle für eine Einheit vorberechneten Eigenschaften/IDs.
		/// </summary>
		private class UnitIDContainer
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