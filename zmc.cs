using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace osusb1 {
partial class all {
	class Zmc : Z {
		static Color[] hwite = new Color[] {
			Color.White, Color.White, Color.White, Color.White,
			Color.White, Color.White, Color.White, Color.White
		};
		Cube c;
		vec3[] points;
		vec3[] _points;
		IColorOwner[] sides;

		vec3[] tpoints;
		vec3[] _tpoints;
		Rect[] trects;
		Orect[] torects;

		Pixelscreen pixelscreen;

		Odot[] statictext;
		vec2[] textloc;
		Odot[] sizetext;
		vec4[] sizetextloc;
		Odot[] sizetext2;
		vec4[] sizetextloc2;

		const int TEXTSPACING = 3;
		string[] text = {
			"Pixelgrid",
			"Triangles",
		};
		vec2[] textlocstart = {
			v2(20f, 50f),
			v2(290f, 50f),
		};
		vec2 textoffset = v2(80f, 30f);

		public Zmc(int start, int stop, int pixelsize)
		{

			this.start = start;
			this.stop = stop;
			framedelta = 50;

			this.sides = new IColorOwner[] {
				GrassSide.instance,
				GrassSide.instance,
				GrassSide.instance,
				GrassTop.instance,
				GrassSide.instance,
				GrassSide.instance,
			};
			this.points = new vec3[8];
			this._points = new vec3[this.points.Length];

			new Pcube(points, 0).set(v3(), 8f, 8f, 8f);
			copy(_points, points);
			c = new Cube(hwite, this._points, 0);
			Rect[] r = c.rects;

			tpoints = new vec3[4 * 8 * 8 * 6];
			_tpoints = new vec3[tpoints.Length];
			trects = new Rect[8 * 8 * 6];
			shit(c.rects[Cube.U], 0, GrassTop.instance);
			shit(c.rects[Cube.L], 1 * 8 * 8, GrassSide.instance);
			shit(c.rects[Cube.R], 2 * 8 * 8, GrassSide.instance);
			shit(c.rects[Cube.F], 3 * 8 * 8, GrassSide.instance);
			shit(c.rects[Cube.B], 4 * 8 * 8, GrassSide.instance);
			shit(c.rects[Cube.D], 5 * 8 * 8, GrassSide.instance);
			torects = new Orect[trects.Length];
			for (int i = 0; i < torects.Length; i++) {
				torects[i] = new Orect(trects[i], 0);
			}

			int pxs = pixelsize;
			this.pixelscreen = new Pixelscreen(/*widescreen mode*/854 / pxs, 480 / pxs, pxs);






			sizetext = new Odot[8/*charwidth*/ * 8/*charheight*/ * 10];
			sizetext2 = new Odot[8/*charwidth*/ * 8/*charheight*/ * 10];
			for (int i = 0; i < sizetext.Length; i++) {
				sizetext[i] = new Odot(Sprite.SPRITE_SQUARE_2_2, 0);
				sizetext2[i] = new Odot(Sprite.SPRITE_SQUARE_2_2, 0);
			}
			sizetextloc = new vec4[sizetext.Length];
			sizetextloc2 = new vec4[sizetext.Length];
			for (int i = 0; i < sizetextloc.Length; i++) {
				sizetextloc[i] = v4(90f, 150f, 1f, 1f);
				sizetextloc[i].x += (i % 80) * TEXTSPACING;
				sizetextloc[i].y += (i / 80 - 6) * TEXTSPACING - 2;
				sizetextloc2[i] = v4(360f, 150f, 1f, 1f);
				sizetextloc2[i].x += (i % 80) * TEXTSPACING;
				sizetextloc2[i].y += (i / 80 - 6) * TEXTSPACING - 2;
			}

			inittext();
		}

		private void inittext()
		{
			int pointcount = 0;
			for (int i = 0; i < text.Length; i++) {
				pointcount += font.calcPointCount(text[i]);
			}
			statictext = new Odot[pointcount];
			textloc = new vec2[pointcount];

			int idx = 0;
			for (int q = 0; q < text.Length; q++) {
				string t = text[q];
				int xoff = 0;
				for (int i = 0; i < t.Length; i++) {
					int c = t[i] - 32;
					int cw = font.charwidth[c];
					for (int j = 0; j < font.charheight; j++) {
						for (int k = 0; k < cw; k++) {
							if (((font.chardata[c][j] >> k) & 1) == 1) {
								int x = xoff + k;
								textloc[idx] = v2(x, j) * TEXTSPACING + textlocstart[q] + textoffset;
								statictext[idx] = new Odot(Sprite.SPRITE_SQUARE_2_2, 0);
								idx++;
							}
						}
					}
					xoff += cw + 1;
				}
			}
		}

		private void shit(Rect rect, int baseidx, IColorOwner co)
		{
			int ridx = baseidx;
			baseidx *= 4;
			vec3 a = rect.pts[rect.a];
			vec3 b = rect.pts[rect.b];
			vec3 c = rect.pts[rect.c];
			vec3 d = rect.pts[rect.d];
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					float pi = (i) / 8f;
					float pi2 = (i + 1) / 8f;
					float pj = j / 8f;
					float pj2 = (j + 1) / 8f;
					vec3 ab1 = lerp(a, b, pi);
					vec3 ab2 = lerp(a, b, pi2);
					vec3 cd1 = lerp(c, d, pi);
					vec3 cd2 = lerp(c, d, pi2);
					int z = baseidx;
					tpoints[baseidx++] = lerp(ab1, cd1, pj);
					tpoints[baseidx++] = lerp(ab2, cd2, pj);
					tpoints[baseidx++] = lerp(ab1, cd1, pj2);
					tpoints[baseidx++] = lerp(ab2, cd2, pj2);
					Color col = co.getColor(0, 0, 0, 0, 0, v2(pi, pj));
					trects[ridx++] = new Rect(_tpoints, col, _tpoints, z, z + 1, z + 2, z + 3);
				}
			}
		}

		public override void draw(SCENE scene)
		{
			copy(_points, points);
			move(_points, v3(0f, 0f, -7.5f));
			turn(_points, v3(), quat(0f, 0f, -.8f + 3.4f * scene.progress));
			move(_points, v3(0f, -8f, 0f));
			move(_points, Zcamera.mid);
			Zcamera.adjust(_points);
			copy(_tpoints, tpoints);
			move(_tpoints, v3(0f, 0f, -7.5f));
			turn(_tpoints, v3(), quat(0f, 0f, .8f - 3.4f * scene.progress));
			move(_tpoints, v3(0f, 8f, 0f));
			move(_tpoints, Zcamera.mid);
			Zcamera.adjust(_tpoints);

			pixelscreen.clear();
			for (int i = 0; i < this.c.rects.Length; i++) {
				Rect r = this.c.rects[i];
				if (r.shouldcull()) {
					continue;
				}
				// abc, cbd
				vec4 a = project(this._points[r.a]);
				vec4 b = project(this._points[r.b]);
				vec4 c = project(this._points[r.c]);
				vec4 d = project(this._points[r.d]);
				if (a.z < 1 || b.z < 1 || c.z < 1 || d.z < 1) {
					continue;
				}
				this.pixelscreen.tri_(sides[i], new vec6[] {
				v6(a, v2(0f)),
				v6(b, v2(1f, 0f)),
				v6(c, v2(0f, 1f))
			});
				this.pixelscreen.tri_(sides[i], new vec6[] {
				v6(c, v2(0f, 1f)),
				v6(b, v2(1f, 0f)),
				v6(d, v2(1f))
			});
			}
			this.pixelscreen.draw(scene);

			foreach (Orect o in torects) {
				o.update(scene);
			}







			for (int i = 0; i < statictext.Length; i++) {
				statictext[i].update(scene.time, v4(1f), v4(textloc[i], 1f, 1f));
				statictext[i].draw(scene.g);
			}


			{
				int size = 0;
				for (int i = 0; i < pixelscreen.hpixels; i++) {
					for (int j = 0; j < pixelscreen.vpixels; j++) {
						size += pixelscreen.odot[i, j].calcStoryboardCommandSize();
					}
				}
				if (!rendering) {
					size = 2030333;
				}
				string sizestr = string.Format("{0,8:###0.000}KB", (size / 1000f));
				bool[] sizetextshown = new bool[sizetext.Length];
				int xoff = 0;
				for (int i = 0; i < sizestr.Length; i++) {
					int c = sizestr[i] - 32;
					int cw = font.charwidth[c];
					for (int j = 0; j < font.charheight; j++) {
						for (int k = 0; k < cw; k++) {
							if (((font.chardata[c][j] >> k) & 1) == 1) {
								int idx = 80 * j + xoff + k;
								sizetextshown[idx] = true;
								sizetext[idx].update(scene.time, v4(1f), sizetextloc[idx]);
								sizetext[idx].draw(scene.g);
							}
						}
					}
					xoff += cw + 1;
				}
				for (int i = 0; i < sizetextshown.Length; i++) {
					if (!sizetextshown[i]) {
						sizetext[i].update(scene.time, null, null);
					}
				}
			}
			{
				int size = 0;
				foreach (Orect r in torects) {
					size += r.tris[0].calcStoryboardCommandSize();
					size += r.tris[1].calcStoryboardCommandSize();
					size += r.tris[2].calcStoryboardCommandSize();
					size += r.tris[3].calcStoryboardCommandSize();
				}
				if (!rendering) {
					size = 3222000;
				}
				string sizestr = string.Format("{0,8:###0.000}KB", (size / 1000f));
				bool[] sizetextshown = new bool[sizetext.Length];
				int xoff = 0;
				for (int i = 0; i < sizestr.Length; i++) {
					int c = sizestr[i] - 32;
					int cw = font.charwidth[c];
					for (int j = 0; j < font.charheight; j++) {
						for (int k = 0; k < cw; k++) {
							if (((font.chardata[c][j] >> k) & 1) == 1) {
								int idx = 80 * j + xoff + k;
								sizetextshown[idx] = true;
								sizetext2[idx].update(scene.time, v4(1f), sizetextloc2[idx]);
								sizetext2[idx].draw(scene.g);
							}
						}
					}
					xoff += cw + 1;
				}
				for (int i = 0; i < sizetextshown.Length; i++) {
					if (!sizetextshown[i]) {
						sizetext2[i].update(scene.time, null, null);
					}
				}
			}
		}

		public override void fin(Writer w)
		{
			this.pixelscreen.fin(w);
			foreach (Orect o in torects) {
				o.fin(w);
			}
			foreach (Odot d in statictext) {
				d.fin(w);
			}
			foreach (Odot d in sizetext) {
				d.fin(w);
			}
			foreach (Odot d in sizetext2) {
				d.fin(w);
			}
		}
	}

	class GrassSide : IColorOwner
	{
		public static IColorOwner instance = new GrassSide();
		Bitmap bm;
		GrassSide()
		{
			try {
				using (FileStream fs = File.OpenRead("grass-side.png")) {
					bm = new Bitmap(fs);
				}
			} catch (Exception e) {
				Console.WriteLine(e);
			}
		}
		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			if (bm == null) {
				return v3(1f, 0f, 0f).col();
			} else {
				x = (int)(uv.x * (bm.Width));
				y = (int)(uv.y * (bm.Height));
				return bm.GetPixel(x, y);
			}
		}
	}

	class GrassTop : IColorOwner
	{
		public static IColorOwner instance = new GrassTop();
		Bitmap bm;
		GrassTop()
		{
			try {
				using (FileStream fs = File.OpenRead("grass-top.png")) {
					bm = new Bitmap(fs);
				}
			} catch (Exception e) {
				Console.WriteLine(e);
			}
		}
		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			if (bm == null) {
				return v3(1f, 0f, 0f).col();
			} else {
				x = (int)(uv.x * (bm.Width));
				y = (int)(uv.y * (bm.Height));
				return bm.GetPixel(x, y);
			}
		}
	}
}
}
