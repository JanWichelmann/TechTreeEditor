namespace X2AddOnTechTreeEditor
{
	partial class EditResearchAttributeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditResearchAttributeForm));
			this._bottomPanel = new System.Windows.Forms.Panel();
			this._closeButton = new System.Windows.Forms.Button();
			this._mainTabControl = new System.Windows.Forms.TabControl();
			this._mainTabPage = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this._unknown1Field = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._timeField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._cost3Field = new X2AddOnTechTreeEditor.Controls.ResourceCostControl();
			this._cost2Field = new X2AddOnTechTreeEditor.Controls.ResourceCostControl();
			this._cost1Field = new X2AddOnTechTreeEditor.Controls.ResourceCostControl();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._fullTechModeField = new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl();
			this._isAgeField = new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl();
			this._iconIDField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._nameLabel = new System.Windows.Forms.Label();
			this._nameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._dllHelpField = new X2AddOnTechTreeEditor.Controls.LanguageDLLControl();
			this._dllName2Field = new X2AddOnTechTreeEditor.Controls.LanguageDLLControl();
			this._dllDescriptionField = new X2AddOnTechTreeEditor.Controls.LanguageDLLControl();
			this._dllName1Field = new X2AddOnTechTreeEditor.Controls.LanguageDLLControl();
			this._effectTabPage = new System.Windows.Forms.TabPage();
			this._effectsListBox = new X2AddOnTechTreeEditor.DoubleBufferedListBox();
			this._effectOptionsPanel = new System.Windows.Forms.Panel();
			this._effectTypeLabel = new System.Windows.Forms.Label();
			this._effectTypeComboBox = new System.Windows.Forms.ComboBox();
			this._sortEffectsButton = new System.Windows.Forms.Button();
			this._newEffectButton = new System.Windows.Forms.Button();
			this._deleteEffectButton = new System.Windows.Forms.Button();
			this._effectDownButton = new System.Windows.Forms.Button();
			this._effectUpButton = new System.Windows.Forms.Button();
			this._resourceLabel = new System.Windows.Forms.Label();
			this._resourceComboBox = new System.Windows.Forms.ComboBox();
			this._armourClassLabel = new System.Windows.Forms.Label();
			this._armourClassComboBox = new System.Windows.Forms.ComboBox();
			this._attributeLabel = new System.Windows.Forms.Label();
			this._attributeComboBox = new System.Windows.Forms.ComboBox();
			this._classLabel = new System.Windows.Forms.Label();
			this._classComboBox = new System.Windows.Forms.ComboBox();
			this._valueField = new X2AddOnTechTreeEditor.Controls.NumberFieldControl();
			this._modeCheckBox = new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl();
			this._researchField = new X2AddOnTechTreeEditor.Controls.DropDownFieldControl();
			this._destUnitField = new X2AddOnTechTreeEditor.Controls.DropDownFieldControl();
			this._unitField = new X2AddOnTechTreeEditor.Controls.DropDownFieldControl();
			this._bottomPanel.SuspendLayout();
			this._mainTabControl.SuspendLayout();
			this._mainTabPage.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this._effectTabPage.SuspendLayout();
			this._effectOptionsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			this._bottomPanel.Controls.Add(this._closeButton);
			resources.ApplyResources(this._bottomPanel, "_bottomPanel");
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
			this._mainTabControl.Controls.Add(this._mainTabPage);
			this._mainTabControl.Controls.Add(this._effectTabPage);
			resources.ApplyResources(this._mainTabControl, "_mainTabControl");
			this._mainTabControl.Name = "_mainTabControl";
			this._mainTabControl.SelectedIndex = 0;
			// 
			// _mainTabPage
			// 
			this._mainTabPage.Controls.Add(this.groupBox2);
			this._mainTabPage.Controls.Add(this.groupBox4);
			this._mainTabPage.Controls.Add(this.groupBox3);
			this._mainTabPage.Controls.Add(this.groupBox1);
			resources.ApplyResources(this._mainTabPage, "_mainTabPage");
			this._mainTabPage.Name = "_mainTabPage";
			this._mainTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this._unknown1Field);
			this.groupBox2.Controls.Add(this._timeField);
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// _unknown1Field
			// 
			resources.ApplyResources(this._unknown1Field, "_unknown1Field");
			this._unknown1Field.Name = "_unknown1Field";
			this._unknown1Field.NameString = "Unbekannt 1:";
			this._unknown1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown1Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown1Field_ValueChanged);
			// 
			// _timeField
			// 
			resources.ApplyResources(this._timeField, "_timeField");
			this._timeField.Name = "_timeField";
			this._timeField.NameString = "Entwicklungsdauer:";
			this._timeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._timeField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._timeField_ValueChanged);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this._cost3Field);
			this.groupBox4.Controls.Add(this._cost2Field);
			this.groupBox4.Controls.Add(this._cost1Field);
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			// 
			// _cost3Field
			// 
			resources.ApplyResources(this._cost3Field, "_cost3Field");
			this._cost3Field.Name = "_cost3Field";
			this._cost3Field.NameString = "Kosten 3:";
			this._cost3Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._cost3Field_ValueChanged);
			// 
			// _cost2Field
			// 
			resources.ApplyResources(this._cost2Field, "_cost2Field");
			this._cost2Field.Name = "_cost2Field";
			this._cost2Field.NameString = "Kosten 2:";
			this._cost2Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._cost2Field_ValueChanged);
			// 
			// _cost1Field
			// 
			resources.ApplyResources(this._cost1Field, "_cost1Field");
			this._cost1Field.Name = "_cost1Field";
			this._cost1Field.NameString = "Kosten 1:";
			this._cost1Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.ResourceCostControl.ValueChangedEventHandler(this._cost1Field_ValueChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._fullTechModeField);
			this.groupBox3.Controls.Add(this._isAgeField);
			this.groupBox3.Controls.Add(this._iconIDField);
			this.groupBox3.Controls.Add(this._nameLabel);
			this.groupBox3.Controls.Add(this._nameTextBox);
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// _fullTechModeField
			// 
			resources.ApplyResources(this._fullTechModeField, "_fullTechModeField");
			this._fullTechModeField.Name = "_fullTechModeField";
			this._fullTechModeField.NameString = "\"Alle Technologien\"-Modus";
			this._fullTechModeField.Value = false;
			this._fullTechModeField.ValueChanged += new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._fullTechModeField_ValueChanged);
			// 
			// _isAgeField
			// 
			resources.ApplyResources(this._isAgeField, "_isAgeField");
			this._isAgeField.Name = "_isAgeField";
			this._isAgeField.NameString = "Zeitalter-Technologie";
			this._isAgeField.Value = false;
			this._isAgeField.ValueChanged += new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._isAgeField_ValueChanged);
			// 
			// _iconIDField
			// 
			resources.ApplyResources(this._iconIDField, "_iconIDField");
			this._iconIDField.Name = "_iconIDField";
			this._iconIDField.NameString = "Icon-ID:";
			this._iconIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._iconIDField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._iconIDField_ValueChanged);
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
			this.groupBox1.Controls.Add(this._dllHelpField);
			this.groupBox1.Controls.Add(this._dllName2Field);
			this.groupBox1.Controls.Add(this._dllDescriptionField);
			this.groupBox1.Controls.Add(this._dllName1Field);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// _dllHelpField
			// 
			resources.ApplyResources(this._dllHelpField, "_dllHelpField");
			this._dllHelpField.Name = "_dllHelpField";
			this._dllHelpField.NameString = "Hilfe:";
			this._dllHelpField.ProjectFile = null;
			this._dllHelpField.Value = 0;
			this._dllHelpField.ValueChanged += new X2AddOnTechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllHelpField_ValueChanged);
			// 
			// _dllName2Field
			// 
			resources.ApplyResources(this._dllName2Field, "_dllName2Field");
			this._dllName2Field.Name = "_dllName2Field";
			this._dllName2Field.NameString = "Name 2:";
			this._dllName2Field.ProjectFile = null;
			this._dllName2Field.Value = 0;
			this._dllName2Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllName2Field_ValueChanged);
			// 
			// _dllDescriptionField
			// 
			resources.ApplyResources(this._dllDescriptionField, "_dllDescriptionField");
			this._dllDescriptionField.Name = "_dllDescriptionField";
			this._dllDescriptionField.NameString = "Beschreibung:";
			this._dllDescriptionField.ProjectFile = null;
			this._dllDescriptionField.Value = 0;
			this._dllDescriptionField.ValueChanged += new X2AddOnTechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllDescriptionField_ValueChanged);
			// 
			// _dllName1Field
			// 
			resources.ApplyResources(this._dllName1Field, "_dllName1Field");
			this._dllName1Field.Name = "_dllName1Field";
			this._dllName1Field.NameString = "Name 1: ";
			this._dllName1Field.ProjectFile = null;
			this._dllName1Field.Value = 0;
			this._dllName1Field.ValueChanged += new X2AddOnTechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllName1Field_ValueChanged);
			// 
			// _effectTabPage
			// 
			this._effectTabPage.Controls.Add(this._effectsListBox);
			this._effectTabPage.Controls.Add(this._effectOptionsPanel);
			resources.ApplyResources(this._effectTabPage, "_effectTabPage");
			this._effectTabPage.Name = "_effectTabPage";
			this._effectTabPage.UseVisualStyleBackColor = true;
			// 
			// _effectsListBox
			// 
			resources.ApplyResources(this._effectsListBox, "_effectsListBox");
			this._effectsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this._effectsListBox.Name = "_effectsListBox";
			this._effectsListBox.SelectedIndexChanged += new System.EventHandler(this._effectsListBox_SelectedIndexChanged);
			// 
			// _effectOptionsPanel
			// 
			this._effectOptionsPanel.Controls.Add(this._effectTypeLabel);
			this._effectOptionsPanel.Controls.Add(this._effectTypeComboBox);
			this._effectOptionsPanel.Controls.Add(this._sortEffectsButton);
			this._effectOptionsPanel.Controls.Add(this._newEffectButton);
			this._effectOptionsPanel.Controls.Add(this._deleteEffectButton);
			this._effectOptionsPanel.Controls.Add(this._effectDownButton);
			this._effectOptionsPanel.Controls.Add(this._effectUpButton);
			this._effectOptionsPanel.Controls.Add(this._resourceLabel);
			this._effectOptionsPanel.Controls.Add(this._resourceComboBox);
			this._effectOptionsPanel.Controls.Add(this._armourClassLabel);
			this._effectOptionsPanel.Controls.Add(this._armourClassComboBox);
			this._effectOptionsPanel.Controls.Add(this._attributeLabel);
			this._effectOptionsPanel.Controls.Add(this._attributeComboBox);
			this._effectOptionsPanel.Controls.Add(this._classLabel);
			this._effectOptionsPanel.Controls.Add(this._classComboBox);
			this._effectOptionsPanel.Controls.Add(this._valueField);
			this._effectOptionsPanel.Controls.Add(this._modeCheckBox);
			this._effectOptionsPanel.Controls.Add(this._researchField);
			this._effectOptionsPanel.Controls.Add(this._destUnitField);
			this._effectOptionsPanel.Controls.Add(this._unitField);
			resources.ApplyResources(this._effectOptionsPanel, "_effectOptionsPanel");
			this._effectOptionsPanel.Name = "_effectOptionsPanel";
			// 
			// _effectTypeLabel
			// 
			resources.ApplyResources(this._effectTypeLabel, "_effectTypeLabel");
			this._effectTypeLabel.Name = "_effectTypeLabel";
			// 
			// _effectTypeComboBox
			// 
			this._effectTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._effectTypeComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._effectTypeComboBox, "_effectTypeComboBox");
			this._effectTypeComboBox.Name = "_effectTypeComboBox";
			this._effectTypeComboBox.SelectedIndexChanged += new System.EventHandler(this._effectTypeComboBox_SelectedIndexChanged);
			// 
			// _sortEffectsButton
			// 
			resources.ApplyResources(this._sortEffectsButton, "_sortEffectsButton");
			this._sortEffectsButton.FlatAppearance.BorderSize = 0;
			this._sortEffectsButton.Name = "_sortEffectsButton";
			this._sortEffectsButton.UseVisualStyleBackColor = true;
			this._sortEffectsButton.Click += new System.EventHandler(this._sortEffectsButton_Click);
			// 
			// _newEffectButton
			// 
			resources.ApplyResources(this._newEffectButton, "_newEffectButton");
			this._newEffectButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.Free;
			this._newEffectButton.FlatAppearance.BorderSize = 0;
			this._newEffectButton.Name = "_newEffectButton";
			this._newEffectButton.UseVisualStyleBackColor = true;
			this._newEffectButton.Click += new System.EventHandler(this._newEffectButton_Click);
			// 
			// _deleteEffectButton
			// 
			resources.ApplyResources(this._deleteEffectButton, "_deleteEffectButton");
			this._deleteEffectButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.DeleteElement;
			this._deleteEffectButton.FlatAppearance.BorderSize = 0;
			this._deleteEffectButton.Name = "_deleteEffectButton";
			this._deleteEffectButton.UseVisualStyleBackColor = true;
			this._deleteEffectButton.Click += new System.EventHandler(this._deleteEffectButton_Click);
			// 
			// _effectDownButton
			// 
			resources.ApplyResources(this._effectDownButton, "_effectDownButton");
			this._effectDownButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.AgeDown;
			this._effectDownButton.FlatAppearance.BorderSize = 0;
			this._effectDownButton.Name = "_effectDownButton";
			this._effectDownButton.UseVisualStyleBackColor = true;
			this._effectDownButton.Click += new System.EventHandler(this._effectDownButton_Click);
			// 
			// _effectUpButton
			// 
			resources.ApplyResources(this._effectUpButton, "_effectUpButton");
			this._effectUpButton.BackgroundImage = global::X2AddOnTechTreeEditor.Icons.AgeUp;
			this._effectUpButton.FlatAppearance.BorderSize = 0;
			this._effectUpButton.Name = "_effectUpButton";
			this._effectUpButton.UseVisualStyleBackColor = true;
			this._effectUpButton.Click += new System.EventHandler(this._effectUpButton_Click);
			// 
			// _resourceLabel
			// 
			resources.ApplyResources(this._resourceLabel, "_resourceLabel");
			this._resourceLabel.Name = "_resourceLabel";
			// 
			// _resourceComboBox
			// 
			this._resourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._resourceComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._resourceComboBox, "_resourceComboBox");
			this._resourceComboBox.Name = "_resourceComboBox";
			this._resourceComboBox.SelectedIndexChanged += new System.EventHandler(this._resourceComboBox_SelectedIndexChanged);
			// 
			// _armourClassLabel
			// 
			resources.ApplyResources(this._armourClassLabel, "_armourClassLabel");
			this._armourClassLabel.Name = "_armourClassLabel";
			// 
			// _armourClassComboBox
			// 
			this._armourClassComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._armourClassComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._armourClassComboBox, "_armourClassComboBox");
			this._armourClassComboBox.Name = "_armourClassComboBox";
			this._armourClassComboBox.SelectedIndexChanged += new System.EventHandler(this._armourClassComboBox_SelectedIndexChanged);
			// 
			// _attributeLabel
			// 
			resources.ApplyResources(this._attributeLabel, "_attributeLabel");
			this._attributeLabel.Name = "_attributeLabel";
			// 
			// _attributeComboBox
			// 
			this._attributeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._attributeComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._attributeComboBox, "_attributeComboBox");
			this._attributeComboBox.Name = "_attributeComboBox";
			this._attributeComboBox.SelectedIndexChanged += new System.EventHandler(this._attributeComboBox_SelectedIndexChanged);
			// 
			// _classLabel
			// 
			resources.ApplyResources(this._classLabel, "_classLabel");
			this._classLabel.Name = "_classLabel";
			// 
			// _classComboBox
			// 
			this._classComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._classComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._classComboBox, "_classComboBox");
			this._classComboBox.Name = "_classComboBox";
			this._classComboBox.SelectedIndexChanged += new System.EventHandler(this._classComboBox_SelectedIndexChanged);
			// 
			// _valueField
			// 
			resources.ApplyResources(this._valueField, "_valueField");
			this._valueField.Name = "_valueField";
			this._valueField.NameString = "Wert:";
			this._valueField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._valueField.ValueChanged += new X2AddOnTechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._valueField_ValueChanged);
			// 
			// _modeCheckBox
			// 
			resources.ApplyResources(this._modeCheckBox, "_modeCheckBox");
			this._modeCheckBox.Name = "_modeCheckBox";
			this._modeCheckBox.NameString = "Modus: [  ]  = Setzen, [X] = +/-";
			this._modeCheckBox.Value = false;
			this._modeCheckBox.ValueChanged += new X2AddOnTechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._modeCheckBox_ValueChanged);
			// 
			// _researchField
			// 
			resources.ApplyResources(this._researchField, "_researchField");
			this._researchField.Name = "_researchField";
			this._researchField.NameString = "Technologie:";
			this._researchField.Value = null;
			this._researchField.ValueChanged += new X2AddOnTechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._researchField_ValueChanged);
			// 
			// _destUnitField
			// 
			resources.ApplyResources(this._destUnitField, "_destUnitField");
			this._destUnitField.Name = "_destUnitField";
			this._destUnitField.NameString = "Upgrade:";
			this._destUnitField.Value = null;
			this._destUnitField.ValueChanged += new X2AddOnTechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._destUnitField_ValueChanged);
			// 
			// _unitField
			// 
			resources.ApplyResources(this._unitField, "_unitField");
			this._unitField.Name = "_unitField";
			this._unitField.NameString = "Einheit:";
			this._unitField.Value = null;
			this._unitField.ValueChanged += new X2AddOnTechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._unitField_ValueChanged);
			// 
			// EditResearchAttributeForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._mainTabControl);
			this.Controls.Add(this._bottomPanel);
			this.Name = "EditResearchAttributeForm";
			this.ResizeBegin += new System.EventHandler(this.EditResearchAttributeForm_ResizeBegin);
			this.ResizeEnd += new System.EventHandler(this.EditResearchAttributeForm_ResizeEnd);
			this._bottomPanel.ResumeLayout(false);
			this._mainTabControl.ResumeLayout(false);
			this._mainTabPage.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this._effectTabPage.ResumeLayout(false);
			this._effectOptionsPanel.ResumeLayout(false);
			this._effectOptionsPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel _bottomPanel;
		private System.Windows.Forms.TabControl _mainTabControl;
		private System.Windows.Forms.Button _closeButton;
		private System.Windows.Forms.TabPage _mainTabPage;
		private System.Windows.Forms.TabPage _effectTabPage;
		private System.Windows.Forms.GroupBox groupBox1;
		private Controls.LanguageDLLControl _dllHelpField;
		private Controls.LanguageDLLControl _dllName2Field;
		private Controls.LanguageDLLControl _dllDescriptionField;
		private Controls.LanguageDLLControl _dllName1Field;
		private System.Windows.Forms.GroupBox groupBox3;
		private Controls.NumberFieldControl _iconIDField;
		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.TextBox _nameTextBox;
		private System.Windows.Forms.GroupBox groupBox4;
		private Controls.ResourceCostControl _cost3Field;
		private Controls.ResourceCostControl _cost2Field;
		private Controls.ResourceCostControl _cost1Field;
		private Controls.CheckBoxFieldControl _isAgeField;
		private Controls.CheckBoxFieldControl _fullTechModeField;
		private System.Windows.Forms.GroupBox groupBox2;
		private Controls.NumberFieldControl _unknown1Field;
		private Controls.NumberFieldControl _timeField;
		private X2AddOnTechTreeEditor.DoubleBufferedListBox _effectsListBox;
		private System.Windows.Forms.Panel _effectOptionsPanel;
		private Controls.DropDownFieldControl _destUnitField;
		private Controls.DropDownFieldControl _unitField;
		private Controls.DropDownFieldControl _researchField;
		private Controls.CheckBoxFieldControl _modeCheckBox;
		private Controls.NumberFieldControl _valueField;
		private System.Windows.Forms.Label _classLabel;
		private System.Windows.Forms.ComboBox _classComboBox;
		private System.Windows.Forms.Label _resourceLabel;
		private System.Windows.Forms.ComboBox _resourceComboBox;
		private System.Windows.Forms.Label _armourClassLabel;
		private System.Windows.Forms.ComboBox _armourClassComboBox;
		private System.Windows.Forms.Label _attributeLabel;
		private System.Windows.Forms.ComboBox _attributeComboBox;
		private System.Windows.Forms.Button _effectUpButton;
		private System.Windows.Forms.Button _effectDownButton;
		private System.Windows.Forms.Button _newEffectButton;
		private System.Windows.Forms.Button _deleteEffectButton;
		private System.Windows.Forms.Button _sortEffectsButton;
		private System.Windows.Forms.Label _effectTypeLabel;
		private System.Windows.Forms.ComboBox _effectTypeComboBox;

	}
}