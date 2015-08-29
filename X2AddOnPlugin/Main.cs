using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTreeEditor;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;

namespace X2AddOnPlugin
{
	/// <summary>
	/// Die Plugin-Hauptklasse.
	/// </summary>
	public class Main : IPlugin
	{
		#region Variablen

		/// <summary>
		/// Das aktuelle Projekt.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Die Plugin-Kommunikationsschnittstelle.
		/// </summary>
		private MainForm.PluginCommunicator _communicator = null;

		/// <summary>
		/// Das Menü-Element für eine neue Einheit.
		/// </summary>
		private ToolStripMenuItem _newCreatableMenuButton;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor. Erstellt die internen Objekte.
		/// </summary>
		public Main()
		{
			// Neue Einheit-Button erstellen
			_newCreatableMenuButton = new ToolStripMenuItem("Neue Einheit erstellen");
			_newCreatableMenuButton.Click += new EventHandler((sender, e) =>
			{
				// Neue Einheit-Fenster öffnen
				NewCreatableForm form = new NewCreatableForm(_projectFile, _communicator.CurrentSelection as TechTreeUnit, _communicator);
				if(form.ShowDialog() == DialogResult.OK)
					_communicator.IssueTreeUpdate();
			});
		}

		#endregion

		#region Plugin-Funktionen

		/// <summary>
		/// Ruft den Plugin-Namen ab.
		/// </summary>
		public string Name
		{
			get { return "X2-AddOn"; }
		}

		/// <summary>
		/// Übergibt das angegebene geladene Projekt an das Plugin.
		/// </summary>
		/// <param name="projectFile">Das Projekt, das im Hauptprogramm geladen ist und bearbeitet werden soll.</param>
		public void AssignProject(TechTreeFile projectFile)
		{
			// Projekt merken
			_projectFile = projectFile;
		}

		/// <summary>
		/// Übergibt die Plugin-Kommunikationsschnittstelle an das Plugin.
		/// </summary>
		/// <param name="communicator">Die Plugin-Kommunikationsschnittstelle.</param>
		public void AssignCommunicator(MainForm.PluginCommunicator communicator)
		{
			// Kommunikator merken
			_communicator = communicator;
        }

		/// <summary>
		/// Ruft das dem Plugin zugehörige Menü ab.
		/// </summary>
		/// <returns></returns>
		public List<ToolStripItem> GetPluginMenu()
		{
			// Menü-Element-Liste zurückgeben
			List<ToolStripItem> result = new List<ToolStripItem>();
			result.Add(_newCreatableMenuButton);
			return result;
		}

		#endregion
	}
}
