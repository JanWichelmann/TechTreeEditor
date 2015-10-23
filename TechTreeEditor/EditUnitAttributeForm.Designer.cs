namespace TechTreeEditor
{
	partial class EditUnitAttributeForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUnitAttributeForm));
			this._bottomPanel = new System.Windows.Forms.Panel();
			this._closeButton = new System.Windows.Forms.Button();
			this._mainTabControl = new System.Windows.Forms.TabControl();
			this._commonTabPage = new System.Windows.Forms.TabPage();
			this._nameLabel = new System.Windows.Forms.Label();
			this._nameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._dllHotkeyField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllDescriptionField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllHelpField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllNameField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._abilitiesTabPage = new System.Windows.Forms.TabPage();
			this._abilitiesListBox = new TechTreeEditor.DoubleBufferedListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this._abilityUnitComboBox = new System.Windows.Forms.ComboBox();
			this._abilityUnitLabel = new System.Windows.Forms.Label();
			this._abilityUnknown6Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityUnknown5Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityUnknown4Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityUnknown3Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityUnknown2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityUnknown1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityDropSoundField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityExecutionSoundField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityCarryGraphicField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._abilityActionGraphicField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._abilityProceedGraphicField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._abilityToolGraphicField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._abilityRightClickModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityTerrainField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityPlunderSourceField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilitySelectionEnablerField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilitySelectionModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityRangeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityRadiusField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityWorkRateFactorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._abilityResourceProductivityLabel = new System.Windows.Forms.Label();
			this._abilityResourceProductivityComboBox = new System.Windows.Forms.ComboBox();
			this._abilityResourceDropLabel = new System.Windows.Forms.Label();
			this._abilityResourceDropComboBox = new System.Windows.Forms.ComboBox();
			this._abilityResourceCarryLabel = new System.Windows.Forms.Label();
			this._abilityResourceCarryComboBox = new System.Windows.Forms.ComboBox();
			this._newAbilityButton = new System.Windows.Forms.Button();
			this._deleteAbilityButton = new System.Windows.Forms.Button();
			this._abilityTypeComboBox = new System.Windows.Forms.ComboBox();
			this._abilityClassComboBox = new System.Windows.Forms.ComboBox();
			this._abilityInfoLabel = new System.Windows.Forms.Label();
			this._abilityResourceLabel = new System.Windows.Forms.Label();
			this._abilityResourceComboBox = new System.Windows.Forms.ComboBox();
			this._abilityTypeLabel = new System.Windows.Forms.Label();
			this._abilityClassLabel = new System.Windows.Forms.Label();
			this._statsTabPage = new System.Windows.Forms.TabPage();
			this._garrisonHealRateField = new TechTreeEditor.Controls.NumberFieldControl();
			this._workRateField = new TechTreeEditor.Controls.NumberFieldControl();
			this._searchRadiusField = new TechTreeEditor.Controls.NumberFieldControl();
			this._rotationSpeedField = new TechTreeEditor.Controls.NumberFieldControl();
			this._speedField = new TechTreeEditor.Controls.NumberFieldControl();
			this._losField = new TechTreeEditor.Controls.NumberFieldControl();
			this._resourceCapacityField = new TechTreeEditor.Controls.NumberFieldControl();
			this._garrisonCapacityField = new TechTreeEditor.Controls.NumberFieldControl();
			this._combatTabPage = new System.Windows.Forms.TabPage();
			this._blastLevelField = new TechTreeEditor.Controls.NumberFieldControl();
			this._terrainMultField = new TechTreeEditor.Controls.NumberFieldControl();
			this._blastRadiusField = new TechTreeEditor.Controls.NumberFieldControl();
			this._defaultArmorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._attackModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox22 = new System.Windows.Forms.GroupBox();
			this._attackReloadTimeDisplayedField = new TechTreeEditor.Controls.NumberFieldControl();
			this._attackReloadTimeField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox20 = new System.Windows.Forms.GroupBox();
			this._rangeDisplayedField = new TechTreeEditor.Controls.NumberFieldControl();
			this._rangeMaxField = new TechTreeEditor.Controls.NumberFieldControl();
			this._rangeMinField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox19 = new System.Windows.Forms.GroupBox();
			this._armourValuesField = new System.Windows.Forms.DataGridView();
			this._displayedPierceArmorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._displayedMeleeArmourField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox18 = new System.Windows.Forms.GroupBox();
			this._attackValuesField = new System.Windows.Forms.DataGridView();
			this._displayedAttackField = new TechTreeEditor.Controls.NumberFieldControl();
			this._blastTypeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._hitpointsField = new TechTreeEditor.Controls.NumberFieldControl();
			this._projectileTabPage = new System.Windows.Forms.TabPage();
			this._missileDuplMaxField = new TechTreeEditor.Controls.NumberFieldControl();
			this._missileSpawnZField = new TechTreeEditor.Controls.NumberFieldControl();
			this._missileDuplMinField = new TechTreeEditor.Controls.NumberFieldControl();
			this._frameDelayField = new TechTreeEditor.Controls.NumberFieldControl();
			this._missileSpawnYField = new TechTreeEditor.Controls.NumberFieldControl();
			this._graphicDisplacementZField = new TechTreeEditor.Controls.NumberFieldControl();
			this._missileSpawnXField = new TechTreeEditor.Controls.NumberFieldControl();
			this._graphicDisplacementYField = new TechTreeEditor.Controls.NumberFieldControl();
			this._graphicDisplacementXField = new TechTreeEditor.Controls.NumberFieldControl();
			this._projectileArcField = new TechTreeEditor.Controls.NumberFieldControl();
			this._penetrationModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._accuracyErrorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._dropAnimationModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._accuracyPercentField = new TechTreeEditor.Controls.NumberFieldControl();
			this._compensationModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._stretchModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._trainTabPage = new System.Windows.Forms.TabPage();
			this._trainTimeField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox25 = new System.Windows.Forms.GroupBox();
			this._cost3Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost2Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost1Field = new TechTreeEditor.Controls.ResourceCostControl();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this._resourceStorage3ModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._resourceStorage2ModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._resourceStorage1ModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._resourceStorage3Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._resourceStorage2Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._resourceStorage1Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._editorTabPage = new System.Windows.Forms.TabPage();
			this._classLabel = new System.Windows.Forms.Label();
			this._classComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox33 = new System.Windows.Forms.GroupBox();
			this._lootStoneField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._lootGoldField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._lootFoodField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._lootWoodField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this.groupBox13 = new System.Windows.Forms.GroupBox();
			this._selEditorColorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._selShapeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._selShapeTypeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._selEffectField = new TechTreeEditor.Controls.NumberFieldControl();
			this._selMaskField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this._terrRestrField = new TechTreeEditor.Controls.NumberFieldControl();
			this._terrSide2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._terrSide1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._terrPlacement2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._terrPlacement1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._foundTerrainField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this._selectionYField = new TechTreeEditor.Controls.NumberFieldControl();
			this._selectionXField = new TechTreeEditor.Controls.NumberFieldControl();
			this._editorYField = new TechTreeEditor.Controls.NumberFieldControl();
			this._editorXField = new TechTreeEditor.Controls.NumberFieldControl();
			this._sizeYField = new TechTreeEditor.Controls.NumberFieldControl();
			this._sizeXField = new TechTreeEditor.Controls.NumberFieldControl();
			this._garrisonTypeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._chargeModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._heroModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._animalModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._villagerModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._sheepConvField = new TechTreeEditor.Controls.NumberFieldControl();
			this._trackUnitDensityField = new TechTreeEditor.Controls.NumberFieldControl();
			this._civilizationField = new TechTreeEditor.Controls.NumberFieldControl();
			this._hpBar2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._hpBar1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._disappearsField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._graphicAngleField = new TechTreeEditor.Controls.NumberFieldControl();
			this._adjacentModeField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._placementModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._enabledField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._deadModeField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._behaviourTabPage = new System.Windows.Forms.TabPage();
			this._edibleMeatField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._towerModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._unselectableField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._flyModeField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._airModeField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._interfaceModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._behaviourModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._hillModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._minimapModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._interactionModeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._fogOfWarField = new TechTreeEditor.Controls.NumberFieldControl();
			this._minimapColorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._resourceDecayField = new TechTreeEditor.Controls.NumberFieldControl();
			this._graphicsTabPage = new System.Windows.Forms.TabPage();
			this.groupBox14 = new System.Windows.Forms.GroupBox();
			this._dmgGraphicsField = new System.Windows.Forms.DataGridView();
			this._iconIDField = new TechTreeEditor.Controls.NumberFieldControl();
			this._graAttackField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graSnowField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graConstructionField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graChargeField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graGarrisonField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graMoving2Field = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graMoving1Field = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graStanding2Field = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graFalling2Field = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graFalling1Field = new TechTreeEditor.Controls.DropDownFieldControl();
			this._graStanding1Field = new TechTreeEditor.Controls.DropDownFieldControl();
			this._soundsTabPage = new System.Windows.Forms.TabPage();
			this._unknownSoundField = new TechTreeEditor.Controls.NumberFieldControl();
			this._constructionSoundField = new TechTreeEditor.Controls.NumberFieldControl();
			this._soundMoveField = new TechTreeEditor.Controls.NumberFieldControl();
			this._soundAttackField = new TechTreeEditor.Controls.NumberFieldControl();
			this._soundFallingField = new TechTreeEditor.Controls.NumberFieldControl();
			this._soundTrain2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._soundTrain1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._soundSelectionField = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknownTabPage = new System.Windows.Forms.TabPage();
			this._unknown10Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown15Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown14Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown12Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown13Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown11Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown9EField = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown9DField = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown9CField = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown9BField = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown9AField = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown8Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown7Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown6Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown4Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown5Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown3Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._attValClassColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this._attValValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._armValClassColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this._armValValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._dmgGraPercentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._dmgGraIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._dmgGraApplyModeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._dmgGraUnknownColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._bottomPanel.SuspendLayout();
			this._mainTabControl.SuspendLayout();
			this._commonTabPage.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this._abilitiesTabPage.SuspendLayout();
			this.panel1.SuspendLayout();
			this._statsTabPage.SuspendLayout();
			this._combatTabPage.SuspendLayout();
			this.groupBox22.SuspendLayout();
			this.groupBox20.SuspendLayout();
			this.groupBox19.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._armourValuesField)).BeginInit();
			this.groupBox18.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._attackValuesField)).BeginInit();
			this._projectileTabPage.SuspendLayout();
			this._trainTabPage.SuspendLayout();
			this.groupBox25.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this._editorTabPage.SuspendLayout();
			this.groupBox33.SuspendLayout();
			this.groupBox13.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this._behaviourTabPage.SuspendLayout();
			this._graphicsTabPage.SuspendLayout();
			this.groupBox14.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._dmgGraphicsField)).BeginInit();
			this._soundsTabPage.SuspendLayout();
			this._unknownTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			resources.ApplyResources(this._bottomPanel, "_bottomPanel");
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Name = "_bottomPanel";
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _mainTabControl
			// 
			resources.ApplyResources(this._mainTabControl, "_mainTabControl");
			this._mainTabControl.Controls.Add(this._commonTabPage);
			this._mainTabControl.Controls.Add(this._abilitiesTabPage);
			this._mainTabControl.Controls.Add(this._statsTabPage);
			this._mainTabControl.Controls.Add(this._combatTabPage);
			this._mainTabControl.Controls.Add(this._projectileTabPage);
			this._mainTabControl.Controls.Add(this._trainTabPage);
			this._mainTabControl.Controls.Add(this._editorTabPage);
			this._mainTabControl.Controls.Add(this._behaviourTabPage);
			this._mainTabControl.Controls.Add(this._graphicsTabPage);
			this._mainTabControl.Controls.Add(this._soundsTabPage);
			this._mainTabControl.Controls.Add(this._unknownTabPage);
			this._mainTabControl.Name = "_mainTabControl";
			this._mainTabControl.SelectedIndex = 0;
			// 
			// _commonTabPage
			// 
			resources.ApplyResources(this._commonTabPage, "_commonTabPage");
			this._commonTabPage.Controls.Add(this._nameLabel);
			this._commonTabPage.Controls.Add(this._nameTextBox);
			this._commonTabPage.Controls.Add(this.groupBox1);
			this._commonTabPage.Name = "_commonTabPage";
			this._commonTabPage.UseVisualStyleBackColor = true;
			// 
			// _nameLabel
			// 
			resources.ApplyResources(this._nameLabel, "_nameLabel");
			this._nameLabel.Name = "_nameLabel";
			// 
			// _nameTextBox
			// 
			resources.ApplyResources(this._nameTextBox, "_nameTextBox");
			this._nameTextBox.Name = "_nameTextBox";
			this._nameTextBox.TextChanged += new System.EventHandler(this._nameTextBox_TextChanged);
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this._dllHotkeyField);
			this.groupBox1.Controls.Add(this._dllDescriptionField);
			this.groupBox1.Controls.Add(this._dllHelpField);
			this.groupBox1.Controls.Add(this._dllNameField);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// _dllHotkeyField
			// 
			resources.ApplyResources(this._dllHotkeyField, "_dllHotkeyField");
			this._dllHotkeyField.Name = "_dllHotkeyField";
			this._dllHotkeyField.NameString = "Hotkey: ";
			this._dllHotkeyField.ProjectFile = null;
			this._dllHotkeyField.Value = 0;
			this._dllHotkeyField.ValueChanged += new TechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllHotkeyField_ValueChanged);
			// 
			// _dllDescriptionField
			// 
			resources.ApplyResources(this._dllDescriptionField, "_dllDescriptionField");
			this._dllDescriptionField.Name = "_dllDescriptionField";
			this._dllDescriptionField.NameString = "Description:";
			this._dllDescriptionField.ProjectFile = null;
			this._dllDescriptionField.Value = 0;
			this._dllDescriptionField.ValueChanged += new TechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllDescriptionField_ValueChanged);
			// 
			// _dllHelpField
			// 
			resources.ApplyResources(this._dllHelpField, "_dllHelpField");
			this._dllHelpField.Name = "_dllHelpField";
			this._dllHelpField.NameString = "Help:";
			this._dllHelpField.ProjectFile = null;
			this._dllHelpField.Value = 0;
			this._dllHelpField.ValueChanged += new TechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllHelpField_ValueChanged);
			// 
			// _dllNameField
			// 
			resources.ApplyResources(this._dllNameField, "_dllNameField");
			this._dllNameField.Name = "_dllNameField";
			this._dllNameField.NameString = "Name: ";
			this._dllNameField.ProjectFile = null;
			this._dllNameField.Value = 0;
			this._dllNameField.ValueChanged += new TechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllNameField_ValueChanged);
			// 
			// _abilitiesTabPage
			// 
			resources.ApplyResources(this._abilitiesTabPage, "_abilitiesTabPage");
			this._abilitiesTabPage.Controls.Add(this._abilitiesListBox);
			this._abilitiesTabPage.Controls.Add(this.panel1);
			this._abilitiesTabPage.Name = "_abilitiesTabPage";
			this._abilitiesTabPage.UseVisualStyleBackColor = true;
			// 
			// _abilitiesListBox
			// 
			resources.ApplyResources(this._abilitiesListBox, "_abilitiesListBox");
			this._abilitiesListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this._abilitiesListBox.FormattingEnabled = true;
			this._abilitiesListBox.Name = "_abilitiesListBox";
			this._abilitiesListBox.SelectedIndexChanged += new System.EventHandler(this._abilitiesListBox_SelectedIndexChanged);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this._abilityUnitComboBox);
			this.panel1.Controls.Add(this._abilityUnitLabel);
			this.panel1.Controls.Add(this._abilityUnknown6Field);
			this.panel1.Controls.Add(this._abilityUnknown5Field);
			this.panel1.Controls.Add(this._abilityUnknown4Field);
			this.panel1.Controls.Add(this._abilityUnknown3Field);
			this.panel1.Controls.Add(this._abilityUnknown2Field);
			this.panel1.Controls.Add(this._abilityUnknown1Field);
			this.panel1.Controls.Add(this._abilityDropSoundField);
			this.panel1.Controls.Add(this._abilityExecutionSoundField);
			this.panel1.Controls.Add(this._abilityCarryGraphicField);
			this.panel1.Controls.Add(this._abilityActionGraphicField);
			this.panel1.Controls.Add(this._abilityProceedGraphicField);
			this.panel1.Controls.Add(this._abilityToolGraphicField);
			this.panel1.Controls.Add(this._abilityRightClickModeField);
			this.panel1.Controls.Add(this._abilityTerrainField);
			this.panel1.Controls.Add(this._abilityPlunderSourceField);
			this.panel1.Controls.Add(this._abilitySelectionEnablerField);
			this.panel1.Controls.Add(this._abilitySelectionModeField);
			this.panel1.Controls.Add(this._abilityRangeField);
			this.panel1.Controls.Add(this._abilityRadiusField);
			this.panel1.Controls.Add(this._abilityWorkRateFactorField);
			this.panel1.Controls.Add(this._abilityResourceProductivityLabel);
			this.panel1.Controls.Add(this._abilityResourceProductivityComboBox);
			this.panel1.Controls.Add(this._abilityResourceDropLabel);
			this.panel1.Controls.Add(this._abilityResourceDropComboBox);
			this.panel1.Controls.Add(this._abilityResourceCarryLabel);
			this.panel1.Controls.Add(this._abilityResourceCarryComboBox);
			this.panel1.Controls.Add(this._newAbilityButton);
			this.panel1.Controls.Add(this._deleteAbilityButton);
			this.panel1.Controls.Add(this._abilityTypeComboBox);
			this.panel1.Controls.Add(this._abilityClassComboBox);
			this.panel1.Controls.Add(this._abilityInfoLabel);
			this.panel1.Controls.Add(this._abilityResourceLabel);
			this.panel1.Controls.Add(this._abilityResourceComboBox);
			this.panel1.Controls.Add(this._abilityTypeLabel);
			this.panel1.Controls.Add(this._abilityClassLabel);
			this.panel1.Name = "panel1";
			// 
			// _abilityUnitComboBox
			// 
			resources.ApplyResources(this._abilityUnitComboBox, "_abilityUnitComboBox");
			this._abilityUnitComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this._abilityUnitComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this._abilityUnitComboBox.FormattingEnabled = true;
			this._abilityUnitComboBox.Name = "_abilityUnitComboBox";
			this._abilityUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._abilityUnitComboBox_SelectedIndexChanged);
			// 
			// _abilityUnitLabel
			// 
			resources.ApplyResources(this._abilityUnitLabel, "_abilityUnitLabel");
			this._abilityUnitLabel.Name = "_abilityUnitLabel";
			// 
			// _abilityUnknown6Field
			// 
			resources.ApplyResources(this._abilityUnknown6Field, "_abilityUnknown6Field");
			this._abilityUnknown6Field.Name = "_abilityUnknown6Field";
			this._abilityUnknown6Field.NameString = "UUnknown 6:";
			this._abilityUnknown6Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityUnknown6Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityUnknown6Field_ValueChanged);
			// 
			// _abilityUnknown5Field
			// 
			resources.ApplyResources(this._abilityUnknown5Field, "_abilityUnknown5Field");
			this._abilityUnknown5Field.Name = "_abilityUnknown5Field";
			this._abilityUnknown5Field.NameString = "Unknown 5:";
			this._abilityUnknown5Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityUnknown5Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityUnknown5Field_ValueChanged);
			// 
			// _abilityUnknown4Field
			// 
			resources.ApplyResources(this._abilityUnknown4Field, "_abilityUnknown4Field");
			this._abilityUnknown4Field.Name = "_abilityUnknown4Field";
			this._abilityUnknown4Field.NameString = "Unknown 4:";
			this._abilityUnknown4Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityUnknown4Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityUnknown4Field_ValueChanged);
			// 
			// _abilityUnknown3Field
			// 
			resources.ApplyResources(this._abilityUnknown3Field, "_abilityUnknown3Field");
			this._abilityUnknown3Field.Name = "_abilityUnknown3Field";
			this._abilityUnknown3Field.NameString = "Unknown 3:";
			this._abilityUnknown3Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityUnknown3Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityUnknown3Field_ValueChanged);
			// 
			// _abilityUnknown2Field
			// 
			resources.ApplyResources(this._abilityUnknown2Field, "_abilityUnknown2Field");
			this._abilityUnknown2Field.Name = "_abilityUnknown2Field";
			this._abilityUnknown2Field.NameString = "Unknown 2:";
			this._abilityUnknown2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityUnknown2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityUnknown2Field_ValueChanged);
			// 
			// _abilityUnknown1Field
			// 
			resources.ApplyResources(this._abilityUnknown1Field, "_abilityUnknown1Field");
			this._abilityUnknown1Field.Name = "_abilityUnknown1Field";
			this._abilityUnknown1Field.NameString = "Unknown 1:";
			this._abilityUnknown1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityUnknown1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityUnknown1Field_ValueChanged);
			// 
			// _abilityDropSoundField
			// 
			resources.ApplyResources(this._abilityDropSoundField, "_abilityDropSoundField");
			this._abilityDropSoundField.Name = "_abilityDropSoundField";
			this._abilityDropSoundField.NameString = "Drop sound:";
			this._abilityDropSoundField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityDropSoundField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityDropSoundField_ValueChanged);
			// 
			// _abilityExecutionSoundField
			// 
			resources.ApplyResources(this._abilityExecutionSoundField, "_abilityExecutionSoundField");
			this._abilityExecutionSoundField.Name = "_abilityExecutionSoundField";
			this._abilityExecutionSoundField.NameString = "Execution sound:";
			this._abilityExecutionSoundField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityExecutionSoundField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityExecutionSoundField_ValueChanged);
			// 
			// _abilityCarryGraphicField
			// 
			resources.ApplyResources(this._abilityCarryGraphicField, "_abilityCarryGraphicField");
			this._abilityCarryGraphicField.Name = "_abilityCarryGraphicField";
			this._abilityCarryGraphicField.NameString = "Carry graphic:";
			this._abilityCarryGraphicField.Value = null;
			this._abilityCarryGraphicField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._abilityCarryGraphicField_ValueChanged);
			// 
			// _abilityActionGraphicField
			// 
			resources.ApplyResources(this._abilityActionGraphicField, "_abilityActionGraphicField");
			this._abilityActionGraphicField.Name = "_abilityActionGraphicField";
			this._abilityActionGraphicField.NameString = "Action graphic:";
			this._abilityActionGraphicField.Value = null;
			this._abilityActionGraphicField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._abilityActionGraphicField_ValueChanged);
			// 
			// _abilityProceedGraphicField
			// 
			resources.ApplyResources(this._abilityProceedGraphicField, "_abilityProceedGraphicField");
			this._abilityProceedGraphicField.Name = "_abilityProceedGraphicField";
			this._abilityProceedGraphicField.NameString = "Proceed graph.:";
			this._abilityProceedGraphicField.Value = null;
			this._abilityProceedGraphicField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._abilityProceedGraphicField_ValueChanged);
			// 
			// _abilityToolGraphicField
			// 
			resources.ApplyResources(this._abilityToolGraphicField, "_abilityToolGraphicField");
			this._abilityToolGraphicField.Name = "_abilityToolGraphicField";
			this._abilityToolGraphicField.NameString = "Walk graphic:";
			this._abilityToolGraphicField.Value = null;
			this._abilityToolGraphicField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._abilityToolGraphicField_ValueChanged);
			// 
			// _abilityRightClickModeField
			// 
			resources.ApplyResources(this._abilityRightClickModeField, "_abilityRightClickModeField");
			this._abilityRightClickModeField.Name = "_abilityRightClickModeField";
			this._abilityRightClickModeField.NameString = "Right click mode:";
			this._abilityRightClickModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityRightClickModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityRightClickModeField_ValueChanged);
			// 
			// _abilityTerrainField
			// 
			resources.ApplyResources(this._abilityTerrainField, "_abilityTerrainField");
			this._abilityTerrainField.Name = "_abilityTerrainField";
			this._abilityTerrainField.NameString = "Terrain:";
			this._abilityTerrainField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityTerrainField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityTerrainField_ValueChanged);
			// 
			// _abilityPlunderSourceField
			// 
			resources.ApplyResources(this._abilityPlunderSourceField, "_abilityPlunderSourceField");
			this._abilityPlunderSourceField.Name = "_abilityPlunderSourceField";
			this._abilityPlunderSourceField.NameString = "Plunder Mode:";
			this._abilityPlunderSourceField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityPlunderSourceField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityPlunderSourceField_ValueChanged);
			// 
			// _abilitySelectionEnablerField
			// 
			resources.ApplyResources(this._abilitySelectionEnablerField, "_abilitySelectionEnablerField");
			this._abilitySelectionEnablerField.Name = "_abilitySelectionEnablerField";
			this._abilitySelectionEnablerField.NameString = "Selection enabler:";
			this._abilitySelectionEnablerField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilitySelectionEnablerField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilitySelectionEnablerField_ValueChanged);
			// 
			// _abilitySelectionModeField
			// 
			resources.ApplyResources(this._abilitySelectionModeField, "_abilitySelectionModeField");
			this._abilitySelectionModeField.Name = "_abilitySelectionModeField";
			this._abilitySelectionModeField.NameString = "Selection mode:";
			this._abilitySelectionModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilitySelectionModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilitySelectionModeField_ValueChanged);
			// 
			// _abilityRangeField
			// 
			resources.ApplyResources(this._abilityRangeField, "_abilityRangeField");
			this._abilityRangeField.Name = "_abilityRangeField";
			this._abilityRangeField.NameString = "Extra range:";
			this._abilityRangeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityRangeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityRangeField_ValueChanged);
			// 
			// _abilityRadiusField
			// 
			resources.ApplyResources(this._abilityRadiusField, "_abilityRadiusField");
			this._abilityRadiusField.Name = "_abilityRadiusField";
			this._abilityRadiusField.NameString = "Radius: ";
			this._abilityRadiusField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityRadiusField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityRadiusField_ValueChanged);
			// 
			// _abilityWorkRateFactorField
			// 
			resources.ApplyResources(this._abilityWorkRateFactorField, "_abilityWorkRateFactorField");
			this._abilityWorkRateFactorField.Name = "_abilityWorkRateFactorField";
			this._abilityWorkRateFactorField.NameString = "Work rate factor:";
			this._abilityWorkRateFactorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._abilityWorkRateFactorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._abilityWorkRateFactorField_ValueChanged);
			// 
			// _abilityResourceProductivityLabel
			// 
			resources.ApplyResources(this._abilityResourceProductivityLabel, "_abilityResourceProductivityLabel");
			this._abilityResourceProductivityLabel.Name = "_abilityResourceProductivityLabel";
			// 
			// _abilityResourceProductivityComboBox
			// 
			resources.ApplyResources(this._abilityResourceProductivityComboBox, "_abilityResourceProductivityComboBox");
			this._abilityResourceProductivityComboBox.FormattingEnabled = true;
			this._abilityResourceProductivityComboBox.Name = "_abilityResourceProductivityComboBox";
			this._abilityResourceProductivityComboBox.SelectedIndexChanged += new System.EventHandler(this._abilityResourceProductivityComboBox_SelectedIndexChanged);
			// 
			// _abilityResourceDropLabel
			// 
			resources.ApplyResources(this._abilityResourceDropLabel, "_abilityResourceDropLabel");
			this._abilityResourceDropLabel.Name = "_abilityResourceDropLabel";
			// 
			// _abilityResourceDropComboBox
			// 
			resources.ApplyResources(this._abilityResourceDropComboBox, "_abilityResourceDropComboBox");
			this._abilityResourceDropComboBox.FormattingEnabled = true;
			this._abilityResourceDropComboBox.Name = "_abilityResourceDropComboBox";
			this._abilityResourceDropComboBox.SelectedIndexChanged += new System.EventHandler(this._abilityResourceDropComboBox_SelectedIndexChanged);
			// 
			// _abilityResourceCarryLabel
			// 
			resources.ApplyResources(this._abilityResourceCarryLabel, "_abilityResourceCarryLabel");
			this._abilityResourceCarryLabel.Name = "_abilityResourceCarryLabel";
			// 
			// _abilityResourceCarryComboBox
			// 
			resources.ApplyResources(this._abilityResourceCarryComboBox, "_abilityResourceCarryComboBox");
			this._abilityResourceCarryComboBox.FormattingEnabled = true;
			this._abilityResourceCarryComboBox.Name = "_abilityResourceCarryComboBox";
			this._abilityResourceCarryComboBox.SelectedIndexChanged += new System.EventHandler(this._abilityResourceCarryComboBox_SelectedIndexChanged);
			// 
			// _newAbilityButton
			// 
			resources.ApplyResources(this._newAbilityButton, "_newAbilityButton");
			this._newAbilityButton.BackgroundImage = global::TechTreeEditor.Icons.NewAbility;
			this._newAbilityButton.FlatAppearance.BorderSize = 0;
			this._newAbilityButton.Name = "_newAbilityButton";
			this._newAbilityButton.UseVisualStyleBackColor = true;
			this._newAbilityButton.Click += new System.EventHandler(this._newAbilityButton_Click);
			// 
			// _deleteAbilityButton
			// 
			resources.ApplyResources(this._deleteAbilityButton, "_deleteAbilityButton");
			this._deleteAbilityButton.BackgroundImage = global::TechTreeEditor.Icons.DeleteAbility;
			this._deleteAbilityButton.FlatAppearance.BorderSize = 0;
			this._deleteAbilityButton.Name = "_deleteAbilityButton";
			this._deleteAbilityButton.UseVisualStyleBackColor = true;
			this._deleteAbilityButton.Click += new System.EventHandler(this._deleteAbilityButton_Click);
			// 
			// _abilityTypeComboBox
			// 
			resources.ApplyResources(this._abilityTypeComboBox, "_abilityTypeComboBox");
			this._abilityTypeComboBox.FormattingEnabled = true;
			this._abilityTypeComboBox.Name = "_abilityTypeComboBox";
			this._abilityTypeComboBox.SelectedIndexChanged += new System.EventHandler(this._abilityTypeComboBox_SelectedIndexChanged);
			// 
			// _abilityClassComboBox
			// 
			resources.ApplyResources(this._abilityClassComboBox, "_abilityClassComboBox");
			this._abilityClassComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this._abilityClassComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this._abilityClassComboBox.FormattingEnabled = true;
			this._abilityClassComboBox.Name = "_abilityClassComboBox";
			this._abilityClassComboBox.SelectedIndexChanged += new System.EventHandler(this._abilityClassComboBox_SelectedIndexChanged);
			// 
			// _abilityInfoLabel
			// 
			resources.ApplyResources(this._abilityInfoLabel, "_abilityInfoLabel");
			this._abilityInfoLabel.Name = "_abilityInfoLabel";
			// 
			// _abilityResourceLabel
			// 
			resources.ApplyResources(this._abilityResourceLabel, "_abilityResourceLabel");
			this._abilityResourceLabel.Name = "_abilityResourceLabel";
			// 
			// _abilityResourceComboBox
			// 
			resources.ApplyResources(this._abilityResourceComboBox, "_abilityResourceComboBox");
			this._abilityResourceComboBox.FormattingEnabled = true;
			this._abilityResourceComboBox.Name = "_abilityResourceComboBox";
			this._abilityResourceComboBox.SelectedIndexChanged += new System.EventHandler(this._abilityResourceComboBox_SelectedIndexChanged);
			// 
			// _abilityTypeLabel
			// 
			resources.ApplyResources(this._abilityTypeLabel, "_abilityTypeLabel");
			this._abilityTypeLabel.Name = "_abilityTypeLabel";
			// 
			// _abilityClassLabel
			// 
			resources.ApplyResources(this._abilityClassLabel, "_abilityClassLabel");
			this._abilityClassLabel.Name = "_abilityClassLabel";
			// 
			// _statsTabPage
			// 
			resources.ApplyResources(this._statsTabPage, "_statsTabPage");
			this._statsTabPage.Controls.Add(this._garrisonHealRateField);
			this._statsTabPage.Controls.Add(this._workRateField);
			this._statsTabPage.Controls.Add(this._searchRadiusField);
			this._statsTabPage.Controls.Add(this._rotationSpeedField);
			this._statsTabPage.Controls.Add(this._speedField);
			this._statsTabPage.Controls.Add(this._losField);
			this._statsTabPage.Controls.Add(this._resourceCapacityField);
			this._statsTabPage.Controls.Add(this._garrisonCapacityField);
			this._statsTabPage.Name = "_statsTabPage";
			this._statsTabPage.UseVisualStyleBackColor = true;
			// 
			// _garrisonHealRateField
			// 
			resources.ApplyResources(this._garrisonHealRateField, "_garrisonHealRateField");
			this._garrisonHealRateField.Name = "_garrisonHealRateField";
			this._garrisonHealRateField.NameString = "Heal rate:";
			this._garrisonHealRateField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._garrisonHealRateField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._garrisonHealRateField_ValueChanged);
			// 
			// _workRateField
			// 
			resources.ApplyResources(this._workRateField, "_workRateField");
			this._workRateField.Name = "_workRateField";
			this._workRateField.NameString = "Work rate:";
			this._workRateField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._workRateField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._workRateField_ValueChanged);
			// 
			// _searchRadiusField
			// 
			resources.ApplyResources(this._searchRadiusField, "_searchRadiusField");
			this._searchRadiusField.Name = "_searchRadiusField";
			this._searchRadiusField.NameString = "Search radius:";
			this._searchRadiusField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._searchRadiusField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._searchRadiusField_ValueChanged);
			// 
			// _rotationSpeedField
			// 
			resources.ApplyResources(this._rotationSpeedField, "_rotationSpeedField");
			this._rotationSpeedField.Name = "_rotationSpeedField";
			this._rotationSpeedField.NameString = "Rotation speed:";
			this._rotationSpeedField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._rotationSpeedField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._rotationSpeedField_ValueChanged);
			// 
			// _speedField
			// 
			resources.ApplyResources(this._speedField, "_speedField");
			this._speedField.Name = "_speedField";
			this._speedField.NameString = "Speed:";
			this._speedField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._speedField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._speedField_ValueChanged);
			// 
			// _losField
			// 
			resources.ApplyResources(this._losField, "_losField");
			this._losField.Name = "_losField";
			this._losField.NameString = "Line of sight:";
			this._losField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._losField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._losField_ValueChanged);
			// 
			// _resourceCapacityField
			// 
			resources.ApplyResources(this._resourceCapacityField, "_resourceCapacityField");
			this._resourceCapacityField.Name = "_resourceCapacityField";
			this._resourceCapacityField.NameString = "Resource capacity:";
			this._resourceCapacityField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._resourceCapacityField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._resourceCapacityField_ValueChanged);
			// 
			// _garrisonCapacityField
			// 
			resources.ApplyResources(this._garrisonCapacityField, "_garrisonCapacityField");
			this._garrisonCapacityField.Name = "_garrisonCapacityField";
			this._garrisonCapacityField.NameString = "Garrison capacity:";
			this._garrisonCapacityField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._garrisonCapacityField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._garrisonCapacityField_ValueChanged);
			// 
			// _combatTabPage
			// 
			resources.ApplyResources(this._combatTabPage, "_combatTabPage");
			this._combatTabPage.Controls.Add(this._blastLevelField);
			this._combatTabPage.Controls.Add(this._terrainMultField);
			this._combatTabPage.Controls.Add(this._blastRadiusField);
			this._combatTabPage.Controls.Add(this._defaultArmorField);
			this._combatTabPage.Controls.Add(this._attackModeField);
			this._combatTabPage.Controls.Add(this.groupBox22);
			this._combatTabPage.Controls.Add(this.groupBox20);
			this._combatTabPage.Controls.Add(this.groupBox19);
			this._combatTabPage.Controls.Add(this.groupBox18);
			this._combatTabPage.Controls.Add(this._blastTypeField);
			this._combatTabPage.Controls.Add(this._hitpointsField);
			this._combatTabPage.Name = "_combatTabPage";
			this._combatTabPage.UseVisualStyleBackColor = true;
			// 
			// _blastLevelField
			// 
			resources.ApplyResources(this._blastLevelField, "_blastLevelField");
			this._blastLevelField.Name = "_blastLevelField";
			this._blastLevelField.NameString = "Blast level:";
			this._blastLevelField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._blastLevelField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._blastLevelField_ValueChanged);
			// 
			// _terrainMultField
			// 
			resources.ApplyResources(this._terrainMultField, "_terrainMultField");
			this._terrainMultField.Name = "_terrainMultField";
			this._terrainMultField.NameString = "Terrain multiplicator:";
			this._terrainMultField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._terrainMultField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._terrainMultField_ValueChanged);
			// 
			// _blastRadiusField
			// 
			resources.ApplyResources(this._blastRadiusField, "_blastRadiusField");
			this._blastRadiusField.Name = "_blastRadiusField";
			this._blastRadiusField.NameString = "Blast radius:";
			this._blastRadiusField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._blastRadiusField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._blastRadiusField_ValueChanged);
			// 
			// _defaultArmorField
			// 
			resources.ApplyResources(this._defaultArmorField, "_defaultArmorField");
			this._defaultArmorField.Name = "_defaultArmorField";
			this._defaultArmorField.NameString = "Default armour:";
			this._defaultArmorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._defaultArmorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._defaultArmorField_ValueChanged);
			// 
			// _attackModeField
			// 
			resources.ApplyResources(this._attackModeField, "_attackModeField");
			this._attackModeField.Name = "_attackModeField";
			this._attackModeField.NameString = "Attack mode:";
			this._attackModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._attackModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._attackModeField_ValueChanged);
			// 
			// groupBox22
			// 
			resources.ApplyResources(this.groupBox22, "groupBox22");
			this.groupBox22.Controls.Add(this._attackReloadTimeDisplayedField);
			this.groupBox22.Controls.Add(this._attackReloadTimeField);
			this.groupBox22.Name = "groupBox22";
			this.groupBox22.TabStop = false;
			// 
			// _attackReloadTimeDisplayedField
			// 
			resources.ApplyResources(this._attackReloadTimeDisplayedField, "_attackReloadTimeDisplayedField");
			this._attackReloadTimeDisplayedField.Name = "_attackReloadTimeDisplayedField";
			this._attackReloadTimeDisplayedField.NameString = "Displayed:";
			this._attackReloadTimeDisplayedField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._attackReloadTimeDisplayedField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._attackReloadTimeDisplayedField_ValueChanged);
			// 
			// _attackReloadTimeField
			// 
			resources.ApplyResources(this._attackReloadTimeField, "_attackReloadTimeField");
			this._attackReloadTimeField.Name = "_attackReloadTimeField";
			this._attackReloadTimeField.NameString = "Value:";
			this._attackReloadTimeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._attackReloadTimeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._attackReloadTimeField_ValueChanged);
			// 
			// groupBox20
			// 
			resources.ApplyResources(this.groupBox20, "groupBox20");
			this.groupBox20.Controls.Add(this._rangeDisplayedField);
			this.groupBox20.Controls.Add(this._rangeMaxField);
			this.groupBox20.Controls.Add(this._rangeMinField);
			this.groupBox20.Name = "groupBox20";
			this.groupBox20.TabStop = false;
			// 
			// _rangeDisplayedField
			// 
			resources.ApplyResources(this._rangeDisplayedField, "_rangeDisplayedField");
			this._rangeDisplayedField.Name = "_rangeDisplayedField";
			this._rangeDisplayedField.NameString = "Displayed:";
			this._rangeDisplayedField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._rangeDisplayedField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._rangeDisplayedField_ValueChanged);
			// 
			// _rangeMaxField
			// 
			resources.ApplyResources(this._rangeMaxField, "_rangeMaxField");
			this._rangeMaxField.Name = "_rangeMaxField";
			this._rangeMaxField.NameString = "Maximum:";
			this._rangeMaxField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._rangeMaxField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._rangeMaxField_ValueChanged);
			// 
			// _rangeMinField
			// 
			resources.ApplyResources(this._rangeMinField, "_rangeMinField");
			this._rangeMinField.Name = "_rangeMinField";
			this._rangeMinField.NameString = "Minimum:";
			this._rangeMinField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._rangeMinField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._rangeMinField_ValueChanged);
			// 
			// groupBox19
			// 
			resources.ApplyResources(this.groupBox19, "groupBox19");
			this.groupBox19.Controls.Add(this._armourValuesField);
			this.groupBox19.Controls.Add(this._displayedPierceArmorField);
			this.groupBox19.Controls.Add(this._displayedMeleeArmourField);
			this.groupBox19.Name = "groupBox19";
			this.groupBox19.TabStop = false;
			// 
			// _armourValuesField
			// 
			resources.ApplyResources(this._armourValuesField, "_armourValuesField");
			this._armourValuesField.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._armourValuesField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._armourValuesField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._armValClassColumn,
            this._armValValueColumn});
			this._armourValuesField.Name = "_armourValuesField";
			this._armourValuesField.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._armourValuesField_CellValidating);
			this._armourValuesField.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._armourValuesField_CellValueChanged);
			this._armourValuesField.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this._armourValuesField_RowsRemoved);
			// 
			// _displayedPierceArmorField
			// 
			resources.ApplyResources(this._displayedPierceArmorField, "_displayedPierceArmorField");
			this._displayedPierceArmorField.Name = "_displayedPierceArmorField";
			this._displayedPierceArmorField.NameString = "/";
			this._displayedPierceArmorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._displayedPierceArmorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._displayedPierceArmorField_ValueChanged);
			// 
			// _displayedMeleeArmourField
			// 
			resources.ApplyResources(this._displayedMeleeArmourField, "_displayedMeleeArmourField");
			this._displayedMeleeArmourField.Name = "_displayedMeleeArmourField";
			this._displayedMeleeArmourField.NameString = "Displayed:";
			this._displayedMeleeArmourField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._displayedMeleeArmourField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._displayedMeleeArmourField_ValueChanged);
			// 
			// groupBox18
			// 
			resources.ApplyResources(this.groupBox18, "groupBox18");
			this.groupBox18.Controls.Add(this._attackValuesField);
			this.groupBox18.Controls.Add(this._displayedAttackField);
			this.groupBox18.Name = "groupBox18";
			this.groupBox18.TabStop = false;
			// 
			// _attackValuesField
			// 
			resources.ApplyResources(this._attackValuesField, "_attackValuesField");
			this._attackValuesField.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._attackValuesField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._attackValuesField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._attValClassColumn,
            this._attValValueColumn});
			this._attackValuesField.Name = "_attackValuesField";
			this._attackValuesField.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._attackValuesField_CellValidating);
			this._attackValuesField.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._attackValuesField_CellValueChanged);
			this._attackValuesField.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this._attackValuesField_RowsRemoved);
			// 
			// _displayedAttackField
			// 
			resources.ApplyResources(this._displayedAttackField, "_displayedAttackField");
			this._displayedAttackField.Name = "_displayedAttackField";
			this._displayedAttackField.NameString = "Displayed:";
			this._displayedAttackField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._displayedAttackField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._displayedAttackField_ValueChanged);
			// 
			// _blastTypeField
			// 
			resources.ApplyResources(this._blastTypeField, "_blastTypeField");
			this._blastTypeField.Name = "_blastTypeField";
			this._blastTypeField.NameString = "Blast type:";
			this._blastTypeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._blastTypeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._blastTypeField_ValueChanged);
			// 
			// _hitpointsField
			// 
			resources.ApplyResources(this._hitpointsField, "_hitpointsField");
			this._hitpointsField.Name = "_hitpointsField";
			this._hitpointsField.NameString = "Hitpoints:";
			this._hitpointsField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._hitpointsField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._hitpointsField_ValueChanged);
			// 
			// _projectileTabPage
			// 
			resources.ApplyResources(this._projectileTabPage, "_projectileTabPage");
			this._projectileTabPage.Controls.Add(this._missileDuplMaxField);
			this._projectileTabPage.Controls.Add(this._missileSpawnZField);
			this._projectileTabPage.Controls.Add(this._missileDuplMinField);
			this._projectileTabPage.Controls.Add(this._frameDelayField);
			this._projectileTabPage.Controls.Add(this._missileSpawnYField);
			this._projectileTabPage.Controls.Add(this._graphicDisplacementZField);
			this._projectileTabPage.Controls.Add(this._missileSpawnXField);
			this._projectileTabPage.Controls.Add(this._graphicDisplacementYField);
			this._projectileTabPage.Controls.Add(this._graphicDisplacementXField);
			this._projectileTabPage.Controls.Add(this._projectileArcField);
			this._projectileTabPage.Controls.Add(this._penetrationModeField);
			this._projectileTabPage.Controls.Add(this._accuracyErrorField);
			this._projectileTabPage.Controls.Add(this._dropAnimationModeField);
			this._projectileTabPage.Controls.Add(this._accuracyPercentField);
			this._projectileTabPage.Controls.Add(this._compensationModeField);
			this._projectileTabPage.Controls.Add(this._stretchModeField);
			this._projectileTabPage.Name = "_projectileTabPage";
			this._projectileTabPage.UseVisualStyleBackColor = true;
			// 
			// _missileDuplMaxField
			// 
			resources.ApplyResources(this._missileDuplMaxField, "_missileDuplMaxField");
			this._missileDuplMaxField.Name = "_missileDuplMaxField";
			this._missileDuplMaxField.NameString = "Maximum duplication:";
			this._missileDuplMaxField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._missileDuplMaxField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._missileDuplMaxField_ValueChanged);
			// 
			// _missileSpawnZField
			// 
			resources.ApplyResources(this._missileSpawnZField, "_missileSpawnZField");
			this._missileSpawnZField.Name = "_missileSpawnZField";
			this._missileSpawnZField.NameString = "Spawn randomness:";
			this._missileSpawnZField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._missileSpawnZField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._missileSpawnZField_ValueChanged);
			// 
			// _missileDuplMinField
			// 
			resources.ApplyResources(this._missileDuplMinField, "_missileDuplMinField");
			this._missileDuplMinField.Name = "_missileDuplMinField";
			this._missileDuplMinField.NameString = "Minimum duplication:";
			this._missileDuplMinField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._missileDuplMinField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._missileDuplMinField_ValueChanged);
			// 
			// _frameDelayField
			// 
			resources.ApplyResources(this._frameDelayField, "_frameDelayField");
			this._frameDelayField.Name = "_frameDelayField";
			this._frameDelayField.NameString = "Frame delay:";
			this._frameDelayField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._frameDelayField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._frameDelayField_ValueChanged);
			// 
			// _missileSpawnYField
			// 
			resources.ApplyResources(this._missileSpawnYField, "_missileSpawnYField");
			this._missileSpawnYField.Name = "_missileSpawnYField";
			this._missileSpawnYField.NameString = "Spawn radius 2:";
			this._missileSpawnYField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._missileSpawnYField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._missileSpawnYField_ValueChanged);
			// 
			// _graphicDisplacementZField
			// 
			resources.ApplyResources(this._graphicDisplacementZField, "_graphicDisplacementZField");
			this._graphicDisplacementZField.Name = "_graphicDisplacementZField";
			this._graphicDisplacementZField.NameString = "Height:";
			this._graphicDisplacementZField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._graphicDisplacementZField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._graphicDisplacementZField_ValueChanged);
			// 
			// _missileSpawnXField
			// 
			resources.ApplyResources(this._missileSpawnXField, "_missileSpawnXField");
			this._missileSpawnXField.Name = "_missileSpawnXField";
			this._missileSpawnXField.NameString = "Spawn radius 1:";
			this._missileSpawnXField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._missileSpawnXField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._missileSpawnXField_ValueChanged);
			// 
			// _graphicDisplacementYField
			// 
			resources.ApplyResources(this._graphicDisplacementYField, "_graphicDisplacementYField");
			this._graphicDisplacementYField.Name = "_graphicDisplacementYField";
			this._graphicDisplacementYField.NameString = "Displacement to the front:";
			this._graphicDisplacementYField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._graphicDisplacementYField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._graphicDisplacementYField_ValueChanged);
			// 
			// _graphicDisplacementXField
			// 
			resources.ApplyResources(this._graphicDisplacementXField, "_graphicDisplacementXField");
			this._graphicDisplacementXField.Name = "_graphicDisplacementXField";
			this._graphicDisplacementXField.NameString = "Displacement to the right:";
			this._graphicDisplacementXField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._graphicDisplacementXField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._graphicDisplacementXField_ValueChanged);
			// 
			// _projectileArcField
			// 
			resources.ApplyResources(this._projectileArcField, "_projectileArcField");
			this._projectileArcField.Name = "_projectileArcField";
			this._projectileArcField.NameString = "Trajectory arc:";
			this._projectileArcField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._projectileArcField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._projectileArcField_ValueChanged);
			// 
			// _penetrationModeField
			// 
			resources.ApplyResources(this._penetrationModeField, "_penetrationModeField");
			this._penetrationModeField.Name = "_penetrationModeField";
			this._penetrationModeField.NameString = "Penetration mode:";
			this._penetrationModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._penetrationModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._penetrationModeField_ValueChanged);
			// 
			// _accuracyErrorField
			// 
			resources.ApplyResources(this._accuracyErrorField, "_accuracyErrorField");
			this._accuracyErrorField.Name = "_accuracyErrorField";
			this._accuracyErrorField.NameString = "Error accuracy:";
			this._accuracyErrorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._accuracyErrorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._accuracyErrorField_ValueChanged);
			// 
			// _dropAnimationModeField
			// 
			resources.ApplyResources(this._dropAnimationModeField, "_dropAnimationModeField");
			this._dropAnimationModeField.Name = "_dropAnimationModeField";
			this._dropAnimationModeField.NameString = "Disappearance mode:";
			this._dropAnimationModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._dropAnimationModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._dropAnimationModeField_ValueChanged);
			// 
			// _accuracyPercentField
			// 
			resources.ApplyResources(this._accuracyPercentField, "_accuracyPercentField");
			this._accuracyPercentField.Name = "_accuracyPercentField";
			this._accuracyPercentField.NameString = "Percentage accuracy:";
			this._accuracyPercentField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._accuracyPercentField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._accuracyPercentField_ValueChanged);
			// 
			// _compensationModeField
			// 
			resources.ApplyResources(this._compensationModeField, "_compensationModeField");
			this._compensationModeField.Name = "_compensationModeField";
			this._compensationModeField.NameString = "Compensation mode:";
			this._compensationModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._compensationModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._compensationModeField_ValueChanged);
			// 
			// _stretchModeField
			// 
			resources.ApplyResources(this._stretchModeField, "_stretchModeField");
			this._stretchModeField.Name = "_stretchModeField";
			this._stretchModeField.NameString = "Trajectory type:";
			this._stretchModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._stretchModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._stretchModeField_ValueChanged);
			// 
			// _trainTabPage
			// 
			resources.ApplyResources(this._trainTabPage, "_trainTabPage");
			this._trainTabPage.Controls.Add(this._trainTimeField);
			this._trainTabPage.Controls.Add(this.groupBox25);
			this._trainTabPage.Controls.Add(this.groupBox6);
			this._trainTabPage.Name = "_trainTabPage";
			this._trainTabPage.UseVisualStyleBackColor = true;
			// 
			// _trainTimeField
			// 
			resources.ApplyResources(this._trainTimeField, "_trainTimeField");
			this._trainTimeField.Name = "_trainTimeField";
			this._trainTimeField.NameString = "Creation time:";
			this._trainTimeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._trainTimeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._trainTimeField_ValueChanged);
			// 
			// groupBox25
			// 
			resources.ApplyResources(this.groupBox25, "groupBox25");
			this.groupBox25.Controls.Add(this._cost3Field);
			this.groupBox25.Controls.Add(this._cost2Field);
			this.groupBox25.Controls.Add(this._cost1Field);
			this.groupBox25.Name = "groupBox25";
			this.groupBox25.TabStop = false;
			// 
			// _cost3Field
			// 
			resources.ApplyResources(this._cost3Field, "_cost3Field");
			this._cost3Field.Name = "_cost3Field";
			this._cost3Field.NameString = "Cost 3:";
			this._cost3Field.ValueChanged += new TechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._cost3Field_ValueChanged);
			// 
			// _cost2Field
			// 
			resources.ApplyResources(this._cost2Field, "_cost2Field");
			this._cost2Field.Name = "_cost2Field";
			this._cost2Field.NameString = "Cost 2:";
			this._cost2Field.ValueChanged += new TechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._cost2Field_ValueChanged);
			// 
			// _cost1Field
			// 
			resources.ApplyResources(this._cost1Field, "_cost1Field");
			this._cost1Field.Name = "_cost1Field";
			this._cost1Field.NameString = "Cost 1:";
			this._cost1Field.ValueChanged += new TechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._cost1Field_ValueChanged);
			// 
			// groupBox6
			// 
			resources.ApplyResources(this.groupBox6, "groupBox6");
			this.groupBox6.Controls.Add(this._resourceStorage3ModeField);
			this.groupBox6.Controls.Add(this._resourceStorage2ModeField);
			this.groupBox6.Controls.Add(this._resourceStorage1ModeField);
			this.groupBox6.Controls.Add(this._resourceStorage3Field);
			this.groupBox6.Controls.Add(this._resourceStorage2Field);
			this.groupBox6.Controls.Add(this._resourceStorage1Field);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.TabStop = false;
			// 
			// _resourceStorage3ModeField
			// 
			resources.ApplyResources(this._resourceStorage3ModeField, "_resourceStorage3ModeField");
			this._resourceStorage3ModeField.Name = "_resourceStorage3ModeField";
			this._resourceStorage3ModeField.NameString = "Mode:";
			this._resourceStorage3ModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._resourceStorage3ModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._resourceStorage3ModeField_ValueChanged);
			// 
			// _resourceStorage2ModeField
			// 
			resources.ApplyResources(this._resourceStorage2ModeField, "_resourceStorage2ModeField");
			this._resourceStorage2ModeField.Name = "_resourceStorage2ModeField";
			this._resourceStorage2ModeField.NameString = "Mode:";
			this._resourceStorage2ModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._resourceStorage2ModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._resourceStorage2ModeField_ValueChanged);
			// 
			// _resourceStorage1ModeField
			// 
			resources.ApplyResources(this._resourceStorage1ModeField, "_resourceStorage1ModeField");
			this._resourceStorage1ModeField.Name = "_resourceStorage1ModeField";
			this._resourceStorage1ModeField.NameString = "Mode:";
			this._resourceStorage1ModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._resourceStorage1ModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._resourceStorage1ModeField_ValueChanged);
			// 
			// _resourceStorage3Field
			// 
			resources.ApplyResources(this._resourceStorage3Field, "_resourceStorage3Field");
			this._resourceStorage3Field.Name = "_resourceStorage3Field";
			this._resourceStorage3Field.NameString = "Storage 3:";
			this._resourceStorage3Field.ValueChanged += new TechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._resourceStorage3Field_ValueChanged);
			// 
			// _resourceStorage2Field
			// 
			resources.ApplyResources(this._resourceStorage2Field, "_resourceStorage2Field");
			this._resourceStorage2Field.Name = "_resourceStorage2Field";
			this._resourceStorage2Field.NameString = "Storage 2:";
			this._resourceStorage2Field.ValueChanged += new TechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._resourceStorage2Field_ValueChanged);
			// 
			// _resourceStorage1Field
			// 
			resources.ApplyResources(this._resourceStorage1Field, "_resourceStorage1Field");
			this._resourceStorage1Field.Name = "_resourceStorage1Field";
			this._resourceStorage1Field.NameString = "Storage 1:";
			this._resourceStorage1Field.ValueChanged += new TechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._resourceStorage1Field_ValueChanged);
			// 
			// _editorTabPage
			// 
			resources.ApplyResources(this._editorTabPage, "_editorTabPage");
			this._editorTabPage.Controls.Add(this._classLabel);
			this._editorTabPage.Controls.Add(this._classComboBox);
			this._editorTabPage.Controls.Add(this.groupBox33);
			this._editorTabPage.Controls.Add(this.groupBox13);
			this._editorTabPage.Controls.Add(this.groupBox9);
			this._editorTabPage.Controls.Add(this.groupBox5);
			this._editorTabPage.Controls.Add(this._garrisonTypeField);
			this._editorTabPage.Controls.Add(this._chargeModeField);
			this._editorTabPage.Controls.Add(this._heroModeField);
			this._editorTabPage.Controls.Add(this._animalModeField);
			this._editorTabPage.Controls.Add(this._villagerModeField);
			this._editorTabPage.Controls.Add(this._sheepConvField);
			this._editorTabPage.Controls.Add(this._trackUnitDensityField);
			this._editorTabPage.Controls.Add(this._civilizationField);
			this._editorTabPage.Controls.Add(this._hpBar2Field);
			this._editorTabPage.Controls.Add(this._hpBar1Field);
			this._editorTabPage.Controls.Add(this._disappearsField);
			this._editorTabPage.Controls.Add(this._graphicAngleField);
			this._editorTabPage.Controls.Add(this._adjacentModeField);
			this._editorTabPage.Controls.Add(this._placementModeField);
			this._editorTabPage.Controls.Add(this._enabledField);
			this._editorTabPage.Controls.Add(this._deadModeField);
			this._editorTabPage.Name = "_editorTabPage";
			this._editorTabPage.UseVisualStyleBackColor = true;
			// 
			// _classLabel
			// 
			resources.ApplyResources(this._classLabel, "_classLabel");
			this._classLabel.Name = "_classLabel";
			// 
			// _classComboBox
			// 
			resources.ApplyResources(this._classComboBox, "_classComboBox");
			this._classComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this._classComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this._classComboBox.FormattingEnabled = true;
			this._classComboBox.Name = "_classComboBox";
			this._classComboBox.SelectedIndexChanged += new System.EventHandler(this._classComboBox_SelectedIndexChanged);
			// 
			// groupBox33
			// 
			resources.ApplyResources(this.groupBox33, "groupBox33");
			this.groupBox33.Controls.Add(this._lootStoneField);
			this.groupBox33.Controls.Add(this._lootGoldField);
			this.groupBox33.Controls.Add(this._lootFoodField);
			this.groupBox33.Controls.Add(this._lootWoodField);
			this.groupBox33.Name = "groupBox33";
			this.groupBox33.TabStop = false;
			// 
			// _lootStoneField
			// 
			resources.ApplyResources(this._lootStoneField, "_lootStoneField");
			this._lootStoneField.Name = "_lootStoneField";
			this._lootStoneField.NameString = "Stone";
			this._lootStoneField.Value = false;
			this._lootStoneField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._lootStoneField_ValueChanged);
			// 
			// _lootGoldField
			// 
			resources.ApplyResources(this._lootGoldField, "_lootGoldField");
			this._lootGoldField.Name = "_lootGoldField";
			this._lootGoldField.NameString = "Gold";
			this._lootGoldField.Value = false;
			this._lootGoldField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._lootGoldField_ValueChanged);
			// 
			// _lootFoodField
			// 
			resources.ApplyResources(this._lootFoodField, "_lootFoodField");
			this._lootFoodField.Name = "_lootFoodField";
			this._lootFoodField.NameString = "Food";
			this._lootFoodField.Value = false;
			this._lootFoodField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._lootFoodField_ValueChanged);
			// 
			// _lootWoodField
			// 
			resources.ApplyResources(this._lootWoodField, "_lootWoodField");
			this._lootWoodField.Name = "_lootWoodField";
			this._lootWoodField.NameString = "Wood";
			this._lootWoodField.Value = false;
			this._lootWoodField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._lootWoodField_ValueChanged);
			// 
			// groupBox13
			// 
			resources.ApplyResources(this.groupBox13, "groupBox13");
			this.groupBox13.Controls.Add(this._selEditorColorField);
			this.groupBox13.Controls.Add(this._selShapeField);
			this.groupBox13.Controls.Add(this._selShapeTypeField);
			this.groupBox13.Controls.Add(this._selEffectField);
			this.groupBox13.Controls.Add(this._selMaskField);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.TabStop = false;
			// 
			// _selEditorColorField
			// 
			resources.ApplyResources(this._selEditorColorField, "_selEditorColorField");
			this._selEditorColorField.Name = "_selEditorColorField";
			this._selEditorColorField.NameString = "Editor color:";
			this._selEditorColorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._selEditorColorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._selEditorColorField_ValueChanged);
			// 
			// _selShapeField
			// 
			resources.ApplyResources(this._selShapeField, "_selShapeField");
			this._selShapeField.Name = "_selShapeField";
			this._selShapeField.NameString = "Shape:";
			this._selShapeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._selShapeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._selShapeField_ValueChanged);
			// 
			// _selShapeTypeField
			// 
			resources.ApplyResources(this._selShapeTypeField, "_selShapeTypeField");
			this._selShapeTypeField.Name = "_selShapeTypeField";
			this._selShapeTypeField.NameString = "Shape type:";
			this._selShapeTypeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._selShapeTypeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._selShapeTypeField_ValueChanged);
			// 
			// _selEffectField
			// 
			resources.ApplyResources(this._selEffectField, "_selEffectField");
			this._selEffectField.Name = "_selEffectField";
			this._selEffectField.NameString = "Effect:";
			this._selEffectField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._selEffectField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._selEffectField_ValueChanged);
			// 
			// _selMaskField
			// 
			resources.ApplyResources(this._selMaskField, "_selMaskField");
			this._selMaskField.Name = "_selMaskField";
			this._selMaskField.NameString = "Mask:";
			this._selMaskField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._selMaskField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._selMaskField_ValueChanged);
			// 
			// groupBox9
			// 
			resources.ApplyResources(this.groupBox9, "groupBox9");
			this.groupBox9.Controls.Add(this._terrRestrField);
			this.groupBox9.Controls.Add(this._terrSide2Field);
			this.groupBox9.Controls.Add(this._terrSide1Field);
			this.groupBox9.Controls.Add(this._terrPlacement2Field);
			this.groupBox9.Controls.Add(this._terrPlacement1Field);
			this.groupBox9.Controls.Add(this._foundTerrainField);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.TabStop = false;
			// 
			// _terrRestrField
			// 
			resources.ApplyResources(this._terrRestrField, "_terrRestrField");
			this._terrRestrField.Name = "_terrRestrField";
			this._terrRestrField.NameString = "Restriction:";
			this._terrRestrField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._terrRestrField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._terrRestrField_ValueChanged);
			// 
			// _terrSide2Field
			// 
			resources.ApplyResources(this._terrSide2Field, "_terrSide2Field");
			this._terrSide2Field.Name = "_terrSide2Field";
			this._terrSide2Field.NameString = "Side 2:";
			this._terrSide2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._terrSide2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._terrSide2Field_ValueChanged);
			// 
			// _terrSide1Field
			// 
			resources.ApplyResources(this._terrSide1Field, "_terrSide1Field");
			this._terrSide1Field.Name = "_terrSide1Field";
			this._terrSide1Field.NameString = "Side 1:";
			this._terrSide1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._terrSide1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._terrSide1Field_ValueChanged);
			// 
			// _terrPlacement2Field
			// 
			resources.ApplyResources(this._terrPlacement2Field, "_terrPlacement2Field");
			this._terrPlacement2Field.Name = "_terrPlacement2Field";
			this._terrPlacement2Field.NameString = "Bottom 2:";
			this._terrPlacement2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._terrPlacement2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._terrPlacement2Field_ValueChanged);
			// 
			// _terrPlacement1Field
			// 
			resources.ApplyResources(this._terrPlacement1Field, "_terrPlacement1Field");
			this._terrPlacement1Field.Name = "_terrPlacement1Field";
			this._terrPlacement1Field.NameString = "Bottom 1:";
			this._terrPlacement1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._terrPlacement1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._terrPlacement1Field_ValueChanged);
			// 
			// _foundTerrainField
			// 
			resources.ApplyResources(this._foundTerrainField, "_foundTerrainField");
			this._foundTerrainField.Name = "_foundTerrainField";
			this._foundTerrainField.NameString = "Foundation:";
			this._foundTerrainField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._foundTerrainField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._foundTerrainField_ValueChanged);
			// 
			// groupBox5
			// 
			resources.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.Controls.Add(this._selectionYField);
			this.groupBox5.Controls.Add(this._selectionXField);
			this.groupBox5.Controls.Add(this._editorYField);
			this.groupBox5.Controls.Add(this._editorXField);
			this.groupBox5.Controls.Add(this._sizeYField);
			this.groupBox5.Controls.Add(this._sizeXField);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			// 
			// _selectionYField
			// 
			resources.ApplyResources(this._selectionYField, "_selectionYField");
			this._selectionYField.Name = "_selectionYField";
			this._selectionYField.NameString = "Selection Y:";
			this._selectionYField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._selectionYField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._selectionYField_ValueChanged);
			// 
			// _selectionXField
			// 
			resources.ApplyResources(this._selectionXField, "_selectionXField");
			this._selectionXField.Name = "_selectionXField";
			this._selectionXField.NameString = "Selection X:";
			this._selectionXField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._selectionXField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._selectionXField_ValueChanged);
			// 
			// _editorYField
			// 
			resources.ApplyResources(this._editorYField, "_editorYField");
			this._editorYField.Name = "_editorYField";
			this._editorYField.NameString = "Editor Y:";
			this._editorYField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._editorYField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._editorYField_ValueChanged);
			// 
			// _editorXField
			// 
			resources.ApplyResources(this._editorXField, "_editorXField");
			this._editorXField.Name = "_editorXField";
			this._editorXField.NameString = "Editor X:";
			this._editorXField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._editorXField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._editorXField_ValueChanged);
			// 
			// _sizeYField
			// 
			resources.ApplyResources(this._sizeYField, "_sizeYField");
			this._sizeYField.Name = "_sizeYField";
			this._sizeYField.NameString = "Size Y:";
			this._sizeYField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._sizeYField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._sizeYField_ValueChanged);
			// 
			// _sizeXField
			// 
			resources.ApplyResources(this._sizeXField, "_sizeXField");
			this._sizeXField.Name = "_sizeXField";
			this._sizeXField.NameString = "Size X:";
			this._sizeXField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._sizeXField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._sizeXField_ValueChanged);
			// 
			// _garrisonTypeField
			// 
			resources.ApplyResources(this._garrisonTypeField, "_garrisonTypeField");
			this._garrisonTypeField.Name = "_garrisonTypeField";
			this._garrisonTypeField.NameString = "Garrison type:";
			this._garrisonTypeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._garrisonTypeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._garrisonTypeField_ValueChanged);
			// 
			// _chargeModeField
			// 
			resources.ApplyResources(this._chargeModeField, "_chargeModeField");
			this._chargeModeField.Name = "_chargeModeField";
			this._chargeModeField.NameString = "Charge mode:";
			this._chargeModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._chargeModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._chargeModeField_ValueChanged);
			// 
			// _heroModeField
			// 
			resources.ApplyResources(this._heroModeField, "_heroModeField");
			this._heroModeField.Name = "_heroModeField";
			this._heroModeField.NameString = "Hero mode:";
			this._heroModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._heroModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._heroModeField_ValueChanged);
			// 
			// _animalModeField
			// 
			resources.ApplyResources(this._animalModeField, "_animalModeField");
			this._animalModeField.Name = "_animalModeField";
			this._animalModeField.NameString = "Animal mode:";
			this._animalModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._animalModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._animalModeField_ValueChanged);
			// 
			// _villagerModeField
			// 
			resources.ApplyResources(this._villagerModeField, "_villagerModeField");
			this._villagerModeField.Name = "_villagerModeField";
			this._villagerModeField.NameString = "Villager mode:";
			this._villagerModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._villagerModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._villagerModeField_ValueChanged);
			// 
			// _sheepConvField
			// 
			resources.ApplyResources(this._sheepConvField, "_sheepConvField");
			this._sheepConvField.Name = "_sheepConvField";
			this._sheepConvField.NameString = "Automatic conversion:";
			this._sheepConvField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._sheepConvField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._sheepConvField_ValueChanged);
			// 
			// _trackUnitDensityField
			// 
			resources.ApplyResources(this._trackUnitDensityField, "_trackUnitDensityField");
			this._trackUnitDensityField.Name = "_trackUnitDensityField";
			this._trackUnitDensityField.NameString = "Track unit distance:";
			this._trackUnitDensityField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._trackUnitDensityField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._trackUnitDensityField_ValueChanged);
			// 
			// _civilizationField
			// 
			resources.ApplyResources(this._civilizationField, "_civilizationField");
			this._civilizationField.Name = "_civilizationField";
			this._civilizationField.NameString = "Culture ID:";
			this._civilizationField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._civilizationField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._civilizationField_ValueChanged);
			// 
			// _hpBar2Field
			// 
			resources.ApplyResources(this._hpBar2Field, "_hpBar2Field");
			this._hpBar2Field.Name = "_hpBar2Field";
			this._hpBar2Field.NameString = "HP bar position 2:";
			this._hpBar2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._hpBar2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._hpBar2Field_ValueChanged);
			// 
			// _hpBar1Field
			// 
			resources.ApplyResources(this._hpBar1Field, "_hpBar1Field");
			this._hpBar1Field.Name = "_hpBar1Field";
			this._hpBar1Field.NameString = "HP bar position 1:";
			this._hpBar1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._hpBar1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._hpBar1Field_ValueChanged);
			// 
			// _disappearsField
			// 
			resources.ApplyResources(this._disappearsField, "_disappearsField");
			this._disappearsField.Name = "_disappearsField";
			this._disappearsField.NameString = "Disappears after completion";
			this._disappearsField.Value = false;
			this._disappearsField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._disappearsField_ValueChanged);
			// 
			// _graphicAngleField
			// 
			resources.ApplyResources(this._graphicAngleField, "_graphicAngleField");
			this._graphicAngleField.Name = "_graphicAngleField";
			this._graphicAngleField.NameString = "Graphic angle:";
			this._graphicAngleField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._graphicAngleField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._graphicAngleField_ValueChanged);
			// 
			// _adjacentModeField
			// 
			resources.ApplyResources(this._adjacentModeField, "_adjacentModeField");
			this._adjacentModeField.Name = "_adjacentModeField";
			this._adjacentModeField.NameString = "Adjacent building part";
			this._adjacentModeField.Value = false;
			this._adjacentModeField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._adjacentModeField_ValueChanged);
			// 
			// _placementModeField
			// 
			resources.ApplyResources(this._placementModeField, "_placementModeField");
			this._placementModeField.Name = "_placementModeField";
			this._placementModeField.NameString = "Placement mode:";
			this._placementModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._placementModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._placementModeField_ValueChanged);
			// 
			// _enabledField
			// 
			resources.ApplyResources(this._enabledField, "_enabledField");
			this._enabledField.Name = "_enabledField";
			this._enabledField.NameString = "Enabled per default";
			this._enabledField.Value = false;
			this._enabledField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._enabledField_ValueChanged);
			// 
			// _deadModeField
			// 
			resources.ApplyResources(this._deadModeField, "_deadModeField");
			this._deadModeField.Name = "_deadModeField";
			this._deadModeField.NameString = "Dead unit:";
			this._deadModeField.Value = false;
			this._deadModeField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._deadModeField_ValueChanged);
			// 
			// _behaviourTabPage
			// 
			resources.ApplyResources(this._behaviourTabPage, "_behaviourTabPage");
			this._behaviourTabPage.Controls.Add(this._edibleMeatField);
			this._behaviourTabPage.Controls.Add(this._towerModeField);
			this._behaviourTabPage.Controls.Add(this._unselectableField);
			this._behaviourTabPage.Controls.Add(this._flyModeField);
			this._behaviourTabPage.Controls.Add(this._airModeField);
			this._behaviourTabPage.Controls.Add(this._interfaceModeField);
			this._behaviourTabPage.Controls.Add(this._behaviourModeField);
			this._behaviourTabPage.Controls.Add(this._hillModeField);
			this._behaviourTabPage.Controls.Add(this._minimapModeField);
			this._behaviourTabPage.Controls.Add(this._interactionModeField);
			this._behaviourTabPage.Controls.Add(this._fogOfWarField);
			this._behaviourTabPage.Controls.Add(this._minimapColorField);
			this._behaviourTabPage.Controls.Add(this._resourceDecayField);
			this._behaviourTabPage.Name = "_behaviourTabPage";
			this._behaviourTabPage.UseVisualStyleBackColor = true;
			// 
			// _edibleMeatField
			// 
			resources.ApplyResources(this._edibleMeatField, "_edibleMeatField");
			this._edibleMeatField.Name = "_edibleMeatField";
			this._edibleMeatField.NameString = "Contains meat";
			this._edibleMeatField.Value = false;
			this._edibleMeatField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._edibleMeatField_ValueChanged);
			// 
			// _towerModeField
			// 
			resources.ApplyResources(this._towerModeField, "_towerModeField");
			this._towerModeField.Name = "_towerModeField";
			this._towerModeField.NameString = "Tower mode:";
			this._towerModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._towerModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._towerModeField_ValueChanged);
			// 
			// _unselectableField
			// 
			resources.ApplyResources(this._unselectableField, "_unselectableField");
			this._unselectableField.Name = "_unselectableField";
			this._unselectableField.NameString = "Not selectable";
			this._unselectableField.Value = false;
			this._unselectableField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._unselectableField_ValueChanged);
			// 
			// _flyModeField
			// 
			resources.ApplyResources(this._flyModeField, "_flyModeField");
			this._flyModeField.Name = "_flyModeField";
			this._flyModeField.NameString = "Fly mode";
			this._flyModeField.Value = false;
			this._flyModeField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._flyModeField_ValueChanged);
			// 
			// _airModeField
			// 
			resources.ApplyResources(this._airModeField, "_airModeField");
			this._airModeField.Name = "_airModeField";
			this._airModeField.NameString = "No footprints";
			this._airModeField.Value = false;
			this._airModeField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._airModeField_ValueChanged);
			// 
			// _interfaceModeField
			// 
			resources.ApplyResources(this._interfaceModeField, "_interfaceModeField");
			this._interfaceModeField.Name = "_interfaceModeField";
			this._interfaceModeField.NameString = "Interface mode:";
			this._interfaceModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._interfaceModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._interfaceModeField_ValueChanged);
			// 
			// _behaviourModeField
			// 
			resources.ApplyResources(this._behaviourModeField, "_behaviourModeField");
			this._behaviourModeField.Name = "_behaviourModeField";
			this._behaviourModeField.NameString = "Behaviour mode:";
			this._behaviourModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._behaviourModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._behaviourModeField_ValueChanged);
			// 
			// _hillModeField
			// 
			resources.ApplyResources(this._hillModeField, "_hillModeField");
			this._hillModeField.Name = "_hillModeField";
			this._hillModeField.NameString = "Hill mode:";
			this._hillModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._hillModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._hillModeField_ValueChanged);
			// 
			// _minimapModeField
			// 
			resources.ApplyResources(this._minimapModeField, "_minimapModeField");
			this._minimapModeField.Name = "_minimapModeField";
			this._minimapModeField.NameString = "Minimap mode:";
			this._minimapModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._minimapModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._minimapModeField_ValueChanged);
			// 
			// _interactionModeField
			// 
			resources.ApplyResources(this._interactionModeField, "_interactionModeField");
			this._interactionModeField.Name = "_interactionModeField";
			this._interactionModeField.NameString = "Interaction mode:";
			this._interactionModeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._interactionModeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._interactionModeField_ValueChanged);
			// 
			// _fogOfWarField
			// 
			resources.ApplyResources(this._fogOfWarField, "_fogOfWarField");
			this._fogOfWarField.Name = "_fogOfWarField";
			this._fogOfWarField.NameString = "Fog of war mode:";
			this._fogOfWarField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._fogOfWarField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._fogOfWarField_ValueChanged);
			// 
			// _minimapColorField
			// 
			resources.ApplyResources(this._minimapColorField, "_minimapColorField");
			this._minimapColorField.Name = "_minimapColorField";
			this._minimapColorField.NameString = "Minimap color:";
			this._minimapColorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._minimapColorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._minimapColorField_ValueChanged);
			// 
			// _resourceDecayField
			// 
			resources.ApplyResources(this._resourceDecayField, "_resourceDecayField");
			this._resourceDecayField.Name = "_resourceDecayField";
			this._resourceDecayField.NameString = "Resource decay:";
			this._resourceDecayField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._resourceDecayField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._resourceDecayField_ValueChanged);
			// 
			// _graphicsTabPage
			// 
			resources.ApplyResources(this._graphicsTabPage, "_graphicsTabPage");
			this._graphicsTabPage.Controls.Add(this.groupBox14);
			this._graphicsTabPage.Controls.Add(this._iconIDField);
			this._graphicsTabPage.Controls.Add(this._graAttackField);
			this._graphicsTabPage.Controls.Add(this._graSnowField);
			this._graphicsTabPage.Controls.Add(this._graConstructionField);
			this._graphicsTabPage.Controls.Add(this._graChargeField);
			this._graphicsTabPage.Controls.Add(this._graGarrisonField);
			this._graphicsTabPage.Controls.Add(this._graMoving2Field);
			this._graphicsTabPage.Controls.Add(this._graMoving1Field);
			this._graphicsTabPage.Controls.Add(this._graStanding2Field);
			this._graphicsTabPage.Controls.Add(this._graFalling2Field);
			this._graphicsTabPage.Controls.Add(this._graFalling1Field);
			this._graphicsTabPage.Controls.Add(this._graStanding1Field);
			this._graphicsTabPage.Name = "_graphicsTabPage";
			this._graphicsTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox14
			// 
			resources.ApplyResources(this.groupBox14, "groupBox14");
			this.groupBox14.Controls.Add(this._dmgGraphicsField);
			this.groupBox14.Name = "groupBox14";
			this.groupBox14.TabStop = false;
			// 
			// _dmgGraphicsField
			// 
			resources.ApplyResources(this._dmgGraphicsField, "_dmgGraphicsField");
			this._dmgGraphicsField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dmgGraphicsField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._dmgGraPercentColumn,
            this._dmgGraIDColumn,
            this._dmgGraApplyModeColumn,
            this._dmgGraUnknownColumn});
			this._dmgGraphicsField.Name = "_dmgGraphicsField";
			this._dmgGraphicsField.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._dmgGraphicsField_CellValidating);
			this._dmgGraphicsField.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._dmgGraphicsField_CellValueChanged);
			this._dmgGraphicsField.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this._dmgGraphicsField_RowsRemoved);
			// 
			// _iconIDField
			// 
			resources.ApplyResources(this._iconIDField, "_iconIDField");
			this._iconIDField.Name = "_iconIDField";
			this._iconIDField.NameString = "Icon ID:";
			this._iconIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._iconIDField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._iconIDField_ValueChanged);
			// 
			// _graAttackField
			// 
			resources.ApplyResources(this._graAttackField, "_graAttackField");
			this._graAttackField.Name = "_graAttackField";
			this._graAttackField.NameString = "Attacking 1:";
			this._graAttackField.Value = null;
			this._graAttackField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graAttackField_ValueChanged);
			// 
			// _graSnowField
			// 
			resources.ApplyResources(this._graSnowField, "_graSnowField");
			this._graSnowField.Name = "_graSnowField";
			this._graSnowField.NameString = "Snow:";
			this._graSnowField.Value = null;
			this._graSnowField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graSnowField_ValueChanged);
			// 
			// _graConstructionField
			// 
			resources.ApplyResources(this._graConstructionField, "_graConstructionField");
			this._graConstructionField.Name = "_graConstructionField";
			this._graConstructionField.NameString = "Building:";
			this._graConstructionField.Value = null;
			this._graConstructionField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graConstructionField_ValueChanged);
			// 
			// _graChargeField
			// 
			resources.ApplyResources(this._graChargeField, "_graChargeField");
			this._graChargeField.Name = "_graChargeField";
			this._graChargeField.NameString = "Charging:";
			this._graChargeField.Value = null;
			this._graChargeField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graChargeField_ValueChanged);
			// 
			// _graGarrisonField
			// 
			resources.ApplyResources(this._graGarrisonField, "_graGarrisonField");
			this._graGarrisonField.Name = "_graGarrisonField";
			this._graGarrisonField.NameString = "Garrison:";
			this._graGarrisonField.Value = null;
			this._graGarrisonField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graGarrisonField_ValueChanged);
			// 
			// _graMoving2Field
			// 
			resources.ApplyResources(this._graMoving2Field, "_graMoving2Field");
			this._graMoving2Field.Name = "_graMoving2Field";
			this._graMoving2Field.NameString = "Walking 2:";
			this._graMoving2Field.Value = null;
			this._graMoving2Field.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graMoving2Field_ValueChanged);
			// 
			// _graMoving1Field
			// 
			resources.ApplyResources(this._graMoving1Field, "_graMoving1Field");
			this._graMoving1Field.Name = "_graMoving1Field";
			this._graMoving1Field.NameString = "Walking 1:";
			this._graMoving1Field.Value = null;
			this._graMoving1Field.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graMoving1Field_ValueChanged);
			// 
			// _graStanding2Field
			// 
			resources.ApplyResources(this._graStanding2Field, "_graStanding2Field");
			this._graStanding2Field.Name = "_graStanding2Field";
			this._graStanding2Field.NameString = "Standing 2:";
			this._graStanding2Field.Value = null;
			this._graStanding2Field.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graStanding2Field_ValueChanged);
			// 
			// _graFalling2Field
			// 
			resources.ApplyResources(this._graFalling2Field, "_graFalling2Field");
			this._graFalling2Field.Name = "_graFalling2Field";
			this._graFalling2Field.NameString = "Dying 2:";
			this._graFalling2Field.Value = null;
			this._graFalling2Field.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graFalling2Field_ValueChanged);
			// 
			// _graFalling1Field
			// 
			resources.ApplyResources(this._graFalling1Field, "_graFalling1Field");
			this._graFalling1Field.Name = "_graFalling1Field";
			this._graFalling1Field.NameString = "Dying 1:";
			this._graFalling1Field.Value = null;
			this._graFalling1Field.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graFalling1Field_ValueChanged);
			// 
			// _graStanding1Field
			// 
			resources.ApplyResources(this._graStanding1Field, "_graStanding1Field");
			this._graStanding1Field.Name = "_graStanding1Field";
			this._graStanding1Field.NameString = "Standing 1:";
			this._graStanding1Field.Value = null;
			this._graStanding1Field.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._graStanding1Field_ValueChanged);
			// 
			// _soundsTabPage
			// 
			resources.ApplyResources(this._soundsTabPage, "_soundsTabPage");
			this._soundsTabPage.Controls.Add(this._unknownSoundField);
			this._soundsTabPage.Controls.Add(this._constructionSoundField);
			this._soundsTabPage.Controls.Add(this._soundMoveField);
			this._soundsTabPage.Controls.Add(this._soundAttackField);
			this._soundsTabPage.Controls.Add(this._soundFallingField);
			this._soundsTabPage.Controls.Add(this._soundTrain2Field);
			this._soundsTabPage.Controls.Add(this._soundTrain1Field);
			this._soundsTabPage.Controls.Add(this._soundSelectionField);
			this._soundsTabPage.Name = "_soundsTabPage";
			this._soundsTabPage.UseVisualStyleBackColor = true;
			// 
			// _unknownSoundField
			// 
			resources.ApplyResources(this._unknownSoundField, "_unknownSoundField");
			this._unknownSoundField.Name = "_unknownSoundField";
			this._unknownSoundField.NameString = "Unknown:";
			this._unknownSoundField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknownSoundField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknownSoundField_ValueChanged);
			// 
			// _constructionSoundField
			// 
			resources.ApplyResources(this._constructionSoundField, "_constructionSoundField");
			this._constructionSoundField.Name = "_constructionSoundField";
			this._constructionSoundField.NameString = "Completion:";
			this._constructionSoundField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._constructionSoundField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._constructionSoundField_ValueChanged);
			// 
			// _soundMoveField
			// 
			resources.ApplyResources(this._soundMoveField, "_soundMoveField");
			this._soundMoveField.Name = "_soundMoveField";
			this._soundMoveField.NameString = "Walking:";
			this._soundMoveField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundMoveField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundMoveField_ValueChanged);
			// 
			// _soundAttackField
			// 
			resources.ApplyResources(this._soundAttackField, "_soundAttackField");
			this._soundAttackField.Name = "_soundAttackField";
			this._soundAttackField.NameString = "Attacking:";
			this._soundAttackField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundAttackField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundAttackField_ValueChanged);
			// 
			// _soundFallingField
			// 
			resources.ApplyResources(this._soundFallingField, "_soundFallingField");
			this._soundFallingField.Name = "_soundFallingField";
			this._soundFallingField.NameString = "Dying:";
			this._soundFallingField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundFallingField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundFallingField_ValueChanged);
			// 
			// _soundTrain2Field
			// 
			resources.ApplyResources(this._soundTrain2Field, "_soundTrain2Field");
			this._soundTrain2Field.Name = "_soundTrain2Field";
			this._soundTrain2Field.NameString = "Creation 2:";
			this._soundTrain2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundTrain2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundTrain2Field_ValueChanged);
			// 
			// _soundTrain1Field
			// 
			resources.ApplyResources(this._soundTrain1Field, "_soundTrain1Field");
			this._soundTrain1Field.Name = "_soundTrain1Field";
			this._soundTrain1Field.NameString = "Creation 1:";
			this._soundTrain1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundTrain1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundTrain1Field_ValueChanged);
			// 
			// _soundSelectionField
			// 
			resources.ApplyResources(this._soundSelectionField, "_soundSelectionField");
			this._soundSelectionField.Name = "_soundSelectionField";
			this._soundSelectionField.NameString = "Selection:";
			this._soundSelectionField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundSelectionField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundSelectionField_ValueChanged);
			// 
			// _unknownTabPage
			// 
			resources.ApplyResources(this._unknownTabPage, "_unknownTabPage");
			this._unknownTabPage.Controls.Add(this._unknown10Field);
			this._unknownTabPage.Controls.Add(this._unknown15Field);
			this._unknownTabPage.Controls.Add(this._unknown14Field);
			this._unknownTabPage.Controls.Add(this._unknown12Field);
			this._unknownTabPage.Controls.Add(this._unknown13Field);
			this._unknownTabPage.Controls.Add(this._unknown11Field);
			this._unknownTabPage.Controls.Add(this._unknown9EField);
			this._unknownTabPage.Controls.Add(this._unknown9DField);
			this._unknownTabPage.Controls.Add(this._unknown9CField);
			this._unknownTabPage.Controls.Add(this._unknown9BField);
			this._unknownTabPage.Controls.Add(this._unknown9AField);
			this._unknownTabPage.Controls.Add(this._unknown8Field);
			this._unknownTabPage.Controls.Add(this._unknown7Field);
			this._unknownTabPage.Controls.Add(this._unknown6Field);
			this._unknownTabPage.Controls.Add(this._unknown4Field);
			this._unknownTabPage.Controls.Add(this._unknown2Field);
			this._unknownTabPage.Controls.Add(this._unknown5Field);
			this._unknownTabPage.Controls.Add(this._unknown3Field);
			this._unknownTabPage.Controls.Add(this._unknown1Field);
			this._unknownTabPage.Name = "_unknownTabPage";
			this._unknownTabPage.UseVisualStyleBackColor = true;
			// 
			// _unknown10Field
			// 
			resources.ApplyResources(this._unknown10Field, "_unknown10Field");
			this._unknown10Field.Name = "_unknown10Field";
			this._unknown10Field.NameString = "10:";
			this._unknown10Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown10Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown10Field_ValueChanged);
			// 
			// _unknown15Field
			// 
			resources.ApplyResources(this._unknown15Field, "_unknown15Field");
			this._unknown15Field.Name = "_unknown15Field";
			this._unknown15Field.NameString = "15:";
			this._unknown15Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown15Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown15Field_ValueChanged);
			// 
			// _unknown14Field
			// 
			resources.ApplyResources(this._unknown14Field, "_unknown14Field");
			this._unknown14Field.Name = "_unknown14Field";
			this._unknown14Field.NameString = "14:";
			this._unknown14Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown14Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown14Field_ValueChanged);
			// 
			// _unknown12Field
			// 
			resources.ApplyResources(this._unknown12Field, "_unknown12Field");
			this._unknown12Field.Name = "_unknown12Field";
			this._unknown12Field.NameString = "12:";
			this._unknown12Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown12Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown12Field_ValueChanged);
			// 
			// _unknown13Field
			// 
			resources.ApplyResources(this._unknown13Field, "_unknown13Field");
			this._unknown13Field.Name = "_unknown13Field";
			this._unknown13Field.NameString = "13:";
			this._unknown13Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown13Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown13Field_ValueChanged);
			// 
			// _unknown11Field
			// 
			resources.ApplyResources(this._unknown11Field, "_unknown11Field");
			this._unknown11Field.Name = "_unknown11Field";
			this._unknown11Field.NameString = "11:";
			this._unknown11Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown11Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown11Field_ValueChanged);
			// 
			// _unknown9EField
			// 
			resources.ApplyResources(this._unknown9EField, "_unknown9EField");
			this._unknown9EField.Name = "_unknown9EField";
			this._unknown9EField.NameString = "9e:";
			this._unknown9EField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown9EField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown9EField_ValueChanged);
			// 
			// _unknown9DField
			// 
			resources.ApplyResources(this._unknown9DField, "_unknown9DField");
			this._unknown9DField.Name = "_unknown9DField";
			this._unknown9DField.NameString = "9d:";
			this._unknown9DField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown9DField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown9DField_ValueChanged);
			// 
			// _unknown9CField
			// 
			resources.ApplyResources(this._unknown9CField, "_unknown9CField");
			this._unknown9CField.Name = "_unknown9CField";
			this._unknown9CField.NameString = "9c:";
			this._unknown9CField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown9CField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown9CField_ValueChanged);
			// 
			// _unknown9BField
			// 
			resources.ApplyResources(this._unknown9BField, "_unknown9BField");
			this._unknown9BField.Name = "_unknown9BField";
			this._unknown9BField.NameString = "9b:";
			this._unknown9BField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown9BField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown9BField_ValueChanged);
			// 
			// _unknown9AField
			// 
			resources.ApplyResources(this._unknown9AField, "_unknown9AField");
			this._unknown9AField.Name = "_unknown9AField";
			this._unknown9AField.NameString = "9a:";
			this._unknown9AField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown9AField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown9AField_ValueChanged);
			// 
			// _unknown8Field
			// 
			resources.ApplyResources(this._unknown8Field, "_unknown8Field");
			this._unknown8Field.Name = "_unknown8Field";
			this._unknown8Field.NameString = "8:";
			this._unknown8Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown8Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown8Field_ValueChanged);
			// 
			// _unknown7Field
			// 
			resources.ApplyResources(this._unknown7Field, "_unknown7Field");
			this._unknown7Field.Name = "_unknown7Field";
			this._unknown7Field.NameString = "7:";
			this._unknown7Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown7Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown7Field_ValueChanged);
			// 
			// _unknown6Field
			// 
			resources.ApplyResources(this._unknown6Field, "_unknown6Field");
			this._unknown6Field.Name = "_unknown6Field";
			this._unknown6Field.NameString = "6:";
			this._unknown6Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown6Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown6Field_ValueChanged);
			// 
			// _unknown4Field
			// 
			resources.ApplyResources(this._unknown4Field, "_unknown4Field");
			this._unknown4Field.Name = "_unknown4Field";
			this._unknown4Field.NameString = "4:";
			this._unknown4Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown4Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown4Field_ValueChanged);
			// 
			// _unknown2Field
			// 
			resources.ApplyResources(this._unknown2Field, "_unknown2Field");
			this._unknown2Field.Name = "_unknown2Field";
			this._unknown2Field.NameString = "2:";
			this._unknown2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown2Field_ValueChanged);
			// 
			// _unknown5Field
			// 
			resources.ApplyResources(this._unknown5Field, "_unknown5Field");
			this._unknown5Field.Name = "_unknown5Field";
			this._unknown5Field.NameString = "5:";
			this._unknown5Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown5Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown5Field_ValueChanged);
			// 
			// _unknown3Field
			// 
			resources.ApplyResources(this._unknown3Field, "_unknown3Field");
			this._unknown3Field.Name = "_unknown3Field";
			this._unknown3Field.NameString = "3:";
			this._unknown3Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown3Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown3Field_ValueChanged);
			// 
			// _unknown1Field
			// 
			resources.ApplyResources(this._unknown1Field, "_unknown1Field");
			this._unknown1Field.Name = "_unknown1Field";
			this._unknown1Field.NameString = "1:";
			this._unknown1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown1Field_ValueChanged);
			// 
			// _attValClassColumn
			// 
			this._attValClassColumn.FillWeight = 80F;
			resources.ApplyResources(this._attValClassColumn, "_attValClassColumn");
			this._attValClassColumn.Name = "_attValClassColumn";
			// 
			// _attValValueColumn
			// 
			this._attValValueColumn.FillWeight = 20F;
			resources.ApplyResources(this._attValValueColumn, "_attValValueColumn");
			this._attValValueColumn.Name = "_attValValueColumn";
			// 
			// _armValClassColumn
			// 
			this._armValClassColumn.FillWeight = 80F;
			resources.ApplyResources(this._armValClassColumn, "_armValClassColumn");
			this._armValClassColumn.Name = "_armValClassColumn";
			// 
			// _armValValueColumn
			// 
			this._armValValueColumn.FillWeight = 20F;
			resources.ApplyResources(this._armValValueColumn, "_armValValueColumn");
			this._armValValueColumn.Name = "_armValValueColumn";
			// 
			// _dmgGraPercentColumn
			// 
			this._dmgGraPercentColumn.FillWeight = 20F;
			resources.ApplyResources(this._dmgGraPercentColumn, "_dmgGraPercentColumn");
			this._dmgGraPercentColumn.Name = "_dmgGraPercentColumn";
			// 
			// _dmgGraIDColumn
			// 
			this._dmgGraIDColumn.FillWeight = 50F;
			resources.ApplyResources(this._dmgGraIDColumn, "_dmgGraIDColumn");
			this._dmgGraIDColumn.Name = "_dmgGraIDColumn";
			// 
			// _dmgGraApplyModeColumn
			// 
			this._dmgGraApplyModeColumn.FillWeight = 20F;
			resources.ApplyResources(this._dmgGraApplyModeColumn, "_dmgGraApplyModeColumn");
			this._dmgGraApplyModeColumn.Name = "_dmgGraApplyModeColumn";
			// 
			// _dmgGraUnknownColumn
			// 
			this._dmgGraUnknownColumn.FillWeight = 10F;
			resources.ApplyResources(this._dmgGraUnknownColumn, "_dmgGraUnknownColumn");
			this._dmgGraUnknownColumn.Name = "_dmgGraUnknownColumn";
			// 
			// EditUnitAttributeForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._mainTabControl);
			this.Controls.Add(this._bottomPanel);
			this.Name = "EditUnitAttributeForm";
			this.ResizeBegin += new System.EventHandler(this.EditUnitAttributeForm_ResizeBegin);
			this.ResizeEnd += new System.EventHandler(this.EditUnitAttributeForm_ResizeEnd);
			this._bottomPanel.ResumeLayout(false);
			this._mainTabControl.ResumeLayout(false);
			this._commonTabPage.ResumeLayout(false);
			this._commonTabPage.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this._abilitiesTabPage.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this._statsTabPage.ResumeLayout(false);
			this._combatTabPage.ResumeLayout(false);
			this.groupBox22.ResumeLayout(false);
			this.groupBox20.ResumeLayout(false);
			this.groupBox19.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._armourValuesField)).EndInit();
			this.groupBox18.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._attackValuesField)).EndInit();
			this._projectileTabPage.ResumeLayout(false);
			this._trainTabPage.ResumeLayout(false);
			this.groupBox25.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this._editorTabPage.ResumeLayout(false);
			this._editorTabPage.PerformLayout();
			this.groupBox33.ResumeLayout(false);
			this.groupBox13.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this._behaviourTabPage.ResumeLayout(false);
			this._graphicsTabPage.ResumeLayout(false);
			this.groupBox14.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._dmgGraphicsField)).EndInit();
			this._soundsTabPage.ResumeLayout(false);
			this._unknownTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel _bottomPanel;
		private System.Windows.Forms.Button _closeButton;
		private System.Windows.Forms.TabControl _mainTabControl;
		private Controls.DropDownFieldControl _graFalling2Field;
		private Controls.DropDownFieldControl _graStanding2Field;
		private Controls.DropDownFieldControl _graFalling1Field;
		private Controls.DropDownFieldControl _graStanding1Field;
		private System.Windows.Forms.TabPage _commonTabPage;
		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.TextBox _nameTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private Controls.LanguageDLLControl _dllHotkeyField;
		private Controls.LanguageDLLControl _dllDescriptionField;
		private Controls.LanguageDLLControl _dllHelpField;
		private Controls.LanguageDLLControl _dllNameField;
		private System.Windows.Forms.TabPage _combatTabPage;
		private Controls.NumberFieldControl _terrainMultField;
		private Controls.NumberFieldControl _blastLevelField;
		private Controls.NumberFieldControl _blastRadiusField;
		private System.Windows.Forms.GroupBox groupBox22;
		private Controls.NumberFieldControl _attackReloadTimeDisplayedField;
		private Controls.NumberFieldControl _attackReloadTimeField;
		private System.Windows.Forms.GroupBox groupBox20;
		private Controls.NumberFieldControl _rangeDisplayedField;
		private Controls.NumberFieldControl _rangeMaxField;
		private Controls.NumberFieldControl _rangeMinField;
		private System.Windows.Forms.GroupBox groupBox19;
		private Controls.NumberFieldControl _defaultArmorField;
		private System.Windows.Forms.DataGridView _armourValuesField;
		private Controls.NumberFieldControl _displayedMeleeArmourField;
		private System.Windows.Forms.GroupBox groupBox18;
		private System.Windows.Forms.DataGridView _attackValuesField;
		private Controls.NumberFieldControl _displayedAttackField;
		private Controls.NumberFieldControl _blastTypeField;
		private Controls.NumberFieldControl _hitpointsField;
		private System.Windows.Forms.TabPage _graphicsTabPage;
		private Controls.DropDownFieldControl _graAttackField;
		private Controls.DropDownFieldControl _graSnowField;
		private Controls.DropDownFieldControl _graConstructionField;
		private Controls.DropDownFieldControl _graChargeField;
		private Controls.DropDownFieldControl _graGarrisonField;
		private Controls.DropDownFieldControl _graMoving2Field;
		private Controls.DropDownFieldControl _graMoving1Field;
		private Controls.NumberFieldControl _iconIDField;
		private System.Windows.Forms.TabPage _projectileTabPage;
		private Controls.NumberFieldControl _attackModeField;
		private System.Windows.Forms.GroupBox groupBox14;
		private System.Windows.Forms.DataGridView _dmgGraphicsField;
		private Controls.NumberFieldControl _missileSpawnZField;
		private Controls.NumberFieldControl _missileSpawnYField;
		private Controls.NumberFieldControl _missileSpawnXField;
		private Controls.NumberFieldControl _missileDuplMaxField;
		private Controls.NumberFieldControl _missileDuplMinField;
		private Controls.NumberFieldControl _projectileArcField;
		private Controls.NumberFieldControl _penetrationModeField;
		private Controls.NumberFieldControl _dropAnimationModeField;
		private Controls.NumberFieldControl _compensationModeField;
		private Controls.NumberFieldControl _stretchModeField;
		private System.Windows.Forms.TabPage _editorTabPage;
		private System.Windows.Forms.GroupBox groupBox33;
		private Controls.CheckBoxFieldControl _lootStoneField;
		private Controls.CheckBoxFieldControl _lootGoldField;
		private Controls.CheckBoxFieldControl _lootFoodField;
		private Controls.CheckBoxFieldControl _lootWoodField;
		private Controls.NumberFieldControl _garrisonTypeField;
		private Controls.NumberFieldControl _chargeModeField;
		private Controls.NumberFieldControl _heroModeField;
		private Controls.NumberFieldControl _animalModeField;
		private Controls.NumberFieldControl _villagerModeField;
		private Controls.NumberFieldControl _sheepConvField;
		private Controls.NumberFieldControl _trackUnitDensityField;
		private Controls.NumberFieldControl _civilizationField;
		private Controls.NumberFieldControl _hpBar2Field;
		private Controls.NumberFieldControl _hpBar1Field;
		private System.Windows.Forms.GroupBox groupBox13;
		private Controls.NumberFieldControl _selEditorColorField;
		private Controls.NumberFieldControl _selShapeField;
		private Controls.NumberFieldControl _selShapeTypeField;
		private Controls.NumberFieldControl _selEffectField;
		private Controls.NumberFieldControl _selMaskField;
		private Controls.CheckBoxFieldControl _disappearsField;
		private Controls.NumberFieldControl _foundTerrainField;
		private Controls.NumberFieldControl _graphicAngleField;
		private Controls.CheckBoxFieldControl _adjacentModeField;
		private Controls.NumberFieldControl _placementModeField;
		private Controls.CheckBoxFieldControl _enabledField;
		private Controls.CheckBoxFieldControl _deadModeField;
		private System.Windows.Forms.GroupBox groupBox9;
		private Controls.NumberFieldControl _terrRestrField;
		private Controls.NumberFieldControl _terrSide2Field;
		private Controls.NumberFieldControl _terrSide1Field;
		private Controls.NumberFieldControl _terrPlacement2Field;
		private Controls.NumberFieldControl _terrPlacement1Field;
		private System.Windows.Forms.GroupBox groupBox5;
		private Controls.NumberFieldControl _selectionYField;
		private Controls.NumberFieldControl _selectionXField;
		private Controls.NumberFieldControl _editorYField;
		private Controls.NumberFieldControl _editorXField;
		private Controls.NumberFieldControl _sizeYField;
		private Controls.NumberFieldControl _sizeXField;
		private System.Windows.Forms.TabPage _behaviourTabPage;
		private Controls.CheckBoxFieldControl _unselectableField;
		private Controls.CheckBoxFieldControl _flyModeField;
		private Controls.CheckBoxFieldControl _airModeField;
		private Controls.NumberFieldControl _interfaceModeField;
		private Controls.NumberFieldControl _behaviourModeField;
		private Controls.NumberFieldControl _hillModeField;
		private Controls.NumberFieldControl _minimapModeField;
		private Controls.NumberFieldControl _interactionModeField;
		private Controls.NumberFieldControl _fogOfWarField;
		private Controls.NumberFieldControl _minimapColorField;
		private Controls.NumberFieldControl _resourceDecayField;
		private Controls.NumberFieldControl _displayedPierceArmorField;
		private System.Windows.Forms.TabPage _statsTabPage;
		private Controls.NumberFieldControl _garrisonHealRateField;
		private Controls.NumberFieldControl _workRateField;
		private Controls.NumberFieldControl _searchRadiusField;
		private Controls.NumberFieldControl _rotationSpeedField;
		private Controls.NumberFieldControl _speedField;
		private Controls.NumberFieldControl _losField;
		private Controls.NumberFieldControl _resourceCapacityField;
		private Controls.NumberFieldControl _garrisonCapacityField;
		private System.Windows.Forms.TabPage _soundsTabPage;
		private Controls.NumberFieldControl _unknownSoundField;
		private Controls.NumberFieldControl _constructionSoundField;
		private Controls.NumberFieldControl _soundMoveField;
		private Controls.NumberFieldControl _soundAttackField;
		private Controls.NumberFieldControl _soundFallingField;
		private Controls.NumberFieldControl _soundTrain2Field;
		private Controls.NumberFieldControl _soundTrain1Field;
		private Controls.NumberFieldControl _soundSelectionField;
		private Controls.NumberFieldControl _graphicDisplacementZField;
		private Controls.NumberFieldControl _graphicDisplacementYField;
		private Controls.NumberFieldControl _graphicDisplacementXField;
		private Controls.NumberFieldControl _frameDelayField;
		private Controls.NumberFieldControl _accuracyErrorField;
		private Controls.NumberFieldControl _accuracyPercentField;
		private Controls.NumberFieldControl _towerModeField;
		private System.Windows.Forms.TabPage _trainTabPage;
		private Controls.NumberFieldControl _trainTimeField;
		private System.Windows.Forms.GroupBox groupBox25;
		private Controls.ResourceCostControl _cost3Field;
		private Controls.ResourceCostControl _cost2Field;
		private Controls.ResourceCostControl _cost1Field;
		private System.Windows.Forms.GroupBox groupBox6;
		private Controls.ResourceCostControl _resourceStorage3Field;
		private Controls.ResourceCostControl _resourceStorage2Field;
		private Controls.ResourceCostControl _resourceStorage1Field;
		private System.Windows.Forms.TabPage _unknownTabPage;
		private Controls.NumberFieldControl _unknown10Field;
		private Controls.NumberFieldControl _unknown15Field;
		private Controls.NumberFieldControl _unknown14Field;
		private Controls.NumberFieldControl _unknown12Field;
		private Controls.NumberFieldControl _unknown13Field;
		private Controls.NumberFieldControl _unknown11Field;
		private Controls.NumberFieldControl _unknown9EField;
		private Controls.NumberFieldControl _unknown9DField;
		private Controls.NumberFieldControl _unknown9CField;
		private Controls.NumberFieldControl _unknown9BField;
		private Controls.NumberFieldControl _unknown9AField;
		private Controls.NumberFieldControl _unknown8Field;
		private Controls.NumberFieldControl _unknown7Field;
		private Controls.NumberFieldControl _unknown6Field;
		private Controls.NumberFieldControl _unknown4Field;
		private Controls.NumberFieldControl _unknown2Field;
		private Controls.NumberFieldControl _unknown5Field;
		private Controls.NumberFieldControl _unknown3Field;
		private Controls.NumberFieldControl _unknown1Field;
		private System.Windows.Forms.Label _classLabel;
		private System.Windows.Forms.ComboBox _classComboBox;
		private Controls.CheckBoxFieldControl _edibleMeatField;
		private Controls.NumberFieldControl _resourceStorage3ModeField;
		private Controls.NumberFieldControl _resourceStorage2ModeField;
		private Controls.NumberFieldControl _resourceStorage1ModeField;
		private System.Windows.Forms.TabPage _abilitiesTabPage;
		private System.Windows.Forms.Label _abilityInfoLabel;
		private DoubleBufferedListBox _abilitiesListBox;
		private System.Windows.Forms.Label _abilityTypeLabel;
		private System.Windows.Forms.ComboBox _abilityTypeComboBox;
		private System.Windows.Forms.Label _abilityResourceLabel;
		private System.Windows.Forms.ComboBox _abilityResourceComboBox;
		private System.Windows.Forms.Label _abilityClassLabel;
		private System.Windows.Forms.ComboBox _abilityClassComboBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button _newAbilityButton;
		private System.Windows.Forms.Button _deleteAbilityButton;
		private System.Windows.Forms.Label _abilityResourceCarryLabel;
		private System.Windows.Forms.ComboBox _abilityResourceCarryComboBox;
		private System.Windows.Forms.Label _abilityResourceDropLabel;
		private System.Windows.Forms.ComboBox _abilityResourceDropComboBox;
		private System.Windows.Forms.Label _abilityResourceProductivityLabel;
		private System.Windows.Forms.ComboBox _abilityResourceProductivityComboBox;
		private Controls.NumberFieldControl _abilityRadiusField;
		private Controls.NumberFieldControl _abilityWorkRateFactorField;
		private Controls.NumberFieldControl _abilityRangeField;
		private Controls.NumberFieldControl _abilityPlunderSourceField;
		private Controls.NumberFieldControl _abilitySelectionEnablerField;
		private Controls.NumberFieldControl _abilitySelectionModeField;
		private Controls.NumberFieldControl _abilityTerrainField;
		private Controls.NumberFieldControl _abilityRightClickModeField;
		private Controls.DropDownFieldControl _abilityToolGraphicField;
		private Controls.DropDownFieldControl _abilityCarryGraphicField;
		private Controls.DropDownFieldControl _abilityActionGraphicField;
		private Controls.DropDownFieldControl _abilityProceedGraphicField;
		private Controls.NumberFieldControl _abilityDropSoundField;
		private Controls.NumberFieldControl _abilityExecutionSoundField;
		private Controls.NumberFieldControl _abilityUnknown6Field;
		private Controls.NumberFieldControl _abilityUnknown5Field;
		private Controls.NumberFieldControl _abilityUnknown4Field;
		private Controls.NumberFieldControl _abilityUnknown3Field;
		private Controls.NumberFieldControl _abilityUnknown2Field;
		private Controls.NumberFieldControl _abilityUnknown1Field;
		private System.Windows.Forms.ComboBox _abilityUnitComboBox;
		private System.Windows.Forms.Label _abilityUnitLabel;
		private System.Windows.Forms.DataGridViewComboBoxColumn _armValClassColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _armValValueColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn _attValClassColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attValValueColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _dmgGraPercentColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _dmgGraIDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _dmgGraApplyModeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _dmgGraUnknownColumn;
	}
}