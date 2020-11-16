using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace osusb1 {
partial class all
{
	public static Bez harrx = new Bez("harrx");
	public static Bez harry = new Bez("harry");
	public static Bez harrz = new Bez("harrz");
	public static Bez pitch = new Bez("pitch");
	public static Bez roll = new Bez("roll");
	public static Bez pitch2 = new Bez("pitch2");
	public static Bez yaw = new Bez("yaw");
	public static Bez camx = new Bez("camx");
	public static Bez camy = new Bez("camy");
	public static Bez camz = new Bez("camz");
	public static Bez camr = new Bez("camr");
	public static Bez camattach = new Bez("camattach");
	public static Bez camoffx = new Bez("camoffx");
	public static Bez camoffy = new Bez("camoffy");
	public static Bez camoffz = new Bez("camoffz");

	public static Bez[] bezs = new Bez[] {
		harrx, harry, harrz, pitch, roll, pitch2, yaw,
		camx, camy, camz, camr, camattach, camoffx, camoffy, camoffz
	};

	public static vec3 getHarrPos(int time)
	{
		return v3(harrx.valueAt(time) * 1500f, harry.valueAt(time) * 1500f, (harrz.valueAt(time) + .75f) * 500f);
	}

	public static vec3 getCamPos(int time)
	{
		if (camattach.valueAt(time) >= 0f) {
			vec3 pos = getHarrPos(time);
			pos.x += camoffx.valueAt(time) * 100f;
			pos.y += camoffy.valueAt(time) * 100f;
			pos.z += camoffz.valueAt(time) * 100f;
			return pos;
		} else {
			return v3(camx.valueAt(time) * 1000f, camy.valueAt(time) * 1000f, (camz.valueAt(time) + 1f) * 500f);
		}
	}

	public class Bez
	{
		public static vec2[][][][] data;
		public static int[][] data_numsegments;
		private static int activeIndex = -1;
		private static int[] starttimes = new int[] { 18291, 54208, 104085 };
		private static int[] endtimes = new int[] { 39000, 67458, 124000 };

		public static void init()
		{
			for (int i = 0; i < starttimes.Length; i++) {
				starttimes[i] -= 1000;
				endtimes[i] += 1000;
			}
			data = new vec2[bezs.Length][][][];
			data_numsegments = new int[bezs.Length][];
			for (int i = 0; i < bezs.Length; i++) {
				data[i] = new vec2[3][][];
				data_numsegments[i] = new int[3];
				for (int j = 0; j < 3; j++) {
					data_numsegments[i][j] = 1;
					data[i][j] = new vec2[4][];
					data[i][j][0] = new vec2[] { v2(0f) };
					data[i][j][1] = new vec2[] { v2(.1f, .5f) };
					data[i][j][2] = new vec2[] { v2(.9f, .5f) };
					data[i][j][3] = new vec2[] { v2(1f) };
				}
			}
			using (StreamReader r = new StreamReader("bez.txt")) {
				string line;
				while ((line = r.ReadLine()) != null) {
					int i = 0;
					for (; i < bezs.Length; i++) {
						if (bezs[i].name == line) {
							break;
						}
					}
					if (i >= bezs.Length) {
						throw new Exception("no bez " + line);
					}
					for (int j = 0; j < 3; j++) {
						line = r.ReadLine();
						int numsegments = int.Parse(line);
						data_numsegments[i][j] = numsegments;
						data[i][j][0] = new vec2[numsegments];
						data[i][j][1] = new vec2[numsegments];
						data[i][j][2] = new vec2[numsegments];
						data[i][j][3] = new vec2[numsegments];
						for (int s = 0; s < numsegments; s++) {
							data[i][j][0][s] = v2(rf(r), rf(r));
							data[i][j][1][s] = v2(rf(r), rf(r));
							data[i][j][2][s] = v2(rf(r), rf(r));
							data[i][j][3][s] = v2(rf(r), rf(r));
						}
					}
				}
			}
			selectDataIndex(0);
		}

		public static void save()
		{
			int idx = activeIndex;
			selectDataIndex(idx ^ 1);
			selectDataIndex(idx);
			using (StreamWriter w = new StreamWriter("bez.txt")) {
				for (int i = 0; i < bezs.Length; i++) {
					w.WriteLine(bezs[i].name);
					for (int j = 0; j < 3; j++) {
						w.WriteLine("" + data_numsegments[i][j]);
						for (int s = 0; s < data_numsegments[i][j]; s++) {
							ww(w, data[i][j][0][s]);
							ww(w, data[i][j][1][s]);
							ww(w, data[i][j][2][s]);
							ww(w, data[i][j][3][s]);
						}
					}
				}
			}
		}

		static void ww(StreamWriter w, vec2 v)
		{
			w.WriteLine("{0}:{1}", v.x, f2h(v.x));
			w.WriteLine("{0}:{1}", v.y, f2h(v.y));
		}

		static float rf(StreamReader r)
		{
			string line = r.ReadLine();
			if (line.Contains(":")) {
				return h2f(line.Substring(line.IndexOf(':') + 1));
			} else {
				return float.Parse(line);
			}
		}

		public static void selectDataIndex(int index)
		{
			if (activeIndex != -1) {
				for (int i = 0; i < bezs.Length; i++) {
					data_numsegments[i][activeIndex] = bezs[i].numsegments;
					data[i][activeIndex][0] = bezs[i].from;
					data[i][activeIndex][1] = bezs[i].p1;
					data[i][activeIndex][2] = bezs[i].p2;
					data[i][activeIndex][3] = bezs[i].to;
				}
			}
			activeIndex = index;
			for (int i = 0; i < bezs.Length; i++) {
				bezs[i].numsegments = data_numsegments[i][activeIndex];
				bezs[i].start = starttimes[activeIndex];
				bezs[i].end = endtimes[activeIndex];
				bezs[i].from = data[i][activeIndex][0];
				bezs[i].p1 = data[i][activeIndex][1];
				bezs[i].p2 = data[i][activeIndex][2];
				bezs[i].to = data[i][activeIndex][3];
			}
		}

		public string name;
		public int numsegments;
		public int start;
		public int end;
		// x is time
		public vec2[] from;
		public vec2[] to;
		public vec2[] p1; // x based on [0,1]
		public vec2[] p2; // x based on [0,1]

		public Bez(string name)
		{
			this.name = name;
			numsegments = 1;
			from = new vec2[] { v2(0f) };
			to = new vec2[] { v2(1f, .5f) };
			p1 = new vec2[] { v2(.2f, 1f) };
			p2 = new vec2[] { v2(.8f, .2f) };
			start = starttimes[0];
			end = endtimes[0];
		}

		public float valueAt(int time)
		{
			if (time < start || end < time) {
				for (int i = 0; i < starttimes.Length; i++) {
					if (starttimes[i] <= time && time < endtimes[i]) {
						selectDataIndex(i);
					}
				}
			}
			return (valueAt(progress(start, end, time)) - 0.5f) * 2f;
		}

		public float valueAt(float t)
		{
			for (int i = 0; i < numsegments; i++) {
				if (from[i].x <= t && t <= to[i].x) {
					t = progress(from[i].x, to[i].x, t);
					float ct = 1f - t;
					return
						ct * ct * ct * from[i].y +
						3 * ct * ct * t * p1[i].y +
						3 * ct * t * t * p2[i].y +
						t * t * t * to[i].y;
				}
			}
			return 0f;
		}

		public void separate(float t)
		{
			vec2[] from = new vec2[numsegments + 1];
			vec2[] p1 = new vec2[numsegments + 1];
			vec2[] p2 = new vec2[numsegments + 1];
			vec2[] to = new vec2[numsegments + 1];
			for (int i = 0; i < numsegments; i++) {
				if (this.from[i].x <= t && t <= this.to[i].x) {
					int z = 0;
					for (int j = 0; j <= i; j++) {
						from[z] = this.from[j];
						p1[z] = this.p1[j];
						p2[z] = this.p2[j];
						to[z] = this.to[j];
						z++;
					}
					z++;
					for (int j = i + 1; j < numsegments; j++) {
						from[z] = this.from[j];
						p1[z] = this.p1[j];
						p2[z] = this.p2[j];
						to[z] = this.to[j];
						z++;
					}
					vec2 newpt = v2(t, valueAt(t));
					from[i + 1] = v2(newpt);
					p1[i + 1] = v2(.1f, newpt.y);
					p2[i + 1] = v2(this.p2[i]);
					to[i + 1] = v2(this.to[i]);
					p2[i] = v2(.9f, newpt.y);
					to[i] = v2(newpt);
					numsegments++;
					this.from = from;
					this.p1 = p1;
					this.p2 = p2;
					this.to = to;
					return;
				}
			}
		}

		public void delete(int index)
		{
			if (index == 0 || index >= numsegments) {
				return;
			}
			vec2[] from = new vec2[numsegments - 1];
			vec2[] p1 = new vec2[numsegments - 1];
			vec2[] p2 = new vec2[numsegments - 1];
			vec2[] to = new vec2[numsegments - 1];
			for (int i = 0; i < index; i++) {
				from[i] = this.from[i];
				p1[i] = this.p1[i];
				p2[i] = this.p2[i];
				to[i] = this.to[i];
			}
			for (int i = index + 1; i < numsegments; i++) {
				from[i - 1] = this.from[i];
				p1[i - 1] = this.p1[i];
				p2[i - 1] = this.p2[i];
				to[i - 1] = this.to[i];
			}
			to[index - 1] = v2(this.to[index]);
			this.from = from;
			this.p1 = p1;
			this.p2 = p2;
			this.to = to;
			numsegments--;
		}
	}
}
}
