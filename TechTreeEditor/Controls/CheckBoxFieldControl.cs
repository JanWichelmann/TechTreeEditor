using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TechTreeEditor.Controls
{
	/// <summary>
	/// Definiert ein Steuerelement zur Eingabe eines Zahlenwerts.
	/// Hier wird mit decimal-Werten gearbeitet, um einen großen Wertebereich zu unterstützen, da sich in C# leider keine generischen Zahlen-Typen herausfiltern lassen.
	/// </summary>
	[DefaultEvent("ValueChanged")]
	public partial class CheckBoxFieldControl : UserControl
	{
		#region Variablen

		/// <summary>
		/// Der aktuelle Wert.
		/// </summary>
		private bool _value = false;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public CheckBoxFieldControl()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		#endregion Funktionen

		#region Eventhandler

		private void _checkBox_CheckedChanged(object sender, EventArgs e)
		{
			// Neuen Wert merken
			_value = _checkBox.Checked;

			// Ereignis weiterreichen
			OnValueChanged(new ValueChangedEventArgs(_value));
		}

		#endregion Eventhandler

		#region Eigenschaften

		/// <summary>
		/// Ruft den Wert ab oder setzt diesen.
		/// </summary>
		public bool Value
		{
			get
			{
				return _value;
			}
			set
			{
				_checkBox.Checked = value;
			}
		}

		/// <summary>
		/// Ruft den Namenstext ab oder setzt diesen.
		/// </summary>
		public string NameString
		{
			get
			{
				return _checkBox.Text;
			}
			set
			{
				_checkBox.Text = value;
			}
		}

		#endregion Eigenschaften

		#region Ereignisse

		#region Event: Geänderter Wert

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
			private bool _newValue;

			/// <summary>
			/// Ruft den neuen Wert ab.
			/// </summary>
			public bool NewValue
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
			public ValueChangedEventArgs(bool newValue)
			{
				// Parameter speichern
				_newValue = newValue;
			}
		}

		#endregion Event: Geänderter Wert

		#endregion Ereignisse
	}
}