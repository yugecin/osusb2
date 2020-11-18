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
			lockedharrpos = null;

			dp = v3(0f);
			vec3 dir = v3(0f);
			float rz = 0f;

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
				if (!rendering) {
					Console.WriteLine(getCamPos(t) + " " + getHarrPos(t));
				}
			} else if (t < 56333) {
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

				vec3 her_dir = v3(0f, -1f, 0f);
				vec3 her_pos = v3(398f, -145.85317f, 5f);
				vec3 her_lok = her_pos + her_dir * 50f;
				vec3 her_int = lerp(_10_pos, her_pos, 0.2f) + v3(0, 400f, 0f);
				vec3 her_int2 = lerp(_10_pos, her_pos, 0.8f) + v3(0, 400f, 0f);
				vec3 her_pos2 = v3(398f, -145.85317f, 28f);
				vec3 her_lok2 = her_pos2 + her_dir * 50f;

				vec3 qua_lok = v3(300f, 80f, 230f);
				vec3 qua_pos = v3(-748.7392f, -298.6331f, 80f);
				qua_lok = lerp(qua_lok, qua_pos, 0.4f);
				vec3 qua_int = lerp(her_pos2, qua_pos, 0.2f) + v3(0, -200f, 0);
				vec3 qua_int2 = lerp(her_pos2, qua_pos, 0.8f) + v3(0, -200f, 0);
				vec3 qua_pos2 = v3(-748.7392f, -240.6331f, 80f);

				vec3 luk_pos = v3(632.1013f, 358.0017f, 48.1202f);
				vec3 luk_lok = v3(-263.8631f, 1.909196f, 270f);
				vec3 luk_pos2 = luk_pos + v3(0f, -40f, 0f);
				vec3 luk_int = lerp(qua_pos2, luk_pos, 0.5f) + v3(0f, 500f, 200f);
				vec3 luk_lokint = lerp(qua_lok, luk_lok, 0.5f) + v3(0f, 0f, -400f);

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
				} else if (t < 42333) {
					// transition to herakles
					float f = greetprogress(39750, 42333, 40666, 41416, t, .11f);
					dp = v3() - lerp(_10_pos, her_pos, f);
					dp = v3() - cubic(_10_pos, her_pos, her_int, her_int2, f);
					dir = dp + lerp(_05_lokb, her_lok, f);
					rz = lerp(0f, 1f, eq_in_quart(f));
				} else if (t < 46375) {
					// around herakles
					float f = progress(42333, 46375, t);
					dp = v3() - lerp(her_pos, her_pos2, f);
					dir = dp + lerp(her_lok, her_lok2, f);
					rz = 1f;
				} else if (t < 48850) {
					// transition to quack
					float f = greetprogress(46375, 48850, 47333, 48000, t, .11f);
					dp = v3() - cubic(her_pos2, qua_pos, qua_int, qua_int2, f);
					dir = dp + lerp(her_lok2, qua_lok, eq_in_cubic(f));
					rz = lerp(1f, 0f, f);
				} else if (t < 51333) {
					// around quack
					float f = progress(48850, 51333, t);
					dp = v3() - lerp(qua_pos, qua_pos2, f);
					dir = dp + qua_lok;
					rz = lerp(0f, -0.2f, f);
				} else if (t < 53900) {
					// transition to luki
					float f = greetprogress(51333, 53900, 52291, 53000, t, .11f);
					dp = v3() - quadratic(qua_pos2, luk_pos, luk_int, f);
					dir = dp + quadratic(qua_lok, luk_lok, luk_lokint, f);
					rz = lerp(-0.2f, 0f, f);
				} else {
					// around luki
					float f = progress(53900, 56333, t);
					dp = v3() - lerp(luk_pos, luk_pos2, f);
					dir = dp + luk_lok;
				}

				if (t < 40000) {
					Zctext.position = _05_lokb;
					Zctext.rotation = quat(0f, 0f, 1.5f);
					float f = progress(36458, 39125, t);
					Zctext.showbg = f >= 0f;
					Zctext.bgsize = eq_out_circ(clamp(f * 2.2f, 0f, 1f));
					Zctext.bgcol = v4(1f, .42f, .8f, 1f - eq_in_expo(f));
					Zctext.bgstyle = 0;
				} else if (t < 47000) {
					Zctext.position = v3(400f, -205f, 20f);
					Zctext.rotation = quat(-PI2, 0f, PI);
					float f = progress(43040, 44750, t);
					Zctext.showbg = f >= 0f;
					Zctext.bgsize = eq_out_circ(clamp(f * 2.2f, 0f, 1f));
					Zctext.bgcol = v4(.4f, .7f, 1f, 1f - eq_in_expo(f));
					Zctext.bgstyle = 1;
				} else if (t < 52000) {
					Zctext.position = lerp(qua_lok, lerp(qua_pos, qua_pos2, 0.8f), 0.9f);
					Zctext.rotation = quat(0f, 0f, -1.5f);
					float f = progress(49583, 50916, t);
					Zctext.showbg = f >= 0f;
					Zctext.bgsize = eq_out_circ(clamp(f * 2.2f, 0f, 1f));
					Zctext.bgcol = v4(.4f, .7f, 1f, 1f - eq_in_expo(f));
					Zctext.bgstyle = 1;
				} else if (t < 56541) {
					Zctext.position = lerp(luk_lok, lerp(luk_pos, luk_pos2, 0.6f), 0.95f);
					Zctext.rotation = quat(0f, 0f, 1.9f);
					float f = progress(54400, 55800, t);
					Zctext.showbg = f >= 0f;
					Zctext.bgsize = eq_out_circ(clamp(f * 2.2f, 0f, 1f));
					Zctext.bgcol = v4(.4f, .7f, 1f, 1f - eq_in_expo(f));
					Zctext.bgstyle = 1;
				}
			} else if (t < 58000) {
				vec3 cpos;
				int seg = (58000 - 56333) / 6;
				switch ((t - 56333) / seg) {
				case 0:
					cpos = v3(10f, 0f, -2f);
					break;
				case 1:
					cpos = v3(7f, 10f, 0f);
					break;
				case 2:
					cpos = v3(0f, -5f, 1f);
					break;
				case 3:
					cpos = v3(-9f, 0f, 10f);
					break;
				case 4:
					cpos = v3(3f, 8f, 4f);
					break;
				default:
					cpos = v3(10f, -10f, 7f);
					rz = 2.3f;
					break;
				}
				lockedharrpos = v3(0f);
				dp = v3() - (getHarrPos(t) + cpos.norm() * 50f);
				dir = dp + getHarrPos(t);
			} else if (t < 67458) {
				dp = v3() - getCamPos(t);
				dir = dp + getHarrPos(t);
				if (!rendering) {
					Console.WriteLine(getCamPos(t) + " " + getHarrPos(t));
				}
			} else if (t < 104085) {
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
