Shader "Custom/CloudsShader"
{

	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Velocity("Velocity", Range(0, 10)) = 0
	}
		SubShader
		{
			Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
			LOD 200

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest Off

			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows alpha:fade
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex : TEXCOORD0;
			};

			fixed4 _Color;
			float _Velocity;

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				float2 uv = IN.uv_MainTex;
				uv.x += _Velocity * _Time.y;
				fixed4 c = tex2D(_MainTex, uv) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Standard"
}
