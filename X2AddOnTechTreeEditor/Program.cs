﻿using System;
using System.Windows.Forms;
using System.Threading;

namespace X2AddOnTechTreeEditor
{
	internal static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			// Anwendung starten
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}