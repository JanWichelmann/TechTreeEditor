namespace X2AddOnTechTreeEditor
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
			this._buildingDepViewCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._buildingDepViewNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._closeButton = new System.Windows.Forms.Button();
			this._buildingDependencyGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._buildingDepView)).BeginInit();
			this.SuspendLayout();
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
			// _closeButton
			// 
			resources.ApplyResources(this._closeButton, "_closeButton");
			this._closeButton.Name = "_closeButton";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// EditResearchForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._buildingDependencyGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "EditResearchForm";
			this._buildingDependencyGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._buildingDepView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _buildingDependencyGroupBox;
		private System.Windows.Forms.DataGridView _buildingDepView;
		private System.Windows.Forms.DataGridViewTextBoxColumn _buildingDepViewCountColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _buildingDepViewNameColumn;
		private System.Windows.Forms.Button _closeButton;
	}
}