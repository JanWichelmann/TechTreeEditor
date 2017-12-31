using IORAMHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
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
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Gibt an, ob das Projekt nach Ende des Exportvorgangs neu geladen werden muss.
		/// </summary>
		public bool ProjectReloadNeeded
		{
			get;
			private set;
		}

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
		/// Erstellt ein neues DAT-Exportfenster.
		/// </summary>
		/// <param name="projectFile">Das zugrundeliegende Projekt.</param>
		public ExportDATFile(TechTreeFile projectFile)
			: this()
		{
			// Parameter merken
			_projectFile = projectFile;
		}

		/// <summary>
		/// Aktualisiert die Fortschrittsanzeige.
		/// </summary>
		/// <param name="step">Der aktuelle Schritt.</param>
		/// <param name="suffix">Optional. Ein dem Fortschrittslabel anzuhängender String.</param>
		private void UpdateExportStep(ExportSteps step, string suffix = "")
		{
			// In Form-Thread Aktualisierung auslösen
			Invoke(new Action(() =>
			{
				// Fortschrittsbalken setzen
				_finishProgressBar.Value = ExportStepsTexts[step].Item1;

				// Beschreibung zeigen
				_finishProgressLabel.Text = ExportStepsTexts[step].Item2 + suffix;
			}));
		}

		#endregion Funktionen

		#region Ereignishandler

		private void _baseDATButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openDATDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_baseDATTextBox.Text = _openDATDialog.FileName;
			}
		}

		private void _outputDATButton_Click(object sender, EventArgs e)
		{
			// Speicherfenster anzeigen
			if(_saveDATDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_outputDATTextBox.Text = _saveDATDialog.FileName;
			}
		}

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Ggf. sicherheitshalber fragen
			if(string.IsNullOrWhiteSpace(_baseDATTextBox.Text) || MessageBox.Show(Strings.ExportDATFile_Message_CloseWindow, Strings.ExportDATFile_Message_CloseWindow_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void _finishButton_Click(object sender, EventArgs e)
		{
			// Auf TechTree-Design prüfen
			if(_projectFile.ExportNewTechTree && (string.IsNullOrWhiteSpace(_projectFile.TechTreeDesignPath) || !File.Exists(_projectFile.TechTreeDesignPath)))
				if(MessageBox.Show(Strings.ExportDATFile_Message_TechTreeDesignNotFound, Strings.ExportDATFile_Message_TechTreeDesignNotFound_Title, MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
					return;

			// Soll die ID-Zuordnung aktualisiert werden? => Warnung
			if(_updateProjectIDsCheckBox.Checked && MessageBox.Show(Strings.ExportDATFile_Message_SaveIDMapping, Strings.ExportDATFile_Message_SaveIDMapping_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				_updateProjectIDsCheckBox.Checked = false;

			// Existiert die ausgewählte DAT-Datei?
			if(!File.Exists(_baseDATTextBox.Text))
			{
				// Fehler
				MessageBox.Show(Strings.ExportDATFile_Message_DATNotFound, Strings.ExportDATFile_Message_DATNotFound_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Steuerelemente sperren
			_baseDATTextBox.Enabled = false;
			_baseDATButton.Enabled = false;
			_outputDATTextBox.Enabled = false;
			_outputDATButton.Enabled = false;
			_finishButton.Enabled = false;

			// Ladebalken anzeigen
			_finishProgressBar.Visible = true;
			_finishProgressBar.Maximum = ExportStepsTexts.Max(est => est.Value.Item1);
			_finishProgressLabel.Visible = true;
			this.Cursor = Cursors.WaitCursor;

			// Fertigstellungs-Funktion in separatem Thread ausführen, um Formular nicht zu blockieren
			Task.Run(() =>
			{
				// Basis-DAT laden
				UpdateExportStep(ExportSteps.Initialize);
				GenieLibrary.GenieFile exportDAT = new GenieLibrary.GenieFile(GenieLibrary.GenieFile.DecompressData(new RAMBuffer(_baseDATTextBox.Text)));

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

				// Gewisse IDs sind freizuhalten (Spanische Kriegsgaleone)
				// TODO: Dies kann man später vielleicht noch mit einer ID-Änderungsfunktion ergänzen, sodass diese Einheiten im Programm referenziert werden können
				TechTreeCreatable unit768 = new TechTreeCreatable() { ID = 768 };
				TechTreeCreatable unit770 = new TechTreeCreatable() { ID = 770 };
				unitIDMap.Add(unit768, 768);
				unitIDMap.Add(unit770, 770);

				// Technologie-ID-Verteilung erstellen
				Dictionary<TechTreeResearch, int> researchIDMap = new Dictionary<TechTreeResearch, int>();

				// Technologie-Effektdaten-ID-Verteilung erstellen
				SortedDictionary<int, GenieLibrary.DataElements.Techage> techages = new SortedDictionary<int, GenieLibrary.DataElements.Techage>();

				// Alle Einheiten abrufen
				List<TechTreeUnit> units = _projectFile.Where(el => el is TechTreeUnit).Cast<TechTreeUnit>().ToList();

				// ID-geschützte Einheiten haben Vorrang, diesen zuerst IDs zuweisen
				UpdateExportStep(ExportSteps.CalculateUnitMapping);
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
							throw new Exception(string.Format(Strings.ExportDATFile_Exception_AmbiguousLockedID, units[i].ID));
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

				// Etwas Platz lassen für etwaige später eingefügte Standard-Einheiten
				if(_updateProjectIDsCheckBox.Checked)
					lastID += 50;

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

				// Dunkle Zeit in Technologie-Liste einfügen, hat höchste Priorität
				UpdateExportStep(ExportSteps.CalculateResearchMapping);
				TechTreeResearch darkAgeResearch = new TechTreeResearch()
				{
					DATResearch = _projectFile.BasicGenieFile.Researches[100 + _projectFile.AgeCount],
					ID = 100 + _projectFile.AgeCount
				};
				darkAgeResearch.Effects.Add(new TechEffect() { Type = TechEffect.EffectType.ResourceSetPM, Mode = TechEffect.EffectMode.Set_Disable, ParameterID = 6, Value = 0 });
				darkAgeResearch.Effects.Add(new TechEffect() { Type = TechEffect.EffectType.ResourceSetPM, Mode = TechEffect.EffectMode.Set_Disable, ParameterID = 67, Value = 0 });
				darkAgeResearch.Effects.Add(new TechEffect() { Type = TechEffect.EffectType.ResourceSetPM, Mode = TechEffect.EffectMode.Set_Disable, ParameterID = 66, Value = 1 });
				researchIDMap.Add(darkAgeResearch, 100 + _projectFile.AgeCount);

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
							throw new Exception(Strings.ExportDATFile_Exception_AmbiguousLockedID);
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
				UpdateExportStep(ExportSteps.BuildTechEffects);
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
				UpdateExportStep(ExportSteps.BuildUnitData);
				SortedDictionary<int, UnitIDContainer> unitData = new SortedDictionary<int, UnitIDContainer>();
				foreach(var unit in unitIDMap)
				{
					// Fähigkeitenliste erstellen
					List<GenieLibrary.DataElements.UnitHeader.UnitCommand> newUnitCommands = new List<GenieLibrary.DataElements.UnitHeader.UnitCommand>();
					foreach(UnitAbility ab in unit.Key.Abilities)
					{
						// Ungültige Typen überspringen
						if(ab.Type == UnitAbility.AbilityType.Undefined)
							continue;

						// Enthaltene Fähigkeiten-Struktur kopieren
						GenieLibrary.DataElements.UnitHeader.UnitCommand cmd = (GenieLibrary.DataElements.UnitHeader.UnitCommand)ab.CommandData.Clone();

						// Referenzen und Typ auflösen
						cmd.Type = (short)ab.Type;
						cmd.UnitID = (short)(ab.Unit != null ? unitIDMap[ab.Unit] : -1);

						// Fähigkeit zur Liste hinzufügen
						newUnitCommands.Add(cmd);
					}

					// Header erstellen
					UnitIDContainer newUnit = new UnitIDContainer()
					{
						BaseTreeElement = unit.Key,
						UnitHeader = new GenieLibrary.DataElements.UnitHeader()
						{
							Exists = 1,
							Commands = newUnitCommands
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
						TrainParentID = -1,
						ShowInEditor = ((unit.Key.Flags & TechTreeElement.ElementFlags.ShowInEditor) == TechTreeElement.ElementFlags.ShowInEditor)
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
				UpdateExportStep(ExportSteps.ApplyAgeUpgrades);
				foreach(var unit in unitIDMap)
				{
					// Gebäude?
					if(unit.Key is TechTreeBuilding currB)
					{
						// Upgrades erstellen
						foreach(var currAU in currB.AgeUpgrades)
						{
							// Effekt in jeweiligem Zeitalter erstellen
							// TODO: Hardcoded
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
				UpdateExportStep(ExportSteps.AssignChildButtons);
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
				UpdateExportStep(ExportSteps.AssignInheritedChildButtons);
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
				UpdateExportStep(ExportSteps.BuildUnitUpgrades);
				List<TechTreeUnit> nonSuccessorUnits = unitIDMap.Keys.ToList();
				int lastSuccUnitUpgradeResearchId = 0;
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

						// Effekte erstellen für jede Entwicklungsstufe der Einheit (z.B. C im Fall A => B => C: Es soll sowohl A => C als auch B => C wirken)
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
							// Keine weiterentwickelnden Technologien
							// -> Einheit für alle Völker verfügbar? => automatisches Zeitalter-Upgrade
							if(_projectFile.CivTrees.Count(c => c.BlockedElements.Contains(succUnit)) == 0)
								techages[datResearches[100 + succUnit.Age].TechageID].Effects.AddRange(effects);
							else
							{
								// Upgrade-Technologie für spezifische Völker anlegen
								while(datResearches.ContainsKey(lastSuccUnitUpgradeResearchId))
									++lastSuccUnitUpgradeResearchId;
								techages[lastSuccUnitUpgradeResearchId] = new GenieLibrary.DataElements.Techage()
								{
									Name = "Upgrade: " + (succUnit.DATUnit.Name1.Length <= 21 ? succUnit.DATUnit.Name1 : succUnit.DATUnit.Name1.Substring(0, 19) + "~\0"),
									Effects = effects
								};
								GenieLibrary.DataElements.Research succUnitUpgradeResearch = new GenieLibrary.DataElements.Research()
								{
									Name = "#AgeUpgrade: " + succUnit.DATUnit.Name1,
									ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
									RequiredTechs = new List<short>(new short[] { (short)(100 + succUnit.Age), -1, -1, -1, -1, -1 }), // TODO: Hardcoded
									RequiredTechCount = 1,
									LanguageDLLName1 = 7000,
									LanguageDLLName2 = 157000,
									LanguageDLLDescription = 8000,
									LanguageDLLHelp = 107000,
									Unknown1 = -1,
									TechageID = (short)lastSuccUnitUpgradeResearchId,
									ResearchLocation = -1,
									Civ = -1
								};
								succUnitUpgradeResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
								succUnitUpgradeResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
								succUnitUpgradeResearch.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
								datResearches[lastSuccUnitUpgradeResearchId] = succUnitUpgradeResearch;

								// Technologie für alle blockierenden Völker sperren
								for(int c = 0; c < _projectFile.CivTrees.Count; ++c)
									if(_projectFile.CivTrees[c].BlockedElements.Contains(succUnit))
										civBlockIDs[c].Add(lastSuccUnitUpgradeResearchId);
							}
						}
						else if(succRes != null)
						{
							// Upgrade-Effekt zu angegebener Technologie hinzufügen
							techages[datResearches[researchIDMap[succRes]].TechageID].Effects.AddRange(effects);
						}
					}
				}

				// Alle Elemente mit leerer Button-ID von ihrer Elterneinheit lösen
				UpdateExportStep(ExportSteps.FreeButtonlessChildren);
				foreach(var unitD in unitData)
					if(unitD.Value.ButtonID == 0)
						unitD.Value.TrainParentID = -1;
				foreach(var res in datResearches)
					if(res.Value.ButtonID == 0)
						res.Value.ResearchLocation = -1;

				// Alle Gebäude suchen, von denen Elemente abhängen, und entsprechende Technologien definieren
				UpdateExportStep(ExportSteps.BuildBuildingDependencies);
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
				UpdateExportStep(ExportSteps.BuildResearchDependencies);
				foreach(var res in researchIDMap)
				{
					// Abhängigkeiten erstellen
					CreateResearchDependencyStructure(res.Key, researchIDMap, buildingDependencyResearchIDs, datResearches);
				}

				// Einheiten-Abhängigkeiten erstellen
				UpdateExportStep(ExportSteps.BuildUnitDependencies);
				foreach(TechTreeUnit unit in nonSuccessorUnits)
				{
					// Abhängigkeiten erstellen (nur für Einheiten, die allen Kulturen zur Verfügung stehen)
					if((unit.Flags & TechTreeElement.ElementFlags.GaiaOnly) == TechTreeElement.ElementFlags.GaiaOnly)
					{
						// Als Gaia-Einheit markieren
						unitData[unitIDMap[unit]].GaiaOnly = true;
					}
					else if(CreateUnitDependencyStructure(unitIDMap[unit], unit, researchIDMap, buildingDependencyResearchIDs, datResearches, techages, civBlockIDs))
					{
						// Die Einheit muss aktiviert werden
						unitData[unitIDMap[unit]].NeedsResearch = true;
					}
				}

				// Leere Technologie für nicht vergebene DAT-IDs erstellen
				UpdateExportStep(ExportSteps.FillEmptyResearchSlots);
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
				UpdateExportStep(ExportSteps.FillEmptyTechEffectSlots);
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
				UpdateExportStep(ExportSteps.CopyUnitHeaders);
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
				UpdateExportStep(ExportSteps.BuildCivilizationData);
				for(int c = 0; c < _projectFile.CivTrees.Count; ++c)
				{
					// Kultur-Konfiguration abrufen
					UpdateExportStep(ExportSteps.BuildCivilizationData, $" ({c + 1}/{_projectFile.CivTrees.Count})");
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

					// Technologie-"Kostenlos"-Flags in Effekte umwandeln
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
							if(resource.Mode > 0)
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
						if(!unitData.ContainsKey(u) || u == unit768.ID || u == unit770.ID || (unitData[u].GaiaOnly && c != 0))
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
						currDATUnit.HideInEditor = (byte)(currUnit.ShowInEditor ? 0 : 1);
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

				// Ggf. neuen TechTree generieren
				UpdateExportStep(ExportSteps.GenerateNewTechTree);
				exportDAT.NewTechTree = _projectFile.ExportNewTechTree;
				if(_projectFile.ExportNewTechTree)
				{
					// Struktur leer anlegen
					exportDAT.TechTreeNew = new GenieLibrary.DataElements.TechTreeNew()
					{
						ParentElements = new List<GenieLibrary.DataElements.TechTreeNew.TechTreeElement>(),
						DesignData = new GenieLibrary.DataElements.TechTreeNew.TechTreeDesign()
					};

					// TechTree-Design einlesen
					exportDAT.TechTreeNew.DesignData.ReadData(new RAMBuffer(_projectFile.TechTreeDesignPath));

					// Liste mit TechTree-Element-Zuordnung behalten
					Dictionary<TechTreeElement, GenieLibrary.DataElements.TechTreeNew.TechTreeElement> newTechTreeElementMap = new Dictionary<TechTreeElement, GenieLibrary.DataElements.TechTreeNew.TechTreeElement>();

					// Liste mit noch nicht Elternelementen zugeordneten Kindern anlegen (Kind => späteres Elternelement)
					Dictionary<GenieLibrary.DataElements.TechTreeNew.TechTreeElement, TechTreeElement> newTechTreeMissingParentsList = new Dictionary<GenieLibrary.DataElements.TechTreeNew.TechTreeElement, TechTreeElement>();

					// Lambda-Ausdruck: Rekursiv durch Baum iterieren und TechTree-Einträge erstellen
					Func<TechTreeElement, TechTreeElement, GenieLibrary.DataElements.TechTreeNew.TechTreeElement> generateSubTechTree = null;
					generateSubTechTree = (currElement, parent) =>
					{
						// Element erstellen
						GenieLibrary.DataElements.TechTreeNew.TechTreeElement ttElement = new GenieLibrary.DataElements.TechTreeNew.TechTreeElement();
						newTechTreeElementMap.Add(currElement, ttElement);

						// Eigenschaften setzen
						ttElement.Age = (byte)currElement.Age;
						ttElement.NodeTypeIndex = currElement.NewTechTreeNodeDesign;
						ttElement.RequiredElements = new List<Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>>();

						// Nach Typen unterscheiden
						if(currElement is TechTreeResearch)
						{
							// Technologie
							ttElement.ElementType = GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Research;
							ttElement.ElementObjectID = (short)researchIDMap[(TechTreeResearch)currElement];

							// Benötigte Elemente setzen
							foreach(var dep in ((TechTreeResearch)currElement).Dependencies)
								ttElement.RequiredElements.Add(new Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>(GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Research, (short)researchIDMap[dep.Key]));
						}
						else if(currElement is TechTreeBuilding)
						{
							// Gebäude
							ttElement.ElementType = GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Building;
							ttElement.ElementObjectID = (short)unitIDMap[(TechTreeBuilding)currElement];

							// Benötigte Elemente setzen
							if(((TechTreeBuilding)currElement).EnablerResearch != null)
								ttElement.RequiredElements.Add(new Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>(GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Research, (short)researchIDMap[((TechTreeBuilding)currElement).EnablerResearch]));
						}
						else
						{
							// Irgendwas anderes
							ttElement.ElementType = GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Creatable;
							ttElement.ElementObjectID = (short)unitIDMap[(TechTreeUnit)currElement];

							// Für lebende Einheiten benötigte Elemente setzen
							if(currElement is TechTreeCreatable)
								if(((TechTreeCreatable)currElement).EnablerResearch != null)
									ttElement.RequiredElements.Add(new Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>(GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Research, (short)researchIDMap[((TechTreeCreatable)currElement).EnablerResearch]));
						}

						// Ggf. Elternelement als Abhängigkeit setzen, falls dieses nicht im Baum vorkommen soll
						if(parent != null && (parent.Flags & TechTreeElement.ElementFlags.ShowInNewTechTree) != TechTreeElement.ElementFlags.ShowInNewTechTree)
							if(parent is TechTreeResearch)
								ttElement.RequiredElements.Add(new Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>(GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Research, (short)researchIDMap[(TechTreeResearch)parent]));
							else if(parent is TechTreeBuilding)
								ttElement.RequiredElements.Add(new Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>(GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Building, (short)unitIDMap[(TechTreeBuilding)parent]));
							else
								ttElement.RequiredElements.Add(new Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>(GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Creatable, (short)unitIDMap[(TechTreeUnit)parent]));

						// Ggf. Nachfolger-Tech des Elternelements als benötigtes Element einfügen
						if(parent != null && parent != currElement.AlternateNewTechTreeParentElement)
							if(parent is IUpgradeable && ((IUpgradeable)parent).SuccessorResearch != null)
								ttElement.RequiredElements.Add(new Tuple<GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType, short>(GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemType.Research, (short)researchIDMap[((IUpgradeable)parent).SuccessorResearch]));

						// Rendermodus setzen
						if((currElement.Flags & TechTreeElement.ElementFlags.HideInNewTechTreeIfDisabled) == TechTreeElement.ElementFlags.HideInNewTechTreeIfDisabled)
							ttElement.RenderMode = GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemRenderMode.HideIfDisabled;
						else
							ttElement.RenderMode = GenieLibrary.DataElements.TechTreeNew.TechTreeElement.ItemRenderMode.Standard;

						// Kulturen, die dieses Element blockieren, auflisten
						ttElement.DisableCivs = new List<byte>();
						for(byte c = 1; c < _projectFile.CivTrees.Count; ++c)
							if(_projectFile.CivTrees[c].BlockedElements.Contains(currElement))
								ttElement.DisableCivs.Add(c);

						// Kindelemente durchlaufen
						ttElement.Children = new List<GenieLibrary.DataElements.TechTreeNew.TechTreeElement>();
						foreach(TechTreeElement currChild in currElement.GetChildren())
						{
							// Ist das Kind überhaupt sichtbar?
							if((currChild.Flags & TechTreeElement.ElementFlags.ShowInNewTechTree) != TechTreeElement.ElementFlags.ShowInNewTechTree)
								continue;

							// Kindelement erstellen und dem richtigen Elternelement unterordnen
							if(currChild.AlternateNewTechTreeParentElement == null)
								ttElement.Children.Add(generateSubTechTree(currChild, currElement));
							else
								newTechTreeMissingParentsList.Add(generateSubTechTree(currChild, currChild.AlternateNewTechTreeParentElement), currChild.AlternateNewTechTreeParentElement);
						}

						// Element zurückgeben
						return ttElement;
					};

					// Mit Elternelementen beginnen
					foreach(TechTreeElement currParent in _projectFile.TechTreeParentElements)
					{
						// Ist das Element sichtbar?
						if((currParent.Flags & TechTreeElement.ElementFlags.ShowInNewTechTree) != TechTreeElement.ElementFlags.ShowInNewTechTree)
							continue;

						// Als Hauptelement setzen oder erstmal zurückstellen
						if(currParent.AlternateNewTechTreeParentElement == null)
							exportDAT.TechTreeNew.ParentElements.Add(generateSubTechTree(currParent, null));
						else
							newTechTreeMissingParentsList.Add(generateSubTechTree(currParent, currParent.AlternateNewTechTreeParentElement), currParent.AlternateNewTechTreeParentElement);
					}

					// Ausstehende Kindelemente den passenden Elternelementen zuweisen
					foreach(var currEntry in newTechTreeMissingParentsList)
						newTechTreeElementMap[currEntry.Value].Children.Add(currEntry.Key);
				}

				// Neue DAT speichern
				UpdateExportStep(ExportSteps.SaveGeneratedDatFile);
				RAMBuffer tmpBuffer = new RAMBuffer();
				exportDAT.WriteData(tmpBuffer);
				try
				{
					GenieLibrary.GenieFile.CompressData(tmpBuffer).Save(_outputDATTextBox.Text);
				}
				catch(IOException ex)
				{
					MessageBox.Show(TechTreeEditor.Strings.ExportDATFile_Message_ErrorSaving + ex.Message, Strings.ExportDATFile_Message_ErrorSaving_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				// KI-Bezeichner exportieren
				UpdateExportStep(ExportSteps.ExportAiNames);
				using(FileStream perFile = File.Open(_outputDATTextBox.Text + ".per", FileMode.Create, FileAccess.Write))
				using(StreamWriter perFileStream = new StreamWriter(perFile))
				{
					// Alle Gebäude schreiben
					perFileStream.WriteLine("; --- BUILDINGS ---");
					foreach(var unitIdMappingElement in unitIDMap.Where(el => el.Key is TechTreeBuilding))
						if(!string.IsNullOrWhiteSpace(unitIdMappingElement.Key.AiName))
						{
							// Kommentar und Definition schreiben
							perFileStream.WriteLine("; " + unitIdMappingElement.Key.Name);
							perFileStream.WriteLine($"(defconst {unitIdMappingElement.Key.AiName} {unitIdMappingElement.Value})");
						}

					// Alle Einheiten schreiben
					perFileStream.WriteLine("");
					perFileStream.WriteLine("; --- UNITS ---");
					foreach(var unitIdMappingElement in unitIDMap.Where(el => el.Key is TechTreeCreatable))
						if(!string.IsNullOrWhiteSpace(unitIdMappingElement.Key.AiName))
						{
							// Kommentar und Definition schreiben
							perFileStream.WriteLine("; " + unitIdMappingElement.Key.Name);
							perFileStream.WriteLine($"(defconst {unitIdMappingElement.Key.AiName} {unitIdMappingElement.Value})");
						}

					// Alle Technologien schreiben
					perFileStream.WriteLine("");
					perFileStream.WriteLine("; --- RESEARCHES ---");
					foreach(var researchIdMappingElement in researchIDMap)
						if(!string.IsNullOrWhiteSpace(researchIdMappingElement.Key.AiName))
						{
							// Kommentar und Definition schreiben
							perFileStream.WriteLine("; " + researchIdMappingElement.Key.Name);
							perFileStream.WriteLine($"(defconst ri-{researchIdMappingElement.Key.AiName} {researchIdMappingElement.Value})");
						}
				}

				// Ggf. ID-Zuordnung exportieren
				UpdateExportStep(ExportSteps.ExportIdMapping);
				if(_exportIdMappingCheckBox.Checked)
				{
					// Berechne Hash über String aller Einheiten-IDs und -Namen, sodass jede Einheit einmal vorkommt, sowie aller Technologie-IDs
					byte[] hash;
					using(MemoryStream hashString = new MemoryStream())
					{
						// Alle Einheiten-IDs und -Namen in Stream schreiben
						HashSet<int> unitIds = new HashSet<int>(unitIDMap.Values);
						foreach(GenieLibrary.DataElements.Civ c in exportDAT.Civs)
						{
							// Alle noch in der Einheitenliste enthaltenen Einheiten suchen
							foreach(var u in c.Units)
								if(unitIds.Contains(u.Key))
								{
									// Namen und ID anhängen
									byte[] uName = Encoding.ASCII.GetBytes(u.Value.Name1.TrimEnd('\0'));
									hashString.Write(uName, 0, uName.Length);
									hashString.Write(BitConverter.GetBytes((short)u.Key), 0, 2);

									// Einheit ist geschrieben
									unitIds.Remove(u.Key);
								}

							// Fertig?
							if(unitIds.Count == 0)
								break;
						}

						// Alle Tech-IDs und -Namen in Stream schreiben
						for(short r = 0; r < exportDAT.Researches.Count; ++r)
						{
							// Namen und ID anhängen
							byte[] rName = Encoding.ASCII.GetBytes(exportDAT.Researches[r].Name.TrimEnd('\0'));
							hashString.Write(rName, 0, rName.Length);
							hashString.Write(BitConverter.GetBytes(r), 0, 2);
						}

						// Hash berechnen
						hashString.Seek(0, SeekOrigin.Begin);
						using(MD5 md5 = MD5.Create())
							hash = md5.ComputeHash(hashString);
					}

					// Puffer erstellen und ID-Zuordnung schreiben
					RAMBuffer mappingBuffer = new RAMBuffer();
					mappingBuffer.Write(hash); // 128 Bit = 16 Bytes
					mappingBuffer.WriteInteger(unitIDMap.Count);
					foreach(var m in unitIDMap)
					{
						mappingBuffer.WriteShort((short)m.Value);
						mappingBuffer.WriteShort((short)m.Key.ID);
					}
					mappingBuffer.WriteInteger(researchIDMap.Count);
					foreach(var m in researchIDMap)
					{
						mappingBuffer.WriteShort((short)m.Value);
						mappingBuffer.WriteShort((short)m.Key.ID);
					}
					mappingBuffer.Save(_outputDATTextBox.Text + ".mapping");
				}

				// Ggf. ID-Zuordnung im Projekt neu erstellen
				UpdateExportStep(ExportSteps.RebuildProjectIdMapping);
				if(_updateProjectIDsCheckBox.Checked)
				{
					// Die exportierte DAT ist Grundlage
					_projectFile.BasicGenieFile.UnitHeaders = exportDAT.UnitHeaders;
					_projectFile.BasicGenieFile.Civs = exportDAT.Civs;
					_projectFile.BasicGenieFile.Researches = exportDAT.Researches;

					// Baum-Elementen neue IDs zuweisen
					foreach(TechTreeElement elem in _projectFile.Where(elem => true))
					{
						// Nach Typ unterscheiden
						if(elem is TechTreeUnit)
							elem.ID = unitIDMap[(TechTreeUnit)elem];
						else if(elem is TechTreeResearch)
							elem.ID = researchIDMap[(TechTreeResearch)elem];

						// ID sperren
						elem.Flags |= TechTreeElement.ElementFlags.LockID;
					}

					// Neuladen des Projekt ist erforderlich
					ProjectReloadNeeded = true;
				}

				// Fertig
				this.Invoke(new Action(() =>
				{
					// Fenster schließen
					this.DialogResult = DialogResult.OK;
					this.Close();
				}));
			});
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

			// Slots leeren
			datResearch.RequiredTechCount = 0;
			datResearch.RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 });

			// Die Dunkle Zeit darf keine Abhängigkeiten haben
			if(research.ID == 100 + _projectFile.AgeCount)
				return;

			// Der erste Slot ist für das Zeitalter
			int slotNum = 0;
			if(research.Age > 0)
				datResearch.RequiredTechs[slotNum++] = (short)(100 + research.Age);
			else
				datResearch.RequiredTechs[slotNum++] = (short)(100 + _projectFile.AgeCount); // Dunkle Zeit
			++datResearch.RequiredTechCount;

			// Gebäude-Abhängigkeiten bekommen einen eigenen Knoten und landen dann im 2. Slot
			int lastID = 0;
			if(research.BuildingDependencies.Count > 0)
			{
				// Element erstellen
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
			TechTreeElement parent = _projectFile.Where(el => el.GetChildren().Contains(research) && (el.ID < 101 || el.ID > 100 + _projectFile.AgeCount)).FirstOrDefault();
			if(parent != null && parent.GetType() == typeof(TechTreeResearch))
			{
				// Abhängigkeit erstellen
				datResearch.RequiredTechs[slotNum++] = (short)researchIDMap[(TechTreeResearch)parent];
				++datResearch.RequiredTechCount;

				// Ggf. Elternelement aus Abhängigkeitenliste löschen
				if(research.Dependencies.ContainsKey((TechTreeResearch)parent))
					research.Dependencies.Remove((TechTreeResearch)parent);
			}

			// Gibt es sonstige Technologie-Abhängigkeiten?
			if(research.Dependencies.Count > 0)
			{
				// Haben Technologie-Abhängigkeiten unterschiedliche Anzahlen?
				bool differentDepCounts = false;
				int tmpDepCount = research.Dependencies.ElementAt(0).Value;
				foreach(var dep in research.Dependencies)
					if(dep.Value != tmpDepCount)
					{
						// Abbrechen
						differentDepCounts = true;
						break;
					}

				// Verschiedene Anzahlen => Es müssen Nodes erstellt werden
				if(differentDepCounts || (datResearch.RequiredTechCount > 0 && tmpDepCount != -1))
				{
					// Sammelnode erstellen
					int mainDepSlotNum = 0;
					GenieLibrary.DataElements.Research depMainNode = new GenieLibrary.DataElements.Research()
					{
						Name = "#RDepMainNode: " + research.DATResearch.Name,
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
					depMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					depMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
					depMainNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

					// Freie ID suchen und Abhängigkeitsknoten erstellen
					while(datResearches.ContainsKey(lastID))
						++lastID;
					datResearches[lastID] = depMainNode;
					datResearch.RequiredTechs[slotNum++] = (short)lastID;
					++datResearch.RequiredTechCount;

					// Unterknoten erstellen, jeweils für Abhängigkeitszahl x mit allen Technologien mit Abh.-Zahl kleiner als x
					for(int x = 1; x <= 6; ++x)
						if(research.Dependencies.Count(dep => dep.Value == x - 1) > 0)
						{
							// Unterknoten anlegen
							GenieLibrary.DataElements.Research depSubNode = new GenieLibrary.DataElements.Research()
							{
								Name = "#RDepNode_" + x + ": " + research.DATResearch.Name,
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
								RequiredTechCount = (short)x // x Techs hiervon reichen
							};
							depSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
							depSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });
							depSubNode.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>() { Type = -1 });

							// Gebäude einfügen
							int depSlotNum = 0;
							foreach(var dep in research.Dependencies)
								if(dep.Value < x)
								{
									// Maximal 6 Slots, das muss reichen
									if(depSlotNum >= 6)
										break;

									// Abhängigkeit erstellen
									depSubNode.RequiredTechs[depSlotNum++] = (short)researchIDMap[dep.Key];
								}

							// Unterknoten speichern
							while(datResearches.ContainsKey(lastID))
								++lastID;
							datResearches[lastID] = depSubNode;
							depMainNode.RequiredTechs[mainDepSlotNum++] = (short)lastID;
						}
				}
				else
				{
					// Sonstige Technologie-Abhängigkeiten direkt in die restlichen Slots packen
					foreach(var dep in research.Dependencies)
					{
						// Slots voll?
						// Muss reichen
						if(slotNum >= 6)
							break;

						// Abhängigkeit erstellen
						datResearch.RequiredTechs[slotNum++] = (short)researchIDMap[dep.Key];
						++datResearch.RequiredTechCount;
					}

					// Anzahl erforderlicher Technologien setzen
					if(tmpDepCount != -1)
						datResearch.RequiredTechCount = (short)(tmpDepCount + 1);
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

			// Dummy-Einheiten überspringen
			if(unit.DATUnit == null)
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
			// Oder ist die Einheit für mindestens ein Volk gesperrt?
			if(availResearch.RequiredTechCount > 0 || blocks.Count > 0)
			{
				// Neue DAT-Technologie speichern
				while(datResearches.ContainsKey(lastID))
					++lastID;
				datResearches[lastID] = availResearch;

				// Nur für eine Kultur, die nicht Gaia ist?
				// Gaia darf hier nicht gesetzt werden, da sonst die Technologie automatisch für alle Völker verfügbar ist!
				if(avails.Count == 1 && avails[0] != 0)
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

				// Nachfolger durchlaufen und schauen, ob nicht blockierte folgen (d.h. z.B. A --> B, A ist blockiert ab 1. Zeitalter, aber B ab 3. Zeitalter verfügbar)
				TechTreeUnit currSuccessor = ((IUpgradeable)unit).Successor;
				TechTreeResearch currSuccRes = ((IUpgradeable)unit).SuccessorResearch;
				while(currSuccessor != null)
				{
					// Upgrade-Technologie-ID abrufen
					// TODO: Hardcoded [evtl. die Upgrade-Tech-ID merken, um Redundanz zu verhindern?]
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
					currSuccRes = ((IUpgradeable)currSuccessor)?.SuccessorResearch;
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
				int filterMode = 0; // Gibt an, ob ein Element-Filter angewandt werden soll; 1 = Einheiten, 2 = Technologien
				switch(eff.Type)
				{
					case TechEffect.EffectType.AttributeSet:
					case TechEffect.EffectType.AttributePM:
					case TechEffect.EffectType.AttributeMult:
						newEff.A = -1;
						if(eff.Element != null)
							newEff.A = (short)unitIDMap[(TechTreeUnit)eff.Element];
						else if(eff.ClassID < 0)
							filterMode = 1;
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
							filterMode = 1;
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
							filterMode = 2;
						newEff.B = eff.ParameterID;
						newEff.C = (short)eff.Mode;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.ResearchCostMult:
						if(eff.Element == null)
							filterMode = 2;
						else
							newEff = CreateMultTechCostEffect((TechTreeResearch)eff.Element, eff);
						break;

					case TechEffect.EffectType.ResearchDisable:
						if(eff.Element != null)
							newEff.D = (float)researchIDMap[(TechTreeResearch)eff.Element];
						else
							filterMode = 2;
						break;

					case TechEffect.EffectType.ResearchTimeSetPM:
						if(eff.Element != null)
							newEff.A = (short)researchIDMap[(TechTreeResearch)eff.Element];
						else
							filterMode = 2;
						newEff.C = (short)eff.Mode;
						newEff.D = eff.Value;
						break;

					case TechEffect.EffectType.ResearchTimeMult:
						if(eff.Element == null)
							filterMode = 2;
						else
							newEff = CreateMultTechTimeEffect((TechTreeResearch)eff.Element, eff);
						break;
				}

				// Filtern?
				if(filterMode == 1)
				{
					// Nach Einheiten filtern und Effekt für jede Einheit kopieren
					TechEffect.ExpressionEvaluator eval = new TechEffect.ExpressionEvaluator(eff.ElementExpression, true);
					_projectFile.Where(el => eval.CheckElement(el, _projectFile)).ForEach(u =>
					   techage.Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
					   {
						   A = (short)unitIDMap[(TechTreeUnit)u],
						   B = newEff.B,
						   C = newEff.C,
						   D = newEff.D,
						   Type = newEff.Type
					   }));
				}
				else if(filterMode == 2)
				{
					// Nach Technologien filtern und Effekt für jede Technologie kopieren
					TechEffect.ExpressionEvaluator eval = new TechEffect.ExpressionEvaluator(eff.ElementExpression, false);
					_projectFile.Where(el => eval.CheckElement(el, _projectFile)).ForEach(r =>
					 {
						 // Nach Effekt-Typen unterscheiden
						 if(eff.Type == TechEffect.EffectType.ResearchCostMult)
							 techage.Effects.Add(CreateMultTechCostEffect((TechTreeResearch)r, eff));
						 else if(eff.Type == TechEffect.EffectType.ResearchTimeMult)
							 techage.Effects.Add(CreateMultTechTimeEffect((TechTreeResearch)r, eff));
						 else
							 techage.Effects.Add(new GenieLibrary.DataElements.Techage.TechageEffect()
							 {
								 A = (eff.Type != TechEffect.EffectType.ResearchDisable ? (short)researchIDMap[(TechTreeResearch)r] : newEff.A),
								 B = newEff.B,
								 C = newEff.C,
								 D = (eff.Type == TechEffect.EffectType.ResearchDisable ? (float)researchIDMap[(TechTreeResearch)r] : newEff.D),
								 Type = newEff.Type
							 });

					 });
				}
				else
					techage.Effects.Add(newEff);
			}

			// Hilfsfunktion zum Erstellen eines Technologie-Effektes, der eine multiplikative Änderung von Technologie-Kosten bewirkt.
			GenieLibrary.DataElements.Techage.TechageEffect CreateMultTechCostEffect(TechTreeResearch research, TechEffect effect)
			=> new GenieLibrary.DataElements.Techage.TechageEffect()
			{
				Type = (byte)TechEffect.EffectType.ResearchCostSetPM,
				A = (short)researchIDMap[research],
				B = effect.ParameterID,
				C = (short)TechEffect.EffectMode.PM_Enable,
				D = (effect.Value - 1) * research.DATResearch.ResourceCosts.Cast<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>?>().FirstOrDefault(rc => rc?.Type == effect.ParameterID)?.Amount ?? 0
			};

			// Hilfsfunktion zum Erstellen eines Technologie-Effektes, der eine multiplikative Änderung der Technologie-Entwicklungszeit bewirkt.
			GenieLibrary.DataElements.Techage.TechageEffect CreateMultTechTimeEffect(TechTreeResearch research, TechEffect effect)
			=> new GenieLibrary.DataElements.Techage.TechageEffect()
			{
				Type = (byte)TechEffect.EffectType.ResearchTimeSetPM,
				A = (short)researchIDMap[research],
				C = (short)TechEffect.EffectMode.PM_Enable,
				D = (effect.Value - 1) * research.DATResearch.ResearchTime
			};
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

		#endregion Hilfsfunktionen

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

			/// <summary>
			/// Gibt an, ob die Einheit im Editor setzbar ist.
			/// </summary>
			public bool ShowInEditor;

			/// <summary>
			/// Gibt an, ob die Einheit ausschließlich Kultur 0 (Gaia) zur Verfügung steht.
			/// </summary>
			public bool GaiaOnly;
		}

		#endregion Strukturen

		#region Hilfselemente für die Fortschrittsanzeige

		/// <summary>
		/// The export step descriptions and status bar offsets.
		/// </summary>
		private readonly ReadOnlyDictionary<ExportSteps, Tuple<int, string>> ExportStepsTexts = new ReadOnlyDictionary<ExportSteps, Tuple<int, string>>(
			 new Dictionary<ExportSteps, Tuple<int, string>>
			 {
				{ ExportSteps.Initialize, new Tuple<int, string>(0, "Initializing") },
				{ ExportSteps.CalculateUnitMapping, new Tuple<int, string>(1, "Calculating unit mapping") },
				{ ExportSteps.CalculateResearchMapping, new Tuple<int, string>(2, "Calculating research mapping") },
				{ ExportSteps.BuildTechEffects, new Tuple<int, string>(3, "Building tech effects") },
				{ ExportSteps.BuildUnitData, new Tuple<int, string>(4, "Building unit data") },
				{ ExportSteps.ApplyAgeUpgrades, new Tuple<int, string>(5, "Applying age upgrades") },
				{ ExportSteps.AssignChildButtons, new Tuple<int, string>(6, "Assigning child buttons") },
				{ ExportSteps.AssignInheritedChildButtons, new Tuple<int, string>(7, "Assigning inherited child buttons") },
				{ ExportSteps.BuildUnitUpgrades, new Tuple<int, string>(8, "Building unit upgrades") },
				{ ExportSteps.FreeButtonlessChildren, new Tuple<int, string>(9, "Freeing button-less children") },
				{ ExportSteps.BuildBuildingDependencies, new Tuple<int, string>(10, "Building building dependencies") },
				{ ExportSteps.BuildResearchDependencies, new Tuple<int, string>(11, "Building research dependencies") },
				{ ExportSteps.BuildUnitDependencies, new Tuple<int, string>(12, "Building unit dependencies") },
				{ ExportSteps.FillEmptyResearchSlots, new Tuple<int, string>(13, "Filling empty research slots") },
				{ ExportSteps.FillEmptyTechEffectSlots, new Tuple<int, string>(14, "Filling empty tech effect slots") },
				{ ExportSteps.CopyUnitHeaders, new Tuple<int, string>(15, "Copying unit headers") },
				{ ExportSteps.BuildCivilizationData, new Tuple<int, string>(16, "Building civilization data") },
				{ ExportSteps.GenerateNewTechTree, new Tuple<int, string>(17, "Generating new tech tree") },
				{ ExportSteps.SaveGeneratedDatFile, new Tuple<int, string>(18, "Saving generated DAT file") },
				{ ExportSteps.ExportAiNames, new Tuple<int, string>(19, "Exporting AI names") },
				{ ExportSteps.ExportIdMapping, new Tuple<int, string>(20, "Exporting ID mapping") },
				{ ExportSteps.RebuildProjectIdMapping, new Tuple<int, string>(21, "Rebuilding project ID mapping") },
			 });

		/// <summary>
		/// Die einzelnen Schritte beim Exportieren.
		/// </summary>
		private enum ExportSteps
		{
			Initialize,
			CalculateUnitMapping,
			CalculateResearchMapping,
			BuildTechEffects,
			BuildUnitData,
			ApplyAgeUpgrades,
			AssignChildButtons,
			AssignInheritedChildButtons,
			BuildUnitUpgrades,
			FreeButtonlessChildren,
			BuildBuildingDependencies,
			BuildResearchDependencies,
			BuildUnitDependencies,
			FillEmptyResearchSlots,
			FillEmptyTechEffectSlots,
			CopyUnitHeaders,
			BuildCivilizationData,
			GenerateNewTechTree,
			SaveGeneratedDatFile,
			ExportAiNames,
			ExportIdMapping,
			RebuildProjectIdMapping
		}

		#endregion
	}
}