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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this._ageComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._soundTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox25 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._civListBox = new System.Windows.Forms.CheckedListBox();
			this._noCivsButton = new System.Windows.Forms.Button();
			this._allCivsButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._baseUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this._cost3Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost2Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost1Field = new TechTreeEditor.Controls.ResourceCostControl();
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
			this._timeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._iconField = new TechTreeEditor.Controls.NumberFieldControl();
			this._dllHotkeyField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllDescriptionField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllHelpField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllNameField = new TechTreeEditor.Controls.LanguageDLLControl();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox25.SuspendLayout();
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
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._soundTypeComboBox);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this._slpSFrameField);
			this.groupBox3.Controls.Add(this._slpSIDField);
			this.groupBox3.Controls.Add(this._slpMFrameField);
			this.groupBox3.Controls.Add(this._slpMIDField);
			this.groupBox3.Controls.Add(this._slpFFrameField);
			this.groupBox3.Controls.Add(this._slpFIDField);
			this.groupBox3.Controls.Add(this._slpDFrameField);
			this.groupBox3.Controls.Add(this._slpDIDField);
			this.groupBox3.Controls.Add(this._slpAFrameField);
			this.groupBox3.Controls.Add(this._slpAIDField);
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// _soundTypeComboBox
			// 
			this._soundTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._soundTypeComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._soundTypeComboBox, "_soundTypeComboBox");
			this._soundTypeComboBox.Name = "_soundTypeComboBox";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
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
			// _baseUnitComboBox
			// 
			this._baseUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._baseUnitComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._baseUnitComboBox, "_baseUnitComboBox");
			this._baseUnitComboBox.Name = "_baseUnitComboBox";
			this._baseUnitComboBox.Sorted = true;
			this._baseUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._baseUnitComboBox_SelectedIndexChanged);
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
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
			// _slpSFrameField
			// 
			resources.ApplyResources(this._slpSFrameField, "_slpSFrameField");
			this._slpSFrameField.Name = "_slpSFrameField";
			this._slpSFrameField.NameString = "Framezahl:";
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
			this._slpSIDField.NameString = "SLP S:";
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
			this._slpMFrameField.NameString = "Framezahl:";
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
			this._slpMIDField.NameString = "SLP M:";
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
			this._slpFFrameField.NameString = "Framezahl:";
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
			this._slpFIDField.NameString = "SLP F:";
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
			this._slpDFrameField.NameString = "Framezahl:";
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
			this._slpDIDField.NameString = "SLP D:";
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
			this._slpAFrameField.NameString = "Framezahl:";
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
			this._slpAIDField.NameString = "SLP A:";
			this._slpAIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
			// NewCreatableForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox25);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "NewCreatableForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox25.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
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
		private System.Windows.Forms.GroupBox groupBox3;
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
	}
}