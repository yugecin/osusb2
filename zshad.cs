using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	static float shadborder = 0.025f;
	class Zshad : Z, IColorOwner {
		static Color[] hwite = new Color[] {
			Color.White, Color.White, Color.White, Color.White,
			Color.White, Color.White, Color.White, Color.White
		};
		struct LINE
		{
			public Oline line;
			public Rect r1, r2;
			public vec3[] pts;
			public int a, b;
			public LINE(Rect r1, Rect r2, int a, int b)
			{
				pts = new vec3[2];
				line = new Oline(pts, 0, 1);
				this.r1 = r1;
				this.r2 = r2;
				this.a = a;
				this.b = b;
			}
		}
		Cube c;
		vec3[] points;
		vec3[] _points;
		LINE[] lines;
		IColorOwner[] sides = new IColorOwner[] {
			shadsomejapanesecharacter.instance,
			shadosulogo.instance,
			shadsomejapanesecharacter.instance,
			shadsomejapanesecharacter.instance,
			shadsomejapanesecharacter.instance,
			shadsomejapanesecharacter.instance,
		};

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
			Rect[] r = c.rects;
			lines = new LINE[] {
				new LINE(r[Cube.F], r[Cube.U], 0, 1),
				new LINE(r[Cube.F], r[Cube.L], 3, 0),
				new LINE(r[Cube.F], r[Cube.R], 1, 2),
				new LINE(r[Cube.F], r[Cube.D], 2, 3),
				new LINE(r[Cube.L], r[Cube.U], 0, 5),
				new LINE(r[Cube.L], r[Cube.B], 5, 4),
				new LINE(r[Cube.L], r[Cube.D], 4, 3),
				new LINE(r[Cube.B], r[Cube.D], 4, 7),
				new LINE(r[Cube.R], r[Cube.U], 6, 1),
				new LINE(r[Cube.R], r[Cube.B], 7, 6),
				new LINE(r[Cube.R], r[Cube.D], 2, 7),
				new LINE(r[Cube.B], r[Cube.U], 6, 5),
			};

			int pxs = 3;
			this.pixelscreen = new Pixelscreen(/*widescreen mode*/854 / pxs, 480 / pxs, pxs);
		}

		public override void draw(SCENE scene)
		{
			copy(_points, points);
			turn(_points, v3(), quat(sin(scene.progress * 5f), 0f, sin(scene.progress) * .5f));
			move(_points, v3(20f, 0f, -5f));
			move(_points, Zcamera.mid);
			Zcamera.adjust(_points);

			pixelscreen.clear();
			if (scene.time < scene.starttime + 100) {
				// disgusting hack beacuse the osushader would have ghost pixels on the first frames.
				for (int i = 0; i < this.pixelscreen.owner.GetLength(0); i++) {
					for (int j = 0; j < this.pixelscreen.owner.GetLength(1); j++) {
						this.pixelscreen.owner[i, j] = this;
					}
				}
			} else {
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
			}
			this.pixelscreen.draw(scene);

			ICommand.round_move_decimals.Push(5);
			foreach (LINE l in lines) {
				if (l.r1.shouldcull() && l.r2.shouldcull()) {
					l.line.update(scene.time, null);
					continue;
				}
				l.pts[0] = _points[l.a];
				l.pts[1] = _points[l.b];
				l.line.update(scene.time, v4(1f));
				l.line.draw(scene.g);
			}
			ICommand.round_move_decimals.Pop();
		}

		public override void fin(Writer w)
		{
			this.pixelscreen.fin(w);

			ICommand.round_move_decimals.Push(5);
			foreach (LINE l in lines) {
				l.line.fin(w);
			}
			ICommand.round_move_decimals.Pop();
		}

		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			return v3(.1f).col();
		}
	}
}
}
