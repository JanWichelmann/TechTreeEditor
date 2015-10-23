namespace TechTreeEditor.Controls
{
	partial class TechEffectControl
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechEffectControl));
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
			this._valueField = new TechTreeEditor.Controls.NumberFieldControl();
			this._modeCheckBox = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._researchField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._destUnitField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._unitField = new TechTreeEditor.Controls.DropDownFieldControl();
			this._effectsListBox = new TechTreeEditor.DoubleBufferedListBox();
			this._effectOptionsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _effectOptionsPanel
			// 
			resources.ApplyResources(this._effectOptionsPanel, "_effectOptionsPanel");
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
			this._effectOptionsPanel.Name = "_effectOptionsPanel";
			// 
			// _effectTypeLabel
			// 
			resources.ApplyResources(this._effectTypeLabel, "_effectTypeLabel");
			this._effectTypeLabel.Name = "_effectTypeLabel";
			// 
			// _effectTypeComboBox
			// 
			resources.ApplyResources(this._effectTypeComboBox, "_effectTypeComboBox");
			this._effectTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._effectTypeComboBox.FormattingEnabled = true;
			this._effectTypeComboBox.Name = "_effectTypeComboBox";
			this._effectTypeComboBox.SelectedIndexChanged += new System.EventHandler(this._effectTypeComboBox_SelectedIndexChanged);
			// 
			// _sortEffectsButton
			// 
			resources.ApplyResources(this._sortEffectsButton, "_sortEffectsButton");
			this._sortEffectsButton.BackgroundImage = global::TechTreeEditor.Icons.SortTechEffects;
			this._sortEffectsButton.FlatAppearance.BorderSize = 0;
			this._sortEffectsButton.Name = "_sortEffectsButton";
			this._sortEffectsButton.UseVisualStyleBackColor = true;
			this._sortEffectsButton.Click += new System.EventHandler(this._sortEffectsButton_Click);
			// 
			// _newEffectButton
			// 
			resources.ApplyResources(this._newEffectButton, "_newEffectButton");
			this._newEffectButton.BackgroundImage = global::TechTreeEditor.Icons.NewTechEffect;
			this._newEffectButton.FlatAppearance.BorderSize = 0;
			this._newEffectButton.Name = "_newEffectButton";
			this._newEffectButton.UseVisualStyleBackColor = true;
			this._newEffectButton.Click += new System.EventHandler(this._newEffectButton_Click);
			// 
			// _deleteEffectButton
			// 
			resources.ApplyResources(this._deleteEffectButton, "_deleteEffectButton");
			this._deleteEffectButton.BackgroundImage = global::TechTreeEditor.Icons.DeleteTechEffect;
			this._deleteEffectButton.FlatAppearance.BorderSize = 0;
			this._deleteEffectButton.Name = "_deleteEffectButton";
			this._deleteEffectButton.UseVisualStyleBackColor = true;
			this._deleteEffectButton.Click += new System.EventHandler(this._deleteEffectButton_Click);
			// 
			// _effectDownButton
			// 
			resources.ApplyResources(this._effectDownButton, "_effectDownButton");
			this._effectDownButton.BackgroundImage = global::TechTreeEditor.Icons.DownTechEffect;
			this._effectDownButton.FlatAppearance.BorderSize = 0;
			this._effectDownButton.Name = "_effectDownButton";
			this._effectDownButton.UseVisualStyleBackColor = true;
			this._effectDownButton.Click += new System.EventHandler(this._effectDownButton_Click);
			// 
			// _effectUpButton
			// 
			resources.ApplyResources(this._effectUpButton, "_effectUpButton");
			this._effectUpButton.BackgroundImage = global::TechTreeEditor.Icons.UpTechEffect;
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
			resources.ApplyResources(this._resourceComboBox, "_resourceComboBox");
			this._resourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._resourceComboBox.FormattingEnabled = true;
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
			resources.ApplyResources(this._armourClassComboBox, "_armourClassComboBox");
			this._armourClassComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._armourClassComboBox.FormattingEnabled = true;
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
			resources.ApplyResources(this._attributeComboBox, "_attributeComboBox");
			this._attributeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._attributeComboBox.FormattingEnabled = true;
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
			resources.ApplyResources(this._classComboBox, "_classComboBox");
			this._classComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._classComboBox.FormattingEnabled = true;
			this._classComboBox.Name = "_classComboBox";
			this._classComboBox.SelectedIndexChanged += new System.EventHandler(this._classComboBox_SelectedIndexChanged);
			// 
			// _valueField
			// 
			resources.ApplyResources(this._valueField, "_valueField");
			this._valueField.Name = "_valueField";
			this._valueField.NameString = "Value:";
			this._valueField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._valueField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._valueField_ValueChanged);
			// 
			// _modeCheckBox
			// 
			resources.ApplyResources(this._modeCheckBox, "_modeCheckBox");
			this._modeCheckBox.Name = "_modeCheckBox";
			this._modeCheckBox.NameString = "Mode: [  ]  = Set, [X] = +/-";
			this._modeCheckBox.Value = false;
			this._modeCheckBox.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._modeCheckBox_ValueChanged);
			// 
			// _researchField
			// 
			resources.ApplyResources(this._researchField, "_researchField");
			this._researchField.Name = "_researchField";
			this._researchField.NameString = "Research:";
			this._researchField.Value = null;
			this._researchField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._researchField_ValueChanged);
			// 
			// _destUnitField
			// 
			resources.ApplyResources(this._destUnitField, "_destUnitField");
			this._destUnitField.Name = "_destUnitField";
			this._destUnitField.NameString = "Upgrade unit:";
			this._destUnitField.Value = null;
			this._destUnitField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._destUnitField_ValueChanged);
			// 
			// _unitField
			// 
			resources.ApplyResources(this._unitField, "_unitField");
			this._unitField.Name = "_unitField";
			this._unitField.NameString = "Unit:";
			this._unitField.Value = null;
			this._unitField.ValueChanged += new TechTreeEditor.Controls.DropDownFieldControl.ValueChangedEventHandler(this._unitField_ValueChanged);
			// 
			// _effectsListBox
			// 
			resources.ApplyResources(this._effectsListBox, "_effectsListBox");
			this._effectsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this._effectsListBox.Name = "_effectsListBox";
			this._effectsListBox.SelectedIndexChanged += new System.EventHandler(this._effectsListBox_SelectedIndexChanged);
			// 
			// TechEffectControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._effectsListBox);
			this.Controls.Add(this._effectOptionsPanel);
			this.Name = "TechEffectControl";
			this._effectOptionsPanel.ResumeLayout(false);
			this._effectOptionsPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DoubleBufferedListBox _effectsListBox;
		private System.Windows.Forms.Panel _effectOptionsPanel;
		private System.Windows.Forms.Label _effectTypeLabel;
		private System.Windows.Forms.ComboBox _effectTypeComboBox;
		private System.Windows.Forms.Button _sortEffectsButton;
		private System.Windows.Forms.Button _newEffectButton;
		private System.Windows.Forms.Button _deleteEffectButton;
		private System.Windows.Forms.Button _effectDownButton;
		private System.Windows.Forms.Button _effectUpButton;
		private System.Windows.Forms.Label _resourceLabel;
		private System.Windows.Forms.ComboBox _resourceComboBox;
		private System.Windows.Forms.Label _armourClassLabel;
		private System.Windows.Forms.ComboBox _armourClassComboBox;
		private System.Windows.Forms.Label _attributeLabel;
		private System.Windows.Forms.ComboBox _attributeComboBox;
		private System.Windows.Forms.Label _classLabel;
		private System.Windows.Forms.ComboBox _classComboBox;
		private Controls.NumberFieldControl _valueField;
		private Controls.CheckBoxFieldControl _modeCheckBox;
		private Controls.DropDownFieldControl _researchField;
		private Controls.DropDownFieldControl _destUnitField;
		private Controls.DropDownFieldControl _unitField;
	}
}
