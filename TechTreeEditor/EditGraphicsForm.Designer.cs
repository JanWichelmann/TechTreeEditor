namespace TechTreeEditor
{
	partial class EditGraphicsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGraphicsForm));
			this._bottomPanel = new System.Windows.Forms.Panel();
			this._saveButton = new System.Windows.Forms.Button();
			this._closeButton = new System.Windows.Forms.Button();
			this._mainPanel = new System.Windows.Forms.Panel();
			this._attackSoundField = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._attackSoundView = new System.Windows.Forms.DataGridView();
			this._attackSoundAngleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound1IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound1DelayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound2IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound2DelayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound3IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._attackSound3DelayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this._deltaView = new System.Windows.Forms.DataGridView();
			this._deltaIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._deltaXColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._deltaYColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._unknown3Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._unknown1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._layerField = new TechTreeEditor.Controls.NumberFieldControl();
			this._speedField = new TechTreeEditor.Controls.NumberFieldControl();
			this._rainbowField = new TechTreeEditor.Controls.NumberFieldControl();
			this._mirrorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._sequenceTypeField = new TechTreeEditor.Controls.NumberFieldControl();
			this._coord4Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._coord3Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._coord2Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._coord1Field = new TechTreeEditor.Controls.NumberFieldControl();
			this._playerColorField = new TechTreeEditor.Controls.NumberFieldControl();
			this._soundIDField = new TechTreeEditor.Controls.NumberFieldControl();
			this._idField = new TechTreeEditor.Controls.NumberFieldControl();
			this._angleCountField = new TechTreeEditor.Controls.NumberFieldControl();
			this._replayDelayField = new TechTreeEditor.Controls.NumberFieldControl();
			this._replayCheckBox = new TechTreeEditor.Controls.CheckBoxFieldControl();
			this._frameCountField = new TechTreeEditor.Controls.NumberFieldControl();
			this._frameRateField = new TechTreeEditor.Controls.NumberFieldControl();
			this._slpField = new TechTreeEditor.Controls.NumberFieldControl();
			this._name2TextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this._name1TextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._filterTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._newGraphicButton = new System.Windows.Forms.Button();
			this._deleteGraphicButton = new System.Windows.Forms.Button();
			this._graphicListBox = new TechTreeEditor.DoubleBufferedListBox();
			this._bottomPanel.SuspendLayout();
			this._mainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._attackSoundView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._deltaView)).BeginInit();
			this.SuspendLayout();
			// 
			// _bottomPanel
			// 
			resources.ApplyResources(this._bottomPanel, "_bottomPanel");
			this._bottomPanel.Controls.Add(this._saveButton);
			this._bottomPanel.Controls.Add(this._closeButton);
			this._bottomPanel.Name = "_bottomPanel";
			// 
			// _saveButton
			// 
			resources.ApplyResources(this._saveButton, "_saveButton");
			this._saveButton.Name = "_saveButton";
			this._saveButton.UseVisualStyleBackColor = true;
			this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _mainPanel
			// 
			resources.ApplyResources(this._mainPanel, "_mainPanel");
			this._mainPanel.Controls.Add(this._attackSoundField);
			this._mainPanel.Controls.Add(this._attackSoundView);
			this._mainPanel.Controls.Add(this.label4);
			this._mainPanel.Controls.Add(this._deltaView);
			this._mainPanel.Controls.Add(this._unknown3Field);
			this._mainPanel.Controls.Add(this._unknown2Field);
			this._mainPanel.Controls.Add(this._unknown1Field);
			this._mainPanel.Controls.Add(this._layerField);
			this._mainPanel.Controls.Add(this._speedField);
			this._mainPanel.Controls.Add(this._rainbowField);
			this._mainPanel.Controls.Add(this._mirrorField);
			this._mainPanel.Controls.Add(this._sequenceTypeField);
			this._mainPanel.Controls.Add(this._coord4Field);
			this._mainPanel.Controls.Add(this._coord3Field);
			this._mainPanel.Controls.Add(this._coord2Field);
			this._mainPanel.Controls.Add(this._coord1Field);
			this._mainPanel.Controls.Add(this._playerColorField);
			this._mainPanel.Controls.Add(this._soundIDField);
			this._mainPanel.Controls.Add(this._idField);
			this._mainPanel.Controls.Add(this._angleCountField);
			this._mainPanel.Controls.Add(this._replayDelayField);
			this._mainPanel.Controls.Add(this._replayCheckBox);
			this._mainPanel.Controls.Add(this._frameCountField);
			this._mainPanel.Controls.Add(this._frameRateField);
			this._mainPanel.Controls.Add(this._slpField);
			this._mainPanel.Controls.Add(this._name2TextBox);
			this._mainPanel.Controls.Add(this.label3);
			this._mainPanel.Controls.Add(this._name1TextBox);
			this._mainPanel.Controls.Add(this.label2);
			this._mainPanel.Controls.Add(this._filterTextBox);
			this._mainPanel.Controls.Add(this.label1);
			this._mainPanel.Controls.Add(this._newGraphicButton);
			this._mainPanel.Controls.Add(this._deleteGraphicButton);
			this._mainPanel.Name = "_mainPanel";
			// 
			// _attackSoundField
			// 
			resources.ApplyResources(this._attackSoundField, "_attackSoundField");
			this._attackSoundField.Name = "_attackSoundField";
			this._attackSoundField.NameString = "Attack sounds:";
			this._attackSoundField.Value = false;
			this._attackSoundField.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._attackSoundField_ValueChanged);
			// 
			// _attackSoundView
			// 
			resources.ApplyResources(this._attackSoundView, "_attackSoundView");
			this._attackSoundView.AllowUserToAddRows = false;
			this._attackSoundView.AllowUserToDeleteRows = false;
			this._attackSoundView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._attackSoundView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._attackSoundView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._attackSoundAngleColumn,
            this._attackSound1IDColumn,
            this._attackSound1DelayColumn,
            this._attackSound2IDColumn,
            this._attackSound2DelayColumn,
            this._attackSound3IDColumn,
            this._attackSound3DelayColumn});
			this._attackSoundView.Name = "_attackSoundView";
			this._attackSoundView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._attackSoundView_CellValidating);
			this._attackSoundView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._attackSoundView_CellValueChanged);
			// 
			// _attackSoundAngleColumn
			// 
			this._attackSoundAngleColumn.FillWeight = 10F;
			resources.ApplyResources(this._attackSoundAngleColumn, "_attackSoundAngleColumn");
			this._attackSoundAngleColumn.Name = "_attackSoundAngleColumn";
			this._attackSoundAngleColumn.ReadOnly = true;
			// 
			// _attackSound1IDColumn
			// 
			this._attackSound1IDColumn.FillWeight = 14F;
			resources.ApplyResources(this._attackSound1IDColumn, "_attackSound1IDColumn");
			this._attackSound1IDColumn.Name = "_attackSound1IDColumn";
			this._attackSound1IDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound1DelayColumn
			// 
			this._attackSound1DelayColumn.FillWeight = 16F;
			resources.ApplyResources(this._attackSound1DelayColumn, "_attackSound1DelayColumn");
			this._attackSound1DelayColumn.Name = "_attackSound1DelayColumn";
			this._attackSound1DelayColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound2IDColumn
			// 
			this._attackSound2IDColumn.FillWeight = 14F;
			resources.ApplyResources(this._attackSound2IDColumn, "_attackSound2IDColumn");
			this._attackSound2IDColumn.Name = "_attackSound2IDColumn";
			this._attackSound2IDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound2DelayColumn
			// 
			this._attackSound2DelayColumn.FillWeight = 16F;
			resources.ApplyResources(this._attackSound2DelayColumn, "_attackSound2DelayColumn");
			this._attackSound2DelayColumn.Name = "_attackSound2DelayColumn";
			this._attackSound2DelayColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound3IDColumn
			// 
			this._attackSound3IDColumn.FillWeight = 14F;
			resources.ApplyResources(this._attackSound3IDColumn, "_attackSound3IDColumn");
			this._attackSound3IDColumn.Name = "_attackSound3IDColumn";
			this._attackSound3IDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _attackSound3DelayColumn
			// 
			this._attackSound3DelayColumn.FillWeight = 16F;
			resources.ApplyResources(this._attackSound3DelayColumn, "_attackSound3DelayColumn");
			this._attackSound3DelayColumn.Name = "_attackSound3DelayColumn";
			this._attackSound3DelayColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// _deltaView
			// 
			resources.ApplyResources(this._deltaView, "_deltaView");
			this._deltaView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._deltaView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._deltaView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._deltaIDColumn,
            this._deltaXColumn,
            this._deltaYColumn});
			this._deltaView.Name = "_deltaView";
			this._deltaView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._deltaView_CellValidating);
			this._deltaView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._deltaView_CellValueChanged);
			this._deltaView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this._deltaView_RowsRemoved);
			// 
			// _deltaIDColumn
			// 
			this._deltaIDColumn.FillWeight = 34F;
			resources.ApplyResources(this._deltaIDColumn, "_deltaIDColumn");
			this._deltaIDColumn.Name = "_deltaIDColumn";
			this._deltaIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _deltaXColumn
			// 
			this._deltaXColumn.FillWeight = 33F;
			resources.ApplyResources(this._deltaXColumn, "_deltaXColumn");
			this._deltaXColumn.Name = "_deltaXColumn";
			this._deltaXColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _deltaYColumn
			// 
			this._deltaYColumn.FillWeight = 33F;
			resources.ApplyResources(this._deltaYColumn, "_deltaYColumn");
			this._deltaYColumn.Name = "_deltaYColumn";
			this._deltaYColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// _unknown3Field
			// 
			resources.ApplyResources(this._unknown3Field, "_unknown3Field");
			this._unknown3Field.Name = "_unknown3Field";
			this._unknown3Field.NameString = "Unknown 3:";
			this._unknown3Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown3Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown3Field_ValueChanged);
			// 
			// _unknown2Field
			// 
			resources.ApplyResources(this._unknown2Field, "_unknown2Field");
			this._unknown2Field.Name = "_unknown2Field";
			this._unknown2Field.NameString = "Unknown 2:";
			this._unknown2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._unknown2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._unknown2Field_ValueChanged);
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
			// _layerField
			// 
			resources.ApplyResources(this._layerField, "_layerField");
			this._layerField.Name = "_layerField";
			this._layerField.NameString = "Layer:";
			this._layerField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._layerField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._layerField_ValueChanged);
			// 
			// _speedField
			// 
			resources.ApplyResources(this._speedField, "_speedField");
			this._speedField.Name = "_speedField";
			this._speedField.NameString = "Speed multiplier:";
			this._speedField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._speedField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._speedField_ValueChanged);
			// 
			// _rainbowField
			// 
			resources.ApplyResources(this._rainbowField, "_rainbowField");
			this._rainbowField.Name = "_rainbowField";
			this._rainbowField.NameString = "Rainbow mode:";
			this._rainbowField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._rainbowField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._rainbowField_ValueChanged);
			// 
			// _mirrorField
			// 
			resources.ApplyResources(this._mirrorField, "_mirrorField");
			this._mirrorField.Name = "_mirrorField";
			this._mirrorField.NameString = "Mirror mode:";
			this._mirrorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._mirrorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._mirrorField_ValueChanged);
			// 
			// _sequenceTypeField
			// 
			resources.ApplyResources(this._sequenceTypeField, "_sequenceTypeField");
			this._sequenceTypeField.Name = "_sequenceTypeField";
			this._sequenceTypeField.NameString = "Sequence type:";
			this._sequenceTypeField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._sequenceTypeField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._sequenceTypeField_ValueChanged);
			// 
			// _coord4Field
			// 
			resources.ApplyResources(this._coord4Field, "_coord4Field");
			this._coord4Field.Name = "_coord4Field";
			this._coord4Field.NameString = "Coordinates 4:";
			this._coord4Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord4Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord4Field_ValueChanged);
			// 
			// _coord3Field
			// 
			resources.ApplyResources(this._coord3Field, "_coord3Field");
			this._coord3Field.Name = "_coord3Field";
			this._coord3Field.NameString = "Coordinates 3:";
			this._coord3Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord3Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord3Field_ValueChanged);
			// 
			// _coord2Field
			// 
			resources.ApplyResources(this._coord2Field, "_coord2Field");
			this._coord2Field.Name = "_coord2Field";
			this._coord2Field.NameString = "Coordinates 2:";
			this._coord2Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord2Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord2Field_ValueChanged);
			// 
			// _coord1Field
			// 
			resources.ApplyResources(this._coord1Field, "_coord1Field");
			this._coord1Field.Name = "_coord1Field";
			this._coord1Field.NameString = "Coordinates 1:";
			this._coord1Field.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._coord1Field.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._coord1Field_ValueChanged);
			// 
			// _playerColorField
			// 
			resources.ApplyResources(this._playerColorField, "_playerColorField");
			this._playerColorField.Name = "_playerColorField";
			this._playerColorField.NameString = "Fixed player color:";
			this._playerColorField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._playerColorField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._playerColorField_ValueChanged);
			// 
			// _soundIDField
			// 
			resources.ApplyResources(this._soundIDField, "_soundIDField");
			this._soundIDField.Name = "_soundIDField";
			this._soundIDField.NameString = "Sound ID:";
			this._soundIDField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._soundIDField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._soundIDField_ValueChanged);
			// 
			// _idField
			// 
			resources.ApplyResources(this._idField, "_idField");
			this._idField.Name = "_idField";
			this._idField.NameString = "ID: ";
			this._idField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._idField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._idField_ValueChanged);
			// 
			// _angleCountField
			// 
			resources.ApplyResources(this._angleCountField, "_angleCountField");
			this._angleCountField.Name = "_angleCountField";
			this._angleCountField.NameString = "Angle count:";
			this._angleCountField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._angleCountField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._angleCountField_ValueChanged);
			// 
			// _replayDelayField
			// 
			resources.ApplyResources(this._replayDelayField, "_replayDelayField");
			this._replayDelayField.Name = "_replayDelayField";
			this._replayDelayField.NameString = "=> Delay before replaying:";
			this._replayDelayField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._replayDelayField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._replayDelayField_ValueChanged);
			// 
			// _replayCheckBox
			// 
			resources.ApplyResources(this._replayCheckBox, "_replayCheckBox");
			this._replayCheckBox.Name = "_replayCheckBox";
			this._replayCheckBox.NameString = "Replay?";
			this._replayCheckBox.Value = false;
			this._replayCheckBox.ValueChanged += new TechTreeEditor.Controls.CheckBoxFieldControl.ValueChangedEventHandler(this._replayCheckBox_ValueChanged);
			// 
			// _frameCountField
			// 
			resources.ApplyResources(this._frameCountField, "_frameCountField");
			this._frameCountField.Name = "_frameCountField";
			this._frameCountField.NameString = "Frame count (per angle):";
			this._frameCountField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._frameCountField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._frameCountField_ValueChanged);
			// 
			// _frameRateField
			// 
			resources.ApplyResources(this._frameRateField, "_frameRateField");
			this._frameRateField.Name = "_frameRateField";
			this._frameRateField.NameString = "Frame display time:";
			this._frameRateField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._frameRateField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._frameRateField_ValueChanged);
			// 
			// _slpField
			// 
			resources.ApplyResources(this._slpField, "_slpField");
			this._slpField.Name = "_slpField";
			this._slpField.NameString = "SLP ID: ";
			this._slpField.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._slpField.ValueChanged += new TechTreeEditor.Controls.NumberFieldControl.ValueChangedEventHandler(this._slpField_ValueChanged);
			// 
			// _name2TextBox
			// 
			resources.ApplyResources(this._name2TextBox, "_name2TextBox");
			this._name2TextBox.Name = "_name2TextBox";
			this._name2TextBox.TextChanged += new System.EventHandler(this._name2TextBox_TextChanged);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _name1TextBox
			// 
			resources.ApplyResources(this._name1TextBox, "_name1TextBox");
			this._name1TextBox.Name = "_name1TextBox";
			this._name1TextBox.TextChanged += new System.EventHandler(this._name1TextBox_TextChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// _filterTextBox
			// 
			resources.ApplyResources(this._filterTextBox, "_filterTextBox");
			this._filterTextBox.Name = "_filterTextBox";
			this._filterTextBox.TextChanged += new System.EventHandler(this._filterTextBox_TextChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _newGraphicButton
			// 
			resources.ApplyResources(this._newGraphicButton, "_newGraphicButton");
			this._newGraphicButton.BackgroundImage = global::TechTreeEditor.Icons.NewGraphic;
			this._newGraphicButton.FlatAppearance.BorderSize = 0;
			this._newGraphicButton.Name = "_newGraphicButton";
			this._newGraphicButton.UseVisualStyleBackColor = true;
			this._newGraphicButton.Click += new System.EventHandler(this._newGraphicButton_Click);
			// 
			// _deleteGraphicButton
			// 
			resources.ApplyResources(this._deleteGraphicButton, "_deleteGraphicButton");
			this._deleteGraphicButton.BackgroundImage = global::TechTreeEditor.Icons.DeleteGraphic;
			this._deleteGraphicButton.FlatAppearance.BorderSize = 0;
			this._deleteGraphicButton.Name = "_deleteGraphicButton";
			this._deleteGraphicButton.UseVisualStyleBackColor = true;
			this._deleteGraphicButton.Click += new System.EventHandler(this._deleteGraphicButton_Click);
			// 
			// _graphicListBox
			// 
			resources.ApplyResources(this._graphicListBox, "_graphicListBox");
			this._graphicListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this._graphicListBox.FormattingEnabled = true;
			this._graphicListBox.Name = "_graphicListBox";
			this._graphicListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this._graphicListBox.SelectedIndexChanged += new System.EventHandler(this._graphicListBox_SelectedIndexChanged);
			// 
			// EditGraphicsForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._mainPanel);
			this.Controls.Add(this._graphicListBox);
			this.Controls.Add(this._bottomPanel);
			this.Name = "EditGraphicsForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditGraphicsForm_FormClosing);
			this._bottomPanel.ResumeLayout(false);
			this._mainPanel.ResumeLayout(false);
			this._mainPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._attackSoundView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._deltaView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel _bottomPanel;
		private System.Windows.Forms.Button _closeButton;
		private DoubleBufferedListBox _graphicListBox;
		private System.Windows.Forms.Panel _mainPanel;
		private System.Windows.Forms.Button _newGraphicButton;
		private System.Windows.Forms.Button _deleteGraphicButton;
		private System.Windows.Forms.TextBox _filterTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _saveButton;
		private System.Windows.Forms.TextBox _name1TextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _name2TextBox;
		private System.Windows.Forms.Label label3;
		private Controls.NumberFieldControl _slpField;
		private Controls.NumberFieldControl _frameRateField;
		private Controls.NumberFieldControl _frameCountField;
		private Controls.CheckBoxFieldControl _replayCheckBox;
		private Controls.NumberFieldControl _replayDelayField;
		private Controls.NumberFieldControl _angleCountField;
		private Controls.NumberFieldControl _idField;
		private Controls.NumberFieldControl _playerColorField;
		private Controls.NumberFieldControl _soundIDField;
		private Controls.NumberFieldControl _coord4Field;
		private Controls.NumberFieldControl _coord3Field;
		private Controls.NumberFieldControl _coord2Field;
		private Controls.NumberFieldControl _coord1Field;
		private Controls.NumberFieldControl _speedField;
		private Controls.NumberFieldControl _rainbowField;
		private Controls.NumberFieldControl _mirrorField;
		private Controls.NumberFieldControl _sequenceTypeField;
		private Controls.NumberFieldControl _unknown3Field;
		private Controls.NumberFieldControl _unknown2Field;
		private Controls.NumberFieldControl _unknown1Field;
		private Controls.NumberFieldControl _layerField;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView _deltaView;
		private System.Windows.Forms.DataGridView _attackSoundView;
		private Controls.CheckBoxFieldControl _attackSoundField;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSoundAngleColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound1IDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound1DelayColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound2IDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound2DelayColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound3IDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _attackSound3DelayColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _deltaIDColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _deltaXColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _deltaYColumn;
	}
}