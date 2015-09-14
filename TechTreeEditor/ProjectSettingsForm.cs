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
	public partial class ProjectSettingsForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private ProjectSettingsForm()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Projekteinstellungen-Fenster.
		/// </summary>
		/// <param name="projectFile">Das zugrundeliegende Projekt.</param>
		public ProjectSettingsForm(TechTreeFile projectFile)
			: this()
		{
			// Parameter merken
			_projectFile = projectFile;

			// Werte setzen
			_dllTextBox.Text = string.Join(";", ((IEnumerable<string>)_projectFile.LanguageFilePaths).Reverse());
			_interfacDRSTextBox.Text = _projectFile.InterfacDRSPath;
		}

		#endregion Funktionen

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

		private void _okButton_Click(object sender, EventArgs e)
		{
			// Dateipfade aktualisieren
			List<string> languageFileNames = _dllTextBox.Text.Split(';').ToList();
			languageFileNames.RemoveAll(fn => string.IsNullOrEmpty(fn));
			languageFileNames.Reverse();
			_projectFile.LanguageFilePaths = languageFileNames;

			// Interfac-DRS-Pfad merken
			_projectFile.InterfacDRSPath = _interfacDRSTextBox.Text;

			// Fertig
			DialogResult = DialogResult.OK;
			Close();
		}

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			DialogResult = DialogResult.Cancel;
			Close();
		}

		#endregion Ereignishandler
	}
}
