using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechTreeEditor;
using TechTreeEditor.Controls;
using TechTreeEditor.TechTreeStructure;
using GenieLibrary;
using IORAMHelper;
using X2AddOnShipTool;

namespace X2AddOnPlugin
{
	/// <summary>
	/// Bietet Funktionen zum schnellen Erstellen einer neuen Einheit aufbauend auf einer vorhandenen.
	/// </summary>
	public partial class NewShipForm : Form
	{
		#region Variablen

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Die Basis-Einheit.
		/// </summary>
		private TechTreeCreatable _baseUnit = null;

		/// <summary>
		/// Die Plugin-Kommunikationsschnittstelle.
		/// </summary>
		private MainForm.PluginCommunicator _communicator = null;

		/// <summary>
		/// Die Kulturen-Liste.
		/// </summary>
		private List<KeyValuePair<uint, string>> _civs;

		/// <summary>
		/// Die Grafiksets der einzelnen Kulturen (Index = Kultur-ID).
		/// </summary>
		private readonly ShipFile.Civ[] _civGraphicSets =
		{
			ShipFile.Civ.OR,
			ShipFile.Civ.WE,
			ShipFile.Civ.WE,
			ShipFile.Civ.ME,
			ShipFile.Civ.ME,
			ShipFile.Civ.AS,
			ShipFile.Civ.AS,
			ShipFile.Civ.OR,
			ShipFile.Civ.OR,
			ShipFile.Civ.OR,
			ShipFile.Civ.OR,
			ShipFile.Civ.ME,
			ShipFile.Civ.AS,
			ShipFile.Civ.WE,
			ShipFile.Civ.WE,
			ShipFile.Civ.IN,
			ShipFile.Civ.IN,
			ShipFile.Civ.ME,
			ShipFile.Civ.AS
		};

		/// <summary>
		/// Die generierten Standing-Grafiken.
		/// </summary>
		private Dictionary<ShipFile.Civ, GenieLibrary.DataElements.Graphic> _graStandings = null;

		/// <summary>
		/// Die generierten Attacking-Grafiken.
		/// </summary>
		private Dictionary<ShipFile.Civ, GenieLibrary.DataElements.Graphic> _graAttackings = null;

		/// <summary>
		/// Die generierten Moving-Grafiken.
		/// </summary>
		private Dictionary<ShipFile.Civ, GenieLibrary.DataElements.Graphic> _graMovings = null;

		/// <summary>
		/// Die aktivierten zu exportierenden Grafiksets.
		/// </summary>
		private List<ShipFile.Civ> _enabledCivSets;

		#endregion

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private NewShipForm()
		{
			// Steuerelement laden
			InitializeComponent();
		}

		/// <summary>
		/// Erstellt ein neues Einheit-Erstellungsfenster.
		/// </summary>
		/// <param name="projectFile">Die zugrundeliegenden Projektdaten.</param>
		/// <param name="baseUnit">Die Einheit, deren Eigenschaften als Vorlage genutzt werden sollen.</param>
		/// <param name="communicator">Die Plugin-Kommunikationsschnittstelle.</param>
		public NewShipForm(TechTreeFile projectFile, TechTreeCreatable baseUnit, MainForm.PluginCommunicator communicator)
			: this()
		{
			// Parameter merken
			_projectFile = projectFile;
			_communicator = communicator;
			_civs = _communicator.Civs;

			// Basiseinheit merken
			_baseUnit = baseUnit;

			// Einheitenliste erstellen
			_baseUnitComboBox.SuspendLayout();
			_baseUnitComboBox.DisplayMember = "Name";
			_projectFile.Where(u => u is TechTreeCreatable).ForEach(u =>
			{
				_baseUnitComboBox.Items.Add(u);
			});
			_baseUnitComboBox.ResumeLayout();

			// Gewählte Basiseinheit vormerken
			_baseUnitComboBox.SelectedItem = _baseUnit;

			// Zeitalter-Liste erstellen
			for(int i = 0; i < _projectFile.AgeCount; ++i)
				_ageComboBox.Items.Add(i);
			_ageComboBox.SelectedItem = _baseUnit.Age;

			// DLL-Felder versorgen
			_dllNameField.ProjectFile = _projectFile;
			_dllDescriptionField.ProjectFile = _projectFile;
			_dllHelpField.ProjectFile = _projectFile;
			_dllHotkeyField.ProjectFile = _projectFile;
			_relIDTextBox.Text = "0";

			// Standardkosten zuweisen
			_cost1Field.Value = _cost2Field.Value = _cost3Field.Value = new IGenieDataElement.ResourceTuple<int, float, byte>()
			{
				Amount = 0,
				Mode = 0,
				Type = -1
			};

			// Kulturenliste zuweisen
			_civListBox.DisplayMember = "Value";
			_civs.ForEach(c =>
			{
				// Gaia ignorieren
				if(c.Key > 0)
					_civListBox.Items.Add(c, false);
			});
		}

		/// <summary>
		/// Generiert die Schiffsgrafiken abhängig vom Breitseitenmodus.
		/// </summary>
		/// <param name="ship">Das Schiff.</param>
		/// <param name="currentGraphicID">Die Basis-DAT-Grafik-ID.</param>
		/// <param name="baseSlpID">Die Basis-SLP-ID.</param>
		/// <param name="broadsideRun">Gibt an, ob gerade die Breitseitengrafiken generiert werden.</param>
		private void GenerateShipGraphics(ShipFile ship, short currentGraphicID, short baseSlpID, bool broadsideRun)
		{
			// Schattengrafiken erstellen
			GenieLibrary.DataElements.Graphic graShadow1 = null;
			GenieLibrary.DataElements.Graphic graShadow2 = null;
			GenieLibrary.DataElements.Graphic graShadow3 = null;
			{
				// Schatten 1
				graShadow1 = new GenieLibrary.DataElements.Graphic();
				graShadow1.AngleCount = 16;
				graShadow1.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
				graShadow1.AttackSoundUsed = 0;
				graShadow1.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
				graShadow1.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[] { });
				graShadow1.FrameCount = 1;
				graShadow1.FrameRate = 0.1f;
				graShadow1.ID = currentGraphicID++;
				graShadow1.Layer = 10;
				graShadow1.MirroringMode = 12;
				graShadow1.Name1 = graShadow1.Name2 = _nameTextBox.Text + "_SSH1\0";
				graShadow1.NewSpeed = 1;
				graShadow1.PlayerColor = 255;
				graShadow1.Rainbow = 255;
				graShadow1.Replay = 1;
				graShadow1.ReplayDelay = 0;
				graShadow1.SequenceType = 2;
				graShadow1.SLP = baseSlpID;
				graShadow1.SoundID = -1;
				graShadow1.Unknown1 = 0;
				graShadow1.Unknown2 = 0;
				graShadow1.Unknown3 = 0;
				_projectFile.BasicGenieFile.Graphics.Add(graShadow1.ID, graShadow1);
				_projectFile.BasicGenieFile.GraphicPointers.Add(1);

				// Schatten 2
				graShadow2 = new GenieLibrary.DataElements.Graphic();
				graShadow2.AngleCount = 16;
				graShadow2.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
				graShadow2.AttackSoundUsed = 0;
				graShadow2.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
				graShadow2.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[] { });
				graShadow2.FrameCount = 1;
				graShadow2.FrameRate = 0.2f;
				graShadow2.ID = currentGraphicID++;
				graShadow2.Layer = 10;
				graShadow2.MirroringMode = 12;
				graShadow2.Name1 = graShadow2.Name2 = _nameTextBox.Text + "_SSH2\0";
				graShadow2.NewSpeed = 0;
				graShadow2.PlayerColor = 255;
				graShadow2.Rainbow = 255;
				graShadow2.Replay = 1;
				graShadow2.ReplayDelay = 1;
				graShadow2.SequenceType = 6;
				graShadow2.SLP = baseSlpID;
				graShadow2.SoundID = -1;
				graShadow2.Unknown1 = 0;
				graShadow2.Unknown2 = 0;
				graShadow2.Unknown3 = 0;
				_projectFile.BasicGenieFile.Graphics.Add(graShadow2.ID, graShadow2);
				_projectFile.BasicGenieFile.GraphicPointers.Add(1);

				// Schatten 3
				graShadow3 = new GenieLibrary.DataElements.Graphic();
				graShadow3.AngleCount = 16;
				graShadow3.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
				graShadow3.AttackSoundUsed = 0;
				graShadow3.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
				graShadow3.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[] { });
				graShadow3.FrameCount = 1;
				graShadow3.FrameRate = 0.1f;
				graShadow3.ID = currentGraphicID++;
				graShadow3.Layer = 10;
				graShadow3.MirroringMode = 12;
				graShadow3.Name1 = graShadow3.Name2 = _nameTextBox.Text + "_SSH3\0";
				graShadow3.NewSpeed = 0;
				graShadow3.PlayerColor = 255;
				graShadow3.Rainbow = 255;
				graShadow3.Replay = 1;
				graShadow3.ReplayDelay = 0;
				graShadow3.SequenceType = 2;
				graShadow3.SLP = baseSlpID;
				graShadow3.SoundID = -1;
				graShadow3.Unknown1 = 0;
				graShadow3.Unknown2 = 0;
				graShadow3.Unknown3 = 0;
				_projectFile.BasicGenieFile.Graphics.Add(graShadow3.ID, graShadow3);
				_projectFile.BasicGenieFile.GraphicPointers.Add(1);
			}

			// Rumpfgrafik erstellen
			GenieLibrary.DataElements.Graphic graBase = null;
			{
				graBase = new GenieLibrary.DataElements.Graphic();
				graBase.AngleCount = 16;
				graBase.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
				graBase.AttackSoundUsed = 0;
				graBase.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
				graBase.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[]
				{
					new GenieLibrary.DataElements.Graphic.GraphicDelta()
					{
						DirectionX = 0,
						DirectionY = 0,
						GraphicID = graShadow1.ID,
						Unknown4 = -1
					}
				});
				graBase.FrameCount = 1;
				graBase.FrameRate = 0.15f;
				graBase.ID = currentGraphicID++;
				graBase.Layer = 20;
				graBase.MirroringMode = 12;
				graBase.Name1 = graBase.Name2 = _nameTextBox.Text + "_SM\0";
				graBase.NewSpeed = 1;
				graBase.PlayerColor = 255;
				graBase.Rainbow = 255;
				graBase.Replay = 1;
				graBase.ReplayDelay = 0;
				graBase.SequenceType = 2;
				graBase.SLP = ++baseSlpID;
				graBase.SoundID = -1;
				graBase.Unknown1 = 0;
				graBase.Unknown2 = 1;
				graBase.Unknown3 = 0;
				_projectFile.BasicGenieFile.Graphics.Add(graBase.ID, graBase);
				_projectFile.BasicGenieFile.GraphicPointers.Add(1);
			}

			// Funktion zur Erstellung von Segelgrafiken
			Dictionary<ShipFile.Civ, Dictionary<Sail.SailType, GenieLibrary.DataElements.Graphic>> graSails = new Dictionary<ShipFile.Civ, Dictionary<Sail.SailType, GenieLibrary.DataElements.Graphic>>();
			Action<Sail.SailType, ShipFile.Civ> createSailGraphic = (type, civ) =>
			{
				// Segelgrafik erstellen
				GenieLibrary.DataElements.Graphic graSail = new GenieLibrary.DataElements.Graphic();
				graSail.AngleCount = 16;
				graSail.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
				graSail.AttackSoundUsed = 0;
				graSail.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
				graSail.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[] { });
				graSail.FrameCount = (ushort)(type == Sail.SailType.MainGo || type == Sail.SailType.MainStop ? 10 : 1);
				graSail.FrameRate = 0.15f;
				graSail.ID = currentGraphicID++;
				graSail.Layer = 20;
				graSail.MirroringMode = 12;
				graSail.Name1 = graSail.Name2 = _nameTextBox.Text + "_" + ShipFile.CivNames[civ] + "_SA" + ((byte)type) + "\0";
				graSail.NewSpeed = 1;
				graSail.PlayerColor = 255;
				graSail.Rainbow = 255;
				graSail.Replay = 1;
				graSail.ReplayDelay = 0;
				graSail.SequenceType = 2;
				graSail.SLP = ++baseSlpID;
				graSail.SoundID = -1;
				graSail.Unknown1 = 0;
				graSail.Unknown2 = 1;
				graSail.Unknown3 = 0;
				_projectFile.BasicGenieFile.Graphics.Add(graSail.ID, graSail);
				_projectFile.BasicGenieFile.GraphicPointers.Add(1);
				graSails[civ].Add(type, graSail);
			};

			// Segel nacheinander explizit erstellen (eine Schleife ist unsicher, da eine konsistente Reihenfolge gewünscht ist)
			Action<ShipFile.Civ> createSailsForCiv = (civ) =>
			{
				// Segellisten-Eintrag anlegen
				graSails.Add(civ, new Dictionary<Sail.SailType, GenieLibrary.DataElements.Graphic>());

				// Haupt Fahren
				if(ship.Sails[Sail.SailType.MainGo].Used)
					createSailGraphic(Sail.SailType.MainGo, civ);

				// Klein 1
				if(ship.Sails[Sail.SailType.Small1].Used)
					if(ship.InvertedSails.Contains(Sail.SailType.Small1))
						createSailGraphic(Sail.SailType.Small2, civ);
					else
						createSailGraphic(Sail.SailType.Small1, civ);

				// Mittel 1
				if(ship.Sails[Sail.SailType.Mid1].Used)
					if(ship.InvertedSails.Contains(Sail.SailType.Mid1))
						createSailGraphic(Sail.SailType.Mid2, civ);
					else
						createSailGraphic(Sail.SailType.Mid1, civ);

				// Groß 1 (getauscht)
				if(ship.Sails[Sail.SailType.Large1].Used)
					if(ship.InvertedSails.Contains(Sail.SailType.Large1))
						createSailGraphic(Sail.SailType.Large1, civ);
					else
						createSailGraphic(Sail.SailType.Large2, civ);

				// Haupt Stehen
				if(ship.Sails[Sail.SailType.MainStop].Used)
					createSailGraphic(Sail.SailType.MainStop, civ);

				// Groß 2 (getauscht)
				if(ship.Sails[Sail.SailType.Large1].Used)
					if(ship.InvertedSails.Contains(Sail.SailType.Large1))
						createSailGraphic(Sail.SailType.Large2, civ);
					else
						createSailGraphic(Sail.SailType.Large1, civ);

				// Mittel 2
				if(ship.Sails[Sail.SailType.Mid1].Used)
					if(ship.InvertedSails.Contains(Sail.SailType.Mid1))
						createSailGraphic(Sail.SailType.Mid1, civ);
					else
						createSailGraphic(Sail.SailType.Mid2, civ);

				// Klein 2
				if(ship.Sails[Sail.SailType.Small1].Used)
					if(ship.InvertedSails.Contains(Sail.SailType.Small1))
						createSailGraphic(Sail.SailType.Small1, civ);
					else
						createSailGraphic(Sail.SailType.Small2, civ);
			};
			foreach(ShipFile.Civ enabledCivSet in _enabledCivSets)
				createSailsForCiv(enabledCivSet);

			// Grafiken nur erstellen, wenn keine Breitseite gewünscht oder gerade Breitseitenmodus aktiv
			if(!_broadsideCheckBox.Checked || broadsideRun)
			{
				// Standing-Grafik
				_graStandings = new Dictionary<ShipFile.Civ, GenieLibrary.DataElements.Graphic>();
				Action<ShipFile.Civ> createStandingForCiv = (civ) =>
				{
					// Standing-Grafik erstellen
					GenieLibrary.DataElements.Graphic graStanding = new GenieLibrary.DataElements.Graphic();
					graStanding.AngleCount = 16;
					graStanding.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
					graStanding.AttackSoundUsed = 0;
					graStanding.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });

					// Deltas erstellen
					graStanding.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[] { });
					graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graBase.ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Small1))
						graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Small1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Mid1))
						graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Mid1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Large1))
						graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Large1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.MainStop))
						graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.MainStop].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Large2))
						graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Large2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Mid2))
						graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Mid2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Small2))
						graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Small2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					graStanding.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graShadow2.ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });

					// Grafik weiter erstellen
					graStanding.FrameCount = 10;
					graStanding.FrameRate = 0.15f;
					graStanding.ID = currentGraphicID++;
					graStanding.Layer = 20;
					graStanding.MirroringMode = 12;
					graStanding.Name1 = graStanding.Name2 = _nameTextBox.Text + "_" + ShipFile.CivNames[civ] + "_S\0";
					graStanding.NewSpeed = 0;
					graStanding.PlayerColor = 255;
					graStanding.Rainbow = 255;
					graStanding.Replay = 1;
					graStanding.ReplayDelay = 50;
					graStanding.SequenceType = 3;
					graStanding.SLP = -1;
					graStanding.SoundID = -1;
					graStanding.Unknown1 = 0;
					graStanding.Unknown2 = 1;
					graStanding.Unknown3 = 0;
					_projectFile.BasicGenieFile.Graphics.Add(graStanding.ID, graStanding);
					_projectFile.BasicGenieFile.GraphicPointers.Add(1);
					_graStandings.Add(civ, graStanding);
				};
				foreach(ShipFile.Civ enabledCivSet in _enabledCivSets)
					createStandingForCiv(enabledCivSet);

				// Attacking-Grafik
				_graAttackings = new Dictionary<ShipFile.Civ, GenieLibrary.DataElements.Graphic>();
				Action<ShipFile.Civ> createAttackingForCiv = (civ) =>
				{
					// Attacking-Grafik erstellen
					GenieLibrary.DataElements.Graphic graAttacking = new GenieLibrary.DataElements.Graphic();
					graAttacking.AngleCount = 16;
					graAttacking.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
					graAttacking.AttackSoundUsed = 0;
					graAttacking.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });

					// Deltas erstellen
					graAttacking.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[] { });
					graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graBase.ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Small1))
						graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Small1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Mid1))
						graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Mid1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Large1))
						graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Large1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.MainGo))
						graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.MainGo].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Large2))
						graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Large2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Mid2))
						graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Mid2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Small2))
						graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Small2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					graAttacking.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graShadow3.ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });

					// Grafik weiter erstellen
					graAttacking.FrameCount = 10;
					graAttacking.FrameRate = 0.15f;
					graAttacking.ID = currentGraphicID++;
					graAttacking.Layer = 20;
					graAttacking.MirroringMode = 12;
					graAttacking.Name1 = graAttacking.Name2 = _nameTextBox.Text + "_" + ShipFile.CivNames[civ] + "_A\0";
					graAttacking.NewSpeed = 1;
					graAttacking.PlayerColor = 255;
					graAttacking.Rainbow = 255;
					graAttacking.Replay = 1;
					graAttacking.ReplayDelay = 0;
					graAttacking.SequenceType = 3;
					graAttacking.SLP = -1;
					graAttacking.SoundID = 428;
					graAttacking.Unknown1 = 0;
					graAttacking.Unknown2 = 1;
					graAttacking.Unknown3 = 0;
					_projectFile.BasicGenieFile.Graphics.Add(graAttacking.ID, graAttacking);
					_projectFile.BasicGenieFile.GraphicPointers.Add(1);
					_graAttackings.Add(civ, graAttacking);
				};
				foreach(ShipFile.Civ enabledCivSet in _enabledCivSets)
					createAttackingForCiv(enabledCivSet);
			}

			// Grafiken nur erstellen, wenn keine Breitseite gewünscht oder gerade kein Breitseitenmodus aktiv
			if(!_broadsideCheckBox.Checked || !broadsideRun)
			{
				// Moving-Grafik
				_graMovings = new Dictionary<ShipFile.Civ, GenieLibrary.DataElements.Graphic>();
				Action<ShipFile.Civ> createMovingForCiv = (civ) =>
				{
					// Moving-Grafik erstellen
					GenieLibrary.DataElements.Graphic graMoving = new GenieLibrary.DataElements.Graphic();
					graMoving.AngleCount = 16;
					graMoving.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
					graMoving.AttackSoundUsed = 0;
					graMoving.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });

					// Deltas erstellen
					graMoving.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[] { });
					graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graBase.ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Small1))
						graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Small1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Mid1))
						graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Mid1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Large1))
						graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Large1].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.MainGo))
						graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.MainGo].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Large2))
						graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Large2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Mid2))
						graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Mid2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					if(graSails[civ].ContainsKey(Sail.SailType.Small2))
						graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graSails[civ][Sail.SailType.Small2].ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });
					graMoving.Deltas.Add(new GenieLibrary.DataElements.Graphic.GraphicDelta() { GraphicID = graShadow3.ID, DirectionX = 0, DirectionY = 0, Unknown4 = -1 });

					// Grafik weiter erstellen
					graMoving.FrameCount = 10;
					graMoving.FrameRate = 0.15f;
					graMoving.ID = currentGraphicID++;
					graMoving.Layer = 20;
					graMoving.MirroringMode = 12;
					graMoving.Name1 = graMoving.Name2 = _nameTextBox.Text + "_" + ShipFile.CivNames[civ] + "_M\0";
					graMoving.NewSpeed = 0;
					graMoving.PlayerColor = 255;
					graMoving.Rainbow = 255;
					graMoving.Replay = 1;
					graMoving.ReplayDelay = 50;
					graMoving.SequenceType = 3;
					graMoving.SLP = -1;
					graMoving.SoundID = -1;
					graMoving.Unknown1 = 0;
					graMoving.Unknown2 = 1;
					graMoving.Unknown3 = 0;
					_projectFile.BasicGenieFile.Graphics.Add(graMoving.ID, graMoving);
					_projectFile.BasicGenieFile.GraphicPointers.Add(1);
					_graMovings.Add(civ, graMoving);
				};
				foreach(ShipFile.Civ enabledCivSet in _enabledCivSets)
					createMovingForCiv(enabledCivSet);
			}

			// Nicht-Breitseiten-Grafiken erstellen
			if(broadsideRun)
				GenerateShipGraphics(ship, currentGraphicID, ++baseSlpID, false);
		}

		#endregion

		#region Ereignishandler

		private void _okButton_Click(object sender, EventArgs e)
		{
			// Aktivierte Grafiksets merken
			_enabledCivSets = new List<ShipFile.Civ>();
			if(_civSetASCheckBox.Checked)
				_enabledCivSets.Add(ShipFile.Civ.AS);
			if(_civSetINCheckBox.Checked)
				_enabledCivSets.Add(ShipFile.Civ.IN);
			if(_civSetMECheckBox.Checked)
				_enabledCivSets.Add(ShipFile.Civ.ME);
			if(_civSetORCheckBox.Checked)
				_enabledCivSets.Add(ShipFile.Civ.OR);
			if(_civSetWECheckBox.Checked)
				_enabledCivSets.Add(ShipFile.Civ.WE);
			if(_enabledCivSets.Count == 0)
			{
				// Fehler
				MessageBox.Show("Bitte mindestens ein Grafikset aktivieren!");
				return;
			}

			// Schiffdatei laden
			ShipFile ship = null;
			try { ship = new ShipFile(_shipFileTextBox.Text); }
			catch { MessageBox.Show("Bitte eine gültige Schiffdatei angeben!"); return; }

			// Leeres Delta
			GenieLibrary.DataElements.Graphic.GraphicDelta emptyDelta = new GenieLibrary.DataElements.Graphic.GraphicDelta()
			{
				GraphicID = -1,
				DirectionX = 0,
				DirectionY = 0,
				Unknown4 = -1
			};

			// Grafiken erstellen
			GenerateShipGraphics(ship, (short)_projectFile.BasicGenieFile.GraphicPointers.Count, (short)_slpBaseId.Value, _broadsideCheckBox.Checked);

			// Neue Einheit erstellen?
			if(!_createOnlyGraphicCheckBox.Checked)
			{
				// Basiseinheit abrufen
				TechTreeCreatable baseUnit = (TechTreeCreatable)_baseUnitComboBox.SelectedItem;

				// Neue Einheit ausgehend von der gewählten Basiseinheit erstellen
				TechTreeCreatable newUnit = (TechTreeCreatable)_projectFile.CreateElement("TechTreeCreatable");

				// Parameter kopieren
				newUnit.StandardElement = baseUnit.StandardElement;
				newUnit.TrackingUnit = baseUnit.TrackingUnit;
				newUnit.DropSite1Unit = baseUnit.DropSite1Unit;
				newUnit.DropSite2Unit = baseUnit.DropSite2Unit;
				newUnit.ProjectileUnit = baseUnit.ProjectileUnit;
				newUnit.ProjectileDuplicationUnit = baseUnit.ProjectileDuplicationUnit;

				// Flags kopieren
				newUnit.Flags = TechTreeElement.ElementFlags.ShowInEditor;

				// Zeitalter setzen
				newUnit.Age = (int)_ageComboBox.SelectedItem;

				// DAT-Einheit auf Basis des ausgewählten Elements erstellen
				GenieLibrary.DataElements.Civ.Unit newUnitDAT = (GenieLibrary.DataElements.Civ.Unit)baseUnit.DATUnit.Clone();
				newUnit.DATUnit = newUnitDAT;

				// DAT-Einheit mit gewählten Parametern ausstatten
				newUnitDAT.Name1 = _nameTextBox.Text + "\0";
				newUnitDAT.DeadUnitID = -1;
				newUnitDAT.IconID = (short)_iconField.Value;
				newUnitDAT.Creatable.TrainTime = (short)_timeField.Value;
				newUnitDAT.LanguageDLLName = (ushort)_dllNameField.Value;
				newUnitDAT.LanguageDLLHotKeyText = _dllNameField.Value + 150000;
				newUnitDAT.LanguageDLLCreation = (ushort)_dllDescriptionField.Value;
				newUnitDAT.LanguageDLLHelp = _dllHelpField.Value + 79000;
				newUnitDAT.HotKey = _dllHotkeyField.Value;
				newUnitDAT.Creatable.ResourceCosts[0] = new IGenieDataElement.ResourceTuple<short, short, short>()
				{
					Mode = _cost1Field.Value.Mode,
					Type = (short)_cost1Field.Value.Type,
					Amount = (short)_cost1Field.Value.Amount
				};
				newUnitDAT.Creatable.ResourceCosts[1] = new IGenieDataElement.ResourceTuple<short, short, short>()
				{
					Mode = _cost2Field.Value.Mode,
					Type = (short)_cost2Field.Value.Type,
					Amount = (short)_cost2Field.Value.Amount
				};
				newUnitDAT.Creatable.ResourceCosts[2] = new IGenieDataElement.ResourceTuple<short, short, short>()
				{
					Mode = _cost3Field.Value.Mode,
					Type = (short)_cost3Field.Value.Type,
					Amount = (short)_cost3Field.Value.Amount
				};

				// Neue ID abrufen und DAT-Einheit auf alle Zivilisationen verteilen
				int newUnitID = _projectFile.BasicGenieFile.UnitHeaders.Count;
				newUnit.ID = newUnitID;
				newUnitDAT.ID1 = newUnitDAT.ID2 = newUnitDAT.ID3 = (short)newUnitID;
				_projectFile.BasicGenieFile.UnitHeaders.Add(new GenieLibrary.DataElements.UnitHeader() { Exists = 1, Commands = new List<GenieLibrary.DataElements.UnitHeader.UnitCommand>() });
				newUnit.Abilities.AddRange(baseUnit.Abilities.Select(ab => ab.Clone()));
				newUnit.Abilities.ForEach(ab => _projectFile.BasicGenieFile.UnitHeaders[newUnitID].Commands.Add(ab.CommandData));
				for(int civId = 0; civId < _projectFile.BasicGenieFile.Civs.Count; ++civId)
				{
					// Civ abrufen
					GenieLibrary.DataElements.Civ civ = _projectFile.BasicGenieFile.Civs[civId];

					// Klonen, da sonst alle Civs in dieselbe DAT-Einheit schreiben
					GenieLibrary.DataElements.Civ.Unit newUnitCopy = (GenieLibrary.DataElements.Civ.Unit)newUnitDAT.Clone();

					// Grafiken zuweisen
					ShipFile.Civ civSet = _civGraphicSets[civId];
					if(!_enabledCivSets.Contains(civSet))
						civSet = _enabledCivSets[0];
					newUnitCopy.Type50.AttackGraphic = _graAttackings[civSet].ID;
					newUnitCopy.DyingGraphic1 = 1900; // WARGA_DN
					newUnitCopy.DeadFish.WalkingGraphic1 = _graMovings[civSet].ID;
					newUnitCopy.StandingGraphic1 = _graStandings[civSet].ID;

					// Einheit speichern
					civ.UnitPointers.Add(newUnitID);
					civ.Units.Add(newUnitID, newUnitCopy);
				}

				// Einheit als Elternelement setzen und an den Anfang stellen
				_projectFile.TechTreeParentElements.Insert(0, newUnit);

				// Kultur-Sperren erstellen: Nicht markiert? => Kultur abrufen und Sperre erstellen
				for(int i = 0; i < _civListBox.Items.Count; ++i)
					if(!_civListBox.GetItemChecked(i))
						_projectFile.CivTrees[(int)((KeyValuePair<uint, string>)_civListBox.Items[i]).Key].BlockedElements.Add(newUnit);

				// Icon und Namen erstellen
				_communicator.CreateElementIconTexture(newUnit);
				newUnit.UpdateName(_projectFile.LanguageFileWrapper);
			}

			// Fenster schließen
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void _cancelButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void _relIDTextBox_TextChanged(object sender, EventArgs e)
		{
			// Wert parsen
			if(int.TryParse(_relIDTextBox.Text, out int relID) && relID >= 0 && relID < 1000)
			{
				// Wert ist gültig
				_relIDTextBox.BackColor = SystemColors.Window;

				// Wert in alle vier Felder übertragen
				_dllNameField.Value = 5000 + relID;
				_dllDescriptionField.Value = 6000 + relID;
				_dllHelpField.Value = 26000 + relID;
				_dllHotkeyField.Value = 16000 + relID;
			}
			else
			{
				// Den User auf die Falscheingabe hinweisen
				_relIDTextBox.BackColor = Color.Red;
			}
		}

		private void _civListBox_Format(object sender, ListControlConvertEventArgs e)
		{
			// Kulturnamen anzeigen
			e.Value = ((KeyValuePair<uint, string>)e.ListItem).Value;
		}

		private void _allCivsButton_Click(object sender, EventArgs e)
		{
			// Alle Kulturen markieren
			for(int i = 0; i < _civListBox.Items.Count; ++i)
				_civListBox.SetItemChecked(i, true);
		}

		private void _noCivsButton_Click(object sender, EventArgs e)
		{
			// Alle Kulturen demarkieren
			for(int i = 0; i < _civListBox.Items.Count; ++i)
				_civListBox.SetItemChecked(i, false);
		}

		private void _openShipFileButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openShipDialog.ShowDialog() == DialogResult.OK)
				_shipFileTextBox.Text = _openShipDialog.FileName;
		}

		#endregion
	}
}
