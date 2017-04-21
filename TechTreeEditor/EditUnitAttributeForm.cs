using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	public partial class EditUnitAttributeForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die referenzierte Baum-Einheit.
		/// </summary>
		private TechTreeUnit _treeUnit;

		/// <summary>
		/// Der Einheiten-Manager zum korrekten Kopieren der veränderten Attribute.
		/// </summary>
		private GenieUnitManager _unitManager;

		/// <summary>
		/// Die aktuelle Projektdatei.
		/// </summary>
		private TechTreeFile _projectFile;

		/// <summary>
		/// Gibt an, ob das Formular komplett geladen ist.
		/// </summary>
		private bool _formLoaded = false;

		/// <summary>
		/// Gibt an, ob gerade ein Aktualisierungsvorgang läuft (relevant für das Fähigkeitenfenster).
		/// </summary>
		private bool _updating = false;

		/// <summary>
		/// Die gecachten Typen-Bezeichnungen.
		/// </summary>
		private static List<KeyValuePair<UnitAbility.AbilityType, string>> _abilityTypes = null;

		/// <summary>
		/// Die gecachten Einheiten-Klassen.
		/// </summary>
		private static string[] _classes = null;

		/// <summary>
		/// Die gecachten Ressourcen-Typen.
		/// </summary>
		private static string[] _resourceTypes = null;

		/// <summary>
		/// Eine leere Grafik.
		/// </summary>
		private static GenieLibrary.DataElements.Graphic _emptyG = new GenieLibrary.DataElements.Graphic() { ID = -1 };

		/// <summary>
		/// Die Liste aller Grafiken.
		/// </summary>
		private List<GenieLibrary.DataElements.Graphic> _graphicList = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public EditUnitAttributeForm()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Attribut-Formular basierend auf der gegebenen Einheit.
		/// </summary>
		/// <param name="projectFile">Die aktuelle Projektdatei.</param>
		/// <param name="unit">Die referenzierte Baum-Einheit.</param>
		/// <param name="unitManager">Der Einheiten-Manager zum korrekten Kopieren der veränderten Attribute.</param>
		public EditUnitAttributeForm(TechTreeFile projectFile, TechTreeUnit treeUnit, GenieUnitManager unitManager)
			: this()
		{
			// Parameter speichern
			_projectFile = projectFile;
			_treeUnit = treeUnit;
			_unitManager = unitManager;

			// Einheiten-Änderungen sperren, damit die Zuweisungen nicht alle Auto-Kopier-Einheiten überschreiben
			_unitManager.Lock();

			// Ausgewählte Einheit abrufen
			GenieLibrary.DataElements.Civ.Unit unit = unitManager.SelectedUnit;

			// Grafik-Liste erstellen
			_graphicList = _projectFile.BasicGenieFile.Graphics.Values.ToList();
			_graphicList.Add(_emptyG);
			_graphicList.Sort((x, y) => x.ID.CompareTo(y.ID));
			List<object> tmpGrList = _graphicList.Cast<object>().ToList();

			// Standard-Eigenschaften
			{
				// DLL-Werte einfügen
				_dllNameField.ProjectFile = _projectFile;
				_dllNameField.Value = unit.LanguageDLLName;
				_dllDescriptionField.ProjectFile = _projectFile;
				_dllDescriptionField.Value = unit.LanguageDLLCreation;
				_dllHelpField.ProjectFile = _projectFile;
				_dllHelpField.Value = unit.LanguageDLLHelp - 79000;
				_dllHotkeyField.ProjectFile = _projectFile;
				_dllHotkeyField.Value = unit.HotKey;

				// Grafik-Werte einfügen
				_graStanding1Field.ElementList = tmpGrList;
				_graStanding1Field.Value = (unit.StandingGraphic1 < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.StandingGraphic1));
				_graStanding2Field.ElementList = tmpGrList;
				_graStanding2Field.Value = (unit.StandingGraphic2 < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.StandingGraphic2));
				_graFalling1Field.ElementList = tmpGrList;
				_graFalling1Field.Value = (unit.DyingGraphic1 < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.DyingGraphic1));
				_graFalling2Field.ElementList = tmpGrList;
				_graFalling2Field.Value = (unit.DyingGraphic2 < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.DyingGraphic2));

				// Allgemeine Stats einfügen
				_hitpointsField.Value = unit.HitPoints;
				_losField.Value = (decimal)unit.LineOfSight;
				_speedField.Value = (decimal)unit.Speed;

				// Technische Einstellungen setzen
				_deadModeField.Value = unit.DeathMode > 0;
				_airModeField.Value = unit.AirMode > 0;
				_flyModeField.Value = unit.FlyMode > 0;
				_enabledField.Value = unit.Enabled > 0;
				_unselectableField.Value = unit.Unselectable > 0;
				_edibleMeatField.Value = unit.EdibleMeat > 0;

				// Abmessungen setzen
				_sizeXField.Value = (decimal)unit.SizeRadius1;
				_sizeYField.Value = (decimal)unit.SizeRadius2;
				_editorXField.Value = (decimal)unit.EditorRadius1;
				_editorYField.Value = (decimal)unit.EditorRadius2;
				_selectionXField.Value = (decimal)unit.SelectionRadius1;
				_selectionYField.Value = (decimal)unit.SelectionRadius2;

				// Ressourcen-Speicher setzen
				_resourceStorage1Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Paid = unit.ResourceStorages[0].Type >= 0,
					Type = unit.ResourceStorages[0].Type,
					Amount = unit.ResourceStorages[0].Amount
				};
				_resourceStorage1ModeField.Enabled = unit.ResourceStorages[0].Type >= 0;
				_resourceStorage1ModeField.Value = unit.ResourceStorages[0].Paid;
				_resourceStorage2Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Paid = unit.ResourceStorages[1].Type >= 0,
					Type = unit.ResourceStorages[1].Type,
					Amount = unit.ResourceStorages[1].Amount
				};
				_resourceStorage2ModeField.Enabled = unit.ResourceStorages[1].Type >= 0;
				_resourceStorage2ModeField.Value = unit.ResourceStorages[1].Paid;
				_resourceStorage3Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Paid = unit.ResourceStorages[2].Type >= 0,
					Type = unit.ResourceStorages[2].Type,
					Amount = unit.ResourceStorages[2].Amount
				};
				_resourceStorage3ModeField.Enabled = unit.ResourceStorages[2].Type >= 0;
				_resourceStorage3ModeField.Value = unit.ResourceStorages[2].Paid;

				// Klassenliste in ComboBox einfügen
				_classComboBox.Items.AddRange(Strings.ClassNames.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

				// Haupt-Werte setzen
				_nameTextBox.Text = unit.Name1.TrimEnd('\0');
				_classComboBox.SelectedIndex = unit.Class;
				_iconIDField.Value = unit.IconID;

				// Sounds setzen
				_soundSelectionField.Value = unit.SelectionSound;
				_soundFallingField.Value = unit.DyingSound;
				_soundTrain1Field.Value = unit.TrainSound1;
				_soundTrain2Field.Value = unit.TrainSound2;

				// Terrains setzen
				_terrPlacement1Field.Value = unit.PlacementTerrain1;
				_terrPlacement2Field.Value = unit.PlacementTerrain2;
				_terrSide1Field.Value = unit.PlacementBypassTerrain1;
				_terrSide2Field.Value = unit.PlacementBypassTerrain2;
				_terrRestrField.Value = unit.TerrainRestriction;

				// Unbekannte Werte setzen
				_unknown1Field.Value = unit.Unknown1;
				_unknown2Field.Value = unit.Unknown2;
				_unknown3Field.Value = (decimal)unit.Unknown3A;
				_unknown4Field.Value = unit.Unknown6;
				_unknown5Field.Value = unit.Unknown8;
				_unknown6Field.Value = unit.UnknownSelectionMode;

				// Steuerungseinstellungen setzen
				_interactionModeField.Value = unit.InteractionMode;
				_minimapModeField.Value = unit.MinimapMode;
				_placementModeField.Value = unit.PlacementMode;
				_attackModeField.Value = unit.AttackMode;
				_hillModeField.Value = unit.HillMode;
				_behaviourModeField.Value = unit.Attribute;
				_interfaceModeField.Value = unit.CommandAttribute;

				// Sonstige Parameter setzen
				_minimapColorField.Value = unit.MinimapColor;
				_civilizationField.Value = unit.Civilization;
				_fogOfWarField.Value = unit.VisibleInFog;
				_blastTypeField.Value = unit.BlastType;
				_garrisonCapacityField.Value = unit.GarrisonCapacity;
				_resourceCapacityField.Value = unit.ResourceCapacity;
				_hpBar1Field.Value = (decimal)unit.HPBarHeight1;
				_hpBar2Field.Value = (decimal)unit.HPBarHeight2;
				_resourceDecayField.Value = (decimal)unit.ResourceDecay;

				// Auswahl-Einstellungen setzen
				_selMaskField.Value = unit.SelectionMask;
				_selShapeTypeField.Value = unit.SelectionShapeType;
				_selShapeField.Value = unit.SelectionShape;
				_selEffectField.Value = unit.SelectionEffect;
				_selEditorColorField.Value = unit.EditorSelectionColor;

				// Schaden-Grafiken setzen
				foreach(var dmg in unit.DamageGraphics)
				{
					// Neue Zeile erstellen
					DataGridViewRow currRow = new DataGridViewRow();
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = dmg.DamagePercent });
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = dmg.GraphicID });
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = dmg.ApplyMode });
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = dmg.Unknown2 });
					_dmgGraphicsField.Rows.Add(currRow);
				}
			}

			// Elemente je nach Typ aktivieren, sonst jeweilige Steuerelemente füllen
			if(unit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.DeadFish)
			{
				// Elemente aktivieren
				_unknown9EField.Enabled = true;
				_unknown9DField.Enabled = true;
				_unknown9CField.Enabled = true;
				_unknown9BField.Enabled = true;
				_unknown9AField.Enabled = true;
				_unknown8Field.Enabled = true;
				_unknown7Field.Enabled = true;
				_trackUnitDensityField.Enabled = true;
				_rotationSpeedField.Enabled = true;
				_graMoving2Field.Enabled = true;
				_graMoving1Field.Enabled = true;

				// Verschiedene Werte setzen
				_graMoving1Field.ElementList = tmpGrList;
				_graMoving1Field.Value = (unit.DeadFish.WalkingGraphic1 < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.DeadFish.WalkingGraphic1));
				_graMoving2Field.ElementList = tmpGrList;
				_graMoving2Field.Value = (unit.DeadFish.WalkingGraphic2 < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.DeadFish.WalkingGraphic2));
				_rotationSpeedField.Value = (decimal)unit.DeadFish.RotationSpeed;
				_trackUnitDensityField.Value = (decimal)unit.DeadFish.TrackingUnitDensity;

				// Unbekannte Werte setzen
				_unknown7Field.Value = unit.DeadFish.Unknown11;
				_unknown8Field.Value = unit.DeadFish.Unknown16;
				_unknown9AField.Value = unit.DeadFish.Unknown16B[0];
				_unknown9BField.Value = unit.DeadFish.Unknown16B[1];
				_unknown9CField.Value = unit.DeadFish.Unknown16B[2];
				_unknown9DField.Value = unit.DeadFish.Unknown16B[3];
				_unknown9EField.Value = unit.DeadFish.Unknown16B[4];
			}
			if(unit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Bird)
			{
				// Elemente aktivieren
				_soundMoveField.Enabled = true;
				_soundAttackField.Enabled = true;
				_animalModeField.Enabled = true;
				_workRateField.Enabled = true;
				_villagerModeField.Enabled = true;
				_searchRadiusField.Enabled = true;
				_sheepConvField.Enabled = true;

				// Verschiedene Werte setzen
				_sheepConvField.Value = unit.Bird.SheepConversion;
				_searchRadiusField.Value = (decimal)unit.Bird.SearchRadius;
				_villagerModeField.Value = unit.Bird.VillagerMode;
				_animalModeField.Value = unit.Bird.AnimalMode;
				_soundAttackField.Value = unit.Bird.AttackSound;
				_soundMoveField.Value = unit.Bird.MoveSound;
				_workRateField.Value = (decimal)unit.Bird.WorkRate;
			}
			if(unit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Type50)
			{
				// Elemente aktivieren
				_graAttackField.Enabled = true;
				_terrainMultField.Enabled = true;
				_towerModeField.Enabled = true;
				_blastLevelField.Enabled = true;
				_blastRadiusField.Enabled = true;
				_graphicDisplacementZField.Enabled = true;
				_graphicDisplacementYField.Enabled = true;
				_graphicDisplacementXField.Enabled = true;
				_frameDelayField.Enabled = true;
				_accuracyErrorField.Enabled = true;
				_accuracyPercentField.Enabled = true;
				_attackReloadTimeDisplayedField.Enabled = true;
				_attackReloadTimeField.Enabled = true;
				_rangeDisplayedField.Enabled = true;
				_rangeMaxField.Enabled = true;
				_rangeMinField.Enabled = true;
				_defaultArmorField.Enabled = true;
				_armourValuesField.Enabled = true;
				_displayedMeleeArmourField.Enabled = true;
				_attackValuesField.Enabled = true;
				_displayedAttackField.Enabled = true;

				// Rüstungsklassenliste in ComboBox einfügen
				// Diese werden vorher in eine Liste geschrieben, um Indizierung zu ermöglichen, da die DataGridViewCombobox das offenbar nicht unterstützt
				string[] armourClassesArr = Strings.ArmourClasses.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				List<KeyValuePair<ushort, string>> armourClasses = new List<KeyValuePair<ushort, string>>(armourClassesArr.Length);
				for(ushort i = 0; i < armourClassesArr.Length; ++i)
					armourClasses.Add(new KeyValuePair<ushort, string>(i, armourClassesArr[i]));
				_attValClassColumn.DataSource = armourClasses;
				_attValClassColumn.DisplayMember = "Value";
				_attValClassColumn.ValueMember = "Key";
				_armValClassColumn.DataSource = armourClasses;
				_armValClassColumn.DisplayMember = "Value";
				_armValClassColumn.ValueMember = "Key";

				// Angriffswerte setzen
				_displayedAttackField.Value = unit.Type50.DisplayedAttack;
				foreach(var attval in unit.Type50.Attacks)
				{
					// Neue Zeile erstellen
					DataGridViewRow currRow = new DataGridViewRow();
					currRow.Cells.Add(new DataGridViewComboBoxCell()
					{
						Value = attval.Key,
						DataSource = armourClasses,
						DisplayMember = "Value",
						ValueMember = "Key"
					});
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = attval.Value });
					_attackValuesField.Rows.Add(currRow);
				}

				// Rüstungswerte setzen
				_displayedMeleeArmourField.Value = unit.Type50.DisplayedMeleeArmor;
				_defaultArmorField.Value = unit.Type50.DefaultArmor;
				foreach(var armval in unit.Type50.Armors)
				{
					// Neue Zeile erstellen
					DataGridViewRow currRow = new DataGridViewRow();
					currRow.Cells.Add(new DataGridViewComboBoxCell()
					{
						Value = armval.Key,
						DataSource = armourClasses,
						DisplayMember = "Value",
						ValueMember = "Key"
					});
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = armval.Value });
					_armourValuesField.Rows.Add(currRow);
				}

				// Reichweite setzen
				_rangeMinField.Value = (decimal)unit.Type50.MinRange;
				_rangeMaxField.Value = (decimal)unit.Type50.MaxRange;
				_rangeDisplayedField.Value = (decimal)unit.Type50.DisplayedRange;

				// Nachladezeit setzen
				_attackReloadTimeField.Value = (decimal)unit.Type50.ReloadTime;
				_attackReloadTimeDisplayedField.Value = (decimal)unit.Type50.DisplayedReloadTime;

				// Projektilparameter setzen
				_accuracyPercentField.Value = unit.Type50.ProjectileAccuracyPercent;
				_accuracyErrorField.Value = (decimal)unit.Type50.ProjectileDispersion;
				_frameDelayField.Value = unit.Type50.ProjectileFrameDelay;
				_graphicDisplacementXField.Value = (decimal)unit.Type50.ProjectileGraphicDisplacement[0];
				_graphicDisplacementYField.Value = (decimal)unit.Type50.ProjectileGraphicDisplacement[1];
				_graphicDisplacementZField.Value = (decimal)unit.Type50.ProjectileGraphicDisplacement[2];
				
				// Verschiedene Werte setzen
				_blastRadiusField.Value = (decimal)unit.Type50.BlastRadius;
				_blastLevelField.Value = unit.Type50.BlastLevel;
				_towerModeField.Value = unit.Type50.TowerMode;
				_terrainMultField.Value = unit.Type50.TerrainRestrictionForDamageMultiplying;
				_graAttackField.ElementList = tmpGrList;
				_graAttackField.Value = (unit.Type50.AttackGraphic < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.Type50.AttackGraphic));
			}
			if(unit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.Projectile)
			{
				// Elemente aktivieren
				_projectileArcField.Enabled = true;
				_unknown10Field.Enabled = true;
				_penetrationModeField.Enabled = true;
				_dropAnimationModeField.Enabled = true;
				_compensationModeField.Enabled = true;
				_stretchModeField.Enabled = true;

				// Verschiedene Werte setzen
				_stretchModeField.Value = unit.Projectile.StretchMode;
				_compensationModeField.Value = unit.Projectile.CompensationMode;
				_dropAnimationModeField.Value = unit.Projectile.DropAnimationMode;
				_penetrationModeField.Value = unit.Projectile.PenetrationMode;
				_unknown10Field.Value = unit.Projectile.Unknown24;
				_projectileArcField.Value = (decimal)unit.Projectile.ProjectileArc;
			}
			if(unit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable)
			{
				// Elemente aktivieren
				_unknown12Field.Enabled = true;
				_unknown13Field.Enabled = true;
				_unknown11Field.Enabled = true;
				_graChargeField.Enabled = true;
				_graGarrisonField.Enabled = true;
				_displayedPierceArmorField.Enabled = true;
				_chargeModeField.Enabled = true;
				_heroModeField.Enabled = true;
				_trainTimeField.Enabled = true;
				_missileSpawnZField.Enabled = true;
				_missileSpawnYField.Enabled = true;
				_missileSpawnXField.Enabled = true;
				_missileDuplMaxField.Enabled = true;
				_missileDuplMinField.Enabled = true;
				_cost3Field.Enabled = true;
				_cost2Field.Enabled = true;
				_cost1Field.Enabled = true;

				// Ressourcen-Kosten setzen
				_cost1Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Paid = unit.Creatable.ResourceCosts[0].Paid > 0,
					Type = unit.Creatable.ResourceCosts[0].Type,
					Amount = unit.Creatable.ResourceCosts[0].Amount
				};
				_cost2Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Paid = unit.Creatable.ResourceCosts[1].Paid > 0,
					Type = unit.Creatable.ResourceCosts[1].Type,
					Amount = unit.Creatable.ResourceCosts[1].Amount
				};
				_cost3Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Paid = unit.Creatable.ResourceCosts[2].Paid > 0,
					Type = unit.Creatable.ResourceCosts[2].Type,
					Amount = unit.Creatable.ResourceCosts[2].Amount
				};

				// Geschoss-Werte setzen
				_missileDuplMinField.Value = (decimal)unit.Creatable.ProjectileCount;
				_missileDuplMaxField.Value = unit.Creatable.ProjectileCountOnFullGarrison;
				_missileSpawnXField.Value = (decimal)unit.Creatable.ProjectileSpawningAreaWidth;
				_missileSpawnYField.Value = (decimal)unit.Creatable.ProjectileSpawningAreaHeight;
				_missileSpawnZField.Value = (decimal)unit.Creatable.ProjectileSpawningAreaRandomness;

				// Verschiedene Werte setzen
				_trainTimeField.Value = unit.Creatable.TrainTime;
				_heroModeField.Value = unit.Creatable.HeroMode;
				_chargeModeField.Value = unit.Creatable.ChargingMode;
				_displayedPierceArmorField.Value = unit.Creatable.DisplayedPierceArmor;

				// Grafiken setzen
				_graGarrisonField.ElementList = tmpGrList;
				_graGarrisonField.Value = (unit.Creatable.GarrisonGraphic < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.Creatable.GarrisonGraphic));
				_graChargeField.ElementList = tmpGrList;
				_graChargeField.Value = (unit.Creatable.ChargingGraphic < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.Creatable.ChargingGraphic));

				// Unbekannte Werte setzen
				_unknown11Field.Value = unit.Creatable.Unknown26;
				_unknown12Field.Value = unit.Creatable.Unknown27;
				_unknown13Field.Value = unit.Creatable.Unknown28;
			}
			if(unit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.Building)
			{
				// Elemente aktivieren
				_lootStoneField.Enabled = true;
				_lootGoldField.Enabled = true;
				_lootFoodField.Enabled = true;
				_lootWoodField.Enabled = true;
				_unknown15Field.Enabled = true;
				_unknown14Field.Enabled = true;
				_disappearsField.Enabled = true;
				_garrisonHealRateField.Enabled = true;
				_garrisonTypeField.Enabled = true;
				_unknownSoundField.Enabled = true;
				_constructionSoundField.Enabled = true;
				_foundTerrainField.Enabled = true;
				_graphicAngleField.Enabled = true;
				_adjacentModeField.Enabled = true;
				_graSnowField.Enabled = true;
				_graConstructionField.Enabled = true;

				// Grafiken setzen
				_graConstructionField.ElementList = tmpGrList;
				_graConstructionField.Value = (unit.Building.ConstructionGraphicID < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.Building.ConstructionGraphicID));
				_graSnowField.ElementList = tmpGrList;
				_graSnowField.Value = (unit.Building.SnowGraphicID < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == unit.Building.SnowGraphicID));

				// Verschiedene Werte setzen
				_adjacentModeField.Value = unit.Building.AdjacentMode > 0;
				_disappearsField.Value = unit.Building.DisappearsWhenBuilt > 0;
				_graphicAngleField.Value = unit.Building.GraphicsAngle;
				_foundTerrainField.Value = unit.Building.FoundationTerrainID;
				_constructionSoundField.Value = unit.Building.ConstructionSound;
				_unknownSoundField.Value = unit.Building.UnknownSound;
				_garrisonTypeField.Value = unit.Building.GarrisonType;
				_garrisonHealRateField.Value = (decimal)unit.Building.GarrisonHealRateFactor;

				// Unbekannte Werte setzen
				_unknown14Field.Value = unit.Building.Unknown33;
				_unknown15Field.Value = (decimal)unit.Building.Unknown35;

				// Ablegbare Ressourcen setzen
				_lootWoodField.Value = unit.Building.LootingTable[1] > 0;
				_lootFoodField.Value = unit.Building.LootingTable[4] > 0;
				_lootGoldField.Value = unit.Building.LootingTable[3] > 0;
				_lootStoneField.Value = unit.Building.LootingTable[0] > 0;
			}

			// Fertigkeiten
			{
				// Ggf. Nachschlag-Liste für Fähigkeitentypen erstellen
				if(_abilityTypes == null)
				{
					// Liste anlegen
					_abilityTypes = new List<KeyValuePair<UnitAbility.AbilityType, string>>();
					string[] abilityList = Strings.AbilityTypes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
					foreach(string abilityEntry in abilityList)
					{
						// Eintrag splitten und in Liste einfügen
						string[] entrySplit = abilityEntry.Split('|');
						_abilityTypes.Add(new KeyValuePair<UnitAbility.AbilityType, string>((UnitAbility.AbilityType)uint.Parse(entrySplit[0]), entrySplit[1]));
					}

					// Liste nach Bezeichnungen sortieren
					_abilityTypes.Sort((a1, a2) => a1.Value.CompareTo(a2.Value));
				}

				// Fähigkeitentypen laden
				_abilityTypeComboBox.ValueMember = "Key";
				_abilityTypeComboBox.DisplayMember = "Value";
				_abilityTypeComboBox.DataSource = _abilityTypes.ToList();

				// Einheitenliste erstellen
				TechTreeCreatable emptyUnit = new TechTreeCreatable() { ID = -1 };
				_abilityUnitComboBox.Items.Add(emptyUnit);
				_abilityUnitComboBox.SuspendLayout();
				_abilityUnitComboBox.DisplayMember = "Name";
				_projectFile.Where(u => u is TechTreeUnit).ForEach(u =>
				{
					_abilityUnitComboBox.Items.Add(u);
				});
				_abilityUnitComboBox.ResumeLayout();

				// Einheitenklassen-Liste erstellen
				if(_classes == null)
					_classes = Strings.ClassNames.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				_abilityClassComboBox.Items.AddRange(_classes);
				_abilityClassComboBox.Items.Insert(0, "");

				// Ressourcenlisten zuweisen
				if(_resourceTypes == null)
					_resourceTypes = Strings.ResourceTypes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				_abilityResourceComboBox.Items.AddRange(_resourceTypes);
				_abilityResourceCarryComboBox.Items.AddRange(_resourceTypes);
				_abilityResourceDropComboBox.Items.AddRange(_resourceTypes);
				_abilityResourceProductivityComboBox.Items.AddRange(_resourceTypes);
				_abilityResourceComboBox.Items.Insert(0, "");
				_abilityResourceCarryComboBox.Items.Insert(0, "");
				_abilityResourceDropComboBox.Items.Insert(0, "");
				_abilityResourceProductivityComboBox.Items.Insert(0, "");

				// Grafiklisten zuweisen
				_abilityToolGraphicField.ElementList = tmpGrList;
				_abilityProceedGraphicField.ElementList = tmpGrList;
				_abilityActionGraphicField.ElementList = tmpGrList;
				_abilityCarryGraphicField.ElementList = tmpGrList;

				// Fähigkeitenliste füllen
				UpdateAbilityList();
			}

			// Fertig
			Application.DoEvents();
			_unitManager.Unlock();
			_formLoaded = true;
		}

		/// <summary>
		/// Aktualisiert die Fähigkeiten-Liste.
		/// </summary>
		private void UpdateAbilityList()
		{
			// Aktualisierungsvorgang läuft
			if(_updating)
				return;
			else
				_updating = true;

			// Aktualisieren
			object selection = _abilitiesListBox.SelectedItem;
			_abilitiesListBox.SuspendLayout();
			_abilitiesListBox.DataSource = null;
			_abilitiesListBox.DataSource = _treeUnit.Abilities;
			_abilitiesListBox.ResumeLayout();
			_abilitiesListBox.SelectedItem = selection;

			// Fertig
			_updating = false;
		}

		#endregion Funktionen

		#region Ereignishandler

		#region Fenster

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		private void EditUnitAttributeForm_ResizeBegin(object sender, EventArgs e)
		{
			// Flackern vermeiden
			this.SuspendLayout();
		}

		private void EditUnitAttributeForm_ResizeEnd(object sender, EventArgs e)
		{
			// Nach Ende des Resize-Vorgangs Zeichnen wieder aktivieren
			this.ResumeLayout();
		}

		#endregion Fenster

		#region Tab: Allgemein

		private void _dllNameField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.LanguageDLLName = (ushort)e.NewValue);
			_unitManager.UpdateUnitAttribute(u => u.LanguageDLLHotKeyText = (ushort)e.NewValue + 150000);
		}

		private void _dllDescriptionField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.LanguageDLLCreation = (ushort)e.NewValue);
		}

		private void _dllHelpField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.LanguageDLLHelp = (ushort)e.NewValue + 79000);
		}

		private void _dllHotkeyField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.HotKey = (ushort)e.NewValue);
		}

		private void _hitpointsField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.HitPoints = e.NewValue.SafeConvert<short>());
		}

		private void _losField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.LineOfSight = e.NewValue.SafeConvert<float>());
		}

		private void _speedField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Speed = e.NewValue.SafeConvert<float>());
		}

		private void _deadModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeathMode = (byte)(e.NewValue ? 1 : 0));
		}

		private void _airModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.AirMode = (byte)(e.NewValue ? 1 : 0));
		}

		private void _flyModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.FlyMode = (byte)(e.NewValue ? 1 : 0));
		}

		private void _enabledField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Enabled = (byte)(e.NewValue ? 1 : 0));
		}

		private void _unselectableField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Unselectable = (byte)(e.NewValue ? 1 : 0));
		}

		private void _edibleMeatField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.EdibleMeat = (byte)(e.NewValue ? 1 : 0));
		}

		private void _sizeXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SizeRadius1 = e.NewValue.SafeConvert<float>());
		}

		private void _sizeYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SizeRadius2 = e.NewValue.SafeConvert<float>());
		}

		private void _editorXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.EditorRadius1 = e.NewValue.SafeConvert<float>());
		}

		private void _editorYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.EditorRadius2 = e.NewValue.SafeConvert<float>());
		}

		private void _selectionXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SelectionRadius1 = e.NewValue.SafeConvert<float>());
		}

		private void _selectionYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SelectionRadius2 = e.NewValue.SafeConvert<float>());
		}

		private void _resourceStorage1Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceStorages[0] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Paid = (byte)(e.NewValue.Paid ? _resourceStorage1ModeField.Value.SafeConvert<byte>() : 0),
				Type = (short)(e.NewValue.Paid ? e.NewValue.Type : -1),
				Amount = (e.NewValue.Paid ? e.NewValue.Amount : 0)
			});
			_resourceStorage1ModeField.Enabled = e.NewValue.Paid;
		}

		private void _resourceStorage1ModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceStorages[0] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Paid = _resourceStorage1ModeField.Value.SafeConvert<byte>(),
				Type = (short)_resourceStorage1Field.Value.Type,
				Amount = _resourceStorage1Field.Value.Amount
			});
		}

		private void _resourceStorage2Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceStorages[1] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Paid = (byte)(e.NewValue.Paid ? _resourceStorage2ModeField.Value.SafeConvert<byte>() : 0),
				Type = (short)(e.NewValue.Paid ? e.NewValue.Type : -1),
				Amount = (e.NewValue.Paid ? e.NewValue.Amount : 0)
			});
			_resourceStorage2ModeField.Enabled = e.NewValue.Paid;
		}

		private void _resourceStorage2ModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceStorages[1] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Paid = _resourceStorage2ModeField.Value.SafeConvert<byte>(),
				Type = (short)_resourceStorage2Field.Value.Type,
				Amount = _resourceStorage2Field.Value.Amount
			});
		}

		private void _resourceStorage3Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceStorages[2] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Paid = (byte)(e.NewValue.Paid ? _resourceStorage3ModeField.Value.SafeConvert<byte>() : 0),
				Type = (short)(e.NewValue.Paid ? e.NewValue.Type : -1),
				Amount = (e.NewValue.Paid ? e.NewValue.Amount : 0)
			});
			_resourceStorage3ModeField.Enabled = e.NewValue.Paid;
		}

		private void _resourceStorage3ModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceStorages[2] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Paid = _resourceStorage3ModeField.Value.SafeConvert<byte>(),
				Type = (short)_resourceStorage3Field.Value.Type,
				Amount = _resourceStorage3Field.Value.Amount
			});
		}

		private void _nameTextBox_TextChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Name1 = _nameTextBox.Text + "\0");
		}

		private void _classComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Class = (short)_classComboBox.SelectedIndex);
		}

		private void _iconIDField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.IconID = e.NewValue.SafeConvert<short>());
			OnIconChanged(new IconChangedEventArgs(_treeUnit));
		}

		private void _soundSelectionField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SelectionSound = e.NewValue.SafeConvert<short>());
		}

		private void _soundFallingField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DyingSound = e.NewValue.SafeConvert<short>());
		}

		private void _soundTrain1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.TrainSound1 = e.NewValue.SafeConvert<short>());
		}

		private void _soundTrain2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.TrainSound2 = e.NewValue.SafeConvert<short>());
		}

		private void _terrPlacement1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.PlacementTerrain1 = e.NewValue.SafeConvert<short>());
		}

		private void _terrPlacement2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.PlacementTerrain2 = e.NewValue.SafeConvert<short>());
		}

		private void _terrSide1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.PlacementBypassTerrain1 = e.NewValue.SafeConvert<short>());
		}

		private void _terrSide2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.PlacementBypassTerrain2 = e.NewValue.SafeConvert<short>());
		}

		private void _terrRestrField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.TerrainRestriction = e.NewValue.SafeConvert<short>());
		}

		private void _unknown1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Unknown1 = e.NewValue.SafeConvert<short>());
		}

		private void _unknown2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Unknown2 = e.NewValue.SafeConvert<byte>());
		}

		private void _unknown3Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Unknown3A = e.NewValue.SafeConvert<float>());
		}

		private void _unknown4Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Unknown6 = e.NewValue.SafeConvert<byte>());
		}

		private void _unknown5Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Unknown8 = e.NewValue.SafeConvert<byte>());
		}

		private void _unknown6Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.UnknownSelectionMode = e.NewValue.SafeConvert<byte>());
		}

		private void _interactionModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.InteractionMode = e.NewValue.SafeConvert<byte>());
		}

		private void _minimapModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.MinimapMode = e.NewValue.SafeConvert<byte>());
		}

		private void _placementModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.PlacementMode = e.NewValue.SafeConvert<byte>());
		}

		private void _attackModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.AttackMode = e.NewValue.SafeConvert<byte>());
		}

		private void _hillModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.HillMode = e.NewValue.SafeConvert<byte>());
		}

		private void _behaviourModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Attribute = e.NewValue.SafeConvert<byte>());
		}

		private void _interfaceModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.CommandAttribute = e.NewValue.SafeConvert<byte>());
		}

		private void _minimapColorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.MinimapColor = e.NewValue.SafeConvert<byte>());
		}

		private void _civilizationField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Civilization = e.NewValue.SafeConvert<byte>());
		}

		private void _fogOfWarField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.VisibleInFog = e.NewValue.SafeConvert<byte>());
		}

		private void _blastTypeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.BlastType = e.NewValue.SafeConvert<byte>());
		}

		private void _garrisonCapacityField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.GarrisonCapacity = e.NewValue.SafeConvert<byte>());
		}

		private void _resourceCapacityField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceCapacity = e.NewValue.SafeConvert<short>());
		}

		private void _hpBar1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.HPBarHeight1 = e.NewValue.SafeConvert<float>());
		}

		private void _hpBar2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.HPBarHeight2 = e.NewValue.SafeConvert<float>());
		}

		private void _resourceDecayField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.ResourceDecay = e.NewValue.SafeConvert<float>());
		}

		private void _selMaskField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SelectionMask = e.NewValue.SafeConvert<byte>());
		}

		private void _selShapeTypeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SelectionShapeType = e.NewValue.SafeConvert<byte>());
		}

		private void _selShapeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SelectionShape = e.NewValue.SafeConvert<byte>());
		}

		private void _selEffectField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.SelectionEffect = e.NewValue.SafeConvert<byte>());
		}

		private void _selEditorColorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.EditorSelectionColor = e.NewValue.SafeConvert<byte>());
		}

		private void _dmgGraphicsField_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// Formular geladen?
			if(!_formLoaded)
				return;

			// Byte-Spalten überprüfen
			if(e.ColumnIndex == 0 || e.ColumnIndex > 1)
			{
				// Nur Zahlen erlauben
				byte val;
				if(e.RowIndex != _dmgGraphicsField.NewRowIndex && !byte.TryParse((string)e.FormattedValue, out val))
				{
					// Fehlermeldung zeigen
					MessageBox.Show(Strings.EditUnitAttributeForm_Message_Byte, Strings.EditUnitAttributeForm_Message_Byte_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					// Bearbeiten erzwingen
					e.Cancel = true;
				}
			}

			// ID-Spalte überprüfen
			if(e.ColumnIndex == 1)
			{
				// Nur Zahlen erlauben
				short val;
				if(e.RowIndex != _dmgGraphicsField.NewRowIndex && (!short.TryParse((string)e.FormattedValue, out val) || val < 0))
				{
					// Fehlermeldung zeigen
					MessageBox.Show(Strings.EditUnitAttributeForm_Message_PositiveShort, Strings.EditUnitAttributeForm_Message_PositiveShort_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					// Bearbeiten erzwingen
					e.Cancel = true;
				}
				return;
			}
		}

		private void _dmgGraphicsField_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			// Formular geladen?
			if(!_formLoaded)
				return;

			// Alle Werte aus View in Objekt schreiben
			_unitManager.UpdateUnitAttribute(u => u.DamageGraphics = new List<GenieLibrary.DataElements.Civ.Unit.DamageGraphic>());
			foreach(DataGridViewRow currRow in _dmgGraphicsField.Rows)
			{
				// Neue Reihe überspringen
				if(currRow.IsNewRow || currRow.Cells[0].Value == null || currRow.Cells[1].Value == null || currRow.Cells[2].Value == null || currRow.Cells[3].Value == null)
					continue;

				// Daten einfügen
				GenieLibrary.DataElements.Civ.Unit.DamageGraphic dmg = new GenieLibrary.DataElements.Civ.Unit.DamageGraphic()
				{
					DamagePercent = (currRow.Cells[0].Value.GetType() == typeof(byte) ? (byte)currRow.Cells[0].Value : byte.Parse((string)currRow.Cells[0].Value)),
					GraphicID = (currRow.Cells[1].Value.GetType() == typeof(short) ? (short)currRow.Cells[1].Value : short.Parse((string)currRow.Cells[1].Value)),
					ApplyMode = (currRow.Cells[2].Value.GetType() == typeof(byte) ? (byte)currRow.Cells[2].Value : byte.Parse((string)currRow.Cells[2].Value)),
					Unknown2 = (currRow.Cells[3].Value.GetType() == typeof(byte) ? (byte)currRow.Cells[3].Value : byte.Parse((string)currRow.Cells[3].Value))
				};
				_unitManager.UpdateUnitAttribute(u => u.DamageGraphics.Add(dmg));
			}
		}

		private void _dmgGraphicsField_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			// Genauso behandelt wie die Zellwert-Änderung
			_dmgGraphicsField_CellValueChanged(sender, null);
		}

		private void _graStanding1Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.StandingGraphic1 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _graStanding2Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.StandingGraphic2 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _graFalling1Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DyingGraphic1 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _graFalling2Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DyingGraphic2 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		#endregion Tab: Allgemein

		#region Tab: Tote Einheiten/Fisch

		private void _graMoving1Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.WalkingGraphic1 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _graMoving2Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.WalkingGraphic2 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _rotationSpeedField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.RotationSpeed = e.NewValue.SafeConvert<float>());
		}

		private void _trackUnitDensityField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.TrackingUnitDensity = e.NewValue.SafeConvert<float>());
		}

		private void _unknown7Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.Unknown11 = e.NewValue.SafeConvert<byte>());
		}

		private void _unknown8Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.Unknown16 = e.NewValue.SafeConvert<byte>());
		}

		private void _unknown9AField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.Unknown16B[0] = e.NewValue.SafeConvert<int>());
		}

		private void _unknown9BField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.Unknown16B[1] = e.NewValue.SafeConvert<int>());
		}

		private void _unknown9CField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.Unknown16B[2] = e.NewValue.SafeConvert<int>());
		}

		private void _unknown9DField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.Unknown16B[3] = e.NewValue.SafeConvert<int>());
		}

		private void _unknown9EField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.DeadFish.Unknown16B[4] = e.NewValue.SafeConvert<int>());
		}

		#endregion Tab: Tote Einheiten/Fisch

		#region Tab: Zivilwerte

		private void _sheepConvField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Bird.SheepConversion = e.NewValue.SafeConvert<short>());
		}

		private void _searchRadiusField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Bird.SearchRadius = e.NewValue.SafeConvert<float>());
		}

		private void _villagerModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Bird.VillagerMode = e.NewValue.SafeConvert<byte>());
		}

		private void _animalModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Bird.AnimalMode = e.NewValue.SafeConvert<byte>());
		}

		private void _soundAttackField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Bird.AttackSound = e.NewValue.SafeConvert<short>());
		}

		private void _soundMoveField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Bird.MoveSound = e.NewValue.SafeConvert<short>());
		}

		private void _workRateField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Bird.WorkRate = e.NewValue.SafeConvert<float>());
		}

		#endregion Tab: Zivilwerte

		#region Tab: Angriffswerte

		private void _displayedAttackField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.DisplayedAttack = e.NewValue.SafeConvert<short>());
		}

		private void _attackValuesField_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// Formular geladen?
			if(!_formLoaded)
				return;

			// Wert-Spalte überprüfen
			if(e.ColumnIndex == 1)
			{
				// Nur Zahlen erlauben
				if(e.RowIndex != _attackValuesField.NewRowIndex && (!short.TryParse((string)e.FormattedValue, out short val) || val < 0))
				{
					// Fehlermeldung zeigen
					MessageBox.Show(Strings.EditUnitAttributeForm_Message_PositiveShort, Strings.EditUnitAttributeForm_Message_PositiveShort_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					// Bearbeiten erzwingen
					e.Cancel = true;
				}
				return;
			}
		}

		private void _attackValuesField_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			// Formular geladen?
			if(!_formLoaded)
				return;

			// Alle Werte aus View in Objekt schreiben
			_unitManager.UpdateUnitAttribute(u => u.Type50.Attacks = new Dictionary<ushort, ushort>());
			foreach(DataGridViewRow currRow in _attackValuesField.Rows)
			{
				// Neue Reihe überspringen
				if(currRow.IsNewRow || currRow.Cells[0].Value == null || currRow.Cells[1].Value == null)
					continue;

				// Daten einfügen
				_unitManager.UpdateUnitAttribute(u => u.Type50.Attacks.Add
				(
					(ushort)currRow.Cells[0].Value,
					currRow.Cells[1].Value.GetType() == typeof(ushort) ? (ushort)currRow.Cells[1].Value : ushort.Parse((string)currRow.Cells[1].Value)
				));
			}
		}

		private void _attackValuesField_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			// Genauso behandeln wie die Zellwert-Änderung
			_attackValuesField_CellValueChanged(sender, null);
		}

		private void _displayedMeleeArmourField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.DisplayedMeleeArmor = e.NewValue.SafeConvert<short>());
		}

		private void _defaultArmorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.DefaultArmor = e.NewValue.SafeConvert<short>());
		}

		private void _armourValuesField_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// Formular geladen?
			if(!_formLoaded)
				return;

			// Wert-Spalte überprüfen
			if(e.ColumnIndex == 1)
			{
				// Nur Zahlen erlauben
				short val;
				if(e.RowIndex != _armourValuesField.NewRowIndex && (!short.TryParse((string)e.FormattedValue, out val) || val < 0))
				{
					// Fehlermeldung zeigen
					MessageBox.Show(Strings.EditUnitAttributeForm_Message_PositiveShort, Strings.EditUnitAttributeForm_Message_PositiveShort_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					// Bearbeiten erzwingen
					e.Cancel = true;
				}
				return;
			}
		}

		private void _armourValuesField_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			// Formular geladen?
			if(!_formLoaded)
				return;

			// Alle Werte aus View in Objekt schreiben
			_unitManager.UpdateUnitAttribute(u => u.Type50.Armors = new Dictionary<ushort, ushort>());
			foreach(DataGridViewRow currRow in _armourValuesField.Rows)
			{
				// Neue Reihe überspringen
				if(currRow.IsNewRow || currRow.Cells[0].Value == null || currRow.Cells[1].Value == null)
					continue;

				// Daten einfügen
				_unitManager.UpdateUnitAttribute(u => u.Type50.Armors.Add
				(
					(ushort)currRow.Cells[0].Value,
					currRow.Cells[1].Value.GetType() == typeof(ushort) ? (ushort)currRow.Cells[1].Value : ushort.Parse((string)currRow.Cells[1].Value)
				));
			}
		}

		private void _armourValuesField_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			// Genauso behandeln wie die Zellwert-Änderung
			_armourValuesField_CellValueChanged(sender, null);
		}

		private void _rangeMinField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.MinRange = e.NewValue.SafeConvert<float>());
		}

		private void _rangeMaxField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.MaxRange = e.NewValue.SafeConvert<float>());
		}

		private void _rangeDisplayedField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.DisplayedRange = e.NewValue.SafeConvert<float>());
		}

		private void _attackReloadTimeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.ReloadTime = e.NewValue.SafeConvert<float>());
		}

		private void _attackReloadTimeDisplayedField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.DisplayedReloadTime = e.NewValue.SafeConvert<float>());
		}

		private void _accuracyPercentField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.ProjectileAccuracyPercent = e.NewValue.SafeConvert<short>());
		}

		private void _accuracyErrorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.ProjectileDispersion = e.NewValue.SafeConvert<float>());
		}

		private void _frameDelayField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.ProjectileFrameDelay = e.NewValue.SafeConvert<short>());
		}

		private void _graphicDisplacementXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.ProjectileGraphicDisplacement[0] = e.NewValue.SafeConvert<float>());
		}

		private void _graphicDisplacementYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.ProjectileGraphicDisplacement[1] = e.NewValue.SafeConvert<float>());
		}

		private void _graphicDisplacementZField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.ProjectileGraphicDisplacement[2] = e.NewValue.SafeConvert<float>());
		}

		private void _blastRadiusField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.BlastRadius = e.NewValue.SafeConvert<float>());
		}

		private void _blastLevelField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.BlastLevel = e.NewValue.SafeConvert<byte>());
		}

		private void _towerModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.TowerMode = e.NewValue.SafeConvert<byte>());
		}

		private void _terrainMultField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.TerrainRestrictionForDamageMultiplying = e.NewValue.SafeConvert<short>());
		}

		private void _graAttackField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Type50.AttackGraphic = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		#endregion Tab: Angriffswerte

		#region Tab: Projektile

		private void _stretchModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Projectile.StretchMode = e.NewValue.SafeConvert<byte>());
		}

		private void _compensationModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Projectile.CompensationMode = e.NewValue.SafeConvert<byte>());
		}

		private void _dropAnimationModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Projectile.DropAnimationMode = e.NewValue.SafeConvert<byte>());
		}

		private void _penetrationModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Projectile.PenetrationMode = e.NewValue.SafeConvert<byte>());
		}

		private void _unknown10Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Projectile.Unknown24 = e.NewValue.SafeConvert<byte>());
		}

		private void _projectileArcField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Projectile.ProjectileArc = e.NewValue.SafeConvert<float>());
		}

		#endregion Tab: Projektile

		#region Tab: Erschaffbare Einheiten

		private void _cost1Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ResourceCosts[0] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, short>()
			{
				Paid = (short)(e.NewValue.Paid ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = (short)e.NewValue.Amount
			});
		}

		private void _cost2Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ResourceCosts[1] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, short>()
			{
				Paid = (short)(e.NewValue.Paid ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = (short)e.NewValue.Amount
			});
		}

		private void _cost3Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ResourceCosts[2] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, short>()
			{
				Paid = (short)(e.NewValue.Paid ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = (short)e.NewValue.Amount
			});
		}

		private void _missileDuplMinField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ProjectileCount = e.NewValue.SafeConvert<float>());
		}

		private void _missileDuplMaxField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ProjectileCountOnFullGarrison = e.NewValue.SafeConvert<byte>());
		}

		private void _missileSpawnXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ProjectileSpawningAreaWidth = e.NewValue.SafeConvert<float>());
		}

		private void _missileSpawnYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ProjectileSpawningAreaHeight = e.NewValue.SafeConvert<float>());
		}

		private void _missileSpawnZField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ProjectileSpawningAreaRandomness = e.NewValue.SafeConvert<float>());
		}

		private void _trainTimeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.TrainTime = e.NewValue.SafeConvert<short>());
		}

		private void _heroModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.HeroMode = e.NewValue.SafeConvert<byte>());
		}

		private void _chargeModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ChargingMode = e.NewValue.SafeConvert<byte>());
		}

		private void _displayedPierceArmorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.DisplayedPierceArmor = e.NewValue.SafeConvert<short>());
		}

		private void _graGarrisonField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.GarrisonGraphic = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _graChargeField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.ChargingGraphic = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _unknown11Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.Unknown26 = e.NewValue.SafeConvert<int>());
		}

		private void _unknown12Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.Unknown27 = e.NewValue.SafeConvert<int>());
		}

		private void _unknown13Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Creatable.Unknown28 = e.NewValue.SafeConvert<byte>());
		}

		#endregion Tab: Erschaffbare Einheiten

		#region Tab: Gebäude

		private void _graConstructionField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.ConstructionGraphicID = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _graSnowField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.SnowGraphicID = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID);
		}

		private void _adjacentModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.AdjacentMode = (byte)(e.NewValue ? 1 : 0));
		}

		private void _disappearsField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.DisappearsWhenBuilt = (byte)(e.NewValue ? 1 : 0));
		}

		private void _graphicAngleField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.GraphicsAngle = e.NewValue.SafeConvert<short>());
		}

		private void _foundTerrainField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.FoundationTerrainID = e.NewValue.SafeConvert<short>());
		}

		private void _constructionSoundField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.ConstructionSound = e.NewValue.SafeConvert<short>());
		}

		private void _unknownSoundField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.UnknownSound = e.NewValue.SafeConvert<short>());
		}

		private void _garrisonTypeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.GarrisonType = e.NewValue.SafeConvert<byte>());
		}

		private void _garrisonHealRateField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.GarrisonHealRateFactor = e.NewValue.SafeConvert<float>());
		}

		private void _unknown14Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.Unknown33 = e.NewValue.SafeConvert<byte>());
		}

		private void _unknown15Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.Unknown35 = e.NewValue.SafeConvert<float>());
		}

		private void _lootWoodField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.LootingTable[1] = (byte)(e.NewValue ? 1 : 0));
		}

		private void _lootFoodField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.LootingTable[4] = (byte)(e.NewValue ? 1 : 0));
		}

		private void _lootGoldField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.LootingTable[3] = (byte)(e.NewValue ? 1 : 0));
		}

		private void _lootStoneField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unitManager.UpdateUnitAttribute(u => u.Building.LootingTable[0] = (byte)(e.NewValue ? 1 : 0));
		}

		private void _abilitiesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang läuft
			if(_updating)
				return;
			else
				_updating = true;

			// Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;

			// Element-Informationen anzeigen
			if(sel != null)
			{
				// Steuerelement-Werte setzen
				_abilityTypeComboBox.SelectedValue = sel.Type;
				_abilityUnitComboBox.SelectedItem = sel.Unit;
				_abilityClassComboBox.SelectedIndex = sel.CommandData.ClassID + 1;
				_abilityResourceComboBox.SelectedIndex = sel.CommandData.Resource + 1;
				_abilityResourceCarryComboBox.SelectedIndex = sel.CommandData.ResourceIn + 1;
				_abilityResourceDropComboBox.SelectedIndex = sel.CommandData.ResourceOut + 1;
				_abilityResourceProductivityComboBox.SelectedIndex = sel.CommandData.ResourceProductivityMultiplier + 1;
				_abilityToolGraphicField.Value = (sel.CommandData.Graphics[0] < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == sel.CommandData.Graphics[0]));
				_abilityProceedGraphicField.Value = (sel.CommandData.Graphics[1] < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == sel.CommandData.Graphics[1]));
				_abilityActionGraphicField.Value = (sel.CommandData.Graphics[2] < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == sel.CommandData.Graphics[2]));
				_abilityCarryGraphicField.Value = (sel.CommandData.Graphics[3] < 0 ? _emptyG : _graphicList.FirstOrDefault(g => g.ID == sel.CommandData.Graphics[3]));
				_abilityExecutionSoundField.Value = sel.CommandData.Graphics[4];
				_abilityDropSoundField.Value = sel.CommandData.Graphics[5];
				_abilityWorkRateFactorField.Value = (decimal)sel.CommandData.WorkRateMultiplier;
				_abilityRadiusField.Value = (decimal)sel.CommandData.ExecutionRadius;
				_abilityRangeField.Value = (decimal)sel.CommandData.ExtraRange;
				_abilityTerrainField.Value = sel.CommandData.TerrainID;
				_abilitySelectionEnablerField.Value = sel.CommandData.SelectionEnabler;
				_abilitySelectionModeField.Value = sel.CommandData.SelectionMode;
				_abilityRightClickModeField.Value = sel.CommandData.RightClickMode;
				_abilityPlunderSourceField.Value = sel.CommandData.PlunderSource;
				_abilityUnknown1Field.Value = sel.CommandData.Unknown1;
				_abilityUnknown2Field.Value = sel.CommandData.Unknown4;
				_abilityUnknown3Field.Value = (decimal)sel.CommandData.Unknown5;
				_abilityUnknown4Field.Value = sel.CommandData.Unknown7;
				_abilityUnknown5Field.Value = sel.CommandData.Unknown9;
				_abilityUnknown6Field.Value = sel.CommandData.Unknown12;

				// Steuerelemente aktivieren
				_abilityClassLabel.Enabled = true;
				_abilityTypeLabel.Enabled = true;
				_abilityResourceComboBox.Enabled = true;
				_abilityUnitLabel.Enabled = true;
				_abilityUnitComboBox.Enabled = true;
				_abilityResourceLabel.Enabled = true;
				_abilityInfoLabel.Enabled = true;
				_abilityClassComboBox.Enabled = true;
				_abilityTypeComboBox.Enabled = true;
				_abilityResourceCarryComboBox.Enabled = true;
				_abilityResourceCarryLabel.Enabled = true;
				_abilityResourceDropComboBox.Enabled = true;
				_abilityResourceDropLabel.Enabled = true;
				_abilityResourceProductivityComboBox.Enabled = true;
				_abilityResourceProductivityLabel.Enabled = true;
				_abilityWorkRateFactorField.Enabled = true;
				_abilityRadiusField.Enabled = true;
				_abilityRangeField.Enabled = true;
				_abilitySelectionModeField.Enabled = true;
				_abilitySelectionEnablerField.Enabled = true;
				_abilityPlunderSourceField.Enabled = true;
				_abilityTerrainField.Enabled = true;
				_abilityRightClickModeField.Enabled = true;
				_abilityToolGraphicField.Enabled = true;
				_abilityProceedGraphicField.Enabled = true;
				_abilityActionGraphicField.Enabled = true;
				_abilityCarryGraphicField.Enabled = true;
				_abilityExecutionSoundField.Enabled = true;
				_abilityDropSoundField.Enabled = true;
				_abilityUnknown1Field.Enabled = true;
				_abilityUnknown2Field.Enabled = true;
				_abilityUnknown3Field.Enabled = true;
				_abilityUnknown4Field.Enabled = true;
				_abilityUnknown5Field.Enabled = true;
				_abilityUnknown6Field.Enabled = true;
			}
			else
			{
				// Steuerelemente deaktivieren
				_abilityClassLabel.Enabled = false;
				_abilityTypeLabel.Enabled = false;
				_abilityResourceComboBox.Enabled = false;
				_abilityUnitLabel.Enabled = false;
				_abilityUnitComboBox.Enabled = false;
				_abilityResourceLabel.Enabled = false;
				_abilityInfoLabel.Enabled = false;
				_abilityClassComboBox.Enabled = false;
				_abilityTypeComboBox.Enabled = false;
				_abilityResourceCarryComboBox.Enabled = false;
				_abilityResourceCarryLabel.Enabled = false;
				_abilityResourceDropComboBox.Enabled = false;
				_abilityResourceDropLabel.Enabled = false;
				_abilityResourceProductivityComboBox.Enabled = false;
				_abilityResourceProductivityLabel.Enabled = false;
				_abilityWorkRateFactorField.Enabled = false;
				_abilityRadiusField.Enabled = false;
				_abilityRangeField.Enabled = false;
				_abilitySelectionModeField.Enabled = false;
				_abilitySelectionEnablerField.Enabled = false;
				_abilityPlunderSourceField.Enabled = false;
				_abilityTerrainField.Enabled = false;
				_abilityRightClickModeField.Enabled = false;
				_abilityToolGraphicField.Enabled = false;
				_abilityProceedGraphicField.Enabled = false;
				_abilityActionGraphicField.Enabled = false;
				_abilityCarryGraphicField.Enabled = false;
				_abilityExecutionSoundField.Enabled = false;
				_abilityDropSoundField.Enabled = false;
				_abilityUnknown1Field.Enabled = false;
				_abilityUnknown2Field.Enabled = false;
				_abilityUnknown3Field.Enabled = false;
				_abilityUnknown4Field.Enabled = false;
				_abilityUnknown5Field.Enabled = false;
				_abilityUnknown6Field.Enabled = false;
			}

			// Fertig
			_updating = false;
		}

		private void _abilityTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Typ der ausgewählten Fähigkeit ändern
				sel.Type = (UnitAbility.AbilityType)_abilityTypeComboBox.SelectedValue;

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Einheit speichern
				if(((TechTreeUnit)_abilityUnitComboBox.SelectedItem).ID < 0)
					sel.Unit = null;
				else
					sel.Unit = ((TechTreeUnit)_abilityUnitComboBox.SelectedItem);

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Klasse speichern
				sel.CommandData.ClassID = (short)(_abilityClassComboBox.SelectedIndex - 1);

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityResourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Ressource speichern
				sel.CommandData.Resource = (short)(_abilityResourceComboBox.SelectedIndex - 1);

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityToolGraphicField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Grafik speichern
				if(e.NewValue == _emptyG)
					sel.CommandData.Graphics[0] = -1;
				else
					sel.CommandData.Graphics[0] = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityProceedGraphicField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Grafik speichern
				if(e.NewValue == _emptyG)
					sel.CommandData.Graphics[1] = -1;
				else
					sel.CommandData.Graphics[1] = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityActionGraphicField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Grafik speichern
				if(e.NewValue == _emptyG)
					sel.CommandData.Graphics[2] = -1;
				else
					sel.CommandData.Graphics[2] = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityCarryGraphicField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Grafik speichern
				if(e.NewValue == _emptyG)
					sel.CommandData.Graphics[3] = -1;
				else
					sel.CommandData.Graphics[3] = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityWorkRateFactorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.WorkRateMultiplier = e.NewValue.SafeConvert<float>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityRadiusField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.ExecutionRadius = e.NewValue.SafeConvert<float>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityTerrainField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.TerrainID = e.NewValue.SafeConvert<short>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityRangeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.ExtraRange = e.NewValue.SafeConvert<float>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilitySelectionEnablerField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.SelectionEnabler = e.NewValue.SafeConvert<byte>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilitySelectionModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.SelectionMode = e.NewValue.SafeConvert<byte>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityResourceCarryComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Ressource speichern
				sel.CommandData.ResourceIn = (short)(_abilityResourceCarryComboBox.SelectedIndex - 1);

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityResourceDropComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Ressource speichern
				sel.CommandData.ResourceOut = (short)(_abilityResourceDropComboBox.SelectedIndex - 1);

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityResourceProductivityComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Ressource speichern
				sel.CommandData.ResourceProductivityMultiplier = (short)(_abilityResourceProductivityComboBox.SelectedIndex - 1);

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityRightClickModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.RightClickMode = e.NewValue.SafeConvert<byte>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityPlunderSourceField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.PlunderSource = e.NewValue.SafeConvert<short>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityExecutionSoundField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Graphics[4] = e.NewValue.SafeConvert<short>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityDropSoundField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Graphics[5] = e.NewValue.SafeConvert<short>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityUnknown1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Unknown1 = e.NewValue.SafeConvert<byte>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityUnknown2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Unknown4 = e.NewValue.SafeConvert<byte>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityUnknown3Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Unknown5 = e.NewValue.SafeConvert<float>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityUnknown4Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Unknown7 = e.NewValue.SafeConvert<byte>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityUnknown5Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Unknown9 = e.NewValue.SafeConvert<short>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _abilityUnknown6Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			UnitAbility sel = (UnitAbility)_abilitiesListBox.SelectedItem;
			if(sel != null)
			{
				// Wert speichern
				sel.CommandData.Unknown12 = e.NewValue.SafeConvert<byte>();

				// Aktualisieren
				UpdateAbilityList();
			}

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_abilitiesListBox_SelectedIndexChanged(sender, e);
		}

		private void _newAbilityButton_Click(object sender, EventArgs e)
		{
			// Ist eine Fähigkeit ausgewählt?
			if(_abilitiesListBox.SelectedIndex >= 0)
			{
				// Fähigkeit kopieren
				UnitAbility baseAbility = ((UnitAbility)_abilitiesListBox.SelectedItem).Clone();

				// Fähigkeit in Einheit speichern
				baseAbility.CommandData.ID = (short)_treeUnit.Abilities.Count;
				_treeUnit.Abilities.Add(baseAbility);

				// DAT-Einheitenheader aktualisieren
				_projectFile.BasicGenieFile.UnitHeaders[_treeUnit.ID].Commands.Add(baseAbility.CommandData);
			}
			else
			{
				// Neue leere Fähigkeit erstellt
				UnitAbility emptyAbility = new UnitAbility(new GenieLibrary.DataElements.UnitHeader.UnitCommand()
				{
					ClassID = -1,
					Graphics = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 }),
					One = 1,
					Resource = -1,
					ResourceIn = -1,
					ResourceOut = -1,
					ResourceProductivityMultiplier = -1,
					TerrainID = -1,
					UnitID = -1
				});
				emptyAbility.Type = UnitAbility.AbilityType.Undefined;

				// Fähigkeit in Einheit speichern
				emptyAbility.CommandData.ID = (short)_treeUnit.Abilities.Count;
				_treeUnit.Abilities.Add(emptyAbility);

				// DAT-Einheitenheader aktualisieren
				_projectFile.BasicGenieFile.UnitHeaders[_treeUnit.ID].Commands.Add(emptyAbility.CommandData);
			}

			// Aktualisieren
			UpdateAbilityList();
		}

		private void _deleteAbilityButton_Click(object sender, EventArgs e)
		{
			// Fähigkeit löschen
			if(_abilitiesListBox.SelectedIndex >= 0 && MessageBox.Show(Strings.EditUnitAttributeForm_Message_DeleteAbility, Strings.EditUnitAttributeForm_Message_DeleteAbility_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Fähigkeit löschen
				int abilityID = _abilitiesListBox.SelectedIndex;
				_treeUnit.Abilities.RemoveAt(abilityID);
				_projectFile.BasicGenieFile.UnitHeaders[_treeUnit.ID].Commands.RemoveAt(abilityID);

				// Nachfolgende IDs dekrementieren
				for(int i = abilityID; i < _treeUnit.Abilities.Count; ++i)
					--_treeUnit.Abilities[i].CommandData.ID;

				// Aktualisieren
				UpdateAbilityList();
			}
		}

		#endregion Tab: Gebäude

		#endregion Ereignishandler

		#region Ereignisse

		#region Event: Geändertes Icon

		/// <summary>
		/// Wird ausgelöst, wenn sich das Icon der enthaltenen Einheit ändert.
		/// </summary>
		public event IconChangedEventHandler IconChanged;

		/// <summary>
		/// Der Handler-Typ für das IconChanged-Event.
		/// </summary>
		/// <param name="sender">Das auslösende Objekt.</param>
		/// <param name="e">Die Ereignisdaten.</param>
		public delegate void IconChangedEventHandler(object sender, IconChangedEventArgs e);

		/// <summary>
		/// Löst das IconChanged-Ereignis aus.
		/// </summary>
		/// <param name="e">Die Ereignisdaten.</param>
		protected virtual void OnIconChanged(IconChangedEventArgs e)
		{
			IconChangedEventHandler handler = IconChanged;
			if(handler != null)
				handler(this, e);
		}

		/// <summary>
		/// Die Ereignisdaten für das IconChanged-Event.
		/// </summary>
		public class IconChangedEventArgs : EventArgs
		{
			/// <summary>
			/// Die betroffene Einheit.
			/// </summary>
			private TechTreeUnit _unit;

			/// <summary>
			/// Ruft die betroffene Einheit ab.
			/// </summary>
			public TechTreeUnit Unit
			{
				get
				{
					return _unit;
				}
			}

			/// <summary>
			/// Konstruktor.
			/// Erstellt ein neues Ereignisdaten-Objekt mit den gegebenen Daten.
			/// </summary>
			/// <param name="unit">Die betroffene Einheit.</param>
			public IconChangedEventArgs(TechTreeUnit unit)
			{
				// Parameter speichern
				_unit = unit;
			}
		}

		#endregion Event: Geändertes Icon

		#endregion Ereignisse
	}
}