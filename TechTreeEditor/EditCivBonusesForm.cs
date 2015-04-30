using System;
using System.Windows.Forms;

namespace TechTreeEditor
{
	/// <summary>
	/// Definiert ein Fenster zum Editieren der Kultur-Boni.
	/// </summary>
	public partial class EditCivBonusesForm : Form
	{
		#region Variablen

		/// <summary>
		/// Das zu ändernde Kultur-Baum-Objekt.
		/// </summary>
		private TechTreeFile.CivTreeConfig _civConfig;

		/// <summary>
		/// Die zu aktuellen Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private EditCivBonusesForm()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Kultur-Bonus-Fenster.
		/// </summary>
		/// <param name="projectFile">Die zu aktuellen Projektdaten.</param>
		/// <param name="civConfig">Das zu ändernde Kultur-Baum-Objekt.</param>
		public EditCivBonusesForm(TechTreeFile projectFile, TechTreeFile.CivTreeConfig civConfig)
			: this()
		{
			// Parameter merken
			_projectFile = projectFile;
			_civConfig = civConfig;

			// Werte an Effekt-Controls übergeben
			_civBonusEffectField.ProjectFile = _projectFile;
			_civBonusEffectField.EffectList = _civConfig.Bonuses;
			_teamBonusEffectField.ProjectFile = _projectFile;
			_teamBonusEffectField.EffectList = _civConfig.TeamBonuses;
		}

		#endregion Funktionen

		#region Ereignishandler

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		#endregion Ereignishandler
	}
}