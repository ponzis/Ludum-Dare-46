// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Pixelate"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _CellSize("Cell Size", Vector) = (0.02, 0.02, 0, 0)
    }
        SubShader{
            Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
            LOD 200

            Pass {
                CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #include "UnityCG.cginc"

                    struct v2f {
                        float4 pos : SV_POSITION;
                        float4 grabUV : TEXCOORD0;
                    };

                    sampler2D _MainTex;
                    float4 _CellSize;

                    v2f vert(appdata_base v) {
                        v2f o;
                        o.pos = UnityObjectToClipPos(v.vertex);
                        o.grabUV = ComputeGrabScreenPos(o.pos);
                        return o;
                    }

                    float4 frag(v2f IN) : COLOR
                    {
                        float2 steppedUV = IN.grabUV.xy / IN.grabUV.w;
                        steppedUV /= _CellSize.xy;
                        steppedUV = round(steppedUV);
                        steppedUV *= _CellSize.xy;
                        return tex2D(_MainTex, steppedUV);
                    }
                ENDCG
            }
    }
}