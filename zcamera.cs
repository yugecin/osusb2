using System;
using System.Text;
using System.Windows.Forms;

namespace osusb1 {
partial class all
{
	public static float debug_x = 0.0f, debug_y = 0.0f;

	public static void debugmove(Keys e)
	{
		if (e == Keys.Z) {
			all.debug_x -= sin(rad(mouse.x)) * 1.0f;
			all.debug_y -= cos(rad(mouse.x)) * 1.0f;
		}
		if (e == Keys.Q) {
			all.debug_x -= sin(rad(mouse.x - 90f)) * 1.0f;
			all.debug_y -= cos(rad(mouse.x - 90f)) * 1.0f;
		}
		if (e == Keys.S) {
			all.debug_x += sin(rad(mouse.x)) * 1.0f;
			all.debug_y += cos(rad(mouse.x)) * 1.0f;
		}
		if (e == Keys.D) {
			all.debug_x -= sin(rad(mouse.x + 90f)) * 1.0f;
			all.debug_y -= cos(rad(mouse.x + 90f)) * 1.0f;
		}
	}

	class Zcamera : Z {

		public static vec3 mid = v3(campos);

		public static vec3 dp = v3(0f);
		public static vec4 lquatx = v4(0f);
		public static vec4 lquaty = v4(0f);
		public static vec4 lquatz = v4(0f);

		const int T1 = 500;
		const int T2 = 125400;
		const int T3 = 129800;
		const int T4 = 134000;
		const int T5 = 138200;

		public Zcamera(int start, int stop)
		{
			this.start = start;
			this.stop = stop;
			framedelta = 15;
		}

		public override void draw(SCENE scene) {
			vec3[] sp = new vec3[] { v3(0f, 0f, 50f) };
			move(sp, Zcamera.mid);
			Zcamera.adjust(sp);
			sunpos = sp[0];

			//Zharrier.position = v3(0f);

			dp = v3(0f);
			vec3 dir = v3(0f);
			float rz = 0f;

			if (scene.time < T2) {

				// Defaults for easy camera moving around navigation:
				dir = v3(0f, 5f, -1.0f);
				dp.y = 5.0f;

				dir = v3(0f, 5f, -1.0f);
				dp.y = 25.0f;
				dp.z -= 3.0f + udata[0];
				dp.y += 2 * udata[1];
				dp.z -= 0.7f * udata[1];


				//dp.y -= lerp(0f, 20f, progress(0, 5000, scene.time));
				//dp.x -= lerp(0f, 10f, progress(0, 5000, scene.time));
				//dp.z -= lerp(10f, 20f, progress(0, 5000, scene.time));

				//dp.z -= cos(progress(0, 5000, scene.time)) * 20;
				//dp.y += -20 + 20 * sin(progress(0, 5000, scene.time));
				//dp.x += 30 * sin(progress(0, 5000, scene.time));

				//dp.y -= 70.0f;
				
				dir = dp - v3(0);

				/*
				float pr = progress(T1, T2, scene.time);
				vec3 fr = v3(25, 5f, -10f);
				vec3 to = v3(-15, 40f, -10f);
				dp += lerp(fr, to, pr);
				fr = v3(1f, 0f, 0f);
				to = v3(-.3f, 1f, 0f);
				dir = lerp(fr, to, pr);
				//rz = lerp(-PI2, 0, pr);
				*/
			} else if (scene.time < T3) {
				dir = v3(0f, 1f, 0f);
				float pr = progress(T2, T3, scene.time);
				vec3 fr = v3(Zltext.SIZE * -20, 25f, -10f);
				vec3 to = v3(Zltext.SIZE * 15, 25f, -10f);
				dp += lerp(fr, to, pr);
			} else if (scene.time < T4) {
				float pr = progress(T3, T4, scene.time);
				float frangle = -PI2;
				float toangle = PI4;
				vec3 fr = v3(0f, 25f + 10f * cos(frangle), -10f + 10f * sin(frangle));
				vec3 to = v3(0f, 25f + 10f * cos(toangle), -10f + 10f * sin(toangle));
				dp += lerp(fr, to, pr);
				dir = dp - v3(0f, 0f, -10f);
				float fx = 25;
				float tx = -5;
				dp.x = lerp(fx, tx, eq_cub(pr, v2(.5f, .6f), v2(.9f, 1f))) * Zltext.SIZE;
				rz = PI4 / 3f;
			} else {
				float pr = progress(T4, T5, scene.time);
				vec3 fr = v3(Zltext.SIZE * -35, 25f, -10f);
				vec3 to = v3(Zltext.SIZE * 35, 25f, -10f);
				dp += lerp(fr, to, eq_cub(pr, v2(.2f, .4f), v2(.9f, .6f)));
				dir = dp - v3(0f, 0f, -10f);
				dp.x += udata[0];
				dp.y += udata[1];
				dp.z += udata[2];
				//rz = lerp(-PI2, 0, pr);
				rz = 0f;
			}

			int t = scene.time;

			if (t < 5041) {
				// intro
				dp = v3(lerp(-1000f, 1000f, progress(2000, 5041, t)), 0f, 0f);
				dir = v3(10f, 0f, 0f);
			} else if (t < 9950) {
				// first circling
				float f = progress(5041, 9950, t) + .7f;
				dp = v3(500f * cos(f * 4f), 500f * sin(f * 4f), -200f);
				dir = dp - v3(0f, 0f, 0f);
			} else if (t < 11708) {
				// i present
				float f = progress(9950, 11708, t);
				dp = v3(-lerp(-520f, 520f, f), -200f, -40f);
				dir = dp - v3(-lerp(-400f, 400f, f), -450f, -20f);
			} else if (t < 14958) {
				// more showoff
				float f = progress(11708, 14666, t);
				dp = v3(lerp(500f, -500f, f), 0f, -600f);
				dir = dp - v3(lerp(500f, -500f, f), 0f, 0f);
			} else if (t < 18291) {
				// osu sb numero dos
				int nums = 11;
				float size = 0.07f;
				float f = progress(14958, 18291, t) * (1f - nums * size);
				int tt = t + 75;
				if (tt > 15375) f += size;
				for (int i = 0; i < 6; i++) {
					if (t > 15791 + i * 132) {
						f += size;
					}
				}
				if (tt > 16583) f += size;
				if (tt > 16980) f += size;
				if (tt > 17375) f += size;
				if (tt > 17833) f += size;
				dp = v3(-lerp(-700f, 650f, f), 100f, -25f);
				dir = v3(0f, 1f, 0f);
			} else if (t < 31400) {
				// harr
				float f = progressx(18291, 24583, t);
				float fat = f * 0.99f;
				float fto = clamp(f + 0.1f, 0f, 1f);
				/*
				vec3 ha = v3(-600f, 0f, 0f);
				vec3 hp1 = v3(1200f, 200f, 150f);
				vec3 hp2 = v3(1000f, -800f, 350f);
				vec3 hb = v3(-300f, -300f, 200f);

				vec3 at = cubic(ha, hb, hp1, hp2, fat);
				vec3 atn = cubic(ha, hb, hp1, hp2, clamp(fat + 0.001f, 0f, 1f));
				vec3 to = cubic(ha, hb, hp1, hp2, fto);

				Zharrier.position = at;
				Zharrier.yaw = atan2(atn.y - at.y, atn.x - at.x);
				Zharrier.pitch = -atan2(to.z - at.z, (to.xy - at.xy).length());
				Zharrier.roll = -(atan2(atn.x - at.x, atn.y - at.y) - atan2(to.x - at.x, to.y - at.y));*/

				dp = v3() - getCamPos(t);
				dir = dp + getHarrPos(t);
				//Console.WriteLine(getCamPos(t) + " " + getHarrPos(t));
			} else if (t < 56541) {
				// greets
				vec3 _00_pos = v3(-1097.832f, -1175.126f, 271.0345f);
				vec3 _00_lok = v3(-1077.637f, -1267.603f, 285.5392f);
				vec3 _01_lok = v3(0f, 0f, 100f);

				vec3 _05_pos = v3(759.1069f, -193.1827f, 289.7183f);
				vec3 _05_lok = lerp(v3(), _05_pos, 0.9f);
				_05_lok.z = _05_pos.z;
				vec3 _05_lokb = v3(_05_lok);
				_05_lok = _05_pos - (_05_pos - _05_lok) * 5f;

				vec3 _10_pos = _05_pos + v3(-30f, 40f, 0f);
				if (t < 33125) {
					// transition from harr
					float f = progress(31400, 33125, t);
					dp = v3() - _00_pos;
					dir = dp + lerp(_00_lok, _01_lok, f);
				} else if (t < 35708) {
					// transition to em
					float f = greetprogress(33125, 35708, 34041, 34791, t, .11f);
					vec3 to = lerp(_00_pos, _05_pos, f);
					//to = quadratic(_00_pos, _05_pos, v3(800f, -700f, 0f), f);
					//to.z = lerp(_00_pos.z, _05_pos.z, f);
					dp = v3() - to;
					dir = dp + lerp(_01_lok, _05_lok, f);
				} else if (t < 39750) {
					// around em
					float f = progress(36458, 39750, t);
					dp = v3() - lerp(_05_pos, _10_pos, f);
					dir = dp + _05_lokb;
				}

				if (t < 41000) {
					Zctext.position = _05_lokb;
					Zctext.rotation = quat(0f, 0f, 1.5f);
					float f = progress(36458, 39125, t);
					Zctext.showbg = f >= 0f;
					Zctext.bgsize = eq_out_circ(clamp(f * 2.2f, 0f, 1f));
					Zctext.bgcol = v4(1f, .42f, .8f, 1f - eq_in_expo(f));
				} else if (t < 47000) {

				} else if (t < 52000) {

				} else if (t < 56541) {

				}
			} else if (67458/*71208*/ <= t && t < 104085) {
				// harrier breakdown
				dp = v3(-38f, 40f, -20f);
				dir = dp - v3(0f, 20f, -5f);
			}

			vec2 vd = viewdir(dir);
			lquatx = quat(0f, 0f, vd.x);
			lquaty = quat(0f, vd.y, 0f);
			lquatz = quat(rz, 0f, 0f);
		}

		public static void adjust(vec3[] points) {
			if (!rendering) {
				vec3 d = v3(debug_x * 3, debug_y * 3, 0);
				move(points, d);
				turn(points, mid, quat(0f, 0f, rad(mouse.x)));
				turn(points, mid, quat(0f, -rad(mouse.y), 0f));
			}
			move(points, dp);
			turn(points, campos, lquatx);
			turn(points, campos, lquaty);
			turn(points, campos, lquatz);
		}

		public override void fin(Writer w) {
		}

		private float greetprogress(int a, int b, int xa, int xb, int time, float mod)
		{
			xa -= 100;
			xb -= 100;
			if (time < xa) {
				return progress(a, xa, time) * .5f;
			}
			if (time < xb) {
				int mid = (xa + xb) / 2;
				float p;
				if (time < mid) {
					p = progress(xa, mid, time);
				} else {
					p = progress(xb, mid, time);
				}
				return 0.5f - (int) (p * 6) / 6f * mod;
			}
			return .5f + progress(xb, b, time) * 0.5f;
		}

	}
}
}
