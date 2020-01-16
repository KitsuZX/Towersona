// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CelShadingShader"
{
	Properties
	{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_NoiseTex("Noise", 2D) = "white" {}
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
		_XShift("Shift in the X direction", Float) = 0.1
		_YShift("Shift in the Y direction", Float) = 0.1
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf ToonRamp vertex:vert

		sampler2D _Ramp;

		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
		inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_SPOT_LIGHT
			lightDir = normalize(lightDir);
			#endif

			half d = dot(s.Normal, lightDir)*0.5 + 0.5;
			half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = 0;

			return c;
		}


		sampler2D _MainTex, _NoiseTex;
		float4 _Color;

		half _XShift;
		half _YShift;

		struct Input {
			float2 uv_MainTex : TEXCOORD0;
			float2 uv_NoiseTex : TEXCOORD1;
		};

		void vert(inout appdata_full v, out Input o)
		{
			o.uv_NoiseTex = o.uv_NoiseTex + float2(_XShift * _Time.x, _YShift* _Time.x);
			UNITY_INITIALIZE_OUTPUT(Input, o);
		}

		void surf(Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			half4 n = tex2D(_NoiseTex, IN.uv_NoiseTex) * _Color;
			o.Albedo = c.rgb * n.rgb;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG

		}

	Fallback "Diffuse"
}