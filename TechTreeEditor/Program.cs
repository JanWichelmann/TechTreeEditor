using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

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
			/**CultureInfo currCulture = new System.Globalization.CultureInfo("en-US");
			CultureInfo.DefaultThreadCurrentCulture = currCulture;
			CultureInfo.DefaultThreadCurrentUICulture = currCulture;/**/

			// Anwendung starten
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}