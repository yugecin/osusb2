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

			int a, b, c, d, e, f, g, h, j, k;

			points = new vec3[19 * 4 + /*hangar*/10 + /*hangar*/10];
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
			for (i = a; i < a + 10; i++) {
				points[i] += v3(-100f, -200f, 0f);
				points[i + 10] = points[i] + v3(200f, 0f, 0f);
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
				new MultiRect(new Rect(null, colhangarl, _points, h, g, d, c)), // front
				new MultiRect(new Rect(null, colhangarl, _points, j, j, h, g)), // front top
				new MultiRect(new Rect(null, colhangarl, _points, e, f, a, b)), // back
				new MultiRect(new Rect(null, colhangarl, _points, k, k, e, f)), // back top
				new MultiRect(new Rect(null, colhangard, _points, f, h, b, d)), // side
				new MultiRect(new Rect(null, colhangard, _points, g, e, c, a)), // other side
				new MultiRect(new Rect(null, colhangard2, _points, j, k, g, e)), // top side
				new MultiRect(new Rect(null, colhangard3, _points, k, j, f, h)),// top other side
				new MultiRect(new Rect(null, colhangarl, _points, h+10, g+10, d+10, c+10)), // front
				new MultiRect(new Rect(null, colhangarl, _points, j+10, j+10, h+10, g+10)), // front top
				new MultiRect(new Rect(null, colhangarl, _points, e+10, f+10, a+10, b+10)), // back
				new MultiRect(new Rect(null, colhangarl, _points, k+10, k+10, e+10, f+10)), // back top
				new MultiRect(new Rect(null, colhangard, _points, f+10, h+10, b+10, d+10)), // side
				new MultiRect(new Rect(null, colhangard, _points, g+10, e+10, c+10, a+10)), // other side
				new MultiRect(new Rect(null, colhangard2, _points, j+10, k+10, g+10, e+10)), // top side
				new MultiRect(new Rect(null, colhangard3, _points, k+10, j+10, f+10, h+10)),// top other side
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
