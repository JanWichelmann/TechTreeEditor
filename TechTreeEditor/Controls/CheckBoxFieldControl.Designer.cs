namespace TechTreeEditor.Controls
{
	partial class CheckBoxFieldControl
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
			this._checkBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// _checkBox
			// 
			this._checkBox.AutoSize = true;
			this._checkBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._checkBox.Location = new System.Drawing.Point(0, 0);
			this._checkBox.Name = "_checkBox";
			this._checkBox.Size = new System.Drawing.Size(182, 20);
			this._checkBox.TabIndex = 2;
			this._checkBox.Text = "Text...";
			this._checkBox.UseVisualStyleBackColor = true;
			this._checkBox.CheckedChanged += new System.EventHandler(this._checkBox_CheckedChanged);
			// 
			// CheckBoxFieldControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._checkBox);
			this.Name = "CheckBoxFieldControl";
			this.Size = new System.Drawing.Size(182, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox _checkBox;

	}
}
