Shader "solar_outside2"
{
	Properties
	{
		[Header(Albedo)] [Space] [MainTexture]
		_Color("Color", Color) = (1,1,1,1)
		_Albedo("Albedo (RGB), Alpha()", 2D) = "white" {}

		[Header(Details)][Space][NoScaleOffset][Normal]
		_Normal("Normal(RGB)",2D) = "bump"{}

		[NoScaleOffset]
		_MaskMap("Mask Map (Metallic, Occlusion, Detail Mask, Smoothness)",2D) = "black" {}

		[Header(Emission)][Space][NoScaleOffset]
		_Emission("Emmision", 2D) = "black"{}
		_EmissionColor("Color",Color) = (1,1,1,1)
		_EmissionIntensity("Intensity",Float) = 1.0
		_EmissionGlow("Glow", Float) = 1.0
		_EmissionGlowDuration("GlowDuration", Float) = 5.0

		[Header(Mask)][Space]
		_ScrollingMask("Scrolling Mask",2D) = "white"{}
		_ScrollX("Scroll speed x", Float) = 0.2
		_ScrollY("Scroll speed y",Float) = 0.2
	}

		SubShader
		{
			Tags {
				"Queue" = "Geometry"
				"RenderType" = "Opaque"
			}


			CGPROGRAM

			#pragma target 3.0
			#include "UnityPBSLighting.cginc"
			#pragma surface surf Standard
			# pragma exclude_renderers gles

			struct Input
			{
				float2 uv_Albedo;
			};

			sampler2D _Albedo;
			float4 _Color;
			sampler2D _Normal;
			sampler2D _MaskMap;

			sampler2D _Emission;
			float4 _EmissionColor;
			float _EmissionIntensity;
			float _EmissionGlow;
			float _EmissionGlowDuration;

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 albedo = tex2D(_Albedo, IN.uv_Albedo);
				fixed4 mask = tex2D(_MaskMap, IN.uv_Albedo);
				fixed3 normal = UnpackScaleNormal(tex2D(_Normal, IN.uv_Albedo),1);
				fixed4 emission = tex2D(_Emission, IN.uv_Albedo);

				o.Albedo = albedo.rgb * _Color;
				o.Alpha = albedo.a;
				o.Normal = normal;
				o.Metallic = mask.r;
				o.Occlusion = mask.g;
				//o.DetailMask = mask.b;
				o.Smoothness = mask.a;

				o.Emission = emission.rgb * _EmissionColor * (_EmissionIntensity + abs(frac(_Time.y * (1 / _EmissionGlowDuration)) - 0.5) * _EmissionGlow);
			}
			ENDCG

			Pass
			{
				Tags{"RenderType" = "Fade"}
				LOD 200
				ZWrite On
				Blend DstColor Zero

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _ScrollingMask;
				float4 _ScrollingMask_ST;
				float _ScrollX;
				float _ScrollY;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _ScrollingMask);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{

				// do the shader moving (makes you sick in vr, so leave out)
				i.uv.x += _Time.y * _ScrollX;
				i.uv.y += _Time.y * _ScrollY;

				// sample the texture
				fixed4 col = tex2D(_ScrollingMask, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
			}
		}
			FallBack "Diffuse"
}
