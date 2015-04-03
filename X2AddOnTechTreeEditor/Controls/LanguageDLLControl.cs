using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X2AddOnTechTreeEditor.Controls
{
	/// <summary>
	/// Definiert ein Steuerelement zur Auswahl einer Language-DLL-ID mit integrierter Vorschau des gewählten Werts.
	/// </summary>
	[DefaultEvent("ValueChanged")]
	public partial class LanguageDLLControl : UserControl
	{
		#region Variablen

		/// <summary>
		/// Das aktuell geladene Projekt.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Die aktuelle ID.
		/// </summary>
		private int _value = 0;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public LanguageDLLControl()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		#endregion

		#region Eventhandler

		private void _idTextBox_TextChanged(object sender, EventArgs e)
		{
			// Projekt geladen?
			if(_projectFile != null)
			{
				// Wert parsen
				int value;
				if(int.TryParse(_idTextBox.Text, out value) && value >= 0)
				{
					// Neuen Wert merken
					_value = value;

					// Farbe setzen
					_idTextBox.BackColor = Color.White;

					// Label aktualisieren
					_valueLabel.Text = _projectFile.LanguageFileWrapper.GetString((uint)value).Replace('\n', ' ');
					if(string.IsNullOrWhiteSpace(_valueLabel.Text))
						_valueLabel.Text = "???";

					// Ereignis auslösen
					OnValueChanged(new ValueChangedEventArgs(_value));
				}
				else
				{
					// Farbe setzen
					_idTextBox.BackColor = Color.PaleVioletRed;
				}
			}
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft die ID ab oder setzt diese.
		/// </summary>
		public int Value
		{
			get
			{
				return _value;
			}
			set
			{
				_idTextBox.Text = value.ToString();
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
				_projectFile = value;
			}
		}

		#endregion

		#region Ereignisse

		#region Event: Geänderte ID

		/// <summary>
		/// Wird ausgelöst, wenn sich die ID ändert.
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
			private int _newValue;

			/// <summary>
			/// Ruft den neuen Wert ab.
			/// </summary>
			public int NewValue
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
			public ValueChangedEventArgs(int newValue)
			{
				// Parameter speichern
				_newValue = newValue;
			}
		}

		#endregion


		#endregion
	}
}
