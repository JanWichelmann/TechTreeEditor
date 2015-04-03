using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X2AddOnTechTreeEditor.TechTreeStructure;

namespace X2AddOnTechTreeEditor
{
	public partial class EditBuildingForm : Form
	{
		#region Variablen

		/// <summary>
		/// Das in diesem Fenster bearbeitbare Gebäude.
		/// </summary>
		TechTreeBuilding _building = null;

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		TechTreeFile _projectFile = null;

		/// <summary>
		/// Gibt an, ob die Daten noch geladen werden.
		/// </summary>
		bool _loading = true;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private EditBuildingForm()
		{
			// Steuerelement laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Gebäude-Bearbeitungsfenster.
		/// </summary>
		/// <param name="projectile">Das zu bearbeitende Gebäude.</param>
		public EditBuildingForm(TechTreeFile projectFile, TechTreeBuilding building)
			: this()
		{
			// Parameter merken
			_building = building;
			_projectFile = projectFile;

			// Fenstertitel aktualisieren
			this.Text += _building.Name;

			// Age-Upgrade-Steuerelement füllen => TODO: hardcoded...
			for(int i = _building.Age + 1; i < 5; ++i)
			{
				// Zeile erstellen
				_ageUpgradeListBox.Items.Add("Zeitalter " + i, _building.AgeUpgrades.ContainsKey(i));
			}

			// Dummy-Objekte für leere Listenelemente erstellen
			TechTreeBuilding emptyBuilding = new TechTreeBuilding() { ID = -1 };
			_deadUnitComboBox.Items.Add(emptyBuilding);
			_stackUnitComboBox.Items.Add(emptyBuilding);
			_headUnitComboBox.Items.Add(emptyBuilding);
			_transformUnitComboBox.Items.Add(emptyBuilding);
			TechTreeProjectile emptyProj = new TechTreeProjectile() { ID = -1 };
			_projUnitComboBox.Items.Add(emptyProj);
			_projDuplUnitComboBox.Items.Add(emptyProj);

			// Einheitenliste erstellen
			_deadUnitComboBox.SuspendLayout();
			_deadUnitComboBox.DisplayMember = "Name";
			_projectFile.Where(u => u is TechTreeUnit).ForEach(u =>
			{
				_deadUnitComboBox.Items.Add(u);
			});
			_deadUnitComboBox.ResumeLayout();

			// Gebäudelisten erstellen
			_ageUpgradeComboBox.SuspendLayout();
			_stackUnitComboBox.SuspendLayout();
			_headUnitComboBox.SuspendLayout();
			_transformUnitComboBox.SuspendLayout();
			_annex1ComboBox.SuspendLayout();
			_annex2ComboBox.SuspendLayout();
			_annex3ComboBox.SuspendLayout();
			_annex4ComboBox.SuspendLayout();
			_ageUpgradeComboBox.DisplayMember = "Name";
			_stackUnitComboBox.DisplayMember = "Name";
			_headUnitComboBox.DisplayMember = "Name";
			_transformUnitComboBox.DisplayMember = "Name";
			_annex1ComboBox.DisplayMember = "Name";
			_annex2ComboBox.DisplayMember = "Name";
			_annex3ComboBox.DisplayMember = "Name";
			_annex4ComboBox.DisplayMember = "Name";
			_projectFile.Where(b => b.GetType() == typeof(TechTreeBuilding)).ForEach(b =>
			{
				_ageUpgradeComboBox.Items.Add(b);
				_stackUnitComboBox.Items.Add(b);
				_headUnitComboBox.Items.Add(b);
				_transformUnitComboBox.Items.Add(b);
				_annex1ComboBox.Items.Add(b);
				_annex2ComboBox.Items.Add(b);
				_annex3ComboBox.Items.Add(b);
				_annex4ComboBox.Items.Add(b);
			});
			_ageUpgradeComboBox.ResumeLayout();
			_stackUnitComboBox.ResumeLayout();
			_headUnitComboBox.ResumeLayout();
			_transformUnitComboBox.ResumeLayout();
			_annex1ComboBox.ResumeLayout();
			_annex2ComboBox.ResumeLayout();
			_annex3ComboBox.ResumeLayout();
			_annex4ComboBox.ResumeLayout();

			// Projektillisten erstellen
			_projUnitComboBox.SuspendLayout();
			_projDuplUnitComboBox.SuspendLayout();
			_projUnitComboBox.DisplayMember = "Name";
			_projDuplUnitComboBox.DisplayMember = "Name";
			_projectFile.Where(p => p.GetType() == typeof(TechTreeProjectile)).ForEach(p =>
			{
				_projUnitComboBox.Items.Add(p);
				_projDuplUnitComboBox.Items.Add(p);
			});
			_projUnitComboBox.ResumeLayout();
			_projDuplUnitComboBox.ResumeLayout();

			// Elemente auswählen
			_deadUnitComboBox.SelectedItem = (_building.DeadUnit == null ? emptyBuilding : _building.DeadUnit);
			_stackUnitComboBox.SelectedItem = (_building.StackUnit == null ? emptyBuilding : _building.StackUnit);
			_headUnitComboBox.SelectedItem = (_building.HeadUnit == null ? emptyBuilding : _building.HeadUnit);
			_transformUnitComboBox.SelectedItem = (_building.TransformUnit == null ? emptyBuilding : _building.TransformUnit);
			_projUnitComboBox.SelectedItem = (_building.ProjectileUnit == null ? emptyProj : _building.ProjectileUnit);
			_projDuplUnitComboBox.SelectedItem = (_building.ProjectileDuplicationUnit == null ? emptyProj : _building.ProjectileDuplicationUnit);

			// Gebäude-Abhängigkeiten in View schreiben
			foreach(var currD in _building.BuildingDependencies)
			{
				// Zeile erstellen
				DataGridViewRow row = new DataGridViewRow() { Tag = currD.Key };
				row.Cells.Add(new DataGridViewTextBoxCell() { Value = currD.Value.ToString() });
				row.Cells.Add(new DataGridViewTextBoxCell() { Value = currD.Key.Name });
				_buildingDepView.Rows.Add(row);
			}

			// Kindelemente in View schreiben
			foreach(var currC in _building.Children)
			{
				// Zeile erstellen
				DataGridViewRow row = new DataGridViewRow() { Tag = (TechTreeElement)currC.Item2 };
				row.Cells.Add(new DataGridViewTextBoxCell() { Value = currC.Item1.ToString() });
				row.Cells.Add(new DataGridViewTextBoxCell() { Value = currC.Item2.Name });
				_childrenView.Rows.Add(row);
			}

			// Annex-Elemente schreiben
			if(_annex1CheckBox.Checked = _building.AnnexUnits.Count > 0)
			{
				_annex1ComboBox.SelectedItem = _building.AnnexUnits[0].Item1;
				_annex1XBox.Value = (decimal)_building.AnnexUnits[0].Item2;
				_annex1YBox.Value = (decimal)_building.AnnexUnits[0].Item3;
			}
			if(_annex2CheckBox.Checked = _building.AnnexUnits.Count > 1)
			{
				_annex2ComboBox.SelectedItem = _building.AnnexUnits[1].Item1;
				_annex2XBox.Value = (decimal)_building.AnnexUnits[1].Item2;
				_annex2YBox.Value = (decimal)_building.AnnexUnits[1].Item3;
			}
			if(_annex3CheckBox.Checked = _building.AnnexUnits.Count > 2)
			{
				_annex3ComboBox.SelectedItem = _building.AnnexUnits[2].Item1;
				_annex3XBox.Value = (decimal)_building.AnnexUnits[2].Item2;
				_annex3YBox.Value = (decimal)_building.AnnexUnits[2].Item3;
			}
			if(_annex4CheckBox.Checked = _building.AnnexUnits.Count > 3)
			{
				_annex4ComboBox.SelectedItem = _building.AnnexUnits[3].Item1;
				_annex4XBox.Value = (decimal)_building.AnnexUnits[3].Item2;
				_annex4YBox.Value = (decimal)_building.AnnexUnits[3].Item3;
			}

			// Alles geladen
			_loading = false;
		}

		/// <summary>
		/// Aktualisiert die Annex-Liste des Gebäudes aus den Steuerelementen.
		/// </summary>
		private void UpdateAnnexList()
		{
			// Wenn noch geladen wird, abbrechen
			if(_loading)
				return;

			// Liste löschen
			_building.AnnexUnits.Clear();
			if(_annex1CheckBox.Checked)
				_building.AnnexUnits.Add(new Tuple<TechTreeBuilding, float, float>((TechTreeBuilding)_annex1ComboBox.SelectedItem, (float)_annex1XBox.Value, (float)_annex1YBox.Value));
			if(_annex2CheckBox.Checked)
				_building.AnnexUnits.Add(new Tuple<TechTreeBuilding, float, float>((TechTreeBuilding)_annex2ComboBox.SelectedItem, (float)_annex2XBox.Value, (float)_annex2YBox.Value));
			if(_annex3CheckBox.Checked)
				_building.AnnexUnits.Add(new Tuple<TechTreeBuilding, float, float>((TechTreeBuilding)_annex3ComboBox.SelectedItem, (float)_annex3XBox.Value, (float)_annex3YBox.Value));
			if(_annex4CheckBox.Checked)
				_building.AnnexUnits.Add(new Tuple<TechTreeBuilding, float, float>((TechTreeBuilding)_annex4ComboBox.SelectedItem, (float)_annex4XBox.Value, (float)_annex4YBox.Value));
		}

		#endregion

		#region Ereignishandler

		private void _ageUpgradeListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Falls das ausgewählte Element aktiv ist, Auswahlliste freischalten
			if(_ageUpgradeListBox.GetItemChecked(_ageUpgradeListBox.SelectedIndex))
			{
				// Auswahlliste freischalten
				_ageUpgradeComboBox.Enabled = true;

				// Element auswählen
				_ageUpgradeComboBox.SelectedItem = _building.AgeUpgrades[_ageUpgradeListBox.SelectedIndex + _building.Age + 1];
			}
			else
			{
				// Auswahlliste sperren
				_ageUpgradeComboBox.Enabled = false;
			}
		}

		private void _ageUpgradeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Gebäude setzen
			_building.AgeUpgrades[_ageUpgradeListBox.SelectedIndex + _building.Age + 1] = (TechTreeBuilding)_ageUpgradeComboBox.SelectedItem;
		}

		private void _ageUpgradeListBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			// Wird das Element selektiert?
			if(e.NewValue == CheckState.Checked)
			{
				// Standardwert setzen
				if(!_building.AgeUpgrades.ContainsKey(e.Index + _building.Age + 1))
					_building.AgeUpgrades[e.Index + _building.Age + 1] = _building;
			}
			else
			{
				// Wert löschen
				_building.AgeUpgrades.Remove(e.Index + _building.Age + 1);
			}
		}

		private void _deadUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeUnit selItem = (TechTreeUnit)_deadUnitComboBox.SelectedItem;
			_building.DeadUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _projUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeProjectile selItem = (TechTreeProjectile)_projUnitComboBox.SelectedItem;
			_building.ProjectileUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _projDuplUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeProjectile selItem = (TechTreeProjectile)_projDuplUnitComboBox.SelectedItem;
			_building.ProjectileDuplicationUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _stackUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeBuilding selItem = (TechTreeBuilding)_stackUnitComboBox.SelectedItem;
			_building.StackUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _headUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeBuilding selItem = (TechTreeBuilding)_headUnitComboBox.SelectedItem;
			_building.HeadUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _transformUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeBuilding selItem = (TechTreeBuilding)_transformUnitComboBox.SelectedItem;
			_building.TransformUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _buildingDepView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// Anzahl-Spalte
			if(e.ColumnIndex == 0)
			{
				// Der Wert muss numerisch sein
				int val = 0;
				if(!int.TryParse((string)e.FormattedValue, out val) || val < 0)
				{
					// Fehler
					MessageBox.Show("Bitte gib eine positive ganze Zahl an!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
				}
				else
				{
					// Wert speichern
					_building.BuildingDependencies[(TechTreeBuilding)_buildingDepView.Rows[e.RowIndex].Tag] = val;
				}
			}
		}

		private void _childrenView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// Button-Spalte
			if(e.ColumnIndex == 0)
			{
				// Der Wert muss numerisch sein
				byte val = 0;
				if(!byte.TryParse((string)e.FormattedValue, out val) || val < 0)
				{
					// Fehler
					MessageBox.Show("Bitte gib eine positive ganze Zahl an!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
				}
				else
				{
					// Wert speichern
					int index = _building.Children.FindIndex(c => c.Item2 == (TechTreeElement)_childrenView.Rows[e.RowIndex].Tag);
					_building.Children[index] = new Tuple<byte, TechTreeElement>(val, (TechTreeElement)_childrenView.Rows[e.RowIndex].Tag);
				}
			}
		}

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		private void _annex1CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Felder schützen/sperren
			_annex1ComboBox.Enabled = _annex1CheckBox.Checked;
			_annex1XBox.Enabled = _annex1CheckBox.Checked;
			_annex1YBox.Enabled = _annex1CheckBox.Checked;

			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex2CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Felder schützen/sperren
			_annex2ComboBox.Enabled = _annex2CheckBox.Checked;
			_annex2XBox.Enabled = _annex2CheckBox.Checked;
			_annex2YBox.Enabled = _annex2CheckBox.Checked;

			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex3CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Felder schützen/sperren
			_annex3ComboBox.Enabled = _annex3CheckBox.Checked;
			_annex3XBox.Enabled = _annex3CheckBox.Checked;
			_annex3YBox.Enabled = _annex3CheckBox.Checked;

			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex4CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Felder schützen/sperren
			_annex4ComboBox.Enabled = _annex4CheckBox.Checked;
			_annex4XBox.Enabled = _annex4CheckBox.Checked;
			_annex4YBox.Enabled = _annex4CheckBox.Checked;

			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex4ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex1XBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex2XBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex3XBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex4XBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex1YBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex2YBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex3YBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		private void _annex4YBox_ValueChanged(object sender, EventArgs e)
		{
			// Aktualisieren
			UpdateAnnexList();
		}

		#endregion
	}
}
