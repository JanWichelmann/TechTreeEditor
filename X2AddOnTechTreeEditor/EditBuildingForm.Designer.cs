namespace X2AddOnTechTreeEditor
{
	partial class EditBuildingForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBuildingForm));
			this._ageUpgradeGroupBox = new System.Windows.Forms.GroupBox();
			this._ageUpgradeComboBox = new System.Windows.Forms.ComboBox();
			this._ageUpgradeListBox = new System.Windows.Forms.CheckedListBox();
			this._otherUnitsGroupBox = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this._transformUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this._headUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this._stackUnitComboBox = new System.Windows.Forms.ComboBox();
			this._projDuplUnitComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._projUnitComboBox = new System.Windows.Forms.ComboBox();
			this._buildingDependencyGroupBox = new System.Windows.Forms.GroupBox();
			this._buildingDepView = new System.Windows.Forms.DataGridView();
			this._buildingDepViewCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._buildingDepViewNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._childrenGroupBox = new System.Windows.Forms.GroupBox();
			this._childrenView = new System.Windows.Forms.DataGridView();
			this._childrenViewButtonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._childrenViewChildColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._annexGroupBox = new System.Windows.Forms.GroupBox();
			this._annex4YBox = new System.Windows.Forms.NumericUpDown();
			this._annex4XBox = new System.Windows.Forms.NumericUpDown();
			this._annex4ComboBox = new System.Windows.Forms.ComboBox();
			this._annex4CheckBox = new System.Windows.Forms.CheckBox();
			this._annex3YBox = new System.Windows.Forms.NumericUpDown();
			this._annex3XBox = new System.Windows.Forms.NumericUpDown();
			this._annex3ComboBox = new System.Windows.Forms.ComboBox();
			this._annex3CheckBox = new System.Windows.Forms.CheckBox();
			this._annex2YBox = new System.Windows.Forms.NumericUpDown();
			this._annex2XBox = new System.Windows.Forms.NumericUpDown();
			this._annex2ComboBox = new System.Windows.Forms.ComboBox();
			this._annex2CheckBox = new System.Windows.Forms.CheckBox();
			this._annex1YBox = new System.Windows.Forms.NumericUpDown();
			this._annex1XBox = new System.Windows.Forms.NumericUpDown();
			this._annex1ComboBox = new System.Windows.Forms.ComboBox();
			this._annex1CheckBox = new System.Windows.Forms.CheckBox();
			this._closeButton = new System.Windows.Forms.Button();
			this._ageUpgradeGroupBox.SuspendLayout();
			this._otherUnitsGroupBox.SuspendLayout();
			this._buildingDependencyGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._buildingDepView)).BeginInit();
			this._childrenGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._childrenView)).BeginInit();
			this._annexGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._annex4YBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._annex4XBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._annex3YBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._annex3XBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._annex2YBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._annex2XBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._annex1YBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._annex1XBox)).BeginInit();
			this.SuspendLayout();
			// 
			// _ageUpgradeGroupBox
			// 
			this._ageUpgradeGroupBox.Controls.Add(this._ageUpgradeComboBox);
			this._ageUpgradeGroupBox.Controls.Add(this._ageUpgradeListBox);
			resources.ApplyResources(this._ageUpgradeGroupBox, "_ageUpgradeGroupBox");
			this._ageUpgradeGroupBox.Name = "_ageUpgradeGroupBox";
			this._ageUpgradeGroupBox.TabStop = false;
			// 
			// _ageUpgradeComboBox
			// 
			this._ageUpgradeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this._ageUpgradeComboBox, "_ageUpgradeComboBox");
			this._ageUpgradeComboBox.FormattingEnabled = true;
			this._ageUpgradeComboBox.Name = "_ageUpgradeComboBox";
			this._ageUpgradeComboBox.Sorted = true;
			this._ageUpgradeComboBox.SelectedIndexChanged += new System.EventHandler(this._ageUpgradeComboBox_SelectedIndexChanged);
			// 
			// _ageUpgradeListBox
			// 
			this._ageUpgradeListBox.FormattingEnabled = true;
			resources.ApplyResources(this._ageUpgradeListBox, "_ageUpgradeListBox");
			this._ageUpgradeListBox.Name = "_ageUpgradeListBox";
			this._ageUpgradeListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this._ageUpgradeListBox_ItemCheck);
			this._ageUpgradeListBox.SelectedIndexChanged += new System.EventHandler(this._ageUpgradeListBox_SelectedIndexChanged);
			// 
			// _otherUnitsGroupBox
			// 
			this._otherUnitsGroupBox.Controls.Add(this.label5);
			this._otherUnitsGroupBox.Controls.Add(this._transformUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this.label4);
			this._otherUnitsGroupBox.Controls.Add(this._headUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this.label3);
			this._otherUnitsGroupBox.Controls.Add(this._stackUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this._projDuplUnitComboBox);
			this._otherUnitsGroupBox.Controls.Add(this.label2);
			this._otherUnitsGroupBox.Controls.Add(this.label1);
			this._otherUnitsGroupBox.Controls.Add(this._projUnitComboBox);
			resources.ApplyResources(this._otherUnitsGroupBox, "_otherUnitsGroupBox");
			this._otherUnitsGroupBox.Name = "_otherUnitsGroupBox";
			this._otherUnitsGroupBox.TabStop = false;
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// _transformUnitComboBox
			// 
			this._transformUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._transformUnitComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._transformUnitComboBox, "_transformUnitComboBox");
			this._transformUnitComboBox.Name = "_transformUnitComboBox";
			this._transformUnitComboBox.Sorted = true;
			this._transformUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._transformUnitComboBox_SelectedIndexChanged);
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// _headUnitComboBox
			// 
			this._headUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._headUnitComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._headUnitComboBox, "_headUnitComboBox");
			this._headUnitComboBox.Name = "_headUnitComboBox";
			this._headUnitComboBox.Sorted = true;
			this._headUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._headUnitComboBox_SelectedIndexChanged);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _stackUnitComboBox
			// 
			this._stackUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._stackUnitComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._stackUnitComboBox, "_stackUnitComboBox");
			this._stackUnitComboBox.Name = "_stackUnitComboBox";
			this._stackUnitComboBox.Sorted = true;
			this._stackUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._stackUnitComboBox_SelectedIndexChanged);
			// 
			// _projDuplUnitComboBox
			// 
			this._projDuplUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._projDuplUnitComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._projDuplUnitComboBox, "_projDuplUnitComboBox");
			this._projDuplUnitComboBox.Name = "_projDuplUnitComboBox";
			this._projDuplUnitComboBox.Sorted = true;
			this._projDuplUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._projDuplUnitComboBox_SelectedIndexChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _projUnitComboBox
			// 
			this._projUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._projUnitComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._projUnitComboBox, "_projUnitComboBox");
			this._projUnitComboBox.Name = "_projUnitComboBox";
			this._projUnitComboBox.Sorted = true;
			this._projUnitComboBox.SelectedIndexChanged += new System.EventHandler(this._projUnitComboBox_SelectedIndexChanged);
			// 
			// _buildingDependencyGroupBox
			// 
			this._buildingDependencyGroupBox.Controls.Add(this._buildingDepView);
			resources.ApplyResources(this._buildingDependencyGroupBox, "_buildingDependencyGroupBox");
			this._buildingDependencyGroupBox.Name = "_buildingDependencyGroupBox";
			this._buildingDependencyGroupBox.TabStop = false;
			// 
			// _buildingDepView
			// 
			this._buildingDepView.AllowUserToAddRows = false;
			this._buildingDepView.AllowUserToDeleteRows = false;
			this._buildingDepView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._buildingDepView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._buildingDepView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._buildingDepViewCountColumn,
            this._buildingDepViewNameColumn});
			resources.ApplyResources(this._buildingDepView, "_buildingDepView");
			this._buildingDepView.Name = "_buildingDepView";
			this._buildingDepView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._buildingDepView_CellValidating);
			// 
			// _buildingDepViewCountColumn
			// 
			this._buildingDepViewCountColumn.FillWeight = 20F;
			resources.ApplyResources(this._buildingDepViewCountColumn, "_buildingDepViewCountColumn");
			this._buildingDepViewCountColumn.Name = "_buildingDepViewCountColumn";
			// 
			// _buildingDepViewNameColumn
			// 
			this._buildingDepViewNameColumn.FillWeight = 80F;
			resources.ApplyResources(this._buildingDepViewNameColumn, "_buildingDepViewNameColumn");
			this._buildingDepViewNameColumn.Name = "_buildingDepViewNameColumn";
			this._buildingDepViewNameColumn.ReadOnly = true;
			// 
			// _childrenGroupBox
			// 
			this._childrenGroupBox.Controls.Add(this._childrenView);
			resources.ApplyResources(this._childrenGroupBox, "_childrenGroupBox");
			this._childrenGroupBox.Name = "_childrenGroupBox";
			this._childrenGroupBox.TabStop = false;
			// 
			// _childrenView
			// 
			this._childrenView.AllowUserToAddRows = false;
			this._childrenView.AllowUserToDeleteRows = false;
			this._childrenView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._childrenView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._childrenView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._childrenViewButtonColumn,
            this._childrenViewChildColumn});
			resources.ApplyResources(this._childrenView, "_childrenView");
			this._childrenView.Name = "_childrenView";
			this._childrenView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this._childrenView_CellValidating);
			// 
			// _childrenViewButtonColumn
			// 
			this._childrenViewButtonColumn.FillWeight = 20F;
			resources.ApplyResources(this._childrenViewButtonColumn, "_childrenViewButtonColumn");
			this._childrenViewButtonColumn.Name = "_childrenViewButtonColumn";
			// 
			// _childrenViewChildColumn
			// 
			this._childrenViewChildColumn.FillWeight = 80F;
			resources.ApplyResources(this._childrenViewChildColumn, "_childrenViewChildColumn");
			this._childrenViewChildColumn.Name = "_childrenViewChildColumn";
			this._childrenViewChildColumn.ReadOnly = true;
			// 
			// _annexGroupBox
			// 
			this._annexGroupBox.Controls.Add(this._annex4YBox);
			this._annexGroupBox.Controls.Add(this._annex4XBox);
			this._annexGroupBox.Controls.Add(this._annex4ComboBox);
			this._annexGroupBox.Controls.Add(this._annex4CheckBox);
			this._annexGroupBox.Controls.Add(this._annex3YBox);
			this._annexGroupBox.Controls.Add(this._annex3XBox);
			this._annexGroupBox.Controls.Add(this._annex3ComboBox);
			this._annexGroupBox.Controls.Add(this._annex3CheckBox);
			this._annexGroupBox.Controls.Add(this._annex2YBox);
			this._annexGroupBox.Controls.Add(this._annex2XBox);
			this._annexGroupBox.Controls.Add(this._annex2ComboBox);
			this._annexGroupBox.Controls.Add(this._annex2CheckBox);
			this._annexGroupBox.Controls.Add(this._annex1YBox);
			this._annexGroupBox.Controls.Add(this._annex1XBox);
			this._annexGroupBox.Controls.Add(this._annex1ComboBox);
			this._annexGroupBox.Controls.Add(this._annex1CheckBox);
			resources.ApplyResources(this._annexGroupBox, "_annexGroupBox");
			this._annexGroupBox.Name = "_annexGroupBox";
			this._annexGroupBox.TabStop = false;
			// 
			// _annex4YBox
			// 
			this._annex4YBox.DecimalPlaces = 1;
			this._annex4YBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex4YBox, "_annex4YBox");
			this._annex4YBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex4YBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex4YBox.Name = "_annex4YBox";
			this._annex4YBox.ValueChanged += new System.EventHandler(this._annex4YBox_ValueChanged);
			// 
			// _annex4XBox
			// 
			this._annex4XBox.DecimalPlaces = 1;
			this._annex4XBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex4XBox, "_annex4XBox");
			this._annex4XBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex4XBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex4XBox.Name = "_annex4XBox";
			this._annex4XBox.ValueChanged += new System.EventHandler(this._annex4XBox_ValueChanged);
			// 
			// _annex4ComboBox
			// 
			this._annex4ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._annex4ComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._annex4ComboBox, "_annex4ComboBox");
			this._annex4ComboBox.Name = "_annex4ComboBox";
			this._annex4ComboBox.Sorted = true;
			this._annex4ComboBox.SelectedIndexChanged += new System.EventHandler(this._annex4ComboBox_SelectedIndexChanged);
			// 
			// _annex4CheckBox
			// 
			resources.ApplyResources(this._annex4CheckBox, "_annex4CheckBox");
			this._annex4CheckBox.Checked = true;
			this._annex4CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._annex4CheckBox.Name = "_annex4CheckBox";
			this._annex4CheckBox.UseVisualStyleBackColor = true;
			this._annex4CheckBox.CheckedChanged += new System.EventHandler(this._annex4CheckBox_CheckedChanged);
			// 
			// _annex3YBox
			// 
			this._annex3YBox.DecimalPlaces = 1;
			this._annex3YBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex3YBox, "_annex3YBox");
			this._annex3YBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex3YBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex3YBox.Name = "_annex3YBox";
			this._annex3YBox.ValueChanged += new System.EventHandler(this._annex3YBox_ValueChanged);
			// 
			// _annex3XBox
			// 
			this._annex3XBox.DecimalPlaces = 1;
			this._annex3XBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex3XBox, "_annex3XBox");
			this._annex3XBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex3XBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex3XBox.Name = "_annex3XBox";
			this._annex3XBox.ValueChanged += new System.EventHandler(this._annex3XBox_ValueChanged);
			// 
			// _annex3ComboBox
			// 
			this._annex3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._annex3ComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._annex3ComboBox, "_annex3ComboBox");
			this._annex3ComboBox.Name = "_annex3ComboBox";
			this._annex3ComboBox.Sorted = true;
			this._annex3ComboBox.SelectedIndexChanged += new System.EventHandler(this._annex3ComboBox_SelectedIndexChanged);
			// 
			// _annex3CheckBox
			// 
			resources.ApplyResources(this._annex3CheckBox, "_annex3CheckBox");
			this._annex3CheckBox.Checked = true;
			this._annex3CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._annex3CheckBox.Name = "_annex3CheckBox";
			this._annex3CheckBox.UseVisualStyleBackColor = true;
			this._annex3CheckBox.CheckedChanged += new System.EventHandler(this._annex3CheckBox_CheckedChanged);
			// 
			// _annex2YBox
			// 
			this._annex2YBox.DecimalPlaces = 1;
			this._annex2YBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex2YBox, "_annex2YBox");
			this._annex2YBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex2YBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex2YBox.Name = "_annex2YBox";
			this._annex2YBox.ValueChanged += new System.EventHandler(this._annex2YBox_ValueChanged);
			// 
			// _annex2XBox
			// 
			this._annex2XBox.DecimalPlaces = 1;
			this._annex2XBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex2XBox, "_annex2XBox");
			this._annex2XBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex2XBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex2XBox.Name = "_annex2XBox";
			this._annex2XBox.ValueChanged += new System.EventHandler(this._annex2XBox_ValueChanged);
			// 
			// _annex2ComboBox
			// 
			this._annex2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._annex2ComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._annex2ComboBox, "_annex2ComboBox");
			this._annex2ComboBox.Name = "_annex2ComboBox";
			this._annex2ComboBox.Sorted = true;
			this._annex2ComboBox.SelectedIndexChanged += new System.EventHandler(this._annex2ComboBox_SelectedIndexChanged);
			// 
			// _annex2CheckBox
			// 
			resources.ApplyResources(this._annex2CheckBox, "_annex2CheckBox");
			this._annex2CheckBox.Checked = true;
			this._annex2CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._annex2CheckBox.Name = "_annex2CheckBox";
			this._annex2CheckBox.UseVisualStyleBackColor = true;
			this._annex2CheckBox.CheckedChanged += new System.EventHandler(this._annex2CheckBox_CheckedChanged);
			// 
			// _annex1YBox
			// 
			this._annex1YBox.DecimalPlaces = 1;
			this._annex1YBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex1YBox, "_annex1YBox");
			this._annex1YBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex1YBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex1YBox.Name = "_annex1YBox";
			this._annex1YBox.ValueChanged += new System.EventHandler(this._annex1YBox_ValueChanged);
			// 
			// _annex1XBox
			// 
			this._annex1XBox.DecimalPlaces = 1;
			this._annex1XBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			resources.ApplyResources(this._annex1XBox, "_annex1XBox");
			this._annex1XBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._annex1XBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this._annex1XBox.Name = "_annex1XBox";
			this._annex1XBox.ValueChanged += new System.EventHandler(this._annex1XBox_ValueChanged);
			// 
			// _annex1ComboBox
			// 
			this._annex1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._annex1ComboBox.FormattingEnabled = true;
			resources.ApplyResources(this._annex1ComboBox, "_annex1ComboBox");
			this._annex1ComboBox.Name = "_annex1ComboBox";
			this._annex1ComboBox.Sorted = true;
			this._annex1ComboBox.SelectedIndexChanged += new System.EventHandler(this._annex1ComboBox_SelectedIndexChanged);
			// 
			// _annex1CheckBox
			// 
			resources.ApplyResources(this._annex1CheckBox, "_annex1CheckBox");
			this._annex1CheckBox.Checked = true;
			this._annex1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._annex1CheckBox.Name = "_annex1CheckBox";
			this._annex1CheckBox.UseVisualStyleBackColor = true;
			this._annex1CheckBox.CheckedChanged += new System.EventHandler(this._annex1CheckBox_CheckedChanged);
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// EditBuildingForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._annexGroupBox);
			this.Controls.Add(this._childrenGroupBox);
			this.Controls.Add(this._buildingDependencyGroupBox);
			this.Controls.Add(this._otherUnitsGroupBox);
			this.Controls.Add(this._ageUpgradeGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "EditBuildingForm";
			this._ageUpgradeGroupBox.ResumeLayout(false);
			this._otherUnitsGroupBox.ResumeLayout(false);
			this._otherUnitsGroupBox.PerformLayout();
			this._buildingDependencyGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._buildingDepView)).EndInit();
			this._childrenGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._childrenView)).EndInit();
			this._annexGroupBox.ResumeLayout(false);
			this._annexGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._annex4YBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._annex4XBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._annex3YBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._annex3XBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._annex2YBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._annex2XBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._annex1YBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._annex1XBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _ageUpgradeGroupBox;
		private System.Windows.Forms.CheckedListBox _ageUpgradeListBox;
		private System.Windows.Forms.ComboBox _ageUpgradeComboBox;
		private System.Windows.Forms.GroupBox _otherUnitsGroupBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox _stackUnitComboBox;
		private System.Windows.Forms.ComboBox _projDuplUnitComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _projUnitComboBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox _transformUnitComboBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox _headUnitComboBox;
		private System.Windows.Forms.GroupBox _buildingDependencyGroupBox;
		private System.Windows.Forms.DataGridView _buildingDepView;
		private System.Windows.Forms.GroupBox _childrenGroupBox;
		private System.Windows.Forms.DataGridView _childrenView;
		private System.Windows.Forms.DataGridViewTextBoxColumn _buildingDepViewCountColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _buildingDepViewNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _childrenViewButtonColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _childrenViewChildColumn;
		private System.Windows.Forms.GroupBox _annexGroupBox;
		private System.Windows.Forms.CheckBox _annex1CheckBox;
		private System.Windows.Forms.ComboBox _annex1ComboBox;
		private System.Windows.Forms.NumericUpDown _annex1XBox;
		private System.Windows.Forms.NumericUpDown _annex1YBox;
		private System.Windows.Forms.NumericUpDown _annex4YBox;
		private System.Windows.Forms.NumericUpDown _annex4XBox;
		private System.Windows.Forms.ComboBox _annex4ComboBox;
		private System.Windows.Forms.CheckBox _annex4CheckBox;
		private System.Windows.Forms.NumericUpDown _annex3YBox;
		private System.Windows.Forms.NumericUpDown _annex3XBox;
		private System.Windows.Forms.ComboBox _annex3ComboBox;
		private System.Windows.Forms.CheckBox _annex3CheckBox;
		private System.Windows.Forms.NumericUpDown _annex2YBox;
		private System.Windows.Forms.NumericUpDown _annex2XBox;
		private System.Windows.Forms.ComboBox _annex2ComboBox;
		private System.Windows.Forms.CheckBox _annex2CheckBox;
		private System.Windows.Forms.Button _closeButton;
	}
}