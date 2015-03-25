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
	/// Definiert ein Steuerelement zur vereinfachten Auswahl einer Grafik-ID.
	/// </summary>
	public partial class GraphicControl : UserControl
	{
		#region Variablen

		public static List<GenieLibrary.DataElements.Graphic> cache = null;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public GraphicControl()
		{
			// Steuerelemente laden
			InitializeComponent();
		}

		#endregion

		#region Eventhandler

		private void _idComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			// Wurde ein Wert ausgewählt?
			if(_idComboBox.SelectedItem == null)
				return;

			// Ereignis weiterreichen
			OnValueChanged(new ValueChangedEventArgs((GenieLibrary.DataElements.Graphic)_idComboBox.SelectedItem));
		}

		#endregion

		#region Eigenschaften

		/// <summary>
		/// Ruft die ausgewählte Grafik ab oder setzt diese.
		/// </summary>
		public GenieLibrary.DataElements.Graphic Value
		{
			get
			{
				return (GenieLibrary.DataElements.Graphic)_idComboBox.SelectedItem;
			}
			set
			{
				_idComboBox.SelectedItem = value;
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
		/// Setzt die verwendete Grafik-Liste.
		/// </summary>
		public List<GenieLibrary.DataElements.Graphic> GraphicList
		{
			set
			{
				// ComboBox füllen
				if(value != null)
				{
					// Liste anbinden
					_idComboBox.Items.Clear();
					_idComboBox.Items.AddRange(value.ToArray());
				}
			}
		}

		#endregion

		#region Ereignisse

		#region Event: Geänderte ID

		/// <summary>
		/// Wird ausgelöst, wenn sich die ausgewählte Grafik ändert.
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
			private GenieLibrary.DataElements.Graphic _newValue;

			/// <summary>
			/// Ruft den neuen Wert ab.
			/// </summary>
			public GenieLibrary.DataElements.Graphic NewValue
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
			public ValueChangedEventArgs(GenieLibrary.DataElements.Graphic newValue)
			{
				// Parameter speichern
				_newValue = newValue;
			}
		}

		#endregion

		#endregion
	}
}
