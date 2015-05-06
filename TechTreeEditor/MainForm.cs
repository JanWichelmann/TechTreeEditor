using DRSLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
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
		/// Gibt an, ob die letzten Änderungen gespeichert wurden.
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
		private BMPLoaderNew.ColorTable _pal50500 = null;

		/// <summary>
		/// Der Einheiten-Manager.
		/// </summary>
		private GenieUnitManager _unitManager = null;

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

			// Kultur-Kopier-Leisten-Renderer setzen
			_civCopyBar.Renderer = new CivCopyButtonRenderer();

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

			// Interfac-DRS laden
			SetStatus("Lade Interfac-DRS...");
			DRSFile interfacDRS = new DRSFile(_projectFile.InterfacDRSPath);

			// Icons laden
			SetStatus("Lade Icon-SLPs...");
			SLPLoader.Loader _iconsResearches = new SLPLoader.Loader(new IORAMHelper.RAMBuffer(interfacDRS.GetResourceData(50729)));
			SLPLoader.Loader _iconsUnits = new SLPLoader.Loader(new IORAMHelper.RAMBuffer(interfacDRS.GetResourceData(50730)));
			SLPLoader.Loader _iconsBuildings = new SLPLoader.Loader(new IORAMHelper.RAMBuffer(interfacDRS.GetResourceData(50706)));

			// Farb-Palette laden
			SetStatus(Strings.MainForm_Status_LoadingPal);
			_pal50500 = new BMPLoaderNew.ColorTable(new BMPLoaderNew.JASCPalette(new IORAMHelper.RAMBuffer(Properties.Resources.pal50500)));

			// Daten an Render-Control übergeben
			SetStatus(Strings.MainForm_Status_PassRenderData);
			_renderPanel.UpdateIconData(_pal50500, _iconsResearches, _iconsUnits, _iconsBuildings);

			// Icon-Texturen erstellen
			_projectFile.TechTreeParentElements.ForEach(p => p.CreateIconTextures(_renderPanel.LoadIconAsTexture));

			// Kulturennamen laden
			SetStatus("Lade Kulturenliste...");
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
				civButton.ToolTipText = string.Format("Auto-Kopieren zu {0}", civ.Value);
				civButton.Tag = (int)civ.Key;
				_civCopyBar.Items.Add(civButton);
			}

			// Daten an Render-Control übergeben
			SetStatus(Strings.MainForm_Status_PreparingTreeRendering);
			_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);

			// Buttons freischalten
			_saveProjectMenuButton.Enabled = true;
			_saveProjectButton.Enabled = true;
			_exportDATMenuButton.Enabled = true;
			_exportDATButton.Enabled = true;
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
			_newMakeAvailDepButton.Enabled = true;
			_newSuccResDepButton.Enabled = true;
			_newBuildingDepButton.Enabled = true;
			_deleteDepButton.Enabled = true;
			_deleteElementButton.Enabled = true;
			_civCopyBar.Enabled = true;
			_lockAllIDsMenuButton.Enabled = true;

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
					backupPath = Path.ChangeExtension(_projectFileName, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".backup.tpp");
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
					SetStatus("Bitte das unterzuordnene Element auswählen...");
					break;

				case TreeOperations.NewLinkSecondElement:
					SetStatus("Bitte das neue Ober-Element auswählen...");
					break;

				case TreeOperations.DeleteLink:
					SetStatus("Bitte das abzutrennende Unterelement auswählen...");
					break;

				case TreeOperations.DeleteElement:
					SetStatus("Bitte das löschende Element auswählen...");
					break;

				case TreeOperations.NewMakeAvailDependencyFirstElement:
					SetStatus("Bitte das zu aktivierende Element auswählen...");
					break;

				case TreeOperations.NewMakeAvailDependencySecondElement:
					SetStatus("Bitte die aktivierende Technologie auswählen...");
					break;

				case TreeOperations.NewSuccessorResearchDependencyFirstElement:
					SetStatus("Bitte das weiterzuentwickelnde Element auswählen...");
					break;

				case TreeOperations.NewSuccessorResearchDependencySecondElement:
					SetStatus("Bitte die weiterentwickelnde Technologie auswählen...");
					break;

				case TreeOperations.NewBuildingDependencyFirstElement:
					SetStatus("Bitte das abhängige Element auswählen...");
					break;

				case TreeOperations.NewBuildingDependencySecondElement:
					SetStatus("Bitte das benötigte Gebäude auswählen...");
					break;

				case TreeOperations.DeleteDependencyFirstElement:
					SetStatus("Bitte das abhängige Element auswählen...");
					break;

				case TreeOperations.DeleteDependencySecondElement:
					SetStatus("Bitte das referenzierte Element auswählen...");
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
				TechTreeResearch newResearch = (_copyElement as TechTreeResearch).Clone(copyChildren, _projectFile.BasicGenieFile);

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
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
			}
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

				// Nach Operationsmodus vorgehen
				if(_currentOperation == TreeOperations.None)
				{
					// Standard-Element-Button aktualisieren
					if(_selectedElement.GetType() == typeof(TechTreeBuilding))
					{
						_standardElementCheckButton.Enabled = true;
						_standardElementCheckButton.Checked = ((TechTreeBuilding)_selectedElement).StandardElement;
					}
					else if(_selectedElement.GetType() == typeof(TechTreeCreatable))
					{
						_standardElementCheckButton.Enabled = true;
						_standardElementCheckButton.Checked = ((TechTreeCreatable)_selectedElement).StandardElement;
					}
					else
					{
						_standardElementCheckButton.Enabled = (_selectedElement.GetType() == typeof(TechTreeResearch)); // Technologien sind immer Standard-Elemente
						_standardElementCheckButton.Checked = false;
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
				}

				// Buttons aktivieren
				_ageUpButton.Enabled = true;
				_ageDownButton.Enabled = true;
				_editAttributesButton.Enabled = true;
				_editElementPropertiesButton.Enabled = true;
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
							_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
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
							_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
						}
						break;

					// Element löschen
					case TreeOperations.DeleteElement:
						{
							// Änderung durchführen
							if(_selectedElement != null && MessageBox.Show("Dieses Element wirklich löschen?", "Element löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								if(!_projectFile.DestroyElement(_selectedElement))
								{
									// Es wurden noch nicht alle Referenzen entfernt
									MessageBox.Show("Fehler: Es wurden noch nicht alle Referenzen gelöscht!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}

							// Falls es sich um das Kopierelement handelt, dieses leeren
							if(_selectedElement == _copyElement)
								_copyElement = null;

							// Auswahl aktualisieren
							_renderPanel_SelectionChanged(sender, new RenderControl.SelectionChangedEventArgs(null));

							// Fertig
							UpdateCurrentOperation(TreeOperations.None);

							// Baum neu berechnen
							_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
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
									newUTemp.EnablerResearch = selUTemp.EnablerResearch;
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
									newUTemp.EnablerResearch = selUTemp.EnablerResearch;
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

								// DAT-Einheit auf Basis der des ausgewählten Elements erstellen
								GenieLibrary.DataElements.Civ.Unit newUDAT = (GenieLibrary.DataElements.Civ.Unit)((TechTreeUnit)_selectedElement).DATUnit.Clone();
								newU.DATUnit = newUDAT;

								// Neue ID abrufen und DAT-Einheit auf alle Zivilisationen verteilen
								int newUID = _projectFile.BasicGenieFile.UnitHeaders.Count;
								newU.ID = newUID;
								newUDAT.ID1 = newUDAT.ID2 = newUDAT.ID3 = (short)newUID;
								_projectFile.BasicGenieFile.UnitHeaders.Add(new GenieLibrary.DataElements.UnitHeader() { Exists = 1, Commands = new List<GenieLibrary.DataElements.UnitHeader.UnitCommand>(_projectFile.BasicGenieFile.UnitHeaders[_selectedElement.ID].Commands) });
								foreach(var civ in _projectFile.BasicGenieFile.Civs)
								{
									civ.UnitPointers.Add(newUID);
									civ.Units.Add(newUID, (GenieLibrary.DataElements.Civ.Unit)newUDAT.Clone()); // Klonen, da sonst alle Civs in dieselbe DAT-Einheit schreiben
								}

								// Technologie als Elternelement setzen und an den Anfang stellen
								_projectFile.TechTreeParentElements.Insert(0, newU);

								// Icon und Namen erstellen
								newU.CreateIconTexture(_renderPanel.LoadIconAsTexture);
								newU.UpdateName(_projectFile.LanguageFileWrapper);

								// Baum neu berechnen
								_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
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

		private void _importDATButton_Click(object sender, EventArgs e)
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
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
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
				_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
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
			_renderPanel.UpdateTreeData(_projectFile.TechTreeParentElements);
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
			if(MessageBox.Show("Wirklich alle Element-IDs als 'Geschützt' markieren?", "Alle IDs schützen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
			// Dialog anzeigen
			ExportDATFile exportDialog = new ExportDATFile(_projectFile);
			exportDialog.ShowDialog();
		}

		private void _exportDATButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			ExportDATFile exportDialog = new ExportDATFile(_projectFile);
			exportDialog.ShowDialog();
		}

		private void _editCivBoniButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			EditCivBonusesForm bonusDialog = new EditCivBonusesForm(_projectFile, _currCivConfig);
			bonusDialog.ShowDialog();
		}

		private void _editGraphicsButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			EditGraphicsForm graphicDialog = new EditGraphicsForm(_projectFile);
			graphicDialog.ShowDialog();
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
				else
					base.OnRenderButtonBackground(e);
			}
		}

		#endregion Hilfsklassen
	}
}