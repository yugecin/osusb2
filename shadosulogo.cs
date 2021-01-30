using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace osusb1 {
partial class all {
	/*https://www.shadertoy.com/view/4dVyD3*/
	class shadosulogo : IColorOwner {
		public static IColorOwner instance = new shadosulogo();

static vec3 iResolution = v3(10f, 10f, 0f);

// based off https://www.shadertoy.com/view/MdVyRK

float rand (vec2 p) {
    return fract(sin(dot(p,
                         v2(6.8245f,7.1248f)))*
        9.1283f);
}

float tri(vec2 uv, vec2 p, float s){
    vec2 v = uv;
    v -= p;
    v /= max(s, 0.01f);

    float a = atan2(v.x, v.y) + 3.14159265359f;
    float r = 6.28318530718f / 3.0f;
    
    float t = cos(floor(0.5f + a / r) * r - a) * length(v);

    return smoothstep(0.4f, 0.41f, t);
}

float yPos(float i){
    vec2 p = v2(0.12345679f, i);
    
    float r = rand(p);
    return fract(iTime * 0.5f + r);
}

float xPos(float i, float t){
    vec2 p = v2(i, t - iTime * 0.5f);
    return rand(p) + .375f;
}

vec3 triCol(float i, float t){
    vec3 col = v3(0.9411764705882353f,0.4274509803921569f,0.6549019607843137f);
    float r = xPos(i + 1.0f, t);
    col *= lerp(0.9f, 1.1f, r);
    return col;
}

float atan2(float y, float x) {
 	if(x>0f)return atan(y/x);
    if(x==0f)if(y>0f)return 1.5707963268f;else return -1.5707963268f;
    if(y<0f)return atan(y/x)-3.14159265359f;return atan(y/x)+3.14159265359f;
}
float atan2(vec2 v){return atan2(v.y,v.x);}
float steq(float x,float a,float b){return step(a,x)*step(x,b);}
vec2 cub_(float t,vec2 a,vec2 b){
    float ct=1f-t;
    return 3f*ct*ct*t*a+3f*ct*t*t*b+t*t*t;
}
float cub(float x,vec2 a,vec2 b){
    vec2 it=v2(0f,1f);
    for (int i=0;i<7;i++) {
        float pos=(it.x+it.y)/2f;
        vec2 r=cub_(pos,a,b);
        if (r.x>x){
            it.y=pos;
        }else{
            it.x=pos;
        }
    }
    return cub_((it.x+it.y)/2f,a,b).y;
}
float isine(float t){return -1f*cos(t*1.5707963268f)+1f;}
float osine(float t){return sin(t*1.5707963268f);}
float iquad(float t){return t*t;}
float oc(float t){t=t-1f;return t*t*t+1f;}
vec2 oc(vec2 v){return v2(oc(v.x),oc(v.y));}
float icirc(float t){return -1f*(sqrt(1f-t*t)-1f);}
vec3 spin(vec3 col_,vec2 fc) {
    vec3 col=col_;
    float a=mod(deg(atan2(fc-iResolution.xy/2f-.5f)),360f);
    float b=mod(iTime*100f,360f);
    float s=25f;
    float mi=mod(b-s,360f);
    float ma=mod(b+s,360f);
    float d=abs(b-a);
    if(d>180f)d=a<b?a-b+360f:b+360f-a;
    if((a>mi||(mi>ma&&a<ma))&&(a<ma||mi>ma))col+=1f-iquad(d/s);
    return col;
}
float mb(){
   return clamp(/*texture(iChannel0,vec2(0.02,0.2))*/fftval(), 0f, 1f);
}
vec3 barz(float d,vec2 fc,float off,float sp) {
    float a=deg(atan2(fc-iResolution.xy/2f-.5f))/180f+1f;
    a=mod(a+iTime/sp+off/*+mb()/3*/,2f);
    a-=1f;
    if(a<0f)a=-a+0.01f;
    float m=mod(a,.025f);
    if(m<0.01f*(1f+d*.6f))return v3(0f);
    a-=m;
    float[] fftvals = fft.SlowFalloffValue(all.iTime).values;
    float fftval = fftvals[(int) (fftvals.Length * a)] * 1.5f;
    //float fftmod = 1 / max(0.01f, fftval) / 3.0f;
    //fftmod = max(fftmod, 1f);
    //fftval *= fftmod;
    float v = clamp(/*texture(iChannel0,vec2(a,0.1f)).x*/fftval, 0f, 1f);
    if (v>d) return v3(1f);
    return v3(0f);
}
float osu_excdot(vec2 uv) {
    const float ds=.07f;
    vec2 exc=oc((ds*2f-abs(uv))/ds/2f)*.04f;
    return steq(uv.x,-ds-exc.y,ds+exc.y)*steq(uv.y,-ds-exc.x,ds+exc.x);
}
float osu_excbody(vec2 uv) {
    float e=oc((.2f-abs(uv.x))/.2f)*.04f;
    float ew=(uv.y+.15f)*.01f;
    return steq(uv.x,-.1f-ew,.1f+ew)*steq(uv.y,-.2f-e,.2f+e);
}
float osu_u(vec2 uv) {
    float r=1.18181818f;
    uv+=v2(.5f/r,.5f);
    uv.x*=r;
    uv.y=1f-uv.y;
    float c=1f;
    c-=steq(uv.x,.31f,.69f)*steq(uv.y,.0f,.765f-.245f*cub(1f-(uv.x-.31f)/.38f,v2(.4f,-.116f),v2(.994f,-.27f)));
    float b=.48f*cub(1f-uv.x,v2(.252f,-.164f),v2(1.038f,-.52f));
    return c*steq(uv.x,0f,1f)*steq(uv.y,.02f*isine(abs(mod(uv.x,.69f)-.155f)/.31f),.933f-b);
}
float osu_sunpy(vec2 uv) {
    float r=1.397928994f;
    uv+=v2(.5f/r,.5f);
    uv.x*=r;
    if (steq(uv.x,0f,1f)*steq(uv.y,0f,1f)==0f) {
        return 0f;
    }
    uv.y=1f-uv.y;
    float c=1f;
    c-=steq(uv.x,.0f,.035f+.515f*icirc((.3f-uv.y)/.3f))*steq(uv.y,.0f,.3f);
    c-=steq(uv.x,.55f,1f)*steq(uv.y,.0f,.055f*isine(clamp((uv.x-.55f)/.4f,.0f,1f)));
    c-=steq(uv.x,.95f-.07f*isine(clamp((uv.y-.055f)/.192f/*.195*/,.0f,1f)),1f)*steq(uv.y,.055f,.28f);
    c-=steq(uv.x,.59f,.88f)*steq(uv.y,.2f+.045f*isine((uv.x-.59f)/.29f),.28f);
    c-=steq(uv.x,.4f+.19f*icirc(1f-(uv.y-.2f)/.08f),.59f)*steq(uv.y,.2f,.28f);
    c-=steq(uv.x,.4f+.6f*cub((uv.y-.28f)/.395f,v2(.408f,.011f),v2(.104f,1.014f)),1f)*steq(uv.y,.28f,.675f);
    c-=steq(uv.x,1f-.585f*icirc((uv.y-.675f)/.325f),1f)*steq(uv.y,.675f,1f);
    c-=steq(uv.x,.0f,.415f)*steq(uv.y,.94f+.06f*osine(uv.x/.415f),1f);
    c-=steq(uv.x,.0f,.085f*isine(1f-(uv.y-.75f)/.19f))*steq(uv.y,.75f,.94f);
    c-=steq(uv.x,.0f,.4f)*steq(uv.y,.69f,.75f+.045f*osine(clamp((uv.x-.085f)/.315f,0f,1f)));
    c-=steq(uv.x,.4f,.645f)*steq(uv.y,.69f,.795f-.105f*icirc((uv.x-.4f)/.245f));
    c-=steq(uv.x,.0f,.035f+.61f*cub((uv.y-.3f)/.39f,v2(.891f,-.042f),v2(.592f,.977f)))*steq(uv.y,.3f,.69f);
    return c;
}
float osu_o(vec2 uv) {
    float r=1.091666f;
    uv.x*=r;
    uv.y=1f-abs(uv.y);
    uv.x=abs(uv.x);
    float te=cub(uv.x,v2(.667f,.013f),v2(.988f,.366f));
    float be=1f-cub(clamp(uv.x/.402f,0f,1f),v2(.783f,.035f),v2(.915f,.241f));
    return steq(uv.x,0f,1f)*steq(uv.y,0f+te,1f-.595f*be);
}
float osu(vec2 uv) {
    float col=0f;
    col+=osu_excdot((uv-v2(.806f,-.192f))*1.4f);
    col+=osu_excbody((uv-v2(.806f,.23f))*v2(1.35f,.9f));
    col+=osu_u((uv-v2(.379f,.0f))*1.7f);
    col+=osu_sunpy((uv-v2(-.134f,.0f))*1.7f);
    col+=osu_o((uv-v2(-.667f,.0f))*3.4f);
    return col;
}
void mainImage(out vec4 fragColor, vec2 originaluv){
    vec3 col = v3(0.9411764705882353f,0.4274509803921569f,0.6549019607843137f);
    float s = 1.2f -/*texture(iChannel0, vec2(0.52,0.2)).x*/fftval() * .2f;
    vec2 fragCoord = originaluv * iResolution.xy;
    originaluv.y = 1f - originaluv.y;
    vec2 uv = originaluv * s - (s - 1f) * .5f;
    uv.x *= iResolution.x/iResolution.y;
    
    // Generate all dem triangles
    for (float i = 64.0f; i > 0.0; i--){
        float id = i / 64.0f;
        float y = yPos(id);
        float x = xPos(id, y);
        float _s = min(0.89f, max(0.071f, id * 0.5f));
        float shad = tri(
            uv,
            v2(x, lerp(-_s, 1.0f + _s / 2.0f, y)),
            _s
        );
        
        if (shad < 0.1f)
        	col = triCol(id, y) * (1.0f - shad);
    }

    // Set background mask
    vec2 mid = v2(.5f * iResolution.x / iResolution.y, .5f);
    float dist = distance(uv,mid);
    if (dist > 0.4f) {
        col = v3(0.0f);
        if (dist<0.65f){
            float sp=6f;
            float d=(dist-.4f)/.25f;
            col += barz(d, fragCoord, 0f, sp);
            col += barz(d, fragCoord, .5f, sp);
            col += barz(d, fragCoord, 1f, sp);
            col += barz(d, fragCoord, 1.5f, sp);
            col*=.1f+.2f/*mb()*/;
            col.x*=.75f;
            col.y*=.75f;
        }
    } else
    {}//if(dist>0.32f&&dist<0.37f)col=spin(col,fragCoord);
    
    // Make circle logo shadow
    float dist_shad = distance(uv, v2(0.5f, 0.49f));
    float l_shad = abs(dist_shad - 0.4f);
    col *= lerp(0.3f, 1.0f, min(1.0f, l_shad * 30.0f));
    
    // Make circle logo
    float l = abs(dist - 0.4f);
    col += v3(smoothstep(0.96f, 0.97f, 1f - l));
    
    if(dist<0.3f) //for perf
    col+=v3(osu((uv-mid)/.3f));

    /*col += step(originaluv.x, shadborder);
    col += step(1f-shadborder, originaluv.x);
    col += step(originaluv.y, shadborder); 
    col += step(1f-shadborder, originaluv.y);*/
    
    fragColor = v4(col,1f);
}

/*
#define SEED 0.12345679

#define TRI 64.0
#define SP 0.5
#define COLOR vec3(0.9411764705882353,0.4274509803921569,0.6549019607843137)

#define PI 3.14159265359
#define TWO_PI 6.28318530718
#define HALFPI 1.5707963268
*/

		float iTime;
		Color IColorOwner.getColor(int i, int j, int x, int y, float z, vec2 uv)
		{
			this.iTime = all.iTime / 1000.0f;
			vec4 col;
			mainImage(out col, uv);
			col = clamp(col, 0f, 1f);
			return col.col();
		}

		static float fftval() {
			float[] values = fft.SmoothValue(all.iTime).values;
			float v = 0.0f;
			for (int i = 0; i < values.Length; i++) {
				if (values[i] > v) {
					v = values[i];
				}
			}
			return v;
		}
	}
}
}
