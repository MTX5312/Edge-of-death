Shader "Unlit/NewUnlitShader"

{
    Properties
    {
        _GridColor ("Grid Color", Color) = (0,1,0,1)
        _Background ("Background Color", Color) = (1,1,1,1)
        _GridSize ("Grid Size", Float) = 10
        _LineWidth ("Line Width", Range(0.001, 0.1)) = 0.02
        _Enabled ("Grid Enabled", Float) = 1
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
                float2 uv : TEXCOORD0;
            };

            float _GridSize;
            float _LineWidth;
            fixed4 _GridColor;
            fixed4 _Background;
            float _Enabled;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _GridSize;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if (_Enabled < 0.5) return _Background;

                float2 g = frac(i.uv);
                float line = step(g.x, _LineWidth) + step(1 - g.x, _LineWidth) +
                             step(g.y, _LineWidth) + step(1 - g.y, _LineWidth);

                return (line > 0) ? _GridColor : _Background;
            }
            ENDCG
        }
    }
}