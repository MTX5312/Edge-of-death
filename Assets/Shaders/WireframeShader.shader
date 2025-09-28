Shader "Unlit/GridWorldOverlay"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" {}
        _GridColor("Grid Color", Color) = (118,118,118,225)
        _GridSize("Grid Size", Float) = 5
        _LineWidth("Line Width", Float) = 0.01
        _Enabled("Grid Enabled", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                float3 worldPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _GridColor;
            float _GridSize;
            float _LineWidth;
            float _Enabled;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if (_Enabled < 0.5) discard;

                float3 wp = i.worldPos;

                // calcular distancias a la cuadrícula en X, Y y Z
                float lineX = abs(frac(wp.x / _GridSize) - 0.5) / fwidth(wp.x / _GridSize);
                float lineY = abs(frac(wp.y / _GridSize) - 0.5) / fwidth(wp.y / _GridSize);
                float lineZ = abs(frac(wp.z / _GridSize) - 0.5) / fwidth(wp.z / _GridSize);

                float minDist = min(lineX, min(lineY, lineZ));
                float alpha = smoothstep(0.0, _LineWidth, 1.0 - minDist);

                fixed4 col = tex2D(_MainTex, i.uv);
                col = lerp(col, _GridColor, alpha);
                return col;
            }
            ENDCG
        }
    }
}