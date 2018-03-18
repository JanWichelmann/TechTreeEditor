namespace X2AddOnPlugin
{
	partial class NewCreatableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewCreatableForm));
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
            this._buttonField = new TechTreeEditor.Controls.NumberFieldControl();
            this.label5 = new System.Windows.Forms.Label();
            this._baseUnitComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._ageComboBox = new System.Windows.Forms.ComboBox();
            this._timeField = new TechTreeEditor.Controls.NumberFieldControl();
            this._iconField = new TechTreeEditor.Controls.NumberFieldControl();
            this._graphicsGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this._soundTypeComboBox = new System.Windows.Forms.ComboBox();
            this._fallingSoundTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._slpSFrameField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpSIDField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpMFrameField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpMIDField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpFFrameField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpFIDField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpDFrameField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpDIDField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpAFrameField = new TechTreeEditor.Controls.NumberFieldControl();
            this._slpAIDField = new TechTreeEditor.Controls.NumberFieldControl();
            this._createGraphicsCheckBox = new System.Windows.Forms.CheckBox();
            this._costGroupBox = new System.Windows.Forms.GroupBox();
            this._cost3Field = new TechTreeEditor.Controls.ResourceCostControl();
            this._cost2Field = new TechTreeEditor.Controls.ResourceCostControl();
            this._cost1Field = new TechTreeEditor.Controls.ResourceCostControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._civListBox = new System.Windows.Forms.CheckedListBox();
            this._noCivsButton = new System.Windows.Forms.Button();
            this._allCivsButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._heroCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this._graphicsGroupBox.SuspendLayout();
            this._costGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this._dllHotkeyField.ProjectFile = null;
            this._dllHotkeyField.Value = 0;
            // 
            // _dllDescriptionField
            // 
            resources.ApplyResources(this._dllDescriptionField, "_dllDescriptionField");
            this._dllDescriptionField.Name = "_dllDescriptionField";
            this._dllDescriptionField.ProjectFile = null;
            this._dllDescriptionField.Value = 0;
            // 
            // _dllHelpField
            // 
            resources.ApplyResources(this._dllHelpField, "_dllHelpField");
            this._dllHelpField.Name = "_dllHelpField";
            this._dllHelpField.ProjectFile = null;
            this._dllHelpField.Value = 0;
            // 
            // _dllNameField
            // 
            resources.ApplyResources(this._dllNameField, "_dllNameField");
            this._dllNameField.Name = "_dllNameField";
            this._dllNameField.ProjectFile = null;
            this._dllNameField.Value = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._heroCheckBox);
            this.groupBox2.Controls.Add(this._buttonField);
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
            // _buttonField
            // 
            resources.ApplyResources(this._buttonField, "_buttonField");
            this._buttonField.Name = "_buttonField";
            this._buttonField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this._iconField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _graphicsGroupBox
            // 
            this._graphicsGroupBox.Controls.Add(this.label6);
            this._graphicsGroupBox.Controls.Add(this._soundTypeComboBox);
            this._graphicsGroupBox.Controls.Add(this._fallingSoundTypeComboBox);
            this._graphicsGroupBox.Controls.Add(this.label3);
            this._graphicsGroupBox.Controls.Add(this._slpSFrameField);
            this._graphicsGroupBox.Controls.Add(this._slpSIDField);
            this._graphicsGroupBox.Controls.Add(this._slpMFrameField);
            this._graphicsGroupBox.Controls.Add(this._slpMIDField);
            this._graphicsGroupBox.Controls.Add(this._slpFFrameField);
            this._graphicsGroupBox.Controls.Add(this._slpFIDField);
            this._graphicsGroupBox.Controls.Add(this._slpDFrameField);
            this._graphicsGroupBox.Controls.Add(this._slpDIDField);
            this._graphicsGroupBox.Controls.Add(this._slpAFrameField);
            this._graphicsGroupBox.Controls.Add(this._slpAIDField);
            resources.ApplyResources(this._graphicsGroupBox, "_graphicsGroupBox");
            this._graphicsGroupBox.Name = "_graphicsGroupBox";
            this._graphicsGroupBox.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // _soundTypeComboBox
            // 
            this._soundTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._soundTypeComboBox.FormattingEnabled = true;
            resources.ApplyResources(this._soundTypeComboBox, "_soundTypeComboBox");
            this._soundTypeComboBox.Name = "_soundTypeComboBox";
            // 
            // _fallingSoundTypeComboBox
            // 
            this._fallingSoundTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._fallingSoundTypeComboBox.FormattingEnabled = true;
            resources.ApplyResources(this._fallingSoundTypeComboBox, "_fallingSoundTypeComboBox");
            this._fallingSoundTypeComboBox.Name = "_fallingSoundTypeComboBox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // _slpSFrameField
            // 
            resources.ApplyResources(this._slpSFrameField, "_slpSFrameField");
            this._slpSFrameField.Name = "_slpSFrameField";
            this._slpSFrameField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpSIDField
            // 
            resources.ApplyResources(this._slpSIDField, "_slpSIDField");
            this._slpSIDField.Name = "_slpSIDField";
            this._slpSIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpMFrameField
            // 
            resources.ApplyResources(this._slpMFrameField, "_slpMFrameField");
            this._slpMFrameField.Name = "_slpMFrameField";
            this._slpMFrameField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpMIDField
            // 
            resources.ApplyResources(this._slpMIDField, "_slpMIDField");
            this._slpMIDField.Name = "_slpMIDField";
            this._slpMIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpFFrameField
            // 
            resources.ApplyResources(this._slpFFrameField, "_slpFFrameField");
            this._slpFFrameField.Name = "_slpFFrameField";
            this._slpFFrameField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpFIDField
            // 
            resources.ApplyResources(this._slpFIDField, "_slpFIDField");
            this._slpFIDField.Name = "_slpFIDField";
            this._slpFIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpDFrameField
            // 
            resources.ApplyResources(this._slpDFrameField, "_slpDFrameField");
            this._slpDFrameField.Name = "_slpDFrameField";
            this._slpDFrameField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpDIDField
            // 
            resources.ApplyResources(this._slpDIDField, "_slpDIDField");
            this._slpDIDField.Name = "_slpDIDField";
            this._slpDIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpAFrameField
            // 
            resources.ApplyResources(this._slpAFrameField, "_slpAFrameField");
            this._slpAFrameField.Name = "_slpAFrameField";
            this._slpAFrameField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // _slpAIDField
            // 
            resources.ApplyResources(this._slpAIDField, "_slpAIDField");
            this._slpAIDField.Name = "_slpAIDField";
            this._slpAIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._slpAIDField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._slpAIDField_ValueChanged);
            // 
            // _createGraphicsCheckBox
            // 
            resources.ApplyResources(this._createGraphicsCheckBox, "_createGraphicsCheckBox");
            this._createGraphicsCheckBox.Checked = true;
            this._createGraphicsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._createGraphicsCheckBox.Name = "_createGraphicsCheckBox";
            this._createGraphicsCheckBox.UseVisualStyleBackColor = true;
            this._createGraphicsCheckBox.CheckedChanged += new System.EventHandler(this._createGraphicsCheckBox_CheckedChanged);
            // 
            // _costGroupBox
            // 
            this._costGroupBox.Controls.Add(this._cost3Field);
            this._costGroupBox.Controls.Add(this._cost2Field);
            this._costGroupBox.Controls.Add(this._cost1Field);
            resources.ApplyResources(this._costGroupBox, "_costGroupBox");
            this._costGroupBox.Name = "_costGroupBox";
            this._costGroupBox.TabStop = false;
            // 
            // _cost3Field
            // 
            resources.ApplyResources(this._cost3Field, "_cost3Field");
            this._cost3Field.Name = "_cost3Field";
            // 
            // _cost2Field
            // 
            resources.ApplyResources(this._cost2Field, "_cost2Field");
            this._cost2Field.Name = "_cost2Field";
            // 
            // _cost1Field
            // 
            resources.ApplyResources(this._cost1Field, "_cost1Field");
            this._cost1Field.Name = "_cost1Field";
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
            // _heroCheckBox
            // 
            resources.ApplyResources(this._heroCheckBox, "_heroCheckBox");
            this._heroCheckBox.Name = "_heroCheckBox";
            this._heroCheckBox.UseVisualStyleBackColor = true;
            this._heroCheckBox.CheckedChanged += new System.EventHandler(this._heroCheckBox_CheckedChanged);
            // 
            // NewCreatableForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._createGraphicsCheckBox);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this._costGroupBox);
            this.Controls.Add(this._graphicsGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewCreatableForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this._graphicsGroupBox.ResumeLayout(false);
            this._graphicsGroupBox.PerformLayout();
            this._costGroupBox.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
		private System.Windows.Forms.GroupBox _graphicsGroupBox;
		private TechTreeEditor.Controls.NumberFieldControl _slpSFrameField;
		private TechTreeEditor.Controls.NumberFieldControl _slpSIDField;
		private TechTreeEditor.Controls.NumberFieldControl _slpMFrameField;
		private TechTreeEditor.Controls.NumberFieldControl _slpMIDField;
		private TechTreeEditor.Controls.NumberFieldControl _slpFFrameField;
		private TechTreeEditor.Controls.NumberFieldControl _slpFIDField;
		private TechTreeEditor.Controls.NumberFieldControl _slpDFrameField;
		private TechTreeEditor.Controls.NumberFieldControl _slpDIDField;
		private TechTreeEditor.Controls.NumberFieldControl _slpAFrameField;
		private TechTreeEditor.Controls.NumberFieldControl _slpAIDField;
		private System.Windows.Forms.ComboBox _soundTypeComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox _costGroupBox;
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
		private TechTreeEditor.Controls.NumberFieldControl _buttonField;
		private System.Windows.Forms.CheckBox _createGraphicsCheckBox;
		private System.Windows.Forms.ComboBox _fallingSoundTypeComboBox;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox _heroCheckBox;
    }
}