Shader "Unlit/Matcap"
{
	Properties
	{
		[HDR]
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Material Capture", 2D) = "white" {}
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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float3 normal : NORMAL;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			float4 _Color;
			sampler2D _MainTex;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				float3 worldNorm = UnityObjectToWorldNormal(v.normal);
				o.normal = mul((float3x3)UNITY_MATRIX_V, v.normal);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _Color;

				//col.rgb = abs(normal);

				float3 normal = normalize(i.normal);
				float2 uv = fixed2( //IDK what projection this is, probably equirectangular.
					atan2(-normal.z, -normal.x),
					atan2(-normal.z, -normal.y)
				) / UNITY_FOUR_PI;

				col = tex2D(_MainTex, uv) * _Color;
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
