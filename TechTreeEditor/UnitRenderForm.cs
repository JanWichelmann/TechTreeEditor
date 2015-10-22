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
using System.IO;

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
		/// Die Standard-Schattenfarbe.
		/// </summary>
		private Color COLOR_SHADOW = Color.FromArgb(100, 100, 100, 100);

		/// <summary>
		/// Die Zeit in Millisekunden, die zwischen zwei Frames im Rendermodus gewartet wird.
		/// </summary>
		private int RENDER_FRAME_TIME = 40;

		/// <summary>
		/// Der vertikale Abstand der Eckpunkte eines Tiles zu dessen Mittelpunkt.
		/// </summary>
		private double TILE_VERTICAL_OFFSET = 11 * Math.Sqrt(5);

		/// <summary>
		/// Der horizontale Abstand der Eckpunkte eines Tiles zu dessen Mittelpunkt.
		/// </summary>
		private double TILE_HORIZONTAL_OFFSET = 22 * Math.Sqrt(5);

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
		/// Die Zeichen-Textur.
		/// </summary>
		private static int _charTextureID = 0;

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

		/// <summary>
		/// Die Position der Maus beim letzten Betätigen der linken Maustaste im Zeichenfeld.
		/// </summary>
		private Point _mouseClickLocation;

		/// <summary>
		/// Die Verschiebung der Zeichenfläche.
		/// </summary>
		private Point _renderingTranslation = new Point(0, 0);

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private UnitRenderForm()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Steuerelement-Einstellungen laden
			Location = Properties.Settings.Default.UnitRenderFormLocation;
			Size = Properties.Settings.Default.UnitRenderFormSize;
			WindowState = Properties.Settings.Default.UnitRenderFormWindowState;

			// Render-Timer-Intervall setzen
			_renderTimer.Interval = RENDER_FRAME_TIME;

			// Animationsliste anlegen
			_animations = new List<Animation>();

			// Farbpalette laden
			_pal50500 = new BitmapLibrary.ColorTable(new BitmapLibrary.JASCPalette(new IORAMHelper.RAMBuffer(Properties.Resources.pal50500)));

			// Mausrad-Ereignis auf der Zeichenfläche behandeln
			_drawPanel.MouseWheel += _drawPanel_MouseWheel;
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

			// Graphics-DRS laden
			string graDRS = Path.GetFullPath(_projectFile.GraphicsDRSPath);
			if(File.Exists(graDRS))
			{
				// Laden
				_graphicsDRSTextBox.Text = graDRS;
				_loadDRSButton_Click(this, EventArgs.Empty);
			}
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

			// Zeichen-Texturen für String-Rendering erstellen
			{
				// Bitmap laden
				Bitmap charTexBitmap = TechTreeEditor.Properties.Resources.RenderFontConsolas;

				// Textur generieren
				_charTextureID = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, _charTextureID);

				// Parameter setzen
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

				// Textur übergeben
				BitmapData charTexBitmapData = charTexBitmap.LockBits(new Rectangle(0, 0, 256, 128), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 256, 128, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, charTexBitmapData.Scan0);

				// Ressourcen wieder freigeben
				charTexBitmap.UnlockBits(charTexBitmapData);
			}

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
			GL.Ortho(0.0, _drawPanel.Width, _drawPanel.Height, 0.0, -1.0, 1.0); // Pixel oben links ist (0, 0)
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
			{
				// Laufgrafik-Button aktivieren
				_graMovingButton.Enabled = true;
			}
			else
			{
				// Laufgrafik-Button deaktivieren und deselektieren
				_graMovingButton.Enabled = false;
				if(_graMovingButton.Checked)
				{
					// Stehende Grafik auswählen
					_graStandingButton.Select();
					_currShowedGraphic = GraphicMode.Standing;
				}
			}
			if(renderUnit.DATUnit.Type >= GenieLibrary.DataElements.Civ.Unit.UnitType.Type50)
			{
				// Angriffsgrafik-Button aktivieren
				_graAttackingButton.Enabled = true;

				// Projektilpunkt-Funtion aktivieren
				_showProjectilePointCheckBox.Enabled = ((renderUnit as TechTreeBuilding)?.ProjectileUnit != null || (renderUnit as TechTreeCreatable)?.ProjectileUnit != null);
				if(!_showProjectilePointCheckBox.Enabled)
					_showProjectilePointCheckBox.Checked = false;
			}
			else
			{
				// Angriffsgrafik-Button deaktivieren und deselektieren
				_graAttackingButton.Enabled = false;
				if(_graAttackingButton.Checked)
				{
					// Stehende Grafik auswählen
					_graStandingButton.Select();
					_currShowedGraphic = GraphicMode.Standing;
				}

				// Projektilpunkt-Funtion deaktivieren
				_showProjectilePointCheckBox.Enabled = false;
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
		private void PreprocessRenderedGraphic()
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
			_angleField.Maximum = 0;

			// Ist eine Einheit geladen?
			if(_renderUnit == null || _renderUnit.DATUnit == null || _graphicsDRS == null)
				return;

			// Ggf. Annexes durchgehen
			if(_renderUnit is TechTreeBuilding)
				foreach(var annex in ((TechTreeBuilding)_renderUnit).AnnexUnits)
					if(annex != null && annex.Item1 != null)
					{
						// Annex-Einheit abrufen
						TechTreeBuilding annexUnit = annex.Item1;

						// Annex-Grafik-ID holen
						int annexGraID = annexUnit.DATUnit.StandingGraphic1;

						// Grafik abrufen
						if(!_projectFile.BasicGenieFile.Graphics.ContainsKey(annexGraID))
							return;
						GenieLibrary.DataElements.Graphic annexRenderGraphic = _projectFile.BasicGenieFile.Graphics[annexGraID];

						// Offset berechnen und Animation erstellen
						int annexX = (int)Math.Round(TILE_HORIZONTAL_OFFSET * (annex.Item3 + annex.Item2));
						int annexY = (int)Math.Round(TILE_VERTICAL_OFFSET * (annex.Item3 - annex.Item2));
						CreateAnimationFromGraphic(annexRenderGraphic, new Point(annexX, annexY), true);

						// Ggf. Schnee-Grafik mit draufzeichnen
						if(_graSnowCheckBox.Checked)
							if(annexUnit.DATUnit.Building.SnowGraphicID >= 0 && _projectFile.BasicGenieFile.Graphics.ContainsKey(annexUnit.DATUnit.Building.SnowGraphicID))
								CreateAnimationFromGraphic(_projectFile.BasicGenieFile.Graphics[annexUnit.DATUnit.Building.SnowGraphicID], new Point(annexX, annexY), true);
					}

			// Falls die Einheit eine Obereinheit hat, diese zeichnen
			TechTreeUnit renderUnit = _renderUnit;
			if(renderUnit is TechTreeBuilding && ((TechTreeBuilding)renderUnit).HeadUnit != null)
				renderUnit = ((TechTreeBuilding)renderUnit).HeadUnit;

			// Grafik-ID holen
			int graID = -1;
			if(renderUnit is TechTreeBuilding && _currShowedGraphic == GraphicMode.Attacking && renderUnit.DATUnit.Type50.AttackGraphic < 0)
				graID = renderUnit.DATUnit.StandingGraphic1;
			else
				switch(_currShowedGraphic)
				{
					case GraphicMode.Attacking:
						graID = renderUnit.DATUnit.Type50.AttackGraphic;
						break;
					case GraphicMode.Falling:
						graID = renderUnit.DATUnit.DyingGraphic1;
						break;
					case GraphicMode.Moving:
						graID = renderUnit.DATUnit.DeadFish.WalkingGraphic1;
						break;
					case GraphicMode.Standing:
						graID = renderUnit.DATUnit.StandingGraphic1;
						break;
				}

			// Grafik abrufen
			if(!_projectFile.BasicGenieFile.Graphics.ContainsKey(graID))
				return;
			GenieLibrary.DataElements.Graphic renderGraphic = _projectFile.BasicGenieFile.Graphics[graID];

			// Animation erstellen
			CreateAnimationFromGraphic(renderGraphic, new Point(0, 0), true);

			// Ggf. Schneegrafik zeichnen
			if(_graSnowCheckBox.Checked && renderUnit is TechTreeBuilding)
				if(renderUnit.DATUnit.Building.SnowGraphicID >= 0 && _projectFile.BasicGenieFile.Graphics.ContainsKey(renderUnit.DATUnit.Building.SnowGraphicID))
					CreateAnimationFromGraphic(_projectFile.BasicGenieFile.Graphics[renderUnit.DATUnit.Building.SnowGraphicID], new Point(0, 0), true);

			// Zentrieren
			_renderingTranslation = new Point(0, 0);

			// Falls Rendering läuft, Animationen starten
			if(_renderTimer.Enabled)
				foreach(Animation anim in _animations)
					anim.StartAnimation();
		}

		/// <summary>
		/// Erstellt eine Animation anhand der gegebenen DAT-Grafik.
		/// </summary>
		/// <param name="graphic">Die Grafik, anhand der die Animation erstellt werden soll.</param>
		/// <param name="offset">Der gewünschte Versatz der Grafik beim Rendervorgang.</param>
		/// <param name="parseDeltas">Gibt an, ob die Grafik-Delta-Werte zu eigenständigen Animationen umgewandelt werden sollen.</param>
		/// <returns></returns>
		private void CreateAnimationFromGraphic(GenieLibrary.DataElements.Graphic graphic, Point offset, bool parseDeltas)
		{
			// Animation erstellen
			Animation animation = new Animation();
			int animationIndex = _animations.Count;

			// Zur Grafik gehörende SLP-Datei abrufen
			if(graphic.SLP >= 0 && _graphicsDRS.ResourceExists((uint)graphic.SLP))
			{
				// SLP laden
				SLPLoader.SLPFile renderSLP = new SLPLoader.SLPFile(new IORAMHelper.RAMBuffer(_graphicsDRS.GetResourceData((uint)graphic.SLP)));

				// Die DAT-Framezahl sollte das Produkt aus Achsenzahl und SLP-Framezahl sein => nicht benötigte Achsenframes ignorieren
				uint slpFrameCount = renderSLP.FrameCount;
				uint slpAngleCount = (graphic.MirroringMode > 0 ? (uint)(graphic.AngleCount / 2 + 1) : graphic.AngleCount);
				if((slpFrameCount / graphic.FrameCount) != slpAngleCount)
					slpFrameCount = slpAngleCount * graphic.FrameCount;

				// Mindestens ein Frame ist sinnvoll, und mehr als die vorhandenen können auch nicht gerendert werden
				if(slpFrameCount > 0 && slpFrameCount <= renderSLP.FrameCount)
				{
					// Ein paar Parameter setzen
					animation.FrameCount = graphic.AngleCount * graphic.FrameCount;
					animation.BaseFrameTime = graphic.FrameRate * 1000.0f;
					animation.SpeedMultiplier = _speedMult;
					animation.AngleCount = graphic.AngleCount;

					// Achsenauswahlfeld aktualisieren
					// TODO TODO TODO Hier scheint irgendein Bug vorzuliegen, oder doch nicht??
					_angleField.Maximum = Math.Max(_angleField.Maximum, animation.AngleCount - 1) + 1;

					// Maximale Frame-Größe berechnen: Diese setzt sich zusammen aus der Breite der einzelnen SLP-Frames und den zugehörigen Frame-Ankern, ausgehend vom Mittelpunkt.
					// => Beim Rendern muss später nur noch die Textur durchgeschoben werden, Anker sind nicht mehr zu berücksichtigen.
					int offsetLeft = 0;
					int offsetRight = 0;
					int offsetTop = 0;
					int offsetBottom = 0;
					for(int f = 0; f < slpFrameCount; ++f)
					{
						// Werte berechnen
						offsetLeft = Math.Max(offsetLeft, renderSLP._frameInformationHeaders[f].AnchorX);
						offsetRight = Math.Max(offsetRight, (int)renderSLP._frameInformationHeaders[f].Width - renderSLP._frameInformationHeaders[f].AnchorX);
						offsetTop = Math.Max(offsetTop, renderSLP._frameInformationHeaders[f].AnchorY);
						offsetBottom = Math.Max(offsetBottom, (int)renderSLP._frameInformationHeaders[f].Height - renderSLP._frameInformationHeaders[f].AnchorY);
					}

					// Bei Spiegelachsen die horizontalen Offsets gleichsetzen
					if(graphic.MirroringMode > 0)
						offsetLeft = offsetRight = Math.Max(offsetLeft, offsetRight);

					// Framegrößen berechnen
					int frameWidth = offsetLeft + offsetRight;
					if(frameWidth % 2 == 1)
						++frameWidth;
					int frameHeight = offsetTop + offsetBottom;
					if(frameHeight % 2 == 1)
						++frameHeight;
					animation.FrameBounds = new Size(frameWidth, frameHeight);
					animation.RenderOffset = new Point(offset.X - offsetLeft, offset.Y - offsetTop);

					// Möglichst platzsparend mit dem Texturspeicher umgehen => beste Frameverteilung berechnen
					int maxTextureSize = GL.GetInteger(GetPName.MaxTextureSize);
					int minTexSize = int.MaxValue;
					for(int fpl = (int)Math.Ceiling((double)animation.FrameCount / (maxTextureSize / frameHeight)); fpl <= animation.FrameCount / 2 + 1 && (fpl * frameWidth <= maxTextureSize); ++fpl)
					{
						// Neue Größe berechnen und vergleichen
						int newTexSize = Log2(fpl * frameWidth) + Log2((int)Math.Ceiling((double)animation.FrameCount / fpl) * frameHeight);
						if(newTexSize < minTexSize)
						{
							// Besserer Wert gefunden
							minTexSize = newTexSize;
							animation.FramesPerLine = fpl;
						}
					}

					// Finale Texturengröße berechnen
					int textureWidth = (int)NextPowerOfTwo((uint)animation.FramesPerLine * (uint)frameWidth);
					int textureHeight = (int)NextPowerOfTwo((uint)Math.Ceiling((double)animation.FrameCount / animation.FramesPerLine) * (uint)frameHeight);
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
							for(uint f = 0; f < slpFrameCount; ++f)
							{
								// Frame-Bitmap holen und zeichnen
								using(Bitmap currFrame = renderSLP.getFrameAsBitmap(f, _pal50500, SLPLoader.SLPFile.Masks.Graphic, Color.FromArgb(0, 0, 0, 0), COLOR_SHADOW))
									g.DrawImage(currFrame, j * frameWidth + offsetLeft - renderSLP._frameInformationHeaders[(int)f].AnchorX, i * frameHeight + offsetTop - renderSLP._frameInformationHeaders[(int)f].AnchorY);

								// Koordinaten erhöhen
								if(++j >= animation.FramesPerLine)
								{
									// Neue Zeile
									++i;
									j = 0;
								}
							}

							// Ggf. Spiegelframes erzeugen => Alle Achsen außer Norden und Süden werden gespiegelt
							if(graphic.MirroringMode > 0)
								for(int a = (int)(slpFrameCount / graphic.FrameCount) - 2; a > 0; --a)
									for(uint f = (uint)a * graphic.FrameCount; f < (a + 1) * graphic.FrameCount; ++f)
									{
										// Frame-Bitmap holen und zeichnen
										using(Bitmap currFrame = renderSLP.getFrameAsBitmap(f, _pal50500, SLPLoader.SLPFile.Masks.Graphic, Color.FromArgb(0, 0, 0, 0), COLOR_SHADOW))
										{
											// Bild spiegeln
											currFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);

											// Bild in Textur zeichnen
											g.DrawImage(currFrame, j * frameWidth + offsetLeft - (renderSLP._frameInformationHeaders[(int)f].Width - renderSLP._frameInformationHeaders[(int)f].AnchorX), i * frameHeight + offsetTop - renderSLP._frameInformationHeaders[(int)f].AnchorY);
										}

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
					animation.Name = graphic.Name1;
					_animations.Add(animation);
				}
			}

			// Deltas erstellen?
			if(parseDeltas)
			{
				// Deltas durchlaufen
				foreach(var delta in graphic.Deltas)
				{
					// Gültiges Delta?
					if(!_projectFile.BasicGenieFile.Graphics.ContainsKey(delta.GraphicID))
					{
						// Bei ID -1 wird die Basisgrafik neu gezeichnet, d.h. nach vorn gebracht
						if(delta.GraphicID == -1 && _animations.Count > animationIndex)
						{
							// Element ans Ende schieben
							_animations.MoveToEnd(animationIndex);

							// Neuen Index merken
							animationIndex = _animations.Count - 1;
						}

						// Nächstes Delta
						continue;
					}

					// Delta-Grafik abrufen und Animation erstellen
					CreateAnimationFromGraphic(_projectFile.BasicGenieFile.Graphics[delta.GraphicID], new Point(offset.X + delta.DirectionX, offset.Y + delta.DirectionY), false);
				}
			}
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
			// Wurde OpenGL schon geladen?
			if(!_glLoaded)
				return;

			// Kontext holen
			_drawPanel.MakeCurrent();

			// Zeichenfläche leeren
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Während Einheiten-Update lieber nichts tun
			if(_updatingUnit || _renderUnit == null)
			{
				// Hintergrund anzeigen und abbrechen
				_drawPanel.SwapBuffers();
				return;
			}

			// Zeichenmatrix zurücksetzen
			GL.LoadIdentity();

			// Zeichenfläche verschieben
			GL.Translate(_renderingTranslation.X, _renderingTranslation.Y, 0.0f);

			// Ausgang aller Zeichnungen ist der Fenstermittelpunkt
			GL.Translate(_drawPanel.Width / 2.0f, _drawPanel.Height / 2.0f, 0.0f);

			// Tile-Gitternetz zeichnen
			for(float i = 0.0f; i < 16.5f; i += 0.5f)
				for(float j = 0.0f; j < 16.5f; j += 0.5f)
					DrawBorder(i, j, Color.FromArgb(180, 180, 180), false);

			// Rahmen zeichnen
			{
				// Rahmentyp bestimmen
				bool round = (_renderUnit.DATUnit.SelectionShape == 2);

				// Größe
				if(_radiusSizeCheckBox.Checked)
					DrawBorder(_renderUnit.DATUnit.SizeRadius1, _renderUnit.DATUnit.SizeRadius2, Color.Red, round);

				// Editor
				if(_radiusEditorCheckBox.Checked)
					DrawBorder(_renderUnit.DATUnit.EditorRadius1, _renderUnit.DATUnit.EditorRadius2, Color.Gray, round);

				// Auswahl
				if(_radiusSelectionCheckBox.Checked)
					DrawBorder(_renderUnit.DATUnit.SelectionRadius1, _renderUnit.DATUnit.SelectionRadius2, Color.DarkGreen, round);

				// Manuell
				if(_radiusCustomCheckBox.Checked)
					DrawBorder((float)_radiusCustom1Field.Value, (float)_radiusCustom2Field.Value, Color.Blue, round);
			}

			// Animationen der Reihe nach in der Fenstermitte zeichnen
			foreach(Animation animation in _animations)
			{
				// Ist eine Animationstextur vorhanden?
				if(animation.TextureID > 0)
				{
					// Textur binden
					GL.Color3(Color.White);
					GL.BindTexture(TextureTarget.Texture2D, animation.TextureID);

					// Gezoomte Texturabmessungen berechnen
					double zoomedTexHalfWidth = (_zoom / 2) * animation.FrameBounds.Width;
					double zoomedTexHalfHeight = (_zoom / 2) * animation.FrameBounds.Height;

					// Ansicht verschieben
					GL.PushMatrix();
					GL.Translate(animation.RenderOffset.X * _zoom, animation.RenderOffset.Y * _zoom, 0.0);
					{
						// Frame-Koordinaten berechnen
						int frameID = animation.FrameID;
						double frameX = (frameID % animation.FramesPerLine) * animation.FrameBounds.Width;
						double frameY = (frameID / animation.FramesPerLine) * animation.FrameBounds.Height;

						// Frame zeichnen
						double texLeft = frameX / animation.TextureBounds.Width;
						double texRight = (frameX + animation.FrameBounds.Width) / animation.TextureBounds.Width;
						double texTop = frameY / animation.TextureBounds.Height;
						double texBottom = (frameY + animation.FrameBounds.Height) / animation.TextureBounds.Height;
						GL.Begin(PrimitiveType.Quads);
						{
							GL.TexCoord2(texLeft, texTop); GL.Vertex2(0, 0); // Oben links
							GL.TexCoord2(texRight, texTop); GL.Vertex2(2 * zoomedTexHalfWidth, 0); // Oben rechts
							GL.TexCoord2(texRight, texBottom); GL.Vertex2(2 * zoomedTexHalfWidth, 2 * zoomedTexHalfHeight); // Unten rechts
							GL.TexCoord2(texLeft, texBottom); GL.Vertex2(0, 2 * zoomedTexHalfHeight); // Unten links
						}
						GL.End();
					}
					GL.PopMatrix();
				}
			}

			// Keine Textur binden
			GL.BindTexture(TextureTarget.Texture2D, 0);

			// Ankerposition markieren
			if(_showCenterPointCheckBox.Checked)
			{
				// Markierung zeichnen
				GL.Color3(Color.DeepPink);
				GL.Begin(PrimitiveType.Quads);
				{
					GL.Vertex2(-4, -4);
					GL.Vertex2(4, -4);
					GL.Vertex2(4, 4);
					GL.Vertex2(-4, 4);
				}
				GL.End();
			}

			// Projektilposition markieren
			if(_showProjectilePointCheckBox.Checked)
			{
				// Position berechnen
				PointF projPos = new PointF();
				int angle = (int)_angleField.Value;
				if(_angleField.Maximum > 8)
					angle /= ((int)_angleField.Maximum / 8); // Achse auf 8 Achsen normieren
				if(angle % 2 == 0)
				{
					// X/Y bestimmen
					double projX = 0.0;
					double projY = -_renderUnit.DATUnit.Type50.GraphicDisplacement[2];
					if(angle == 0 || angle == 4)
					{
						// Blickrichtung unten/oben
						projX -= (angle - 2) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[0];
						projY -= (angle - 2) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[1];
					}
					else if(angle == 2 || angle == 6)
					{
						// Blickrichtung links/rechts
						projX += (angle - 4) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[1];
						projY += (angle - 4) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[0];
					}

					// Projektilposition berechnen
					projPos.X = (float)(projX * TILE_HORIZONTAL_OFFSET);
					projPos.Y = (float)(projY * TILE_VERTICAL_OFFSET);
				}
				else
				{
					// Tile-Abstand bestimmen
					if(angle == 1 || angle == 5)
					{
						// Blickrichtung unten links/oben rechts
						double proj1 = (angle - 3) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[1];
						double proj2 = (angle - 3) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[0];

						// Projektilposition berechnen
						projPos.X = (float)((proj1 + proj2) * TILE_HORIZONTAL_OFFSET);
						projPos.Y = (float)((-(proj1 - proj2) - _renderUnit.DATUnit.Type50.GraphicDisplacement[2]) * TILE_VERTICAL_OFFSET);
					}
					else if(angle == 3 || angle == 7)
					{
						// Blickrichtung oben links/unten rechts
						double proj1 = -(angle - 5) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[0];
						double proj2 = (angle - 5) / 2 * _renderUnit.DATUnit.Type50.GraphicDisplacement[1];

						// Projektilposition berechnen
						projPos.X = (float)((proj2 - proj1) * TILE_HORIZONTAL_OFFSET);
						projPos.Y = (float)((proj2 + proj1 - _renderUnit.DATUnit.Type50.GraphicDisplacement[2]) * TILE_VERTICAL_OFFSET);
					}
				}

				// Projektilposition an den Zoom anpassen
				projPos.X *= _zoom;
				projPos.Y *= _zoom;

				// Projektilposition zeichnen
				GL.Color3(Color.Red);
				GL.Begin(PrimitiveType.Quads);
				{
					GL.Vertex2(projPos.X - 2, projPos.Y - 2);
					GL.Vertex2(projPos.X + 2, projPos.Y - 2);
					GL.Vertex2(projPos.X + 2, projPos.Y + 2);
					GL.Vertex2(projPos.X - 2, projPos.Y + 2);
				}
				GL.End();
			}

			// Zeichenmatrix zurücksetzen
			GL.LoadIdentity();

			// Ggf. Frame-Nummer-Box zeichnen
			if(_showFrameNumbersCheckBox.Checked)
			{
				// Maximale Breite eines Eintrags "{ANIM_NAME}:{SPACING}{FRAME}" ist x + 1 + (21 - x) + 4 = 26
				Size entrySize = MeasureString(new string('x', 26));

				// Box-Position ist immer mit geringem Abstand am Bildschirmrand
				GL.Translate(_drawPanel.Width - 22 - entrySize.Width, 18, 0);

				// Header-Leiste zeichnen
				string header = "Frame-Nummern";
				GL.BindTexture(TextureTarget.Texture2D, 0);
				GL.Color3(Color.FromArgb(179, 255, 102));
				GL.Begin(PrimitiveType.Quads);
				{
					GL.Vertex2(0, 0);
					GL.Vertex2(entrySize.Width + 4, 0);
					GL.Vertex2(entrySize.Width + 4, entrySize.Height + 4);
					GL.Vertex2(0, entrySize.Height + 4);
				}
				GL.End();

				// Headertext zeichnen
				DrawString(header, 2, 2);

				// Box für Framenummern zeichnen
				GL.Color3(Color.FromArgb(218, 255, 179));
				int frameNumberBoxHeight = _animations.Count * (entrySize.Height + 2);
				GL.Begin(PrimitiveType.Quads);
				{
					GL.Vertex2(0, entrySize.Height + 4);
					GL.Vertex2(entrySize.Width + 4, entrySize.Height + 4);
					GL.Vertex2(entrySize.Width + 4, entrySize.Height + 4 + frameNumberBoxHeight);
					GL.Vertex2(0, entrySize.Height + 4 + frameNumberBoxHeight);
				}
				GL.End();

				// Framenummern zeichnen
				int frameNumberPosY = entrySize.Height + 4;
				foreach(Animation anim in _animations)
				{
					// Eintrag zeichnen
					DrawString(anim.Name + ":" + new string(' ', 21 - anim.Name.Length) + string.Format("{0,4}", anim.FrameID), 2, frameNumberPosY + 1);

					// Nächste
					frameNumberPosY += entrySize.Height + 2;
				}

				// Rahmen zeichnen
				GL.Color3(Color.Black);
				GL.Begin(PrimitiveType.LineLoop);
				{
					GL.Vertex2(0 +0.5, 0 + 0.5);
					GL.Vertex2(entrySize.Width + 4 + 0.5, 0 + 0.5);
					GL.Vertex2(entrySize.Width + 4 + 0.5, entrySize.Height + 4 + frameNumberBoxHeight + 0.5);
					GL.Vertex2(0 + 0.5, entrySize.Height + 4 + frameNumberBoxHeight + 0.5);
				}
				GL.End();
			}

			// Puffer tauschen => Bildschirmausgabe
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

		private void _graSnowCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Button bei Einheiten-Update ignorieren
			if(_updatingUnit)
				return;

			// Grafik laden
			PreprocessRenderedGraphic();

			// Neu zeichnen
			_drawPanel.Invalidate();
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
			// Animationstimer beenden
			_renderTimer.Stop();
			foreach(Animation anim in _animations)
				anim.StopAnimation();

			// Steuerelement-Einstellungen merken
			Properties.Settings.Default.UnitRenderFormLocation = Location;
			Properties.Settings.Default.UnitRenderFormWindowState = WindowState;
			Properties.Settings.Default.UnitRenderFormSize = Size;
		}

		private void _angleField_ValueChanged(object sender, EventArgs e)
		{
			// Werte zyklisch ändern
			if(_angleField.Value == _angleField.Maximum)
				_angleField.Value = _angleField.Minimum + 1;
			else if(_angleField.Value == _angleField.Minimum)
				_angleField.Value = _angleField.Maximum - 1;

			// Neue Achse an Animationen durchgeben
			else if(_angleField.Enabled)
				foreach(Animation anim in _animations)
					anim.CurrentAngle = (int)(_angleField.Value / (_angleField.Maximum / anim.AngleCount));

			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _drawPanel_MouseDown(object sender, MouseEventArgs e)
		{
			// Verschiebe-Cursor anzeigen
			_drawPanel.Cursor = Cursors.SizeAll;
			_mouseClickLocation = e.Location;
		}

		private void _drawPanel_MouseUp(object sender, MouseEventArgs e)
		{
			// Cursor zurücksetzen
			_drawPanel.Cursor = Cursors.Default;
		}

		private void _drawPanel_MouseMove(object sender, MouseEventArgs e)
		{
			// Falls linke Maustause gedrückt, Zeichnung verschieben
			if(e.Button == MouseButtons.Left)
			{
				// Neues Offset berechnen
				_renderingTranslation = new Point(_renderingTranslation.X + e.Location.X - _mouseClickLocation.X, _renderingTranslation.Y + e.Location.Y - _mouseClickLocation.Y);
				_mouseClickLocation = e.Location;

				// Neuzeichnen
				_drawPanel.Invalidate();
			}
		}

		private void _drawPanel_MouseWheel(object sender, MouseEventArgs e)
		{
			// Steuerungstaste gedrückt?
			if(ModifierKeys == Keys.Control)
			{
				// Vom Zoom bereinigten Abstand des unter dem Mauszeiger befindlichen Punktes von Bildmitte berechnen
				PointF mousePointCenterDistance = new PointF((e.X - _drawPanel.Width / 2) / _zoom, (e.Y - _drawPanel.Height / 2) / _zoom);

				// Alte von Zoom bereinigte Verschiebung der Zeichenfläche merken
				PointF oldRenderingTranslation = new PointF(_renderingTranslation.X / _zoom, _renderingTranslation.Y / _zoom);

				// Zoomen
				float zoomDelta = (e.Delta > 0 ? 0.5f : -0.5f);
				if(_zoomField.Value + (decimal)zoomDelta <= _zoomField.Maximum && _zoomField.Value + (decimal)zoomDelta >= _zoomField.Minimum)
					_zoomField.Value += (decimal)zoomDelta;
				else if(zoomDelta < 0)
				{
					zoomDelta = (float)(_zoomField.Minimum - _zoomField.Value);
					_zoomField.Value = _zoomField.Minimum;
				}
				else
				{
					zoomDelta = (float)(_zoomField.Maximum - _zoomField.Value);
					_zoomField.Value = _zoomField.Maximum;
				}

				// Renderoffset entsprechend ändern, um auf die aktuelle Mausposition zoomen zu können
				_renderingTranslation.X = (int)(_zoom * oldRenderingTranslation.X + e.X - _drawPanel.Width / 2 - _zoom * mousePointCenterDistance.X);
				_renderingTranslation.Y = (int)(_zoom * oldRenderingTranslation.Y + e.Y - _drawPanel.Height / 2 - _zoom * mousePointCenterDistance.Y);

				// Neu zeichnen
				_drawPanel.Invalidate();
			}
		}

		private void _centerUnitButton_Click(object sender, EventArgs e)
		{
			// Zentrieren
			_renderingTranslation = new Point(0, 0);

			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _radiusCustomCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Auswahlfelder umschalten
			_radiusCustom1Field.Enabled = _radiusCustom2Field.Enabled = _radiusCustomCheckBox.Checked;

			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _radiusSizeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _radiusEditorCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _radiusSelectionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _radiusCustom1Field_ValueChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _radiusCustom2Field_ValueChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _showCenterPointCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _showProjectilePointCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		private void _showFrameNumbersCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		#endregion Ereignishandler

		#region Hilfsfunktionen

		/// <summary>
		/// Zeichnet einen viereckigen oder runden Tile-Bereich um den aktuellen Mittelpunkt.
		/// </summary>
		/// <param name="size1">Die Höhe des Bereichs.</param>
		/// <param name="size2">Die Breite des Bereichs.</param>
		/// <param name="color">Die Farbe des Rahmens.</param>
		/// <param name="round">Gibt an, ob der Rahmen rund oder eckig sein soll.</param>
		/// <returns></returns>
		private void DrawBorder(float size1, float size2, Color color, bool round)
		{
			// Rund?
			if(round)
			{
				// Radien berechnen
				const double step = 2 * Math.PI / 360;
				double radX = size2 * TILE_HORIZONTAL_OFFSET;
				double radY = size1 * TILE_VERTICAL_OFFSET;

				// Zeichnen
				GL.Color3(color);
				GL.Begin(PrimitiveType.LineLoop);
				for(double i = 0; i < 2 * Math.PI; i += step)
					GL.Vertex2(_zoom * Math.Cos(i) * radX, _zoom * Math.Sin(i) * radY);
				GL.End();
			}
			else
			{
				// Seitenpunkte berechnen
				float topX = (float)((size2 - size1) * TILE_HORIZONTAL_OFFSET);
				float topY = (float)((size2 + size1) * TILE_VERTICAL_OFFSET);
				float leftX = (float)(-(size1 + size2) * TILE_HORIZONTAL_OFFSET);
				float leftY = (float)((size1 - size2) * TILE_VERTICAL_OFFSET);

				// Zeichnen
				GL.Color3(color);
				GL.Begin(PrimitiveType.LineLoop);
				{
					GL.Vertex2(_zoom * leftX, _zoom * leftY); // Links
					GL.Vertex2(_zoom * topX, _zoom * topY); // Oben
					GL.Vertex2(-_zoom * leftX, -_zoom * leftY); // Rechts
					GL.Vertex2(-_zoom * topX, -_zoom * topY); // Unten
				}
				GL.End();
			}
		}

		/// <summary>
		/// Berechnet die nächsthöhere (oder gleiche) Zweierpotenz zum gegebenen Wert.
		/// </summary>
		/// <param name="value">Der Wert, zu dem die nächsthöhere Zweierpotenz gefunden werden soll.</param>
		/// <returns></returns>
		private uint NextPowerOfTwo(uint value)
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

		/// <summary>
		/// Berechnet den aufgerundeten ganzzahligen Logarithmus der angegebenen Zahl zur Basis 2.
		/// </summary>
		/// <param name="value">Die Zahl deren Logarithmus bestimmt werden soll.</param>
		/// <returns></returns>
		private int Log2(int value)
		{
			// Abgerundeten Logarithmus berechnen
			int log = 0;
			int temp = value;
			while((temp >>= 1) > 0)
				++log;

			// Ggf. aufrunden
			if((1 << log) < value)
				++log;

			// Fertgi
			return log;
		}

		/// <summary>
		/// Zeichnet die gegebene Zeichenfolge an die gegebene Position. Dabei wird die obere linke Ecke angegeben.
		/// Die Abmessungen betragen 8x13 Pixel für das erste und dann 6x13 Pixel pro Zeichen; dies kann auch mit der MeasureString-Funktion bestimmt werden.
		/// </summary>
		/// <param name="value">Die zu zeichende Zeichenfolge.</param>
		/// <param name="x">Die X-Position, an die die Zeichenfolge gezeichnet werden soll.</param>
		/// <param name="y">Die Y-Position, an die die Zeichenfolge gezeichnet werden soll.</param>
		private void DrawString(string value, int x, int y)
		{
			// Textur laden
			GL.BindTexture(TextureTarget.Texture2D, _charTextureID);

			// String zeichenweise durchlaufen
			foreach(char c in value.ToCharArray())
			{
				// Zeichnen
				int cTrans = (int)c - 32;
				float charPosX = (cTrans & 31) / 32.0f;
				float charPosY = (cTrans >> 5) / 10.0f;
				GL.Begin(PrimitiveType.Quads);
				{
					GL.TexCoord2(charPosX, charPosY); GL.Vertex2(x, y); // Oben links
					GL.TexCoord2(charPosX + 1 / 32.0f, charPosY); GL.Vertex2(x + 8, y); // Oben rechts
					GL.TexCoord2(charPosX + 1 / 32.0f, charPosY + 1 / 9.84615f); GL.Vertex2(x + 8, y + 13); // Unten rechts
					GL.TexCoord2(charPosX, charPosY + 1 / 10.0f); GL.Vertex2(x, y + 13); // Unten links
				}
				GL.End();

				// X-Position erhöhen
				x += 6;
			}

			// Texture entladen
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		/// <summary>
		/// Berechnet die Abmessungen des gegebenen Strings.
		/// </summary>
		/// <param name="value">Die Zeichenfolge, deren Abmessungen berechnet werden sollen.</param>
		private Size MeasureString(string value)
		{
			// Größe berechnen
			return new Size((value.Length > 0 ? 8 : 0) + (value.Length - 1) * 6, 13);
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
			public float BaseFrameTime { get; set; } = 100.0f;

			/// <summary>
			/// Der Faktor, dessen Inverses auf die Darstellungszeit eines Frames angewandt werden soll.
			/// </summary>
			private float _speedMultiplier = 1.0f;

			/// <summary>
			/// Der Name bzw. Bezeichner der Animation. Sollte maximal 21 Zeichen lang sein.
			/// </summary>
			private string _name = "ANIM";

			/// <summary>
			/// Die Textur-ID der Animation.
			/// </summary>
			public int TextureID { get; set; }

			/// <summary>
			/// Die Abmessungen der Animations-Textur (2er-Potenz).
			/// </summary>
			public Size TextureBounds { get; set; }

			/// <summary>
			/// Die Anzahl der Frames der Animation. Muss ein Vielfaches von AngleCount sein.
			/// </summary>
			public int FrameCount { get; set; }

			/// <summary>
			/// Die Anzahl der Frames in der Animations-Textur pro Zeile.
			/// </summary>
			public int FramesPerLine { get; set; }

			/// <summary>
			/// Der aktuell angezeigte Frame der Animation.
			/// </summary>
			public int FrameID { get; set; }

			/// <summary>
			/// Die Abmessungen der einzelnen Animations-Frames (alle gleich groß).
			/// </summary>
			public Size FrameBounds { get; set; }

			/// <summary>
			/// Die Anzahl der in der Textur vorgerenderten Animationsachsen.
			/// </summary>
			public int AngleCount { get; set; } = 5;

			/// <summary>
			/// Der Versatz beim Rendern.
			/// </summary>
			public Point RenderOffset { get; set; }

			/// <summary>
			/// Der interne Thread, der durch stetiges Ändern der Frame-ID die Animation realisiert.
			/// </summary>
			private System.Timers.Timer _animateTimer;

			/// <summary>
			/// Die aktuelle Animationsachse.
			/// </summary>
			private int _currentAngle = 0;

			#endregion

			#region Eigenschaften

			/// <summary>
			/// Der Faktor, dessen Inverses auf die Darstellungszeit eines Frames angewandt werden soll.
			/// </summary>
			public float SpeedMultiplier
			{
				get { return _speedMultiplier; }
				set
				{
					// Neuen Faktor speichern
					_speedMultiplier = value;

					// Timer-Intervall aktualisieren
					if(BaseFrameTime != 0 && SpeedMultiplier != 0)
						_animateTimer.Interval = BaseFrameTime / SpeedMultiplier;
				}
			}

			/// <summary>
			/// Die aktuelle Animationsachse.
			/// </summary>
			public int CurrentAngle
			{
				get { return _currentAngle; }
				set
				{
					// Wert speichern
					if(value < AngleCount)
						_currentAngle = value;

					// Frame-ID aktualisieren
					FrameID = _currentAngle * (FrameCount / AngleCount);
				}
			}

			/// <summary>
			/// Der Name bzw. Bezeichner der Animation. Sollte maximal 21 Zeichen lang sein, wird ggf. gekürzt.
			/// </summary>
			public string Name
			{
				get
				{
					// Name zurückgeben
					return _name;
				}
				set
				{
					// Null ist verboten
					if(value == null)
						return;

					// Neuen Namen kürzen, ggf. etwaige Nullbytes am Ende entfernen
					_name = value.TrimEnd('\0');
					if(_name.Length > 21)
						_name = _name.Substring(0, 21);
				}
			}

			#endregion

			#region Funktionen

			/// <summary>
			/// Konstruktor. Erstellt eine neue Animation.
			/// </summary>
			public Animation()
			{
				// Timer erstellen
				_animateTimer = new System.Timers.Timer();
				_animateTimer.AutoReset = false;
				_animateTimer.Elapsed += new System.Timers.ElapsedEventHandler(AnimateTimerTick);
			}

			/// <summary>
			/// Startet die Animation.
			/// </summary>
			public void StartAnimation()
			{
				// Das Intervall sollte größer als 0 sein
				if(BaseFrameTime != 0 && SpeedMultiplier != 0)
				{
					// Timer starten
					_animateTimer.Interval = BaseFrameTime / SpeedMultiplier;
					_animateTimer.Start();
				}
			}

			/// <summary>
			/// Stoppt die Animation.
			/// </summary>
			public void StopAnimation()
			{
				// Timer stoppen
				_animateTimer.Stop();
			}

			/// <summary>
			/// Führt bei Auslösen durch den Animationstimer die Animation um einen Schritt fort.
			/// </summary>
			public void AnimateTimerTick(object sender, System.Timers.ElapsedEventArgs e)
			{
				// Ändert sich durch Inkrementieren der Frame-ID die Achse?
				if(FrameID + 1 >= (_currentAngle + 1) * (FrameCount / AngleCount))
					FrameID = _currentAngle * (FrameCount / AngleCount);
				else
					++FrameID;

				// Nächstes Intervall
				_animateTimer.Start();
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
