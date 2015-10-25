using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechTreeEditor
{
	/// <summary>
	/// Das Info-Fenster.
	/// </summary>
	public partial class AboutForm : Form
	{
		#region Variablen

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public AboutForm()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Version anzeigen
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			_versionLabel.Text = string.Format("Version {0}.{1}.{2}", version.Major, version.MajorRevision, version.Minor);

			// Danksagung anzeigen
			_creditsTextBox.Rtf = Strings.Credits;
		}

		#endregion

		#region Ereignishandler

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.Close();
		}

		private void _creditsTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			// Link öffnen
			Process.Start(e.LinkText);
		}

		#endregion
	}
}