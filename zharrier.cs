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

		const int SPECTRUM_HEIGHT = 10;
		const int SPECTRUM_WIDTH = 15;

		/*
		public static vec3 position;
		public static float pitch;
		public static float pitch2;
		public static float roll;
		public static float yaw;
		*/

		vec3[] points;
		vec3[] _points;
		Otri2[] tris;
		Oline[] lines;
		Odot[] dots;
		Pixelscreen pixelscreen = new Pixelscreen(/*widescreen mode*/854/2, 480/2, 2);
		public static Odot dot;

		Odot[][] spectrumdots;
		Odot[][] spectrumdots2;

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
			move(points, v3(15f, 0f, 0f));
			spectrumdots = new Odot[SPECTRUM_HEIGHT][];
			spectrumdots2 = new Odot[SPECTRUM_HEIGHT][];
			for (int i = 0; i < SPECTRUM_HEIGHT; i++) {
				spectrumdots[i] = new Odot[SPECTRUM_WIDTH];
				spectrumdots2[i] = new Odot[SPECTRUM_WIDTH];
				for (int j = 0; j < SPECTRUM_WIDTH; j++) {
					spectrumdots[i][j] = new Odot("", 0);
					spectrumdots2[i][j] = new Odot("", 0);
				}
			}
		}

		public override void draw(SCENE scene)
		{
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);

			if (!rendering) {
				dot.update(scene.time, v4(1f, 0, 0, 1f), project(sunpos));
				dot.draw(scene.g);
			}

			copy(_points, points);
			turn(_points, v3(0f), quat(pitch2.valueAt(scene.time) * -2f, 0f, 0f));
			turn(_points, v3(0f), quat(0f, roll.valueAt(scene.time) * 5f, 0f));
			turn(_points, v3(0f), quat(pitch.valueAt(scene.time) * -5f, 0f, 0f));
			turn(_points, v3(0f), quat(0f, 0f, yaw.valueAt(scene.time) * 5f));
			move(_points, getHarrPos(scene.time));
			/*
			move(_points, position);
			turn(_points, position, quat(pitch2, 0f, 0f));
			turn(_points, position, quat(0f, roll, 0f));
			turn(_points, position, quat(pitch, 0f, 0f));
			turn(_points, position, quat(0f, 0f, yaw));
			*/
			move(_points, Zcamera.mid);
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
					//vec3 p = harr.points[i];
					//vec3[] _p = new vec3[] { p };
					//move(_p, Zcamera.mid);
					//Zcamera.adjust(_p);
					vec4 px = project(_points[i]);
					if (udata[6] > 0 && px.w > 0) {
						scene.g.DrawString(i.ToString(), font, new SolidBrush(Color.White), px.x + 1, px.y + 1);
						scene.g.DrawString(i.ToString(), font, new SolidBrush(Color.Blue), px.x, px.y);
					}
				}
			}

			if (28000 < scene.time && scene.time < 31458) {
				float[] fftval = fft.SmoothValue(scene.time).values;
				for (int i = 0; i < SPECTRUM_HEIGHT; i++) {
					float yval = (i + 1) / (float) (SPECTRUM_HEIGHT + 1);
					float y = i / (float)(SPECTRUM_HEIGHT - 1) * 0.8f + 0.1f;
					float r = clamp(yval * 2f, 0f, 1f);
					float g = 1f - clamp((yval - .5f) * 2f, 0f, 1f);
					vec4 col = v4(r, g, 0f, 1f);
					for (int j = 0; j < SPECTRUM_WIDTH; j++) {
						float x = j / (float)(SPECTRUM_WIDTH - 1) * 0.9f + 0.05f;
						float fftvalx = eq_out_circ(fftval[j]);//clamp(0f, 1f, fftval[j] * 10f);
						if (!tris[22].tri.shouldcull() && yval <= fftvalx) {
							vec3 a = lerp(_points[80], _points[81], y);
							vec3 b = lerp(_points[79], _points[78], y);
							vec4 p = project(lerp(a, b, x));
							spectrumdots[i][j].update(scene.time, col, p);
							spectrumdots[i][j].draw(scene.g);
						} else {
							spectrumdots[i][j].update(scene.time, col, null);
						}
						if (!tris[14].tri.shouldcull() && yval <= fftvalx) {
							vec3 a = lerp(_points[77], _points[76], y);
							vec3 b = lerp(_points[75], _points[74], y);
							vec4 p = project(lerp(a, b, x));
							spectrumdots2[i][j].update(scene.time, col, p);
							spectrumdots2[i][j].draw(scene.g);
						} else {
							spectrumdots2[i][j].update(scene.time, col, null);
						}
					}
				}
			}

			if (!rendering) {
				vec3[] v2 = new vec3[] { harrpoint };
				turn(v2, v3(0f), quat(pitch2.valueAt(scene.time) * -2f, 0f, 0f));
				turn(v2, v3(0f), quat(0f, roll.valueAt(scene.time) * 5f, 0f));
				turn(v2, v3(0f), quat(pitch.valueAt(scene.time) * -5f, 0f, 0f));
				turn(v2, v3(0f), quat(0f, 0f, yaw.valueAt(scene.time) * 5f));
				move(v2, getHarrPos(scene.time));
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
			foreach (Odot[] dots in spectrumdots) {
				foreach (Odot dot in dots) {
					dot.fin(w);
				}
			}
			foreach (Odot[] dots in spectrumdots2) {
				foreach (Odot dot in dots) {
					dot.fin(w);
				}
			}
			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

	}
}
}
