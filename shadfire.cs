using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	/*https://fabiensanglard.net/doom_fire_psx/*/
	class shadfire : IColorOwner {
		public static IColorOwner instance = new shadfire();

		// this requires state... oh well..
		const int SIZE = 200;
		byte[,] texturedata;
		Random rand = new Random();

		Color[] cols = new Color[37] {
			Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF),
			Color.FromArgb(0xFF, 0xEF, 0xEF, 0xC7),
			Color.FromArgb(0xFF, 0xDF, 0xDF, 0x9F),
			Color.FromArgb(0xFF, 0xCF, 0xCF, 0x6F),
			Color.FromArgb(0xFF, 0xB7, 0xB7, 0x37),
			Color.FromArgb(0xFF, 0xB7, 0xB7, 0x2F),
			Color.FromArgb(0xFF, 0xB7, 0xAF, 0x2F),
			Color.FromArgb(0xFF, 0xBF, 0xAF, 0x2F),
			Color.FromArgb(0xFF, 0xBF, 0xA7, 0x27),
			Color.FromArgb(0xFF, 0xBF, 0xA7, 0x27),
			Color.FromArgb(0xFF, 0xBF, 0x9F, 0x1F),
			Color.FromArgb(0xFF, 0xBF, 0x9F, 0x1F),
			Color.FromArgb(0xFF, 0xC7, 0x97, 0x1F),
			Color.FromArgb(0xFF, 0xC7, 0x8F, 0x17),
			Color.FromArgb(0xFF, 0xC7, 0x87, 0x17),
			Color.FromArgb(0xFF, 0xCF, 0x87, 0x17),
			Color.FromArgb(0xFF, 0xCF, 0x7F, 0x0F),
			Color.FromArgb(0xFF, 0xCF, 0x77, 0x0F),
			Color.FromArgb(0xFF, 0xCF, 0x6F, 0x0F),
			Color.FromArgb(0xFF, 0xD7, 0x67, 0x0F),
			Color.FromArgb(0xFF, 0xD7, 0x5F, 0x07),
			Color.FromArgb(0xFF, 0xDF, 0x57, 0x07),
			Color.FromArgb(0xFF, 0xDF, 0x57, 0x07),
			Color.FromArgb(0xFF, 0xDF, 0x4F, 0x07),
			Color.FromArgb(0xFF, 0xC7, 0x47, 0x07),
			Color.FromArgb(0xFF, 0xBF, 0x47, 0x07),
			Color.FromArgb(0xFF, 0xAF, 0x3F, 0x07),
			Color.FromArgb(0xFF, 0x9F, 0x2F, 0x07),
			Color.FromArgb(0xFF, 0x8F, 0x27, 0x07),
			Color.FromArgb(0xFF, 0x8F, 0x27, 0x07),
			Color.FromArgb(0xFF, 0x67, 0x1F, 0x07),
			Color.FromArgb(0xFF, 0x57, 0x17, 0x07),
			Color.FromArgb(0xFF, 0x47, 0x0F, 0x07),
			Color.FromArgb(0xFF, 0x2F, 0x0F, 0x07),
			Color.FromArgb(0xFF, 0x1F, 0x07, 0x07),
			Color.FromArgb(0xFF, 0x07, 0x07, 0x07),
			Color.FromArgb(0xFF, 0x00, 0x00, 0x00),
		};

		shadfire()
		{
			texturedata = new byte[SIZE, SIZE];
			reset();
		}
		public void reset()
		{
			lasttime = 0;
			for (int i = 0; i < SIZE; i++) {
				texturedata[i, SIZE - 1] = (byte)(cols.Length - 1);
			}
		}
		void dofire()
		{
			for (int j = 0; j < SIZE - 1; j++) {
				for (int i = 0; i < SIZE; i++) {
					int r = rand.Next(3);
					int ii = i - min(r, 1) + 1;
					if (ii < 0 || SIZE <= ii) {
						ii = i;
					}
					byte v = texturedata[ii, j + 1];
					if (v > 0) {
						v -= (byte) (r & 1);
					}
					texturedata[i, j] = v;
				}
			}
		}
		int lasttime;
		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			const int step = 20;
			if (lasttime == 0) {
				lasttime = iTime - step * 100;
			}
			while (iTime - lasttime > 20) {
				dofire();
				lasttime += 20;
			}
			if (uv.x > 1f - shadborder || uv.y > 1f - shadborder ||
				uv.x < shadborder || uv.y < shadborder)
			{
				return Color.White;
			}

			x = (int) (uv.x * (SIZE - 1));
			y = (int) (uv.y * (SIZE - 1));
			return cols[cols.Length - 1 - texturedata[x, y]];
		}
	}
}
}
