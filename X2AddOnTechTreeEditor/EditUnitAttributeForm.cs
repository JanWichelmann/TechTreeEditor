using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X2AddOnTechTreeEditor.TechTreeStructure;

/* Es fehlen:

/// <summary>
/// Format: Klasse =&gt; Wert.
/// </summary>
public Dictionary<short, short> Attacks;

/// <summary>
/// Format: Klasse =&gt; Wert.
/// </summary>
public Dictionary<short, short> Armours;

public short TerrainRestrictionForDamageMultiplying;
public float MaxRange;
public float BlastRadius;
public float ReloadTime;
public short AccuracyPercent;
public byte TowerMode;
public short FrameDelay;

/// <summary>
/// Länge: 3.
/// </summary>
public List<float> GraphicDisplacement;

public byte BlastLevel;
public float MinRange;
public float AccuracyErrorRadius;
public short AttackGraphic;
public short DisplayedMeleeArmour;
public short DisplayedAttack;
public float DisplayedRange;
public float DisplayedReloadTime;

*/

namespace X2AddOnTechTreeEditor
{
	public partial class EditUnitAttributeForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die zu bearbeitende Einheit.
		/// </summary>
		TechTreeUnit _unit;

		/// <summary>
		/// Die aktuelle Projektdatei.
		/// </summary>
		TechTreeFile _projectFile;

		/// <summary>
		/// Gibt an, ob das Formular komplett geladen ist.
		/// </summary>
		bool _formLoaded = false;

		#endregion

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
		/// <param name="unit">Die zu bearbeitende Einheit.</param>
		/// <param name="textureFunc">Die Funktion zum Erstellen von Icons.</param>
		public EditUnitAttributeForm(TechTreeFile projectFile, TechTreeUnit unit)
			: this()
		{
			// Parameter speichern
			_unit = unit;
			_projectFile = projectFile;

			// Fenstertitel aktualisieren
			this.Text += _unit.Name;

			// Grafik-Liste erstellen
			GenieLibrary.DataElements.Graphic emptyG = new GenieLibrary.DataElements.Graphic() { ID = -1 };
			List<GenieLibrary.DataElements.Graphic> graphicList = _projectFile.BasicGenieFile.Graphics.Values.ToList();
			graphicList.Add(emptyG);
			graphicList.Sort((x, y) => x.ID.CompareTo(y.ID));
			List<object> tmpGrList = graphicList.Cast<object>().ToList();

			// Einheiten-Liste erstellen
			TechTreeUnit emptyU = new TechTreeDead { ID = -1, Name = "None" };
			List<TechTreeUnit> unitList = _projectFile.Where(p => p is TechTreeUnit).Cast<TechTreeUnit>().ToList();
			unitList.Add(emptyU);
			unitList.Sort((x, y) => x.Name.CompareTo(y.Name));
			List<object> tmpUList = unitList.Cast<object>().ToList();

			// Klassenliste in ComboBox einfügen
			_classComboBox.Items.AddRange(Strings.ClassNames.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

			// Standard-Tab
			{
				// DLL-Werte einfügen
				_dllNameField.ProjectFile = _projectFile;
				_dllNameField.Value = _unit.DATUnit.LanguageDLLName;
				_dllDescriptionField.ProjectFile = _projectFile;
				_dllDescriptionField.Value = _unit.DATUnit.LanguageDLLCreation;
				_dllHelpField.ProjectFile = _projectFile;
				_dllHelpField.Value = _unit.DATUnit.LanguageDLLHelp - 79000;
				_dllHotkeyField.ProjectFile = _projectFile;
				_dllHotkeyField.Value = _unit.DATUnit.HotKey;

				// Grafik-Werte einfügen
				_graStanding1Field.ElementList = tmpGrList;
				_graStanding1Field.Value = (_unit.DATUnit.StandingGraphic1 < 0 ? emptyG : graphicList.FirstOrDefault(g => g.ID == _unit.DATUnit.StandingGraphic1));
				_graStanding2Field.ElementList = tmpGrList;
				_graStanding2Field.Value = (_unit.DATUnit.StandingGraphic2 < 0 ? emptyG : graphicList.FirstOrDefault(g => g.ID == _unit.DATUnit.StandingGraphic2));
				_graFalling1Field.ElementList = tmpGrList;
				_graFalling1Field.Value = (_unit.DATUnit.DyingGraphic1 < 0 ? emptyG : graphicList.FirstOrDefault(g => g.ID == _unit.DATUnit.DyingGraphic1));
				_graFalling2Field.ElementList = tmpGrList;
				_graFalling2Field.Value = (_unit.DATUnit.DyingGraphic2 < 0 ? emptyG : graphicList.FirstOrDefault(g => g.ID == _unit.DATUnit.DyingGraphic2));

				// Allgemeine Stats einfügen
				_hitpointsField.Value = _unit.DATUnit.HitPoints;
				_losField.Value = (decimal)_unit.DATUnit.LineOfSight;
				_speedField.Value = (decimal)_unit.DATUnit.Speed;

				// Technische Einstellungen setzen
				_deadModeField.Value = _unit.DATUnit.DeathMode > 0;
				_airModeField.Value = _unit.DATUnit.AirMode > 0;
				_flyModeField.Value = _unit.DATUnit.FlyMode > 0;
				_enabledField.Value = _unit.DATUnit.Enabled > 0;
				_unselectableField.Value = _unit.DATUnit.Unselectable > 0;
				_edibleMeatField.Value = _unit.DATUnit.EdibleMeat > 0;

				// Abmessungen setzen
				_sizeXField.Value = (decimal)_unit.DATUnit.SizeRadius1;
				_sizeYField.Value = (decimal)_unit.DATUnit.SizeRadius2;
				_editorXField.Value = (decimal)_unit.DATUnit.EditorRadius1;
				_editorYField.Value = (decimal)_unit.DATUnit.EditorRadius2;
				_selectionXField.Value = (decimal)_unit.DATUnit.SelectionRadius1;
				_selectionYField.Value = (decimal)_unit.DATUnit.SelectionRadius2;

				// Ressourcen-Speicher setzen
				_resourceStorage1Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Enabled = _unit.DATUnit.ResourceStorages[0].Enabled > 0,
					Type = _unit.DATUnit.ResourceStorages[0].Type,
					Amount = _unit.DATUnit.ResourceStorages[0].Amount
				};
				_resourceStorage2Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Enabled = _unit.DATUnit.ResourceStorages[1].Enabled > 0,
					Type = _unit.DATUnit.ResourceStorages[1].Type,
					Amount = _unit.DATUnit.ResourceStorages[1].Amount
				};
				_resourceStorage3Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
				{
					Enabled = _unit.DATUnit.ResourceStorages[2].Enabled > 0,
					Type = _unit.DATUnit.ResourceStorages[2].Type,
					Amount = _unit.DATUnit.ResourceStorages[2].Amount
				};

				// Haupt-Werte setzen
				_nameTextBox.Text = _unit.DATUnit.Name1.TrimEnd('\0');
				_classComboBox.SelectedIndex = _unit.DATUnit.Class;
				_deadUnitIDField.ElementList = tmpUList;
				_deadUnitIDField.Value = (_unit.DeadUnit == null ? emptyU : _unit.DeadUnit);
				_iconIDField.Value = _unit.DATUnit.IconID;

				// Sounds setzen
				_soundSelectionField.Value = _unit.DATUnit.SelectionSound;
				_soundFallingField.Value = _unit.DATUnit.DyingSound;
				_soundTrain1Field.Value = _unit.DATUnit.TrainSound1;
				_soundTrain2Field.Value = _unit.DATUnit.TrainSound2;

				// Terrains setzen
				_terrPlacement1Field.Value = _unit.DATUnit.PlacementTerrain1;
				_terrPlacement2Field.Value = _unit.DATUnit.PlacementTerrain2;
				_terrSide1Field.Value = _unit.DATUnit.PlacementBypassTerrain1;
				_terrSide2Field.Value = _unit.DATUnit.PlacementBypassTerrain2;
				_terrRestrField.Value = _unit.DATUnit.TerrainRestriction;

				// Unbekannte Werte setzen
				_unknown1Field.Value = _unit.DATUnit.Unknown1;
				_unknown2Field.Value = _unit.DATUnit.Unknown2;
				_unknown3Field.Value = (decimal)_unit.DATUnit.Unknown3A;
				_unknown4Field.Value = _unit.DATUnit.Unknown6;
				_unknown5Field.Value = _unit.DATUnit.Unknown8;
				_unknown6Field.Value = _unit.DATUnit.UnknownSelectionMode;

				// Steuerungseinstellungen setzen
				_interactionModeField.Value = _unit.DATUnit.InteractionMode;
				_minimapModeField.Value = _unit.DATUnit.MinimapMode;
				_placementModeField.Value = _unit.DATUnit.PlacementMode;
				_attackModeField.Value = _unit.DATUnit.AttackMode;
				_hillModeField.Value = _unit.DATUnit.HillMode;
				_behaviourModeField.Value = _unit.DATUnit.Attribute;
				_interfaceModeField.Value = _unit.DATUnit.CommandAttribute;

				// Sonstige Parameter setzen
				_minimapColorField.Value = _unit.DATUnit.MinimapColor;
				_civilizationField.Value = _unit.DATUnit.Civilization;
				_fogOfWarField.Value = _unit.DATUnit.VisibleInFog;
				_blastTypeField.Value = _unit.DATUnit.BlastType;
				_garrisonCapacityField.Value = _unit.DATUnit.GarrisonCapacity;
				_resourceCapacityField.Value = _unit.DATUnit.ResourceCapacity;
				_hpBar1Field.Value = (decimal)_unit.DATUnit.HPBarHeight1;
				_hpBar2Field.Value = (decimal)_unit.DATUnit.HPBarHeight2;
				_resourceDecayField.Value = (decimal)_unit.DATUnit.ResourceDecay;

				// Auswahl-Einstellungen setzen
				_selMaskField.Value = _unit.DATUnit.SelectionMask;
				_selShapeTypeField.Value = _unit.DATUnit.SelectionShapeType;
				_selShapeField.Value = _unit.DATUnit.SelectionShape;
				_selEffectField.Value = _unit.DATUnit.SelectionEffect;
				_selEditorColorField.Value = _unit.DATUnit.EditorSelectionColor;

				// Schaden-Grafiken setzen
				foreach(var dmg in _unit.DATUnit.DamageGraphics)
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

			// Tab-Seiten je nach Typ sperren, sonst jeweilige Steuerelemente füllen
			if(_deadTabPage.Enabled = (_unit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.DeadFish))
			{
				// Verschiedene Werte setzen
				_graMoving1Field.ElementList = tmpGrList;
				_graMoving1Field.Value = (_unit.DATUnit.DeadFish.WalkingGraphic1 < 0 ? emptyG : graphicList.FirstOrDefault(g => g.ID == _unit.DATUnit.DeadFish.WalkingGraphic1));
				_graMoving2Field.ElementList = tmpGrList;
				_graMoving2Field.Value = (_unit.DATUnit.DeadFish.WalkingGraphic2 < 0 ? emptyG : graphicList.FirstOrDefault(g => g.ID == _unit.DATUnit.DeadFish.WalkingGraphic2));
				_rotationSpeedField.Value = (decimal)_unit.DATUnit.DeadFish.RotationSpeed;
				_trackUnitDensityField.Value = (decimal)_unit.DATUnit.DeadFish.TrackingUnitDensity;

				// Unbekannte Werte setzen
				_unknown7Field.Value = _unit.DATUnit.DeadFish.Unknown11;
				_unknown8Field.Value = _unit.DATUnit.DeadFish.Unknown16;
				_unknown9AField.Value = _unit.DATUnit.DeadFish.Unknown16B[0];
				_unknown9BField.Value = _unit.DATUnit.DeadFish.Unknown16B[1];
				_unknown9CField.Value = _unit.DATUnit.DeadFish.Unknown16B[2];
				_unknown9DField.Value = _unit.DATUnit.DeadFish.Unknown16B[3];
				_unknown9EField.Value = _unit.DATUnit.DeadFish.Unknown16B[4];
			}
			if(_birdTabPage.Enabled = (_unit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Bird))
			{
				// Verschiedene Werte setzen
				_sheepConvField.Value = _unit.DATUnit.Bird.SheepConversion;
				_searchRadiusField.Value = (decimal)_unit.DATUnit.Bird.SearchRadius;
				_villagerModeField.Value = _unit.DATUnit.Bird.VillagerMode;
				_animalModeField.Value = _unit.DATUnit.Bird.AnimalMode;
				_soundAttackField.Value = _unit.DATUnit.Bird.AttackSound;
				_soundMoveField.Value = _unit.DATUnit.Bird.MoveSound;
				_workRateField.Value = (decimal)_unit.DATUnit.Bird.WorkRate;
			}
			if(_type50TabPage.Enabled = (_unit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Type50))
			{

			}
			if(_projectileTabPage.Enabled = (_unit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Projectile))
			{

			}
			if(_creatableTabPage.Enabled = (_unit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable))
			{

			}
			if(_buildingTabPage.Enabled = (_unit.DATUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.Building))
			{

			}

			// Fertig
			_formLoaded = true;
		}

		#endregion

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

		#endregion

		#region Tab: Allgemein

		private void _dllNameField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unit.DATUnit.LanguageDLLName = (ushort)e.NewValue;
			_unit.DATUnit.LanguageDLLHotKeyText = (ushort)e.NewValue + 150000;
		}

		private void _dllDescriptionField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unit.DATUnit.LanguageDLLCreation = (ushort)e.NewValue;
		}

		private void _dllHelpField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unit.DATUnit.LanguageDLLHelp = (ushort)e.NewValue + 79000;
		}

		private void _dllHotkeyField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_unit.DATUnit.HotKey = (ushort)e.NewValue;
		}

		private void _hitpointsField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.HitPoints = e.NewValue.SafeConvert<short>();
		}

		private void _losField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.LineOfSight = e.NewValue.SafeConvert<float>();
		}

		private void _speedField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Speed = e.NewValue.SafeConvert<float>();
		}

		private void _deadModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeathMode = (byte)(e.NewValue ? 1 : 0);
		}

		private void _airModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.AirMode = (byte)(e.NewValue ? 1 : 0);
		}

		private void _flyModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.FlyMode = (byte)(e.NewValue ? 1 : 0);
		}

		private void _enabledField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Enabled = (byte)(e.NewValue ? 1 : 0);
		}

		private void _unselectableField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Unselectable = (byte)(e.NewValue ? 1 : 0);
		}

		private void _edibleMeatField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.EdibleMeat = (byte)(e.NewValue ? 1 : 0);
		}

		private void _sizeXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SizeRadius1 = e.NewValue.SafeConvert<float>();
		}

		private void _sizeYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SizeRadius2 = e.NewValue.SafeConvert<float>();
		}

		private void _editorXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.EditorRadius1 = e.NewValue.SafeConvert<float>();
		}

		private void _editorYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.EditorRadius2 = e.NewValue.SafeConvert<float>();
		}

		private void _selectionXField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SelectionRadius1 = e.NewValue.SafeConvert<float>();
		}

		private void _selectionYField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SelectionRadius2 = e.NewValue.SafeConvert<float>();
		}

		private void _resourceStorage1Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.ResourceStorages[0] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Enabled = (byte)(e.NewValue.Enabled ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = e.NewValue.Amount
			};
		}

		private void _resourceStorage2Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.ResourceStorages[1] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Enabled = (byte)(e.NewValue.Enabled ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = e.NewValue.Amount
			};
		}

		private void _resourceStorage3Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.ResourceStorages[2] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, float, byte>()
			{
				Enabled = (byte)(e.NewValue.Enabled ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = e.NewValue.Amount
			};
		}

		private void _nameTextBox_TextChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Name1 = _nameTextBox.Text + "\0";
		}

		private void _classComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Class = (short)_classComboBox.SelectedIndex;
		}

		private void _iconIDField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.IconID = e.NewValue.SafeConvert<short>();
			OnIconChanged(new IconChangedEventArgs(_unit));
		}

		private void _soundSelectionField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SelectionSound = e.NewValue.SafeConvert<short>();
		}

		private void _soundFallingField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DyingSound = e.NewValue.SafeConvert<short>();
		}

		private void _soundTrain1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.TrainSound1 = e.NewValue.SafeConvert<short>();
		}

		private void _soundTrain2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.TrainSound2 = e.NewValue.SafeConvert<short>();
		}

		private void _terrPlacement1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.PlacementTerrain1 = e.NewValue.SafeConvert<short>();
		}

		private void _terrPlacement2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.PlacementTerrain2 = e.NewValue.SafeConvert<short>();
		}

		private void _terrSide1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.PlacementBypassTerrain1 = e.NewValue.SafeConvert<short>();
		}

		private void _terrSide2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.PlacementBypassTerrain2 = e.NewValue.SafeConvert<short>();
		}

		private void _terrRestrField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.TerrainRestriction = e.NewValue.SafeConvert<short>();
		}

		private void _unknown1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Unknown1 = e.NewValue.SafeConvert<short>();
		}

		private void _unknown2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Unknown2 = e.NewValue.SafeConvert<byte>();
		}

		private void _unknown3Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Unknown3A = e.NewValue.SafeConvert<float>();
		}

		private void _unknown4Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Unknown6 = e.NewValue.SafeConvert<byte>();
		}

		private void _unknown5Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Unknown8 = e.NewValue.SafeConvert<byte>();
		}

		private void _unknown6Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.UnknownSelectionMode = e.NewValue.SafeConvert<byte>();
		}

		private void _interactionModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.InteractionMode = e.NewValue.SafeConvert<byte>();
		}

		private void _minimapModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.MinimapMode = e.NewValue.SafeConvert<byte>();
		}

		private void _placementModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.PlacementMode = e.NewValue.SafeConvert<byte>();
		}

		private void _attackModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.AttackMode = e.NewValue.SafeConvert<byte>();
		}

		private void _hillModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.HillMode = e.NewValue.SafeConvert<byte>();
		}

		private void _behaviourModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Attribute = e.NewValue.SafeConvert<byte>();
		}

		private void _interfaceModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.CommandAttribute = e.NewValue.SafeConvert<byte>();
		}

		private void _minimapColorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.MinimapColor = e.NewValue.SafeConvert<byte>();
		}

		private void _civilizationField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Civilization = e.NewValue.SafeConvert<byte>();
		}

		private void _fogOfWarField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.VisibleInFog = e.NewValue.SafeConvert<byte>();
		}

		private void _blastTypeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.BlastType = e.NewValue.SafeConvert<byte>();
		}

		private void _garrisonCapacityField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.GarrisonCapacity = e.NewValue.SafeConvert<byte>();
		}

		private void _resourceCapacityField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.ResourceCapacity = e.NewValue.SafeConvert<short>();
		}

		private void _hpBar1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.HPBarHeight1 = e.NewValue.SafeConvert<float>();
		}

		private void _hpBar2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.HPBarHeight2 = e.NewValue.SafeConvert<float>();
		}

		private void _resourceDecayField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.ResourceDecay = e.NewValue.SafeConvert<float>();
		}

		private void _selMaskField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SelectionMask = e.NewValue.SafeConvert<byte>();
		}

		private void _selShapeTypeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SelectionShapeType = e.NewValue.SafeConvert<byte>();
		}

		private void _selShapeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SelectionShape = e.NewValue.SafeConvert<byte>();
		}

		private void _selEffectField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.SelectionEffect = e.NewValue.SafeConvert<byte>();
		}

		private void _selEditorColorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.EditorSelectionColor = e.NewValue.SafeConvert<byte>();
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
					MessageBox.Show("Fehler: Ungültiger Zellinhalt. Bitte nur Zahlen zwischen 0 und 255 eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
					MessageBox.Show("Fehler: Ungültiger Zellinhalt. Bitte nur Zahlen zwischen 0 und 32767 eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
			_unit.DATUnit.DamageGraphics = new List<GenieLibrary.DataElements.Civ.Unit.DamageGraphic>();
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
				_unit.DATUnit.DamageGraphics.Add(dmg);
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
			_unit.DATUnit.StandingGraphic1 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;
		}

		private void _graStanding2Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.StandingGraphic2 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;
		}

		private void _graFalling1Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DyingGraphic1 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;
		}

		private void _graFalling2Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DyingGraphic2 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;
		}

		private void _deadUnitIDField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DeadUnit = (TechTreeUnit)e.NewValue;
			if(_unit.DeadUnit.ID < 0)
				_unit.DeadUnit = null;
		}

		#endregion

		#region Tab: Tote Einheiten/Fisch

		private void _graMoving1Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.WalkingGraphic1 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;
		}

		private void _graMoving2Field_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.WalkingGraphic2 = ((GenieLibrary.DataElements.Graphic)e.NewValue).ID;
		}

		private void _rotationSpeedField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.RotationSpeed = e.NewValue.SafeConvert<float>();
		}

		private void _trackUnitDensityField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.TrackingUnitDensity = e.NewValue.SafeConvert<float>();
		}

		private void _unknown7Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.Unknown11 = e.NewValue.SafeConvert<byte>();
		}

		private void _unknown8Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.Unknown16 = e.NewValue.SafeConvert<byte>();
		}

		private void _unknown9AField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.Unknown16B[0] = e.NewValue.SafeConvert<int>();
		}

		private void _unknown9BField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.Unknown16B[1] = e.NewValue.SafeConvert<int>();
		}

		private void _unknown9CField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.Unknown16B[2] = e.NewValue.SafeConvert<int>();
		}

		private void _unknown9DField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.Unknown16B[3] = e.NewValue.SafeConvert<int>();
		}

		private void _unknown9EField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.DeadFish.Unknown16B[4] = e.NewValue.SafeConvert<int>();
		}

		#endregion

		#region Tab: Zivilwerte

		private void _sheepConvField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Bird.SheepConversion = e.NewValue.SafeConvert<short>();
		}

		private void _searchRadiusField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Bird.SearchRadius = e.NewValue.SafeConvert<float>();
		}

		private void _villagerModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Bird.VillagerMode = e.NewValue.SafeConvert<byte>();
		}

		private void _animalModeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Bird.AnimalMode = e.NewValue.SafeConvert<byte>();
		}

		private void _soundAttackField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Bird.AttackSound = e.NewValue.SafeConvert<short>();
		}

		private void _soundMoveField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Bird.MoveSound = e.NewValue.SafeConvert<short>();
		}

		private void _workRateField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_unit.DATUnit.Bird.WorkRate = e.NewValue.SafeConvert<float>();
		}

		#endregion

		#region Tab: Angriffswerte



		#endregion

		#region Tab: Projektile



		#endregion

		#region Tab: Erschaffbare Einheiten



		#endregion

		#region Tab: Gebäude



		#endregion

		#endregion

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

		#endregion

		#endregion
	}
}
