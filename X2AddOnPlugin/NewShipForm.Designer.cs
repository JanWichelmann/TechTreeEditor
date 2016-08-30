namespace X2AddOnPlugin
{
	partial class NewShipForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewShipForm));
			this.label1 = new System.Windows.Forms.Label();
			this._nameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this._relIDTextBox = new System.Windows.Forms.TextBox();
			this._dllHotkeyField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllDescriptionField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllHelpField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllNameField = new TechTreeEditor.Controls.LanguageDLLControl();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this._baseUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this._ageComboBox = new System.Windows.Forms.ComboBox();
			this._timeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._iconField = new TechTreeEditor.Controls.NumberFieldControl();
			this.groupBox25 = new System.Windows.Forms.GroupBox();
			this._cost3Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost2Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost1Field = new TechTreeEditor.Controls.ResourceCostControl();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._civListBox = new System.Windows.Forms.CheckedListBox();
			this._noCivsButton = new System.Windows.Forms.Button();
			this._allCivsButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._graphicsGroupBox = new System.Windows.Forms.GroupBox();
			this._broadsideCheckBox = new System.Windows.Forms.CheckBox();
			this._slpBaseId = new TechTreeEditor.Controls.NumberFieldControl();
			this._openShipFileButton = new System.Windows.Forms.Button();
			this._shipFileTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this._openShipDialog = new System.Windows.Forms.OpenFileDialog();
			this._civSetsGroupBox = new System.Windows.Forms.GroupBox();
			this._civSetWECheckBox = new System.Windows.Forms.CheckBox();
			this._civSetORCheckBox = new System.Windows.Forms.CheckBox();
			this._civSetMECheckBox = new System.Windows.Forms.CheckBox();
			this._civSetINCheckBox = new System.Windows.Forms.CheckBox();
			this._civSetASCheckBox = new System.Windows.Forms.CheckBox();
			this._createOnlyGraphicCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox25.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this._graphicsGroupBox.SuspendLayout();
			this._civSetsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _nameTextBox
			// 
			resources.ApplyResources(this._nameTextBox, "_nameTextBox");
			this._nameTextBox.Name = "_nameTextBox";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this._relIDTextBox);
			this.groupBox1.Controls.Add(this._dllHotkeyField);
			this.groupBox1.Controls.Add(this._dllDescriptionField);
			this.groupBox1.Controls.Add(this._dllHelpField);
			this.groupBox1.Controls.Add(this._dllNameField);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// _relIDTextBox
			// 
			resources.ApplyResources(this._relIDTextBox, "_relIDTextBox");
			this._relIDTextBox.Name = "_relIDTextBox";
			this._relIDTextBox.TextChanged += new System.EventHandler(this._relIDTextBox_TextChanged);
			// 
			// _dllHotkeyField
			// 
			resources.ApplyResources(this._dllHotkeyField, "_dllHotkeyField");
			this._dllHotkeyField.Name = "_dllHotkeyField";
			this._dllHotkeyField.NameString = "Hotkey: ";
			this._dllHotkeyField.ProjectFile = null;
			this._dllHotkeyField.Value = 0;
			// 
			// _dllDescriptionField
			// 
			resources.ApplyResources(this._dllDescriptionField, "_dllDescriptionField");
			this._dllDescriptionField.Name = "_dllDescriptionField";
			this._dllDescriptionField.NameString = "Beschreibung: ";
			this._dllDescriptionField.ProjectFile = null;
			this._dllDescriptionField.Value = 0;
			// 
			// _dllHelpField
			// 
			resources.ApplyResources(this._dllHelpField, "_dllHelpField");
			this._dllHelpField.Name = "_dllHelpField";
			this._dllHelpField.NameString = "Hilfe: ";
			this._dllHelpField.ProjectFile = null;
			this._dllHelpField.Value = 0;
			// 
			// _dllNameField
			// 
			resources.ApplyResources(this._dllNameField, "_dllNameField");
			this._dllNameField.Name = "_dllNameField";
			this._dllNameField.NameString = "Name: ";
			this._dllNameField.ProjectFile = null;
			this._dllNameField.Value = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this._baseUnitComboBox);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this._ageComboBox);
			this.groupBox2.Controls.Add(this._timeField);
			this.groupBox2.Controls.Add(this._nameTextBox);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this._iconField);
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// _baseUnitComboBox
			// 
			this._baseUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._baseUnitComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._baseUnitComboBox, "_baseUnitComboBox");
			this._baseUnitComboBox.Name = "_baseUnitComboBox";
			this._baseUnitComboBox.Sorted = true;
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// _ageComboBox
			// 
			this._ageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._ageComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._ageComboBox, "_ageComboBox");
			this._ageComboBox.Name = "_ageComboBox";
			// 
			// _timeField
			// 
			resources.ApplyResources(this._timeField, "_timeField");
			this._timeField.Name = "_timeField";
			this._timeField.NameString = "Erschaff-Zeit:";
			this._timeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// _iconField
			// 
			resources.ApplyResources(this._iconField, "_iconField");
			this._iconField.Name = "_iconField";
			this._iconField.NameString = "Icon-ID:";
			this._iconField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// groupBox25
			// 
			this.groupBox25.Controls.Add(this._cost3Field);
			this.groupBox25.Controls.Add(this._cost2Field);
			this.groupBox25.Controls.Add(this._cost1Field);
			resources.ApplyResources(this.groupBox25, "groupBox25");
			this.groupBox25.Name = "groupBox25";
			this.groupBox25.TabStop = false;
			// 
			// _cost3Field
			// 
			resources.ApplyResources(this._cost3Field, "_cost3Field");
			this._cost3Field.Name = "_cost3Field";
			this._cost3Field.NameString = "Kosten 3:";
			// 
			// _cost2Field
			// 
			resources.ApplyResources(this._cost2Field, "_cost2Field");
			this._cost2Field.Name = "_cost2Field";
			this._cost2Field.NameString = "Kosten 2:";
			// 
			// _cost1Field
			// 
			resources.ApplyResources(this._cost1Field, "_cost1Field");
			this._cost1Field.Name = "_cost1Field";
			this._cost1Field.NameString = "Kosten 1:";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this._civListBox);
			this.groupBox4.Controls.Add(this._noCivsButton);
			this.groupBox4.Controls.Add(this._allCivsButton);
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			// 
			// _civListBox
			// 
			this._civListBox.CheckOnClick = true;
			this._civListBox.FormattingEnabled = true;
			resources.ApplyResources(this._civListBox, "_civListBox");
			this._civListBox.Name = "_civListBox";
			this._civListBox.Sorted = true;
			this._civListBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this._civListBox_Format);
			// 
			// _noCivsButton
			// 
			resources.ApplyResources(this._noCivsButton, "_noCivsButton");
			this._noCivsButton.Name = "_noCivsButton";
			this._noCivsButton.UseVisualStyleBackColor = true;
			this._noCivsButton.Click += new System.EventHandler(this._noCivsButton_Click);
			// 
			// _allCivsButton
			// 
			resources.ApplyResources(this._allCivsButton, "_allCivsButton");
			this._allCivsButton.Name = "_allCivsButton";
			this._allCivsButton.UseVisualStyleBackColor = true;
			this._allCivsButton.Click += new System.EventHandler(this._allCivsButton_Click);
			// 
			// _okButton
			// 
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this._okButton_Click);
			// 
			// _cancelButton
			// 
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
			// 
			// _graphicsGroupBox
			// 
			this._graphicsGroupBox.Controls.Add(this._createOnlyGraphicCheckBox);
			this._graphicsGroupBox.Controls.Add(this._broadsideCheckBox);
			this._graphicsGroupBox.Controls.Add(this._slpBaseId);
			this._graphicsGroupBox.Controls.Add(this._openShipFileButton);
			this._graphicsGroupBox.Controls.Add(this._shipFileTextBox);
			this._graphicsGroupBox.Controls.Add(this.label3);
			resources.ApplyResources(this._graphicsGroupBox, "_graphicsGroupBox");
			this._graphicsGroupBox.Name = "_graphicsGroupBox";
			this._graphicsGroupBox.TabStop = false;
			// 
			// _broadsideCheckBox
			// 
			resources.ApplyResources(this._broadsideCheckBox, "_broadsideCheckBox");
			this._broadsideCheckBox.Name = "_broadsideCheckBox";
			this._broadsideCheckBox.UseVisualStyleBackColor = true;
			// 
			// _slpBaseId
			// 
			resources.ApplyResources(this._slpBaseId, "_slpBaseId");
			this._slpBaseId.Name = "_slpBaseId";
			this._slpBaseId.NameString = "SLP-Basis-ID (Reihenfolge: Schatten, Rumpf, Segel):";
			this._slpBaseId.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// _openShipFileButton
			// 
			resources.ApplyResources(this._openShipFileButton, "_openShipFileButton");
			this._openShipFileButton.Name = "_openShipFileButton";
			this._openShipFileButton.UseVisualStyleBackColor = true;
			this._openShipFileButton.Click += new System.EventHandler(this._openShipFileButton_Click);
			// 
			// _shipFileTextBox
			// 
			resources.ApplyResources(this._shipFileTextBox, "_shipFileTextBox");
			this._shipFileTextBox.Name = "_shipFileTextBox";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _openShipDialog
			// 
			resources.ApplyResources(this._openShipDialog, "_openShipDialog");
			// 
			// _civSetsGroupBox
			// 
			this._civSetsGroupBox.Controls.Add(this._civSetWECheckBox);
			this._civSetsGroupBox.Controls.Add(this._civSetORCheckBox);
			this._civSetsGroupBox.Controls.Add(this._civSetMECheckBox);
			this._civSetsGroupBox.Controls.Add(this._civSetINCheckBox);
			this._civSetsGroupBox.Controls.Add(this._civSetASCheckBox);
			resources.ApplyResources(this._civSetsGroupBox, "_civSetsGroupBox");
			this._civSetsGroupBox.Name = "_civSetsGroupBox";
			this._civSetsGroupBox.TabStop = false;
			// 
			// _civSetWECheckBox
			// 
			resources.ApplyResources(this._civSetWECheckBox, "_civSetWECheckBox");
			this._civSetWECheckBox.Name = "_civSetWECheckBox";
			this._civSetWECheckBox.UseVisualStyleBackColor = true;
			// 
			// _civSetORCheckBox
			// 
			resources.ApplyResources(this._civSetORCheckBox, "_civSetORCheckBox");
			this._civSetORCheckBox.Name = "_civSetORCheckBox";
			this._civSetORCheckBox.UseVisualStyleBackColor = true;
			// 
			// _civSetMECheckBox
			// 
			resources.ApplyResources(this._civSetMECheckBox, "_civSetMECheckBox");
			this._civSetMECheckBox.Name = "_civSetMECheckBox";
			this._civSetMECheckBox.UseVisualStyleBackColor = true;
			// 
			// _civSetINCheckBox
			// 
			resources.ApplyResources(this._civSetINCheckBox, "_civSetINCheckBox");
			this._civSetINCheckBox.Name = "_civSetINCheckBox";
			this._civSetINCheckBox.UseVisualStyleBackColor = true;
			// 
			// _civSetASCheckBox
			// 
			resources.ApplyResources(this._civSetASCheckBox, "_civSetASCheckBox");
			this._civSetASCheckBox.Name = "_civSetASCheckBox";
			this._civSetASCheckBox.UseVisualStyleBackColor = true;
			// 
			// _createOnlyGraphicCheckBox
			// 
			resources.ApplyResources(this._createOnlyGraphicCheckBox, "_createOnlyGraphicCheckBox");
			this._createOnlyGraphicCheckBox.Name = "_createOnlyGraphicCheckBox";
			this._createOnlyGraphicCheckBox.UseVisualStyleBackColor = true;
			// 
			// NewShipForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._civSetsGroupBox);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox25);
			this.Controls.Add(this._graphicsGroupBox);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "NewShipForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox25.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this._graphicsGroupBox.ResumeLayout(false);
			this._graphicsGroupBox.PerformLayout();
			this._civSetsGroupBox.ResumeLayout(false);
			this._civSetsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _nameTextBox;
		private TechTreeEditor.Controls.NumberFieldControl _iconField;
		private System.Windows.Forms.GroupBox groupBox1;
		private TechTreeEditor.Controls.LanguageDLLControl _dllHotkeyField;
		private TechTreeEditor.Controls.LanguageDLLControl _dllDescriptionField;
		private TechTreeEditor.Controls.LanguageDLLControl _dllHelpField;
		private TechTreeEditor.Controls.LanguageDLLControl _dllNameField;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _relIDTextBox;
		private System.Windows.Forms.GroupBox groupBox25;
		private TechTreeEditor.Controls.ResourceCostControl _cost3Field;
		private TechTreeEditor.Controls.ResourceCostControl _cost2Field;
		private TechTreeEditor.Controls.ResourceCostControl _cost1Field;
		private TechTreeEditor.Controls.NumberFieldControl _timeField;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox _ageComboBox;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button _noCivsButton;
		private System.Windows.Forms.Button _allCivsButton;
		private System.Windows.Forms.CheckedListBox _civListBox;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox _baseUnitComboBox;
		private System.Windows.Forms.GroupBox _graphicsGroupBox;
		private System.Windows.Forms.Button _openShipFileButton;
		private System.Windows.Forms.TextBox _shipFileTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.OpenFileDialog _openShipDialog;
		private TechTreeEditor.Controls.NumberFieldControl _slpBaseId;
		private System.Windows.Forms.CheckBox _broadsideCheckBox;
		private System.Windows.Forms.GroupBox _civSetsGroupBox;
		private System.Windows.Forms.CheckBox _civSetWECheckBox;
		private System.Windows.Forms.CheckBox _civSetORCheckBox;
		private System.Windows.Forms.CheckBox _civSetMECheckBox;
		private System.Windows.Forms.CheckBox _civSetINCheckBox;
		private System.Windows.Forms.CheckBox _civSetASCheckBox;
		private System.Windows.Forms.CheckBox _createOnlyGraphicCheckBox;
	}
}