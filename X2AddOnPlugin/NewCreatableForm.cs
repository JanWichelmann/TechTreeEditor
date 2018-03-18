using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechTreeEditor;
using TechTreeEditor.Controls;
using TechTreeEditor.TechTreeStructure;
using GenieLibrary;
using IORAMHelper;

namespace X2AddOnPlugin
{
    /// <summary>
    /// Bietet Funktionen zum schnellen Erstellen einer neuen Einheit aufbauend auf einer vorhandenen.
    /// </summary>
    public partial class NewCreatableForm : Form
    {
        #region Variablen

        /// <summary>
        /// Die zugrundeliegenden Projektdaten.
        /// </summary>
        private TechTreeFile _projectFile = null;

        /// <summary>
        /// Die Basis-Einheit.
        /// </summary>
        private TechTreeUnit _baseUnit = null;

        /// <summary>
        /// Die Plugin-Kommunikationsschnittstelle.
        /// </summary>
        private MainForm.PluginCommunicator _communicator = null;

        /// <summary>
        /// Die Kulturen-Liste.
        /// </summary>
        private List<KeyValuePair<uint, string>> _civs;

        /// <summary>
        /// Die Angriffssound-Typen.
        /// </summary>
        private static List<KeyValuePair<int, string>> _attackSoundTypes = null;

        /// <summary>
        /// Die Sterbesound-Typen.
        /// </summary>
        private static List<KeyValuePair<int, string>> _fallingSoundTypes = null;

        #endregion

        #region Funktionen

        /// <summary>
        /// Konstruktor.
        /// </summary>
        private NewCreatableForm()
        {
            // Steuerelement laden
            InitializeComponent();

            // Ggf. Sound-Listen erstellen
            if(_attackSoundTypes == null)
                _attackSoundTypes = new List<KeyValuePair<int, string>>(new KeyValuePair<int, string>[] {
                    new KeyValuePair<int, string>(0,"Schwertträger"),
                    new KeyValuePair<int, string>(1,"Bogenschütze"),
                    new KeyValuePair<int, string>(2,"Handkanone"),
                    new KeyValuePair<int, string>(3,"Schwere Kanone"),
                    new KeyValuePair<int, string>(4,"Bolzenschütze")
                });
            if(_fallingSoundTypes == null)
                _fallingSoundTypes = new List<KeyValuePair<int, string>>(new KeyValuePair<int, string>[] {
                    new KeyValuePair<int, string>(0,"Fußsoldat"),
                    new KeyValuePair<int, string>(1,"Kavallerie"),
                    new KeyValuePair<int, string>(2,"Gerät"),
                    new KeyValuePair<int, string>(3,"Kamelreiter"),
                    new KeyValuePair<int, string>(4,"Wagen")
                });
        }

        /// <summary>
        /// Erstellt ein neues Einheit-Erstellungsfenster.
        /// </summary>
        /// <param name="projectFile">Die zugrundeliegenden Projektdaten.</param>
        /// <param name="baseUnit">Die Einheit, der die neue untergeordnet und deren Eigenschaften ggf. als Vorlage genutzt werden sollen.</param>
        /// <param name="communicator">Die Plugin-Kommunikationsschnittstelle.</param>
        public NewCreatableForm(TechTreeFile projectFile, TechTreeUnit baseUnit, MainForm.PluginCommunicator communicator)
            : this()
        {
            // Parameter merken
            _projectFile = projectFile;
            _communicator = communicator;
            _civs = _communicator.Civs;

            // Basiseinheit prüfen und ggf. merken
            if(baseUnit is TechTreeBuilding)
                _baseUnit = baseUnit;
            else if(baseUnit is TechTreeCreatable)
            {
                // Merken und Button-Feld sperren
                _baseUnit = baseUnit;
                _buttonField.Enabled = false;
            }

            // Einheitenliste erstellen
            _baseUnitComboBox.SuspendLayout();
            _baseUnitComboBox.DisplayMember = "Name";
            _projectFile.Where(u => u is TechTreeCreatable).ForEach(u =>
            {
                _baseUnitComboBox.Items.Add(u);
            });
            _baseUnitComboBox.ResumeLayout();

            // Ggf. gewählte Basiseinheit vormerken
            if(_baseUnit is TechTreeCreatable)
                _baseUnitComboBox.SelectedItem = _baseUnit;
            else
                _baseUnitComboBox.SelectedIndex = 0;

            // Zeitalter-Liste abhängig von Basiseinheit erstellen
            for(int i = _baseUnit.Age; i < 5; ++i)
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

            // Angriffssoundliste setzen
            _soundTypeComboBox.DisplayMember = "Value";
            _attackSoundTypes.ForEach(s => _soundTypeComboBox.Items.Add(s));
            _soundTypeComboBox.SelectedIndex = 0;

            // Sterbesoundliste setzen
            _fallingSoundTypeComboBox.DisplayMember = "Value";
            _fallingSoundTypes.ForEach(s => _fallingSoundTypeComboBox.Items.Add(s));
            _fallingSoundTypeComboBox.SelectedIndex = 0;

            // TODO Nur temporär für Helden
            _createGraphicsCheckBox.Checked = false;
            _heroCheckBox.Checked = true;
            _iconField.Value = (_baseUnit?.DATUnit?.IconID ?? 0);
        }

        /// <summary>
        /// Erstellt die Angriffssounds für die gegebene Grafik.
        /// </summary>
        /// <param name="graphic">Die Grafik, für die Angriffssounds erstellt werden sollen.</param>
        private void CreateAttackSounds(GenieLibrary.DataElements.Graphic graphic)
        {
            // Sound erstellen, nach Sound-Typ unterscheiden
            GenieLibrary.DataElements.Graphic.GraphicAttackSound sound = new GenieLibrary.DataElements.Graphic.GraphicAttackSound();
            switch(((KeyValuePair<int, string>)_soundTypeComboBox.SelectedItem).Key)
            {
                // Schwertträger
                case 0:
                    sound.SoundID1 = 329;
                    sound.SoundDelay1 = 7;
                    sound.SoundID2 = -1;
                    sound.SoundDelay2 = -1;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;

                // Bogenschütze
                case 1:
                    sound.SoundID1 = 312;
                    sound.SoundDelay1 = 5;
                    sound.SoundID2 = 314;
                    sound.SoundDelay2 = 7;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;

                // Handkanone
                case 2:
                    sound.SoundID1 = 385;
                    sound.SoundDelay1 = 7;
                    sound.SoundID2 = -1;
                    sound.SoundDelay2 = -1;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;

                // Schwere Kanone
                case 3:
                    sound.SoundID1 = 411;
                    sound.SoundDelay1 = 7;
                    sound.SoundID2 = -1;
                    sound.SoundDelay2 = -1;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;

                // Bolzenschütze
                case 4:
                    sound.SoundID1 = 384;
                    sound.SoundDelay1 = 6;
                    sound.SoundID2 = -1;
                    sound.SoundDelay2 = -1;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;
            }

            // Alle Achsen bekommen denselben Sound
            graphic.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
            for(int i = 0; i < graphic.AngleCount; ++i)
                graphic.AttackSounds.Add((GenieLibrary.DataElements.Graphic.GraphicAttackSound)sound.Clone());
        }

        /// <summary>
        /// Erstellt den Sterbesound für die gegebene Grafik.
        /// </summary>
        /// <param name="graphic">Die Grafik, für die der Sterbesound erstellt werden sollen.</param>
        private void CreateFallingSounds(GenieLibrary.DataElements.Graphic graphic)
        {
            // Sound erstellen, nach Sound-Typ unterscheiden
            GenieLibrary.DataElements.Graphic.GraphicAttackSound sound = null;
            switch(((KeyValuePair<int, string>)_fallingSoundTypeComboBox.SelectedItem).Key)
            {
                // Fußsoldat
                case 0:
                    graphic.SoundID = 294;
                    break;

                // Kavallerie
                case 1:
                    sound = new GenieLibrary.DataElements.Graphic.GraphicAttackSound();
                    sound.SoundID1 = 410;
                    sound.SoundDelay1 = 1;
                    sound.SoundID2 = 409;
                    sound.SoundDelay2 = 6;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;

                // Gerät
                case 2:
                    graphic.SoundID = 293;
                    break;

                // Kamelreiter
                case 3:
                    sound = new GenieLibrary.DataElements.Graphic.GraphicAttackSound();
                    sound.SoundID1 = 431;
                    sound.SoundDelay1 = 1;
                    sound.SoundID2 = 409;
                    sound.SoundDelay2 = 6;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;

                // Wagen
                case 4:
                    sound = new GenieLibrary.DataElements.Graphic.GraphicAttackSound();
                    sound.SoundID1 = 410;
                    sound.SoundDelay1 = 1;
                    sound.SoundID2 = 315;
                    sound.SoundDelay2 = 8;
                    sound.SoundID3 = -1;
                    sound.SoundDelay3 = -1;
                    break;
            }

            // Sounds gesetzt?
            if(sound != null)
            {
                // Alle Achsen bekommen denselben Sound
                graphic.AttackSoundUsed = 1;
                graphic.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
                for(int i = 0; i < graphic.AngleCount; ++i)
                    graphic.AttackSounds.Add((GenieLibrary.DataElements.Graphic.GraphicAttackSound)sound.Clone());
            }
        }

        #endregion

        #region Ereignishandler

        private void _okButton_Click(object sender, EventArgs e)
        {
            // Grafiken erstellen
            short graBaseID = (short)_projectFile.BasicGenieFile.GraphicPointers.Count;
            if(_createGraphicsCheckBox.Checked)
            {
                // Leeres Delta
                GenieLibrary.DataElements.Graphic.GraphicDelta emptyDelta = new GenieLibrary.DataElements.Graphic.GraphicDelta()
                {
                    GraphicID = -1,
                    DirectionX = 0,
                    DirectionY = 0,
                    Unknown4 = -1
                };

                // A
                GenieLibrary.DataElements.Graphic graA = new GenieLibrary.DataElements.Graphic();
                graA.AngleCount = 8;
                graA.AttackSoundUsed = 1;
                graA.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
                graA.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[]
                {
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone()
                });
                graA.FrameCount = (ushort)(_slpAFrameField.Value / 5);
                graA.FrameRate = 0.1f;
                graA.ID = graBaseID;
                graA.Layer = 20;
                graA.MirroringMode = 6;
                graA.Name1 = graA.Name2 = _nameTextBox.Text + "_A\0";
                graA.NewSpeed = 0;
                graA.PlayerColor = 255;
                graA.Rainbow = 255;
                graA.Replay = 1;
                graA.ReplayDelay = 0;
                graA.SequenceType = 3;
                graA.SLP = (int)_slpAIDField.Value;
                graA.SoundID = -1;
                graA.Unknown1 = 0;
                graA.Unknown2 = 1;
                graA.Unknown3 = 0;
                CreateAttackSounds(graA);
                _projectFile.BasicGenieFile.Graphics.Add(graA.ID, graA);
                _projectFile.BasicGenieFile.GraphicPointers.Add(1);

                // D
                GenieLibrary.DataElements.Graphic graD = new GenieLibrary.DataElements.Graphic();
                graD.AngleCount = 8;
                graD.AttackSoundUsed = 0;
                graD.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
                graD.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
                graD.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>();
                graD.FrameCount = (ushort)(_slpDFrameField.Value / 5);
                graD.FrameRate = 6.0f;
                graD.ID = (short)(graBaseID + 1);
                graD.Layer = 10;
                graD.MirroringMode = 6;
                graD.Name1 = graD.Name2 = _nameTextBox.Text + "_D\0";
                graD.NewSpeed = 0;
                graD.PlayerColor = 255;
                graD.Rainbow = 255;
                graD.Replay = 1;
                graD.ReplayDelay = 0;
                graD.SequenceType = 11;
                graD.SLP = (int)_slpDIDField.Value;
                graD.SoundID = -1;
                graD.Unknown1 = 0;
                graD.Unknown2 = 1;
                graD.Unknown3 = 0;
                _projectFile.BasicGenieFile.Graphics.Add(graD.ID, graD);
                _projectFile.BasicGenieFile.GraphicPointers.Add(1);

                // F
                GenieLibrary.DataElements.Graphic graF = new GenieLibrary.DataElements.Graphic();
                graF.AngleCount = 8;
                graF.AttackSoundUsed = 0;
                graF.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
                graF.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
                graF.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[]
                {
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone()
                });
                graF.FrameCount = (ushort)(_slpFFrameField.Value / 5);
                graF.FrameRate = 0.1f;
                graF.ID = (short)(graBaseID + 2);
                graF.Layer = 20;
                graF.MirroringMode = 6;
                graF.Name1 = graF.Name2 = _nameTextBox.Text + "_F\0";
                graF.NewSpeed = 0;
                graF.PlayerColor = 255;
                graF.Rainbow = 255;
                graF.Replay = 1;
                graF.ReplayDelay = 0;
                graF.SequenceType = 11;
                graF.SLP = (int)_slpFIDField.Value;
                graF.SoundID = -1;
                graF.Unknown1 = 0;
                graF.Unknown2 = 1;
                graF.Unknown3 = 0;
                CreateFallingSounds(graF);
                _projectFile.BasicGenieFile.Graphics.Add(graF.ID, graF);
                _projectFile.BasicGenieFile.GraphicPointers.Add(1);

                // M
                GenieLibrary.DataElements.Graphic graM = new GenieLibrary.DataElements.Graphic();
                graM.AngleCount = 8;
                graM.AttackSoundUsed = 0;
                graM.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
                graM.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
                graM.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[]
                {
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone()
                });
                graM.FrameCount = (ushort)(_slpMFrameField.Value / 5);
                graM.FrameRate = 0.07f;
                graM.ID = (short)(graBaseID + 3);
                graM.Layer = 20;
                graM.MirroringMode = 6;
                graM.Name1 = graM.Name2 = _nameTextBox.Text + "_M\0";
                graM.NewSpeed = 1;
                graM.PlayerColor = 255;
                graM.Rainbow = 255;
                graM.Replay = 1;
                graM.ReplayDelay = 0;
                graM.SequenceType = 3;
                graM.SLP = (int)_slpMIDField.Value;
                graM.SoundID = -1;
                graM.Unknown1 = 0;
                graM.Unknown2 = 1;
                graM.Unknown3 = 0;
                _projectFile.BasicGenieFile.Graphics.Add(graM.ID, graM);
                _projectFile.BasicGenieFile.GraphicPointers.Add(1);

                // S
                GenieLibrary.DataElements.Graphic graS = new GenieLibrary.DataElements.Graphic();
                graS.AngleCount = 8;
                graS.AttackSoundUsed = 0;
                graS.AttackSounds = new List<GenieLibrary.DataElements.Graphic.GraphicAttackSound>();
                graS.Coordinates = new List<short>(new short[] { 0, 0, 0, 0 });
                graS.Deltas = new List<GenieLibrary.DataElements.Graphic.GraphicDelta>(new GenieLibrary.DataElements.Graphic.GraphicDelta[]
                {
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone(),
                    (GenieLibrary.DataElements.Graphic.GraphicDelta)emptyDelta.Clone()
                });
                graS.FrameCount = (ushort)(_slpSFrameField.Value / 5);
                graS.FrameRate = 0.3f;
                graS.ID = (short)(graBaseID + 4);
                graS.Layer = 20;
                graS.MirroringMode = 6;
                graS.Name1 = graS.Name2 = _nameTextBox.Text + "_S\0";
                graS.NewSpeed = 0;
                graS.PlayerColor = 255;
                graS.Rainbow = 255;
                graS.Replay = 1;
                graS.ReplayDelay = 1;
                graS.SequenceType = 7;
                graS.SLP = (int)_slpSIDField.Value;
                graS.SoundID = -1;
                graS.Unknown1 = 0;
                graS.Unknown2 = 1;
                graS.Unknown3 = 0;
                _projectFile.BasicGenieFile.Graphics.Add(graS.ID, graS);
                _projectFile.BasicGenieFile.GraphicPointers.Add(1);
            }

            // Basiseinheit abrufen
            TechTreeCreatable baseUnit = (TechTreeCreatable)_baseUnitComboBox.SelectedItem;

            // Tote Einheit erstellen
            TechTreeDead deadUnit = null;
            if(baseUnit.DeadUnit != null && _createGraphicsCheckBox.Checked)
            {
                // Tote Einheit erstellen
                deadUnit = (TechTreeDead)_projectFile.CreateElement("TechTreeDead");
                deadUnit.Age = 0;
                deadUnit.Flags = TechTreeElement.ElementFlags.None;

                // DAT-Einheit erstellen
                GenieLibrary.DataElements.Civ.Unit deadUnitDAT = (GenieLibrary.DataElements.Civ.Unit)baseUnit.DeadUnit.DATUnit.Clone();
                deadUnit.DATUnit = deadUnitDAT;
                deadUnitDAT.Name1 = _nameTextBox.Text + "_D\0";
                deadUnitDAT.StandingGraphic1 = (short)(graBaseID + 1);

                // Neue ID abrufen und DAT-Einheit auf alle Zivilisationen verteilen
                int deadUnitID = _projectFile.BasicGenieFile.UnitHeaders.Count;
                deadUnit.ID = deadUnitID;
                deadUnitDAT.ID1 = deadUnitDAT.ID2 = deadUnitDAT.ID3 = (short)deadUnitID;
                _projectFile.BasicGenieFile.UnitHeaders.Add(new GenieLibrary.DataElements.UnitHeader() { Exists = 1, Commands = new List<GenieLibrary.DataElements.UnitHeader.UnitCommand>(_projectFile.BasicGenieFile.UnitHeaders[baseUnit.DeadUnit.ID].Commands) });
                foreach(var civ in _projectFile.BasicGenieFile.Civs)
                {
                    civ.UnitPointers.Add(deadUnitID);
                    civ.Units.Add(deadUnitID, (GenieLibrary.DataElements.Civ.Unit)deadUnitDAT.Clone()); // Klonen, da sonst alle Civs in dieselbe DAT-Einheit schreiben
                }

                // Icon und Namen erstellen
                _communicator.CreateElementIconTexture(deadUnit);
                deadUnit.UpdateName(_projectFile.LanguageFileWrapper);

                // Einheit als Elternelement setzen und an den Anfang stellen
                _projectFile.TechTreeParentElements.Insert(0, deadUnit);
            }
            else if(baseUnit.DeadUnit != null && baseUnit.DeadUnit is TechTreeDead)
                deadUnit = (TechTreeDead)baseUnit.DeadUnit;

            // Neue Einheit ausgehend von der gewählten Basiseinheit erstellen
            TechTreeCreatable newUnit = (TechTreeCreatable)_projectFile.CreateElement("TechTreeCreatable");

            // Parameter kopieren
            newUnit.DeadUnit = deadUnit;
            newUnit.StandardElement = _heroCheckBox.Checked ? false : baseUnit.StandardElement;
            newUnit.TrackingUnit = baseUnit.TrackingUnit;
            newUnit.DropSite1Unit = baseUnit.DropSite1Unit;
            newUnit.DropSite2Unit = baseUnit.DropSite2Unit;
            newUnit.ProjectileUnit = baseUnit.ProjectileUnit;
            newUnit.ProjectileDuplicationUnit = baseUnit.ProjectileDuplicationUnit;

            // Flags kopieren
            newUnit.Flags = baseUnit.Flags & ~TechTreeElement.ElementFlags.LockID & ~TechTreeElement.ElementFlags.RenderingFlags;

            // Zeitalter setzen
            if(_heroCheckBox.Checked)
                newUnit.Age = 0;
            else
                newUnit.Age = (int)_ageComboBox.SelectedItem;

            // DAT-Einheit auf Basis des ausgewählten Elements erstellen
            GenieLibrary.DataElements.Civ.Unit newUnitDAT = (GenieLibrary.DataElements.Civ.Unit)baseUnit.DATUnit.Clone();
            newUnit.DATUnit = newUnitDAT;

            // DAT-Einheit mit gewählten Parametern ausstatten
            newUnitDAT.Name1 = _nameTextBox.Text + "\0";
            newUnitDAT.DeadUnitID = (short)(deadUnit?.ID ?? 0);
            newUnitDAT.IconID = (short)_iconField.Value;
            newUnitDAT.Creatable.TrainTime = (short)_timeField.Value;
            newUnitDAT.LanguageDLLName = (ushort)_dllNameField.Value;
            newUnitDAT.LanguageDLLHotKeyText = _dllNameField.Value + 150000;
            newUnitDAT.LanguageDLLCreation = (ushort)_dllDescriptionField.Value;
            newUnitDAT.LanguageDLLHelp = _dllHelpField.Value + 79000;
            newUnitDAT.HotKey = _dllHotkeyField.Value;
            newUnitDAT.Creatable.HeroMode = _heroCheckBox.Checked ? (byte)1 : (byte)0;
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

            // Grafiken zuweisen
            if(_createGraphicsCheckBox.Checked)
            {
                newUnitDAT.Type50.AttackGraphic = graBaseID;
                newUnitDAT.DyingGraphic1 = (short)(graBaseID + 2);
                newUnitDAT.DeadFish.WalkingGraphic1 = (short)(graBaseID + 3);
                newUnitDAT.StandingGraphic1 = (short)(graBaseID + 4);
            }

            // Einheiten-Fähigkeiten kopieren
            int abID = 0;
            foreach(UnitAbility ab in baseUnit.Abilities)
            {
                // Fähigkeit kopieren und ID ändern
                UnitAbility newAb = ab.Clone();
                ab.CommandData.ID = (short)abID++;
                newUnit.Abilities.Add(newAb);
            }

            // Neue ID abrufen und DAT-Einheit auf alle Zivilisationen verteilen
            int newUnitID = _projectFile.BasicGenieFile.UnitHeaders.Count;
            newUnit.ID = newUnitID;
            newUnitDAT.ID1 = newUnitDAT.ID2 = newUnitDAT.ID3 = (short)newUnitID;
            _projectFile.BasicGenieFile.UnitHeaders.Add(new GenieLibrary.DataElements.UnitHeader() { Exists = 1, Commands = newUnit.Abilities.Select(ab => ab.CommandData).ToList() });
            foreach(var civ in _projectFile.BasicGenieFile.Civs)
            {
                civ.UnitPointers.Add(newUnitID);
                civ.Units.Add(newUnitID, (GenieLibrary.DataElements.Civ.Unit)newUnitDAT.Clone()); // Klonen, da sonst alle Civs in dieselbe DAT-Einheit schreiben
            }

            // Einheit der Obereinheit unterordnen oder als Elternelement setzen und an den Anfang stellen
            if(_baseUnit == null || _heroCheckBox.Checked)
                _projectFile.TechTreeParentElements.Insert(0, newUnit);
            else
            {
                // Nach Typ unterscheiden
                if(_baseUnit is TechTreeBuilding)
                    ((TechTreeBuilding)_baseUnit).Children.Add(new Tuple<byte, TechTreeElement>((byte)_buttonField.Value, newUnit));
                else if(_baseUnit is TechTreeCreatable && ((TechTreeCreatable)_baseUnit).Successor == null)
                    ((TechTreeCreatable)_baseUnit).Successor = newUnit;
                else
                    _projectFile.TechTreeParentElements.Insert(0, newUnit);
            }

            // Kultur-Sperren erstellen: Nicht markiert? => Kultur abrufen und Sperre erstellen
            if(!_heroCheckBox.Checked)
                for(int i = 0; i < _civListBox.Items.Count; ++i)
                    if(!_civListBox.GetItemChecked(i))
                        _projectFile.CivTrees[(int)((KeyValuePair<uint, string>)_civListBox.Items[i]).Key].BlockedElements.Add(newUnit);

            // Icon und Namen erstellen
            _communicator.CreateElementIconTexture(newUnit);
            newUnit.UpdateName(_projectFile.LanguageFileWrapper);

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

        private void _slpAIDField_ValueChanged(object sender, NumberFieldControl.ValueChangedEventArgs e)
        {
            // Fehler abfangen
            try
            {
                // Ist eine Graphics-DRS geladen? => SLPs versuchen zu laden und Frameraten auslesen
                if(Main.GraphicsDRS != null)
                {
                    // A
                    ushort baseID = (ushort)e.NewValue;
                    SLPLoader.SLPFile slp = new SLPLoader.SLPFile(new RAMBuffer(Main.GraphicsDRS.GetResourceData(baseID)));
                    _slpAFrameField.Value = slp.FrameCount;

                    // D
                    slp = new SLPLoader.SLPFile(new RAMBuffer(Main.GraphicsDRS.GetResourceData(++baseID)));
                    _slpDIDField.Value = baseID;
                    _slpDFrameField.Value = slp.FrameCount;

                    // F
                    slp = new SLPLoader.SLPFile(new RAMBuffer(Main.GraphicsDRS.GetResourceData(++baseID)));
                    _slpFIDField.Value = baseID;
                    _slpFFrameField.Value = slp.FrameCount;

                    // M
                    slp = new SLPLoader.SLPFile(new RAMBuffer(Main.GraphicsDRS.GetResourceData(++baseID)));
                    _slpMIDField.Value = baseID;
                    _slpMFrameField.Value = slp.FrameCount;

                    // S
                    slp = new SLPLoader.SLPFile(new RAMBuffer(Main.GraphicsDRS.GetResourceData(++baseID)));
                    _slpSIDField.Value = baseID;
                    _slpSFrameField.Value = slp.FrameCount;
                }
            }
            catch
            {
                // Schade, aber gut, seis drum
            }
        }

        private void _createGraphicsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Grafik-Felder (de-)aktivieren
            _graphicsGroupBox.Enabled = _createGraphicsCheckBox.Checked;
        }

        private void _heroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Einige Felder (de-)aktivieren
            _buttonField.Enabled = !_heroCheckBox.Checked;
            _ageComboBox.Enabled = !_heroCheckBox.Checked;
            _timeField.Enabled = !_heroCheckBox.Checked;
            _civListBox.Enabled = !_heroCheckBox.Checked;
            _costGroupBox.Enabled = !_heroCheckBox.Checked;
        }

        #endregion
    }
}
