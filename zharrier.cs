using System;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all{
	public static vec3 harrpoint;
	public delegate void harrupdate();
	public static harrupdate haha;
	public static vec3 sunpos = v3(0f); // see zcamera
	class Zharrier : Z {

		vec3[] points;
		vec3[] _points;
		Otri2[] tris;
		Oline[] lines;
		Odot[] dots;
		Pixelscreen pixelscreen = new Pixelscreen(/*widescreen mode*/854/2, 480/2, 2);
		public static Odot dot;

		public Zharrier(int start, int stop) {

			this.start = start;
			this.stop = stop;
			framedelta = 50;

			haha = create;

			dot = new Odot("", 0);
			harrpoint = v3(0f);

			create();
		}

		public void create()
		{
			points = copy(harr.points);
			_points = new vec3[points.Length];
			dots = new Odot[harr.points.Length];
			for (int i = 0; i < dots.Length; i++) {
				dots[i] = new Odot("", 0);
			}
			lines = new Oline[harr.lines.Length];
			for (int i = 0; i < lines.Length; i++) {
				lines[i] = new Oline(_points, harr.lines[i][0], harr.lines[i][1]);
			}
			int s = Orect.SETTING_SHADED;
			tris = new Otri2[harr.tris.Length];
			for (int i = 0; i < tris.Length; i++) {
				tris[i] = new Otri2(new Tri(null,
					harr.cols[harr.tris[i][0]],
					_points,
					harr.tris[i][1],
					harr.tris[i][2],
					harr.tris[i][3]
					), s);
			}
			move(points, Zcamera.mid);
		}

		public override void draw(SCENE scene) {
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);

			if (!rendering) {
				dot.update(scene.time, v4(1f, 0, 0, 1f), project(sunpos));
				dot.draw(scene.g);
			}

			copy(_points, points);
			Zcamera.adjust(_points);

			pixelscreen.clear();
			foreach (Otri2 t in tris) {
				pixelscreen.tri(
					t,
					new vec4[] {
						project(t.tri.points[t.tri.a]),
						project(t.tri.points[t.tri.b]),
						project(t.tri.points[t.tri.c])
					});
			}
			//pixelscreen.draw(scene);

			foreach (Otri2 t in tris) {
				if (pixelscreen.hasOwner(t)) {
					t.update(scene);
				} else {
					t.cullframe(scene);
				}
			}

			foreach (Oline l in lines) {
				l.update(scene.time, v4(1f));
				if (udata[5] > 0) {
					l.draw(scene.g);
				}
			}
			/*
			for (int i = 0; i < dots.Length; i++) {
				dots[i].update(scene.time, v4(1f, 1f, 0f, 1f), project(_points[i]));
				dots[i].draw(scene.g);
			}
			*/

			System.Drawing.Font font = new System.Drawing.Font("Tahoma", 12.0f);
			if (!rendering) {
				for (int i = 0; i < harr.points.Length; i++) {
					vec3 p = harr.points[i];
					vec3[] _p = new vec3[] { p };
					move(_p, Zcamera.mid);
					Zcamera.adjust(_p);
					vec4 px = project(_p[0]);
					if (udata[6] > 0 && px.w > 0) {
						scene.g.DrawString(i.ToString(), font, new SolidBrush(Color.White), px.x + 1, px.y + 1);
						scene.g.DrawString(i.ToString(), font, new SolidBrush(Color.Blue), px.x, px.y);
					}
				}
			}

			if (!rendering) {
				vec3[] v2 = new vec3[] { harrpoint };
				move(v2, Zcamera.mid);
				Zcamera.adjust(v2);
				dot.update(scene.time, v4(1f, 0, 0, 1f), project(v2[0]));
				dot.draw(scene.g);
			}

			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

		public override void fin(Writer w) {
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);
			//pixelscreen.fin(w);
			foreach (Oline l in lines) {
				//l.fin(w);
			}
			foreach (Otri2 t in tris) {
				t.fin(w);
			}
			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

	}
}
}
