using System;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	class Zairport2 : Z {

		const int SIZE2 = 100;
		const int RW_WIDTH2 = 20;
		const float Z = -20f;

		vec3[] points;
		vec3[] _points;
		MultiRect[] rects;

		public Zairport2(int start, int stop) {
			this.start = start;
			this.stop = stop;
			framedelta = 50;

			Color colrunway = Color.FromArgb(0xFF, 0x32, 0x37, 0x39);
			Color colgrass = Color.FromArgb(0xFF, 0x3D, 0x87, 0x53);
			Color coldarkgrass = Color.FromArgb(0xFF, 0x30, 0x68, 0x40);

			points = new vec3[] {
				v3(-SIZE2, SIZE2, Z),
				v3(SIZE2, SIZE2, Z),
				v3(-SIZE2, -SIZE2, Z),
				v3(SIZE2, -SIZE2, Z),
				v3(-SIZE2, RW_WIDTH2, Z),
				v3(SIZE2, RW_WIDTH2, Z),
				v3(-SIZE2, -RW_WIDTH2, Z),
				v3(SIZE2, -RW_WIDTH2, Z),
			};
			_points = new vec3[points.Length];
			move(points, Zcamera.mid);
			copy(_points, points);
			rects = new MultiRect[] {
				new MultiRect(new Rect(null, colgrass, _points, 0, 1, 2, 3)),
				new MultiRect(new Rect(null, colrunway, _points, 4, 5, 6, 7)),	
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
