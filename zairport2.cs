using System;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	class Zairport2 : Z {

		const int SIZEX = 2000;
		const int SIZEY = 1000;
		const int RW_WIDTH = 100;
		const int RW_LENGTH = 1200;
		const int LINE_WIDTH = 5;
		const int H_W = 40;
		const int H_L = 40;
		const int H_H = 20;
		const int H_X = 5;
		const int T_S = 12;
		const int T_S2 = 30;
		const int T_H = 100;
		const int T_H2 = 20;
		const float Z = -20f;

		vec3[] points;
		vec3[] _points;
		MultiRect[] rects;

		private void mkrect(int index, vec2 pos, vec2 size, float z)
		{
			index *= 4;
			this.points[index++] = v3(pos.x - size.x / 2, pos.y + size.y / 2, z);
			this.points[index++] = v3(pos.x + size.x / 2, pos.y + size.y / 2, z);
			this.points[index++] = v3(pos.x - size.x / 2, pos.y - size.y / 2, z);
			this.points[index] = v3(pos.x + size.x / 2, pos.y - size.y / 2, z);
		}

		private void mkrectf(int index, vec3 pos, vec3 size)
		{
			index *= 4;
			this.points[index++] = v3(pos.x - size.x / 2, pos.y, pos.z + size.z / 2);
			this.points[index++] = v3(pos.x + size.x / 2, pos.y, pos.z + size.z / 2);
			this.points[index++] = v3(pos.x - size.x / 2, pos.y, pos.z - size.z / 2);
			this.points[index] = v3(pos.x + size.x / 2, pos.y, pos.z - size.z / 2);
		}

		public Zairport2(int start, int stop) {
			this.start = start;
			this.stop = stop;
			framedelta = 50;

			Color colrunway = Color.FromArgb(0xFF, 0x32, 0x37, 0x39);
			Color colgrass = Color.FromArgb(0xFF, 0x3D, 0x87, 0x53);
			Color coldarkgrass = Color.FromArgb(0xFF, 0x30, 0x68, 0x40);
			Color colmarkings = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
			Color colhangarl = Color.FromArgb(0xFF, 0xAF, 0x69, 0x2B);
			Color colhangard = Color.FromArgb(0xFF, 0xBC, 0x71, 0x2F);
			Color colhangard2 = Color.FromArgb(0xFF, 0xC0, 0x74, 0x32);
			Color colhangard3 = Color.FromArgb(0xFF, 0xC4, 0x78, 0x35);
			Color coltowerglass = Color.FromArgb(0xFF, 38, 139, 255);
			Color coltowerglassb = Color.FromArgb(0xFF, 49, 158, 255);
			Color coltower0 = Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0);
			Color coltower1 = Color.FromArgb(0xFF, 0xC0, 0xC0, 0xC0);
			Color coltower2 = Color.FromArgb(0xFF, 0xB4, 0xB4, 0xB4);
			Color coltower3 = Color.FromArgb(0xFF, 0x47, 0x47, 0x47);

			int a, b, c, d, e, f, g, h, j, k;
			int ta, tb, tc, td, te, tf, tg, th, ti, tj, tk, tl, tm, tn, to, tp;

			points = new vec3[33 * 4 + /*hangar*/10 + /*hangar*/10 + /*tower*/16];
			int i = 0;
			mkrect(i++, v2(0f), v2(SIZEX, SIZEY), Z);
			mkrect(i++, v2(0f), v2(RW_LENGTH + LINE_WIDTH * 3f, RW_WIDTH), Z);
			mkrect(i++, v2(0f, -RW_WIDTH / 2 + LINE_WIDTH * 2), v2(RW_LENGTH, LINE_WIDTH), Z);
			mkrect(i++, v2(0f, RW_WIDTH / 2 - LINE_WIDTH * 2), v2(RW_LENGTH, LINE_WIDTH), Z);
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 5, 0f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 5, -LINE_WIDTH * 2.5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 5, -LINE_WIDTH * 5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 5, LINE_WIDTH * 2.5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 5, LINE_WIDTH * 5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 5, 0f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 5, -LINE_WIDTH * 2.5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 5, -LINE_WIDTH * 5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 5, LINE_WIDTH * 2.5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 5, LINE_WIDTH * 5f), v2(LINE_WIDTH * 10, LINE_WIDTH), Z);
			mkrect(i++, v2(0f, 0f), v2(LINE_WIDTH * 20, LINE_WIDTH), Z);
			mkrect(i++, v2(-LINE_WIDTH * 35, 0f), v2(LINE_WIDTH * 20, LINE_WIDTH), Z);
			mkrect(i++, v2(-LINE_WIDTH * 70, 0f), v2(LINE_WIDTH * 20, LINE_WIDTH), Z);
			mkrect(i++, v2(LINE_WIDTH * 35, 0f), v2(LINE_WIDTH * 20, LINE_WIDTH), Z);
			mkrect(i++, v2(LINE_WIDTH * 70, 0f), v2(LINE_WIDTH * 20, LINE_WIDTH), Z);
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 20, -LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 0
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 24, -LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 0
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 22, -LINE_WIDTH * 3), v2(LINE_WIDTH * 3, LINE_WIDTH), Z); // 0
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 22, -LINE_WIDTH), v2(LINE_WIDTH * 3, LINE_WIDTH), Z); // 0
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 24, LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 3
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 22, LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 3
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 20, LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 3
			mkrect(i++, v2(RW_LENGTH / 2 - LINE_WIDTH * 22, LINE_WIDTH * 3), v2(LINE_WIDTH * 3, LINE_WIDTH), Z); // 3
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 24, LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 2
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 22, LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 2
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 20, LINE_WIDTH * 2), v2(LINE_WIDTH, LINE_WIDTH * 3), Z); // 2
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 23, LINE_WIDTH * 1), v2(LINE_WIDTH * 2, LINE_WIDTH), Z); // 2
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 21, LINE_WIDTH * 3), v2(LINE_WIDTH * 2, LINE_WIDTH), Z); // 2
			mkrect(i++, v2(-RW_LENGTH / 2 + LINE_WIDTH * 22, -LINE_WIDTH * 3), v2(LINE_WIDTH * 5, LINE_WIDTH), Z); // 1
			i *= 4;
			points[a = i++] = v3(-H_W, -H_L, Z);
			points[b = i++] = v3(+H_W, -H_L, Z);
			points[c = i++] = v3(-H_W, +H_L, Z);
			points[d = i++] = v3(+H_W, +H_L, Z);
			points[e = i++] = v3(-H_W, -H_L, Z + H_H);
			points[f = i++] = v3(+H_W, -H_L, Z + H_H);
			points[g = i++] = v3(-H_W, +H_L, Z + H_H);
			points[h = i++] = v3(+H_W, +H_L, Z + H_H);
			points[j = i++] = v3(0f, +H_L, Z + H_H + H_X);
			points[k = i++] = v3(0f, -H_L, Z + H_H + H_X);
			i += 10; /*hangar2*/
			points[ta = i++] = v3(T_S, T_S, Z + T_H);
			points[tb = i++] = v3(-T_S, T_S, Z + T_H);
			points[tc = i++] = v3(T_S, T_S, Z);
			points[td = i++] = v3(-T_S, T_S, Z);
			points[te = i++] = v3(T_S, -T_S, Z + T_H);
			points[tf = i++] = v3(-T_S, -T_S, Z + T_H);
			points[tg = i++] = v3(T_S, -T_S, Z);
			points[th = i++] = v3(-T_S, -T_S, Z);
			points[ti = i++] = v3(T_S2, -T_S2, Z + T_H);
			points[tj = i++] = v3(-T_S2, -T_S2, Z + T_H);
			points[tk = i++] = v3(T_S2, T_S2, Z + T_H);
			points[tl = i++] = v3(-T_S2, T_S2, Z + T_H);
			points[tm = i++] = v3(T_S2, -T_S2, Z + T_H + T_H2);
			points[tn = i++] = v3(-T_S2, -T_S2, Z + T_H + T_H2);
			points[to = i++] = v3(T_S2, T_S2, Z + T_H + T_H2);
			points[tp = i++] = v3(-T_S2, T_S2, Z + T_H + T_H2);
			for (i = a; i < a + 10; i++) {
				points[i] += v3(-300f, -200f, 0f);
				points[i + 10] = points[i] + v3(200f, 0f, 0f);
			}
			for (i = ta; i < ta + 16; i++) {
				points[i] += v3(400f, -200f - H_W / 2, 0f);
			}
			_points = new vec3[points.Length];
			move(points, Zcamera.mid);
			copy(_points, points);
			i = -4;
			rects = new MultiRect[] {
				new MultiRect(new Rect(null, colgrass, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colrunway, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				new MultiRect(new Rect(null, colmarkings, _points, i+=4,i+1,i+2,i+3)),
				// tower
				new MultiRect(new Rect(null, coltower3, _points, tj, ti, tl, tk)), // top bottom
				new MultiRect(new Rect(null, coltower1, _points, ta, tb, tc, td)), // front
				new MultiRect(new Rect(null, coltower1, _points, tf, te, th, tg)), // back
				new MultiRect(new Rect(null, coltower2, _points, tb, tf, td, th)), // side
				new MultiRect(new Rect(null, coltower2, _points, te, ta, tg, tc)), // side
				new MultiRect(new Rect(null, coltower0, _points, tm, tn, to, tp)), // top top
				new MultiRect(new Rect(null, coltowerglassb, _points, tn, tm, tj, ti)), // back window
				new MultiRect(new Rect(null, coltowerglass, _points, tp, tn, tl, tj)), // side window
				new MultiRect(new Rect(null, coltowerglass, _points, tm, to, ti, tk)), // side window
				new MultiRect(new Rect(null, coltowerglassb, _points, to, tp, tk, tl)), // font window
				// hangars
				new MultiRect(new Rect(null, colhangard, _points, f, h, b, d)), // h1 side
				new MultiRect(new Rect(null, colhangard, _points, f+10, h+10, b+10, d+10)), // h2 side
				new MultiRect(new Rect(null, colhangard, _points, g, e, c, a)), // h1 other side
				new MultiRect(new Rect(null, colhangard, _points, g+10, e+10, c+10, a+10)), // h2 other side
				new MultiRect(new Rect(null, colhangard2, _points, j, k, g, e)), // h1 top side
				new MultiRect(new Rect(null, colhangard2, _points, j+10, k+10, g+10, e+10)), // h2 top side
				new MultiRect(new Rect(null, colhangard3, _points, k, j, f, h)),// h1 top other side
				new MultiRect(new Rect(null, colhangard3, _points, k+10, j+10, f+10, h+10)), // h2 top other side
				new MultiRect(new Rect(null, colhangarl, _points, h, g, d, c)), // h1 front
				new MultiRect(new Rect(null, colhangarl, _points, j, j, h, g)), // h1 front top
				new MultiRect(new Rect(null, colhangarl, _points, h+10, g+10, d+10, c+10)), // h2 front
				new MultiRect(new Rect(null, colhangarl, _points, j+10, j+10, h+10, g+10)), // h2 front top
				new MultiRect(new Rect(null, colhangarl, _points, e, f, a, b)), // h1 back
				new MultiRect(new Rect(null, colhangarl, _points, k, k, e, f)), // h1 back top
				new MultiRect(new Rect(null, colhangarl, _points, e+10, f+10, a+10, b+10)), // h2 back
				new MultiRect(new Rect(null, colhangarl, _points, k+10, k+10, e+10, f+10)), // h2 back top
			};
			//move(points, v3(v2(SIZE / 2f) * -SPACING, 0f));
		}

		public override void draw(SCENE scene) {
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);
			
			copy(_points, points);
			Zcamera.adjust(_points);

			foreach (MultiRect r in rects) {
				r.update(scene);
			}

			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

		public override void fin(Writer w) {
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);
			foreach (MultiRect r in rects) {
				r.fin(w);
			}
			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

	}
}
}
