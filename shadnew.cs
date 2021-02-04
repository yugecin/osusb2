using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	/*https://www.shadertoy.com/new*/
	class shadnew : IColorOwner {
		public static IColorOwner instance = new shadnew();

void mainImage(out vec4 fragColor, vec2 uv) {
    vec3 col = v3(0.5f)+v3(0.5f)*cos(v3(iTime/500.0f)+v3(uv,uv.x)+v3(0f,2f,4f));
    col += step(uv.x, shadborder);
    col += step(1f - shadborder, uv.x);
    col += step(uv.y, shadborder);
    col += step(1f - shadborder, uv.y);
    fragColor = v4(col,1f);
}

		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			vec4 col;
			mainImage(out col, uv);
			col = clamp(col, 0f, 1f);
			return col.col();
		}
	}
}
}
