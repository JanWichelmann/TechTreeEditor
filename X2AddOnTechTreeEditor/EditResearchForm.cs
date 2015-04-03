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
	public partial class EditResearchForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die in diesem Fenster bearbeitbare Technologie.
		/// </summary>
		TechTreeResearch _research = null;

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
		private EditResearchForm()
		{
			// Steuerelement laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Technologie-Bearbeitungsfenster.
		/// </summary>
		/// <param name="projectile">Die zu bearbeitende Technologie.</param>
		public EditResearchForm(TechTreeFile projectFile, TechTreeResearch research)
			: this()
		{
			// Parameter merken
			_research = research;
			_projectFile = projectFile;

			// Fenstertitel aktualisieren
			this.Text += _research.Name;

			// Dummy-Objekte für leere Listenelemente erstellen
			TechTreeBuilding emptyBuilding = new TechTreeBuilding() { ID = -1 };

			// Gebäude-Abhängigkeiten in View schreiben
			foreach(var currD in _research.BuildingDependencies)
			{
				// Zeile erstellen
				DataGridViewRow row = new DataGridViewRow() { Tag = currD.Key };
				row.Cells.Add(new DataGridViewTextBoxCell() { Value = currD.Value.ToString() });
				row.Cells.Add(new DataGridViewTextBoxCell() { Value = currD.Key.Name });
				_buildingDepView.Rows.Add(row);
			}

			// Alles geladen
			_loading = false;
		}

		#endregion

		#region Ereignishandler

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
					_research.BuildingDependencies[(TechTreeBuilding)_buildingDepView.Rows[e.RowIndex].Tag] = val;
				}
			}
		}

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		#endregion
	}
}
