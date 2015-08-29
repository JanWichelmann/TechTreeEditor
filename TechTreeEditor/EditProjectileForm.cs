using System;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	public partial class EditProjectileForm : Form
	{
		#region Variablen

		/// <summary>
		/// Das in diesem Fenster bearbeitbare Gebäude.
		/// </summary>
		private TechTreeProjectile _projectile = null;

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private EditProjectileForm()
		{
			// Steuerelement laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Projektil-Bearbeitungsfenster.
		/// </summary>
		/// <param name="projectile">Die zu bearbeitende Einheit.</param>
		public EditProjectileForm(TechTreeFile projectFile, TechTreeProjectile projectile)
			: this()
		{
			// Parameter merken
			_projectile = projectile;
			_projectFile = projectFile;

			// Fenstertitel aktualisieren
			this.Text += _projectile.Name;

			// Dummy-Objekte für leere Listenelemente erstellen
			TechTreeEyeCandy emptyEyeCandy = new TechTreeEyeCandy() { ID = -1 };
			_deadUnitComboBox.Items.Add(emptyEyeCandy);
			_trackUnitComboBox.Items.Add(emptyEyeCandy);

			// Totenliste erstellen
			_deadUnitComboBox.SuspendLayout();
			_deadUnitComboBox.DisplayMember = "Name";
			_projectFile.Where(u => u is TechTreeUnit).ForEach(u =>
			{
				_deadUnitComboBox.Items.Add(u);
			});
			_deadUnitComboBox.ResumeLayout();

			// Trackingliste erstellen
			_trackUnitComboBox.SuspendLayout();
			_trackUnitComboBox.DisplayMember = "Name";
			_projectFile.Where(u => u.GetType() == typeof(TechTreeEyeCandy)).ForEach(u =>
			{
				_trackUnitComboBox.Items.Add(u);
			});
			_trackUnitComboBox.ResumeLayout();

			// Elemente auswählen
			_deadUnitComboBox.SelectedItem = (_projectile.DeadUnit == null ? emptyEyeCandy : _projectile.DeadUnit);
			_trackUnitComboBox.SelectedItem = (_projectile.TrackingUnit == null ? emptyEyeCandy : _projectile.TrackingUnit);
		}

		#endregion Funktionen

		#region Ereignishandler

		private void _deadUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeUnit selItem = (TechTreeUnit)_deadUnitComboBox.SelectedItem;
			_projectile.DeadUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _trackUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			TechTreeEyeCandy selItem = (TechTreeEyeCandy)_trackUnitComboBox.SelectedItem;
			_projectile.TrackingUnit = (selItem.ID >= 0 ? selItem : null);
		}

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		#endregion Ereignishandler
	}
}