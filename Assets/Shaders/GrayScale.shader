Shader "Custom/Grey Texture" {

    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Cutoff("Cutoff", Range(0,1)) = 0.5
            _LightColor("Light Color", COLOR) = (1,1,1,1)
            _DarkColor("Dark  Color", COLOR) = (0,0,0,0)
        _PixelCountU("Pixel Count U", float) = 100
        _PixelCountV("Pixel Count V", float) = 100

    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                #ifdef PIXELSNAP_ON
                o.vertex = UnityPixelSnap(o.vertex);
                #endif
                return o;
            }

            sampler2D _MainTex;
            uniform fixed _Cutoff;
            float4 _LightColor;
            float4 _DarkColor;

            float _PixelCountU;
            float _PixelCountV;

            fixed4 frag(v2f i) : SV_Target
            {
                float pixelWidth = 1.0f / _PixelCountU;
                float pixelHeight = 1.0f / _PixelCountV;

                half2 uv = half2((int)(i.uv.x / pixelWidth) * pixelWidth, (int)(i.uv.y / pixelHeight) * pixelHeight);

                fixed4 c = tex2D(_MainTex, uv);
                fixed l = c.r * 0.3 + c.g * 0.59 + c.b * 0.11;
                if (l >= _Cutoff) {
                    c = _LightColor;
                }
                else {
                    c = _DarkColor;
                }
                return c;
            }

            ENDCG
        }
    }
}