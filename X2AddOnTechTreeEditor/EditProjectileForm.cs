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
	public partial class EditProjectileForm : Form
	{
		#region Variablen

		/// <summary>
		/// Das in diesem Fenster bearbeitbare Gebäude.
		/// </summary>
		TechTreeProjectile _projectile = null;

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		TechTreeFile _projectFile = null;

		#endregion

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
			_trackUnitComboBox.Items.Add(emptyEyeCandy);

			// Trackingliste erstellen
			_trackUnitComboBox.SuspendLayout();
			_trackUnitComboBox.DisplayMember = "Name";
			_projectFile.Where(u => u.GetType() == typeof(TechTreeEyeCandy)).ForEach(u =>
			{
				_trackUnitComboBox.Items.Add(u);
			});
			_trackUnitComboBox.ResumeLayout();

			// Elemente auswählen
			_trackUnitComboBox.SelectedItem = (_projectile.TrackingUnit == null ? emptyEyeCandy : _projectile.TrackingUnit);
		}

		#endregion

		#region Ereignishandler

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

		#endregion
	}
}
