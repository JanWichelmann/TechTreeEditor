using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TechTreeEditor.Controls
{
	/// <summary>
	/// Definiert ein Steuerelement zur vereinfachten Auswahl eines Elements aus einer großen Liste.
	/// </summary>
	[DefaultEvent("ValueChanged")]
	public partial class DropDownFieldControl : UserControl
	{
		#region Variablen

		/// <summary>
		/// Das Formular, das die Element-Liste enthält.
		/// Die ListBox muss in ein eigenes Formular, da diese sonst auf die Größe des Benutzersteuerelements beschränkt ist.
		/// </summary>
		private Form _listBoxForm;

		/// <summary>
		/// Die Element-Liste.
		/// </summary>
		private ListBox _elementListBox;

		/// <summary>
		/// Die Liste aller enthaltenen Elemente samt ihrer Namen.
		/// </summary>
		private Dictionary<string, object> _elementMap;

		/// <summary>
		/// Die Liste der Element-Namen.
		/// Wird anstatt von _elementMap benutzt, um schnelle Suchvorgänge ohne den Dictionary-Overhead durchführen zu können.
		/// </summary>
		private List<string> _elementNames;

		/// <summary>
		/// Der aktuell ausgewählte Wert.
		/// </summary>
		private object _value = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public DropDownFieldControl()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Formular erstellen
			_listBoxForm = new Form()
			{
				FormBorderStyle = FormBorderStyle.None,
				ShowInTaskbar = false,
				AutoSize = false
			};

			// ListBox erstellen
			_elementListBox = new ListBox()
			{
				Name = "_elementListBox",
				ScrollAlwaysVisible = true
			};
			_listBoxForm.Controls.Add(_elementListBox);
			_elementListBox.Dock = DockStyle.Fill;
			_elementListBox.KeyUp += _elementListBox_KeyUp;
			_elementListBox.Leave += _elementListBox_Leave;
			_elementListBox.DoubleClick += _elementListBox_DoubleClick;
		}

		#endregion Funktionen

		#region Eventhandler

		private void _idTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			// Auswahlliste ggf. anzeigen
			if(!_listBoxForm.Visible)
			{
				// Formular anzeigen
				_listBoxForm.Show(this);
				_listBoxForm.Location = this.PointToScreen(new Point(_idTextBox.Location.X, _idTextBox.Location.Y + _idTextBox.Height));

				// Focus auf der TextBox behalten
				_idTextBox.Focus();
			}

			// Nach Tasten unterscheiden
			if(e.KeyCode == Keys.Up)
			{
				// Ans Ende der ListBox springen
				if(_elementListBox.Items.Count > 0)
				{
					_elementListBox.Focus();
					_elementListBox.SelectedIndex = _elementListBox.Items.Count - 1;
				}
			}
			else if(e.KeyCode == Keys.Down)
			{
				// An den Anfang der ListBox springen
				if(_elementListBox.Items.Count > 0)
				{
					_elementListBox.Focus();
					_elementListBox.SelectedIndex = 0;
				}
			}
			else
			{
				// Liste filtern
				string searchText = _idTextBox.Text;
				string[] filteredList;
				if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) // Muss die komplette Liste durchsucht werden, oder reicht die bereits gefilterte?
					filteredList = _elementNames.Where(name => name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToArray();
				else
					filteredList = _elementListBox.Items.Cast<string>().Where(name => name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToArray();
				_elementListBox.Items.Clear();
				_elementListBox.Items.AddRange(filteredList);
			}
		}

		private void _elementListBox_KeyUp(object sender, KeyEventArgs e)
		{
			// Enter-Taste bestätigt die Auswahl
			if(e.KeyCode == Keys.Enter)
			{
				// Ausgewähltes Element in die TextBox schreiben
				_idTextBox.Text = (string)_elementListBox.SelectedItem;
				_listBoxForm.Hide();
				_idTextBox.Focus();
				_idTextBox.SelectionStart = _idTextBox.TextLength;
			}
		}

		private void _elementListBox_DoubleClick(object sender, EventArgs e)
		{
			// Ausgewähltes Element in die TextBox schreiben
			_idTextBox.Text = (string)_elementListBox.SelectedItem;
			_listBoxForm.Hide();
			_idTextBox.Focus();
			_idTextBox.SelectionStart = _idTextBox.TextLength;
		}

		private void _idTextBox_Leave(object sender, EventArgs e)
		{
			// Wenn auch die ListBox nicht im Fokus ist, diese ggf. schließen
			if(_listBoxForm.Visible && !_elementListBox.Focused)
				_listBoxForm.Hide();
		}

		private void _elementListBox_Leave(object sender, EventArgs e)
		{
			// ListBox-Formular schließen
			_listBoxForm.Hide();
		}

		private void DropDownFieldControl_Load(object sender, EventArgs e)
		{
			// Größe für das Listen-Formular setzen
			_listBoxForm.Size = new Size(_idTextBox.Width, 100);
		}

		private void _idTextBox_TextChanged(object sender, EventArgs e)
		{
			// Falls eingegebener Wert existiert, zugehöriges Element wählen
			if(_elementMap.ContainsKey(_idTextBox.Text))
			{
				// Element wählen
				_value = _elementMap[_idTextBox.Text];
				OnValueChanged(new ValueChangedEventArgs(_value));

				// Farbe setzen
				_idTextBox.BackColor = Color.White;
			}
			else
			{
				// Nichts ausgewählt, Fehlerfarbe
				_idTextBox.BackColor = Color.PaleVioletRed;
			}
		}

		#endregion Eventhandler

		#region Eigenschaften

		/// <summary>
		/// Ruft das ausgewählte Objekt ab oder setzt dieses.
		/// </summary>
		public object Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				_idTextBox.Text = (_value == null ? "" : _elementMap.First(elem => elem.Value == _value).Key);
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

		/// <summary>
		/// Setzt die verwendete Element-Liste. Die Elemente sollten eindeutige Ausgaben der ToString-Methode haben.
		/// </summary>
		public List<object> ElementList
		{
			set
			{
				// ListBox füllen
				if(value != null)
				{
					// Map erstellen
					_elementMap = value.ToDictionary(elem => elem.ToString());
					_elementNames = _elementMap.Keys.ToList();

					// Liste anbinden
					_elementListBox.Items.Clear();
					_elementListBox.Items.AddRange(_elementNames.ToArray());
				}
			}
		}

		#endregion Eigenschaften

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
			private object _newValue;

			/// <summary>
			/// Ruft den neuen Wert ab.
			/// </summary>
			public object NewValue
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
			public ValueChangedEventArgs(object newValue)
			{
				// Parameter speichern
				_newValue = newValue;
			}
		}

		#endregion Event: Geänderte ID

		#endregion Ereignisse
	}
}