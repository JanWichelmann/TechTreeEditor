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
			// Sprache ändern (für Debugging)
#if LANG_EN
			CultureInfo currCulture = new System.Globalization.CultureInfo("en-US");
			CultureInfo.DefaultThreadCurrentCulture = currCulture;
			CultureInfo.DefaultThreadCurrentUICulture = currCulture;
#endif

			// Anwendung starten
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}