using System;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	class Zairport : Z {

		const int TILES_X = 20;
		const int TILES_Y = 20;
		const int TILE_SIZE = 100;
		const float Z = -20f;

		vec3[] points;
		vec3[] _points;
		Orect[] rects;

		public Zairport(int start, int stop) {
			this.start = start;
			this.stop = stop;
			framedelta = 50;

			Color colrunway = Color.FromArgb(0xFF, 0x32, 0x37, 0x39);
			Color colgrass = Color.FromArgb(0xFF, 0x3D, 0x87, 0x53);
			Color coldarkgrass = Color.FromArgb(0xFF, 0x30, 0x68, 0x40);

			rects = new Orect[TILES_X * TILES_Y];
			points = new vec3[rects.Length * 4];
			_points = new vec3[points.Length];
			for (int x = 0, j = 0; x < TILES_X; x++) {
				for (int y = 0; y < TILES_Y; y++) {
					Color col = ((x + y) % 2) == 0 ? colgrass : coldarkgrass;
					if ((y == TILES_Y / 2 || y == TILES_Y / 2 - 1) &&
						x >= TILES_X / 2 - 10 &&
						x < TILES_X / 2 + 10)
					{
						col = colrunway;
					}
					rects[x * TILES_X + y] = new Orect(new Rect(null, col, _points, j, j + 1, j + 2, j + 3), 0);
					float xmin = (-TILES_X / 2 * TILE_SIZE  + x * TILE_SIZE);
					float ymin = (-TILES_Y / 2 * TILE_SIZE + y * TILE_SIZE);
					points[j++] = v3(xmin, ymin, Z);
					points[j++] = v3(xmin, ymin + TILE_SIZE, Z);
					points[j++] = v3(xmin + TILE_SIZE, ymin, Z);
					points[j++] = v3(xmin + TILE_SIZE, ymin + TILE_SIZE, Z);
				}
			}
			move(points, Zcamera.mid);
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
