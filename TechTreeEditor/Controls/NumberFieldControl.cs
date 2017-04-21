using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace TechTreeEditor.Controls
{
	/// <summary>
	/// Definiert ein Steuerelement zur Eingabe eines Zahlenwerts.
	/// Hier wird mit decimal-Werten gearbeitet, um einen großen Wertebereich zu unterstützen, da sich in C# leider keine generischen Zahlen-Typen herausfiltern lassen.
	/// </summary>
	[DefaultEvent("ValueChanged")]
	public partial class NumberFieldControl : UserControl
	{
		#region Variablen

		/// <summary>
		/// Der aktuelle Wert.
		/// </summary>
		private decimal _value = 0;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public NumberFieldControl()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		/// <summary>
		/// Setzt den TextBox-Cursor ans Ende der TextBox.
		/// </summary>
		public void SetCursorToEnd()
		{
			// Auswahl in TextBox ans Ende setzen
			_valueTextBox.Focus();
			_valueTextBox.SelectionStart = _valueTextBox.TextLength;
		}

		#endregion Funktionen

		#region Eventhandler

		private void _valueTextBox_TextChanged(object sender, EventArgs e)
		{
			// Wert parsen
			decimal value;
			if(decimal.TryParse(_valueTextBox.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture, out value))
			{
				// Neuen Wert merken
				_value = value;

				// Farbe setzen
				_valueTextBox.BackColor = Color.White;

				// Ereignis auslösen
				OnValueChanged(new ValueChangedEventArgs(_value));
			}
			else
			{
				// Farbe setzen
				_valueTextBox.BackColor = Color.PaleVioletRed;
			}
		}

		#endregion Eventhandler

		#region Eigenschaften

		/// <summary>
		/// Ruft den Wert ab oder setzt diesen.
		/// </summary>
		public decimal Value
		{
			get
			{
				return _value;
			}
			set
			{
				// Nicht beim Zuweisen ein Ereignis auslösen
				_valueTextBox.TextChanged -= _valueTextBox_TextChanged;
				_valueTextBox.Text = value.ToString(CultureInfo.InvariantCulture);
				_value = value;
				_valueTextBox.TextChanged += _valueTextBox_TextChanged;
			}
		}

		/// <summary>
		/// Ruft den Namenstext ab oder setzt diesen.
		/// </summary>
		[Localizable(true)]
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

		#endregion Eigenschaften

		#region Ereignisse

		#region Event: Geänderte ID

		/// <summary>
		/// Wird ausgelöst, wenn sich der Wert ändert.
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
			private decimal _newValue;

			/// <summary>
			/// Ruft den neuen Wert ab.
			/// </summary>
			public decimal NewValue
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
			public ValueChangedEventArgs(decimal newValue)
			{
				// Parameter speichern
				_newValue = newValue;
			}
		}

		#endregion Event: Geänderte ID

		#endregion Ereignisse
	}
}