﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Data;
using System.Linq;
using System.Text;
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
		internal const int BORDER_WIDTH = 5;

		/// <summary>
		/// Die Seitenlängen der einzelnen Techtree-Kästchen.
		/// </summary>
		internal const int BOX_BOUNDS = 64;

		/// <summary>
		/// Der horizontale Platz um die einzelnen Techtree-Kästchen.
		/// </summary>
		internal const int BOX_SPACE_HORI = 4;

		/// <summary>
		/// Der vertikale Platz um die einzelnen Techtree-Kästchen.
		/// </summary>
		internal const int BOX_SPACE_VERT = 8;

		/// <summary>
		/// Der Abstand der Zeichnung vom Zeichenfeld-Rand.
		/// </summary>
		internal const int DRAW_PANEL_PADDING = 17;

		/// <summary>
		/// Der Pixel-Scroll-Faktor der Zeichenfeld-Scrollleiste.
		/// </summary>
		private const int DRAW_PANEL_SCROLL_MULT = 1;

		/// <summary>
		/// Die Hälfte der Breite einer Pfeilspitze.
		/// </summary>
		internal const int ARROW_WIDTH_HALF = 5;

		/// <summary>
		/// Die Länge einer Pfeilspitze.
		/// </summary>
		internal const int ARROW_LENGTH = 10;

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
		private List<TechTreeStructure.TechTreeElement> _techTreeParentElements = new List<TechTreeStructure.TechTreeElement>();

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
		}

		/// <summary>
		/// Aktualisiert den internen Baum mit den übergebenen Daten und zeichnet diesen.
		/// </summary>
		/// <param name="techTreeParentElements">Die Technologiebaum-Elternelemente.</param>
		public void UpdateTreeData(List<TechTreeStructure.TechTreeElement> techTreeParentElements)
		{
			// Elternelemente speichern
			_techTreeParentElements = techTreeParentElements;

			// Baumgröße berechnen
			List<int> ageCounts = new List<int>() { 1, 1, 1, 1, 1 }; // TODO Hardcoded für 5 Zeitalter!
			List<int> tempAgeCounts = new List<int>() { 0, 0, 0, 0, 0 };
			List<int> childAgeCounts = null;
			int fullTreeWidth = 0;
			_techTreeParentElements.ForEach(elem =>
			{
				// Größe rekursiv berechnen
				childAgeCounts = new List<int>(tempAgeCounts);
				elem.CalculateTreeBounds(ref childAgeCounts);
				fullTreeWidth += elem.TreeWidth;

				// Zurückgebene Zeitalter-Werte abgleichen => es wird immer das Maximum pro Zeitalter genommen
				ageCounts = ageCounts.Zip(childAgeCounts, (a1, a2) => Math.Max(a1, a2)).ToList();
			});

			// Scrollbar einstellen
			_drawPanelScrollBar.Maximum = (Math.Max(fullTreeWidth * (BOX_BOUNDS + 2 * BOX_SPACE_HORI) + 2 * DRAW_PANEL_PADDING, _drawPanel.Width) - _drawPanel.Width) / DRAW_PANEL_SCROLL_MULT;

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

			// Fertig, neuzeichnen
			_dataLoaded = true;
			_drawPanel.Invalidate();
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
			int texID;
			GL.GenTextures(1, out texID);
			GL.BindTexture(TextureTarget.Texture2D, texID);

			// Parameter setzen
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			// Das resultierende Techtree-Kästchen-Bild
			Bitmap box = new Bitmap(BOX_BOUNDS, BOX_BOUNDS);
			Graphics boxG = Graphics.FromImage(box);

			// Bitmapdaten laden
			Bitmap icon = null;
			if(iconID >= 0)
			{
				switch(type)
				{
					case "TechTreeResearch":
						if(_iconsResearches.FrameCount > iconID)
						{
							icon = _iconsResearches.getFrameAsBitmap((uint)iconID, _pal50500);
							boxG.FillRectangle(new SolidBrush(Color.FromArgb(125, 202, 98)), 0, 0, BOX_BOUNDS, BOX_BOUNDS);
						}
						break;

					case "TechTreeCreatable":
						if(_iconsUnits.FrameCount > iconID)
						{
							icon = _iconsUnits.getFrameAsBitmap((uint)iconID, _pal50500);
							boxG.FillRectangle(new SolidBrush(Color.FromArgb(128, 187, 226)), 0, 0, BOX_BOUNDS, BOX_BOUNDS);
						}
						break;

					case "TechTreeBuilding":
						if(_iconsBuildings.FrameCount > iconID)
						{
							icon = _iconsBuildings.getFrameAsBitmap((uint)iconID, _pal50500);
							boxG.FillRectangle(new SolidBrush(Color.FromArgb(246, 128, 128)), 0, 0, BOX_BOUNDS, BOX_BOUNDS);
						}
						break;
				}
			}

			// Ggf. Dummy-Icon einsetzen
			if(icon == null)
			{
				icon = new Bitmap(36, 36);
				Graphics.FromImage(icon).Clear(Color.Yellow);
				boxG.FillRectangle(new SolidBrush(Color.OrangeRed), 0, 0, BOX_BOUNDS, BOX_BOUNDS);
			}

			// Icon-Bild mittig auf Kästchen zeichnen
			boxG.DrawImage(icon, new Point((BOX_BOUNDS - icon.Width) / 2, (BOX_BOUNDS - icon.Height) / 2));

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
			}
			GL.End();

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
				foreach(TechTreeStructure.TechTreeElement parent in _techTreeParentElements)
				{
					// Element zeichnen
					parent.Draw(currPos, _ageOffsets, 0);

					// Position um die Breite verschieben
					currPos.X += parent.TreeWidth * (BOX_BOUNDS + 2 * BOX_SPACE_HORI);
				}

				// Ggf. Abhängigkeitspfeile des ausgewählten Elements zeichnen
				if(_selectedElement != null)
					_selectedElement.DrawDependencies();
			}

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

				// Gesamt-Baumbreite erneut ermitteln
				int fullTreeWidth = 0;
				_techTreeParentElements.ForEach(elem =>
				{
					fullTreeWidth += elem.TreeWidth;
				});

				// Scrollbar einstellen
				_drawPanelScrollBar.Maximum = (Math.Max(fullTreeWidth * (BOX_BOUNDS + 2 * BOX_SPACE_HORI) + 2 * DRAW_PANEL_PADDING, _drawPanel.Width) - _drawPanel.Width) / DRAW_PANEL_SCROLL_MULT;

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
			// Wurde die Auswahl geändert?
			if(_hoverElement != _selectedElement)
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

				// Neuzeichnen
				_drawPanel.Invalidate();
			}

			// Mausklickereignis weiterreichen
			OnMouseClick(e);
		}

		private void _drawPanel_MouseMove(object sender, MouseEventArgs e)
		{
			// Überfahrenes Element herausfinden
			TechTreeStructure.TechTreeElement hovered = null;
			foreach(TechTreeStructure.TechTreeElement elem in _techTreeParentElements)
			{
				if((hovered = elem.FindBox(new Point(e.X + _drawPanelScrollBar.Value * DRAW_PANEL_SCROLL_MULT, e.Y))) != null)
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

		#endregion

		#region Statische Hilfsfunktionen

		/// <summary>
		/// Zeichnet einen Pfeil vom gegebenen Quellelement zum Zielelement in der gegebenen Farbe.
		/// </summary>
		/// <param name="sourceElement">Das Quellelement.</param>
		/// <param name="destinationElement">Das Zielelement.</param>
		/// <param name="color">Die Farbe des Pfeils.</param>
		/// <param name="stippleLine">Optional. Gibt an, ob die gezeichnete Linie gestrichelt sein soll. Standardmäßig false.</param>
		internal static void DrawArrow(TechTreeStructure.TechTreeElement sourceElement, TechTreeStructure.TechTreeElement destinationElement, Color color, bool stippleLine = false)
		{
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

		#endregion

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

		#endregion

		#endregion
	}
}
