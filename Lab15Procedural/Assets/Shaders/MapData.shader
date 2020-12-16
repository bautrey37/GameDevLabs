// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//      Author : Ian McEwan, Ashima Arts.
//  Maintainer : ijm
//     License : Copyright (C) 2011 Ashima Arts. All rights reserved.
//               Distributed under the MIT License. See LICENSE file.

Shader "JJ/MapData"
{
	Properties
	{
		_Delta("delta", Float) = 0.0
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

			#include "Noise.cginc"
			#include "UnityCG.cginc"

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

			float _Delta;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			half4 frag (v2f i) : SV_Target
			{					
				float2 pos = (i.uv + _Delta);

				float dx = simplexNoise(pos * 6.9 + 69);
				float dy = simplexNoise(pos * 6.9 - 69);

				pos += float2(dx, dy) * 0.01;

				float h = simplexNoise(pos * 2);
				float h2 = simplexNoise(pos * 3.2);
				float h3 = simplexNoise(pos * 6.2);
				/* h = (h + 1) * 0.5; */
				h = abs(h);
				h2 = abs(h2);
				h3 = 1 - abs(h3);

				float c = h * 0.5 + h2 * 0.4 + h3 * 0.3;

				return half4(c, c, c, 1.0);
			}
			ENDCG
		}
	}
}
