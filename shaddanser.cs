using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace osusb1 {
partial class all {
	class shaddanser : IColorOwner {
		public static IColorOwner instance = new shaddanser();
		Bitmap bm;
		shaddanser() {
			using (FileStream fs = File.OpenRead("danser.png"))
			{
				bm = new Bitmap(fs);
			}
		}

		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 originaluv)
		{
			if (bm == null) {
				return v3(1f, 0f, 0f).col();
			} else {
				float s = 1.2f -shadosulogo.fftval() * .35f;
				vec2 uv = (originaluv * 1.2f) - .1f;
				uv = uv * s - (s - 1f) * .5f;
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
