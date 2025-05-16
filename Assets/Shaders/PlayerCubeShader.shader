Shader "Unlit/PlayerCubeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowTex ("Glow Texture", 2D) = "white" {}
        _LastFireTime ("Last Fire Time", Float) = 0
        _CurrTime ("Current Time", Float) = 0
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _GlowTex;
            float _CurrTime;
            float _LastFireTime;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainTexColor = tex2D(_MainTex, i.uv);

                // Calculate how much we should fade in the glow texture by
                // Want a short ramp up time and a slow ramp down time
                float timeSinceFire = _CurrTime - _LastFireTime;
                float rampDownTime = 0.5;

                float glowAmount = (-1 / rampDownTime) * timeSinceFire + 1; 
                fixed glowFactor = clamp(glowAmount, 0, 0.7);
                fixed4 color = fixed4(glowFactor, glowFactor * 0.6, glowFactor * 0.5, 0.7);

                UNITY_APPLY_FOG(i.fogCoord, color);
                return color;
            }
            ENDCG
        }
    }
}
