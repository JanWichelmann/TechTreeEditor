namespace TechTreeEditor
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
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._nameLabel = new System.Windows.Forms.Label();
			this._nameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this._relIDTextBox = new System.Windows.Forms.TextBox();
			this._effectTabPage = new System.Windows.Forms.TabPage();
			this._unknown1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._timeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._cost3Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost2Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._cost1Field = new TechTreeEditor.Controls.ResourceCostControl();
			this._fullTechModeField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._isAgeField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._iconIDField = new TechTreeEditor.Controls.NumberFieldControl();
			this._dllHelpField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllName2Field = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllDescriptionField = new TechTreeEditor.Controls.LanguageDLLControl();
			this._dllName1Field = new TechTreeEditor.Controls.LanguageDLLControl();
			this._techEffectField = new TechTreeEditor.Controls.TechEffectControl();
			this._bottomPanel.SuspendLayout();
			this._mainTabControl.SuspendLayout();
			this._mainTabPage.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this._effectTabPage.SuspendLayout();
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
			this._mainTabControl.Controls.Add(this._mainTabPage);
			this._mainTabControl.Controls.Add(this._effectTabPage);
			this._mainTabControl.Name = "_mainTabControl";
			this._mainTabControl.SelectedIndex = 0;
			// 
			// _mainTabPage
			// 
			resources.ApplyResources(this._mainTabPage, "_mainTabPage");
			this._mainTabPage.Controls.Add(this.groupBox2);
			this._mainTabPage.Controls.Add(this.groupBox4);
			this._mainTabPage.Controls.Add(this.groupBox3);
			this._mainTabPage.Controls.Add(this.groupBox1);
			this._mainTabPage.Name = "_mainTabPage";
			this._mainTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Controls.Add(this._unknown1Field);
			this.groupBox2.Controls.Add(this._timeField);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// groupBox4
			// 
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Controls.Add(this._cost3Field);
			this.groupBox4.Controls.Add(this._cost2Field);
			this.groupBox4.Controls.Add(this._cost1Field);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			// 
			// groupBox3
			// 
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Controls.Add(this._fullTechModeField);
			this.groupBox3.Controls.Add(this._isAgeField);
			this.groupBox3.Controls.Add(this._iconIDField);
			this.groupBox3.Controls.Add(this._nameLabel);
			this.groupBox3.Controls.Add(this._nameTextBox);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
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
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._relIDTextBox);
			this.groupBox1.Controls.Add(this._dllHelpField);
			this.groupBox1.Controls.Add(this._dllName2Field);
			this.groupBox1.Controls.Add(this._dllDescriptionField);
			this.groupBox1.Controls.Add(this._dllName1Field);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _relIDTextBox
			// 
			resources.ApplyResources(this._relIDTextBox, "_relIDTextBox");
			this._relIDTextBox.Name = "_relIDTextBox";
			this._relIDTextBox.TextChanged += new System.EventHandler(this._relIDTextBox_TextChanged);
			// 
			// _effectTabPage
			// 
			resources.ApplyResources(this._effectTabPage, "_effectTabPage");
			this._effectTabPage.Controls.Add(this._techEffectField);
			this._effectTabPage.Name = "_effectTabPage";
			this._effectTabPage.UseVisualStyleBackColor = true;
			// 
			// _unknown1Field
			// 
			resources.ApplyResources(this._unknown1Field, "_unknown1Field");
			this._unknown1Field.Name = "_unknown1Field";
			this._unknown1Field.NameString = "Unknown 1:";
			this._unknown1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown1Field_ValueChanged);
			// 
			// _timeField
			// 
			resources.ApplyResources(this._timeField, "_timeField");
			this._timeField.Name = "_timeField";
			this._timeField.NameString = "Research time:";
			this._timeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._timeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._timeField_ValueChanged);
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
			// _fullTechModeField
			// 
			resources.ApplyResources(this._fullTechModeField, "_fullTechModeField");
			this._fullTechModeField.Name = "_fullTechModeField";
			this._fullTechModeField.NameString = "Full tech mode";
			this._fullTechModeField.Value = false;
			this._fullTechModeField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._fullTechModeField_ValueChanged);
			// 
			// _isAgeField
			// 
			resources.ApplyResources(this._isAgeField, "_isAgeField");
			this._isAgeField.Name = "_isAgeField";
			this._isAgeField.NameString = "Age research";
			this._isAgeField.Value = false;
			this._isAgeField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._isAgeField_ValueChanged);
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
			// _dllHelpField
			// 
			resources.ApplyResources(this._dllHelpField, "_dllHelpField");
			this._dllHelpField.Name = "_dllHelpField";
			this._dllHelpField.NameString = "Help:";
			this._dllHelpField.ProjectFile = null;
			this._dllHelpField.Value = 0;
			this._dllHelpField.ValueChanged += new TechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllHelpField_ValueChanged);
			// 
			// _dllName2Field
			// 
			resources.ApplyResources(this._dllName2Field, "_dllName2Field");
			this._dllName2Field.Name = "_dllName2Field";
			this._dllName2Field.NameString = "Name 2:";
			this._dllName2Field.ProjectFile = null;
			this._dllName2Field.Value = 0;
			this._dllName2Field.ValueChanged += new TechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllName2Field_ValueChanged);
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
			// _dllName1Field
			// 
			resources.ApplyResources(this._dllName1Field, "_dllName1Field");
			this._dllName1Field.Name = "_dllName1Field";
			this._dllName1Field.NameString = "Name 1: ";
			this._dllName1Field.ProjectFile = null;
			this._dllName1Field.Value = 0;
			this._dllName1Field.ValueChanged += new TechTreeEditor.Controls.LanguageDLLControl.ValueChangedEventHandler(this._dllName1Field_ValueChanged);
			// 
			// _techEffectField
			// 
			resources.ApplyResources(this._techEffectField, "_techEffectField");
			this._techEffectField.EffectList = null;
			this._techEffectField.Name = "_techEffectField";
			this._techEffectField.ProjectFile = null;
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
			this.groupBox1.PerformLayout();
			this._effectTabPage.ResumeLayout(false);
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
		private Controls.TechEffectControl _techEffectField;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _relIDTextBox;

	}
}