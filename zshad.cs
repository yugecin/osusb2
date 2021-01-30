using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	class Zshad : Z, IColorOwner {
		static Color[] hwite = new Color[] {
			Color.White, Color.White, Color.White, Color.White,
			Color.White, Color.White, Color.White, Color.White
		};
		Cube c;
		vec3[] points;
		vec3[] _points;

		Pixelscreen pixelscreen;

		public Zshad(int start, int stop)
		{
			this.start = start;
			this.stop = stop;
			framedelta = 50;

			this.points = new vec3[8];
			this._points = new vec3[this.points.Length];

			new Pcube(points, 0).set(v3(), 10f, 10f, 10f);
			c = new Cube(hwite, this._points, 0);

			this.pixelscreen = new Pixelscreen(/*widescreen mode*/854 / 4, 480 / 4, 4);
		}

		public override void draw(SCENE scene)
		{
			copy(_points, points);
			turn(_points, v3(), quat(sin(scene.progress * 50f), 0f, sin(scene.progress * 20f) * .5f));
			move(_points, v3(20f, 0f, -5f));
			move(_points, Zcamera.mid);
			Zcamera.adjust(_points);

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
				this.pixelscreen.tri_(this, new vec6[] {
					v6(a, v2(0f)),
					v6(b, v2(1f, 0f)),
					v6(c, v2(0f, 1f))
				});
				this.pixelscreen.tri_(this, new vec6[] {
					v6(c, v2(0f, 1f)),
					v6(b, v2(1f, 0f)),
					v6(d, v2(1f))
				});
			}
			this.pixelscreen.draw(scene);
		}

		public override void fin(Writer w) {
			this.pixelscreen.fin(w);
		}

		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			float r = clamp(uv.x, 0f, 1f);
			float g = clamp(uv.y, 0f, 1f);
			//r = distance(a.xy, p) * 0.004f;
			//r = clamp(r, 0f, 1f);
			//g = 1f - r;
			return v3(r, g, 0f).col();
		}
	}
}
}
