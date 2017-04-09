using DRSLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;
using GenieLibrary;

namespace TechTreeEditor
{
	public sealed partial class MainForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die geladenen Plugins.
		/// </summary>
		private List<IPlugin> _plugins = null;

		/// <summary>
		/// Die Plugin-Kommunikationsschnittstelle.
		/// </summary>
		private PluginCommunicator _communicator = null;

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
		/// Die DAT-Kulturen-Namen.
		/// </summary>
		private List<KeyValuePair<uint, string>> _civs = new List<KeyValuePair<uint, string>>();

		/// <summary>
		/// Das aktuell ausgewählte Techtree-Element.
		/// </summary>
		private TechTreeElement _selectedElement = null;

		/// <summary>
		/// Das aktuell zum Kopieren markierte Techtree-Element.
		/// </summary>
		private TechTreeElement _copyElement = null;

		/// <summary>
		/// Die Konfiguration der aktuell ausgewählten Kultur.
		/// </summary>
		private TechTreeFile.CivTreeConfig _currCivConfig = null;

		/// <summary>
		/// Gibt an, ob die letzten Änderungen gespeichert wurden. TODO: Aktuell halten...
		/// </summary>
		private bool _saved = true;

		/// <summary>
		/// Die aktuell aktive Baum-Operation.
		/// </summary>
		private TreeOperations _currentOperation = TreeOperations.None;

		/// <summary>
		/// Das zuerst ausgewählte Element der aktuell aktiven Operation.
		/// </summary>
		private TechTreeElement _currentOperationFirstSelection = null;

		/// <summary>
		/// Die Farbtabelle zu Rendern von Grafiken.
		/// </summary>
		private BitmapLibrary.ColorTable _pal50500 = null;

		/// <summary>
		/// Der Einheiten-Manager.
		/// </summary>
		private GenieUnitManager _unitManager = null;

		/// <summary>
		/// Das Einheiten-Render-Fenster.
		/// </summary>
		private UnitRenderForm _unitRenderForm = null;

		/// <summary>
		/// Das Grafik-Bearbeitungs-Fenster.
		/// </summary>
		private EditGraphicsForm _editGraphicsForm = null;

		/// <summary>
		/// Die allgemeinen Steuerbuttons der Kultur-Kopier-Leiste für Aktivitäten wie "alle markieren".
		/// </summary>
		private ToolStripButton[] _civCopyBarDefaultButtons;

		/// <summary>
		/// Gibt an, ob gerade Steuerelemente zentral aktualisiert werden. Dies unterdrückt etwaige Änderungs-Ereignisse, die zu unerwünschtem Verhalten führen können.
		/// </summary>
		private bool _updatingControls = false;

		/// <summary>
		/// Die Designdaten des neuen Tech-Trees.
		/// </summary>
		private GenieLibrary.DataElements.TechTreeNew.TechTreeDesign _techTreeDesignData = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public MainForm()
		{
			// Steuerelemente laden
			InitializeComponent();

			// ToolBar-Einstellungen laden
#if !DEBUG
			ToolStripManager.LoadSettings(this, "ToolBarSettings");
#endif

			// Spracheinstellung den Buttons zuweisen
			if(Properties.Settings.Default.Language == "en-US")
				_languageEnglishMenuButton.Checked = true;
			else
				_languageGermanMenuButton.Checked = true;

			// Sonstige Einstellungen laden
			Location = Properties.Settings.Default.MainFormLocation;
			Size = Properties.Settings.Default.MainFormSize;
			WindowState = Properties.Settings.Default.MainFormWindowState;

			// Kultur-Kopier-Leisten-Renderer setzen
			_civCopyBar.Renderer = new CivCopyButtonRenderer();

			// Kultur-Kopier-Leiste-Buttons erstellen
			_civCopyBarDefaultButtons = new ToolStripButton[3];
			{
				// Alles markieren
				ToolStripButton selectAllButton = new ToolStripButton(Strings.MainForm_CopyBar_All);
				selectAllButton.Name = "_civCopyDefaultButtonSelectAll";
				selectAllButton.Margin = new System.Windows.Forms.Padding(1, 0, 0, 1);
				selectAllButton.Click += (sender, e) =>
				{
					// Alle Elemente auswählen
					foreach(var elem in _civCopyBar.Items)
					{
						// Button abrufen
						ToolStripButton button = elem as ToolStripButton;
						if(button != null && button.CheckOnClick)
							button.Checked = true;
					}
				};
				selectAllButton.ToolTipText = Strings.MainForm_CopyBar_All_ToolTip;
				_civCopyBarDefaultButtons[0] = selectAllButton;

				// Nichts markieren
				ToolStripButton deselectAllButton = new ToolStripButton(Strings.MainForm_CopyBar_None);
				deselectAllButton.Name = "_civCopyDefaultButtonDeselectAll";
				deselectAllButton.Margin = new System.Windows.Forms.Padding(1, 0, 0, 1);
				deselectAllButton.Click += (sender, e) =>
				{
					// Alle Elemente auswählen
					foreach(var elem in _civCopyBar.Items)
					{
						// Button abrufen
						ToolStripButton button = elem as ToolStripButton;
						if(button != null && button.CheckOnClick)
							button.Checked = false;
					}
				};
				deselectAllButton.ToolTipText = Strings.MainForm_CopyBar_None_ToolTip;
				_civCopyBarDefaultButtons[1] = deselectAllButton;

				// Invertieren
				ToolStripButton invertAllButton = new ToolStripButton(Strings.MainForm_CopyBar_Invert);
				invertAllButton.Name = "_civCopyDefaultButtonInvertAll";
				invertAllButton.Margin = new System.Windows.Forms.Padding(1, 0, 0, 1);
				invertAllButton.Click += (sender, e) =>
				{
					// Alle Elemente auswählen
					foreach(var elem in _civCopyBar.Items)
					{
						// Button abrufen
						ToolStripButton button = elem as ToolStripButton;
						if(button != null && button.CheckOnClick)
							button.Checked = !button.Checked;
					}
				};
				invertAllButton.ToolTipText = Strings.MainForm_CopyBar_Invert_ToolTip;
				_civCopyBarDefaultButtons[2] = invertAllButton;
			}

			// Plugin-Kommunikator erstellen
			_communicator = new PluginCommunicator(this);

			// Plugins laden
			_plugins = new List<IPlugin>();
			if(Directory.Exists("plugins"))
			{
				// Alle gefundenen DLLs nach passenden Typen absuchen
				foreach(string dll in Directory.GetFiles("plugins", "*.dll"))
				{
					// Assembly laden
					try
					{
						Assembly dllA = Assembly.Load(AssemblyName.GetAssemblyName(dll));
						if(dllA != null)
						{
							// Nach Plugin-Typ suchen
							foreach(Type type in dllA.GetTypes())
								if(!type.IsInterface && !type.IsAbstract && type.GetInterface(typeof(IPlugin).FullName) != null)
								{
									// Plugin-Objekt erstellen und Kommunikator zuweisen
									IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
									plugin.AssignCommunicator(_communicator);

									// Plugin speichern
									_plugins.Add(plugin);
								}
						}
					}
					catch
					{
						// Fehler
						MessageBox.Show("Error loading plugin '" + dll + "'.");
					}
				}
			}

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

			// Projektdatei laden; Arbeitsverzeichnis entsprechend setzen, um relative Pfade korrekt auszulösen
			SetStatus(Strings.MainForm_Status_LoadingProjectFile);
			_projectFileName = filename;
			Directory.SetCurrentDirectory(Path.GetDirectoryName(_projectFileName));
			_projectFile = new TechTreeFile(_projectFileName);

			// Interfac-DRS laden
			SetStatus(Strings.MainForm_Status_LoadingInterfacDRS);
			DRSFile interfacDRS = new DRSFile(_projectFile.InterfacDRSPath);

			// Icons laden
			SetStatus(Strings.MainForm_Status_LoadingIconSLPs);
			SLPLoader.SLPFile _iconsResearches = new SLPLoader.SLPFile(new IORAMHelper.RAMBuffer(interfacDRS.GetResourceData(50729)));
			SLPLoader.SLPFile _iconsUnits = new SLPLoader.SLPFile(new IORAMHelper.RAMBuffer(interfacDRS.GetResourceData(50730)));
			SLPLoader.SLPFile _iconsBuildings = new SLPLoader.SLPFile(new IORAMHelper.RAMBuffer(interfacDRS.GetResourceData(50706)));

			// Farb-Palette laden
			SetStatus(Strings.MainForm_Status_LoadingPal);
			_pal50500 = new BitmapLibrary.ColorTable(new BitmapLibrary.JASCPalette(new IORAMHelper.RAMBuffer(Properties.Resources.pal50500)));

			// Daten an Render-Control übergeben
			SetStatus(Strings.MainForm_Status_PassRenderData);
			_renderPanel.UpdateIconData(_pal50500, _iconsResearches, _iconsUnits, _iconsBuildings);

			// Icon-Texturen erstellen
			_projectFile.TechTreeParentElements.ForEach(p => p.CreateIconTextures(_renderPanel.LoadIconAsTexture));

			// Kulturennamen laden
			SetStatus(Strings.MainForm_Status_LoadingCultureList);
			_civs.Clear();
			for(uint i = 0; i < _projectFile.CivTrees.Count; ++i)
			{
				// Zivilisation einfügen
				if(i == 0)
					_civs.Add(new KeyValuePair<uint, string>(i, "[Gaia]"));
				else
					_civs.Add(new KeyValuePair<uint, string>(i, _projectFile.LanguageFileWrapper.GetString(10230u + i)));
			}
			_civs.Sort((x, y) => x.Value.CompareTo(y.Value));

			// Kulturen-Auswahlliste mit Namen-Array neu verknüpfen
			_civSelectComboBox.ComboBox.DisplayMember = "Value";
			_civSelectComboBox.ComboBox.ValueMember = "Key";
			_civSelectComboBox.ComboBox.DataSource = _civs;
			_civSelectComboBox.ComboBox.SelectedIndex = 0;

			// Einheiten-Manager erstellen
			_unitManager = new GenieUnitManager(_projectFile.BasicGenieFile);

			// Erste Kultur auswählen
			_currCivConfig = _projectFile.CivTrees[(int)_civs[0].Key];
			_unitManager.SelectedCivIndex = (int)_civs[0].Key;
			_projectFile.ApplyCivConfiguration(_currCivConfig);

			// Kultur-Kopier-Leiste füllen
			_civCopyBar.Items.Clear();
			_civCopyBar.Items.AddRange(_civCopyBarDefaultButtons);
			foreach(var civ in _civs)
			{
				// Button erstellen
				ToolStripButton civButton = new ToolStripButton(civ.Value);
				civButton.BackColor = Color.FromArgb(239, 255, 163);
				civButton.Name = "_civCopyButton" + civ.Key.ToString();
				civButton.Margin = new System.Windows.Forms.Padding(1, 0, 0, 1);
				civButton.CheckOnClick = true;
				civButton.CheckedChanged += (sender, e) =>
				{
					// Button abrufen
					ToolStripButton senderButton = (ToolStripButton)sender;

					// Kultur-ID abrufen
					int civID = (int)senderButton.Tag;

					// Kultur-Kopier-Wert setzen
					_unitManager[civID] = senderButton.Checked;
				};
				civButton.ToolTipText = string.Format(Strings.MainForm_ToolTip_AutoCopy, civ.Value);
				civButton.Tag = (int)civ.Key;
				_civCopyBar.Items.Add(civButton);
			}

			// Daten an Render-Control übergeben
			SetStatus(Strings.MainForm_Status_PreparingTreeRendering);
			_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);

			// Projektdaten ggf. an Render-Fenster übergeben
			if(_unitRenderForm != null)
				_unitRenderForm.UpdateProjectFile(_projectFile);

			// Projektdaten an Plugins übergeben
			_plugins.ForEach(p => p.AssignProject(_projectFile));

			// TechTree-Designs laden
			_updatingControls = true;
			_techTreeDesignData = null;
			UpdateTechTreeNodeDesignsInContextMenu();
			_updatingControls = false;

			// Buttons freischalten
			_pluginMenuButton.Enabled = true;
			_saveProjectMenuButton.Enabled = true;
			_saveProjectButton.Enabled = true;
			_exportDATMenuButton.Enabled = true;
			_exportDATButton.Enabled = true;
			_importBalancingFileMenuButton.Enabled = true;
			_civSelectComboBox.Enabled = true;
			_editGraphicsButton.Enabled = true;
			_editCivBoniButton.Enabled = true;
			_editorModeButton.Enabled = true;
			_standardModeButton.Enabled = true;
			_searchTextBox.Enabled = true;
			_newUnitButton.Enabled = true;
			_newBuildingButton.Enabled = true;
			_newResearchButton.Enabled = true;
			_newEyeCandyButton.Enabled = true;
			_newProjectileButton.Enabled = true;
			_newDeadButton.Enabled = true;
			_newLinkButton.Enabled = true;
			_deleteLinkButton.Enabled = true;
			_setNewTechTreeParentButton.Enabled = true;
			_newMakeAvailDepButton.Enabled = true;
			_newSuccResDepButton.Enabled = true;
			_newBuildingDepButton.Enabled = true;
			_deleteDepButton.Enabled = true;
			_deleteElementButton.Enabled = true;
			_civCopyBar.Enabled = true;
			_lockAllIDsMenuButton.Enabled = true;
			_renderScreenshotMenuButton.Enabled = true;
			_projectSettingsMenuButton.Enabled = true;
			_unitRendererMenuButton.Enabled = true;

			// Fertig
			_saved = true;
			_dataLoaded = true;
			this.Cursor = Cursors.Default;
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
				// Existiert die Datei bereits?
				string backupPath = "";
				if(File.Exists(_projectFileName))
				{
					// Sicherungskopie anlegen
					backupPath = Path.ChangeExtension(_projectFileName, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".backup.ttp");
					File.Copy(_projectFileName, backupPath, true);
				}

				// Speichern
				SetStatus(Strings.MainForm_Status_Saving);
				try
				{
					_projectFile.WriteData(_projectFileName);
					_saved = true;
				}
				catch
				{
					// BackUp wiederherstellen und Exception weiterreichen
					if(backupPath != "" && File.Exists(backupPath))
						File.Copy(backupPath, _projectFileName, true);
					throw;
				}
				finally
				{
					// Sicherungskopie ggf. löschen
					if(backupPath != "" && File.Exists(backupPath))
						File.Delete(backupPath);
				}

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
			// Text setzen
			_statusLabel.Text = text;

			// Fenster aktualisieren
			Application.DoEvents();
		}

		/// <summary>
		/// Aktualisiert die aktuelle Operation und zeigt einen entsprechenden Hinweis an.
		/// </summary>
		/// <param name="operation">Die neue aktuelle Operation.</param>
		private void UpdateCurrentOperation(TreeOperations operation)
		{
			// Operation zuweisen
			_currentOperation = operation;

			// Status-Text aktualisieren
			switch(operation)
			{
				case TreeOperations.None:
					SetStatus(Strings.MainForm_Status_Ready);
					break;

				case TreeOperations.NewLinkFirstElement:
					SetStatus(Strings.MainForm_Operation_NewLinkFirstElement);
					break;

				case TreeOperations.NewLinkSecondElement:
					SetStatus(Strings.MainForm_Operation_NewLinkSecondElement);
					break;

				case TreeOperations.DeleteLink:
					SetStatus(Strings.MainForm_Operation_DeleteLink);
					break;

				case TreeOperations.SetNewTechTreeParentFirstElement:
					SetStatus(Strings.MainForm_Operation_SetNewTechTreeParentFirstElement);
					break;

				case TreeOperations.SetNewTechTreeParentSecondElement:
					SetStatus(Strings.MainForm_Operation_SetNewTechTreeParentSecondElement);
					break;

				case TreeOperations.DeleteElement:
					SetStatus(Strings.MainForm_Operation_DeleteElement);
					break;

				case TreeOperations.NewMakeAvailDependencyFirstElement:
					SetStatus(Strings.MainForm_Operation_NewMakeAvailDependencyFirstElement);
					break;

				case TreeOperations.NewMakeAvailDependencySecondElement:
					SetStatus(Strings.MainForm_Operation_NewMakeAvailDependencySecondElement);
					break;

				case TreeOperations.NewSuccessorResearchDependencyFirstElement:
					SetStatus(Strings.MainForm_Operation_NewSuccessorResearchDependencyFirstElement);
					break;

				case TreeOperations.NewSuccessorResearchDependencySecondElement:
					SetStatus(Strings.MainForm_Operation_NewSuccessorResearchDependencySecondElement);
					break;

				case TreeOperations.NewBuildingDependencyFirstElement:
					SetStatus(Strings.MainForm_Operation_NewBuildingDependencyFirstElement);
					break;

				case TreeOperations.NewBuildingDependencySecondElement:
					SetStatus(Strings.MainForm_Operation_NewBuildingDependencySecondElement);
					break;

				case TreeOperations.DeleteDependencyFirstElement:
					SetStatus(Strings.MainForm_Operation_DeleteDependencyFirstElement);
					break;

				case TreeOperations.DeleteDependencySecondElement:
					SetStatus(Strings.MainForm_Operation_DeleteDependencySecondElement);
					break;
			}
		}

		/// <summary>
		/// Setzt das ausgewählte Element als Kopier-Element.
		/// </summary>
		private void CopyElement()
		{
			// Element ausgewählt?
			// Aktuell werden nur Technologien unterstützt, Einheiten müssen über das Menü kopiert werden
			if(_selectedElement != null && _selectedElement.GetType() == typeof(TechTreeResearch))
			{
				// Element merken
				_copyElement = _selectedElement;

				// Einfüge-Buttons freigeben
				_pasteMenuButton.Enabled = true;
				_pasteTreeMenuButton.Enabled = true;
			}
		}

		/// <summary>
		/// Kopiert das aktuell ausgewählte Kopier-Element und fügt es ein.
		/// </summary>
		/// <param name="copyChildren">Gibt an, ob die Kindelemente des Kopier-Elements kopiert oder entfernt werden sollen.</param>
		private void PasteElement(bool copyChildren)
		{
			// Ist ein Kopierelement ausgewählt?
			if(_copyElement != null)
			{
				// Technologie kopieren
				TechTreeResearch newResearch = (_copyElement as TechTreeResearch).Clone(copyChildren, _projectFile, _renderPanel.LoadIconAsTexture);

				// Auswahl zurücksetzen
				newResearch.Selected = false;

				// Ist eine Einheit mit Kindern ausgewählt?
				IChildrenContainer selUnit = _selectedElement as IChildrenContainer;
				TechTreeResearch selResearch = _selectedElement as TechTreeResearch;
				if(selUnit != null)
				{
					// Unterordnen
					selUnit.Children.Add(new Tuple<byte, TechTreeElement>(0, newResearch));
					newResearch.Age = Math.Max(newResearch.Age, ((TechTreeElement)selUnit).Age);
					newResearch.GetChildren().ForEach(c => c.Age = Math.Max(newResearch.Age, c.Age));
				}
				else if(selResearch != null)
				{
					// Unterordnen
					selResearch.Successors.Add(newResearch);
					newResearch.Age = Math.Max(newResearch.Age, selResearch.Age);
					newResearch.GetChildren().ForEach(c => c.Age = Math.Max(newResearch.Age, c.Age));
				}
				else
				{
					// Technologie als Elternelement setzen und an den Anfang stellen
					_projectFile.TechTreeParentElements.Insert(0, newResearch);
				}

				// Baum neu berechnen
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
			}
		}

		/// <summary>
		/// Aktualisiert die Liste der TechTree-Node-Designs im Element-Kontextmenü.
		/// </summary>
		private void UpdateTechTreeNodeDesignsInContextMenu()
		{
			// TechTree-Design ist notwendig
			if(_techTreeDesignData == null)
			{
				// Designdatei laden
				if(string.IsNullOrWhiteSpace(_projectFile.TechTreeDesignPath) || !File.Exists(_projectFile.TechTreeDesignPath))
					return;
				try { _techTreeDesignData = new GenieLibrary.DataElements.TechTreeNew.TechTreeDesign().ReadData(new IORAMHelper.RAMBuffer(_projectFile.TechTreeDesignPath)); }
				catch { return; }

				// Ggf. alte Design-Buttons entfernen
				while(_showInNewTechTreeCheckButton.DropDownItems.Count > 2)
					_showInNewTechTreeCheckButton.DropDownItems.RemoveAt(2);

				// Design-Buttons erstellen und einfügen
				for(int i = 0; i < _techTreeDesignData.NodeBackgrounds.Count; ++i)
				{
					// Design abrufen
					var nodeDesign = _techTreeDesignData.NodeBackgrounds[i];

					// Button erstellen
					ToolStripMenuItem nodeDesignButton = new ToolStripMenuItem();
					nodeDesignButton.CheckOnClick = true;
					nodeDesignButton.Text = $"[{i}] {nodeDesign.Name}";
					nodeDesignButton.Tag = i;
					nodeDesignButton.CheckedChanged += NodeDesignButton_CheckedChanged;
					_showInNewTechTreeCheckButton.DropDownItems.Add(nodeDesignButton);
				}
			}

			// Zum ausgewählten Element passenden Button auswählen
			// Wenn das Design nicht existiert, ist nichts gewählt
			if(_selectedElement != null)
				for(int i = 2; i < _showInNewTechTreeCheckButton.DropDownItems.Count; ++i)
					((ToolStripMenuItem)_showInNewTechTreeCheckButton.DropDownItems[i]).Checked = (_selectedElement.NewTechTreeNodeDesign == i - 2);
		}

		#endregion Funktionen

		#region Ereignishandler

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Dummy-Namen anzeigen
			_selectedNameLabel.Text = "";

			// Plugin-Buttons abrufen
			foreach(IPlugin plugin in _plugins)
			{
				// Menü-Element erstellen
				ToolStripMenuItem menuElem = new ToolStripMenuItem(plugin.Name);
				menuElem.DropDownItems.AddRange(plugin.GetPluginMenu().ToArray());
				_pluginMenuButton.DropDownItems.Add(menuElem);
			}
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			// Bereit
			SetStatus(Strings.MainForm_Status_Ready);
		}

		private void _civSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wenn Daten noch nicht geladen sind, nichts tun
			if(!_dataLoaded)
				return;

			// Daten für die ausgewählte Zivilisation einfügen
			int civID = (int)(uint)_civSelectComboBox.ComboBox.SelectedValue;
			_currCivConfig = _projectFile.CivTrees[civID];
			_unitManager.SelectedCivIndex = civID;
			if(_selectedElement != null)
				_unitManager.GaiaUnit = _selectedElement.Flags.HasFlag(TechTreeElement.ElementFlags.GaiaOnly);
			_projectFile.ApplyCivConfiguration(_currCivConfig);

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _renderPanel_SelectionChanged(object sender, RenderControl.SelectionChangedEventArgs e)
		{
			// Element selektieren
			_selectedElement = e.SelectedElement;

			// Ist ein Element ausgewählt?
			if(_selectedElement != null)
			{
				// Es wird aktualisiert, alle Ereignisse blockieren
				_updatingControls = true;

				// Falls es sich um eine Einheit handelt, ID ändern
				if(_selectedElement is TechTreeUnit)
				{
					// ID im Manager ändern
					_unitManager.SelectedUnitIndex = _selectedElement.ID;
					_unitManager.GaiaUnit = _selectedElement.Flags.HasFlag(TechTreeElement.ElementFlags.GaiaOnly);
					((TechTreeUnit)_selectedElement).DATUnit = _unitManager.SelectedUnit;
				}

				// Name des gewählten Elements anzeigen
				_selectedElement.UpdateName(_projectFile.LanguageFileWrapper);
				_selectedNameLabel.Text = string.Format(Strings.MainForm_CurrentSelection, _selectedElement.Name);

				// Standard-Element-Button und Sortier-Button aktualisieren
				if(_selectedElement.GetType() == typeof(TechTreeBuilding))
				{
					_standardElementCheckButton.Enabled = true;
					_standardElementCheckButton.Checked = ((TechTreeBuilding)_selectedElement).StandardElement;
					_sortChildrenMenuButton.Enabled = true;
					_showUnitInRendererMenuButton.Enabled = true;
				}
				else if(_selectedElement.GetType() == typeof(TechTreeCreatable))
				{
					_standardElementCheckButton.Enabled = true;
					_standardElementCheckButton.Checked = ((TechTreeCreatable)_selectedElement).StandardElement;
					_sortChildrenMenuButton.Enabled = true;
					_showUnitInRendererMenuButton.Enabled = true;
				}
				else
				{
					_standardElementCheckButton.Enabled = false; // Technologien sind immer Standard-Elemente
					_standardElementCheckButton.Checked = false;
					_sortChildrenMenuButton.Enabled = false;
					_showUnitInRendererMenuButton.Enabled = false;
				}

				// Editor-Button aktualisieren
				_showInEditorCheckButton.Enabled = (_selectedElement.GetType() != typeof(TechTreeResearch));
				_showInEditorCheckButton.Checked = _selectedElement.Flags.HasFlag(TechTreeElement.ElementFlags.ShowInEditor) && _showInEditorCheckButton.Enabled;

				// Gaia-Button aktualisieren
				if(_standardElementCheckButton.Checked)
				{
					_gaiaOnlyCheckButton.Enabled = false;
					_gaiaOnlyCheckButton.Checked = false;
				}
				else
				{
					_gaiaOnlyCheckButton.Enabled = (_selectedElement.GetType() != typeof(TechTreeResearch));
					_gaiaOnlyCheckButton.Checked = ((_selectedElement.Flags & TechTreeElement.ElementFlags.GaiaOnly) == TechTreeElement.ElementFlags.GaiaOnly);
				}

				// ID-Sperr-Button aktualisieren
				_lockIDCheckButton.Checked = ((_selectedElement.Flags & TechTreeElement.ElementFlags.LockID) == TechTreeElement.ElementFlags.LockID);

				// Blockier-Button aktualisieren
				_blockForCivCheckButton.Checked = ((_selectedElement.Flags & TechTreeElement.ElementFlags.Blocked) == TechTreeElement.ElementFlags.Blocked);

				// Kostenlos-Button aktualisieren
				if(_blockForCivCheckButton.Checked)
				{
					_freeForCivCheckButton.Enabled = false;
					_freeForCivCheckButton.Checked = false;
				}
				else
				{
					_freeForCivCheckButton.Enabled = (_selectedElement.GetType() == typeof(TechTreeResearch));
					_freeForCivCheckButton.Checked = ((_selectedElement.Flags & TechTreeElement.ElementFlags.Free) == TechTreeElement.ElementFlags.Free);
				}

				// NewTechTree-Buttons aktualisieren
				_showInNewTechTreeCheckButton.Checked = ((_selectedElement.Flags & TechTreeElement.ElementFlags.ShowInNewTechTree) == TechTreeElement.ElementFlags.ShowInNewTechTree);
				_hideIfDisabledInNewTechTreeCheckButton.Checked = ((_selectedElement.Flags & TechTreeElement.ElementFlags.HideInNewTechTreeIfDisabled) == TechTreeElement.ElementFlags.HideInNewTechTreeIfDisabled);
				UpdateTechTreeNodeDesignsInContextMenu();

				// Fertig mit dem kritischen Teil
				_updatingControls = false;

				// Buttons aktivieren
				_ageUpButton.Enabled = true;
				_ageDownButton.Enabled = true;
				_editAttributesButton.Enabled = true;
				_editElementPropertiesButton.Enabled = true;
				_elementLeftButton.Enabled = true;
				_elementRightButton.Enabled = true;
				_copyMenuButton.Enabled = true;
			}
			else
			{
				// Dummy-Namen anzeigen
				_selectedNameLabel.Text = "";

				// Buttons deaktivieren
				_ageUpButton.Enabled = false;
				_ageDownButton.Enabled = false;
				_editAttributesButton.Enabled = false;
				_editElementPropertiesButton.Enabled = false;
				_elementLeftButton.Enabled = false;
				_elementRightButton.Enabled = false;
				_copyMenuButton.Enabled = false;
			}
		}

		private void _renderPanel_MouseClick(object sender, MouseEventArgs e)
		{
			// Wenn Daten noch nicht geladen sind, nichts tun
			if(!_dataLoaded)
				return;

			// Kontextmenü immer verbergen
			_techTreeElementContextMenu.Hide();

			// Bei rechtem Mauszeiger Kontextmenü anzeigen
			if(e.Button == MouseButtons.Right && _selectedElement != null)
			{
				// Kontextmenü zeigen an aktueller Mausposition
				_techTreeElementContextMenu.Show(_renderPanel, e.Location);
			}

			// Bei linkem Mauszeiger ggf. Operation ausführen
			else if(e.Button == MouseButtons.Left)
			{
				// Fallunterscheidung der aktuell aktiven Operation
				switch(_currentOperation)
				{
					// Element unterordnen: Erstes Element (Kindelement)
					case TreeOperations.NewLinkFirstElement:
						{
							// Element speichern
							_currentOperationFirstSelection = _selectedElement;

							// Zweites Element auswählen lassen
							UpdateCurrentOperation(TreeOperations.NewLinkSecondElement);
						}
						break;

					// Element unterordnen: Zweites Element (Elternelement)
					case TreeOperations.NewLinkSecondElement:
						{
							// Änderung durchführen
							// Shift => Gebäude als erschaffbare Einheit unterordnen, nicht als Nachfolger
							_projectFile.CreateElementLink(_selectedElement, _currentOperationFirstSelection, ModifierKeys.HasFlag(Keys.Shift));

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Baum neu berechnen
							_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
						}
						break;

					// Unterordnung löschen
					case TreeOperations.DeleteLink:
						{
							// Änderung durchführen
							_projectFile.DeleteElementLink(_selectedElement);

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Baum neu berechnen
							_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
						}
						break;

					// Alternatives TechTree-Elternelement setzen: Erstes Element (Kindelement)
					case TreeOperations.SetNewTechTreeParentFirstElement:
						{
							// Element speichern
							_currentOperationFirstSelection = _selectedElement;

							// Zweites Element auswählen lassen
							if(_currentOperationFirstSelection != null)
								UpdateCurrentOperation(TreeOperations.SetNewTechTreeParentSecondElement);
						}
						break;

					// Alternatives TechTree-Elternelement setzen: Zweites Element (Elternelement)
					case TreeOperations.SetNewTechTreeParentSecondElement:
						{
							// Änderung durchführen, kann auch ein Null-Element sein
							_currentOperationFirstSelection.AlternateNewTechTreeParentElement = _selectedElement;

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Neuzeichnen
							_renderPanel.Redraw();
						}
						break;

					// Element löschen
					case TreeOperations.DeleteElement:
						{
							// Änderung durchführen
							if(_selectedElement != null && MessageBox.Show(Strings.MainForm_Message_DeleteElement, Strings.MainForm_Message_DeleteElement_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								if(!_projectFile.DestroyElement(_selectedElement))
								{
									// Es wurden noch nicht alle Referenzen entfernt
									MessageBox.Show(Strings.MainForm_Message_StillReferencesExisting, Strings.MainForm_Message_StillReferencesExisting_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
								}

							// Falls es sich um das Kopierelement handelt, dieses leeren
							if(_selectedElement == _copyElement)
								_copyElement = null;

							// Auswahl aktualisieren
							_renderPanel_SelectionChanged(sender, new RenderControl.SelectionChangedEventArgs(null));

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Baum neu berechnen
							_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
						}
						break;

					// Freischalt-Abhängigkeit erstellen: Erstes Element (abhängiges Grundelement)
					case TreeOperations.NewMakeAvailDependencyFirstElement:
						{
							// Wurde überhaupt etwas ausgewählt?
							if(_selectedElement == null)
							{
								// Abbrechen
								UpdateCurrentOperation(TreeOperations.None);
							}
							else
							{
								// Element speichern
								_currentOperationFirstSelection = _selectedElement;

								// Zweites Element auswählen lassen
								UpdateCurrentOperation(TreeOperations.NewMakeAvailDependencySecondElement);
							}
						}
						break;

					// Freischalt-Abhängigkeit erstellen: Zweites Element (referenziertes Element)
					case TreeOperations.NewMakeAvailDependencySecondElement:
						{
							// Änderung durchführen
							if(_selectedElement != null && _selectedElement.GetType() == typeof(TechTreeResearch))
								_projectFile.CreateMakeAvailDependency(_currentOperationFirstSelection, (TechTreeResearch)_selectedElement);

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Neuzeichnen
							_renderPanel.Redraw();
						}
						break;

					// Weiterentwicklungs-Abhängigkeit erstellen: Erstes Element (weiterzuentwickelndes Grundelement)
					case TreeOperations.NewSuccessorResearchDependencyFirstElement:
						{
							// Wurde überhaupt etwas ausgewählt?
							if(_selectedElement == null)
							{
								// Abbrechen
								UpdateCurrentOperation(TreeOperations.None);
							}
							else
							{
								// Element speichern
								_currentOperationFirstSelection = _selectedElement;

								// Zweites Element auswählen lassen
								UpdateCurrentOperation(TreeOperations.NewSuccessorResearchDependencySecondElement);
							}
						}
						break;

					// Weiterentwicklungs-Abhängigkeit erstellen: Zweites Element (weiterentwickelnde Technologie)
					case TreeOperations.NewSuccessorResearchDependencySecondElement:
						{
							// Änderung durchführen
							if(_selectedElement != null && _selectedElement.GetType() == typeof(TechTreeResearch))
								_projectFile.CreateUpgradeDependency(_currentOperationFirstSelection, (TechTreeResearch)_selectedElement);

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Neuzeichnen
							_renderPanel.Redraw();
						}
						break;

					// Gebäude-Abhängigkeit erstellen: Erstes Element (abhängiges Element)
					case TreeOperations.NewBuildingDependencyFirstElement:
						{
							// Wurde überhaupt etwas ausgewählt?
							if(_selectedElement == null)
							{
								// Abbrechen
								UpdateCurrentOperation(TreeOperations.None);
							}
							else
							{
								// Element speichern
								_currentOperationFirstSelection = _selectedElement;

								// Zweites Element auswählen lassen
								UpdateCurrentOperation(TreeOperations.NewBuildingDependencySecondElement);
							}
						}
						break;

					// Gebäude-Abhängigkeit erstellen: Zweites Element (aktivierendes Gebäude)
					case TreeOperations.NewBuildingDependencySecondElement:
						{
							// Änderung durchführen
							if(_selectedElement != null && _selectedElement.GetType() == typeof(TechTreeBuilding))
								_projectFile.CreateBuildingDependency(_currentOperationFirstSelection, (TechTreeBuilding)_selectedElement);

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Neuzeichnen
							_renderPanel.Redraw();
						}
						break;

					// Abhängigkeit löschen: Erstes Element (abhängiges Element)
					case TreeOperations.DeleteDependencyFirstElement:
						{
							// Wurde überhaupt etwas ausgewählt?
							if(_selectedElement == null)
							{
								// Abbrechen
								UpdateCurrentOperation(TreeOperations.None);
							}
							else
							{
								// Element speichern
								_currentOperationFirstSelection = _selectedElement;

								// Zweites Element auswählen lassen
								UpdateCurrentOperation(TreeOperations.DeleteDependencySecondElement);
							}
						}
						break;

					// Abhängigkeit löschen: Zweites Element (referenziertes Element)
					case TreeOperations.DeleteDependencySecondElement:
						{
							// Änderung durchführen
							if(_selectedElement != null)
								_projectFile.DeleteDependency(_currentOperationFirstSelection, _selectedElement);

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Neuzeichnen
							_renderPanel.Redraw();
						}
						break;

					// Neue Einheit erstellen
					// TODO: Das sollte eigentlich nach Typen aufgespalten werden...
					case TreeOperations.NewCreatable:
					case TreeOperations.NewBuilding:
					case TreeOperations.NewEyeCandy:
					case TreeOperations.NewProjectile:
					case TreeOperations.NewDead:
						{
							// Es muss eine Einheit ausgewählt sein
							if(_selectedElement != null)
							{
								// Neue Einheit erstellen
								TechTreeUnit newU = null;
								if(_selectedElement.GetType() == typeof(TechTreeCreatable))
								{
									newU = (TechTreeUnit)_projectFile.CreateElement("TechTreeCreatable");

									// Parameter kopieren
									TechTreeCreatable newUTemp = ((TechTreeCreatable)newU);
									TechTreeCreatable selUTemp = ((TechTreeCreatable)_selectedElement);
									newUTemp.DeadUnit = selUTemp.DeadUnit;
									newUTemp.StandardElement = selUTemp.StandardElement;
									newUTemp.TrackingUnit = selUTemp.TrackingUnit;
									newUTemp.DropSite1Unit = selUTemp.DropSite1Unit;
									newUTemp.DropSite2Unit = selUTemp.DropSite2Unit;
									newUTemp.ProjectileUnit = selUTemp.ProjectileUnit;
									newUTemp.ProjectileDuplicationUnit = selUTemp.ProjectileDuplicationUnit;
								}
								else if(_selectedElement.GetType() == typeof(TechTreeBuilding))
								{
									newU = (TechTreeUnit)_projectFile.CreateElement("TechTreeBuilding");

									// Parameter kopieren
									TechTreeBuilding newUTemp = ((TechTreeBuilding)newU);
									TechTreeBuilding selUTemp = ((TechTreeBuilding)_selectedElement);
									newUTemp.DeadUnit = selUTemp.DeadUnit;
									newUTemp.StandardElement = selUTemp.StandardElement;

									foreach(var au in selUTemp.AgeUpgrades)
										newUTemp.AgeUpgrades.Add(au.Key, au.Value);
									newUTemp.ProjectileUnit = selUTemp.ProjectileUnit;
									newUTemp.ProjectileDuplicationUnit = selUTemp.ProjectileDuplicationUnit;
									newUTemp.StackUnit = selUTemp.StackUnit;
									newUTemp.HeadUnit = selUTemp.HeadUnit;
									newUTemp.TransformUnit = selUTemp.TransformUnit;
									foreach(var annex in selUTemp.AnnexUnits)
										newUTemp.AnnexUnits.Add(annex);
								}
								else if(_selectedElement.GetType() == typeof(TechTreeEyeCandy))
								{
									newU = (TechTreeUnit)_projectFile.CreateElement("TechTreeEyeCandy");
								}
								else if(_selectedElement.GetType() == typeof(TechTreeProjectile))
								{
									newU = (TechTreeUnit)_projectFile.CreateElement("TechTreeProjectile");

									// Parameter kopieren
									((TechTreeProjectile)newU).TrackingUnit = ((TechTreeProjectile)_selectedElement).TrackingUnit;
								}
								else if(_selectedElement.GetType() == typeof(TechTreeDead))
								{
									newU = (TechTreeUnit)_projectFile.CreateElement("TechTreeDead");
								}
								else
									break; // Warum auch immer

								// Flags und allgemeine Eigenschaften kopieren
								newU.Flags = _selectedElement.Flags & ~TechTreeElement.ElementFlags.RenderingFlags;
								newU.Age = _selectedElement.Age;

								// Einheiten-Fähigkeiten kopieren
								int abID = 0;
								foreach(UnitAbility ab in ((TechTreeUnit)_selectedElement).Abilities)
								{
									// Fähigkeit kopieren und ID ändern
									UnitAbility newAb = ab.Clone();
									ab.CommandData.ID = (short)abID++;
									newU.Abilities.Add(newAb);
								}

								// DAT-Einheit auf Basis des ausgewählten Elements erstellen
								GenieLibrary.DataElements.Civ.Unit newUDAT = (GenieLibrary.DataElements.Civ.Unit)((TechTreeUnit)_selectedElement).DATUnit.Clone();
								newU.DATUnit = newUDAT;

								// Neue ID abrufen und DAT-Einheit auf alle Zivilisationen verteilen
								int newUID = _projectFile.BasicGenieFile.UnitHeaders.Count;
								newU.ID = newUID;
								newUDAT.ID1 = newUDAT.ID2 = newUDAT.ID3 = (short)newUID;
								_projectFile.BasicGenieFile.UnitHeaders.Add(new GenieLibrary.DataElements.UnitHeader() { Exists = 1, Commands = newU.Abilities.Select(ab => ab.CommandData).ToList() });
								foreach(var civ in _projectFile.BasicGenieFile.Civs)
								{
									civ.UnitPointers.Add(newUID);
									civ.Units.Add(newUID, (GenieLibrary.DataElements.Civ.Unit)newUDAT.Clone()); // Klonen, da sonst alle Civs in dieselbe DAT-Einheit schreiben
								}

								// Einheit als Elternelement setzen und an den Anfang stellen
								_projectFile.TechTreeParentElements.Insert(0, newU);

								// Icon und Namen erstellen
								newU.CreateIconTexture(_renderPanel.LoadIconAsTexture);
								newU.UpdateName(_projectFile.LanguageFileWrapper);

								// Baum neu berechnen
								_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
							}

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);
						}
						break;
				}
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

		private void _openProjectButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openProjectDialog.ShowDialog() == DialogResult.OK)
			{
				// Projekt laden
				LoadProject(_openProjectDialog.FileName);
			}
		}

		private void _saveProjectMenuButton_Click(object sender, EventArgs e)
		{
			// Speichern
			SaveProject();
		}

		private void _saveProjectButton_Click(object sender, EventArgs e)
		{
			// Speichern
			SaveProject();
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

			// ToolBar-Einstellungen speichern
			ToolStripManager.SaveSettings(this, "ToolBarSettings");

			// Anwendungseinstellungen speichern
			Properties.Settings.Default.MainFormLocation = Location;
			Properties.Settings.Default.MainFormWindowState = WindowState;
			Properties.Settings.Default.MainFormSize = Size;
			Properties.Settings.Default.Save();
		}

		private void _editorModeButton_CheckedChanged(object sender, EventArgs e)
		{
			// Markiert?
			if(_editorModeButton.Checked)
				_standardModeButton.CheckState = CheckState.Unchecked;

			// Render-Modus setzen
			_renderPanel.SetRenderMode(false);
		}

		private void _standardModeButton_CheckedChanged(object sender, EventArgs e)
		{
			// Markiert?
			if(_standardModeButton.Checked)
				_editorModeButton.CheckState = CheckState.Unchecked;

			// Render-Modus setzen
			_renderPanel.SetRenderMode(true);
		}

		private void _techTreeElementContextMenu_MouseEnter(object sender, EventArgs e)
		{
			// Kontextmenü anzeigen lassen
			_techTreeElementContextMenu.AutoClose = false;
		}

		private void _techTreeElementContextMenu_MouseLeave(object sender, EventArgs e)
		{
			// Kontextmenü ausblenden lassen
			_techTreeElementContextMenu.AutoClose = true;
		}

		private void _standardElementCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Flag aktualisieren
				if(_selectedElement.GetType() == typeof(TechTreeBuilding))
					((TechTreeBuilding)_selectedElement).StandardElement = _standardElementCheckButton.Checked;
				else if(_selectedElement.GetType() == typeof(TechTreeCreatable))
					((TechTreeCreatable)_selectedElement).StandardElement = _standardElementCheckButton.Checked;

				// Gaia-Button aktualisieren
				if(_standardElementCheckButton.Checked)
				{
					// Standard-Element-Eigenschaft überstimmt Gaia
					_gaiaOnlyCheckButton.Checked = false;
					_gaiaOnlyCheckButton.Enabled = false;
				}
				else
				{
					// Gaia erlauben
					_gaiaOnlyCheckButton.Enabled = true;
				}
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _showInEditorCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Flag aktualisieren
				if(_showInEditorCheckButton.Checked)
					_selectedElement.Flags |= TechTreeElement.ElementFlags.ShowInEditor;
				else
					_selectedElement.Flags &= ~TechTreeElement.ElementFlags.ShowInEditor;
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _gaiaOnlyCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Flag aktualisieren
				if(_gaiaOnlyCheckButton.Checked)
					_selectedElement.Flags |= TechTreeElement.ElementFlags.GaiaOnly;
				else
					_selectedElement.Flags &= ~TechTreeElement.ElementFlags.GaiaOnly;
				_unitManager.GaiaUnit = _gaiaOnlyCheckButton.Checked;
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _lockIDCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Flag aktualisieren
				if(_lockIDCheckButton.Checked)
					_selectedElement.Flags |= TechTreeElement.ElementFlags.LockID;
				else
					_selectedElement.Flags &= ~TechTreeElement.ElementFlags.LockID;
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _blockForCivCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Kultur-Block-Liste aktualisieren
				if(_blockForCivCheckButton.Checked)
				{
					// Element blockieren
					_unitManager.ForEachCiv(c =>
					{
						// Element noch nicht blockiert?
						if(!_projectFile.CivTrees[c].BlockedElements.Contains(_selectedElement))
							_projectFile.CivTrees[c].BlockedElements.Add(_selectedElement);
					});

					// Element kann nicht mehr kostenlos sein
					_freeForCivCheckButton.Checked = false;
					_freeForCivCheckButton.Enabled = false;
				}
				else
				{
					// Element freigeben
					_unitManager.ForEachCiv(c =>
					{
						// Element blockiert?
						if(_projectFile.CivTrees[c].BlockedElements.Contains(_selectedElement))
							_projectFile.CivTrees[c].BlockedElements.Remove(_selectedElement);
					});

					// Element kann kostenlos sein
					_freeForCivCheckButton.Enabled = true;
				}

				// Flag aktualisieren
				if(_blockForCivCheckButton.Checked)
					_selectedElement.Flags |= TechTreeElement.ElementFlags.Blocked;
				else
					_selectedElement.Flags &= ~TechTreeElement.ElementFlags.Blocked;
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _freeForCivCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null && _selectedElement.GetType() == typeof(TechTreeResearch))
			{
				// Kultur-Kostenlos-Liste aktualisieren
				if(_freeForCivCheckButton.Checked)
				{
					// Element kostenlos machen
					_unitManager.ForEachCiv(c =>
					{
						// Element noch nicht kostenlos?
						if(!_projectFile.CivTrees[c].FreeElements.Contains((TechTreeResearch)_selectedElement))
							_projectFile.CivTrees[c].FreeElements.Add((TechTreeResearch)_selectedElement);
					});
				}
				else
				{
					// Element aus Liste löschen
					_unitManager.ForEachCiv(c =>
					{
						// Element kostenlos?
						if(_projectFile.CivTrees[c].FreeElements.Contains((TechTreeResearch)_selectedElement))
							_projectFile.CivTrees[c].FreeElements.Remove((TechTreeResearch)_selectedElement);
					});
				}

				// Flag aktualisieren
				if(_freeForCivCheckButton.Checked)
					_selectedElement.Flags |= TechTreeElement.ElementFlags.Free;
				else
					_selectedElement.Flags &= ~TechTreeElement.ElementFlags.Free;
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _showInNewTechTreeCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Flag entsprechend setzen
				if(_showInNewTechTreeCheckButton.Checked)
					_selectedElement.Flags |= TechTreeElement.ElementFlags.ShowInNewTechTree;
				else
				{
					// Auch zweiten Button deaktivieren
					_selectedElement.Flags &= ~TechTreeElement.ElementFlags.ShowInNewTechTree;
					_hideIfDisabledInNewTechTreeCheckButton.Checked = false;
				}
				UpdateTechTreeNodeDesignsInContextMenu();
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _hideIfDisabledInNewTechTreeCheckButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aufpassen, dass nicht während eines Update-Vorgangs Änderungen durchgeführt werden
			if(_updatingControls)
				return;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Flag entsprechend setzen
				if(_hideIfDisabledInNewTechTreeCheckButton.Checked)
				{
					// Auch den ersten Button setzen
					_selectedElement.Flags |= TechTreeElement.ElementFlags.HideInNewTechTreeIfDisabled;
					_showInNewTechTreeCheckButton.Checked = true;
				}
				else
					_selectedElement.Flags &= ~TechTreeElement.ElementFlags.HideInNewTechTreeIfDisabled;
				UpdateTechTreeNodeDesignsInContextMenu();
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _newLinkButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewLinkFirstElement);
		}

		private void _deleteLinkButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.DeleteLink);
		}

		private void _setNewTechTreeParentButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.SetNewTechTreeParentFirstElement);
		}

		private void _deleteElementButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.DeleteElement);
		}

		private void _ageUpButton_Click(object sender, EventArgs e)
		{
			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Element verschieben
				_projectFile.MoveElementAge(_selectedElement, -1);

				// Baum neu berechnen
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
			}
		}

		private void _ageDownButton_Click(object sender, EventArgs e)
		{
			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Element verschieben
				_projectFile.MoveElementAge(_selectedElement, 1);

				// Baum neu berechnen
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
			}
		}

		private void _elementLeftButton_Click(object sender, EventArgs e)
		{
			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Alte Position bestimmen
				int elementRenderDistance = _renderPanel.GetElementHorizontalScreenOffset(_selectedElement);

				// Element verschieben
				// Bei Shift-Taste direkt an den Anfang schieben
				TechTreeElement succElement = _projectFile.MoveElementHorizontal(_selectedElement, ((ModifierKeys & Keys.Shift) == Keys.Shift ? -100000 : -1));

				// Baum neu berechnen
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);

				// Scrollen
				if(succElement != null && elementRenderDistance > 0 && elementRenderDistance < _renderPanel.Width)
					_renderPanel.ScrollHorizontal(succElement.CacheBoxPosition.X - _selectedElement.CacheBoxPosition.X);
			}
		}

		private void _elementRightButton_Click(object sender, EventArgs e)
		{
			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Alte Position bestimmen
				int elementRenderDistance = _renderPanel.GetElementHorizontalScreenOffset(_selectedElement);

				// Element verschieben
				// Bei Shift-Taste direkt ans Ende schieben
				TechTreeElement succElement = _projectFile.MoveElementHorizontal(_selectedElement, ((ModifierKeys & Keys.Shift) == Keys.Shift ? 100000 : 1));

				// Baum neu berechnen
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);

				// Scrollen
				if(succElement != null && elementRenderDistance > 0 && elementRenderDistance < _renderPanel.Width)
					_renderPanel.ScrollHorizontal(succElement.CacheBoxPosition.X - _selectedElement.CacheBoxPosition.X);
			}
		}

		private void _newMakeAvailDepButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewMakeAvailDependencyFirstElement);
		}

		private void _newSuccResDepButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewSuccessorResearchDependencyFirstElement);
		}

		private void _newBuildingDepButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewBuildingDependencyFirstElement);
		}

		private void _deleteDepButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.DeleteDependencyFirstElement);
		}

		private void _renderPanel_DoubleClick(object sender, EventArgs e)
		{
			// Ist etwas ausgewählt?
			if(_selectedElement != null)
				if(_selectedElement.GetType() == typeof(TechTreeResearch))
				{
					// Fenster anzeigen
					EditResearchAttributeForm form = new EditResearchAttributeForm(_projectFile, (TechTreeResearch)_selectedElement);
					form.IconChanged += (sender2, e2) =>
					{
						e2.Research.FreeIconTexture();
						e2.Research.CreateIconTexture(_renderPanel.LoadIconAsTexture);
						_renderPanel.Redraw();
					};
					form.ShowDialog();
				}
				else
				{
					// Das Element muss eine Einheit sein => Fenster anzeigen
					EditUnitAttributeForm form = new EditUnitAttributeForm(_projectFile, (TechTreeUnit)_selectedElement, _unitManager);
					form.IconChanged += (sender2, e2) =>
					{
						e2.Unit.FreeIconTexture();
						e2.Unit.CreateIconTexture(_renderPanel.LoadIconAsTexture);
						_renderPanel.Redraw();
					};
					form.ShowDialog();
				}
		}

		private void _editElementPropertiesMenuButton_Click(object sender, EventArgs e)
		{
			// Kontextmenü ausblenden
			_techTreeElementContextMenu.AutoClose = true;
			_techTreeElementContextMenu.Hide();

			// Dasselbe tun wie beim Toolbar-Button
			_editElementPropertiesButton_Click(sender, e);
		}

		private void _editElementPropertiesButton_Click(object sender, EventArgs e)
		{
			// Typ abrufen
			Type elemType = _selectedElement.GetType();

			// Nach Typ vom aktuell ausgewählten Element unterscheiden
			if(elemType == typeof(TechTreeBuilding))
			{
				// Gebäude-Dialog anzeigen
				EditBuildingForm form = new EditBuildingForm(_projectFile, (TechTreeBuilding)_selectedElement);
				form.ShowDialog();
			}
			else if(elemType == typeof(TechTreeCreatable))
			{
				// Einheit-Dialog anzeigen
				EditCreatableForm form = new EditCreatableForm(_projectFile, (TechTreeCreatable)_selectedElement);
				form.ShowDialog();
			}
			else if(elemType == typeof(TechTreeProjectile))
			{
				// Einheit-Dialog anzeigen
				EditProjectileForm form = new EditProjectileForm(_projectFile, (TechTreeProjectile)_selectedElement);
				form.ShowDialog();
			}
			else if(elemType == typeof(TechTreeResearch))
			{
				// Einheit-Dialog anzeigen
				EditResearchForm form = new EditResearchForm(_projectFile, (TechTreeResearch)_selectedElement);
				form.ShowDialog();
			}
		}

		private void _searchTextBox_TextChanged(object sender, EventArgs e)
		{
			// Such-Befehl weitergeben
			_renderPanel.UpdateSearchText(_searchTextBox.Text);
		}

		private void _editAttributesMenuButton_Click(object sender, EventArgs e)
		{
			// Kontextmenü ausblenden
			_techTreeElementContextMenu.AutoClose = true;
			_techTreeElementContextMenu.Hide();

			// Das gleiche tun wie beim Doppelklick
			_renderPanel_DoubleClick(sender, e);
		}

		private void _editAttributesButton_Click(object sender, EventArgs e)
		{
			// Das gleiche tun wie beim Doppelklick
			_renderPanel_DoubleClick(sender, e);
		}

		private void _newUnitButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewCreatable);
		}

		private void _newBuildingButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewBuilding);
		}

		private void _newResearchButton_Click(object sender, EventArgs e)
		{
			// Neue Technologie erstellen
			TechTreeResearch newResearch = (TechTreeResearch)_projectFile.CreateElement("TechTreeResearch");
			GenieLibrary.DataElements.Research newResearchDAT = new GenieLibrary.DataElements.Research()
			{
				Name = "New Research\0",
				ResourceCosts = new List<GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>>(3),
				LanguageDLLName1 = 7000,
				LanguageDLLName2 = 157000,
				LanguageDLLDescription = 8000,
				LanguageDLLHelp = 107000,
				Unknown1 = -1,
				RequiredTechs = new List<short>(new short[] { -1, -1, -1, -1, -1, -1 })
			};
			newResearchDAT.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
			newResearchDAT.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
			newResearchDAT.ResourceCosts.Add(new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>());
			newResearch.DATResearch = newResearchDAT;

			// Technologie in die DAT schreiben
			int newID = _projectFile.BasicGenieFile.Researches.Count;
			_projectFile.BasicGenieFile.Researches.Add(newResearchDAT);
			newResearch.ID = newID;
			newResearch.Name = "New Research";

			// Ist eine Einheit mit Kindern ausgewählt?
			IChildrenContainer selUnit = _selectedElement as IChildrenContainer;
			TechTreeResearch selResearch = _selectedElement as TechTreeResearch;
			if(selUnit != null)
			{
				// Unterordnen
				selUnit.Children.Add(new Tuple<byte, TechTreeElement>(0, newResearch));
				newResearch.Age = Math.Max(newResearch.Age, ((TechTreeElement)selUnit).Age);
			}
			else if(selResearch != null)
			{
				// Unterordnen
				selResearch.Successors.Add(newResearch);
				newResearch.Age = Math.Max(newResearch.Age, selResearch.Age);
			}
			else
			{
				// Technologie als Elternelement setzen und an den Anfang stellen
				_projectFile.TechTreeParentElements.Insert(0, newResearch);
			}

			// Icon erstellen
			newResearch.CreateIconTexture(_renderPanel.LoadIconAsTexture);

			// Baum neu berechnen
			_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements, _projectFile.AgeCount);
		}

		private void _newEyeCandyButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewEyeCandy);
		}

		private void _newProjectileButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewProjectile);
		}

		private void _newDeadButton_Click(object sender, EventArgs e)
		{
			// Operation starten
			UpdateCurrentOperation(TreeOperations.NewDead);
		}

		private void _lockAllIDsMenuButton_Click(object sender, EventArgs e)
		{
			// Nachfragen
			if(MessageBox.Show(Strings.MainForm_Message_LockAllIDs, Strings.MainForm_Message_LockAllIDs_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Elemente durchlaufen und IDs schützen
				_projectFile.Where(elem => true).ForEach(elem => elem.Flags |= TechTreeElement.ElementFlags.LockID);

				// Neuzeichnen
				_renderPanel.Redraw();
			}
		}

		private void _renderPanel_KeyDown(object sender, KeyEventArgs e)
		{
			// Ist ein Element markiert?
			if(_selectedElement != null)
			{
				// Hotkeys für Flags
				if(e.KeyCode == Keys.S && _standardElementCheckButton.Enabled)
					_standardElementCheckButton.Checked = !_standardElementCheckButton.Checked;
				if(e.KeyCode == Keys.E && _showInEditorCheckButton.Enabled)
					_showInEditorCheckButton.Checked = !_showInEditorCheckButton.Checked;
				if(e.KeyCode == Keys.G && _gaiaOnlyCheckButton.Enabled)
					_gaiaOnlyCheckButton.Checked = !_gaiaOnlyCheckButton.Checked;
				if(e.KeyCode == Keys.I && _lockIDCheckButton.Enabled)
					_lockIDCheckButton.Checked = !_lockIDCheckButton.Checked;
				if(e.KeyCode == Keys.B && _blockForCivCheckButton.Enabled)
					_blockForCivCheckButton.Checked = !_blockForCivCheckButton.Checked;
				if(e.KeyCode == Keys.F && _freeForCivCheckButton.Enabled)
					_freeForCivCheckButton.Checked = !_freeForCivCheckButton.Checked;
				if(e.KeyCode == Keys.T && _showInNewTechTreeCheckButton.Enabled)
					_showInNewTechTreeCheckButton.Checked = !_showInNewTechTreeCheckButton.Checked;
				if(e.KeyCode == Keys.H && _hideIfDisabledInNewTechTreeCheckButton.Enabled)
					_hideIfDisabledInNewTechTreeCheckButton.Checked = !_hideIfDisabledInNewTechTreeCheckButton.Checked;

				// Hotkey zum Anzeigen des Attributfensters
				if(e.KeyCode == Keys.Space)
					_editAttributesMenuButton_Click(sender, EventArgs.Empty);

				// Hotkey zum Anzeigen des Baumeigenschaftenfensters
				if(e.KeyCode == Keys.Enter)
					_editElementPropertiesMenuButton_Click(sender, EventArgs.Empty);

				// Hotkey zum Verschieben eines Elements um ein Zeitalter
				if(e.KeyCode == Keys.Down)
					_ageDownButton_Click(sender, e);
				if(e.KeyCode == Keys.Up)
					_ageUpButton_Click(sender, e);

				// Hotkey zum Löschen eines Elements
				if(e.KeyCode == Keys.Delete)
				{
					// Lösch-Operation einleiten und direkt Mausklick auf das Element simulieren, verhindert Code-Duplikation
					UpdateCurrentOperation(TreeOperations.DeleteElement);
					_renderPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, _selectedElement.CacheBoxPosition.X, _selectedElement.CacheBoxPosition.Y, 0));
				}
			}
		}

		private void _exportDATMenuButton_Click(object sender, EventArgs e)
		{
			// Der Toolbar-Button macht dasselbe
			_exportDATButton_Click(sender, e);
		}

		private void _exportDATButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			ExportDATFile exportDialog = new ExportDATFile(_projectFile);
			exportDialog.ShowDialog();

			// Muss das Projekt neu geladen werden?
			if(exportDialog.ProjectReloadNeeded)
			{
				// Fehler abfangen
				try
				{
					// Alten Projektpfad merken
					string oldProjectPath = _projectFileName;

					// Temporäre Datei erstellen
					_projectFileName = Path.GetTempFileName();

					// Projekt darin speichern
					SaveProject();

					// Projekt laden
					LoadProject(_projectFileName);

					// Pfade zurücksetzen
					_projectFileName = oldProjectPath;
					UpdateFormTitle();
				}
				catch(IOException ex)
				{
					// Fehlermeldung
					MessageBox.Show(string.Format(Strings.MainForm_Message_CouldNotReloadProject, ex.Message), Strings.MainForm_Message_CouldNotReloadProject_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void _editCivBoniButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			EditCivBonusesForm bonusDialog = new EditCivBonusesForm(_projectFile, _currCivConfig);
			bonusDialog.ShowDialog();
		}

		private void _editGraphicsButton_Click(object sender, EventArgs e)
		{
			// Grafik-Fenster erstellen
			_editGraphicsForm = new EditGraphicsForm(_projectFile);
			_editGraphicsForm.FormClosed += (sender2, e2) =>
			{
				// Variable löschen und Objekt damit freigeben
				_editGraphicsForm = null;
			};
			_editGraphicsForm.Show();
		}

		private void _copyMenuButton_Click(object sender, EventArgs e)
		{
			// Kopieren
			CopyElement();
		}

		private void _pasteMenuButton_Click(object sender, EventArgs e)
		{
			// Element ohne Kinder einfügen
			PasteElement(false);
		}

		private void _pasteTreeMenuButton_Click(object sender, EventArgs e)
		{
			// Element mit Kindern einfügen
			PasteElement(true);
		}

		private void _renderScreenshotMenuButton_Click(object sender, EventArgs e)
		{
			// Zieldatei abfragen
			if(_renderScreenshotDialog.ShowDialog() == DialogResult.OK)
			{
				// Schönen Cursor zeigen, das wird vermutlich etwas dauern
				this.Cursor = Cursors.WaitCursor;
				SetStatus(Strings.Mainform_Status_RenderingTree);

				// Screenshot erstellen
				_renderPanel.RenderToBitmap(_renderScreenshotDialog.FileName);

				// Fertig
				this.Cursor = Cursors.Default;
				SetStatus(Strings.MainForm_Status_Ready);
			}
		}

		private void _sortChildrenMenuButton_Click(object sender, EventArgs e)
		{
			// Gewähltes Gebäude/gewählte Einheit abrufen
			IChildrenContainer sel = _selectedElement as IChildrenContainer;
			if(sel != null)
			{
				// Kindelemente sortieren
				sel.Children.Sort((e1, e2) => e1.Item1.CompareTo(e2.Item1));
			}

			// Neuzeichnen
			_renderPanel.Redraw();
		}

		private void _projectSettingsButton_Click(object sender, EventArgs e)
		{
			// Einstellungsfenster anzeigen
			ProjectSettingsForm settingsDialog = new ProjectSettingsForm(_projectFile, _renderPanel);
			if(settingsDialog.ShowDialog() == DialogResult.OK)
			{
				// Meldung ausgeben
				if(MessageBox.Show(Strings.MainForm_Message_SettingsReloadProject, Strings.MainForm_Message_SettingsReloadProject_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{
					// Projekt speichern
					SaveProject();

					// Projekt neu laden
					LoadProject(_projectFileName);
				}
			}
		}

		private void _unitRendererMenuButton_Click(object sender, EventArgs e)
		{
			// Render-Fenster schon offen?
			if(_unitRenderForm != null)
			{
				// In den Vordergrund bringen
				_unitRenderForm.BringToFront();
				_unitRenderForm.Focus();
				return;
			}

			// Render-Fenster erstellen
			_unitRenderForm = new UnitRenderForm(_projectFile);
			_unitRenderForm.FormClosed += (sender2, e2) =>
			{
				// Variable löschen und Objekt damit freigeben
				_unitRenderForm = null;
			};
			_unitRenderForm.Show();
		}

		private void _showUnitInRendererMenuButton_Click(object sender, EventArgs e)
		{
			// Kontextmenü ausblenden
			_techTreeElementContextMenu.AutoClose = true;
			_techTreeElementContextMenu.Hide();

			// Gegebenenfalls Fenster anzeigen
			_unitRendererMenuButton_Click(sender, e);

			// Markierte Einheit übergeben
			if(_selectedElement != null && _selectedElement is TechTreeUnit)
				_unitRenderForm.UpdateRenderUnit((TechTreeUnit)_selectedElement);
		}

		private void _infoMenuButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			AboutForm aboutDialog = new AboutForm();
			aboutDialog.ShowDialog();
		}

		private void _newProjectMenuButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen und ggf. neues Projekt laden
			NewProjectForm newProjectForm = new NewProjectForm();
			if(newProjectForm.ShowDialog() == DialogResult.OK)
				LoadProject(newProjectForm.NewProjectFile);
		}

		private void _newProjectButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen und ggf. neues Projekt laden
			NewProjectForm newProjectForm = new NewProjectForm();
			if(newProjectForm.ShowDialog() == DialogResult.OK)
				LoadProject(newProjectForm.NewProjectFile);
		}

		private void _languageGermanMenuButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aktiviert?
			if(_languageGermanMenuButton.Checked)
			{
				// Sprache ändern
				Properties.Settings.Default.Language = "de-DE";

				// Anderen Button deselektieren
				_languageEnglishMenuButton.Checked = false;
			}
			else
				MessageBox.Show("The program has to be restarted to apply the language changes.", "Change language", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void _languageEnglishMenuButton_CheckedChanged(object sender, EventArgs e)
		{
			// Aktiviert?
			if(_languageEnglishMenuButton.Checked)
			{
				// Sprache ändern
				Properties.Settings.Default.Language = "en-US";

				// Anderen Button deselektieren
				_languageGermanMenuButton.Checked = false;
			}
			else
				MessageBox.Show("Zur Übernahme der neuen Spracheinstellung muss das Programm neugestartet werden.", "Sprache ändern", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void NodeDesignButton_CheckedChanged(object sender, EventArgs e)
		{
			// Unnötige Aufrufe des Ereignishandlers verhindern
			if(_updatingControls)
				return;
			_updatingControls = true;

			// Element ausgewählt?
			if(_selectedElement != null)
			{
				// Andere Buttons deselektieren
				for(int i = 2; i < _showInNewTechTreeCheckButton.DropDownItems.Count; ++i)
					if(_showInNewTechTreeCheckButton.DropDownItems[i] != sender)
						((ToolStripMenuItem)_showInNewTechTreeCheckButton.DropDownItems[i]).Checked = false;

				// Auswahl merken
				_selectedElement.NewTechTreeNodeDesign = (int)((ToolStripMenuItem)sender).Tag;
			}

			// Fertig
			_updatingControls = false;
		}

		private void _importBalancingFileMenuButton_Click(object sender, EventArgs e)
		{
			// Dialog zeigen
			if(_openBalancingFileDialog.ShowDialog()== DialogResult.OK)
			{
				// Identitätsmapping erstellen
				MappingFile idMapping = new MappingFile();
				_projectFile.Where(elem => elem is TechTreeResearch).ForEach(elem => idMapping.ResearchMapping.Add((short)((TechTreeResearch)elem).ID, (short)((TechTreeResearch)elem).ID));
				_projectFile.Where(elem => elem is TechTreeUnit).ForEach(elem => idMapping.UnitMapping.Add((short)((TechTreeUnit)elem).ID, (short)((TechTreeUnit)elem).ID));

				// Fehler fangen
				try
				{
					// Balancing-Datei laden
					BalancingFile balancingFile = new BalancingFile(_projectFile.BasicGenieFile, _openBalancingFileDialog.FileName, _projectFile.LanguageFilePaths.ToArray(), idMapping);

					// Import-Dialog zeigen
					new ImportBalancingFile(_projectFile, balancingFile).ShowDialog();
				}
				catch(IOException ex)
				{
					// Fehler
					MessageBox.Show($"Error loading balancing file: {ex.Message}");
				}
				catch(KeyNotFoundException)
				{
					// Fehler
					MessageBox.Show($"Error loading balancing file: The balancing file changes dynamically generated items. Remove these changes first.");
				}
			}
		}

		#endregion Ereignishandler

		#region Enumerationen

		/// <summary>
		/// Die durchführbaren Baumoperationen.
		/// </summary>
		private enum TreeOperations
		{
			/// <summary>
			/// Es ist keine Operation aktiv.
			/// </summary>
			None,

			/// <summary>
			/// Das erste Element für die "Neue Verknüpfung"-Operation wird ausgewählt.
			/// </summary>
			NewLinkFirstElement,

			/// <summary>
			/// Das zweite Element für die "Neue Verknüpfung"-Operation wird ausgewählt.
			/// </summary>
			NewLinkSecondElement,

			/// <summary>
			/// Das zu lösende Element für die "Verknüpfung löschen"-Operation wird ausgewählt.
			/// </summary>
			DeleteLink,

			/// <summary>
			/// Das erste Element für die "Alternatives TechTree-Element setzen"-Operation wird ausgewählt.
			/// </summary>
			SetNewTechTreeParentFirstElement,

			/// <summary>
			/// Das erste Element für die "Alternatives TechTree-Element setzen"-Operation wird ausgewählt.
			/// </summary>
			SetNewTechTreeParentSecondElement,

			/// <summary>
			/// Das zu löschende Element für die "Element löschen"-Operation wird ausgewählt.
			/// </summary>
			DeleteElement,

			/// <summary>
			/// Das erste Element für die "Neue Freischalt-Abhängigkeit"-Operation wird ausgewählt.
			/// </summary>
			NewMakeAvailDependencyFirstElement,

			/// <summary>
			/// Das zweite Element für die "Neue Freischalt-Abhängigkeit"-Operation wird ausgewählt.
			/// </summary>
			NewMakeAvailDependencySecondElement,

			/// <summary>
			/// Das erste Element für die "Neue Weiterentwicklungs-Technologie-Abhängigkeit"-Operation wird ausgewählt.
			/// </summary>
			NewSuccessorResearchDependencyFirstElement,

			/// <summary>
			/// Das zweite Element für die "Neue Weiterentwicklungs-Technologie-Abhängigkeit"-Operation wird ausgewählt.
			/// </summary>
			NewSuccessorResearchDependencySecondElement,

			/// <summary>
			/// Das erste Element für die "Neue Gebäude-Abhängigkeit"-Operation wird ausgewählt.
			/// </summary>
			NewBuildingDependencyFirstElement,

			/// <summary>
			/// Das zweite Element für die "Neue Gebäude-Abhängigkeit"-Operation wird ausgewählt.
			/// </summary>
			NewBuildingDependencySecondElement,

			/// <summary>
			/// Das erste Element für die "Abhängigkeit löschen"-Operation wird ausgewählt.
			/// </summary>
			DeleteDependencyFirstElement,

			/// <summary>
			/// Das zweite Element für die "Abhängigkeit löschen"-Operation wird ausgewählt.
			/// </summary>
			DeleteDependencySecondElement,

			/// <summary>
			/// Das Basiselement für eine neue erschaffbare Einheit wird ausgewählt.
			/// </summary>
			NewCreatable,

			/// <summary>
			/// Das Basiselement für ein neues Gebäude wird ausgewählt.
			/// </summary>
			NewBuilding,

			/// <summary>
			/// Das Basiselement für eine neue Deko-Einheit wird ausgewählt.
			/// </summary>
			NewEyeCandy,

			/// <summary>
			/// Das Basiselement für eine neue Projektil-Einheit wird ausgewählt.
			/// </summary>
			NewProjectile,

			/// <summary>
			/// Das Basiselement für eine neue tote Einheit wird ausgewählt.
			/// </summary>
			NewDead
		}

		#endregion Enumerationen

		#region Hilfsklassen

		/// <summary>
		/// Hilfsklasse zum schöneren Zeichnen der Kultur-Kopier-Buttons.
		/// </summary>
		private class CivCopyButtonRenderer : ToolStripProfessionalRenderer
		{
			protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
			{
				// Button abrufen
				ToolStripButton button = e.Item as ToolStripButton;
				if(button != null && button.CheckOnClick)
				{
					// Button-Abmessungen abrufen
					Rectangle bounds = new Rectangle(Point.Empty, button.Size);

					// Button zeichnen
					if(button.Checked)
					{
						// Der Button ist ausgewählt
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 194, 110)), bounds);

						// Einen schönen "3D"-Rahmen zeichnen
						e.Graphics.DrawLine(new Pen(Color.FromArgb(138, 89, 21)), new Point(bounds.Left, bounds.Bottom), new Point(bounds.Left, bounds.Top));
						e.Graphics.DrawLine(new Pen(Color.FromArgb(138, 89, 21)), new Point(bounds.Left, bounds.Top), new Point(bounds.Right, bounds.Top));
					}
					else
					{
						// Der Button ist nicht ausgewählt
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 221, 140)), bounds);
					}
				}
				else if(button != null) // Steuerbutton
				{
					// Button-Abmessungen abrufen
					Rectangle bounds = new Rectangle(Point.Empty, button.Size);

					// Button zeichnen
					if(button.Pressed)
					{
						// Der Button ist gedrückt
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(147, 255, 110)), bounds);

						// Rahmen zeichnen
						e.Graphics.DrawLine(new Pen(Color.FromArgb(48, 138, 21)), new Point(bounds.Left, bounds.Bottom), new Point(bounds.Left, bounds.Top));
						e.Graphics.DrawLine(new Pen(Color.FromArgb(48, 138, 21)), new Point(bounds.Left, bounds.Top), new Point(bounds.Right, bounds.Top));
					}
					else
					{
						// Der Button ist nicht gedrückt
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(156, 255, 141)), bounds);
					}
				}
				else
					base.OnRenderButtonBackground(e);
			}
		}

		/// <summary>
		/// Stellt Funktionen für die Kommunikation zwischen Hauptanwendung und Plugins bereit.
		/// Dies vermeidet das direkte Übergeben des Hauptformulars an die Plugins und erhöht so die Sicherheit und Stabilität der Anwendung.
		/// </summary>
		public sealed class PluginCommunicator
		{
			#region Variablen

			/// <summary>
			/// Das Hauptformular, mit dem kommuniziert werden soll.
			/// </summary>
			MainForm _mainForm;

			#endregion

			#region Funktionen

			/// <summary>
			/// Konstruktor.
			/// </summary>
			/// <param name="mainForm">Das verwaltete Hauptformular.</param>
			public PluginCommunicator(MainForm mainForm)
			{
				// Hauptformular merken
				_mainForm = mainForm;
			}

			/// <summary>
			/// Löst eine Aktualisierung und ein Neuzeichnen des gesamten Baums aus.
			/// </summary>
			public void IssueTreeUpdate()
			{
				// Baum neu berechnen
				_mainForm._renderPanel.UpdateTreeData(_mainForm._projectFile.TechTreeParentElements, _mainForm._projectFile.AgeCount);
			}

			/// <summary>
			/// Erstellt die Icon-Textur für das gegebene Element.
			/// </summary>
			/// <param name="element">Das Element, dessen Icon-Textur erstellen werden soll.</param>
			public void CreateElementIconTexture(TechTreeElement element)
			{
				// Textur erstellen
				element.CreateIconTexture(_mainForm._renderPanel.LoadIconAsTexture);
			}

			#endregion

			#region Eigenschaften

			/// <summary>
			/// Ruft das aktuell ausgewählte Element ab.
			/// </summary>
			public TechTreeElement CurrentSelection
			{
				get { return _mainForm._selectedElement; }
			}

			/// <summary>
			/// Ruft die ID-Namen-Zuordnung der im Projekt enthaltenen Kulturen ab.
			/// </summary>
			public List<KeyValuePair<uint, string>> Civs
			{
				get { return _mainForm._civs; }
			}

			#endregion
		}

		#endregion Hilfsklassen
	}
}