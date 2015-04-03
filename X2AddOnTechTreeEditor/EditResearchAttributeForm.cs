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
	public partial class EditResearchAttributeForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die referenzierte Baum-Technologie.
		/// </summary>
		TechTreeResearch _treeResearch;

		/// <summary>
		/// Die aktuelle Projektdatei.
		/// </summary>
		TechTreeFile _projectFile;

		/// <summary>
		/// Die gecachten Attribute.
		/// </summary>
		static List<KeyValuePair<int, string>> _attributes = null;

		/// <summary>
		/// Gibt an, ob das Formular komplett geladen ist.
		/// </summary>
		bool _formLoaded = false;

		#endregion

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

			// Effektliste füllen
			_effectsListBox.DataSource = _treeResearch.Effects;

			// Einheit-Liste erstellen
			TechTreeUnit emptyU = new TechTreeCreatable() { ID = -1, Name = "[Keine]" };
			List<TechTreeElement> unitList = _projectFile.Where(elem => elem is TechTreeUnit);
			unitList.Add(emptyU);
			unitList.Sort((x, y) => x.ID.CompareTo(y.ID));
			List<object> tmpUList = unitList.Cast<object>().ToList();

			// Technologie-Liste erstellen
			TechTreeResearch emptyR = new TechTreeResearch() { ID = -1, Name = "[Keine]" };
			List<TechTreeElement> resList = _projectFile.Where(elem => elem.GetType() == typeof(TechTreeResearch));
			resList.Add(emptyR);
			resList.Sort((x, y) => x.ID.CompareTo(y.ID));
			List<object> tmpRList = resList.Cast<object>().ToList();

			// Einheiten-Listen an Controls übergeben
			_unitField.ElementList = tmpUList;
			_destUnitField.ElementList = tmpUList;

			// Technologie-Liste übergeben
			_researchField.ElementList = tmpRList;

			// Klassen übergeben
			_classComboBox.Items.AddRange(Strings.ClassNames.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

			// Attribute übergeben
			if(_attributes == null)
			{
				_attributes = new List<KeyValuePair<int, string>>();
				foreach(string a in Strings.Attributes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
					_attributes.Add(new KeyValuePair<int, string>(int.Parse(a.Substring(0, a.IndexOf(':'))), a.Substring(a.IndexOf(':') + 2)));
			}
			_attributeComboBox.ValueMember = "Key";
			_attributeComboBox.DisplayMember = "Value";
			_attributeComboBox.DataSource = _attributes;

			// Rüstungsklassen übergeben
			_armourClassComboBox.Items.AddRange(Strings.ArmourClasses.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

			// Ressourcentypen übergeben
			_resourceComboBox.Items.AddRange(Strings.ResourceTypes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

			// Fertig
			_formLoaded = true;
		}

		#endregion

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

		private void _effectsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Erstmal alle Elemente abschalten, um den Code übersichtlich zu halten, der Performance-Verlust ist minimal
			_unitField.Enabled = false;
			_destUnitField.Enabled = false;
			_researchField.Enabled = false;
			_classLabel.Enabled = _classComboBox.Enabled = false;
			_attributeLabel.Enabled = _attributeComboBox.Enabled = false;
			_armourClassLabel.Enabled = _armourClassComboBox.Enabled = false;
			_resourceLabel.Enabled = _resourceComboBox.Enabled = false;
			_modeCheckBox.Enabled = false;
			_valueField.Enabled = false;

			// Element-Informationen anzeigen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			switch(sel.Type)
			{
				case TechEffect.EffectType.AttributeSet:

					break;
			}
		}

		#endregion

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

		#endregion

		#endregion
	}
}
