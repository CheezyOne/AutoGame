Shader "Custom/Invert"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Radius("Radius", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 200
        Blend OneMinusDstColor Zero

        PASS
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0 Alpha:Blend

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

            fixed4 _Color;
            float _Radius;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; // Передаем UV координаты из вершины
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Нормализуем UV координаты в диапазоне от -1 до 1
                float2 normalizedUV = (i.uv - 0.5) * 2.0;
                float distanceFromCenter = length(normalizedUV);

                // Проверяем, находится ли точка внутри радиуса
                if (distanceFromCenter > _Radius)
                {
                    discard; // Отбрасываем фрагменты вне радиуса
                }

                return _Color; // Возвращаем цвет, если точка внутри круга
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}