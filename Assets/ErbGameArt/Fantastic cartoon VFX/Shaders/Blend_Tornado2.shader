// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33637,y:32826,varname:node_4795,prsc:2|emission-5776-OUT,clip-798-OUT,voffset-8849-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2085-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-7666-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:798,x:33122,y:33044,varname:node_798,prsc:2|A-2053-A,B-797-A,C-8817-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7666,x:32235,y:33101,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_7666,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Vector4Property,id:4202,x:30937,y:32424,ptovrint:False,ptlb:Speed MainTex U/V + Noise Z/W,ptin:_SpeedMainTexUVNoiseZW,varname:node_4202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:-0.3,v3:0.1,v4:-0.2;n:type:ShaderForge.SFN_Append,id:7653,x:31154,y:32623,varname:node_7653,prsc:2|A-4202-X,B-4202-Y;n:type:ShaderForge.SFN_Append,id:3980,x:31154,y:32322,varname:node_3980,prsc:2|A-4202-Z,B-4202-W;n:type:ShaderForge.SFN_Multiply,id:5148,x:31341,y:32623,varname:node_5148,prsc:2|A-6731-T,B-7653-OUT;n:type:ShaderForge.SFN_Time,id:6731,x:31154,y:32467,varname:node_6731,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8420,x:31341,y:32322,varname:node_8420,prsc:2|A-3980-OUT,B-6731-T;n:type:ShaderForge.SFN_Tex2d,id:2252,x:31683,y:32321,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_2252,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3204-OUT;n:type:ShaderForge.SFN_Add,id:3204,x:31511,y:32321,varname:node_3204,prsc:2|A-6924-UVOUT,B-8420-OUT;n:type:ShaderForge.SFN_TexCoord,id:6924,x:31341,y:32457,varname:node_6924,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Add,id:2082,x:31515,y:32623,varname:node_2082,prsc:2|A-6924-UVOUT,B-5148-OUT;n:type:ShaderForge.SFN_Multiply,id:2653,x:31872,y:32321,varname:node_2653,prsc:2|A-2252-R,B-5248-OUT;n:type:ShaderForge.SFN_Slider,id:5248,x:31526,y:32498,ptovrint:False,ptlb:Noise Power,ptin:_NoisePower,varname:node_5248,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Add,id:2085,x:32053,y:32601,varname:node_2085,prsc:2|A-2653-OUT,B-2082-OUT;n:type:ShaderForge.SFN_Tex2d,id:1645,x:32235,y:33211,ptovrint:False,ptlb:Opacity Mask,ptin:_OpacityMask,varname:node_1645,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:23d01b6f277259844a9cf24e3580f375,ntxv:0,isnm:False|UVIN-1447-OUT;n:type:ShaderForge.SFN_Vector4Property,id:1410,x:31502,y:33251,ptovrint:False,ptlb:Opacity Mask Speed,ptin:_OpacityMaskSpeed,varname:node_1410,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:-0.2,v3:0,v4:0;n:type:ShaderForge.SFN_Add,id:1447,x:32068,y:33211,varname:node_1447,prsc:2|A-6924-UVOUT,B-2850-OUT;n:type:ShaderForge.SFN_Append,id:4072,x:31688,y:33251,varname:node_4072,prsc:2|A-1410-X,B-1410-Y;n:type:ShaderForge.SFN_Multiply,id:2850,x:31876,y:33232,varname:node_2850,prsc:2|A-6731-T,B-4072-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8447,x:31012,y:33444,ptovrint:False,ptlb:Number of wawes,ptin:_Numberofwawes,varname:node_4713,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.7;n:type:ShaderForge.SFN_Sin,id:2412,x:31401,y:33525,varname:node_2412,prsc:2|IN-1545-OUT;n:type:ShaderForge.SFN_Tau,id:8107,x:31065,y:33662,varname:node_8107,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1545,x:31218,y:33525,varname:node_1545,prsc:2|A-8447-OUT,B-9595-OUT,C-8107-OUT;n:type:ShaderForge.SFN_Add,id:9595,x:31032,y:33525,varname:node_9595,prsc:2|A-5167-G,B-4669-OUT;n:type:ShaderForge.SFN_Time,id:6450,x:30635,y:33684,varname:node_6450,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:5797,x:32246,y:33578,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:8849,x:32488,y:33549,varname:node_8849,prsc:2|A-5715-OUT,B-5797-OUT,C-8942-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:440,x:30462,y:33525,varname:node_440,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8942,x:32246,y:33739,ptovrint:False,ptlb:Vertex Power,ptin:_VertexPower,varname:node_251,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Multiply,id:4669,x:30809,y:33684,varname:node_4669,prsc:2|A-6450-T,B-6813-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6813,x:30635,y:33826,ptovrint:False,ptlb:Speed of wawes,ptin:_Speedofwawes,varname:node_7318,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-1;n:type:ShaderForge.SFN_Multiply,id:5715,x:31619,y:33525,varname:node_5715,prsc:2|A-2412-OUT,B-6924-V;n:type:ShaderForge.SFN_Transform,id:4993,x:30635,y:33525,varname:node_4993,prsc:2,tffrom:0,tfto:1|IN-440-XYZ;n:type:ShaderForge.SFN_ComponentMask,id:5167,x:30809,y:33525,varname:node_5167,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-4993-XYZ;n:type:ShaderForge.SFN_Desaturate,id:8702,x:32580,y:33210,varname:node_8702,prsc:2|COL-7948-OUT;n:type:ShaderForge.SFN_Multiply,id:6971,x:32741,y:33169,varname:node_6971,prsc:2|A-6924-Z,B-8702-OUT;n:type:ShaderForge.SFN_Add,id:8817,x:32931,y:33108,varname:node_8817,prsc:2|A-6924-Z,B-6971-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:5776,x:32889,y:32743,ptovrint:False,ptlb:Use fresnel?,ptin:_Usefresnel,varname:node_5776,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-2393-OUT,B-9025-OUT;n:type:ShaderForge.SFN_Fresnel,id:5100,x:32495,y:32601,varname:node_5100,prsc:2|EXP-5171-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5171,x:32235,y:32507,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_5171,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:9025,x:32693,y:32601,varname:node_9025,prsc:2|A-5100-OUT,B-2393-OUT;n:type:ShaderForge.SFN_Power,id:5930,x:31876,y:33372,varname:node_5930,prsc:2|VAL-6924-V,EXP-6649-OUT;n:type:ShaderForge.SFN_OneMinus,id:5343,x:32068,y:33372,varname:node_5343,prsc:2|IN-5930-OUT;n:type:ShaderForge.SFN_Clamp01,id:5925,x:32235,y:33372,varname:node_5925,prsc:2|IN-5343-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6649,x:31688,y:33417,ptovrint:False,ptlb:Up Desaturation,ptin:_UpDesaturation,varname:node_6649,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:7948,x:32413,y:33210,varname:node_7948,prsc:2|A-1645-RGB,B-5925-OUT;proporder:6074-2252-5248-4202-797-7666-1645-6649-1410-8447-8942-6813-5776-5171;pass:END;sub:END;*/

Shader "ErbGameArt/Particles/Blend_Tornado2" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _NoisePower ("Noise Power", Range(0, 1)) = 0.5
        _SpeedMainTexUVNoiseZW ("Speed MainTex U/V + Noise Z/W", Vector) = (0,-0.3,0.1,-0.2)
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Emission ("Emission", Float ) = 2
        _OpacityMask ("Opacity Mask", 2D) = "white" {}
        _UpDesaturation ("Up Desaturation", Float ) = 10
        _OpacityMaskSpeed ("Opacity Mask Speed", Vector) = (0,-0.2,0,0)
        _Numberofwawes ("Number of wawes", Float ) = 0.7
        _VertexPower ("Vertex Power", Float ) = 0.15
        _Speedofwawes ("Speed of wawes", Float ) = -1
        [MaterialToggle] _Usefresnel ("Use fresnel?", Float ) = 0
        _Fresnel ("Fresnel", Float ) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
			"PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
                       
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            //#pragma multi_compile_fwdbase
            //#pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Emission;
            uniform float4 _SpeedMainTexUVNoiseZW;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _NoisePower;
            uniform sampler2D _OpacityMask; uniform float4 _OpacityMask_ST;
            uniform float4 _OpacityMaskSpeed;
            uniform float _Numberofwawes;
            uniform float _VertexPower;
            uniform float _Speedofwawes;
            uniform fixed _Usefresnel;
            uniform float _Fresnel;
            uniform float _UpDesaturation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                v.vertex.xyz += ((sin((_Numberofwawes*(mul( unity_WorldToObject, float4(mul(unity_ObjectToWorld, v.vertex).rgb,0) ).xyz.rgb.rgb.g+(_Time.g*_Speedofwawes))*6.28318530718))*o.uv0.g)*v.normal*_VertexPower);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 node_1447 = (i.uv0+(_Time.g*float2(_OpacityMaskSpeed.r,_OpacityMaskSpeed.g)));
                float4 _OpacityMask_var = tex2D(_OpacityMask,TRANSFORM_TEX(node_1447, _OpacityMask));
                clip((i.vertexColor.a*_TintColor.a*(i.uv0.b+(i.uv0.b*dot((_OpacityMask_var.rgb*saturate((1.0 - pow(i.uv0.g,_UpDesaturation)))),float3(0.3,0.59,0.11))))) - 0.5);
                float2 node_3204 = (i.uv0+(float2(_SpeedMainTexUVNoiseZW.b,_SpeedMainTexUVNoiseZW.a)*_Time.g));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_3204, _Noise));
                float2 node_2085 = ((_Noise_var.r*_NoisePower)+(i.uv0+(_Time.g*float2(_SpeedMainTexUVNoiseZW.r,_SpeedMainTexUVNoiseZW.g))));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2085, _MainTex));
                float3 node_2393 = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_Emission);
                float3 emissive = lerp( node_2393, (pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel)+node_2393), _Usefresnel );
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
}