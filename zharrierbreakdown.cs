using System;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all{
	class Zharrierbreakdown : Z {

		vec3[] points;
		vec3[] _points;
		Otri2[] tris;
		Oline[] lines;
		Odot[] dots;
		Pixelscreen pixelscreen = new Pixelscreen(/*widescreen mode*/854/2, 480/2, 2);
		public static Odot dot;

		public Zharrierbreakdown(int start, int stop)
		{
			this.start = start;
			this.stop = stop;
			framedelta = 50;

			dot = new Odot("", 0);
			harrpoint = v3(0f);

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

		private void drawtri(Otri2 t, SCENE scene, Pixelscreen pixelscreen)
		{
			if (pixelscreen.hasOwner(t)) {
				t.update(scene);
			} else {
				t.cullframe(scene);
			}
		}

		public override void draw(SCENE scene) {
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);

			copy(_points, points);

			turn(_points, Zcamera.mid, quat(0f, 0f, cos(scene.progress * 40f) * .18f));
			turn(_points, Zcamera.mid, quat(0f, -cos(scene.progress * 30f) * .18f, 0f));

			Zcamera.adjust(_points);

			pixelscreen.clear();
			foreach (Otri2 tri in tris) {
				pixelscreen.tri(
					tri,
					new vec4[] {
						project(tri.tri.points[tri.tri.a]),
						project(tri.tri.points[tri.tri.b]),
						project(tri.tri.points[tri.tri.c])
					});
			}
			//pixelscreen.draw(scene);

			int t = scene.time;

			// shakes
			if (76166 <= t && t < 77208 ||
				82791 <= t && t < 83875 ||
				89375 <= t && t < 90458) {
				move(_points, v3(.5f * cos(scene.time * 36846f)));
			}

			// dots n lines
			//if (t < 96080) {
				foreach (Oline l in lines) {
					l.update(scene.time, v4(1f));
					l.draw(scene.g);
				}

				for (int i = 0; i < dots.Length; i++) {
					dots[i].update(scene.time, v4(1f, 1f, 0f, 1f), project(_points[i]));
					dots[i].draw(scene.g);
				}
			//}

			// tris
			float faster = 1.2f;
			if (t < 96080) {
				Otri2.rotation_factor = progressxy(72958, 74583, scene.time, faster);
				Otri2.scale_factor = progressxy(74583, 76166, scene.time, faster);
				drawtri(tris[129], scene, pixelscreen);
				Otri2.rotation_factor = Otri2.scale_factor = 1f;
				if (t > 77875) {
					Otri2.rotation_factor = progressxy(79500, 81125, scene.time, faster);
					Otri2.scale_factor = progressxy(81125, 82791, scene.time, faster);
					drawtri(tris[108], scene, pixelscreen);
					Otri2.rotation_factor = Otri2.scale_factor = 1f;
				}
				if (t > 84458) {
					Otri2.rotation_factor = progressxy(86125, 87791, scene.time, faster);
					Otri2.scale_factor = progressxy(87791, 89375, scene.time, faster);
					drawtri(tris[135], scene, pixelscreen);
					Otri2.rotation_factor = Otri2.scale_factor = 1f;
				}
				if (t> 91040) {
					Otri2.rotation_factor = progressxy(92792, 94333, scene.time, faster);
					Otri2.scale_factor = progressxy(94333, 96080, scene.time, faster);
					drawtri(tris[103], scene, pixelscreen);
					Otri2.rotation_factor = Otri2.scale_factor = 1f;
				}
			} else if (t < 97666) {
				int mod = (int) (progressxy(96080, 97666, t, 3f) * 4f);
				for (int i = 0; i < tris.Length; i++ ) {
					if ((i % 5) <= mod || i == 129 || i == 108 || i == 135 || i == 103) {
						drawtri(tris[i], scene, pixelscreen);
					}
				}
			} else {
				foreach (Otri2 tri in tris) {
					drawtri(tri, scene, pixelscreen);
				}
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
				l.fin(w);
			}
			foreach (Odot d in dots) {
				d.fin(w);
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
