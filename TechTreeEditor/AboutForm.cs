using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace TechTreeEditor
{
	/// <summary>
	/// Das Info-Fenster.
	/// </summary>
	public partial class AboutForm : Form
	{
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
			_versionLabel.Text = string.Format("Version {0}.{1}.{2}", version.Major, version.Minor, version.Build);

			// Danksagung anzeigen
			_creditsTextBox.Rtf = Strings.Credits;
		}

		#endregion Funktionen

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

		#endregion Ereignishandler
	}
}