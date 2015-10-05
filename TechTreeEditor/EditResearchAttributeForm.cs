using System;
using System.Drawing;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	public partial class EditResearchAttributeForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die referenzierte Baum-Technologie.
		/// </summary>
		private TechTreeResearch _treeResearch;

		/// <summary>
		/// Die aktuelle Projektdatei.
		/// </summary>
		private TechTreeFile _projectFile;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public EditResearchAttributeForm()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Attribut-Formular basierend auf der gegebenen Technologie.
		/// </summary>
		/// <param name="projectFile">Die aktuelle Projektdatei.</param>
		/// <param name="treeResearch">Die referenzierte Baum-Technologie.</param>
		public EditResearchAttributeForm(TechTreeFile projectFile, TechTreeResearch treeResearch)
			: this()
		{
			// Parameter speichern
			_projectFile = projectFile;
			_treeResearch = treeResearch;

			// Haupteinstellungen setzen
			_nameTextBox.Text = _treeResearch.DATResearch.Name.TrimEnd('\0');
			_iconIDField.Value = _treeResearch.DATResearch.IconID;
			_isAgeField.Value = _treeResearch.DATResearch.Type == 2;
			_fullTechModeField.Value = _treeResearch.DATResearch.FullTechMode > 0;

			// DLL-Werte einfügen
			_dllName1Field.ProjectFile = _projectFile;
			_dllName1Field.Value = treeResearch.DATResearch.LanguageDLLName1;
			_dllName2Field.ProjectFile = _projectFile;
			_dllName2Field.Value = (treeResearch.DATResearch.LanguageDLLName2 - 140000);
			_dllDescriptionField.ProjectFile = _projectFile;
			_dllDescriptionField.Value = treeResearch.DATResearch.LanguageDLLDescription;
			_dllHelpField.ProjectFile = _projectFile;
			_dllHelpField.Value = (treeResearch.DATResearch.LanguageDLLHelp - 79000);

			// Wenn alle vier Language-IDs denselben relativen Wert haben, diesen anzeigen
			int relID = _dllName1Field.Value % 1000;
			if(relID == _dllName2Field.Value % 1000 && relID == _dllDescriptionField.Value % 1000 && relID == _dllHelpField.Value % 1000)
				_relIDTextBox.Text = relID.ToString();

			// Ressourcen-Kosten setzen
			_cost1Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
			{
				Enabled = _treeResearch.DATResearch.ResourceCosts[0].Enabled > 0,
				Type = _treeResearch.DATResearch.ResourceCosts[0].Type,
				Amount = _treeResearch.DATResearch.ResourceCosts[0].Amount
			};
			_cost2Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
			{
				Enabled = _treeResearch.DATResearch.ResourceCosts[1].Enabled > 0,
				Type = _treeResearch.DATResearch.ResourceCosts[1].Type,
				Amount = _treeResearch.DATResearch.ResourceCosts[1].Amount
			};
			_cost3Field.Value = new GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool>()
			{
				Enabled = _treeResearch.DATResearch.ResourceCosts[2].Enabled > 0,
				Type = _treeResearch.DATResearch.ResourceCosts[2].Type,
				Amount = _treeResearch.DATResearch.ResourceCosts[2].Amount
			};

			// Sonstige Werte setzen
			_timeField.Value = _treeResearch.DATResearch.ResearchTime;
			_unknown1Field.Value = _treeResearch.DATResearch.Unknown1;

			// Effekt-Steuerelement füllen
			_techEffectField.ProjectFile = _projectFile;
			_techEffectField.EffectList = _treeResearch.Effects;
		}

		#endregion Funktionen

		#region Ereignishandler

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		private void _dllName1Field_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_treeResearch.DATResearch.LanguageDLLName1 = (ushort)e.NewValue;

			// Namen neu erstellen
			_treeResearch.UpdateName(_projectFile.LanguageFileWrapper);
		}

		private void _dllName2Field_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_treeResearch.DATResearch.LanguageDLLName2 = (int)e.NewValue + 140000;
		}

		private void _dllDescriptionField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_treeResearch.DATResearch.LanguageDLLDescription = (ushort)e.NewValue;
		}

		private void _dllHelpField_ValueChanged(object sender, Controls.LanguageDLLControl.ValueChangedEventArgs e)
		{
			// ID aktualisieren
			_treeResearch.DATResearch.LanguageDLLHelp = (int)e.NewValue + 79000;
		}

		private void _nameTextBox_TextChanged(object sender, EventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.Name = _nameTextBox.Text + "\0";

			// Namen neu erstellen
			_treeResearch.UpdateName(_projectFile.LanguageFileWrapper);
		}

		private void _iconIDField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.IconID = e.NewValue.SafeConvert<short>();
			OnIconChanged(new IconChangedEventArgs(_treeResearch));
		}

		private void _isAgeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.Type = (byte)(e.NewValue ? 2 : 0);
		}

		private void _fullTechModeField_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.FullTechMode = (byte)(e.NewValue ? 1 : 0);
		}

		private void _timeField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.ResearchTime = (short)e.NewValue;
		}

		private void _unknown1Field_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.Unknown1 = (int)e.NewValue;
		}

		private void _cost1Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.ResourceCosts[0] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>()
			{
				Enabled = (byte)(e.NewValue.Enabled ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = (short)e.NewValue.Amount
			};
		}

		private void _cost2Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.ResourceCosts[1] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>()
			{
				Enabled = (byte)(e.NewValue.Enabled ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = (short)e.NewValue.Amount
			};
		}

		private void _cost3Field_ValueChanged(object sender, Controls.ResourceCostControl.ValueChangedEventArgs e)
		{
			// Wert aktualisieren
			_treeResearch.DATResearch.ResourceCosts[2] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>()
			{
				Enabled = (byte)(e.NewValue.Enabled ? 1 : 0),
				Type = (short)e.NewValue.Type,
				Amount = (short)e.NewValue.Amount
			};
		}

		private void _relIDTextBox_TextChanged(object sender, EventArgs e)
		{
			// Wert parsen
			int relID;
			if(int.TryParse(_relIDTextBox.Text, out relID) && relID >= 0 && relID < 1000)
			{
				// Wert ist gültig
				_relIDTextBox.BackColor = SystemColors.Window;

				// Wert in alle vier Felder übertragen
				_dllName1Field.Value = 7000 + relID;
				_dllName2Field.Value = 17000 + relID;
				_dllDescriptionField.Value = 8000 + relID;
				_dllHelpField.Value = 28000 + relID;
			}
			else
			{
				// Den User auf die Falscheingabe hinweisen
				_relIDTextBox.BackColor = Color.Red;
			}
		}

		private void EditResearchAttributeForm_ResizeBegin(object sender, EventArgs e)
		{
			// Flackern vermeiden
			this.SuspendLayout();
		}

		private void EditResearchAttributeForm_ResizeEnd(object sender, EventArgs e)
		{
			// Flackern vermeiden
			this.ResumeLayout();
		}

		#endregion Ereignishandler

		#region Ereignisse

		#region Event: Geändertes Icon

		/// <summary>
		/// Wird ausgelöst, wenn sich das Icon der enthaltenen Technologie ändert.
		/// </summary>
		public event IconChangedEventHandler IconChanged;

		/// <summary>
		/// Der Handler-Typ für das IconChanged-Event.
		/// </summary>
		/// <param name="sender">Das auslösende Objekt.</param>
		/// <param name="e">Die Ereignisdaten.</param>
		public delegate void IconChangedEventHandler(object sender, IconChangedEventArgs e);

		/// <summary>
		/// Löst das IconChanged-Ereignis aus.
		/// </summary>
		/// <param name="e">Die Ereignisdaten.</param>
		protected virtual void OnIconChanged(IconChangedEventArgs e)
		{
			IconChangedEventHandler handler = IconChanged;
			if(handler != null)
				handler(this, e);
		}

		/// <summary>
		/// Die Ereignisdaten für das IconChanged-Event.
		/// </summary>
		public class IconChangedEventArgs : EventArgs
		{
			/// <summary>
			/// Die betroffene Technologie.
			/// </summary>
			private TechTreeResearch _research;

			/// <summary>
			/// Ruft die betroffene Technologie ab.
			/// </summary>
			public TechTreeResearch Research
			{
				get
				{
					return _research;
				}
			}

			/// <summary>
			/// Konstruktor.
			/// Erstellt ein neues Ereignisdaten-Objekt mit den gegebenen Daten.
			/// </summary>
			/// <param name="unit">Die betroffene Technologie.</param>
			public IconChangedEventArgs(TechTreeResearch research)
			{
				// Parameter speichern
				_research = research;
			}
		}

		#endregion Event: Geändertes Icon

		#endregion Ereignisse
	}
}