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
			// Spracheinstellungen in den Programmparametern abrufen
			string[] args = Environment.GetCommandLineArgs();
			if(args.Length >= 2)
				if(args[1] == "/de")
				{
					// Deutsche Sprache einstellen
					CultureInfo currCulture = new CultureInfo("de-DE");
					CultureInfo.DefaultThreadCurrentUICulture = currCulture;
				}
				else if(args[1] == "/en")
				{
					// Englische Sprache einstellen
					CultureInfo currCulture = new CultureInfo("en-US");
					CultureInfo.DefaultThreadCurrentUICulture = currCulture;
				}
				else { } // Standarsprache benutzen

			// Anwendung starten
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}