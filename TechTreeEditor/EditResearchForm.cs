using System;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	public partial class EditResearchForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die in diesem Fenster bearbeitbare Technologie.
		/// </summary>
		private TechTreeResearch _research = null;

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		#endregion Variablen

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
				row.Cells.Add(new DataGridViewCheckBoxCell() { Value = currD.Value });
				row.Cells.Add(new DataGridViewTextBoxCell() { Value = currD.Key.Name });
				_buildingDepView.Rows.Add(row);
			}
		}

		#endregion Funktionen

		#region Ereignishandler

		private void _buildingDepView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			// Anzahl-Spalte
			if(e.ColumnIndex == 0 && _research != null) // Verhindert, dass Ereignis schon bei Initialisierung ausgelöst wird
			{
				// Wert speichern
				_research.BuildingDependencies[(TechTreeBuilding)_buildingDepView.Rows[e.RowIndex].Tag] = (bool)_buildingDepView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			}
		}

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		#endregion Ereignishandler
	}
}