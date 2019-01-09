// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/distortionTrail" 
{
	Properties 
	{
		
		_Distortion ("_Distortion",Range (0.00, 10.0)) = 1.0
	 	_NormalMap ("_NormalMap", 2D) = "white" {}
	 	[MaterialToggle]  _UseDepth ("_UseDepth", float) = 1
	}

SubShader {
 
            Tags { "Queue" = "Transparent" }
     
            GrabPass {"_GrabTexture"}
     
         
            Pass {
                Name "GrabOffset"
                Cull Back
                ZWrite Off
                Blend Off
               
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
     
                #include "UnityCG.cginc"
     
                sampler2D _GrabTexture;
               
                sampler2D _NormalMap;
                float4 _NormalMap_ST;
               
                float _Distortion;
                fixed4 _Color;

                int _UseDepth;

                sampler2D _CameraDepthTexture;
               
                struct appdata 
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };
               
     
                struct v2f {
                    float4 pos : POSITION;
                    float2 texcoord : TEXCOORD0;
                    float4 GrabUV : TEXCOORD2;
                };
     
                v2f vert (appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos (v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _NormalMap);
     
                    o.GrabUV = ComputeGrabScreenPos(o.pos);
                   
                    return o;
                }
               
     
                fixed4 frag (v2f i) : COLOR
                {

                    float4 distort = tex2D(_NormalMap, i.texcoord);
                    _Distortion *= distort.a;

                    fixed4 DistUV = i.GrabUV + (distort * _Distortion );
                   
                    fixed4 refraction = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(DistUV));

                    if (_UseDepth)
                    {
						fixed4 refractionN = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.GrabUV));

						fixed refrFix = UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(DistUV)));

						if(LinearEyeDepth(refrFix) < i.GrabUV.z)
						{
							refraction = refractionN;
						}
					}

                    return fixed4(refraction.rgb,1);
                   
                }
                ENDCG
            }

    }
    }
