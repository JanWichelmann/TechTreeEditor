using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	public partial class ProjectSettingsForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Die Instanz des Baum-Zeichensteuerelements. Wird bei Änderungen der Zeitalter-Anzahl benötigt.
		/// </summary>
		private RenderControl _renderControl = null;

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
		/// <param name="renderControl">Die Instanz des Baum-Zeichensteuerelements.</param>
		public ProjectSettingsForm(TechTreeFile projectFile, RenderControl renderControl)
			: this()
		{
			// Parameter merken
			_projectFile = projectFile;

			// Werte setzen
			_dllTextBox.Text = string.Join(";", ((IEnumerable<string>)_projectFile.LanguageFilePaths).Reverse());
			_interfacDRSTextBox.Text = _projectFile.InterfacDRSPath;
			_graphicsDRSTextBox.Text = _projectFile.GraphicsDRSPath;
			_techTreeDesignTextBox.Text = _projectFile.TechTreeDesignPath;
			_ageCountField.Value = _projectFile.AgeCount;
			_useNewTechTreeCheckBox.Checked = _projectFile.ExportNewTechTree;
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

		private void _graphicsDRSButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openGraphicsDRSDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_graphicsDRSTextBox.Text = _openGraphicsDRSDialog.FileName;
			}
		}

		private void _techTreeDesignButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openTechTreeDesignDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_techTreeDesignTextBox.Text = _openTechTreeDesignDialog.FileName;
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

			// Graphics-DRS-Pfad merken
			_projectFile.GraphicsDRSPath = _graphicsDRSTextBox.Text;

			// TechTree-Design-Pfad merken
			_projectFile.TechTreeDesignPath = _techTreeDesignTextBox.Text;

			// Zeitalter ändern
			bool ageResearchesCreated = false;
			if((int)_ageCountField.Value != _projectFile.AgeCount)
			{
				// Technologie-Anzahl versuchen zu ändern
				if(!_projectFile.ChangeAgeCount((int)_ageCountField.Value))
					MessageBox.Show(Strings.ProjectSettingsForm_Message_AgeCountError, Strings.ProjectSettingsForm_Message_AgeCountErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

				// Erhöhung?
				else if((int)_ageCountField.Value > _projectFile.AgeCount)
				{
					// Technologie-Slots belegen
					for(int id = 101; id <= 100 + _projectFile.AgeCount; ++id)
						if(_projectFile.Where(elem => elem is TechTreeResearch).Count == 0)
						{
							// Dummy-Technologie erstellen
							TechTreeResearch ageRes = (TechTreeResearch)_projectFile.CreateElement("TechTreeResearch");
							GenieLibrary.DataElements.Research ageResDAT = new GenieLibrary.DataElements.Research()
							{
								Name = "Age-Dummy #" + id + "\0",
								ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
								LanguageDLLName1 = 7000,
								LanguageDLLName2 = 157000,
								LanguageDLLDescription = 8000,
								LanguageDLLHelp = 107000,
								Unknown1 = -1,
								RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 })
							};
							ageResDAT.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
							ageResDAT.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
							ageResDAT.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
							ageRes.DATResearch = ageResDAT;

							// Technologie-ID sollte geschützt sein
							ageRes.Flags = TechTreeElement.ElementFlags.LockID;

							// Technologie in die DAT schreiben
							_projectFile.BasicGenieFile.Researches.Add(ageResDAT);
							ageRes.ID = id;
							ageRes.Name = "Age-Dummy #" + id;

							// Technologie als Elternelement setzen und an den Anfang stellen
							_projectFile.TechTreeParentElements.Insert(0, ageRes);

							// Icon erstellen
							ageRes.CreateIconTexture(_renderControl.LoadIconAsTexture);

							// Es gibt mindestens eine neue Technologie
							ageResearchesCreated = true;
						}
				}
			}

			// Sind neue Technologien erstellt worden?
			if(ageResearchesCreated)
			{
				// Meldung anzeigen
				MessageBox.Show(Strings.ProjectSettingsForm_Message_NewAgeResearches, Strings.ProjectSettingsForm_Message_NewAgeResearches_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Baum aktualisieren
				_renderControl.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
			}

			// TechTree-Exportmodus setzen
			_projectFile.ExportNewTechTree = _useNewTechTreeCheckBox.Checked;

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