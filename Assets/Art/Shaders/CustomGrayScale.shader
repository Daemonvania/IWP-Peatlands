    Shader "Custom/Grayscale" {
        Properties {
            _MainTex ("Texture", 2D) = "white" {}
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 3.0

                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert (appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag (v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    //Compute grayscale value
                    fixed gray = (col.r + col.g + col.b) / 3;
                    //Create a new color with the grayscale value
                    fixed4 grayScaleColor = fixed4(gray, gray, gray, col.a);
                    return grayScaleColor;
                }
                ENDCG
            }
        }
    }