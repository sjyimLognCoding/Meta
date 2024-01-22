Shader "Custom/BoxBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Iterations("Iterations", int) = 10
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            samplerCUBE _Tex;
            half4 _Tex_HDR;
            half4 _Tint;
            half _Exposure;
            float _Rotation;

            float3 RotateAroundYInDegrees (float3 vertex, float degrees)
            {
                float alpha = degrees * UNITY_PI / 180.0;
                float sina, cosa;
                sincos(alpha, sina, cosa);
                float2x2 m = float2x2(cosa, -sina, sina, cosa);
                return float3(mul(m, vertex.xz), vertex.y).xzy;
            }

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            sampler2D _MainTex;

            int _Iterations;
            float neighborhoodSize;

            float4 frag (v2f i) : SV_Target
            {
                float4 col = 0;

                for (int index = 0; index < _Iterations; index++)
                {
                    float2 uv = i.uv + float2(0, (index/(_Iterations - 1.0) - .5) * neighborhoodSize);
                    
                    col += tex2D(_MainTex, uv);
                }


                col = col / _Iterations;
                return col;
            }
            ENDCG
        }

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            sampler2D _MainTex;

            int _Iterations;
            float neighborhoodSize;

            float4 frag (v2f i) : SV_Target
            {
                //calculate aspect ratio
                float invAspect = _ScreenParams.y / _ScreenParams.x;

                float4 col = 0;

                for (int index = 0; index < _Iterations; index++)
                {
                    float2 uv = i.uv + float2((index/(_Iterations - 1.0) - .5) * neighborhoodSize * invAspect, 0);
                    
                    col += tex2D(_MainTex, uv);
                }

                col = col / _Iterations;
                return col;
            }
            ENDCG
        }

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            sampler2D _MainTex;
            sampler2D _StartTex;

            float k;

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float4 originalCol = tex2D(_StartTex, i.uv);

                col = originalCol + k * (originalCol - col);

                return col;
            }
            ENDCG
        }
    }
}
