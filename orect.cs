using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace osusb1 {
partial class all {
	class Orect {
		public const int SETTING_SHADED = 0x1;
		public const int SETTING_NO_BCULL = 0x2;

		public readonly Rect rect;
		Otri[] tris;
		int settings;

		public Orect(Rect rect, int settings) {
			this.rect = rect;
			this.settings = settings;
			tris = new Otri[4];
			for (int i = 0; i < 4; i++) {
				tris[i] = new Otri();
			}
		}

		public void addCommandOverride(ICommand cmd) {
			for (int i = 0; i < tris.Length; i++) {
				tris[i].addCommandOverride(cmd);
			}
		}

		public void update_culled(SCENE scene)
		{
			for (int i = 0; i < tris.Length; i++) {
				tris[i].update(scene.time, null, 0f, null, v2(0f));
			}
		}

		public void update(SCENE scene)
		{
			update(scene, .3f, .7f, 1f);
		}

		public bool is_fully_visible()
		{
			foreach (Tri t in new Tri[] { rect.tri1, rect.tri2 }) {
				vec4[] pts4 = {
					project(t.points[t.a]),
					project(t.points[t.b]),
					project(t.points[t.c]),
				};

				if (pts4[0].z < 1f || pts4[1].z < 1f || pts4[2].z < 1f) {
					return false;
				}

				vec2[] pts = { pts4[0].xy, pts4[1].xy, pts4[2].xy };

				if (!isonscreen(pts)) {
					return false;
				}	
			}
			return true;
		}

		public void update(SCENE scene, float light_ambient, float light_diffuse, float light_mod) {
			Tri[] _t = { rect.tri1, rect.tri2 };
			for (int i = 0; i < 2; i++) {
				Tri t = _t[i];
				Otri tri1 = tris[i * 2 + 0];
				Otri tri2 = tris[i * 2 + 1];

				if (light_mod < 0) {
					goto cull;
				}

				if (t.shouldcull() && (settings & SETTING_NO_BCULL) == 0) {
					goto cull;
				}

				vec4 shade = all.col(t.color);
				if ((settings & SETTING_SHADED) > 0) {
					//float rv = (rect.surfacenorm().norm() ^ rect.rayvec().norm());
					//if (rv < 0.0f) {
					//	goto cull; // TODO why wasn't this here before
					//}
					float rv = rect.surfacenorm().norm() ^ (rect.pts[rect.a] - sunpos).norm();
					if (rv < 0.0f) {
						rv = 0.0f;
					}
					float camfactor = rect.surfacenorm().norm() ^ (rect.pts[rect.a] - campos).norm();
					//if (camfactor < 0.0f) {
						camfactor = 0.0f;
					//}
					if (t.shouldcull()) {
						rv *= -1;
					}
					shade *= light_ambient + light_diffuse * rv + .2f * camfactor;
					shade *= light_mod;
					if (shade.x > 1f) shade.x = 1f;
					if (shade.y > 1f) shade.y = 1f;
					if (shade.z > 1f) shade.z = 1f;
					shade.w = 1f;
				}

				vec4[] pts4 = {
					project(t.points[t.a]),
					project(t.points[t.b]),
					project(t.points[t.c]),
				};

				if (pts4[0].z < 1f || pts4[1].z < 1f || pts4[2].z < 1f) {
					goto cull;
				}

				vec2[] pts = { pts4[0].xy, pts4[1].xy, pts4[2].xy };

				if (!isonscreen(pts)) {
					goto cull;
				}

				if (distance(pts[0], pts[1]) < distance(pts[0], pts[2])) {
					swap<vec2>(pts, 1, 2);
				}
				if (distance(pts[0], pts[1]) < distance(pts[1], pts[2])) {
					swap<vec2>(pts, 0, 2);
				}

				float rot = angle(pts[0], pts[1]);
				float dangle = rot - angle(pts[0], pts[2]);
				float x = cos(dangle) * distance(pts[0], pts[2]) / distance(pts[0], pts[1]);
				vec2 phantom = lerp(pts[0], pts[1], x);

				float w = project((t.points[t.a] + t.points[t.b] + t.points[t.c]) / 3f).w;
				dotri(scene, tri1, pts, phantom, 0, w, shade);
				dotri(scene, tri2, pts, phantom, 1, w, shade);
				continue;
cull:
				tri1.update(scene.time, null, 0f, null, v2(0f));
				tri2.update(scene.time, null, 0f, null, v2(0f));
			}
		}

		private bool isonscreen(vec2[] pts) {
			if (isOnScreen(pts[0]) || isOnScreen(pts[1]) || isOnScreen(pts[2])) {
				return true;
			}
			return ios(pts[0], pts[1]) || ios(pts[1], pts[2]) || ios(pts[0], pts[2]);
		}

		private bool ios(vec2 a, vec2 b) {
			// don't look
			vec2[] pts = {a, b};
			if (b.x < a.x) {
				swap<vec2>(pts, 0, 1);
			}
			if ((a.x < LOWERBOUND && b.x > LOWERBOUND) ||
				(a.x < UPPERBOUND && b.x > UPPERBOUND))
			{
				float x = progress(a.x, b.x, LOWERBOUND);
				if (x < 0 || x > 1) {
					x = progress(a.x, b.x, UPPERBOUND);
				}
				float y = lerp(a.y, b.y, x);
				if (y > 0 && y < 480) {
					return true;
				}
			}
			if (b.y < a.y) {
				swap<vec2>(pts, 0, 1);
			}
			if ((a.y < 0 && b.y > 0) ||
				(a.y < 480 && b.y > 480))
			{
				float y = progress(a.y, b.y, 0f);
				if (y < 0 || y > 1) {
					y = progress(a.y, b.y, 480);
				}
				float x = lerp(a.x, b.x, y);
				if (x > LOWERBOUND && x < UPPERBOUND) {
					return true;
				}
			}
			return false;
		}

		private void dotri(SCENE scene, Otri tri, vec2[] pts, vec2 phantom, int i, float w, vec4 col) {
			float rot = angle(phantom, pts[2]) + PI;
			vec2 pos = pts[2];
			vec2 size = v2(distance(phantom, pts[2]), distance(phantom, pts[i]));
			float d = angle(pts[i], phantom) - angle(pts[2], phantom);
			if (d > PI || (d < 0 && d > -PI)) {
				pos = pts[i];
				rot -= PI2;
				size = v2(size.y, size.x);
			}
			tri.update(scene.time, col, rot, v4(pos.x, pos.y, 1f, w), size);
			tri.draw(scene.g);
		}

		public void fin(Writer w) {
			foreach (Otri t in tris) {
				t.fin(w);
			}
		}
	}
	class MultiRect
	{
		private Rect rect;
		public MultiRectChild child;

		public MultiRect(Rect rect)
		{
			this.rect = rect;
			vec3[] mypoints = new vec3[4];
			mypoints[0] = this.rect.pts[this.rect.a];
			mypoints[1] = this.rect.pts[this.rect.b];
			mypoints[2] = this.rect.pts[this.rect.c];
			mypoints[3] = this.rect.pts[this.rect.d];
			this.child = new MultiRectChild(mypoints, rect.color, 0, 0f, 1f, 0f, 1f);
		}

		public void update(SCENE scene)
		{
			this.child.parentpoints[0] = this.rect.pts[this.rect.a];
			this.child.parentpoints[1] = this.rect.pts[this.rect.b];
			this.child.parentpoints[2] = this.rect.pts[this.rect.c];
			this.child.parentpoints[3] = this.rect.pts[this.rect.d];
			this.child.update(scene, false);
		}

		public void fin(Writer w)
		{
			this.child.fin(w);
		}
	}
	class MultiRectChild
	{
		static Color[] colors = new Color[] {
			Color.Aqua,
			Color.Red,
			Color.Orange,
			Color.Green,
			Color.Blue,
			Color.Magenta,
			Color.Maroon,
			Color.Lime,
			Color.Yellow,
			Color.White,
			Color.Gray,
			Color.Beige,
			Color.CornflowerBlue,
			Color.Chocolate,
			Color.MintCream,
		};
		public vec3[] parentpoints;
		public vec3[] mypoints;
		public float abt1, abt2, act1, act2; /*to make this child (mypoints)*/
		public Orect rect;
		public MultiRectChild[] children;
		public int depth;

		public MultiRectChild(vec3[] points, Color col, int depth, float abt1, float abt2, float act1, float act2)
		{
			this.depth = depth;
			this.abt1 = abt1;
			this.abt2 = abt2;
			this.act1 = act1;
			this.act2 = act2;
			this.parentpoints = new vec3[4];
			this.mypoints = new vec3[4];
			copy(this.parentpoints, points);
			this.update_my_points();
			this.rect = new Orect(new Rect(this, col /*colors[this.depth]*/, this.mypoints, 0, 1, 2, 3), 0);
			float acdist = distance(this.mypoints[0], this.mypoints[2]);
			float abdist = distance(this.mypoints[0], this.mypoints[1]);
			if (depth < 12 && (acdist > 30f || abdist > 30f)) {
				depth++;
				if (abdist >= 1.75 * acdist) {
					this.children = new MultiRectChild[] {
					new MultiRectChild(mypoints, col, depth, 0f, .5f, 0f, 1f),
					new MultiRectChild(mypoints, col, depth, .5f, 1f, 0f, 1f),
				};
				} else if (acdist >= 1.75 * abdist) {
					this.children = new MultiRectChild[] {
					new MultiRectChild(mypoints, col, depth, 0f, 1f, 0f, .5f),
					new MultiRectChild(mypoints, col, depth, 0f, 1f, .5f, 1f),
				};
				} else {
					this.children = new MultiRectChild[] {
					new MultiRectChild(mypoints, col, depth, 0f, .5f, 0f, .5f),
					new MultiRectChild(mypoints, col, depth, .5f, 1f, 0f, .5f),
					new MultiRectChild(mypoints, col, depth, 0f, .5f, .5f, 1f),
					new MultiRectChild(mypoints, col, depth, .5f, 1f, .5f, 1f),
				};
				}
			} else {
				this.children = new MultiRectChild[0];
			}
		}

		private void update_my_points()
		{
			vec3 _a = lerp(this.parentpoints[0], this.parentpoints[1], this.abt1);
			vec3 _b = lerp(this.parentpoints[0], this.parentpoints[1], this.abt2);
			vec3 _c = lerp(this.parentpoints[2], this.parentpoints[3], this.abt1);
			vec3 _d = lerp(this.parentpoints[2], this.parentpoints[3], this.abt2);
			vec3 __a = lerp(_a, _c, this.act1);
			vec3 __c = lerp(_a, _c, this.act2);
			vec3 __b = lerp(_b, _d, this.act1);
			vec3 __d = lerp(_b, _d, this.act2);
			this.mypoints[0] = __a;
			this.mypoints[1] = __b;
			this.mypoints[2] = __c;
			this.mypoints[3] = __d;
		}

		public void update(SCENE scene, bool culled)
		{
			this.update_my_points();

			if (culled) {
				this.rect.update_culled(scene);
			} else {
				if (this.rect.is_fully_visible()) {
					this.rect.update(scene, .3f, .7f, 1f);
					culled = true;
				} else {
					this.rect.update_culled(scene);
				}
			}

			foreach (MultiRectChild child in this.children) {
				copy(child.parentpoints, this.mypoints);
				child.update(scene, culled);
			}
		}

		public void fin(Writer w)
		{
			this.rect.fin(w);
			foreach (MultiRectChild child in this.children) {
				child.fin(w);
			}
		}
	}
}
}
