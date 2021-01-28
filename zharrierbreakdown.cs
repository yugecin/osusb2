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
			"145 tris (x2)",
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

		Odot[] sizetext;
		vec4[] sizetextloc;

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

			sizetext = new Odot[8/*charwidth*/ * 8/*charheight*/ * 10];
			for (int i = 0; i < sizetext.Length; i++) {
				sizetext[i] = new Odot("2", 0);
			}
			sizetextloc = new vec4[sizetext.Length];
			for (int i = 0; i < sizetextloc.Length; i++) {
				sizetextloc[i] = v4(500f, 450f, 1f, 1f);
				sizetextloc[i].x += (i % 80) * TEXTSPACING;
				sizetextloc[i].y += (i / 80 - 6) * TEXTSPACING - 2;
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

			float prg = progressx(71208, scene.endtime, scene.time);
			turn(_points, Zcamera.mid, quat(0f, 0f, cos(prg * 40f) * .18f));
			turn(_points, Zcamera.mid, quat(0f, -cos(prg * 30f) * .18f, 0f));

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
				for (int i = 0; i < lines.Length; i++) {
					int mod = 0;
					if (t > 67916) mod++;
					if (t > 68333) mod++;
					if (t > 68750) mod++; // 1
					if (t > 68750 + (69416 - 68750) / 5 * 1) mod++; // 2
					if (t > 68750 + (69416 - 68750) / 5 * 2) mod++; // 3
					if (t > 68750 + (69416 - 68750) / 5 * 3) mod++; // 4
					if (t > 68750 + (69416 - 68750) / 5 * 4) mod++; // 5
					if (t > 69416) mod++; // 6
					if ((i % 8) < mod) {
						lines[i].update(scene.time, v4(1f));
						lines[i].draw(scene.g);
					}
				}

				for (int i = 0; i < dots.Length; i++) {
					int mod = 0;
					if (t > 69666) mod++;
					if (t > 70041) mod++;
					if (t > 70458) mod++; // 1
					if (t > 70458 + (71208 - 70458) / 5 * 1) mod++; // 2
					if (t > 70458 + (71208 - 70458) / 5 * 2) mod++; // 3
					if (t > 70458 + (71208 - 70458) / 5 * 3) mod++; // 4
					if (t > 70458 + (71208 - 70458) / 5 * 4) mod++; // 5
					if (t > 71208) mod++; // 6
					if ((i % 8) < mod) {
						dots[i].update(scene.time, v4(1f, 1f, 0f, 1f), project(_points[i]));
						dots[i].draw(scene.g);
					}
				}
			//}

			if (t < 71208) {
				goto skipnonlinenondots;
			}

			// tris
			float faster = 1.2f;
			if (t < 96080) {
				Otri2.custom_position = v2(200f, 100f);
				Otri2.position_factor = progressxy(71208, 72958, scene.time, faster);
				Otri2.rotation_factor = progressxy(72958, 74583, scene.time, faster);
				Otri2.scale_factor = progressxy(74583, 76166, scene.time, faster);
				showmov |= t < 76166 && t >= 71208;
				showrot |= t < 76166 && t >= 72958;
				showscale |= t < 76166 && t >= 74583;
				drawtri(tris[129], scene, pixelscreen);
				Otri2.custom_position = v2(325f, 100f);
				Otri2.position_factor = progressxy(77875, 79500, scene.time, faster);
				Otri2.rotation_factor = progressxy(79500, 81125, scene.time, faster);
				Otri2.scale_factor = progressxy(81125, 82791, scene.time, faster);
				showmov |= t < 82791 && t >= 77875;
				showrot |= t < 82791 && t >= 79500;
				showscale |= t < 82791 && t >= 81125;
				drawtri(tris[108], scene, pixelscreen);
				Otri2.custom_position = v2(450f, 100f);
				Otri2.position_factor = progressxy(84458, 86125, scene.time, faster);
				Otri2.rotation_factor = progressxy(86125, 87791, scene.time, faster);
				Otri2.scale_factor = progressxy(87791, 89375, scene.time, faster);
				showmov |= t < 89375 && t >= 84458;
				showrot |= t < 89375 && t >= 86125;
				showscale |= t < 89375 && t >= 87791;
				drawtri(tris[135], scene, pixelscreen);
				Otri2.custom_position = v2(575f, 100f);
				Otri2.position_factor = progressxy(91040, 92792, scene.time, faster);
				Otri2.rotation_factor = progressxy(92792, 94333, scene.time, faster);
				Otri2.scale_factor = progressxy(94333, 96080, scene.time, faster);
				showmov |= t < 96080 && t >= 91040;
				showrot |= t < 96080 && t >= 92792;
				showscale |= t < 96080 && t >= 94333;
				drawtri(tris[103], scene, pixelscreen);
				Otri2.position_factor = Otri2.rotation_factor = Otri2.scale_factor = 1f;
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
			bool flicker_text_state = EW_STATE_FLICKER_TEXT_SHOWN || t < 100208 || 101041 < t;

			for (int i = 0; i < statictext.Length; i++) {
				bool show =
					(!movtext[i] || showmov) &&
					(!rottext[i] || showrot) &&
					(!scaletext[i] || showscale) &&
					(!ftext[i] || flicker_text_state);

				if (show) {
					statictext[i].update(scene.time, v4(1f), v4(textloc[i], 1f, 1f));
					statictext[i].draw(scene.g);
				} else {
					statictext[i].update(scene.time, null, null);
				}
			}

			int size = 0;
			foreach (Otri2 tri in tris) {
				size += tri.calcStoryboardCommandSize();
			}
			if (!rendering) {
				size = 2030333;
			}
			string sizestr = string.Format("{0,8:###0.000}", (size / 1000f));

			bool[] sizetextshown = new bool[sizetext.Length];
			if (flicker_text_state) {
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
			}
			for (int i = 0; i < sizetextshown.Length; i++) {
				if (!sizetextshown[i]) {
					sizetext[i].update(scene.time, null, null);
				}
			}
skipnonlinenondots:

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
			foreach (Odot d in sizetext) {
				d.fin(w);
			}
			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

	}
}
}
