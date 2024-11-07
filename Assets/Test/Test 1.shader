Shader "Custom/RoundShaderWithOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", Float) = 0.5
        _OutlineThickness ("Outline Thickness", Float) = 0.05
        _OutlineColor ("Outline Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Radius;
            float _OutlineThickness;
            float4 _OutlineColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5); // Центр круга
                float2 uv = i.uv;

                // Вычисляем расстояние от центра
                float dist = distance(uv, center);

                // Проверяем, находится ли точка внутри круга
                if (dist < _Radius)
                {
                    // Если внутри круга, возвращаем текстуру
                    fixed4 col = tex2D(_MainTex, uv);
                    return col;
                }
                else if (dist < _Radius + _OutlineThickness)
                {
                    // Если в пределах толщины очертаний, возвращаем цвет очертаний
                    return _OutlineColor;
                }
                else
                {
                    // Если вне круга и очертаний, возвращаем прозрачный цвет
                    return fixed4(0, 0, 0, 0);
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}