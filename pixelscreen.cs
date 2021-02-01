using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	interface IColorOwner
	{
		Color getColor(int i, int j, int x, int y, float z, vec2 uv);
	}
	class Pixelscreen {

		int x, y, hpixels, vpixels, pixelsize, hpixeloffset, vpixeloffset;

		public object[,] owner;
		public vec2[,] uv;
		float[,] zbuf;
		Odot[,] odot;

		public Pixelscreen(int x, int y, int hpixels, int vpixels, int pixelsize) {
			this.x = x;
			this.y = y;
			this.hpixels = hpixels;
			this.vpixels = vpixels;
			this.pixelsize = pixelsize;
			init();
		}

		public Pixelscreen(int hpixels, int vpixels, int pixelsize) {
			this.x = 320 - hpixels * pixelsize / 2;
			this.y = 240 - vpixels * pixelsize / 2;
			this.hpixels = hpixels;
			this.vpixels = vpixels;
			this.pixelsize = pixelsize;
			init();
		}

		public object ownerAt(vec2 pos) {
			int x = (int) pos.x;
			int y = (int) pos.y;
			x -= this.x;
			y -= this.y;
			x /= pixelsize;
			y /= pixelsize;
			if (x < 0 || hpixels <= x || y < 0 || vpixels <= y) {
				return null;
			}
			return owner[x, y];
		}

		public bool hasOwner(object owner) {
			for (int i = 0; i < hpixels; i++) {
				for (int j = 0; j < vpixels; j++) {
					if (this.owner[i, j] == owner) {
						return true;
					}
				}
			}
			return false;
		}

		private void init()
		{
			this.zbuf = new float[hpixels,vpixels];
			this.owner = new object[hpixels, vpixels];
			this.odot = new Odot[hpixels, vpixels];
			this.uv = new vec2[hpixels, vpixels];
			this.hpixeloffset = this.x / pixelsize;
			this.vpixeloffset = this.y / pixelsize;
			for (int i = 0; i < hpixels; i++) {
				for (int j = 0; j < vpixels; j++) {
					odot[i,j] = new Odot(pixelsize == 2 ? ".png" : pixelsize + ".png", 0);
				}
			}
		}

		public void clear() {
			for (int i = 0; i < zbuf.GetLength(0); i++) {
				for (int j = 0; j < zbuf.GetLength(1); j++) {
					owner[i,j] = null;
				}
			}
		}

		public void draw(SCENE scene) {
			for (int i = 0; i < hpixels; i++) {
				for (int j = 0; j < vpixels; j++) {
					if (!(owner[i,j] is IColorOwner)) {
						odot[i,j].update(scene.time, null, null, 0f);
						continue;
					}
					IColorOwner co = (IColorOwner) owner[i, j];
					vec4 pos = v4(this.x + i * pixelsize, this.y + j * pixelsize, 1f, 1f);
					vec4 res = col(co.getColor(i, j, (int) pos.x, (int) pos.y, zbuf[i, j], uv[i, j]));
					if (res.x == 0f && res.y == 0f && res.z == 0f) {
						// this somehow fixes things /shrug
						odot[i, j].update(scene.time, null, null, 0f);
						continue;
					}
					odot[i,j].update(scene.time, res, pos);
					odot[i,j].draw(scene.g);
				}
			}
		}

		public void fin(Writer w) {
			for (int i = 0; i < zbuf.GetLength(0); i++) {
				for (int j = 0; j < zbuf.GetLength(1); j++) {
					odot[i,j].fin(w);
				}
			}
		}

		public void tri(object owner, vec4[] points) {
			if (points[0].z < 1 || points[1].z < 1 || points[2].z < 1) {
				return;
			}
			Array.Sort(points, sorter.instance);
			if (points[0].y == points[1].y) {
				toptri(owner, points[0], points[1], points[2]);
				return;
			}
			if (points[1].y == points[2].y) {
				bottri(owner, points[0], points[1], points[2]);
				return;
			}
			float perc = progress(points[0].y, points[2].y, points[1].y);
			vec4 phantom = (points[2] - points[0]) * perc + points[0];
			bottri(owner, points[0], phantom, points[1]);
			toptri(owner, phantom, points[1], points[2]);
		}

		private void toptri(object owner, vec4 p0, vec4 p1, vec4 p2) {
			if (p1.x < p0.x) {
				vec4 _ = p1;
				p1 = p0;
				p0 = _;
			}
			if (p0.y - p2.y == 0) {
				return;
			}

			/*
			 0  1
			  \/
			  2 
			*/

			float minx = min(p0.x, p2.x);
			float miny = p0.y;
			float maxx = max(p1.x, p2.x);
			float maxy = p2.y;

			int p_minx = -hpixeloffset + (int) minx / pixelsize;
			int p_miny = -vpixeloffset + (int) miny / pixelsize;
			int p_maxx = -hpixeloffset + (int) maxx / pixelsize + 1;
			int p_maxy = -vpixeloffset + (int) maxy / pixelsize + 1;

			p_miny = max(0, min(p_miny, vpixels - 1));
			p_minx = max(0, min(p_minx, hpixels - 1));
			p_maxy = max(0, min(p_maxy, vpixels));
			p_maxx = max(0, min(p_maxx, hpixels));
			for (int y = p_miny; y < p_maxy; y++) {
				float realy = this.y + y * pixelsize + pixelsize / 2f;

				for (int x = p_minx; x < p_maxx; x++) {
					float realx = this.x + x * pixelsize + pixelsize / 2f;

					if (realy < p0.y) {
						continue;
					}
					if (realy >= p2.y) {
						continue;
					}

					float ypercleft = progress(p0.y, p2.y, realy);
					float xminbound = lerp(p0.x, p2.x, ypercleft);

					float ypercright = progress(p1.y, p2.y, realy);
					float xmaxbound = lerp(p1.x, p2.x, ypercright);

					if (realx < xminbound) {
						continue;
					}
					if (realx >= xmaxbound) {
						continue;
					}

					float xperc = progress(xminbound, xmaxbound, realx);

					float dist1 = lerp(p0.w, p2.w, ypercleft);
					float dist2 = lerp(p1.w, p2.w, ypercright);
					float realdist = lerp(dist1, dist2, xperc);

					/*
					if (realz < 1f) {
						continue;
					}
					*/
					if (this.owner[x, y] != null && zbuf[x, y] < realdist) {
						continue;
					}
					zbuf[x, y] = realdist;
					this.owner[x, y] = owner;
				}
			}
		}

		private void bottri(object owner, vec4 p0, vec4 p1, vec4 p2) {
			if (p2.x < p1.x) {
				vec4 _ = p2;
				p2 = p1;
				p1 = _;
			}
			if (p0.y - p2.y == 0) {
				return;
			}

			/*
			   0
			  /\
			  1 2 
			*/

			float minx = min(p0.x, p1.x);
			float miny = p0.y;
			float maxx = max(p0.x, p2.x);
			float maxy = p2.y;

			int p_minx = -hpixeloffset + (int) minx / pixelsize;
			int p_miny = -vpixeloffset + (int) miny / pixelsize;
			int p_maxx = -hpixeloffset + (int) maxx / pixelsize + 1;
			int p_maxy = -vpixeloffset + (int) maxy / pixelsize + 1;

			p_miny = max(0, min(p_miny, vpixels - 1));
			p_minx = max(0, min(p_minx, hpixels - 1));
			p_maxy = max(0, min(p_maxy, vpixels));
			p_maxx = max(0, min(p_maxx, hpixels));
			for (int y = p_miny; y < p_maxy; y++) {
				float realy = this.y + y * pixelsize + pixelsize / 2f;

				for (int x = p_minx; x < p_maxx; x++) {
					float realx = this.x + x * pixelsize + pixelsize / 2f;

					if (realy <= p0.y) {
						continue;
					}
					if (realy >= p2.y) {
						continue;
					}

					float ypercleft = progress(p0.y, p1.y, realy);
					float xminbound = lerp(p0.x, p1.x, ypercleft);

					float ypercright = progress(p0.y, p2.y, realy);
					float xmaxbound = lerp(p0.x, p2.x, ypercright);

					if (realx < xminbound) {
						continue;
					}
					if (realx >= xmaxbound) {
						continue;
					}

					float xperc = progress(xminbound, xmaxbound, realx);

					float dist1 = lerp(p0.w, p1.w, ypercleft);
					float dist2 = lerp(p0.w, p2.w, ypercright);
					float realdist = lerp(dist1, dist2, xperc);

					/*
					if (realz < 1f) {
						continue;
					}
					*/
					if (this.owner[x, y] != null && zbuf[x, y] < realdist) {
						continue;
					}
					zbuf[x, y] = realdist;
					this.owner[x, y] = owner;
				}
			}
		}

		// yes duplicate code, fuck all, I just want to be done with this.
		public void tri_(object owner, vec6[] points)
		{
			if (points[0].z < 1 || points[1].z < 1 || points[2].z < 1) {
				return;
			}
			Array.Sort(points, sorter6.instance);
			if (points[0].y == points[1].y) {
				toptri_(owner, points[0], points[1], points[2]);
				return;
			}
			if (points[1].y == points[2].y) {
				bottri_(owner, points[0], points[1], points[2]);
				return;
			}
			float perc = progress(points[0].y, points[2].y, points[1].y);
			// lerp of z & uv need to be corrected (perspective correction thingies)
			float actualz = 1f / lerp(1f / points[0].w, 1f / points[2].w, perc);
			vec2 uv = lerp(points[0].uv / points[0].w, points[2].uv / points[2].w, perc) * actualz;
			vec6 phantom = v6(v4(lerp(points[0].xyz, points[2].xyz, perc), actualz), uv);
			bottri_(owner, points[0], phantom, points[1]);
			toptri_(owner, phantom, points[1], points[2]);
		}

		private void toptri_(object owner, vec6 p0, vec6 p1, vec6 p2)
		{
			if (p1.x < p0.x) {
				vec6 _ = p1;
				p1 = p0;
				p0 = _;
			}
			if (p0.y - p2.y == 0) {
				return;
			}

			/*
			 0  1
			  \/
			  2 
			*/

			float minx = min(p0.x, p2.x);
			float miny = p0.y;
			float maxx = max(p1.x, p2.x);
			float maxy = p2.y;

			int p_minx = -hpixeloffset + (int)minx / pixelsize;
			int p_miny = -vpixeloffset + (int)miny / pixelsize;
			int p_maxx = -hpixeloffset + (int)maxx / pixelsize + 1;
			int p_maxy = -vpixeloffset + (int)maxy / pixelsize + 1;

			p_miny = max(0, min(p_miny, vpixels - 1));
			p_minx = max(0, min(p_minx, hpixels - 1));
			p_maxy = max(0, min(p_maxy, vpixels));
			p_maxx = max(0, min(p_maxx, hpixels));
			for (int y = p_miny; y < p_maxy; y++) {
				float realy = this.y + y * pixelsize + pixelsize / 2f;

				for (int x = p_minx; x < p_maxx; x++) {
					float realx = this.x + x * pixelsize + pixelsize / 2f;

					if (realy < p0.y) {
						continue;
					}
					if (realy >= p2.y) {
						continue;
					}

					float ypercleft = progress(p0.y, p2.y, realy);
					float xminbound = lerp(p0.x, p2.x, ypercleft);

					float ypercright = progress(p1.y, p2.y, realy);
					float xmaxbound = lerp(p1.x, p2.x, ypercright);

					if (realx < xminbound) {
						continue;
					}
					if (realx >= xmaxbound) {
						continue;
					}

					float xperc = progress(xminbound, xmaxbound, realx);

					float zleft = 1f / lerp(1f / p0.w, 1f / p2.w, ypercleft);
					float zright = 1f / lerp(1f / p1.w, 1f / p2.w, ypercright);
					float zreal = 1f / lerp(1f / zleft, 1f / zright, xperc);

					/*
					if (realz < 1f) {
						continue;
					}
					*/
					if (this.owner[x, y] != null && zbuf[x, y] < zreal) {
						continue;
					}
					vec2 uvleft = lerp(p0.uv / p0.w, p2.uv / p2.w, ypercleft) /** zleft*/;
					vec2 uvright = lerp(p1.uv / p1.w, p2.uv / p2.w, ypercright) /** zright*/;
					vec2 uvreal = lerp(uvleft /*/ zleft*/, uvright /*/ zright*/, xperc) * zreal;
					uv[x, y] = uvreal;
					zbuf[x, y] = zreal;
					this.owner[x, y] = owner;
				}
			}
		}

		private void bottri_(object owner, vec6 p0, vec6 p1, vec6 p2)
		{
			if (p2.x < p1.x) {
				vec6 _ = p2;
				p2 = p1;
				p1 = _;
			}
			if (p0.y - p2.y == 0) {
				return;
			}

			/*
			   0
			  /\
			  1 2 
			*/

			float minx = min(p0.x, p1.x);
			float miny = p0.y;
			float maxx = max(p0.x, p2.x);
			float maxy = p2.y;

			int p_minx = -hpixeloffset + (int)minx / pixelsize;
			int p_miny = -vpixeloffset + (int)miny / pixelsize;
			int p_maxx = -hpixeloffset + (int)maxx / pixelsize + 1;
			int p_maxy = -vpixeloffset + (int)maxy / pixelsize + 1;

			p_miny = max(0, min(p_miny, vpixels - 1));
			p_minx = max(0, min(p_minx, hpixels - 1));
			p_maxy = max(0, min(p_maxy, vpixels));
			p_maxx = max(0, min(p_maxx, hpixels));

			for (int y = p_miny; y < p_maxy; y++) {
				float realy = this.y + y * pixelsize + pixelsize / 2f;

				for (int x = p_minx; x < p_maxx; x++) {
					float realx = this.x + x * pixelsize + pixelsize / 2f;

					if (realy <= p0.y) {
						continue;
					}
					if (realy >= p2.y) {
						continue;
					}

					float ypercleft = progress(p0.y, p1.y, realy);
					float xminbound = lerp(p0.x, p1.x, ypercleft);

					float ypercright = progress(p0.y, p2.y, realy);
					float xmaxbound = lerp(p0.x, p2.x, ypercright);

					if (realx < xminbound) {
						continue;
					}
					if (realx >= xmaxbound) {
						continue;
					}

					float xperc = progress(xminbound, xmaxbound, realx);

					float zleft = 1f / lerp(1f / p0.w, 1f / p1.w, ypercleft);
					float zright = 1f / lerp(1f / p0.w, 1f / p2.w, ypercright);
					float zreal = 1f / lerp(1f / zleft, 1f / zright, xperc);

					/*
					if (realz < 1f) {
						continue;
					}
					*/
					if (this.owner[x, y] != null && zbuf[x, y] < zreal) {
						continue;
					}
					vec2 uvleft = lerp(p0.uv / p0.w, p1.uv / p1.w, ypercleft) /** zleft*/;
					vec2 uvright = lerp(p0.uv / p0.w, p2.uv / p2.w, ypercright) /** zright*/;
					vec2 uvreal = lerp(uvleft /*/ zleft*/, uvright /*/ zright*/, xperc) * zreal;
					uv[x, y] = uvreal;
					zbuf[x, y] = zreal;
					this.owner[x, y] = owner;
				}
			}
		}
	}

	class sorter : IComparer<vec4>
	{
		public static sorter instance = new sorter();

		public int Compare(vec4 a, vec4 b)
		{
			return a.y.CompareTo(b.y);
		}
	}

	class sorter6 : IComparer<vec6> {
		public static sorter6 instance = new sorter6();

		public int Compare(vec6 a, vec6 b) {
			return a.y.CompareTo(b.y);
		}
	}

}
}
