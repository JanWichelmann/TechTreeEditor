using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTreeEditor;
using System.Windows.Forms;
using TechTreeEditor.TechTreeStructure;
using DRSLibrary;

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
		/// Die Menü-Elemente.
		/// </summary>
		private List<ToolStripMenuItem> _menuButtons = new List<ToolStripMenuItem>();

		/// <summary>
		/// Ruft die geladene Graphics-DRS-Datei ab.
		/// </summary>
		internal static DRSFile GraphicsDRS
		{
			get;
			private set;
		}

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor. Erstellt die internen Objekte.
		/// </summary>
		public Main()
		{
			// Neue Einheit-Button erstellen
			var newCreatableMenuButton = new ToolStripMenuItem("Neue Einheit erstellen");
			newCreatableMenuButton.Click += new EventHandler((sender, e) =>
			{
				// Es muss was ausgewählt sein
				if(_communicator.CurrentSelection == null as TechTreeUnit)
					MessageBox.Show("Bitte eine Basiseinheit markieren!");
				else
				{
					// Neue Einheit-Fenster öffnen
					NewCreatableForm form = new NewCreatableForm(_projectFile, (TechTreeUnit)_communicator.CurrentSelection, _communicator);
					if(form.ShowDialog() == DialogResult.OK)
						_communicator.IssueTreeUpdate();
				}
			});
			_menuButtons.Add(newCreatableMenuButton);

			// Neues Schiff-Button erstellen
			var newShipMenuButton = new ToolStripMenuItem("Neues Schiff erstellen");
			newShipMenuButton.Click += new EventHandler((sender, e) =>
			{
				// Es muss was ausgewählt sein
				if(_communicator.CurrentSelection as TechTreeCreatable == null)
					MessageBox.Show("Bitte ein gültiges Basisschiff markieren!");
				else
				{
					// Neues Schiff-Fenster öffnen
					NewShipForm form = new NewShipForm(_projectFile, (TechTreeCreatable)_communicator.CurrentSelection, _communicator);
					if(form.ShowDialog() == DialogResult.OK)
						_communicator.IssueTreeUpdate();
				}
			});
			_menuButtons.Add(newShipMenuButton);

			// Standardelement-Hierarchie-Button erstellen
			var standardElementHierarchyButton = new ToolStripMenuItem("Standardelement-Vererbung reparieren");
			standardElementHierarchyButton.Click += new EventHandler((sender, e) =>
			{
				// Eigenschaft aus Elternelementen rekursiv vererben
				void recSetStandardElement(TechTreeUnit u, bool s)
				{
					if(u is TechTreeCreatable c)
						c.StandardElement = s;
					else if(u is TechTreeBuilding b)
						b.StandardElement = s;
					foreach(var child in u.GetChildren())
						if(!_projectFile.TechTreeParentElements.Contains(child) && child is TechTreeUnit childU)
							recSetStandardElement(childU, s);
				};
				foreach(var elem in _projectFile.TechTreeParentElements.Where(p => p is TechTreeBuilding || p is TechTreeCreatable))
					if(elem is TechTreeBuilding b)
						recSetStandardElement(b, b.StandardElement);
					else if(elem is TechTreeCreatable c)
						recSetStandardElement(c, c.StandardElement);
				_communicator.IssueTreeUpdate();
				MessageBox.Show("Vererbung erfolgreich.");
			});
			_menuButtons.Add(standardElementHierarchyButton);

			// Kosten-Aufräum-Button erstellen
			var cleanCostsButton = new ToolStripMenuItem("Einheiten/Technologie-Kosten aufräumen");
			cleanCostsButton.Click += new EventHandler((sender, e) =>
			{
				// Einheiten
				foreach(TechTreeUnit u in _projectFile.Where(el => el is TechTreeCreatable || el is TechTreeBuilding))
					foreach(var c in _projectFile.BasicGenieFile.Civs)
						if(c.Units.ContainsKey(u.ID))
							for(int i = 0; i < 3; ++i)
								if(c.Units[u.ID].Creatable.ResourceCosts[i].Amount == 0)
									c.Units[u.ID].Creatable.ResourceCosts[i] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, short>()
									{
										Amount = 0,
										Mode = 0,
										Type = -1
									};

				// Technologien
				foreach(TechTreeResearch r in _projectFile.Where(el => el is TechTreeResearch))
					for(int i = 0; i < 3; ++i)
						if(r.DATResearch.ResourceCosts[i].Amount == 0)
							r.DATResearch.ResourceCosts[i] = new GenieLibrary.IGenieDataElement.ResourceTuple<short, short, byte>()
							{
								Amount = 0,
								Mode = 0,
								Type = -1
							};

				// Baum aktualisieren
				_communicator.IssueTreeUpdate();
				MessageBox.Show("Aufräumen erfolgreich.");
			});
			_menuButtons.Add(cleanCostsButton);
		}

		#endregion

		#region Plugin-Funktionen

		/// <summary>
		/// Ruft den Plugin-Namen ab.
		/// </summary>
		public string Name
		{
			get { return "Agearena AddOn"; }
		}

		/// <summary>
		/// Übergibt das angegebene geladene Projekt an das Plugin.
		/// </summary>
		/// <param name="projectFile">Das Projekt, das im Hauptprogramm geladen ist und bearbeitet werden soll.</param>
		public void AssignProject(TechTreeFile projectFile)
		{
			// Projekt merken
			_projectFile = projectFile;

			// Asynchron im Hintergrund Graphics-DRS laden
			(new Task(() =>
			{
	// Graphics-DRS laden
	try
				{
		// Datei laden, falls in Projektverzeichnis auffindbar
		string graphicsDRSPath = Path.GetDirectoryName(_projectFile.InterfacDRSPath) + "\\graphics.drs";
					if(File.Exists(graphicsDRSPath))
						GraphicsDRS = new DRSFile(graphicsDRSPath);
					else
						GraphicsDRS = null;
				}
				catch
				{
		// Egal, Pech gehabt
		GraphicsDRS = null;
				}

			})).Start();
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
			return _menuButtons.Cast<ToolStripItem>().ToList();
		}

		#endregion
	}
}
