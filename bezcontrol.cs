using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace osusb1 {
partial class all
{
	public class Bezcontrol : Panel
	{
		const int HS = 1;
		const int SS = HS * 2 + 1;
		public static float controlpointscale = 4f;

		static List<Bezcontrol> controls = new List<Bezcontrol>();
		public static void invalidate()
		{
			foreach (Bezcontrol b in controls) {
				b.panel.Invalidate();
			}
		}

		private PictureBox panel;
		private Bitmap bufferedimage;
		private Size lastSize;
		public Bez bez;

		SolidBrush bbg;
		SolidBrush bwhite, bred, blime, bblue;
		Pen pgraph;

		public Bezcontrol() { }

		public Bezcontrol(Bez bez, int x, int y, int w, int h)
		{
			this.bez = bez;
			controls.Add(this);
			this.Size = new Size(w - 6, h);
			this.Location = new Point(x + 3, y);
			this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Label lbl = new Label();
			lbl.Text = bez.name;
			lbl.AutoSize = false;
			lbl.Location = new Point(5, 0);
			lbl.Size = new Size(95, h);
			lbl.TextAlign = ContentAlignment.MiddleCenter;
			lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
			this.panel = new PictureBox();
			this.panel.Location = new Point(100, 0);
			this.panel.Size = new Size(w - 106 - 20, h);
			this.panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
			this.panel.Paint += new PaintEventHandler(panel_Paint);
			this.panel.MouseMove += new MouseEventHandler(panel_MouseMove);
			this.panel.MouseDown += new MouseEventHandler(panel_MouseDown);
			this.panel.MouseUp += new MouseEventHandler(panel_MouseUp);
			this.Controls.Add(lbl);
			this.Controls.Add(this.panel);
			bbg = new SolidBrush(Color.FromArgb(0xFF, 0, 30, 66));
			bwhite = new SolidBrush(Color.White);
			bred = new SolidBrush(Color.Red);
			blime = new SolidBrush(Color.Lime);
			bblue = new SolidBrush(Color.Blue);
			pgraph = new Pen(Color.FromArgb(0xFF, 254, 228, 151));
			drawtobuffer();
		}

		void drawtobuffer()
		{
			if (panel.Size.Width != lastSize.Width || panel.Size.Height != lastSize.Height) {
				if (bufferedimage != null) {
					bufferedimage.Dispose();
				}
				bufferedimage = new Bitmap(panel.Size.Width, panel.Size.Height);
				lastSize = panel.Size;
			}
			int w = this.panel.Width;
			int h = this.panel.Height;

			vec2 from, p1, p2, to;
			Graphics g = Graphics.FromImage(bufferedimage);
			g.FillRectangle(bbg, 0, 0, w, h);
			g.DrawLine(Pens.White, 0, h / 2, 50, h / 2);
			g.DrawLine(Pens.White, w - 50, h / 2, w, h / 2);
			GraphicsPath myPath = new GraphicsPath();
			for (int i = 0; i < w; i++) {
				int y = h - (int)(h * bez.valueAt(i / (float)w));
				if (0 <= y && y < h) {
					bufferedimage.SetPixel(i, y, Color.Cyan);
				}
			}
			for (int i = 0; i < bez.numsegments; i++) {
				absBezSeg(i, out from, out p1, out p2, out to);
				g.DrawLine(pgraph, from.x, from.y, p1.x, p1.y);
				g.FillRectangle(bred, p1.x - HS, p1.y - HS, SS, SS);
				g.FillRectangle(blime, from.x - HS, from.y - HS, SS, SS);
				g.DrawLine(pgraph, p2.x, p2.y, to.x, to.y);
				g.FillRectangle(bred, p2.x - HS, p2.y - 2f, SS, SS);
				if (i == bez.numsegments - 1) {
					g.FillRectangle(blime, to.x - HS, to.y - HS, SS, SS);
				}
			}
			g.Dispose();
		}

		int mousex = -5;
		int mousey = -5;

		void panel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int w = this.panel.Width;
			int h = this.panel.Height;

			drawtobuffer();
			g.DrawImage(bufferedimage, 0, 0);

			float t = progress(bez.start, bez.end, form.getUItime());
			if (0f <= t && t <= 1f) {
				int x = (int)(t * w);
				g.DrawLine(Pens.White, x, 0, x, h);
			}

			t = progress(0, w, mousex);
			mousey = (int) (h - bez.valueAt(t) * h);
			g.FillRectangle(bblue, mousex - 4, mousey - 4, 9, 9);
			mousex = -5;
			mousey = -5;
		}

		bool dragging;
		int movefromidx = -1;
		int movep1idx = -1;
		int movep2idx = -1;
		int movetoidx = -1;

		void dodrag(MouseEventArgs e)
		{
			vec2 from, p1, p2, to;
			int w = panel.Width;
			int h = panel.Height;
			if (movefromidx != -1) {
				float prevy = bez.from[movefromidx].y;
				bez.from[movefromidx].x = progress(0, w, e.X);
				bez.from[movefromidx].y = progress(h, 0, e.Y);
				bez.p1[movefromidx].y += bez.from[movefromidx].y - prevy;
				if (movefromidx == 0) {
					//bez.from[0].x = 0f;
				} else {
					bez.to[movefromidx - 1] = v2(bez.from[movefromidx]);
				}
			} else if (movep1idx != -1) {
				absBezSeg(movep1idx, out from, out p1, out p2, out to);
				vec2 mouse = from + (v2(e.X, e.Y) - from) * controlpointscale;
				bez.p1[movep1idx].x = progress(bez.from[movep1idx].x, bez.to[movep1idx].x, mouse.x / w);
				bez.p1[movep1idx].y = progress(h, 0, mouse.y);
				if (movep1idx > 0) {
					//bez.p2[movep1idx - 1].y = lerp(bez.from[movep1idx].y, bez.p1[movep1idx].y, -1f);
				}
			} else if (movep2idx != -1) {
				absBezSeg(movep2idx, out from, out p1, out p2, out to);
				vec2 mouse = to + (v2(e.X, e.Y) - to) * controlpointscale;
				bez.p2[movep2idx].x = progress(bez.from[movep2idx].x, bez.to[movep2idx].x, mouse.x / w);
				bez.p2[movep2idx].y = progress(h, 0, mouse.y);
				if (movep1idx < bez.numsegments - 1) {
					//bez.p1[movep2idx + 1].y = lerp(bez.to[movep2idx].y, bez.p2[movep2idx].y, -1f);
				}
			} else if (movetoidx != -1) {
				float prevy = bez.to[movetoidx].y;
				bez.to[movetoidx].x = progress(0, w, e.X);
				bez.to[movetoidx].y = progress(h, 0, e.Y);
				bez.p2[movetoidx].y += bez.to[movetoidx].y - prevy;
				if (movetoidx == bez.numsegments - 1) {
					//bez.to[0].x = 1f;
				} else {
					bez.from[movetoidx + 1] = v2(bez.to[movetoidx]);
				}
			}
			lastSize.Width = -1;
			this.panel.Invalidate();
			form.preview.Invalidate();
		}

		void panel_MouseUp(object sender, MouseEventArgs e)
		{
			dodrag(e);
			dragging = false;
			movefromidx = -1;
			movep1idx = -1;
			movep2idx = -1;
			movetoidx = -1;
		}

		void panel_MouseDown(object sender, MouseEventArgs e)
		{
			mousex = e.X;
			mousey = e.Y;
			vec2 from, p1, p2, to;
			for (int i = 0; i < bez.numsegments; i++) {
				absBezSeg(i, out from, out p1, out p2, out to);
				if (inrect(from)) {
					if (e.Button == MouseButtons.Right) {
						bez.delete(i);
						mousex = -5;
						this.panel.Invalidate();
						return;
					}
					movefromidx = i;
				} else if (inrect(p1)) {
					movep1idx = i;
				} else if (inrect(p2)) {
					movep2idx = i;
				} else if (inrect(to)) {
					if (e.Button == MouseButtons.Right) {
						bez.delete(i + 1);
						mousex = -5;
						this.panel.Invalidate();
						return;
					}
					movetoidx = i;
				} else {
					continue;
				}
				mousex = -5;
				dragging = true;
				this.panel.Invalidate();
				form.preview.Invalidate();
				return;
			}
			bez.separate(e.X / (float)panel.Width);
			lastSize.Width = -1;
			this.panel.Invalidate();
			form.preview.Invalidate();
		}

		void panel_MouseMove(object sender, MouseEventArgs e)
		{
			mousex = e.X;
			mousey = e.Y;
			if (dragging) {
				lastSize.Width = -1;
				mousex = -5;
				dodrag(e);
			} else {
				vec2 from, p1, p2, to;
				for (int i = 0; i < bez.numsegments; i++) {
					absBezSeg(i, out from, out p1, out p2, out to);
					if (inrect(from) || inrect(p1) || inrect(p2) || inrect(to)) {
						mousex = -5;
						break;
					}
				}
			}
			this.panel.Invalidate();
		}

		void absBezSeg(int i, out vec2 from, out vec2 p1, out vec2 p2, out vec2 to)
		{
			int w = panel.Width - 1;
			int h = panel.Height;
			float bw = bez.to[i].x - bez.from[i].x;
			from = v2(bez.from[i].x * w, h - bez.from[i].y * h);
			p1 = v2((bez.from[i].x + bez.p1[i].x * bw) * w, h - bez.p1[i].y * h);
			p2 = v2((bez.from[i].x + bez.p2[i].x * bw) * w, h - bez.p2[i].y * h);
			to = v2(bez.to[i].x * w, h - bez.to[i].y * h);
			p1 = from + (p1 - from) / controlpointscale;
			p2 = to + (p2 - to) / controlpointscale;
		}

		bool inrect(vec2 pos)
		{
			int hs = HS + 2;
			return pos.x - hs <= mousex && mousex <= pos.x + hs && pos.y - hs <= mousey && mousey <= pos.y + hs;
		}
	}
}
}
