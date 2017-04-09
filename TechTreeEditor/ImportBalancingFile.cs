using GenieLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	/// <summary>
	/// Bietet Funktionen für das Importieren und Anwenden einer Balancing-Datei.
	/// </summary>
	public partial class ImportBalancingFile : Form
	{
		#region Variablen

		/// <summary>
		/// Die aktuelle Projektdatei.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Die aktuelle Balancingdatei.
		/// </summary>
		private BalancingFile _balancingFile = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public ImportBalancingFile(TechTreeFile projectFile, BalancingFile balancingFile)
		{
			// Steuerelemente laden
			InitializeComponent();

			// Parameter merken
			_projectFile = projectFile;
			_balancingFile = balancingFile;

			// Geänderte Objekte in Listen schreiben
			foreach(var u in balancingFile.UnitEntries.Where(u => u.Value.HasModifiedFields))
				_changedUnitsListBox.Items.Add(new KeyValuePair<short, string>(u.Key, u.Value.DisplayName), true);
			_changedUnitsListBox.DisplayMember = "Value";
			foreach(var r in balancingFile.ResearchEntries.Where(r => r.Value.HasModifiedFields))
				_changedResearchesListBox.Items.Add(new KeyValuePair<short, string>(r.Key, r.Value.DisplayName), true);
			_changedResearchesListBox.DisplayMember = "Value";
		}

		#endregion Funktionen

		#region Ereignishandler


		#endregion

		private void _okButton_Click(object sender, EventArgs e)
		{
			// Alle ausgewählten Einheiten-Änderungen auf alle Zivilisationen anwenden
			var checkedUnitIds = _changedUnitsListBox.CheckedItems.Cast<KeyValuePair<short, string>>().Select(u => u.Key);
			var checkedUnitEntries = _balancingFile.UnitEntries.Where(u => checkedUnitIds.Contains(u.Key));
			foreach(GenieLibrary.DataElements.Civ c in _projectFile.BasicGenieFile.Civs)
				foreach(KeyValuePair<short, UnitEntry> ue in checkedUnitEntries)
				{
					// Falls Einheit existiert, Änderungen anwenden
					if(!c.Units.ContainsKey(ue.Key))
						continue;
					ue.Value.WriteChangesToGenieUnit(c.Units[ue.Key]);
				}

			// Alle ausgewählten Technologie-Änderungen anwenden
			var checkedResearchIds = _changedResearchesListBox.CheckedItems.Cast<KeyValuePair<short, string>>().Select(r => r.Key);
			var checkedResearchEntries = _balancingFile.ResearchEntries.Where(u => checkedResearchIds.Contains(u.Key));
			foreach(KeyValuePair<short, ResearchEntry> re in checkedResearchEntries)
				re.Value.WriteChangesToGenieResearch(_projectFile.BasicGenieFile.Researches[re.Key]);

			// Fertig, Fenster schließen
			Close();
		}

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Abbruch
			Close();
		}
	}
}