using System;
using System.Drawing;
using System.IO;

namespace osusb1
{
	class harr
	{
		public static Color[] cols = new Color[] {
			Color.FromArgb(0xFF, 0x7A, 0x8B, 0x9E),
			//Color.FromArgb(0xFF, 0xFF, 0x8B, 0xFF),
			Color.FromArgb(0xFF, 0x46, 0x59, 0x6E),
			Color.FromArgb(0xFF, 38, 139, 255),
		};

		public static all.vec3[] points;
		public static int[][] lines;
		public static int[][] tris;

		public static void read()
		{
			using (StreamReader i = new StreamReader("harr.txt")) {
				string line;
				bool plines = false, ppoints = false, ptris = false;
				int lidx = 0, pidx = 0, tidx = 0;
				while ((line = i.ReadLine()) != null) {
					string[] z = line.Split(' ');
					if (line.StartsWith("lines")) {
						plines = true;
						ppoints = false;
						ptris = false;
						lines = new int[int.Parse(z[1])][];
						continue;
					}
					if (line.StartsWith("points")) {
						ppoints = true;
						plines = false;
						ptris = false;
						points = new all.vec3[int.Parse(z[1])];
						continue;
					}
					if (line.StartsWith("tris")) {
						ptris = true;
						plines = false;
						ppoints = false;
						tris = new int[int.Parse(z[1])][];
						continue;
					}
					if (z[0].EndsWith(":")) {
						string[] zz = new string[z.Length - 1];
						for (int o = 0; o < zz.Length; o++) {
							zz[o] = z[o + 1];
						}
						z = zz;
					}
					if (ppoints) {
						points[pidx++] = all.v3(float.Parse(z[0]), float.Parse(z[1]),
							float.Parse(z[2]));
					}
					if (plines) {
						lines[lidx++] = new int[] { int.Parse(z[0]), int.Parse(z[1]) };
					}
					if (ptris) {
						tris[tidx++] = new int[] { int.Parse(z[0]), int.Parse(z[1]),
						int.Parse(z[2]), int.Parse(z[3]) };
					}
				}
			}

			
			int[][] newtris = new int[tris.Length][];
			int newi = 0;
			for (int i = 0; i < tris.Length; i++) {
				if (points[tris[i][0]].z < 0f || points[tris[i][1]].z < 0f || points[tris[i][2]].z < 0f) {
					newtris[newi] = tris[i];
					newi++;
				}
			}
			for (int i = 0; i < tris.Length; i++) {
				if (!(points[tris[i][0]].z < 0f || points[tris[i][1]].z < 0f || points[tris[i][2]].z < 0f)) {
					newtris[newi] = tris[i];
					newi++;
				}
			}
			tris = newtris;
			
		}

		public static void write()
		{
			using (StreamWriter z = new StreamWriter("harr.txt")) {
				z.WriteLine("points " + points.Length);
				for (int i = 0; i < points.Length; i++) {
					z.WriteLine(i + ": " + points[i].x + " " + points[i].y + " " +
						points[i].z);
				}
				z.WriteLine("lines " + lines.Length);
				for (int i = 0; i < lines.Length; i++) {
					z.WriteLine(i + ": " + lines[i][0] + " " + lines[i][1]);
				}
				z.WriteLine("tris " + tris.Length);
				for (int i = 0; i < tris.Length; i++) {
					z.WriteLine(i + ": " + tris[i][0] + " " + tris[i][1]
						 + " " + tris[i][2] + " " + tris[i][3]);
				}
			}
		}
	}
}
