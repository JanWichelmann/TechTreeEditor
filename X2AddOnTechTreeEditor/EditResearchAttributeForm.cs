using System;
using System.Collections.Generic;
using System.Linq;
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
		private TechTreeResearch _treeResearch;

		/// <summary>
		/// Die aktuelle Projektdatei.
		/// </summary>
		private TechTreeFile _projectFile;

		/// <summary>
		/// Gibt an, ob gerade Werte geladen werden und deshalb Ereignisse unterbunden werden sollen.
		/// </summary>
		private bool _updating = false;

		/// <summary>
		/// Die gecachten Attribute.
		/// </summary>
		private static List<KeyValuePair<int, string>> _attributes = null;

		/// <summary>
		/// Die Beschreibungstexte der Effekt-Typen.
		/// </summary>
		private static List<KeyValuePair<TechEffect.EffectType, string>> _effectTypes = (new KeyValuePair<TechEffect.EffectType, string>[]
		{
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.AttributeSet, "Attribut setzen"),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResourceSetPM, "Ressource setzen/additiv ändern"),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.AttributePM, "Attribut additiv ändern"),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.AttributeMult, "Attribut multiplikativ ändern"),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResourceMult, "Ressource multiplikativ ändern"),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResearchCostSetPM, "Technologie-Kosten setzen/additiv ändern"),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResearchTimeSetPM, "Technologie-Entwicklungszeit setzen/additiv ändern")
		}).ToList();

		/// <summary>
		/// Leere Einheit.
		/// </summary>
		static TechTreeUnit _emptyUnit = new TechTreeCreatable() { ID = -1, Name = "[Keine]" };

		/// <summary>
		/// Leere Technologie
		/// </summary>
		static TechTreeResearch _emptyResearch = new TechTreeResearch() { ID = -1, Name = "[Keine]" };

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

			// Effekt-Typ-Liste zuweisen
			_effectTypeComboBox.ValueMember = "Key";
			_effectTypeComboBox.DisplayMember = "Value";
			_effectTypeComboBox.DataSource = _effectTypes;

			// Einheit-Liste erstellen
			List<TechTreeElement> unitList = _projectFile.Where(elem => elem is TechTreeUnit);
			unitList.Add(_emptyUnit);
			unitList.Sort((x, y) => x.ID.CompareTo(y.ID));
			List<object> tmpUList = unitList.Cast<object>().ToList();

			// Technologie-Liste erstellen
			List<TechTreeElement> resList = _projectFile.Where(elem => elem.GetType() == typeof(TechTreeResearch));
			resList.Add(_emptyResearch);
			resList.Sort((x, y) => x.ID.CompareTo(y.ID));
			List<object> tmpRList = resList.Cast<object>().ToList();

			// Einheiten-Listen an Controls übergeben
			_unitField.ElementList = tmpUList;
			_destUnitField.ElementList = tmpUList;

			// Technologie-Liste übergeben
			_researchField.ElementList = tmpRList;

			// Klassen übergeben
			_classComboBox.Items.AddRange(Strings.ClassNames.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
			_classComboBox.Items.Insert(0, "");

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

			// Effektliste füllen
			// Dies muss zum Schluss geschehen, da dabei das SelectedIndexChanged-Ereignis aufgerufen wird und dieses sonst lauter NullReference-Exceptions auslöst
			_effectsListBox.DataSource = _treeResearch.Effects;
		}

		/// <summary>
		/// Aktualisiert die Effekt-Liste.
		/// </summary>
		private void UpdateEffectList()
		{
			// Aktualisierungsvorgang läuft
			if(_updating)
				return;
			else
				_updating = true;

			// Aktualisieren
			_effectsListBox.SuspendLayout();
			_effectsListBox.DataSource = null;
			_effectsListBox.DataSource = _treeResearch.Effects;
			_effectsListBox.ResumeLayout();

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
			// Aktualisierungsvorgang läuft
			if(_updating)
				return;
			else
				_updating = true;

			// Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;

			// Erstmal alle Elemente abschalten, um den Code übersichtlich zu halten, der Performance-Verlust ist minimal
			_effectTypeLabel.Visible = _effectTypeComboBox.Visible = (sel != null);
			_unitField.Visible = false;
			_destUnitField.Visible = false;
			_researchField.Visible = false;
			_classLabel.Visible = _classComboBox.Visible = false;
			_attributeLabel.Visible = _attributeComboBox.Visible = false;
			_armourClassLabel.Visible = _armourClassComboBox.Visible = false;
			_resourceLabel.Visible = _resourceComboBox.Visible = false;
			_modeCheckBox.Visible = false;
			_valueField.Visible = false;

			// Element-Informationen anzeigen
			if(sel != null)
			{
				_effectTypeComboBox.SelectedValue = sel.Type;
				switch(sel.Type)
				{
					case TechEffect.EffectType.AttributeSet:
					case TechEffect.EffectType.AttributePM:
					case TechEffect.EffectType.AttributeMult:
						_unitField.Visible = true;
						_unitField.Value = (sel.Element == null ? _emptyUnit : sel.Element);
						_classLabel.Visible = _classComboBox.Visible = true;
						_classComboBox.SelectedIndex = sel.ClassID + 1;
						_attributeLabel.Visible = _attributeComboBox.Visible = true;
						_attributeComboBox.SelectedValue = (int)sel.ParameterID;
						_valueField.Visible = true;
						if(sel.ParameterID == 8 || sel.ParameterID == 9)
						{
							_armourClassLabel.Visible = _armourClassComboBox.Visible = true;
							_armourClassComboBox.SelectedIndex = ((int)sel.Value >> 8);
							_valueField.Value = ((int)sel.Value & 0xFF);
						}
						else
							_valueField.Value = (decimal)sel.Value;
						break;

					case TechEffect.EffectType.ResourceSetPM:
						_resourceLabel.Visible = _resourceComboBox.Visible = true;
						_resourceComboBox.SelectedIndex = sel.ParameterID;
						_modeCheckBox.Visible = true;
						_modeCheckBox.Value = sel.Mode == TechEffect.EffectMode.PM_Enable;
						_valueField.Value = (decimal)sel.Value;
						break;

					case TechEffect.EffectType.ResourceMult:
						_resourceLabel.Visible = _resourceComboBox.Visible = true;
						_resourceComboBox.SelectedIndex = sel.ParameterID;
						_valueField.Value = (decimal)sel.Value;
						break;

					case TechEffect.EffectType.ResearchCostSetPM:
						_researchField.Visible = true;
						_researchField.Value = (sel.Element == null ? _emptyResearch : sel.Element);
						_resourceLabel.Visible = _resourceComboBox.Visible = true;
						_resourceComboBox.SelectedIndex = sel.ParameterID;
						_modeCheckBox.Visible = true;
						_modeCheckBox.Value = sel.Mode == TechEffect.EffectMode.PM_Enable;
						_valueField.Value = (decimal)sel.Value;
						break;

					case TechEffect.EffectType.ResearchTimeSetPM:

						_researchField.Visible = true;
						_researchField.Value = (sel.Element == null ? _emptyResearch : sel.Element);
						_modeCheckBox.Visible = true;
						_modeCheckBox.Value = sel.Mode == TechEffect.EffectMode.PM_Enable;
						_valueField.Value = (decimal)sel.Value;
						break;
				}
			}

			// Fertig
			_updating = false;
		}

		private void _effectUpButton_Click(object sender, EventArgs e)
		{
			// Ausgewählten Effekt mit oberem vertauschen
			if(_effectsListBox.SelectedIndex > 0)
			{
				// Effekte vertauschen
				TechEffect temp = _treeResearch.Effects[_effectsListBox.SelectedIndex - 1];
				_treeResearch.Effects[_effectsListBox.SelectedIndex - 1] = _treeResearch.Effects[_effectsListBox.SelectedIndex];
				_treeResearch.Effects[_effectsListBox.SelectedIndex] = temp;
				--_effectsListBox.SelectedIndex;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _effectDownButton_Click(object sender, EventArgs e)
		{
			// Ausgewählten Effekt mit unterem vertauschen
			if(_effectsListBox.SelectedIndex >= 0 && _effectsListBox.SelectedIndex < _treeResearch.Effects.Count - 1)
			{
				// Effekte vertauschen
				TechEffect temp = _treeResearch.Effects[_effectsListBox.SelectedIndex + 1];
				_treeResearch.Effects[_effectsListBox.SelectedIndex + 1] = _treeResearch.Effects[_effectsListBox.SelectedIndex];
				_treeResearch.Effects[_effectsListBox.SelectedIndex] = temp;
				++_effectsListBox.SelectedIndex;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _newEffectButton_Click(object sender, EventArgs e)
		{
			// Neuen Effekt hinzufügen
			_treeResearch.Effects.Add(new TechEffect());

			// Aktualisieren
			UpdateEffectList();
		}

		private void _deleteEffectButton_Click(object sender, EventArgs e)
		{
			// Effekt löschen
			if(_effectsListBox.SelectedIndex >= 0 && MessageBox.Show("Diesen Effekt wirklich löschen?", "Effekt löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Effekt löschen
				_treeResearch.Effects.RemoveAt(_effectsListBox.SelectedIndex);

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _sortEffectsButton_Click(object sender, EventArgs e)
		{
			// Effekte sortieren
			if(MessageBox.Show("Wirklich alle Effekte alphabetisch sortieren?", "Effekte sortieren", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Effekte sortieren
				_treeResearch.Effects.Sort((eff1, eff2) => eff1.ToString().CompareTo(eff2.ToString()));

				// Aktualisieren
				UpdateEffectList();
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

		private void _effectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Typ der ausgewählten Technologie ändern
				sel.Type = (TechEffect.EffectType)_effectTypeComboBox.SelectedValue;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _unitField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Einheit ändern
				sel.Element = (TechTreeElement)_unitField.Value;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _destUnitField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Einheit ändern
				sel.DestinationElement = (TechTreeElement)_destUnitField.Value;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _researchField_ValueChanged(object sender, Controls.DropDownFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Technologie ändern
				sel.Element = (TechTreeElement)_researchField.Value;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _classComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Klasse ändern
				sel.ClassID = (short)(_classComboBox.SelectedIndex - 1);

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _attributeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Attribut ändern
				sel.ParameterID = (short)(int)_attributeComboBox.SelectedValue;

				// Falls Rüstungsklassen benötigt werden, diese anzeigen
				if(sel.ParameterID == 8 || sel.ParameterID == 9)
					_armourClassLabel.Visible = _armourClassComboBox.Visible = true;
				else
					_armourClassLabel.Visible = _armourClassComboBox.Visible = false;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _armourClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Wert ändern
				sel.Value = (float)(_armourClassComboBox.SelectedIndex << 8 | ((int)_valueField.Value & 0xFF));

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _resourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Ressource ändern
				sel.ParameterID = (short)_resourceComboBox.SelectedIndex;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _modeCheckBox_ValueChanged(object sender, Controls.CheckBoxFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Modus ändern
				sel.Mode = _modeCheckBox.Value ? TechEffect.EffectMode.PM_Enable : TechEffect.EffectMode.Set_Disable;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _valueField_ValueChanged(object sender, Controls.NumberFieldControl.ValueChangedEventArgs e)
		{
			// Aktualisierungsvorgang?
			if(_updating)
				return;

			// Ausgewähltes Element abrufen
			TechEffect sel = (TechEffect)_effectsListBox.SelectedItem;
			if(sel != null)
			{
				// Wert ändern
				if(_armourClassComboBox.Visible)
					sel.Value = (float)(_armourClassComboBox.SelectedIndex << 8 | ((int)_valueField.Value & 0xFF));
				else
					sel.Value = (float)_valueField.Value;

				// Aktualisieren
				UpdateEffectList();

				// Fokus halten
				_valueField.Focus();
				_valueField.SetCursorToEnd();
			}
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