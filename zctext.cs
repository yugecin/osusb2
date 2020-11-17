using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	class Zctext : Z {

		public static string[] luki = {
			"xxx                     xxx                             ",
			"xxx                     xxx                             ",
			"xxx                     xxx                             ",
			"xxx         xxx    xxx  xxx   xxx     xxxx    xxx xxxx  ",
			"xxx         xxx    xxx  xxx  xxx    xxxxxxx   xxxxxxxxx ",
			"xxx         xxx    xxx  xxx xxx     xx   xxx  xxxx  xxxx",
			"xxx         xxx    xxx  xxxxxx     xxx   xxx  xxx    xxx",
			"xxx         xxx    xxx  xxxxxx     xxxxxxxxx  xxx    xxx",
			"xxx         xxx    xxx  xxxxxxx    xxxxxxxxx  xxx    xxx",
			"xxx         xxx    xxx  xxx xxx    xxx        xxx    xxx",
			"xxx         xxx    xxx  xxx  xxx   xxx        xxx    xxx",
			"xxxxxxxxxx  xxxx  xxxx  xxx  xxx    xxx  xxx  xxx    xxx",
			"xxxxxxxxxx   xxxxxxxxx  xxx   xxx   xxxxxxx   xxx    xxx",
			"xxxxxxxxxx    xxxx xxx  xxx   xxx     xxxx    xxx    xxx",
		};
		public static string[] herakles = {
			"xxxxx     xxxxx          xxxxxxxxx    xxx                    xxx                                 xxxxxxxxxx               xxxxx           xxx  xxx",
			"xxxxx     xxxxx          xxxxxxxxxx   xxx                    xxx                                 xxxxxxxxxx              xxxxxx           xxx  xxx",
			"xxxxxx   xxxxxx          xxxxxxxxxxx  xxx                                                        xxxxxxxxxx              xxx              xxx  xxx",
			"xxx xx   xx xxx  xxx xxx xxx     xxx  xxx xxxx       xxxx    xxx  xxx xxxx       xxxx    xxx xxx       xxxx  xxx    xxx xxxxxx   xxxxx    xxx  xxx",
			"xxx xx   xx xxx  xxxxxxx xxx     xxx  xxxxxxxxxx   xxxxxxx   xxx  xxxxxxxxx    xxxxxxx   xxxxxxx      xxxx   xxx    xxx xxxxxx  xxxxxxx   xxx  xxx",
			"xxx xx   xx xxx  xxxx    xxx     xxx  xxxx   xxx   xx   xxx  xxx  xxxx  xxxx   xx   xxx  xxxx        xxxx    xxx    xxx  xxx   xx    xxx  xxx  xxx",
			"xxx xxx xxx xxx  xxx     xxxxxxxxxx   xxx    xxx  xxx   xxx  xxx  xxx    xxx  xxx   xxx  xxx        xxxx     xxx    xxx  xxx        xxxx  xxx  xxx",
			"xxx  xx xx  xxx  xxx     xxxxxxxxxx   xxx    xxx  xxxxxxxxx  xxx  xxx    xxx  xxxxxxxxx  xxx       xxxx      xxx    xxx  xxx     xxxxxxx  xxx  xxx",
			"xxx  xx xx  xxx  xxx     xxxxxxxx     xxx    xxx  xxxxxxxxx  xxx  xxx    xxx  xxxxxxxxx  xxx      xxxx       xxx    xxx  xxx    xxxx xxx  xxx  xxx",
			"xxx  xx xx  xxx  xxx     xxx  xxxx    xxx    xxx  xxx        xxx  xxx    xxx  xxx        xxx     xxxx        xxx    xxx  xxx   xxx   xxx  xxx  xxx",
			"xxx  xxxxx  xxx  xxx     xxx   xxxx   xxx    xxx  xxx        xxx  xxx    xxx  xxx        xxx    xxxx         xxx    xxx  xxx   xxx   xxx  xxx  xxx",
			"xxx   xxx   xxx  xxx     xxx    xxxx  xxx    xxx   xxx  xxx  xxx  xxx    xxx   xxx  xxx  xxx    xxxxxxxxxxx  xxxx  xxxx  xxx   xxx  xxxx  xxx  xxx",
			"xxx   xxx   xxx  xxx     xxx    xxxx  xxx    xxx   xxxxxxx   xxx  xxx    xxx   xxxxxxx   xxx    xxxxxxxxxxx   xxxxxxxxx  xxx    xxxxxxxx  xxx  xxx",
			"xxx   xxx   xxx  xxx     xxx     xxxx xxx    xxx     xxxx    xxx  xxx    xxx     xxxx    xxx    xxxxxxxxxxx    xxxx xxx  xxx     xxx  xxx xxx  xxx",
		};
		public static string[] em = {
			"xxxxxxxxxxx                    xxx  xxx                 xx     xxxxxx",
			"xxxxxxxxxxx                    xxx  xxx                 xx    xxxxxxxxx",
			"xxxxxxxxxxx                         xxx                 xx   xxxxxxxxxx",
			"xxx          xxx xxx    xxx    xxx  xxx xxx     xxx     xx   xxx    xxxx  xxx    xxx  xxx xxxx    xxx xxxx   xxx     xxx",
			"xxx          xxxxxxxx  xxxxx   xxx  xxx xxx     xxx     xx   xxx     xxx  xxx    xxx  xxxxxxxxx   xxxxxxxxx  xxx     xxx",
			"xxxxxxxxxx   xxxx   xxxx  xxx  xxx  xxx  xxx   xxx      xx   xxxxx        xxx    xxx  xxxx  xxxx  xxxx  xxx   xxx   xxx",
			"xxxxxxxxxx   xxx    xxx   xxx  xxx  xxx  xxx   xxx      xx    xxxxxxx     xxx    xxx  xxx    xxx  xxx    xxx  xxx   xxx",
			"xxxxxxxxxx   xxx    xxx   xxx  xxx  xxx  xxx   xxx      xx     xxxxxxxx   xxx    xxx  xxx    xxx  xxx    xxx  xxx   xxx",
			"xxx          xxx    xxx   xxx  xxx  xxx   xxx xxx       xx         xxxxx  xxx    xxx  xxx    xxx  xxx    xxx   xxx xxx",
			"xxx          xxx    xxx   xxx  xxx  xxx   xxx xxx       xx   xxx     xxx  xxx    xxx  xxx    xxx  xxx    xxx   xxx xxx",
			"xxx          xxx    xxx   xxx  xxx  xxx    xx xx        xx   xxxx    xxx  xxx    xxx  xxx    xxx  xxx    xxx    xx xx",
			"xxxxxxxxxxx  xxx    xxx   xxx  xxx  xxx    xxxxx        xx    xxxxxxxxxx  xxxx  xxxx  xxx    xxx  xxxx  xxx     xxxxx",
			"xxxxxxxxxxx  xxx    xxx   xxx  xxx  xxx    xxxxx        xx    xxxxxxxxx    xxxxxxxxx  xxx    xxx  xxxxxxxxx     xxxxx",
			"xxxxxxxxxxx  xxx    xxx   xxx  xxx  xxx     xxx         xx      xxxxxx      xxxx xxx  xxx    xxx  xxx xxxx       xxx",
			"                                            xxx         xx                                        xxx            xxx",
			"                                           xxx          xx                                        xxx           xxx",
			"                                         xxxxx          xx                                        xxx         xxxxx",
			"                                         xxxx           xx                                        xxx         xxxx",
		};
		public static string[] quack = {
			"xxx    xxx    xxx xxx             xxx",
			"xxx    xxx    xxx xxx             xxx",
			"xxx   xxxxx   xxx                 xxx",
			" xxx  xxxxx  xxx  xxx     xxxx    xxx   xxx  xxx    xxx",
			" xxx  xx xx  xxx  xxx   xxxxxxx   xxx  xxx   xxx    xxx",
			" xxx  xx xx  xxx  xxx   xx   xxx  xxx xxx    xxx    xxx",
			" xxx xxx xxx xxx  xxx  xxx   xxx  xxxxxx     xxx    xxx",
			" xxx xxx xxx xxx  xxx  xxxxxxxxx  xxxxxx     xxx    xxx",
			" xxx xx   xx xxx  xxx  xxxxxxxxx  xxxxxxx    xxx    xxx",
			" xxx xx   xx xxx  xxx  xxx        xxx xxx    xxx    xxx",
			"  xxxxx   xxxxx   xxx  xxx        xxx  xxx   xxx    xxx",
			"  xxxxx   xxxxx   xxx   xxx  xxx  xxx  xxx   xxxx  xxxx",
			"  xxxx     xxxx   xxx   xxxxxxx   xxx   xxx   xxxxxxxxx",
			"  xxxx     xxxx   xxx     xxxx    xxx   xxx    xxxx xxx",

		};

		Odot[] dots;
		vec3[] pos;
		vec3[] tripos;
		vec3[] _tripos;
		Tri tri;

		public const float SIZE = 0.3f;

		public static vec3 position = v3();
		public static vec4 rotation = v4();

		public Zctext(int start, int stop, string[] text) {
			this.start = start;
			this.stop = stop;
			framedelta = 50;

			int numdots = 0;
			foreach (string line in text) {
				numdots += line.Replace(" ", "").Length;
			}
			dots = new Odot[numdots];
			pos = new vec3[numdots];

			tripos = new vec3[] { v3(0f), v3(10f, 0f, 0f), v3(10f, 0f, -10f) };
			_tripos = new vec3[tripos.Length];
			tri = new Tri(this, Color.Wheat, _tripos, 0, 1, 2);

			float maxx = 0f;
			float minz = 0f;
			int idx = 0;
			for (int i = 0; i < text.Length; i++ ) {
				for (int j = 0; j < text[i].Length; j++) {
					if (text[i][j] == 'x') {
						dots[idx] = new Odot("2", 0);
						pos[idx] = v3(j, 0f, -i) * SIZE;
						maxx = max(maxx, pos[idx].x);
						minz = min(minz, pos[idx].z);
						idx++;
					}
				}
			}
			for (int i = 0; i < pos.Length; i++) {
				pos[i].x -= maxx / 2;
				pos[i].z -= minz / 2;
			}
			/*
			switch (rotation) {
			case 1:
				turn(pos, v3(0f), quat(0f, 0f, 1f));
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			}*/
			//move(pos, mid);
		}


		public override void draw(SCENE scene)
		{
			ICommand.round_move_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_scale_decimals.Push(DECIMALS_PRECISE);
			ICommand.round_rot_decimals.Push(DECIMALS_PRECISE);
			vec3[] vecs = new vec3[1];
			for (int i = 0; i < dots.Length; i++) {
				vecs[0] = v3(pos[i]);
				turn(vecs, v3(), rotation);
				move(vecs, position);
				move(vecs, Zcamera.mid);
				Zcamera.adjust(vecs);
				copy(_tripos, tripos);
				turn(_tripos, v3(), rotation);
				move(_tripos, position);
				move(_tripos, Zcamera.mid);
				Zcamera.adjust(_tripos);
				if (tri.shouldcull()) {
					dots[i].update(scene.time, v4(1f), null);
				} else {
					dots[i].update(scene.time, v4(1f), project(vecs[0]));
					dots[i].draw(scene.g);
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
			foreach (Odot dot in dots) {
				dot.fin(w);
			}
			ICommand.round_move_decimals.Pop();
			ICommand.round_scale_decimals.Pop();
			ICommand.round_rot_decimals.Pop();
		}

	}
}
}
