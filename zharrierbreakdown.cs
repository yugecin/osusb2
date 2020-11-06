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

		const int TEXTSPACING = 3;
		string[] text = {
			"How to 3d:",
			"Move",
			"Rotate",
			"Scale",
			"82 verts",
			"215 lines",
			"145 tris",
			"triangle commands size (KB):",
		};
		vec2[] textlocstart = {
			v2(-75f, 50f),
			v2(-75f, 150f),
			v2(-75f, 180f),
			v2(-75f, 210f),
			v2(-75f, 300f),
			v2(-75f, 330f),
			v2(-75f, 360f),
			v2(-75f, 450f),
		};
		vec2 textoffset = v2(-10f, -20f);
		Odot[] statictext;
		vec2[] textloc;
		bool[] movtext;
		bool[] rottext;
		bool[] scaletext;
		bool[] ftext;

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
			movtext = new bool[pointcount];
			rottext = new bool[pointcount];
			scaletext = new bool[pointcount];
			ftext = new bool[pointcount];

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
								statictext[idx] = new Odot("2", 0);
								movtext[idx] = q == 1;
								rottext[idx] = q == 2;
								scaletext[idx] = q == 3;
								ftext[idx] = q == 7;
								idx++;
							}
						}
					}
					xoff += cw + 1;
				}
			}
		}

		private void drawtri(Otri2 t, SCENE scene, Pixelscreen pixelscreen)
		{
			if (pixelscreen.hasOwner(t)) {
				t.update(scene);
			} else {
				t.cullframe(scene);
			}
		}

		bool EW_STATE_FLICKER_TEXT_SHOWN; // state sux but it has to be perfect yknow
		int lastshakeytime;

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

			bool showmov = false, showrot = false, showscale = false;

			// shakes
			if (76166 <= t && t < 77208 ||
				82791 <= t && t < 83875 ||
				89375 <= t && t < 90458) {
				move(_points, v3(.5f * cos(scene.time * 368f)));
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
				showmov = true;
				Otri2.rotation_factor = progressxy(72958, 74583, scene.time, faster);
				Otri2.scale_factor = progressxy(74583, 76166, scene.time, faster);
				showrot |= t < 77875 && t > 72958;
				showscale |= t < 77875 && t > 74583;
				drawtri(tris[129], scene, pixelscreen);
				Otri2.rotation_factor = Otri2.scale_factor = 1f;
				if (t > 77875) {
					Otri2.rotation_factor = progressxy(79500, 81125, scene.time, faster);
					Otri2.scale_factor = progressxy(81125, 82791, scene.time, faster);
					showrot |= t < 84458 && t > 79500;
					showscale |= t < 84458 && t > 81125;
					drawtri(tris[108], scene, pixelscreen);
					Otri2.rotation_factor = Otri2.scale_factor = 1f;
				}
				if (t > 84458) {
					Otri2.rotation_factor = progressxy(86125, 87791, scene.time, faster);
					Otri2.scale_factor = progressxy(87791, 89375, scene.time, faster);
					showrot |= t < 91040 && t > 86125;
					showscale |= t < 91040 && t > 87791;
					drawtri(tris[135], scene, pixelscreen);
					Otri2.rotation_factor = Otri2.scale_factor = 1f;
				}
				if (t> 91040) {
					Otri2.rotation_factor = progressxy(92792, 94333, scene.time, faster);
					Otri2.scale_factor = progressxy(94333, 96080, scene.time, faster);
					showrot |= t > 92792;
					showscale |= t > 94333;
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

			// limit to 40ms flickers, we don't want to give high fps peoples epilepsy (and it looks bad)
			if (scene.time - lastshakeytime > 40) {
				EW_STATE_FLICKER_TEXT_SHOWN = !EW_STATE_FLICKER_TEXT_SHOWN;
				lastshakeytime = scene.time;
			}

			for (int i = 0; i < statictext.Length; i++) {
				bool show =
					(!movtext[i] || showmov) &&
					(!rottext[i] || showrot) &&
					(!scaletext[i] || showscale) &&
					(!ftext[i] || EW_STATE_FLICKER_TEXT_SHOWN || t < 100208 || 101041 < t);

				if (show) {
					statictext[i].update(scene.time, v4(1f), v4(textloc[i], 1f, 1f));
					statictext[i].draw(scene.g);
				} else {
					statictext[i].update(scene.time, null, null);
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
			foreach (Odot d in statictext) {
				d.fin(w);
			}
			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

	}
}
}
