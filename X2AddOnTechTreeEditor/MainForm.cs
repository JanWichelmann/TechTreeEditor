using DRSLibrary;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using X2AddOnTechTreeEditor.Resources;

namespace X2AddOnTechTreeEditor
{
	public partial class MainForm : Form
	{
		#region Variablen

		/// <summary>
		/// Gibt an, ob die Daten bereits geladen wurden.
		/// </summary>
		private bool _dataLoaded = false;

		/// <summary>
		/// Die aktuell geöffnete Projektdatei.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Der Pfad zur aktuell geöffneten Projektdatei.
		/// </summary>
		private string _projectFileName = "";

		/// <summary>
		/// Die Interfac-DRS. Enthält u.a. die Icons.
		/// </summary>
		private DRSFile _interfacDRS = null;

		/// <summary>
		/// Die DAT-Kulturen-Namen.
		/// </summary>
		//private List<KeyValuePair<uint, string>> _civs = new List<KeyValuePair<uint, string>>();

		/// <summary>
		/// Die blockierten Elemente der einzelnen Kulturen.
		/// </summary>
		//private Dictionary<int, CivBlock> _civBlocks = new Dictionary<int, CivBlock>();

		/// <summary>
		/// Das aktuell ausgewählte Techtree-Element.
		/// </summary>
		private TechTreeStructure.TechTreeElement _selectedElement = null;

		/// <summary>
		/// Gibt an, ob die letzten Änderungen gespeichert wurden.
		/// </summary>
		private bool _saved = true;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public MainForm()
		{
			/* Sprache ändern (für Debugging)
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");/**/

			// Steuerelemente laden
			InitializeComponent();

			// Fenstertitel setzen
			UpdateFormTitle();
		}

		/// <summary>
		/// Lädt die angegebene Projektdatei.
		/// </summary>
		/// <param name="filename">Die zu ladende Projektdatei.</param>
		private void LoadProject(string filename)
		{
			// Ist noch ein ungespeichertes Projekt geöffnet?
			if(!_saved)
			{
				// Nachfragen
				DialogResult res = MessageBox.Show(Strings.MainForm_Message_CloseProjectSaveChanges, Strings.MainForm_Message_CloseProjectSaveChanges_Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
				if(res == DialogResult.Cancel)
					return;
				else if(res == DialogResult.Yes)
				{
					// Speichern
					SaveProject();
				}
			}

			// Schönen Cursor zeigen
			this.Cursor = Cursors.WaitCursor;

			// Projektdatei laden
			SetStatus(Strings.MainForm_Status_LoadingProjectFile);
			_projectFileName = filename;
			_projectFile = new TechTreeFile(_projectFileName);

			// Icon-Texturen erstellen


			// Daten an Render-Control übergeben
			SetStatus(Strings.MainForm_Status_PreparingTreeRendering);
			_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);

			// Fertig
			_saved = true;
			_dataLoaded = true;
			this.Cursor = Cursors.Default;
			_saveProjectMenuButton.Enabled = true;
			UpdateFormTitle();
			SetStatus(Strings.MainForm_Status_Ready);
		}

		/// <summary>
		/// Speichert das Projekt.
		/// </summary>
		private void SaveProject()
		{
			// Projekt im angegebenen Pfad speichern
			if(_projectFile != null)
			{
				// Speichern
				SetStatus(Strings.MainForm_Status_Saving);
				_projectFile.WriteData(_projectFileName);
				_saved = true;

				// Fenstertitel aktualisieren
				UpdateFormTitle();
				SetStatus(Strings.MainForm_Status_SavingSuccessful);
			}
		}

		/// <summary>
		/// Aktualisiert den Fenstertitel.
		/// </summary>
		private void UpdateFormTitle()
		{
			// Titel-Status-String erstellen
			string titleParameter = "";

			// Ist ein Projekt geladen?
			if(_projectFile != null)
			{
				// Projekt-Pfad anzeigen
				titleParameter += "[" + _projectFileName + "]";

				// Ist das Projekt gespeichert?
				if(!_saved)
				{
					// Symbol zeigen
					titleParameter += " *";
				}
			}

			// Titel aktualisieren
			this.Text = string.Format(Strings.MainForm_Title, titleParameter);
		}

		/// <summary>
		/// Setzt das Status-Label auf den gegebenen Text.
		/// </summary>
		/// <param name="text">Der Status-Text.</param>
		private void SetStatus(string text)
		{
			// Test setzen
			_statusLabel.Text = text;

			// Fenster aktualisieren
			Application.DoEvents();
		}

		#endregion Funktionen

		#region Ereignishandler

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Dummy-Namen anzeigen
			_selectedNameLabel.Text = "";
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			// Schönen Cursor zeigen
			this.Cursor = Cursors.AppStarting;

			// Kulturen-Auswahlliste mit Daten verknüpfen
			/*SetStatus("Lade Kulturenliste...");
			uint i = 0;
			foreach(GenieLibrary.DataElements.Civ civ in _dat.Civs)
			{
				// Zivilisation einfügen
				if(i != 0)
					_civs.Add(new KeyValuePair<uint, string>(i, _languageFiles.GetString(10230u + i)));
				++i;
			}
			_civs.Sort((x, y) => x.Value.CompareTo(y.Value));
			_civSelectComboBox.ComboBox.DataSource = new BindingSource(_civs, null);
			_civSelectComboBox.ComboBox.ValueMember = "Key";
			_civSelectComboBox.ComboBox.DisplayMember = "Value";
			_civSelectComboBox.ComboBox.SelectedIndex = 0;*/

			// Interfac-DRS laden
			SetStatus("Lade Interfac-DRS...");
			_interfacDRS = new DRSFile("interfac.drs");

			// Icons laden
			SetStatus("Lade Icon-SLPs...");
			SLPLoader.Loader _iconsResearches = new SLPLoader.Loader(new IORAMHelper.RAMBuffer(_interfacDRS.GetResourceData(50729)));
			SLPLoader.Loader _iconsUnits = new SLPLoader.Loader(new IORAMHelper.RAMBuffer(_interfacDRS.GetResourceData(50730)));
			SLPLoader.Loader _iconsBuildings = new SLPLoader.Loader(new IORAMHelper.RAMBuffer(_interfacDRS.GetResourceData(50706)));

			// Farb-Palette laden
			SetStatus(Strings.MainForm_Status_LoadingPal);
			BMPLoaderNew.ColorTable _pal50500 = new BMPLoaderNew.ColorTable(new BMPLoaderNew.JASCPalette(new IORAMHelper.RAMBuffer(Properties.Resources.pal50500)));

			// Daten an Render-Control übergeben
			SetStatus(Strings.MainForm_Status_PassRenderData);
			_renderPanel.UpdateIconData(_pal50500, _iconsResearches, _iconsUnits, _iconsBuildings);

			// Fertig
			_dataLoaded = true;
			this.Cursor = Cursors.Default;
			SetStatus(Strings.MainForm_Status_Ready);
		}

		private void _civSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wenn Daten noch nicht geladen sind, nichts tun
			if(!_dataLoaded)
				return;
		}

		private void _renderPanel_SelectionChanged(object sender, RenderControl.SelectionChangedEventArgs e)
		{
			// Element selektieren
			_selectedElement = e.SelectedElement;

			// Ist ein Element ausgewählt?
			if(_selectedElement != null)
			{
				// Name des gewählten Elements anzeigen
				_selectedNameLabel.Text = string.Format(Strings.MainForm_CurrentSelection, _selectedElement.Name);
			}
			else
			{
				// Dummy-Namen anzeigen
				_selectedNameLabel.Text = "";
			}
		}

		private void _renderPanel_MouseClick(object sender, MouseEventArgs e)
		{
			// Kontextmenü immer verbergen
			_techTreeElementContextMenu.Hide();

			// Bei rechtem Mauszeiger Kontextmenü anzeigen
			if(e.Button == MouseButtons.Right && _selectedElement != null)
			{
				// Kontextmenü zeigen an aktueller Mausposition
				_techTreeElementContextMenu.Show(_renderPanel, e.Location);
			}
		}

		private void _importDATMenuButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			ImportDATFile importDialog = new ImportDATFile();
			if(importDialog.ShowDialog() == DialogResult.OK)
			{
				// Projektdatei laden
				LoadProject(importDialog.ProjectFileName);
			}
		}

		private void _openProjectMenuButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openProjectDialog.ShowDialog() == DialogResult.OK)
			{
				// Projekt laden
				LoadProject(_openProjectDialog.FileName);
			}
		}

		private void _exitMenuButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Ist noch ein ungespeichertes Projekt geöffnet?
			if(!_saved)
			{
				// Nachfragen
				DialogResult res = MessageBox.Show(Strings.MainForm_Message_CloseProjectSaveChanges, Strings.MainForm_Message_CloseProjectSaveChanges_Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
				if(res == DialogResult.Cancel)
				{
					// Abbrechen
					e.Cancel = true;
					return;
				}
				else if(res == DialogResult.Yes)
				{
					// Speichern
					SaveProject();
				}
			}
		}

		private void _saveProjectMenuButton_Click(object sender, EventArgs e)
		{
			// Speichern
			SaveProject();
		}

		#endregion Ereignishandler

		#region Strukturen

		/// <summary>
		/// Verwaltet die deaktivierten Elemente einer Kultur.
		/// </summary>
		private class CivBlock
		{
			/// <summary>
			/// Die blockierten Technologien.
			/// </summary>
			public List<int> BlockedResearches;

			/// <summary>
			/// Die blockierten Einheiten / Gebäude.
			/// </summary>
			public List<int> BlockedUnits;

			/// <summary>
			/// Konstruktor.
			/// </summary>
			public CivBlock()
			{
				// Objekte erstellen
				BlockedResearches = new List<int>();
				BlockedUnits = new List<int>();
			}

			/// <summary>
			/// Prüft, ob die angegebene Technologie blockiert ist.
			/// </summary>
			/// <param name="id">Die ID der zu prüfenden Technologie.</param>
			/// <returns></returns>
			public bool ResearchIsBlocked(int id)
			{
				return BlockedResearches.Contains(id);
			}

			/// <summary>
			/// Prüft, ob die angegebene Einheit blockiert ist.
			/// </summary>
			/// <param name="id">Die ID der zu prüfenden Einheit.</param>
			/// <returns></returns>
			public bool UnitIsBlocked(int id)
			{
				return BlockedUnits.Contains(id);
			}
		}

		#endregion Strukturen
	}
}