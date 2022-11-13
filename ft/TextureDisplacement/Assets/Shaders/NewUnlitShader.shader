Shader "Unlit/VertexFragmentShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _SideTex ("Side Texture", 2D) = "white" {}

        _SaveTex ("Save Texture", 2D) = "white" {}

        _ComparePos ("Compare Position", Vector) = (0, 0, 0, 0)

        _MaxDistance ("Max Distance", Float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : uv;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SideTex;
            sampler2D _SaveTex;

            fixed4 _MainCol;

            fixed4 _ComparePos;

            float _MaxDistance;

            v2f vert (appdata v)
            {
                v2f o;

                float distance = (_ComparePos.x-v.vertex.x) * (_ComparePos.x-v.vertex.x) + (_ComparePos.y-v.vertex.y) * (_ComparePos.y-v.vertex.y) + (_ComparePos.z-v.vertex.z) * (_ComparePos.z-v.vertex.z);

                if( distance < _MaxDistance )
                {
                    o.color.r = distance / _MaxDistance;
                }
                else o.color.r = 0;

                

                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;

                if( tex2D(_SaveTex, i.uv).r < 0.2 )
                {
                    col = tex2D(_SideTex, i.uv);
                }
                else
                {
                    if(i.color.r > 0.01) col = tex2D(_MainTex, i.uv);
                    else
                    {
                        col = tex2D(_SideTex, i.uv);
                    }
                }

                

                return col;
            }
            ENDCG
        }
    }
}