using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace TechTreeEditor
{
	/// <summary>
	/// Definiert ein ladbares Plugin.
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// Ruft den Anzeigenamen des Plugins ab.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Übergibt das angegebene geladene Projekt an das Plugin.
		/// </summary>
		/// <param name="projectFile">Das Projekt, das im Hauptprogramm geladen ist und bearbeitet werden soll.</param>
		void AssignProject(TechTreeFile projectFile);

		/// <summary>
		/// Übergibt die Plugin-Kommunikationsschnittstelle an das Plugin.
		/// </summary>
		/// <param name="communicator">Die Plugin-Kommunikationsschnittstelle.</param>
		void AssignCommunicator(MainForm.PluginCommunicator communicator);

		/// <summary>
		/// Ruft das dem Plugin zugehörige Menü ab.
		/// </summary>
		/// <returns></returns>
		List<ToolStripItem> GetPluginMenu();
	}
}
