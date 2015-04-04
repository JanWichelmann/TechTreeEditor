using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X2AddOnTechTreeEditor
{
	/// <summary>
	/// Hilfsklasse für eine etwas weniger flackernde ListBox.
	/// </summary>
	internal class DoubleBufferedListBox : ListBox
	{
		/// <summary>
		/// Konstruktor. Erstellt eine neue ListBox.
		/// </summary>
		public DoubleBufferedListBox()
		{
			// Double-Buffering aktivieren, manuell zeichnen
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
			this.DrawMode = DrawMode.OwnerDrawFixed;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			// Wenn es Elemente zu zeichnen gibt, diese zeichnen
			if(this.Items.Count > 0)
			{
				// Hintergrund zeichnen
				e.DrawBackground();

				// Elementwert zeichnen
				TextRenderer.DrawText(e.Graphics, this.Items[e.Index].ToString(), e.Font, new Point(e.Bounds.X, e.Bounds.Y), e.ForeColor);
			}
			base.OnDrawItem(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			// Zeichenbereich füllen
			Region region = new Region(e.ClipRectangle);
			e.Graphics.FillRegion(new SolidBrush(this.BackColor), region);

			// Gibt es Elemente?
			if(this.Items.Count > 0)
			{
				// Alle Elemente zeichnen
				for(int i = 0; i < this.Items.Count; ++i)
				{
					// Element-Rechteck abrufen
					System.Drawing.Rectangle rect = this.GetItemRectangle(i);

					// Liegt das Element im Zeichenbereich?
					if(e.ClipRectangle.IntersectsWith(rect))
					{
						// Element ausgewählt?
						if((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
						|| (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
						|| (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
						{
							// Ausgewähltes Element zeichnen
							OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, rect, i, DrawItemState.Selected, this.ForeColor, this.BackColor));
						}
						else
						{
							// Nicht ausgewähltes Element zeichnen
							OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, rect, i, DrawItemState.Default, this.ForeColor, this.BackColor));
						}
					}
				}
			}

			// Zeichenfunktion der Basisklasse aufrufen
			base.OnPaint(e);
		}
	}
}
