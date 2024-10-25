Shader "Unlit/Glow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mirrors ("Mirrors", Float) = 7
        _Color ("Color", Color) = (1,1,1,1)
        _RotateSpeed ("Rotate Speed", Float) = 0.1
        _ShrinkSize ("Shrink Size", Float) = 0.2
        _ShrinkPeriod ("Shrink Period", Float) = 1
    }
    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #define PI2 6.28318530718

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            int _Mirrors;
            float4 _Color;
            float _RotateSpeed;
            float _ShrinkSize;
            float _ShrinkPeriod;
            float _Opacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv * 2 - 1;
                float a = atan2(uv.y, uv.x) / PI2 + 0.5;
                a += _Time.y * _RotateSpeed;

                float m = floor(a * 2 * _Mirrors) % 2;

                float d = max(0, 1 - length(uv));

                float dt = sin(_Time.y * _ShrinkPeriod) * 0.5 + 0.5;
                float shrinkOffset = dt * _ShrinkSize;
                float d2 = smoothstep(0 + shrinkOffset, 0.8 + shrinkOffset, d);

                //d = lerp(d, d * _ShrinkSize, dt);

                float v = m * d2 + d * d;


                return v * _Color;
            }
            ENDCG
        }
    }
}
