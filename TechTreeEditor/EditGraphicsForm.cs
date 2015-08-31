using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TechTreeEditor
{
	/// <summary>
	/// Definiert ein Formular zur Bearbeitung von DAT-Grafiken.
	/// </summary>
	public partial class EditGraphicsForm : Form
	{
		#region Variablen

		/// <summary>
		/// Das zu bearbeitende Projekt.
		/// </summary>
		private TechTreeFile _projectFile;

		/// <summary>
		/// Die angezeigten und bearbeiteten Grafiken, nach ID sortiert.
		/// </summary>
		private SortedDictionary<int, GenieLibrary.DataElements.Graphic> _graphics;

		/// <summary>
		/// Die aktuell ausgewählte Grafik.
		/// </summary>
		private GenieLibrary.DataElements.Graphic _selectedGraphic = null;

		/// <summary>
		/// Gibt an, ob die letzten Änderungen gespeichert wurden.
		/// </summary>
		private bool _saved = true;

		/// <summary>
		/// Gibt an, ob gerade Werte geladen werden und deshalb Ereignisse unterbunden werden sollen.
		/// </summary>
		private bool _updating = false;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private EditGraphicsForm()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Grafik-Bearbeitungsformular für das angegebene Projekt.
		/// </summary>
		/// <param name="projectFile">Das zu bearbeitende Projekt.</param>
		public EditGraphicsForm(TechTreeFile projectFile)
			: this()
		{
			// Parameter speichern
			_projectFile = projectFile;

			// Grafikliste anzeigen
			_graphics = new SortedDictionary<int, GenieLibrary.DataElements.Graphic>();
			foreach(var gra in _projectFile.BasicGenieFile.Graphics)
				_graphics[gra.Key] = (GenieLibrary.DataElements.Graphic)gra.Value.Clone();
			UpdateGraphicList();

			// Alles ist geladen, logischerweise auch gespeichert
			SetSaveStatus(true);
		}

		/// <summary>
		/// Setzt den angezeigten Speicher-Status. Weitere Aktionen werden nicht vorgenommen.
		/// </summary>
		/// <param name="status">Der Status, ob die letzten Änderungen gespeichert wurden oder nicht.</param>
		private void SetSaveStatus(bool status)
		{
			// Status setzen
			_saved = status;
			_saveButton.Enabled = !_saved;
		}

		/// <summary>
		/// Aktualisiert die Grafik-Liste.
		/// </summary>
		private void UpdateGraphicList()
		{
			// Aktualisierungsvorgang läuft
			if(_updating)
				return;
			else
				_updating = true;

			// Aktualisieren
			object selection = _graphicListBox.SelectedItem;
			_graphicListBox.SuspendLayout();
			_graphicListBox.Items.Clear();
			_graphicListBox.Items.AddRange(_graphics.Values.Where(g => g.ToString().Contains(_filterTextBox.Text, StringComparison.InvariantCultureIgnoreCase)).ToArray());
			_graphicListBox.ResumeLayout();
			_graphicListBox.SelectedItem = selection;

			// Fertig
			_updating = false;
		}

		#endregion Funktionen

		#region Ereignishandler

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		private void _saveButton_Click(object sender, EventArgs e)
		{
			// Neue Grafikliste kopieren
			_projectFile.BasicGenieFile.Graphics.Clear();
			_projectFile.BasicGenieFile.GraphicPointers.Clear();

			// Grafiken einfügen
			int maxGraphicID = _graphics.Keys.Max();
			for(int g = 0; g <= maxGraphicID; ++g)
			{
				// Existiert die Grafik?
				if(_graphics.ContainsKey(g))
				{
					// Pointer erstellen
					_projectFile.BasicGenieFile.GraphicPointers.Add(1);

					// Objekt in DAT kopieren
					_projectFile.BasicGenieFile.Graphics[g] = (GenieLibrary.DataElements.Graphic)_graphics[g].Clone();
				}
				else
				{
					// Leeren Pointer erstellen
					_projectFile.BasicGenieFile.GraphicPointers.Add(0);
				}
			}

			// Es ist gespeichert
			SetSaveStatus(true);
		}

		private void EditGraphicsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Gespeichert?
			if(!_saved)
			{
				// Nachfragen
				DialogResult result = MessageBox.Show("Änderungen speichern?", "Fenster schließen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if(result == DialogResult.Yes)
					_saveButton_Click(sender, EventArgs.Empty);
				else if(result == DialogResult.Cancel)
					e.Cancel = true;
			}
		}

		private void _filterTextBox_TextChanged(object sender, EventArgs e)
		{
			// Liste neu füllen
			UpdateGraphicList();
		}

		private void _graphicListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang läuft
			if(_updating)
				return;
			else
				_updating = true;

			// Element abrufen
			_selectedGraphic = (GenieLibrary.DataElements.Graphic)_graphicListBox.SelectedItem;
			if(_selectedGraphic != null)
			{
				// Werte setzen
				_idField.Value = _selectedGraphic.ID;
				_name1TextBox.Text = _selectedGraphic.Name1.TrimEnd('\0');
				_name2TextBox.Text = _selectedGraphic.Name2.TrimEnd('\0');
				_slpField.Value = _selectedGraphic.SLP;
				_frameCountField.Value = _selectedGraphic.FrameCount;
				_frameRateField.Value = (decimal)_selectedGraphic.FrameRate;
				_angleCountField.Value = _selectedGraphic.AngleCount;
				_soundIDField.Value = _selectedGraphic.SoundID;
				_playerColorField.Value = _selectedGraphic.PlayerColor;
				_replayCheckBox.Value = (_selectedGraphic.Replay > 0);
				_replayDelayField.Value = (decimal)_selectedGraphic.ReplayDelay;
				_coord1Field.Value = _selectedGraphic.Coordinates[0];
				_coord2Field.Value = _selectedGraphic.Coordinates[1];
				_coord3Field.Value = _selectedGraphic.Coordinates[2];
				_coord4Field.Value = _selectedGraphic.Coordinates[3];
				_sequenceTypeField.Value = _selectedGraphic.SequenceType;
				_mirrorField.Value = _selectedGraphic.MirroringMode;
				_rainbowField.Value = _selectedGraphic.Rainbow;
				_speedField.Value = (decimal)_selectedGraphic.NewSpeed;
				_layerField.Value = _selectedGraphic.Layer;
				_unknown1Field.Value = _selectedGraphic.Unknown1;
				_unknown2Field.Value = _selectedGraphic.Unknown2;
				_unknown3Field.Value = _selectedGraphic.Unknown3;

				// Deltas
				_deltaView.Rows.Clear();
				foreach(var delta in _selectedGraphic.Deltas)
				{
					// Neue Zeile erstellen
					DataGridViewRow currRow = new DataGridViewRow();
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = delta.GraphicID });
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = delta.DirectionX });
					currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = delta.DirectionY });
					_deltaView.Rows.Add(currRow);
				}

				// Angriffssounds
				_attackSoundField.Value = (_selectedGraphic.AttackSoundUsed > 0);
				if(_attackSoundView.Enabled = _attackSoundField.Value)
				{
					// Es müssen genau AngleCount Werte sein
					if(_selectedGraphic.AttackSounds == null)
						_selectedGraphic.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
					while(_selectedGraphic.AttackSounds.Count > _selectedGraphic.AngleCount)
						_selectedGraphic.AttackSounds.RemoveAt(_selectedGraphic.AttackSounds.Count - 1);
					while(_selectedGraphic.AttackSounds.Count < _selectedGraphic.AngleCount)
						_selectedGraphic.AttackSounds.Add(new GenieLibrary.DataElements.Graphic.GraphicAttackSound()
						{
							SoundID1 = -1,
							SoundDelay1 = -1,
							SoundID2 = -1,
							SoundDelay2 = -1,
							SoundID3 = -1,
							SoundDelay3 = -1
						});

					// Aktualisieren
					_updating = true;
					_attackSoundView.Rows.Clear();
					for(int i = 0; i < _selectedGraphic.AngleCount; ++i)
					{
						// Sound abrufen
						GenieLibrary.DataElements.Graphic.GraphicAttackSound sound = _selectedGraphic.AttackSounds[i];

						// Neue Zeile erstellen
						DataGridViewRow currRow = new DataGridViewRow();
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = i });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID1 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay1 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID2 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay2 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID3 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay3 });
						_attackSoundView.Rows.Add(currRow);
					}
				}
			}

			// Fertig
			_updating = false;
		}

		private void _idField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null && _selectedGraphic.ID != (int)e.NewValue)
			{
				// Ist die ID eindeutig?
				if(_graphics.ContainsKey((int)e.NewValue))
				{
					// Fehler
					_idField.BackColor = Color.Red;
				}
				else
				{
					// Wert ändern
					_idField.BackColor = SystemColors.Control;
					_graphics.Remove(_selectedGraphic.ID);
					_selectedGraphic.ID = (short)e.NewValue;
					_graphics[_selectedGraphic.ID] = _selectedGraphic;
				}

				// Liste neu zeichnen
				UpdateGraphicList();

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _name1TextBox_TextChanged(object sender, EventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Name1 = _name1TextBox.Text + "\0";

				// Liste neu zeichnen
				UpdateGraphicList();

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _name2TextBox_TextChanged(object sender, EventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Name2 = _name2TextBox.Text + "\0";

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _slpField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.SLP = (int)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _frameCountField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.FrameCount = (ushort)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _frameRateField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.FrameRate = (float)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _angleCountField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.AngleCount = (ushort)e.NewValue;

				// Attack-Sound-Werte neu einfügen
				if(_attackSoundField.Value)
				{
					// Es müssen genau AngleCount Werte sein
					if(_selectedGraphic.AttackSounds == null)
						_selectedGraphic.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
					while(_selectedGraphic.AttackSounds.Count > _selectedGraphic.AngleCount)
						_selectedGraphic.AttackSounds.RemoveAt(_selectedGraphic.AttackSounds.Count - 1);
					while(_selectedGraphic.AttackSounds.Count < _selectedGraphic.AngleCount)
						_selectedGraphic.AttackSounds.Add(new GenieLibrary.DataElements.Graphic.GraphicAttackSound()
						{
							SoundID1 = -1,
							SoundDelay1 = -1,
							SoundID2 = -1,
							SoundDelay2 = -1,
							SoundID3 = -1,
							SoundDelay3 = -1
						});

					// Aktualisieren
					_updating = true;
					_attackSoundView.Rows.Clear();
					for(int i = 0; i < _selectedGraphic.AngleCount; ++i)
					{
						// Sound abrufen
						GenieLibrary.DataElements.Graphic.GraphicAttackSound sound = _selectedGraphic.AttackSounds[i];

						// Neue Zeile erstellen
						DataGridViewRow currRow = new DataGridViewRow();
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = i });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID1 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay1 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID2 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay2 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID3 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay3 });
						_attackSoundView.Rows.Add(currRow);
					}
					_updating = false;
				}

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _soundIDField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.SoundID = (short)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _playerColorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.PlayerColor = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _replayCheckBox_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Replay = (byte)(e.NewValue ? 1 : 0);

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _replayDelayField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.ReplayDelay = (float)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _coord1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Coordinates[0] = (short)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _coord2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Coordinates[1] = (short)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _coord3Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Coordinates[2] = (short)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _coord4Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Coordinates[3] = (short)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _sequenceTypeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.SequenceType = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _mirrorField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.MirroringMode = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _rainbowField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Rainbow = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _speedField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.NewSpeed = (float)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _layerField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Layer = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _unknown1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Unknown1 = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _unknown2Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Unknown2 = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _unknown3Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.Unknown3 = (byte)e.NewValue;

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _attackSoundField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Wert ändern
				_selectedGraphic.AttackSoundUsed = (byte)(e.NewValue ? 1 : 0);

				// Wird das View neu angezeigt?
				if(_attackSoundView.Enabled = e.NewValue)
				{
					// Es müssen genau AngleCount Werte sein
					if(_selectedGraphic.AttackSounds == null)
						_selectedGraphic.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
					while(_selectedGraphic.AttackSounds.Count > _selectedGraphic.AngleCount)
						_selectedGraphic.AttackSounds.RemoveAt(_selectedGraphic.AttackSounds.Count - 1);
					while(_selectedGraphic.AttackSounds.Count < _selectedGraphic.AngleCount)
						_selectedGraphic.AttackSounds.Add(new GenieLibrary.DataElements.Graphic.GraphicAttackSound()
						{
							SoundID1 = -1,
							SoundDelay1 = -1,
							SoundID2 = -1,
							SoundDelay2 = -1,
							SoundID3 = -1,
							SoundDelay3 = -1
						});

					// Aktualisieren
					_updating = true;
					_attackSoundView.Rows.Clear();
					for(int i = 0; i < _selectedGraphic.AngleCount; ++i)
					{
						// Sound abrufen
						GenieLibrary.DataElements.Graphic.GraphicAttackSound sound = _selectedGraphic.AttackSounds[i];

						// Neue Zeile erstellen
						DataGridViewRow currRow = new DataGridViewRow();
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = i });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID1 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay1 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID2 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay2 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundID3 });
						currRow.Cells.Add(new DataGridViewTextBoxCell() { Value = sound.SoundDelay3 });
						_attackSoundView.Rows.Add(currRow);
					}
					_updating = false;
				}

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _deltaView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// Nur Zahlen erlauben
			short val;
			if(!short.TryParse((string)e.FormattedValue, out val) && e.RowIndex != _deltaView.NewRowIndex)
			{
				// Fehlersound spielen
				System.Media.SystemSounds.Beep.Play();

				// Bearbeiten erzwingen
				e.Cancel = true;
			}
		}

		private void _deltaView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Alle Werte aus View in Delta-Liste schreiben
				_selectedGraphic.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>();
				foreach(DataGridViewRow currRow in _deltaView.Rows)
				{
					// Neue Reihe überspringen
					if(currRow.IsNewRow || currRow.Cells[0].Value == null || currRow.Cells[1].Value == null || currRow.Cells[2].Value == null)
						continue;

					// Daten einfügen
					GenieLibrary.DataElements.Graphic.GraphicDelta delta = new GenieLibrary.DataElements.Graphic.GraphicDelta();
					delta.Unknown4 = -1;
					delta.GraphicID = (currRow.Cells[0].Value.GetType() == typeof(short) ? (short)currRow.Cells[0].Value : short.Parse((string)currRow.Cells[0].Value));
					delta.DirectionX = (currRow.Cells[1].Value.GetType() == typeof(short) ? (short)currRow.Cells[1].Value : short.Parse((string)currRow.Cells[1].Value));
					delta.DirectionY = (currRow.Cells[2].Value.GetType() == typeof(short) ? (short)currRow.Cells[2].Value : short.Parse((string)currRow.Cells[2].Value));
					_selectedGraphic.Deltas.Add(delta);
				}

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _deltaView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			// Genauso behandelt wie die Zellwert-Änderung
			_deltaView_CellValueChanged(sender, null);
		}

		private void _attackSoundView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// Nur Zahlen erlauben
			short val;
			if(!short.TryParse((string)e.FormattedValue, out val) && e.RowIndex != _attackSoundView.NewRowIndex)
			{
				// Fehlersound spielen
				System.Media.SystemSounds.Beep.Play();

				// Bearbeiten erzwingen
				e.Cancel = true;
			}
		}

		private void _attackSoundView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			// Kein Aktualisierungsvorgang, Objekt ausgewählt?
			if(!_updating && _selectedGraphic != null)
			{
				// Alle Werte aus View in Sound-Liste schreiben
				_selectedGraphic.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
				foreach(DataGridViewRow currRow in _attackSoundView.Rows)
				{
					// Neue Reihe überspringen
					if(currRow.IsNewRow || currRow.Cells[0].Value == null || currRow.Cells[1].Value == null || currRow.Cells[2].Value == null)
						continue;

					// Daten einfügen
					GenieLibrary.DataElements.Graphic.GraphicAttackSound sound = new GenieLibrary.DataElements.Graphic.GraphicAttackSound();
					sound.SoundID1 = (currRow.Cells[1].Value.GetType() == typeof(short) ? (short)currRow.Cells[1].Value : short.Parse((string)currRow.Cells[1].Value));
					sound.SoundDelay1 = (currRow.Cells[2].Value.GetType() == typeof(short) ? (short)currRow.Cells[2].Value : short.Parse((string)currRow.Cells[2].Value));
					sound.SoundID2 = (currRow.Cells[3].Value.GetType() == typeof(short) ? (short)currRow.Cells[3].Value : short.Parse((string)currRow.Cells[3].Value));
					sound.SoundDelay2 = (currRow.Cells[4].Value.GetType() == typeof(short) ? (short)currRow.Cells[4].Value : short.Parse((string)currRow.Cells[4].Value));
					sound.SoundID3 = (currRow.Cells[5].Value.GetType() == typeof(short) ? (short)currRow.Cells[5].Value : short.Parse((string)currRow.Cells[5].Value));
					sound.SoundDelay3 = (currRow.Cells[6].Value.GetType() == typeof(short) ? (short)currRow.Cells[6].Value : short.Parse((string)currRow.Cells[6].Value));
					_selectedGraphic.AttackSounds.Add(sound);
				}

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		private void _newGraphicButton_Click(object sender, EventArgs e)
		{
			// Sind Grafiken markiert? => Kopieren
			if(_graphicListBox.SelectedItems.Count > 0)
			{
				// Markierte Grafiken sortiert durchlaufen und kopieren
				List<GenieLibrary.DataElements.Graphic> _selectedGraphics = new List<GenieLibrary.DataElements.Graphic>(_graphicListBox.SelectedItems.Cast<GenieLibrary.DataElements.Graphic>());
				_selectedGraphics.Sort((g1, g2) => g1.ID.CompareTo(g2.ID));
				foreach(var gra in _selectedGraphics)
				{
					// Grafik kopieren
					int newID = _graphics.Keys.Max() + 1;
					GenieLibrary.DataElements.Graphic newG = (GenieLibrary.DataElements.Graphic)gra.Clone();
					newG.ID = (short)newID;

					// Grafik einfügen
					_graphics.Add(newID, newG);
				}
			}
			else
			{
				// Neue Grafik erstellen
				int newID = _graphics.Keys.Max() + 1;
				GenieLibrary.DataElements.Graphic newG = new GenieLibrary.DataElements.Graphic()
				{
					AngleCount = 0,
					AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>(),
					AttackSoundUsed = 0,
					Coordinates = new List<short>(new short[4] { 0, 0, 0, 0 }),
					Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(),
					FrameCount = 0,
					FrameRate = 0,
					ID = (short)newID,
					Layer = 0,
					MirroringMode = 0,
					Name1 = "New Graphic",
					Name2 = "New Graphic",
					NewSpeed = 0,
					PlayerColor = 255,
					Rainbow = 255,
					Replay = 0,
					ReplayDelay = 0,
					SequenceType = 0,
					SLP = -1,
					SoundID = -1,
					Unknown1 = 0,
					Unknown2 = 1,
					Unknown3 = 0
				};

				// Grafik einfügen
				_graphics.Add(newID, newG);
			}

			// Aktualisieren
			UpdateGraphicList();

			// Nicht mehr gespeichert
			SetSaveStatus(false);
		}

		private void _deleteGraphicButton_Click(object sender, EventArgs e)
		{
			// Effekt löschen
			if(_graphicListBox.SelectedItems.Count > 0 && MessageBox.Show("Die ausgewählten Grafiken wirklich löschen?", "Grafiken löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Grafiken löschen
				foreach(GenieLibrary.DataElements.Graphic gra in _graphicListBox.SelectedItems)
					_graphics.Remove(gra.ID);

				// Aktualisieren
				UpdateGraphicList();

				// Nicht mehr gespeichert
				SetSaveStatus(false);
			}
		}

		#endregion Ereignishandler
	}
}