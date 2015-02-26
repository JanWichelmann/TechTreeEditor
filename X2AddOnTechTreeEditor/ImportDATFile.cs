using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X2AddOnTechTreeEditor
{
	/// <summary>
	/// Bietet Funktionen für das Importieren einer DAT-Datei in das eigene Format.
	/// </summary>
	public partial class ImportDATFile : Form
	{
		#region Variablen

		/// <summary>
		/// Gibt an, ob das Fenster geschlossen wird.
		/// </summary>
		bool _formClosing = false;

		/// <summary>
		/// Gibt an, ob das Fenster geschlossen werden kann.
		/// </summary>
		bool _formClosingAllowed = true;

		/// <summary>
		/// Die neue Projektdatei.
		/// </summary>
		TechTreeFile projectFile = null;

		/// <summary>
		/// Ruft den Dateinamen der neu erstellten Projektdatei ab.
		/// </summary>
		public string ProjectFileName { get; private set; }

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public ImportDATFile()
		{
			// Steuerelemente laden
			InitializeComponent();

			// ToolTip zuweisen
			_dllHelpBox.SetToolTip(_dllTextBox, "Hier sollen alle referenzierten Language-DLL-Dateien angegeben werden.\nDiese sollen nach Priorität aufsteigend sortiert sein, also zum Beispiel:\n\nLANGUAGE.DLL;language_x1.dll;language_x1_p1.dll");
			_dllHelpBox.InitialDelay = 0;
			_dllHelpBox.ReshowDelay = 0;
			_dllHelpBox.AutomaticDelay = 0;
			_dllHelpBox.ShowAlways = true;

			// Standard-Dialogrückgabestatus setzen
			this.DialogResult = DialogResult.Cancel;
		}

		#endregion

		#region Ereignishandler

		private void _datButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openDATDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_datTextBox.Text = _openDATDialog.FileName;
			}
		}

		private void _dllButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openDLLDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_dllTextBox.Text = string.Join(";", _openDLLDialog.FileNames);
			}
		}

		private void _loadDataButton_Click(object sender, EventArgs e)
		{
			// Sicherstellen, dass DAT-Datei angegeben wurde
			if(!File.Exists(_datTextBox.Text))
			{
				// Fehler
				MessageBox.Show("Fehler: Es wurde keine gültige DAT-Datei angegeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Fortschrittsleiste aktivieren und Mauszeiger ändern, Steuerelemente deaktivieren
			_loadProgressBar.Visible = true;
			this.Cursor = Cursors.WaitCursor;
			_datTextBox.Enabled = false;
			_datButton.Enabled = false;
			_dllTextBox.Enabled = false;
			_dllButton.Enabled = false;
			_loadDataButton.Enabled = false;

			// Lade-Funktion in separatem Thread ausführen, um Formular nicht zu blockieren
			Task.Factory.StartNew(() =>
			{
				// TechTree-Datei erstellen und DAT-Datei laden
				projectFile = new TechTreeFile(new GenieLibrary.GenieFile(GenieLibrary.GenieFile.DecompressData(new IORAMHelper.RAMBuffer(_datTextBox.Text))));
				GenieLibrary.GenieFile dat = projectFile.BasicGenieFile; // Kürzel

				// Wenn DLLs angegeben wurden, diese laden
				projectFile.LanguageFileNames = _dllTextBox.Text.Split(';').ToList();

				// Briten-Einheiten kopieren (sollte alle Einheiten in der Liste haben? => TODO: evtl. besser mit Gaia? Macht irgendwie was komisches mit der Enabled-Eigenschaft...)
				Dictionary<int, GenieLibrary.DataElements.Civ.Unit> remainingUnits = new Dictionary<int, GenieLibrary.DataElements.Civ.Unit>(dat.Civs[1].Units);

				// Technologien kopieren
				Dictionary<int, GenieLibrary.DataElements.Research> remainingResearches = new Dictionary<int, GenieLibrary.DataElements.Research>(dat.Researches.Count);
				for(int r = 0; r < dat.Researches.Count; ++r)
					remainingResearches.Add(r, dat.Researches[r]);

				// Liste für die bereits entwickelten Technologien
				List<int> completedResearches = new List<int>(dat.Researches.Count);

				// Alle Gebäude als Stammelemente schreiben, erschaffbare Einheiten aus Einheiten-Liste nehmen und Untereinheiten anhängen
				// Als IDs benutzen wir hier zuerst die DAT-IDs, diese werden dann später in eindeutige IDs umgesetzt
				List<TechTreeStructure.TechTreeCreatable> creatableUnits = new List<TechTreeStructure.TechTreeCreatable>(remainingUnits.Count);
				List<TechTreeStructure.TechTreeUnit> otherUnits = new List<TechTreeStructure.TechTreeUnit>(remainingUnits.Count); // Tote und Eye-Candy-Einheiten
				for(int u = remainingUnits.Count - 1; u >= 0; --u)
				{
					// Einheit abrufen
					GenieLibrary.DataElements.Civ.Unit currUnit = remainingUnits.ElementAt(u).Value;
					if(currUnit == null || currUnit.Type < GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable)
						continue;

					// Tote Einheit vorhanden?
					TechTreeStructure.TechTreeUnit deadUnitElement = null;
					if(currUnit.DeadUnitID >= 0)
					{
						// Einheit bereits geladen?
						deadUnitElement = otherUnits.FirstOrDefault(un => un.ID == currUnit.DeadUnitID);
						if(deadUnitElement == null)
						{
							// Einheit erstellen
							if(remainingUnits.ContainsKey(currUnit.DeadUnitID))
							{
								// Einheit abrufen
								GenieLibrary.DataElements.Civ.Unit deadUnit = remainingUnits[currUnit.DeadUnitID];

								// Nach Typ unterscheiden
								if(deadUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.DeadFish)
									deadUnitElement = new TechTreeStructure.TechTreeDead()
									{
										ID = deadUnit.ID1,
										DATUnit = deadUnit
									};
								else if(deadUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.EyeCandy)
									deadUnitElement = new TechTreeStructure.TechTreeEyeCandy()
									{
										ID = deadUnit.ID1,
										DATUnit = deadUnit
									};

								// Wenn nun Einheit gefunden, die bei den toten Einheiten mit speichern
								if(deadUnitElement != null)
									otherUnits.Add(deadUnitElement);

								// TODO: "Mehrfachtod" erschaffbarer Einheiten (siehe z.B. Shaolin)

								// Einheit als gelöscht markieren
								remainingUnits[currUnit.DeadUnitID] = null;
							}
						}
					}

					// Nach Typen unterscheiden
					if(currUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.Creatable)
					{
						// Einheit hinzufügen
						projectFile.TechTreeParentElements.Add(new TechTreeStructure.TechTreeCreatable()
						{
							Age = 0,
							ID = currUnit.ID1,
							DATUnit = currUnit,
							DeadUnit = deadUnitElement
						});
					}
					else if(currUnit.Type == GenieLibrary.DataElements.Civ.Unit.UnitType.Building)
					{
						// Gebäude hinzufügen
						projectFile.TechTreeParentElements.Add(new TechTreeStructure.TechTreeBuilding()
						{
							Age = 0,
							ID = currUnit.ID1,
							DATUnit = currUnit,
							DeadUnit = deadUnitElement
						});
					}

					// Einheit aus Einheiten-Liste nehmen
					remainingUnits.Remove(currUnit.ID1);
				}

				// Liste aufräumen
				remainingUnits = remainingUnits.Where(un => un.Value != null).ToDictionary(un => un.Key, un => un.Value);

				// Techtree kulturweise berechnen
				/*for(int c = 0; c < _civs.Count; ++c)
				{
					// Statusmeldung
					SetStatus(string.Format("Generiere Techtree für Kultur " + c + " von " + _civs.Count + "...", ""));

					// Kultur-Objekt abrufen
					GenieLibrary.DataElements.Civ currCiv = _dat.Civs[c];

					// Kultur-Techtree anwenden (Sperren usw.)
					// ...
				}*/

				// Fehlende Einheiten in Liste für Restauswahlfeld schreiben
				List<DataGridViewRow> rows = new List<DataGridViewRow>(remainingUnits.Count);
				foreach(var unit in remainingUnits)
				{
					// Element hinzufügen
					DataGridViewRow row = new DataGridViewRow();

					// Werte setzen
					row.Cells.AddRange(
						new DataGridViewCheckBoxCell() { Value = false },
						new DataGridViewTextBoxCell() { Value = unit.Value.ID1.ToString() },
						new DataGridViewTextBoxCell() { Value = unit.Value.Name1.Trim() },
						new DataGridViewTextBoxCell() { Value = projectFile.LanguageFileWrapper.GetString(unit.Value.LanguageDLLName) },
						new DataGridViewTextBoxCell() { Value = unit.Value.Type.ToString() }
					);

					// Zeile hinzufügen
					rows.Add(row);
				}

				// Wenn das Fenster noch offen ist, Steuerelement-Befehle in GUI-Thread ausführen
				if(!_formClosing)
					this.Invoke(new Action(() =>
					{
						// Importmenü deaktivieren und Mauszeiger zurücksetzen
						_loadProgressBar.Visible = false;
						_datTextBox.Enabled = false;
						_loadDataButton.Enabled = false;
						_datButton.Enabled = false;
						this.Cursor = Cursors.Default;

						// Auswahlmenü aktivieren
						_selectionGroupBox.Enabled = true;
						_finishButton.Enabled = true;

						// Fehlende Einheiten zur Auswahl bereitstellen
						_selectionView.SuspendLayout();
						_selectionView.Rows.Clear();
						_selectionView.Rows.AddRange(rows.ToArray());
						_selectionView.ResumeLayout();
					}));
			});
		}

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Sicherheitshalber fragen
			if(MessageBox.Show("Wollen Sie dieses Fenster wirklich schließen und damit den Importvorgang abbrechen?", "Importvorgang abbrechen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void _selectionView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			// ID-Spalte sortieren
			if(e.Column.Index == 1)
			{
				// Zahlen sortieren
				e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
				e.Handled = true;
			}
		}

		private void ImportDATFile_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Ist das erlaubt?
			if(!_formClosingAllowed)
			{
				// Schließen verbieten
				MessageBox.Show("Bitte warten Sie auf den Abschluss des Vorgangs. Schließen dieses Fensters kann zu Instabilität führen.", "Importvorgang abbrechen", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}

			// Variable setzen, um ggf. asynchrone Aufgaben darüber zu informieren
			_formClosing = true;
		}

		private void _finishButton_Click(object sender, EventArgs e)
		{
			// Speicherfenster anzeigen
			if(_saveProjectDialog.ShowDialog() == DialogResult.OK)
			{
				// Steuerelemente sperren
				_selectionGroupBox.Enabled = false;
				_cancelButton.Enabled = false;
				_finishButton.Enabled = false;
				_formClosingAllowed = false;

				// Ladebalken anzeigen
				_finishProgressBar.Visible = true;
				this.Cursor = Cursors.WaitCursor;

				// Dateinamen merken
				ProjectFileName = _saveProjectDialog.FileName;

				// Fertigstellungs-Funktion in separatem Thread ausführen, um Formular nicht zu blockieren
				Task.Factory.StartNew(() =>
				{
					// TODO: Viel zu tun hier...

					// Projektdatei speichern
					projectFile.WriteData(ProjectFileName);

					// Fertig
					this.Invoke(new Action(() =>
					{
						// Fenster schließen
						_formClosingAllowed = true;
						this.DialogResult = DialogResult.OK;
						this.Close();
					}));
				});
			}
		}

		#endregion

		#region Hilfsfunktionen für die Baumerstellung

		/// <summary>
		/// Simuliert für das angegebene Zeitalter die Entwicklungen und daraus folgenden Freischaltungen von Einheiten und Gebäuden sowie folgender Technologien.
		/// Die Ergebnisse werden automatisch in den Baum geschrieben.
		/// </summary>
		/// <param name="ttf">Das TechTree-Objekt, mit dem gearbeitet werden soll.</param>
		/// <param name="age">Die Nummer des zu simulierenden Zeitalters.</param>
		/// <param name="remainingTechs">Alle noch nicht simulierten (= entwickelten) Technologien.</param>
		/// <param name="completedTechs">Die IDs aller bereits simulierten (= entwickelten) Technologien.</param>
		/// <param name="remainingUnits">Die noch nicht in den Baum geschriebenen Einheiten.</param>
		private void SimulateAge(TechTreeFile ttf, int age, Dictionary<int, GenieLibrary.DataElements.Research> remainingTechs, List<int> completedTechs, List<GenieLibrary.DataElements.Civ.Unit> remainingUnits)
		{

		}

		#endregion Hilfsfunktionen für die Baumerstellung
	}
}
