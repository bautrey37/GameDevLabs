Shader "JJ/Terrain" {
	Properties {
		_Color ("Color", Color) = (0,0,0,1)
		_Color2 ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;	
		fixed4 _Color;
		fixed4 _Color2;

		struct Input {
			float2 uv_MainTex;
			fixed3 viewDir;
		};

		void surf(Input IN, inout SurfaceOutputStandard o) {
			half4 data = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = data.r;

			/*o.Albedo = c.rgb;*/
			o.Albedo = lerp(_Color, _Color2, c);
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
