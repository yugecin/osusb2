using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace osusb1 {
partial class all {
	class shademilyrobin : IColorOwner {
		public static IColorOwner instance = new shademilyrobin();
		Bitmap bm;
		shademilyrobin() {
			using (FileStream fs = File.OpenRead("emilyrobin.bmp"))
			{
				bm = new Bitmap(fs);
			}
		}

		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			if (bm == null) {
				return v3(1f, 0f, 0f).col();
			} else {
				uv *= 1.2f;
				uv -= 0.1f;
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
