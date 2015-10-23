using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechTreeEditor
{
	public partial class NewProjectForm : Form
	{
		#region Variablen

		/// <summary>
		/// Der Pfad zur neu erstellten Projektdatei.
		/// </summary>
		private string _projectFilePath = null;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public NewProjectForm()
		{
			// Steuerelemente laden
			InitializeComponent();

			// ToolTip zuweisen
			_dllHelpBox.SetToolTip(_dllTextBox, Strings.ImportDATFile_ToolTip_LanguageFiles);
			_dllHelpBox.InitialDelay = 0;
			_dllHelpBox.ReshowDelay = 0;
			_dllHelpBox.AutomaticDelay = 0;
			_dllHelpBox.ShowAlways = true;
		}

		#endregion

		#region Ereignishandler

		private void _dllButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openDLLDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_dllTextBox.Text = string.Join(";", _openDLLDialog.FileNames);
			}
		}

		private void _interfacDRSButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openInterfacDRSDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_interfacDRSTextBox.Text = _openInterfacDRSDialog.FileName;
			}
		}

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void _finishButton_Click(object sender, EventArgs e)
		{
			// Speicherfenster anzeigen
			if(_saveProjectDialog.ShowDialog() == DialogResult.OK)
			{
				// TODO

			}

			// Fenster schließen
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft den Pfad zur neu erstellten Projektdatei ab.
		/// </summary>
		public string NewProjectFile
		{
			get { return _projectFilePath; }
		}

		#endregion
	}
}
