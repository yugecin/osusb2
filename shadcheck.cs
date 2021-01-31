using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace osusb1 {
partial class all {
	class shadcheck : IColorOwner {
		public static IColorOwner instance = new shadcheck();
		Bitmap bm;
		shadcheck() {
			try {
				using (FileStream fs = File.OpenRead("check.png"))
				{
					bm = new Bitmap(fs);
				}
			}catch (Exception e) {
				Console.WriteLine(e);
			}
		}

		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			if (bm == null) {
				return v3(1f, 0f, 0f).col();
			} else {
				if (uv.x > 1f - shadborder || uv.y > 1f - shadborder ||
					uv.x < shadborder || uv.y < shadborder)
				{
					return Color.White;
				}
				x = (int) (uv.x * bm.Width);
				y = (int) (uv.y * bm.Height);
				if (x > 0 && y > 0 && x < bm.Width && y < bm.Height) {
					return bm.GetPixel(x, y);
				}
				return v3().col();
			}
		}
	}
}
}
