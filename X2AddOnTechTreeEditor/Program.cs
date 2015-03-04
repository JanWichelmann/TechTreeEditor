using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace X2AddOnTechTreeEditor
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
			/*CultureInfo currCulture = new System.Globalization.CultureInfo("de-DE");
			System.Threading.Thread.CurrentThread.CurrentCulture = currCulture;
			System.Threading.Thread.CurrentThread.CurrentUICulture = currCulture;*/

			// Anwendung starten
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}