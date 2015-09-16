using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using DRSLibrary;
using TechTreeEditor.TechTreeStructure;
using System.Drawing.Imaging;
using System.Threading;

namespace TechTreeEditor
{
	public partial class UnitRenderForm : Form
	{
		#region Konstanten

		/// <summary>
		/// Die Standard-Hintergrundfarbe.
		/// </summary>
		private Color COLOR_BACKGROUND = Color.FromArgb(243, 233, 212);

		/// <summary>
		/// Die Zeit in Millisekunden, die zwischen zwei Frames im Rendermodus gewartet wird.
		/// </summary>
		private int RENDER_FRAME_TIME = 40;

		#endregion Konstanten

		#region Variablen

		/// <summary>
		/// Gibt an, ob die OpenGL-Zeichenfläche bereits initialisiert wurde.
		/// </summary>
		private bool _glLoaded = false;

		/// <summary>
		/// Gibt an, ob gerade die zurendernde Einheit aktualisiert wird. Dies unterbindet Ereignisse von Buttons o.ä..
		/// </summary>
		private bool _updatingUnit = false;

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Die zu rendernde Einheit.
		/// </summary>
		private TechTreeUnit _renderUnit = null;

		/// <summary>
		/// Die Farbtabelle zu Rendern von Grafiken.
		/// </summary>
		private BitmapLibrary.ColorTable _pal50500 = null;

		/// <summary>
		/// Die Graphics-DRS mit Grafiken.
		/// </summary>
		private DRSFile _graphicsDRS = null;

		/// <summary>
		/// Die aktuell gezeigte Grafik.
		/// </summary>
		private GraphicMode _currShowedGraphic = GraphicMode.Attacking;

		/// <summary>
		/// Die einzelnen zu zeichnenden Animationen.
		/// </summary>
		private List<Animation> _animations;

		/// <summary>
		/// Die Vergrößerung der zu zeichnenden Animationen.
		/// </summary>
		private float _zoom = 1.0f;

		/// <summary>
		/// Der Faktor zur Rendergeschwindigkeit.
		/// </summary>
		private float _speedMult = 1.0f;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private UnitRenderForm()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Render-Timer-Intervall setzen
			_renderTimer.Interval = RENDER_FRAME_TIME;

			// Animationsliste anlegen
			_animations = new List<Animation>();

			// Farbpalette laden
			_pal50500 = new BitmapLibrary.ColorTable(new BitmapLibrary.JASCPalette(new IORAMHelper.RAMBuffer(Properties.Resources.pal50500)));
		}

		/// <summary>
		/// Erstellt ein neues Einheiten-Render-Fenster.
		/// </summary>
		/// <param name="projectFile">Das zugrundeliegende Projekt.</param>
		public UnitRenderForm(TechTreeFile projectFile)
			: this()
		{
			// Parameter merken
			_projectFile = projectFile;
		}

		/// <summary>
		/// Initialisiert OpenGL und die Zeichenfläche.
		/// </summary>
		private void InitDrawPanel()
		{
			// Ich bin dran
			_drawPanel.MakeCurrent();

			// Panel leeren
			GL.ClearColor(COLOR_BACKGROUND);

			// Texturen aktivieren
			GL.Enable(EnableCap.Texture2D);

			// 2D-Grafik benötigt keine Tiefenerkennung
			GL.Disable(EnableCap.DepthTest);

			// Blending einschalten
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			// Blickwinkel erstellen
			SetupDrawPanelViewPort();

			// Alles ist geladen
			_glLoaded = true;
		}

		/// <summary>
		/// Erstellt die Koordinaten und Blickwinkel der Zeichenfläche.
		/// </summary>
		private void SetupDrawPanelViewPort()
		{
			// Ich bin dran
			_drawPanel.MakeCurrent();

			// Blickwinkel erstellen
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, _drawPanel.Width, _drawPanel.Height, 0, -1, 1); // Pixel oben links ist (0, 0)
			GL.Viewport(0, 0, _drawPanel.Width, _drawPanel.Height);

			// Zeichenmodus laden
			GL.MatrixMode(MatrixMode.Modelview);
		}

		/// <summary>
		/// Aktualisiert die zugrundeliegende Projektdatei.
		/// </summary>
		/// <param name="projectFile">Die neue Projektdatei.</param>
		public void UpdateProjectFile(TechTreeFile projectFile)
		{
			// Aktualisieren
			_projectFile = projectFile;

			// Neuzeichnen
			_drawPanel.Invalidate();
		}

		/// <summary>
		/// Aktualisiert die zu rendernde Einheit.
		/// </summary>
		/// <param name="renderUnit">Die neue zu rendernde Einheit.</param>
		public void UpdateRenderUnit(TechTreeUnit renderUnit)
		{
			// Merken
			_renderUnit = renderUnit;

			// Einheit definiert?
			if(renderUnit == null || renderUnit.DATUnit == null)
				return;

			// Je nach Einheitentyp Buttons (de-)aktivieren
			_updatingUnit = true;
			if(renderUnit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.DeadFish)
				_graMovingButton.Enabled = true;
			else
			{
				// Button deaktivieren und deselektieren
				_graMovingButton.Enabled = false;
				if(_graMovingButton.Checked)
				{
					// Stehende Grafik auswählen
					_graStandingButton.Select();
					_currShowedGraphic = GraphicMode.Standing;
				}
			}
			if(renderUnit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Type50)
				_graAttackingButton.Enabled = true;
			else
			{
				// Button deaktivieren und deselektieren
				_graAttackingButton.Enabled = false;
				if(_graAttackingButton.Checked)
				{
					// Stehende Grafik auswählen
					_graStandingButton.Select();
					_currShowedGraphic = GraphicMode.Standing;
				}
			}

			// Fertig
			_updatingUnit = false;

			// Zu rendernde Grafik erstellen
			PreprocessRenderedGraphic();

			// Neuzeichnen
			_drawPanel.Invalidate();
		}

		/// <summary>
		/// Lädt die zu zeigende Grafik und bereitet diese vor, sodass sie effizient gerendert werden kann.
		/// </summary>
		public void PreprocessRenderedGraphic()
		{
			// Kontext holen
			_drawPanel.MakeCurrent();

			// Alte Animationen löschen
			foreach(Animation anim in _animations)
			{
				// Stoppen und Textur freigeben
				anim.StopAnimation();
				if(anim.TextureID > 0)
					GL.DeleteTexture(anim.TextureID);
			}
			_animations.Clear();

			// Ist eine Einheit geladen?
			if(_renderUnit == null || _renderUnit.DATUnit == null || _graphicsDRS == null)
				return;

			// Animation erstellen
			Animation animation = new Animation();

			// Grafik-ID holen
			int graID = -1;
			switch(_currShowedGraphic)
			{
				case GraphicMode.Attacking:
					graID = _renderUnit.DATUnit.Type50.AttackGraphic;
					break;
				case GraphicMode.Falling:
					graID = _renderUnit.DATUnit.DyingGraphic1;
					break;
				case GraphicMode.Moving:
					graID = _renderUnit.DATUnit.DeadFish.WalkingGraphic1;
					break;
				case GraphicMode.Standing:
					graID = _renderUnit.DATUnit.StandingGraphic1;
					break;
			}

			// Grafik abrufen
			if(!_projectFile.BasicGenieFile.Graphics.ContainsKey(graID))
				return;
			GenieLibrary.DataElements.Graphic renderGraphic = _projectFile.BasicGenieFile.Graphics[graID];

			// Zugehörige SLP-Datei abrufen
			if(renderGraphic.SLP < 0 || !_graphicsDRS.ResourceExists((uint)renderGraphic.SLP))
				return;
			SLPLoader.Loader renderSLP = new SLPLoader.Loader(new IORAMHelper.RAMBuffer(_graphicsDRS.GetResourceData((uint)renderGraphic.SLP)));
			animation.FrameCount = (int)renderSLP.FrameCount;
			animation.BaseFrameTime = renderGraphic.FrameRate * 1000.0f;

			// Maximale Frame-Größe berechnen: Diese setzt sich zusammen aus der Breite der einzelnen SLP-Frames und den zugehörigen Frame-Ankern, ausgehend vom Mittelpunkt.
			// => Beim Rendern muss später nur noch die Textur durchgeschoben werden, Anker sind nicht mehr zu berücksichtigen.
			int offsetLeft = 0;
			int offsetRight = 0;
			int offsetTop = 0;
			int offsetBottom = 0;
			for(int f = 0; f < animation.FrameCount; ++f)
			{
				// Werte berechnen
				offsetLeft = Math.Max(offsetLeft, renderSLP._frameInformationenHeaders[f].XAnker);
				offsetRight = Math.Max(offsetRight, (int)renderSLP._frameInformationenHeaders[f].Breite - renderSLP._frameInformationenHeaders[f].XAnker);
				offsetTop = Math.Max(offsetTop, renderSLP._frameInformationenHeaders[f].YAnker);
				offsetBottom = Math.Max(offsetBottom, (int)renderSLP._frameInformationenHeaders[f].Höhe - renderSLP._frameInformationenHeaders[f].YAnker);
			}

			// Framegrößen berechnen
			int frameWidth = offsetLeft + offsetRight;
			if(frameWidth % 2 == 1)
				++frameWidth;
			int frameHeight = offsetTop + offsetBottom;
			if(frameHeight % 2 == 1)
				++frameHeight;
			animation.FrameBounds = new Size(frameWidth, frameHeight);

			// Eine ungefähr quadratische Textur soll am Ende herauskommen
			animation.FramesPerLine = (int)Math.Sqrt(animation.FrameCount);
			int textureWidth = (int)NextPowerOfTwo((uint)animation.FramesPerLine * (uint)frameWidth);
			int textureHeight = (int)NextPowerOfTwo((uint)Math.Round((double)animation.FrameCount / animation.FramesPerLine) * (uint)frameWidth);
			animation.TextureBounds = new Size(textureWidth, textureHeight);

			// Frames nacheinander auf Bitmap zeichnen
			using(Bitmap textureBitmap = new Bitmap(textureWidth, textureHeight))
			{
				// Bitmap-Grafikobjekt abrufen
				using(Graphics g = Graphics.FromImage(textureBitmap))
				{
					// Frames durchlaufen
					int i = 0;
					int j = 0;
					for(uint f = 0; f < animation.FrameCount; ++f)
					{
						// Frame-Bitmap holen und zeichnen
						using(Bitmap currFrame = renderSLP.getFrameAsBitmap(f, _pal50500, SLPLoader.Loader.Masks.Graphic, COLOR_BACKGROUND))
							g.DrawImage(currFrame, j * frameWidth + offsetLeft - renderSLP._frameInformationenHeaders[(int)f].XAnker, i * frameHeight + offsetTop - renderSLP._frameInformationenHeaders[(int)f].YAnker);

						// Koordinaten erhöhen
						if(++j >= animation.FramesPerLine)
						{
							// Neue Zeile
							++i;
							j = 0;
						}
					}
				}

				// Textur anlegen und binden
				animation.TextureID = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, animation.TextureID);

				// Textur soll bei Verkleinerung/Vergrößerung scharf (= pixelig) bleiben
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

				// Bild-Bits sperren
				BitmapData data = textureBitmap.LockBits(new Rectangle(0, 0, textureWidth, textureHeight), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				// Textur an OpenGL übergeben
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textureWidth, textureHeight, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

				// Bild-Bits wieder freigeben
				textureBitmap.UnlockBits(data);

				// Texturbindung beenden
				GL.BindTexture(TextureTarget.Texture2D, 0);
			}

			// Animation speichern
			_animations.Add(animation);

			// Falls Rendering läuft, Animationen starten
			if(_renderTimer.Enabled)
				foreach(Animation anim in _animations)
					anim.StartAnimation();
		}

		/// <summary>
		/// Aktualisiert die Aktualisierungsgeschwindigkeiten der einzelnen Animationen.
		/// </summary>
		/// <param name="newSpeedMult">Der neue Geschwindigkeitsfaktor.</param>
		private void UpdateAnimationSpeed(float newSpeedMult)
		{
			// Faktoren aktualisieren
			_speedMult = newSpeedMult;
			foreach(Animation anim in _animations)
				anim.SpeedMultiplier = _speedMult;
		}

		#endregion Funktionen

		#region Ereignishandler

		private void UnitRenderForm_Load(object sender, EventArgs e)
		{
			// OpenGL und das Zeichenpanel initialisieren
			if(!DesignMode)
				InitDrawPanel();
		}

		private void _drawPanel_Paint(object sender, PaintEventArgs e)
		{
			// Während Einheiten-Update lieber nichts tun
			if(_updatingUnit)
				return;

			// Wurde OpenGL schon geladen?
			if(!_glLoaded)
				return;

			// Kontext holen
			_drawPanel.MakeCurrent();

			// Zeichenfläche leeren
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Zeichenmatrix zurücksetzen
			GL.LoadIdentity();

			// Animationen der Reihe nach zeichnen
			foreach(Animation animation in _animations)
			{
				// Ist eine Animationstextur vorhanden?
				if(animation.TextureID > 0)
				{
					// Textur binden
					GL.BindTexture(TextureTarget.Texture2D, animation.TextureID);

					// Frame-Koordinaten berechnen
					double frameX = (animation.FrameID % animation.FramesPerLine) * animation.FrameBounds.Width;
					double frameY = (animation.FrameID / animation.FramesPerLine) * animation.FrameBounds.Height;

					// Frame zeichnen
					double texLeft = frameX / animation.TextureBounds.Width;
					double texRight = (frameX + animation.FrameBounds.Width) / animation.TextureBounds.Width;
					double texTop = frameY / animation.TextureBounds.Height;
					double texBottom = (frameY + animation.FrameBounds.Height) / animation.TextureBounds.Height;
					GL.Begin(PrimitiveType.Quads);
					{
						GL.TexCoord2(texLeft, texTop); GL.Vertex2(0, 0); // Oben links
						GL.TexCoord2(texRight, texTop); GL.Vertex2(_zoom * animation.FrameBounds.Width, 0); // Oben rechts
						GL.TexCoord2(texRight, texBottom); GL.Vertex2(_zoom * animation.FrameBounds.Width, _zoom * animation.FrameBounds.Height); // Unten rechts
						GL.TexCoord2(texLeft, texBottom); GL.Vertex2(0, _zoom * animation.FrameBounds.Height); // Unten links
					}
					GL.End();
				}
			}

			// Puffer tauschen, um Flackern zu vermeiden
			_drawPanel.SwapBuffers();
		}

		private void _drawPanel_Resize(object sender, EventArgs e)
		{
			// Visual Studio Designer-Crash verhindern
			if(DesignMode)
				return;

			// Prüfen, ob das Panel überhaupt eine Größe hat (Fenster minimiert?)
			if(_drawPanel.Width != 0 && _drawPanel.Height != 0)
			{
				// Blickwinkel neu laden
				SetupDrawPanelViewPort();

				// Neuzeichnen erzwingen
				_drawPanel.Invalidate();
			}
		}

		private void _graphicsDRSButton_Click(object sender, EventArgs e)
		{
			// Dialog anzeigen
			if(_openGraphicsDRSDialog.ShowDialog() == DialogResult.OK)
			{
				// Dateinamen aktualisieren
				_graphicsDRSTextBox.Text = _openGraphicsDRSDialog.FileName;
			}
		}

		private void _loadDRSButton_Click(object sender, EventArgs e)
		{
			// DRS-Datei laden
			try
			{
				// Laden
				_graphicsDRS = new DRSFile(_graphicsDRSTextBox.Text);

				// Play-Button freischalten
				_playButton.Enabled = true;
			}
			catch(Exception ex)
			{
				// Fehlermeldung anzeigen und Fenster schließen
				MessageBox.Show("Fehler beim Laden der Graphics-DRS: " + ex.Message + "\n\nDas Fenster wird geschlossen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

		private void _closeButton_Click(object sender, EventArgs e)
		{
			// Fenster schließen
			if(MessageBox.Show("Fenster wirklich schließen?", "Fenster schließen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				Close();
		}

		private void _graAttackingButton_CheckedChanged(object sender, EventArgs e)
		{
			// Button bei Einheiten-Update ignorieren
			if(_updatingUnit)
				return;

			// Aktiviert?
			if(_graAttackingButton.Checked)
			{
				// Gezeigte Grafik ändern
				_currShowedGraphic = GraphicMode.Attacking;

				// Grafik laden
				PreprocessRenderedGraphic();

				// Neu zeichnen
				_drawPanel.Invalidate();
			}
		}

		private void _graStandingButton_CheckedChanged(object sender, EventArgs e)
		{
			// Button bei Einheiten-Update ignorieren
			if(_updatingUnit)
				return;

			// Aktiviert?
			if(_graStandingButton.Checked)
			{
				// Gezeigte Grafik ändern
				_currShowedGraphic = GraphicMode.Standing;

				// Grafik laden
				PreprocessRenderedGraphic();

				// Neu zeichnen
				_drawPanel.Invalidate();
			}
		}

		private void _graMovingButton_CheckedChanged(object sender, EventArgs e)
		{
			// Button bei Einheiten-Update ignorieren
			if(_updatingUnit)
				return;

			// Aktiviert?
			if(_graMovingButton.Checked)
			{
				// Gezeigte Grafik ändern
				_currShowedGraphic = GraphicMode.Moving;

				// Grafik laden
				PreprocessRenderedGraphic();

				// Neu zeichnen
				_drawPanel.Invalidate();
			}
		}

		private void _graFallingButton_CheckedChanged(object sender, EventArgs e)
		{
			// Button bei Einheiten-Update ignorieren
			if(_updatingUnit)
				return;

			// Aktiviert?
			if(_graFallingButton.Checked)
			{
				// Gezeigte Grafik ändern
				_currShowedGraphic = GraphicMode.Falling;

				// Grafik laden
				PreprocessRenderedGraphic();

				// Neu zeichnen
				_drawPanel.Invalidate();
			}
		}

		private void _zoomField_ValueChanged(object sender, EventArgs e)
		{
			// Neuen Zoom speichern
			_zoom = (float)_zoomField.Value;

			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _speedSlowButton_CheckedChanged(object sender, EventArgs e)
		{
			// Geschwindigkeit ändern
			UpdateAnimationSpeed(0.6f);
		}

		private void _speedNormalButton_CheckedChanged(object sender, EventArgs e)
		{
			// Geschwindigkeit ändern
			UpdateAnimationSpeed(0.75f);
		}

		private void _speedFastButton_CheckedChanged(object sender, EventArgs e)
		{
			// Geschwindigkeit ändern
			UpdateAnimationSpeed(1.0f);
		}

		private void _speedCustomButton_CheckedChanged(object sender, EventArgs e)
		{
			// Eingabefeld (de-)aktivieren
			_speedCustomField.Enabled = _speedCustomButton.Checked;
			if(_speedCustomButton.Checked)
				UpdateAnimationSpeed((float)_speedCustomField.Value);
		}

		private void _speedCustomField_ValueChanged(object sender, EventArgs e)
		{
			// Wert setzen
			UpdateAnimationSpeed((float)_speedCustomField.Value);
		}

		private void _playButton_Click(object sender, EventArgs e)
		{
			// Render-Timer starten
			_playButton.Enabled = false;
			_stopButton.Enabled = true;
			_renderTimer.Start();

			// Animationstimer starten
			foreach(Animation anim in _animations)
				anim.StartAnimation();
		}

		private void _stopButton_Click(object sender, EventArgs e)
		{
			// Animationstimer beenden
			foreach(Animation anim in _animations)
				anim.StopAnimation();

			// Render-Timer beenden
			_playButton.Enabled = true;
			_stopButton.Enabled = false;
			_renderTimer.Stop();
		}

		private void _renderTimer_Tick(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void UnitRenderForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Animationsthreads beenden
			_renderTimer.Stop();
			foreach(Animation anim in _animations)
				anim.StopAnimation();
		}

		#endregion Ereignishandler

		#region Hilfsfunktionen

		/// <summary>
		/// Berechnet die nächsthöhere (oder gleiche) Zweierpotenz zum gegebenen Wert.
		/// </summary>
		/// <param name="value">Der Wert, zu dem die nächsthöhere Zweierpotenz gefunden werden soll.</param>
		/// <returns></returns>
		public uint NextPowerOfTwo(uint value)
		{
			// Bit-Magie
			--value;
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return ++value;
		}

		#endregion

		#region Hilfsklassen

		/// <summary>
		/// Definiert eine Animation.
		/// </summary>
		private class Animation
		{
			#region Variablen

			/// <summary>
			/// Die Zeit in Millisekunden, die ein Frame angezeigt wird.
			/// </summary>
			public float BaseFrameTime = 100.0f;

			/// <summary>
			/// Der Faktor, dessen Inverses auf die Darstellungszeit eines Frames angewandt werden soll.
			/// </summary>
			public float SpeedMultiplier = 1.0f;

			/// <summary>
			/// Die Textur-ID der Animation.
			/// </summary>
			public int TextureID = 0;

			/// <summary>
			/// Die Abmessungen der Animations-Textur (2er-Potenz).
			/// </summary>
			public Size TextureBounds;

			/// <summary>
			/// Die Anzahl der Frames der Animation.
			/// </summary>
			public int FrameCount = 0;

			/// <summary>
			/// Die Anzahl der Frames in der Animations-Textur pro Zeile.
			/// </summary>
			public int FramesPerLine = 0;

			/// <summary>
			/// Der aktuell angezeigte Frame der Animation.
			/// </summary>
			public int FrameID = 0;

			/// <summary>
			/// Die Abmessungen der einzelnen Animations-Frames (alle gleich groß).
			/// </summary>
			public Size FrameBounds;

			/// <summary>
			/// Der interne Thread, der durch stetiges Ändern der Frame-ID die Animation realisiert.
			/// </summary>
			private Thread _animateThread = null;

			/// <summary>
			/// Gibt an, ob der Animate-Thread weiter laufen soll (Abbruch bei false).
			/// </summary>
			private volatile bool _runAnimateThread = false;

			#endregion

			#region Funktionen

			/// <summary>
			/// Konstruktor. Erstellt eine neue Animation.
			/// </summary>
			public Animation()
			{
				// Nix zu tun
			}

			/// <summary>
			/// Startet die Animation.
			/// </summary>
			public void StartAnimation()
			{
				// Thread starten
				if(_animateThread == null)
				{
					// Thread neu erstellen und starten
					_runAnimateThread = true;
					_animateThread = new Thread(AnimateThread);
					_animateThread.IsBackground = true;
					_animateThread.Start();
				}
			}

			/// <summary>
			/// Stoppt die Animation.
			/// </summary>
			public void StopAnimation()
			{
				// Thread stoppen
				_runAnimateThread = false;
				if(_animateThread.IsAlive)
					_animateThread.Join();
				_animateThread = null;
			}

			/// <summary>
			/// Definiert den Animationsthread.
			/// </summary>
			public void AnimateThread()
			{
				// Endlos laufen, bis Thread beendet wird
				// TODO: Bei möglicherweise kuriosen Laufzeitfehlern hier etwas mehr Synchronität versuchen?
				while(_runAnimateThread)
				{
					// Frame-ID inkrementieren
					if(FrameID < FrameCount - 1)
						++FrameID;
					else
						FrameID = 0;

					// Etwas warten
					Thread.Sleep((int)(BaseFrameTime / SpeedMultiplier));
				}
			}

			#endregion
		}

		#endregion

		#region Enumerationen

		/// <summary>
		/// Die verschiedenen Grafik-Typen.
		/// </summary>
		private enum GraphicMode
		{
			Attacking,
			Falling,
			Moving,
			Standing
		}

		#endregion
	}
}
