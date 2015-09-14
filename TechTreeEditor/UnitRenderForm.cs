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

namespace TechTreeEditor
{
	public partial class UnitRenderForm : Form
	{
		#region Konstanten

		/// <summary>
		/// Die Standard-Hintergrundfarbe.
		/// </summary>
		private Color COLOR_BACKGROUND = Color.FromArgb(243, 233, 212);

		#endregion Konstanten

		#region Variablen

		/// <summary>
		/// Gibt an, ob die OpenGL-Zeichenfläche bereits initialisiert wurde.
		/// </summary>
		private bool _glLoaded = false;

		/// <summary>
		/// Die zugrundeliegenden Projektdaten.
		/// </summary>
		private TechTreeFile _projectFile = null;

		/// <summary>
		/// Die zu rendernde Einheit.
		/// </summary>
		private TechTreeUnit _renderUnit = null;

		/// <summary>
		/// Die Graphics-DRS mit Grafiken.
		/// </summary>
		private DRSFile _graphicsDRS = null;

		#endregion Variablen

		#region Funktionen

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private UnitRenderForm()
		{
			// Steuerelemente laden
			InitializeComponent();
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

			// Anti-Aliasing einschalten
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.PointSmooth);
			GL.Enable(EnableCap.LineSmooth);

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
			// Aktualisieren
			_renderUnit = renderUnit;

			// Neuzeichnen
			_drawPanel.Invalidate();
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
			// Ich bin dran
			_drawPanel.MakeCurrent();

			// Wurde OpenGL schon geladen?
			if(!_glLoaded)
				return;

			// Zeichenfläche leeren
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Zeichenmatrix zurücksetzen
			GL.LoadIdentity();

			// Zeichnen...


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

		#endregion Ereignishandler
	}
}
