using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace TechTreeEditor.Controls
{
	/// <summary>
	/// Definiert ein Steuerelement zur Auswahl von Ressourcen-Kosten.
	/// </summary>
	[DefaultEvent("ValueChanged")]
	public partial class ResourceCostControl : UserControl
	{
		#region Variablen

		/// <summary>
		/// Die Liste mit den Ressourcen-Typen.
		/// </summary>
		private static string[] _resourceList = null;

		/// <summary>
		/// Der ausgewählte Ressourcen-Wert.
		/// </summary>
		private GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool> _value;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public ResourceCostControl()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Ggf. Ressourcen-Liste laden
			if(_resourceList == null)
			{
				// Liste aus Programm-Ressourcen holen
				_resourceList = Strings.ResourceTypes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			}

			// Ressourcen-ComboBox füllen
			_typeComboBox.Items.AddRange(_resourceList);
		}

		#endregion

		#region Eventhandler

		private void _amountTextBox_TextChanged(object sender, EventArgs e)
		{
			// Wert parsen
			float value;
			if(float.TryParse(_amountTextBox.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture.NumberFormat, out value))
			{
				// Neuen Wert merken
				_value.Amount = value;

				// Farbe setzen
				_amountTextBox.BackColor = Color.White;

				// Ereignis auslösen
				OnValueChanged(new ValueChangedEventArgs(_value));
			}
			else
			{
				// Farbe setzen
				_amountTextBox.BackColor = Color.PaleVioletRed;
			}
		}

		private void _enableCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Je nach Zustand die Elemente ein-/ausschalten
			_typeComboBox.Enabled = _enableCheckBox.Checked;
			_amountTextBox.Enabled = _enableCheckBox.Checked;
			_value.Enabled = _enableCheckBox.Checked;

			// Ereignis auslösen
			OnValueChanged(new ValueChangedEventArgs(_value));
		}

		private void _typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Neuen Wert abrufen
			_value.Type = _typeComboBox.SelectedIndex;

			// Ereignis auslösen
			OnValueChanged(new ValueChangedEventArgs(_value));
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft die ID ab oder setzt diese.
		/// </summary>
		public GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool> Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				_enableCheckBox.Checked = _value.Enabled;
				_typeComboBox.SelectedIndex = _value.Type;
				_amountTextBox.Text = _value.Amount.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		/// <summary>
		/// Ruft den Namenstext ab oder setzt diesen.
		/// </summary>
		public string NameString
		{
			get
			{
				return _nameLabel.Text;
			}
			set
			{
				_nameLabel.Text = value;
			}
		}

		#endregion

		#region Ereignisse

		#region Event: Geänderter Wert

		/// <summary>
		/// Wird ausgelöst, wenn sich der Auswahlwert ändert.
		/// </summary>
		public event ValueChangedEventHandler ValueChanged;

		/// <summary>
		/// Der Handler-Typ für das ValueChanged-Event.
		/// </summary>
		/// <param name="sender">Das auslösende Objekt.</param>
		/// <param name="e">Die Ereignisdaten.</param>
		public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);

		/// <summary>
		/// Löst das ValueChanged-Ereignis aus.
		/// </summary>
		/// <param name="e">Die Ereignisdaten.</param>
		protected virtual void OnValueChanged(ValueChangedEventArgs e)
		{
			ValueChangedEventHandler handler = ValueChanged;
			if(handler != null)
				handler(this, e);
		}

		/// <summary>
		/// Die Ereignisdaten für das ValueChanged-Event.
		/// </summary>
		public class ValueChangedEventArgs : EventArgs
		{
			/// <summary>
			/// Der neue Wert.
			/// </summary>
			private GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool> _newValue;

			/// <summary>
			/// Ruft den neuen Wert ab.
			/// </summary>
			public GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool> NewValue
			{
				get
				{
					return _newValue;
				}
			}

			/// <summary>
			/// Konstruktor.
			/// Erstellt ein neues Ereignisdaten-Objekt mit den gegebenen Daten.
			/// </summary>
			/// <param name="newValue">Der neue Wert.</param>
			public ValueChangedEventArgs(GenieLibrary.IGenieDataElement.ResourceTuple<int, float, bool> newValue)
			{
				// Parameter speichern
				_newValue = newValue;
			}
		}

		#endregion

		#endregion
	}
}
