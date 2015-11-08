using System;
using System.Globalization;
using System.Windows.Forms;

namespace TechTreeEditor
{
	public static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			// Sprache einstellen
			CultureInfo currCulture = new CultureInfo(Properties.Settings.Default.Language);
			CultureInfo.DefaultThreadCurrentUICulture = currCulture;

			// Anwendung starten
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}