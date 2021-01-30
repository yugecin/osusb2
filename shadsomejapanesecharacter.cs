using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	/*https://www.shadertoy.com/view/4lKfzK*/
	class shadsomejapanesecharacter : IColorOwner {
		public static IColorOwner instance = new shadsomejapanesecharacter();

vec2 cub_(float t,vec2 a,vec2 b){
    float ct=1.0f-t;
    return 3.0f*ct*ct*t*a+3.0f*ct*t*t*b+t*t*t;
}
float cub(vec2 a,vec2 b,float x){
    vec2 it=v2(0f,1f);
    for (int i=0;i<10;i++) {
        float pos=(it.x+it.y)/2f;
        vec2 r=cub_(pos,a,b);
        if (r.x>x){
            it.y=pos;
        }else{
            it.x=pos;
        }
    }
    return cub_((it.x+it.y)/2.0f,a,b).y;
}
float smthstep(float a, float x)
{
 	return smoothstep(a, a + .003f, x);   
}
void mainImage(out vec4 fragColor, vec2 uv)
{
    float x = uv.x;
    float y = uv.y;
    vec3 col = v3(0f);

    /*col += step(x, shadborder);
    col += step(1f-shadborder, x);
    col += step(y, shadborder); 
    col += step(1f-shadborder, y);*/
    col = col + 1f
        *smthstep(.135f+.015f*(x-.7f)/.07f,y)
        *smthstep(y,.8f+.05f*(x-.3f)/.04f)
        *smthstep(.7f-.4f*cub(v2(.52f,.17f),v2(.85f,.43f),(y-.135f)/.665f),x)
        *smthstep(x,.77f-.43f*cub(v2(.52f,.17f),v2(.85f,.43f),(y-.15f)/.7f))
        ;
    
    for (int i = 0; i < 2; i++) {
        col += 1.0f
            *smthstep(.14f-.02f*(x-.38f)/.06f,y)
            *smthstep(y,.38f-.02f*(x-.46f)/.06f)
            *smthstep(.38f+.08f*cub(v2(.28f,.37f),v2(.61f,.69f),(y-.14f)/.24f),x)
            *smthstep(x,.44f+.08f*cub(v2(.28f,.37f),v2(.61f,.69f),(y-.12f)/.24f))
            ;
        x += .2f;
        y -= .05f;
    }

    fragColor = v4(col, 1.0f);
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
