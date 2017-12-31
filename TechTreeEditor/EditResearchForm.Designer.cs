namespace TechTreeEditor
{
	partial class EditResearchForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditResearchForm));
			this._buildingDependencyGroupBox = new System.Windows.Forms.GroupBox();
			this._buildingDepView = new System.Windows.Forms.DataGridView();
			this._buildingDepViewCountColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this._buildingDepViewNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._closeButton = new System.Windows.Forms.Button();
			this._researchDependencyGroupBox = new System.Windows.Forms.GroupBox();
			this._researchDepView = new System.Windows.Forms.DataGridView();
			this._researchDepViewCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._researchDepViewResearchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label7 = new System.Windows.Forms.Label();
			this._aiNameTextBox = new System.Windows.Forms.TextBox();
			this._buildingDependencyGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._buildingDepView)).BeginInit();
			this._researchDependencyGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._researchDepView)).BeginInit();
			this.SuspendLayout();
			// 
			// _buildingDependencyGroupBox
			// 
			resources.ApplyResources(this._buildingDependencyGroupBox, "_buildingDependencyGroupBox");
			this._buildingDependencyGroupBox.Controls.Add(this._buildingDepView);
			this._buildingDependencyGroupBox.Name = "_buildingDependencyGroupBox";
			this._buildingDependencyGroupBox.TabStop = false;
			// 
			// _buildingDepView
			// 
			resources.ApplyResources(this._buildingDepView, "_buildingDepView");
			this._buildingDepView.AllowUserToAddRows = false;
			this._buildingDepView.AllowUserToDeleteRows = false;
			this._buildingDepView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._buildingDepView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._buildingDepView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._buildingDepViewCountColumn,
            this._buildingDepViewNameColumn});
			this._buildingDepView.Name = "_buildingDepView";
			this._buildingDepView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._buildingDepView_CellValueChanged);
			// 
			// _buildingDepViewCountColumn
			// 
			this._buildingDepViewCountColumn.FillWeight = 20F;
			resources.ApplyResources(this._buildingDepViewCountColumn, "_buildingDepViewCountColumn");
			this._buildingDepViewCountColumn.Name = "_buildingDepViewCountColumn";
			this._buildingDepViewCountColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this._buildingDepViewCountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// _buildingDepViewNameColumn
			// 
			this._buildingDepViewNameColumn.FillWeight = 80F;
			resources.ApplyResources(this._buildingDepViewNameColumn, "_buildingDepViewNameColumn");
			this._buildingDepViewNameColumn.Name = "_buildingDepViewNameColumn";
			this._buildingDepViewNameColumn.ReadOnly = true;
			// 
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// _researchDependencyGroupBox
			// 
			resources.ApplyResources(this._researchDependencyGroupBox, "_researchDependencyGroupBox");
			this._researchDependencyGroupBox.Controls.Add(this._researchDepView);
			this._researchDependencyGroupBox.Name = "_researchDependencyGroupBox";
			this._researchDependencyGroupBox.TabStop = false;
			// 
			// _researchDepView
			// 
			resources.ApplyResources(this._researchDepView, "_researchDepView");
			this._researchDepView.AllowUserToAddRows = false;
			this._researchDepView.AllowUserToDeleteRows = false;
			this._researchDepView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._researchDepView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._researchDepView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._researchDepViewCountColumn,
            this._researchDepViewResearchColumn});
			this._researchDepView.Name = "_researchDepView";
			this._researchDepView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._researchDepView_CellValueChanged);
			// 
			// _researchDepViewCountColumn
			// 
			this._researchDepViewCountColumn.FillWeight = 25F;
			resources.ApplyResources(this._researchDepViewCountColumn, "_researchDepViewCountColumn");
			this._researchDepViewCountColumn.Name = "_researchDepViewCountColumn";
			this._researchDepViewCountColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// _researchDepViewResearchColumn
			// 
			this._researchDepViewResearchColumn.FillWeight = 75F;
			resources.ApplyResources(this._researchDepViewResearchColumn, "_researchDepViewResearchColumn");
			this._researchDepViewResearchColumn.Name = "_researchDepViewResearchColumn";
			this._researchDepViewResearchColumn.ReadOnly = true;
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// _aiNameTextBox
			// 
			resources.ApplyResources(this._aiNameTextBox, "_aiNameTextBox");
			this._aiNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this._aiNameTextBox.Name = "_aiNameTextBox";
			this._aiNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._aiNameTextBox_Validating);
			// 
			// EditResearchForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label7);
			this.Controls.Add(this._aiNameTextBox);
			this.Controls.Add(this._researchDependencyGroupBox);
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._buildingDependencyGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "EditResearchForm";
			this._buildingDependencyGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._buildingDepView)).EndInit();
			this._researchDependencyGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._researchDepView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox _buildingDependencyGroupBox;
		private System.Windows.Forms.DataGridView _buildingDepView;
		private System.Windows.Forms.Button _closeButton;
		private System.Windows.Forms.GroupBox _researchDependencyGroupBox;
		private System.Windows.Forms.DataGridView _researchDepView;
		private System.Windows.Forms.DataGridViewTextBoxColumn _researchDepViewCountColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _researchDepViewResearchColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn _buildingDepViewCountColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _buildingDepViewNameColumn;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox _aiNameTextBox;
	}
}