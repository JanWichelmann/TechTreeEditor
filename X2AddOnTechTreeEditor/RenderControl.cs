using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace X2AddOnTechTreeEditor
{
	public partial class RenderControl : UserControl
	{
		#region Konstanten

		/// <summary>
		/// Die Standard-Hintergrundfarbe.
		/// </summary>
		private Color COLOR_BACKGROUND = Color.FromArgb(243, 233, 212);

		/// <summary>
		/// Die Standard-Hintergrundfarbe, leicht abgedunkelt.
		/// </summary>
		private Color COLOR_BACKGROUND_DARK = Color.FromArgb(220, 210, 189);

		/// <summary>
		/// Die Standard-Rahmenfarbe.
		/// </summary>
		private Color COLOR_BORDER = Color.FromArgb(87, 74, 43);

		/// <summary>
		/// Die Rahmenbreite.
		/// </summary>
		public const int BORDER_WIDTH = 3;

		/// <summary>
		/// Die Seitenlängen der einzelnen Techtree-Kästchen.
		/// </summary>
		public const int BOX_BOUNDS = 64;

		/// <summary>
		/// Der horizontale Platz um die einzelnen Techtree-Kästchen.
		/// </summary>
		public const int BOX_SPACE_HORI = 4;

		/// <summary>
		/// Der vertikale Platz um die einzelnen Techtree-Kästchen.
		/// </summary>
		public const int BOX_SPACE_VERT = 8;

		/// <summary>
		/// Die Seitenlängen der Icons in den Techtree-Kästchen.
		/// </summary>
		public const int ICON_BOUNDS = 36;

		/// <summary>
		/// Der Abstand der Zeichnung vom Zeichenfeld-Rand.
		/// </summary>
		public const int DRAW_PANEL_PADDING = 17;

		/// <summary>
		/// Der Pixel-Scroll-Faktor der Zeichenfeld-Scrollleiste.
		/// </summary>
		private const int DRAW_PANEL_SCROLL_MULT = 1;

		/// <summary>
		/// Die Hälfte der Breite einer Pfeilspitze.
		/// </summary>
		public const int ARROW_WIDTH_HALF = 5;

		/// <summary>
		/// Die Länge einer Pfeilspitze.
		/// </summary>
		public const int ARROW_LENGTH = 10;

		#endregion Konstanten

		#region Variablen

		/// <summary>
		/// Gibt an, ob die OpenGL-Zeichenfläche bereits initialisiert wurde.
		/// </summary>
		private bool _glLoaded = false;

		/// <summary>
		/// Gibt an, ob alle nötigen Daten für die Anzeige bereits geladen wurden.
		/// </summary>
		private bool _dataLoaded = false;

		/// <summary>
		/// Die Techtree-Elternelemente.
		/// </summary>
		private Dictionary<TechTreeStructure.TechTreeElement, bool> _techTreeParentElements = new Dictionary<TechTreeStructure.TechTreeElement, bool>();

		/// <summary>
		/// Die 0-basierten Element-Versatzwerte der einzelnen Zeitalter. Das bedeutet, dass ein Element von Zeitalter i mindestens um offsets[i] vertikal versetzt wird.
		/// </summary>
		private List<int> _ageOffsets = new List<int>();

		/// <summary>
		/// Die 50500er-Palette.
		/// </summary>
		private BMPLoaderNew.ColorTable _pal50500 = null;

		/// <summary>
		/// Die Technologie-Icon-SLP.
		/// </summary>
		private SLPLoader.Loader _iconsResearches = null;

		/// <summary>
		/// Die Einheit-Icon-SLP.
		/// </summary>
		private SLPLoader.Loader _iconsUnits = null;

		/// <summary>
		/// Die Gebäude-Icon-SLP.
		/// </summary>
		private SLPLoader.Loader _iconsBuildings = null;

		/// <summary>
		/// Das aktuell ausgewählte Techtree-Element.
		/// </summary>
		private TechTreeStructure.TechTreeElement _selectedElement = null;

		/// <summary>
		/// Das aktuell mit der Maus überfahrene Techtree-Element.
		/// </summary>
		private TechTreeStructure.TechTreeElement _hoverElement = null;

		/// <summary>
		/// Die Zeichen-Textur.
		/// </summary>
		private static int _charTextureID = 0;

		/// <summary>
		/// Die berechnete Gesamt-Baumbreite.
		/// </summary>
		private int _fullTreeWidth = 0;

		/// <summary>
		/// Legt fest, ob nur Standard-Elemente gezeichnet werden sollen.
		/// </summary>
		private bool _drawOnlyStandardElements = false;

		/// <summary>
		/// Enthält die Flag-Texturen.
		/// </summary>
		private static Dictionary<TechTreeStructure.TechTreeElement.ElementFlags, int> _flagTextures = null;

		/// <summary>
		/// Der aktuelle Suchtext.
		/// </summary>
		string _currentSearchText = "";

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public RenderControl()
		{
			// Steuerelemente laden
			InitializeComponent();

			// Dem Zeichenfeld das Mausrad-Ereignis zuweisen und an die Scrollleiste weitergeben
			_drawPanel.MouseWheel += new MouseEventHandler((sender, e) =>
			{
				// Scrollleiste scrollen
				int newVal = _drawPanelScrollBar.Value + (Control.ModifierKeys == Keys.Shift ? (e.Delta / 12) : (e.Delta / 120));
				_drawPanelScrollBar.Value = (newVal < 0 ? 0 : (newVal > _drawPanelScrollBar.Maximum ? _drawPanelScrollBar.Maximum : newVal));
			});
		}

		/// <summary>
		/// Aktualisiert die internen Icon-Ressourcen mit den übergebenen.
		/// </summary>
		/// <param name="pal50500">Die 50500er-Palette.</param>
		/// <param name="iconsResearches">Die Technologie-Icon-SLP.</param>
		/// <param name="iconsUnits">Die Einheit-Icon-SLP.</param>
		/// <param name="iconsBuildings">Die Gebäude-Icon-SLP.</param>
		public void UpdateIconData(BMPLoaderNew.ColorTable pal50500, SLPLoader.Loader iconsResearches, SLPLoader.Loader iconsUnits, SLPLoader.Loader iconsBuildings)
		{
			// Parameter speichern
			_pal50500 = pal50500;
			_iconsResearches = iconsResearches;
			_iconsUnits = iconsUnits;
			_iconsBuildings = iconsBuildings;

			// Neu zeichnen
			_drawPanel.Invalidate();
		}

		/// <summary>
		/// Aktualisiert den internen Baum mit den übergebenen Daten und zeichnet diesen.
		/// </summary>
		/// <param name="techTreeParentElements">Die Technologiebaum-Elternelemente.</param>
		public void UpdateTreeData(List<TechTreeStructure.TechTreeElement> techTreeParentElements)
		{
			// Elternelemente speichern
			_techTreeParentElements = techTreeParentElements.ToDictionary(p => p, p => true);

			// Baumgröße berechnen
			List<int> ageCounts = new List<int>() { 1, 1, 1, 1, 1 }; // TODO Hardcoded für 5 Zeitalter!
			List<int> tempAgeCounts = new List<int>() { 0, 0, 0, 0, 0 };
			List<int> childAgeCounts = null;
			_fullTreeWidth = 0;
			foreach(var elem in _techTreeParentElements)
			{
				// Größe rekursiv berechnen
				childAgeCounts = new List<int>(tempAgeCounts);
				elem.Key.CalculateTreeBounds(ref childAgeCounts);
				_fullTreeWidth += elem.Key.TreeWidth;

				// Zurückgebene Zeitalter-Werte abgleichen => es wird immer das Maximum pro Zeitalter genommen
				ageCounts = ageCounts.Zip(childAgeCounts, (a1, a2) => Math.Max(a1, a2)).ToList();
			}

			// Scrollbar einstellen
			_drawPanelScrollBar.Maximum = (Math.Max(_fullTreeWidth * (BOX_BOUNDS + 2 * BOX_SPACE_HORI) + 2 * DRAW_PANEL_PADDING, _drawPanel.Width) - _drawPanel.Width) / DRAW_PANEL_SCROLL_MULT;

			// Zeitalter-Offsets erstellen
			int currOffset = 0;
			int tempOffset = 0;
			_ageOffsets = ageCounts.Select(c =>
			{
				// Post-Inkrement auf currOffset
				tempOffset = currOffset;
				currOffset += c;
				return tempOffset;
			}).ToList();

			// Filter erneut anwenden
			ApplyFilters();

			// Fertig, neuzeichnen
			_dataLoaded = true;
			_drawPanel.Invalidate();
		}

		/// <summary>
		/// Aktualisiert den aktuellen Suchtext.
		/// </summary>
		/// <param name="search">Der neue Suchtext.</param>
		public void UpdateSearchText(string search)
		{
			// Suchtext merken
			_currentSearchText = search.ToLower();

			// Filter neu anwenden
			ApplyFilters();

			// Neuzeichnen
			_drawPanel.Invalidate();
		}

		/// <summary>
		/// Wendet die gespeicherten Filter auf die Baumelternelemente an.
		/// </summary>
		private void ApplyFilters()
		{
			// Pro Element Filter ausführen
			List<TechTreeStructure.TechTreeElement> parentElements = new List<TechTreeStructure.TechTreeElement>(_techTreeParentElements.Keys);
			foreach(var elem in parentElements)
			{
				// Element wird prinzipiell erstmal angezeigt
				bool newVal = true;

				// Standardelement?
				if(_drawOnlyStandardElements)
					if(elem.GetType() == typeof(TechTreeStructure.TechTreeBuilding))
						newVal = ((TechTreeStructure.TechTreeBuilding)elem).StandardElement;
					else if(elem.GetType() == typeof(TechTreeStructure.TechTreeCreatable))
						newVal = ((TechTreeStructure.TechTreeCreatable)elem).StandardElement;
					else if(elem.GetType() != typeof(TechTreeStructure.TechTreeResearch)) // Für Technologien eine Ausnahme, sind ja strenggenommen Standardelemente...
						newVal = false;

				// Suchstring angegeben?
				if(newVal && !string.IsNullOrWhiteSpace(_currentSearchText))
					newVal = elem.HasChildWithName(_currentSearchText);

				// Anzeigewert merken
				_techTreeParentElements[elem] = newVal;
			}
		}

		/// <summary>
		/// Initialisiert OpenGL und die Zeichenfläche.
		/// </summary>
		private void InitDrawPanel()
		{
			// Panel leeren
			GL.ClearColor(COLOR_BACKGROUND);

			// Texturen aktivieren
			GL.Enable(EnableCap.Texture2D);

			// 2D-Grafik benötigt keine Tiefenerkennung
			GL.Disable(EnableCap.DepthTest);

			// Anti-Aliasing einschalten
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.PointSmooth);
			GL.Enable(EnableCap.LineSmooth);

			// Gestrichelte Linien konfigurieren (werden nach Bedarf aktiviert/deaktiviert)
			GL.LineStipple(1, 0xCCCC);

			// Blickwinkel erstellen
			SetupDrawPanelViewPort();

			// Zeichen-Texturen für String-Rendering erstellen
			{
				// Bitmap laden
				Bitmap charTexBitmap = X2AddOnTechTreeEditor.Properties.Resources.RenderFontConsolas;

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

			// Flag-Texturen erstellen
			{
				// Flag-Liste initialisieren
				_flagTextures = new Dictionary<TechTreeStructure.TechTreeElement.ElementFlags, int>();

				// Allgemeines Hilfs-Bitmap
				Bitmap flagBitmap = new Bitmap(BOX_BOUNDS, BOX_BOUNDS);
				BitmapData flagBitmapData;
				Graphics flagBitmapGraphics = Graphics.FromImage(flagBitmap);

				// Hilfsvariablen
				int newTexID = 0;
				Rectangle boxBounds = new Rectangle(0, 0, BOX_BOUNDS, BOX_BOUNDS);

				// Editor-Flag
				flagBitmapGraphics.Clear(Color.Transparent);
				flagBitmapGraphics.DrawImage(X2AddOnTechTreeEditor.Icons.MapEditor, 1, 50, (BOX_BOUNDS - ICON_BOUNDS) / 2, (BOX_BOUNDS - ICON_BOUNDS) / 2);
				newTexID = GL.GenTexture();
				_flagTextures[TechTreeStructure.TechTreeElement.ElementFlags.ShowInEditor] = newTexID;
				GL.BindTexture(TextureTarget.Texture2D, newTexID);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				flagBitmapData = flagBitmap.LockBits(boxBounds, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, BOX_BOUNDS, BOX_BOUNDS, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, flagBitmapData.Scan0);
				flagBitmap.UnlockBits(flagBitmapData);

				// Gaia-Only-Flag
				flagBitmapGraphics.Clear(Color.Transparent);
				flagBitmapGraphics.DrawImage(X2AddOnTechTreeEditor.Icons.Gaia, 32, 50, (BOX_BOUNDS - ICON_BOUNDS) / 2, (BOX_BOUNDS - ICON_BOUNDS) / 2);
				newTexID = GL.GenTexture();
				_flagTextures[TechTreeStructure.TechTreeElement.ElementFlags.GaiaOnly] = newTexID;
				GL.BindTexture(TextureTarget.Texture2D, newTexID);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				flagBitmapData = flagBitmap.LockBits(boxBounds, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, BOX_BOUNDS, BOX_BOUNDS, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, flagBitmapData.Scan0);
				flagBitmap.UnlockBits(flagBitmapData);

				// Lock-ID-Flag
				flagBitmapGraphics.Clear(Color.Transparent);
				flagBitmapGraphics.DrawImage(X2AddOnTechTreeEditor.Icons.Lock, 48, 50, (BOX_BOUNDS - ICON_BOUNDS) / 2, (BOX_BOUNDS - ICON_BOUNDS) / 2);
				newTexID = GL.GenTexture();
				_flagTextures[TechTreeStructure.TechTreeElement.ElementFlags.LockID] = newTexID;
				GL.BindTexture(TextureTarget.Texture2D, newTexID);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				flagBitmapData = flagBitmap.LockBits(boxBounds, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, BOX_BOUNDS, BOX_BOUNDS, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, flagBitmapData.Scan0);
				flagBitmap.UnlockBits(flagBitmapData);

				// Blocked-Flag
				flagBitmapGraphics.Clear(Color.Transparent);
				flagBitmapGraphics.DrawImage(X2AddOnTechTreeEditor.Icons.BlockedTexture, 0, 0, BOX_BOUNDS, BOX_BOUNDS);
				newTexID = GL.GenTexture();
				_flagTextures[TechTreeStructure.TechTreeElement.ElementFlags.Blocked] = newTexID;
				GL.BindTexture(TextureTarget.Texture2D, newTexID);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				flagBitmapData = flagBitmap.LockBits(boxBounds, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, BOX_BOUNDS, BOX_BOUNDS, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, flagBitmapData.Scan0);
				flagBitmap.UnlockBits(flagBitmapData);

				// Free-Flag
				flagBitmapGraphics.Clear(Color.Transparent);
				flagBitmapGraphics.DrawImage(X2AddOnTechTreeEditor.Icons.Free, 16, 50, (BOX_BOUNDS - ICON_BOUNDS) / 2, (BOX_BOUNDS - ICON_BOUNDS) / 2);
				newTexID = GL.GenTexture();
				_flagTextures[TechTreeStructure.TechTreeElement.ElementFlags.Free] = newTexID;
				GL.BindTexture(TextureTarget.Texture2D, newTexID);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				flagBitmapData = flagBitmap.LockBits(boxBounds, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, BOX_BOUNDS, BOX_BOUNDS, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, flagBitmapData.Scan0);
				flagBitmap.UnlockBits(flagBitmapData);
			}

			// Alles ist geladen
			_glLoaded = true;
		}

		/// <summary>
		/// Erstellt die Koordinaten und Blickwinkel der Zeichenfläche.
		/// </summary>
		private void SetupDrawPanelViewPort()
		{
			// Blickwinkel erstellen
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, _drawPanel.Width, _drawPanel.Height, 0, -1, 1); // Pixel oben links ist (0, 0)
			GL.Viewport(0, 0, _drawPanel.Width, _drawPanel.Height);

			// Zeichenmodus laden
			GL.MatrixMode(MatrixMode.Modelview);
		}

		/// <summary>
		/// Lädt das angegebene Icon für den angegebenen Objekttyp und übergibt es OpenGL als Box-Textur, die Textur-ID wird anschließend zurückgegeben.
		/// </summary>
		/// <param name="type">Der Typ des Objekts, dem das Icon zugeordnet ist. Dies ist einfach der Klassenname des Objekts.</param>
		/// <param name="iconID">Die ID des Icons.</param>
		/// <returns></returns>
		public int LoadIconAsTexture(string type, short iconID)
		{
			// Texturobjekt vorbereiten
			int texID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, texID);

			// Parameter setzen
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			// Das resultierende Techtree-Kästchen-Bild
			Bitmap box = new Bitmap(BOX_BOUNDS, BOX_BOUNDS);
			Graphics boxG = Graphics.FromImage(box);

			// Bitmapdaten laden
			Bitmap icon = null;
			switch(type)
			{
				case "TechTreeResearch":
					if(iconID >= 0 && _iconsResearches.FrameCount > iconID)
						icon = _iconsResearches.getFrameAsBitmap((uint)iconID, _pal50500);
					else
					{
						icon = new Bitmap(ICON_BOUNDS, ICON_BOUNDS);
						Graphics iconG = Graphics.FromImage(icon);
						iconG.Clear(Color.FromArgb(220, 220, 220));
						iconG.DrawImage(Icons.ResearchIcon, 2, 2, 32, 32);
					}
					boxG.Clear(Color.FromArgb(125, 202, 98));
					break;

				case "TechTreeCreatable":
					if(iconID >= 0 && _iconsUnits.FrameCount > iconID)
						icon = _iconsUnits.getFrameAsBitmap((uint)iconID, _pal50500);
					else
					{
						icon = new Bitmap(ICON_BOUNDS, ICON_BOUNDS);
						Graphics iconG = Graphics.FromImage(icon);
						iconG.Clear(Color.FromArgb(220, 220, 220));
						iconG.DrawImage(Icons.UnitIcon, 2, 2, 32, 32);
					}
					boxG.Clear(Color.FromArgb(128, 187, 226));
					break;

				case "TechTreeBuilding":
					if(iconID >= 0 && _iconsBuildings.FrameCount > iconID)
						icon = _iconsBuildings.getFrameAsBitmap((uint)iconID, _pal50500);
					else
					{
						icon = new Bitmap(ICON_BOUNDS, ICON_BOUNDS);
						Graphics iconG = Graphics.FromImage(icon);
						iconG.Clear(Color.FromArgb(220, 220, 220));
						iconG.DrawImage(Icons.BuildingIcon, 2, 2, 32, 32);
					}
					boxG.Clear(Color.FromArgb(246, 128, 128));
					break;

				case "TechTreeEyeCandy":
					{
						icon = new Bitmap(ICON_BOUNDS, ICON_BOUNDS);
						Graphics iconG = Graphics.FromImage(icon);
						iconG.Clear(Color.FromArgb(220, 220, 220));
						iconG.DrawImage(Icons.EyeCandyIcon, 2, 2, 32, 32);
						boxG.Clear(Color.FromArgb(196, 145, 250));
					}
					break;

				case "TechTreeProjectile":
					{
						icon = new Bitmap(ICON_BOUNDS, ICON_BOUNDS);
						Graphics iconG = Graphics.FromImage(icon);
						iconG.Clear(Color.FromArgb(220, 220, 220));
						iconG.DrawImage(Icons.ProjectileIcon, 2, 2, 32, 32);
						boxG.Clear(Color.FromArgb(250, 174, 132));
					}
					break;

				case "TechTreeDead":
					{
						icon = new Bitmap(ICON_BOUNDS, ICON_BOUNDS);
						Graphics iconG = Graphics.FromImage(icon);
						iconG.Clear(Color.FromArgb(220, 220, 220));
						iconG.DrawImage(Icons.DeadIcon, 2, 2, 32, 32);
						boxG.Clear(Color.LightYellow);
					}
					break;

				default:
					{
						icon = new Bitmap(ICON_BOUNDS, ICON_BOUNDS);
						Graphics iconG = Graphics.FromImage(icon);
						iconG.Clear(Color.FromArgb(220, 220, 220));
						boxG.Clear(Color.Peru);
					}
					break;
			}

			// Icon-Bild mittig auf Kästchen zeichnen
			boxG.DrawImage(icon, new Point((BOX_BOUNDS - ICON_BOUNDS) / 2, (BOX_BOUNDS - ICON_BOUNDS) / 2));

			// Kästchen-Rahmen zeichnen
			boxG.DrawRectangle(Pens.Black, 0, 0, BOX_BOUNDS - 1, BOX_BOUNDS - 1);

			// Bild-Bits sperren
			BitmapData data = box.LockBits(new Rectangle(0, 0, BOX_BOUNDS, BOX_BOUNDS), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			// Textur an OpenGL übergeben
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, BOX_BOUNDS, BOX_BOUNDS, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			// Ressourcen freigeben und aufräumen
			GL.BindTexture(TextureTarget.Texture2D, 0);
			box.UnlockBits(data);
			box.Dispose();
			icon.Dispose();
			
			// Textur-ID zurückgeben
			return texID;
		}

		/// <summary>
		/// Zeichnet den Baum neu.
		/// </summary>
		public void Redraw()
		{
			// Neuzeichnen
			_drawPanel.Invalidate();
		}

		/// <summary>
		/// Setzt den Render-Modus.
		/// </summary>
		/// <param name="onlyStandardElements">Gibt an, ob nur Standardelemente gezeichnet werden sollen.</param>
		public void SetRenderMode(bool renderOnlyStandardElements)
		{
			// Wert merken
			_drawOnlyStandardElements = renderOnlyStandardElements;

			// Filter neu anwenden
			ApplyFilters();

			// Neuzeichnen
			_drawPanel.Invalidate();
		}

		#endregion Funktionen

		#region Ereignishandler

		private void RenderControl_Load(object sender, EventArgs e)
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

			// Zeichenfläche leeren
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Zeichenmatrix zurücksetzen
			GL.LoadIdentity();

			GL.Color3(COLOR_BACKGROUND);
			DrawString("[Render Mode: " + (_drawOnlyStandardElements ? "StandardOnly" : "All") + "]", 6, 6);

			// Wurden alle Daten geladen?
			if(_dataLoaded)
			{
				// Jedes zweite Zeitalter etwas dunkler unterlegen
				const int vertBoxBounds = (2 * BOX_SPACE_VERT + BOX_BOUNDS);
				for(int i = 1; i < 5; i += 2) // TODO: Hardcoded für 5 Zeitalter
				{
					// Hintergrund zeichnen
					GL.Color3(COLOR_BACKGROUND_DARK);
					GL.Begin(PrimitiveType.Quads);
					{
						GL.Vertex2(0, DRAW_PANEL_PADDING + _ageOffsets[i] * vertBoxBounds);
						GL.Vertex2(_drawPanel.Width, DRAW_PANEL_PADDING + _ageOffsets[i] * vertBoxBounds);
						GL.Vertex2(_drawPanel.Width, DRAW_PANEL_PADDING + _ageOffsets[i + 1] * vertBoxBounds);
						GL.Vertex2(0, DRAW_PANEL_PADDING + _ageOffsets[i + 1] * vertBoxBounds);
					}
					GL.End();
				}

				// Zeichnung der ScrollBar entsprechend verschieben
				GL.Translate(-_drawPanelScrollBar.Value * DRAW_PANEL_SCROLL_MULT, 0, 0);

				// Elternelemente zeichnen
				Point currPos = new Point(DRAW_PANEL_PADDING, DRAW_PANEL_PADDING);
				foreach(var parent in _techTreeParentElements)
				{
					// Element angezeigt?
					if(!parent.Value)
						continue;

					// Element zeichnen
					parent.Key.Draw(currPos, _ageOffsets, 0);

					// Position um die Breite verschieben
					currPos.X += parent.Key.TreeWidth * (BOX_BOUNDS + 2 * BOX_SPACE_HORI);
				}

				// Ggf. Abhängigkeitspfeile des ausgewählten Elements zeichnen
				if(_selectedElement != null)
					_selectedElement.DrawDependencies();
			}

			// Zeichenmatrix zurücksetzen
			GL.LoadIdentity();

			// Rahmen zeichnen
			GL.Color3(COLOR_BORDER);
			GL.Begin(PrimitiveType.Quads);
			{
				// Oben
				GL.Vertex2(0, 0);
				GL.Vertex2(_drawPanel.Width, 0);
				GL.Vertex2(_drawPanel.Width, BORDER_WIDTH);
				GL.Vertex2(0, BORDER_WIDTH);

				// Unten
				GL.Vertex2(0, _drawPanel.Height - BORDER_WIDTH);
				GL.Vertex2(_drawPanel.Width, _drawPanel.Height - BORDER_WIDTH);
				GL.Vertex2(_drawPanel.Width, _drawPanel.Height);
				GL.Vertex2(0, _drawPanel.Height);

				// Links
				GL.Vertex2(0, 0);
				GL.Vertex2(BORDER_WIDTH, 0);
				GL.Vertex2(BORDER_WIDTH, _drawPanel.Height);
				GL.Vertex2(0, _drawPanel.Height);

				// Rechts
				GL.Vertex2(_drawPanel.Width - BORDER_WIDTH, 0);
				GL.Vertex2(_drawPanel.Width, 0);
				GL.Vertex2(_drawPanel.Width, _drawPanel.Height);
				GL.Vertex2(_drawPanel.Width - BORDER_WIDTH, _drawPanel.Height);
			}
			GL.End();

			// Puffer tauschen, um Flackern zu vermeiden
			_drawPanel.SwapBuffers();
		}

		private void _drawPanel_Resize(object sender, EventArgs e)
		{
			// Prüfen, ob das Panel überhaupt eine Größe hat (Fenster minimiert?)
			if(_drawPanel.Width != 0 && _drawPanel.Height != 0)
			{
				// Blickwinkel neu laden
				SetupDrawPanelViewPort();

				// Scrollbar einstellen
				_drawPanelScrollBar.Maximum = (Math.Max(_fullTreeWidth * (BOX_BOUNDS + 2 * BOX_SPACE_HORI) + 2 * DRAW_PANEL_PADDING, _drawPanel.Width) - _drawPanel.Width) / DRAW_PANEL_SCROLL_MULT;

				// Neuzeichnen erzwingen
				_drawPanel.Invalidate();
			}
		}

		private void _drawPanelScrollBar_ValueChanged(object sender, EventArgs e)
		{
			// Neuzeichnen erzwingen
			_drawPanel.Invalidate();
		}

		private void _drawPanel_MouseClick(object sender, MouseEventArgs e)
		{
			// Auswahl geändert?
			if(_selectedElement != _hoverElement)
			{
				// Bisher selektiertes Element ggf. deselektieren
				if(_selectedElement != null)
				{
					// Element ist nicht mehr ausgewählt
					_selectedElement.Selected = false;
				}

				// Das selektierte Element ist das aktuelle Hover-Element
				_selectedElement = _hoverElement;
				if(_selectedElement != null)
				{
					// Element selektieren
					_selectedElement.Selected = true;
				}

				// Ereignis weiterreichen
				OnSelectionChanged(new SelectionChangedEventArgs(_selectedElement));
			}

			// Neuzeichnen
			_drawPanel.Invalidate();

			// Mausklickereignis weiterreichen
			OnMouseClick(e);
		}

		private void _drawPanel_MouseMove(object sender, MouseEventArgs e)
		{
			// Überfahrenes Element herausfinden
			TechTreeStructure.TechTreeElement hovered = null;
			foreach(var elem in _techTreeParentElements)
			{
				if(elem.Value && (hovered = elem.Key.FindBox(new Point(e.X + _drawPanelScrollBar.Value * DRAW_PANEL_SCROLL_MULT, e.Y))) != null)
					break;
			}

			// Wurde die Auswahl geändert?
			if(_hoverElement != hovered)
			{
				// Bisher selektiertes Element ggf. deselektieren
				if(_hoverElement != null)
				{
					// Element ist nicht mehr ausgewählt
					_hoverElement.Hovered = false;
				}

				// Neues Element selektieren
				_hoverElement = hovered;

				// Wurde ein neues Element überfahren?
				if(_hoverElement != null)
				{
					// Dem Element mitteilen, dass es überfahren worden ist
					_hoverElement.Hovered = true;
				}

				// Neuzeichnen
				_drawPanel.Invalidate();
			}
		}

		private void _drawPanel_DoubleClick(object sender, EventArgs e)
		{
			// Ereignis weiterreichen
			OnDoubleClick(e);
		}

		#endregion Ereignishandler

		#region Statische Hilfsfunktionen

		/// <summary>
		/// Zeichnet einen Pfeil vom gegebenen Quellelement zum Zielelement in der gegebenen Farbe.
		/// </summary>
		/// <param name="sourceElement">Das Quellelement.</param>
		/// <param name="destinationElement">Das Zielelement.</param>
		/// <param name="color">Die Farbe des Pfeils.</param>
		/// <param name="stippleLine">Optional. Gibt an, ob die gezeichnete Linie gestrichelt sein soll. Standardmäßig false.</param>
		public static void DrawArrow(TechTreeStructure.TechTreeElement sourceElement, TechTreeStructure.TechTreeElement destinationElement, Color color, bool stippleLine = false)
		{
			// Falls das Zielelement Schatten-Element und Gebäude ist, kann es sich um eine Untereinheit handeln
			if(destinationElement.GetType() == typeof(TechTreeStructure.TechTreeBuilding) && destinationElement.ShadowElement)
			{
				// Obergebäude setzen
				if(((TechTreeStructure.TechTreeBuilding)destinationElement).StackUnit != null)
					destinationElement = ((TechTreeStructure.TechTreeBuilding)destinationElement).StackUnit;
			}

			// Richtungsvektor der Pfeillinie berechnen
			Vector2 arrowLine = new Vector2(destinationElement.CacheBoxPosition.X - sourceElement.CacheBoxPosition.X + (sourceElement.CacheBoxPosition.X < destinationElement.CacheBoxPosition.X ? -BOX_BOUNDS : BOX_BOUNDS), destinationElement.CacheBoxPosition.Y - sourceElement.CacheBoxPosition.Y);

			// Stützvektor der Pfeillinie berechnen
			Vector2 arrowOrigin = new Vector2(sourceElement.CacheBoxPosition.X + (sourceElement.CacheBoxPosition.X < destinationElement.CacheBoxPosition.X ? BOX_BOUNDS : 0), sourceElement.CacheBoxPosition.Y + BOX_BOUNDS / 2);

			// Senkrechten Seiten-Vektor für den hinteren Teil der Pfeilspitze ermitteln
			Vector2 arrowHeadSide = arrowLine.PerpendicularRight.Normalized() * ARROW_WIDTH_HALF;

			// Stützvektor für Pfeilspitzenansatz berechnen
			Vector2 arrowHeadOrigin = arrowOrigin + arrowLine - arrowLine.Normalized() * ARROW_LENGTH;

			// Pfeillinie zeichnen (gestrichelt)
			GL.Color3(color);
			if(stippleLine)
				GL.Enable(EnableCap.LineStipple);
			GL.LineWidth(2);
			GL.Begin(PrimitiveType.Lines);
			{
				GL.Vertex2(arrowOrigin);
				GL.Vertex2(arrowHeadOrigin);
			}
			GL.End();
			GL.LineWidth(1);
			if(stippleLine)
				GL.Disable(EnableCap.LineStipple);

			// Pfeilspitze
			GL.Begin(PrimitiveType.Triangles);
			{
				// Seite
				GL.Vertex2(arrowHeadOrigin - arrowHeadSide);

				// Spitze
				GL.Vertex2(arrowOrigin + arrowLine);

				// Seite
				GL.Vertex2(arrowHeadOrigin + arrowHeadSide);
			}
			GL.End();
		}

		/// <summary>
		/// Zeichnet eine Linie vom gegebenen Quellelement zum Zielelement in der gegebenen Farbe.
		/// </summary>
		/// <param name="sourceElement">Das Quellelement.</param>
		/// <param name="destinationElement">Das Zielelement.</param>
		/// <param name="color">Die Farbe der Linie.</param>
		/// <param name="stippleLine">Optional. Gibt an, ob die gezeichnete Linie gestrichelt sein soll. Standardmäßig false.</param>
		public static void DrawLine(TechTreeStructure.TechTreeElement sourceElement, TechTreeStructure.TechTreeElement destinationElement, Color color, bool stippleLine = false)
		{
			// Falls das Zielelement Schatten-Element und Gebäude ist, kann es sich um eine Untereinheit handeln
			if(destinationElement.GetType() == typeof(TechTreeStructure.TechTreeBuilding) && destinationElement.ShadowElement)
			{
				// Obergebäude setzen
				if(((TechTreeStructure.TechTreeBuilding)destinationElement).StackUnit != null)
					destinationElement = ((TechTreeStructure.TechTreeBuilding)destinationElement).StackUnit;
			}

			// Startvektor der Linie berechnen
			Vector2 lineOrigin = new Vector2(sourceElement.CacheBoxPosition.X + (sourceElement.CacheBoxPosition.X < destinationElement.CacheBoxPosition.X ? BOX_BOUNDS : 0), sourceElement.CacheBoxPosition.Y + BOX_BOUNDS / 2);

			// Zielvektor der Linie berechnen
			Vector2 lineDest = new Vector2(destinationElement.CacheBoxPosition.X + (sourceElement.CacheBoxPosition.X < destinationElement.CacheBoxPosition.X ? 0 : BOX_BOUNDS), destinationElement.CacheBoxPosition.Y + BOX_BOUNDS / 2);

			// Linie zeichnen (gestrichelt)
			GL.Color3(color);
			if(stippleLine)
				GL.Enable(EnableCap.LineStipple);
			GL.LineWidth(2);
			GL.Begin(PrimitiveType.Lines);
			{
				GL.Vertex2(lineOrigin);
				GL.Vertex2(lineDest);
			}
			GL.End();
			GL.LineWidth(1);
			if(stippleLine)
				GL.Disable(EnableCap.LineStipple);
		}

		/// <summary>
		/// Zeichnet ein Flag an der aktuellen Box-Position. Es kann immer nur ein Flag auf einmal gezeichnet werden.
		/// </summary>
		/// <param name="flag">Das zu zeichnende Flag.</param>
		public static void DrawFlagOverlay(TechTreeStructure.TechTreeElement.ElementFlags flag)
		{
			// Flag-Textur abrufen
			int flagTexID = _flagTextures[flag];

			// Flag-Textur binden und zeichnen
			GL.BindTexture(TextureTarget.Texture2D, flagTexID);
			GL.Begin(PrimitiveType.Quads);
			{
				GL.TexCoord2(0.0, 0.0); GL.Vertex2(0, 0); // Oben links
				GL.TexCoord2(1.0, 0.0); GL.Vertex2(RenderControl.BOX_BOUNDS, 0); // Oben rechts
				GL.TexCoord2(1.0, 1.0); GL.Vertex2(RenderControl.BOX_BOUNDS, RenderControl.BOX_BOUNDS); // Unten rechts
				GL.TexCoord2(0.0, 1.0); GL.Vertex2(0, RenderControl.BOX_BOUNDS); // Unten links
			}
			GL.End();
		}

		/// <summary>
		/// Zeichnet die gegebene Zeichenfolge an die gegebene Position. Dabei wird die obere linke Ecke angegeben.
		/// Die Abmessungen betragen 8x13 Pixel für das erste und dann 6x13 Pixel pro Zeichen.
		/// </summary>
		/// <param name="value">Die zu zeichende Zeichenfolge.</param>
		/// <param name="position">Die Position, an die die Zeichenfolge gezeichnet werden soll.</param>
		public static void DrawString(string value, Point position)
		{
			// Überladene Funktion aufrufen
			DrawString(value, position.X, position.Y);
		}

		/// <summary>
		/// Zeichnet die gegebene Zeichenfolge an die gegebene Position. Dabei wird die obere linke Ecke angegeben.
		/// Die Abmessungen betragen 8x13 Pixel für das erste und dann 6x13 Pixel pro Zeichen.
		/// </summary>
		/// <param name="value">Die zu zeichende Zeichenfolge.</param>
		/// <param name="x">Die X-Position, an die die Zeichenfolge gezeichnet werden soll.</param>
		/// <param name="y">Die Y-Position, an die die Zeichenfolge gezeichnet werden soll.</param>
		public static void DrawString(string value, int x, int y)
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

		private void _drawPanel_KeyDown(object sender, KeyEventArgs e)
		{
			// Ereignis weiterreichen
			OnKeyDown(e);
		}

		#endregion Statische Hilfsfunktionen

		#region Ereignisse

		#region Event: Geänderte Auswahl

		/// <summary>
		/// Wird ausgelöst, wenn sich die Auswahl ändert.
		/// </summary>
		public event SelectionChangedEventHandler SelectionChanged;

		/// <summary>
		/// Der Handler-Typ für das SelectionChanged-Event.
		/// </summary>
		/// <param name="sender">Das auslösende Objekt.</param>
		/// <param name="e">Die Ereignisdaten.</param>
		public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);

		/// <summary>
		/// Löst das SelectionChanged-Ereignis aus.
		/// </summary>
		/// <param name="e">Die Ereignisdaten.</param>
		protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			SelectionChangedEventHandler handler = SelectionChanged;
			if(handler != null)
				handler(this, e);
		}

		/// <summary>
		/// Die Ereignisdaten für das SelectionChanged-Event.
		/// </summary>
		public class SelectionChangedEventArgs : EventArgs
		{
			/// <summary>
			/// Das ausgewählte Element.
			/// </summary>
			private TechTreeStructure.TechTreeElement _selectedElement;

			/// <summary>
			/// Ruft das ausgewählte Objekt ab.
			/// </summary>
			public TechTreeStructure.TechTreeElement SelectedElement
			{
				get
				{
					return _selectedElement;
				}
			}

			/// <summary>
			/// Konstruktor.
			/// Erstellt ein neues Ereignisdaten-Objekt mit den gegebenen Daten.
			/// </summary>
			/// <param name="selectedElement">Das ausgewählte Element.</param>
			public SelectionChangedEventArgs(TechTreeStructure.TechTreeElement selectedElement)
			{
				// Parameter speichern
				_selectedElement = selectedElement;
			}
		}

		#endregion Event: Geänderte Auswahl

		#endregion Ereignisse
	}
}