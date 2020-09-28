using System;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	class Zairport : Z {

		vec3[] points;
		vec3[] _points;
		Orect[] rects;

		public Zairport(int start, int stop) {
			this.start = start;
			this.stop = stop;
			framedelta = 50;

			Color colrunway = Color.FromArgb(0xFF, 0x32, 0x37, 0x39);
			Color colgrass = Color.FromArgb(0xFF, 0x3D, 0x87, 0x53);

			rects = new Orect[3];
			points = new vec3[rects.Length * 4];
			_points = new vec3[points.Length];
			points = new vec3[] {
				v3(-5.0f, -20.0f, 0f),
				v3(-5.0f, 20.0f, 0f),
				v3(5.0f, -20.0f, 0f),
				v3(5.0f, 20.0f, 0f),
				
				v3(-20.0f, -20.0f, 0f),
				v3(-20.0f, 20.0f, 0f),
				v3(-5.0f, -20.0f, 0f),
				v3(-5.0f, 20.0f, 0f),
				
				v3(5.0f, -20.0f, 0f),
				v3(5.0f, 20.0f, 0f),
				v3(20.0f, -20.0f, 0f),
				v3(20.0f, 20.0f, 0f),
			};
			rects = new Orect[] {
				//new Orect(new Rect(null, Color.White, _points, 0, 1, 2, 3), 0),
				new Orect(new Rect(null, colrunway, _points, 0, 1, 2, 3), 0),
				new Orect(new Rect(null, colgrass, _points, 4, 5, 6, 7), 0),
				new Orect(new Rect(null, colgrass, _points, 8, 9, 10, 11), 0),
			};
			move(points, Zcamera.mid);
			move(points, v3(0, 0, -10));
			//move(points, v3(v2(SIZE / 2f) * -SPACING, 0f));
		}

		public override void draw(SCENE scene) {
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);
			
			copy(_points, points);
			Zcamera.adjust(_points);

			foreach (Orect r in rects) {
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
			foreach (Orect r in rects) {
				r.fin(w);
			}
			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

	}
}
}
