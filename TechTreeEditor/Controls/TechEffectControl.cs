using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor.Controls
{
	/// <summary>
	/// Definiert ein Steuerelement zum Bearbeiten einer Technologie-Effekt-Liste.
	/// </summary>
	public partial class TechEffectControl : UserControl
	{
		#region Variablen

		/// <summary>
		/// Die zu bearbeitende Effekt-Liste.
		/// </summary>
		private List<TechEffect> _effectList;

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
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.AttributeSet, Strings.TechEffectControl_EffectTypes_AttributeSet),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResourceSetPM, Strings.TechEffectControl_EffectTypes_ResourceSetPM),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.AttributePM, Strings.TechEffectControl_EffectTypes_AttributePM),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.AttributeMult, Strings.TechEffectControl_EffectTypes_AttributeMult),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResourceMult, Strings.TechEffectControl_EffectTypes_ResourceMult),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResearchCostSetPM, Strings.TechEffectControl_EffectTypes_ResearchCostSetPM),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResearchDisable, Strings.TechEffectControl_EffectTypes_ResearchDisable),
			new KeyValuePair<TechEffect.EffectType,string>( TechEffect.EffectType.ResearchTimeSetPM, Strings.TechEffectControl_EffectTypes_ResearchTimeSetPM)
        }).ToList();

		/// <summary>
		/// Leere Einheit.
		/// </summary>
		private static TechTreeUnit _emptyUnit = new TechTreeCreatable() { ID = -1, Name = Strings.Common_EmptyElementName };

		/// <summary>
		/// Leere Technologie.
		/// </summary>
		private static TechTreeResearch _emptyResearch = new TechTreeResearch() { ID = -1, Name = Strings.Common_EmptyElementName };

		/// <summary>
		/// Die gecachten Einheiten-Klassen.
		/// </summary>
		private static string[] _classes = null;

		/// <summary>
		/// Die gecachten Ressourcen.
		/// </summary>
		private static string[] _resourceTypes = null;

		/// <summary>
		/// Die gecachten Rüstungsklassen.
		/// </summary>
		private static string[] _armourClasses = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public TechEffectControl()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Effekt-Typ-Liste zuweisen
			_effectTypeComboBox.ValueMember = "Key";
			_effectTypeComboBox.DisplayMember = "Value";
			_effectTypeComboBox.DataSource = _effectTypes;

			// Klassen übergeben
			if(_classes == null)
				_classes = Strings.ClassNames.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			_classComboBox.Items.AddRange(_classes);
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
			if(_armourClasses == null)
				_armourClasses = Strings.ArmourClasses.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			_armourClassComboBox.Items.AddRange(_armourClasses);

			// Ressourcentypen übergeben
			if(_resourceTypes == null)
				_resourceTypes = Strings.ResourceTypes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			_resourceComboBox.Items.AddRange(_resourceTypes);
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
			object selection = _effectsListBox.SelectedItem;
			_effectsListBox.SuspendLayout();
			_effectsListBox.DataSource = null;
			_effectsListBox.DataSource = _effectList;
			_effectsListBox.ResumeLayout();
			_effectsListBox.SelectedItem = selection;

			// Fertig
			_updating = false;
		}

		#endregion Funktionen

		#region Ereignishandler

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
						_valueField.Visible = true;
						_valueField.Value = (decimal)sel.Value;
						break;

					case TechEffect.EffectType.ResourceMult:
						_resourceLabel.Visible = _resourceComboBox.Visible = true;
						_resourceComboBox.SelectedIndex = sel.ParameterID;
						_valueField.Visible = true;
						_valueField.Value = (decimal)sel.Value;
						break;

					case TechEffect.EffectType.ResearchCostSetPM:
						_researchField.Visible = true;
						_researchField.Value = (sel.Element == null ? _emptyResearch : sel.Element);
						_resourceLabel.Visible = _resourceComboBox.Visible = true;
						_resourceComboBox.SelectedIndex = sel.ParameterID;
						_modeCheckBox.Visible = true;
						_modeCheckBox.Value = sel.Mode == TechEffect.EffectMode.PM_Enable;
						_valueField.Visible = true;
						_valueField.Value = (decimal)sel.Value;
						break;

					case TechEffect.EffectType.ResearchDisable:
						_researchField.Visible = true;
						_researchField.Value = (sel.Element == null ? _emptyResearch : sel.Element);
						break;

					case TechEffect.EffectType.ResearchTimeSetPM:

						_researchField.Visible = true;
						_researchField.Value = (sel.Element == null ? _emptyResearch : sel.Element);
						_modeCheckBox.Visible = true;
						_modeCheckBox.Value = sel.Mode == TechEffect.EffectMode.PM_Enable;
						_valueField.Visible = true;
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
				TechEffect temp = _effectList[_effectsListBox.SelectedIndex - 1];
				_effectList[_effectsListBox.SelectedIndex - 1] = _effectList[_effectsListBox.SelectedIndex];
				_effectList[_effectsListBox.SelectedIndex] = temp;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _effectDownButton_Click(object sender, EventArgs e)
		{
			// Ausgewählten Effekt mit unterem vertauschen
			if(_effectsListBox.SelectedIndex >= 0 && _effectsListBox.SelectedIndex < _effectList.Count - 1)
			{
				// Effekte vertauschen
				TechEffect temp = _effectList[_effectsListBox.SelectedIndex + 1];
				_effectList[_effectsListBox.SelectedIndex + 1] = _effectList[_effectsListBox.SelectedIndex];
				_effectList[_effectsListBox.SelectedIndex] = temp;

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _newEffectButton_Click(object sender, EventArgs e)
		{
			// Ist ein Effekt ausgewählt?
			if(_effectsListBox.SelectedIndex >= 0)
			{
				// Effekt kopieren
				TechEffect baseEff = _effectList[_effectsListBox.SelectedIndex];
				_effectList.Add(new TechEffect()
				{
					ClassID = baseEff.ClassID,
					DestinationElement = baseEff.DestinationElement,
					Element = baseEff.Element,
					Mode = baseEff.Mode,
					ParameterID = baseEff.ParameterID,
					Type = baseEff.Type,
					Value = baseEff.Value
				});
			}
			else
			{
				// Neuen leeren Effekt hinzufügen
				_effectList.Add(new TechEffect());
			}

			// Aktualisieren
			UpdateEffectList();
		}

		private void _deleteEffectButton_Click(object sender, EventArgs e)
		{
			// Effekt löschen
			if(_effectsListBox.SelectedIndex >= 0 && MessageBox.Show(Strings.TechEffectControl_Message_DeleteEffect, Strings.TechEffectControl_Message_DeleteEffect_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Effekt löschen
				_effectList.RemoveAt(_effectsListBox.SelectedIndex);

				// Aktualisieren
				UpdateEffectList();
			}
		}

		private void _sortEffectsButton_Click(object sender, EventArgs e)
		{
			// Effekte sortieren
			if(MessageBox.Show(Strings.TechEffectControl_Message_SortEffects, Strings.TechEffectControl_Message_SortEffects_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Effekte sortieren
				_effectList.Sort((eff1, eff2) => eff1.ToString().CompareTo(eff2.ToString()));

				// Aktualisieren
				UpdateEffectList();
			}
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

			// Steuerelemente aktualisieren, indem neue Auswahl vorgetäuscht wird
			_effectsListBox_SelectedIndexChanged(sender, e);
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
				if(sel.Element == _emptyUnit)
					sel.Element = null;

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
				if(sel.DestinationElement == _emptyUnit)
					sel.DestinationElement = null;

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
				if(sel.Element == _emptyResearch)
					sel.Element = null;

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

		#region Eigenschaften

		/// <summary>
		/// Ruft die Projektdatei ab oder setzt diese.
		/// </summary>
		public TechTreeFile ProjectFile
		{
			get
			{
				return _projectFile;
			}
			set
			{
				// Merken
				_projectFile = value;

				// Gültiger Wert übergeben?
				if(_projectFile != null)
				{
					// Einheit-Liste erstellen
					List<TechTreeElement> unitList = _projectFile.Where(elem => elem is TechTreeUnit);
					unitList.Add(_emptyUnit);
					unitList.Sort((x, y) => x.ID.CompareTo(y.ID));
					List<object> tmpUList = unitList.Cast<object>().ToList();

					// Einheiten-Listen an Controls übergeben
					_unitField.ElementList = tmpUList;
					_destUnitField.ElementList = tmpUList;

					// Technologie-Liste erstellen
					List<TechTreeElement> resList = _projectFile.Where(elem => elem.GetType() == typeof(TechTreeResearch));
					resList.Add(_emptyResearch);
					resList.Sort((x, y) => x.ID.CompareTo(y.ID));
					List<object> tmpRList = resList.Cast<object>().ToList();

					// Technologie-Liste übergeben
					_researchField.ElementList = tmpRList;
				}
			}
		}

		/// <summary>
		/// Ruft die zu bearbeitende Effekt-Liste oder setzt diese.
		/// </summary>
		public List<TechEffect> EffectList
		{
			get
			{
				return _effectList;
			}
			set
			{
				// Effektliste setzen
				_effectList = value;

				// Effektliste füllen
				if(_effectList != null)
					_effectsListBox.DataSource = _effectList;
			}
		}

		#endregion Eigenschaften
	}
}