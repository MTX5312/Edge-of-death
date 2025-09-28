Shader "Unlit/GridWorldOverlay"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _GridColor ("Grid Color", Color) = (0,1,0,1)
        _GridSize ("Grid Size", Float) = 1
        _LineWidth ("Line Width", Range(0.001, 0.1)) = 0.02
        _Enabled ("Grid Enabled", Float) = 1
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
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _GridSize;
            float _LineWidth;
            fixed4 _GridColor;
            float _Enabled;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // textura base del objeto
                fixed4 baseColor = tex2D(_MainTex, i.uv);

                if (_Enabled < 0.5) return baseColor;

                // cuadrícula en espacio de mundo (XZ)
                float2 g = frac(i.worldPos.xz / _GridSize);

                float gx = step(g.x, _LineWidth) + step(1 - g.x, _LineWidth);
                float gy = step(g.y, _LineWidth) + step(1 - g.y, _LineWidth);
                float gridLine = saturate(gx + gy);

                return (gridLine > 0) ? _GridColor : baseColor;
            }
            ENDCG
        }
    }
}