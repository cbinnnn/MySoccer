// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Toon and Diffuse" {
   Properties {
      _Color ("Diffuse Color", Color) = (1,1,1,1) 
      _UnlitColor ("Unlit Diffuse Color", Color) = (0.5,0.5,0.5,1) 
      _DiffuseThreshold ("Threshold for Diffuse Colors", Range(0,1)) 
         = 0.1 
      _OutlineColor ("Outline Color", Color) = (0,0,0,1)
      _LitOutlineThickness ("Lit Outline Thickness", Range(0,1)) = 0.1
      _UnlitOutlineThickness ("Unlit Outline Thickness", Range(0,1)) 
         = 0.4
      _SpecColor ("Specular Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 10
      
	  _DetailMaskTex ("Detail Mask Texture", 2D) = "black" {} 
	  _DetailColor ("Detail Color", Color) = (1,1,1,1) 
   }
   SubShader {
      Pass {      
         Tags { "LightMode" = "ForwardBase" } 
            // pass for ambient light and first light source
 
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
                  
         uniform float4 _LightColor0; 
            // color of light source (from "Lighting.cginc")
 
         // User-specified properties
         uniform float4 _Color; 
         uniform float4 _UnlitColor;
         uniform float _DiffuseThreshold;
         uniform float4 _OutlineColor;
         uniform float _LitOutlineThickness;
         uniform float _UnlitOutlineThickness;
         uniform float4 _SpecColor; 
         uniform float _Shininess;
         uniform float4 _DetailColor;
	 
		 sampler2D _DetailMaskTex;
		  
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float4 uv : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posWorld : TEXCOORD0;
            float3 normalDir : TEXCOORD1;
			float4 uv : TEXCOORD2;
         };
         
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject; 
               // multiplication with unity_Scale.w is unnecessary 
               // because we normalize transformed vectors
 
            output.posWorld = mul(modelMatrix, input.vertex);
            output.normalDir = normalize(float4(mul(float4(input.normal, 0.0), modelMatrixInverse)));
            output.pos = UnityObjectToClipPos(input.vertex);
            output.uv = input.uv; 
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float3 normalDirection = normalize(input.normalDir);
 
            float3 viewDirection = normalize(
               _WorldSpaceCameraPos - float4(input.posWorld));
            float3 lightDirection;
            float attenuation;
 
            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1.0; // no attenuation
               lightDirection = normalize(float4(_WorldSpaceLightPos0));
            } 
            else // point or spot light
            {
               float3 vertexToLightSource = 
                  float3((float3)_WorldSpaceLightPos0 - input.posWorld);
               float distance = length(vertexToLightSource);
               attenuation = 1.0 / distance; // linear attenuation 
               lightDirection = normalize(vertexToLightSource);
            }
 
            // default: unlit 
            float3 fragmentColor = _UnlitColor; 
 
            // low priority: diffuse illumination
            if (attenuation 
               * max(0.0, dot(normalDirection, lightDirection)) 
               >= _DiffuseThreshold)
            {
               fragmentColor = _LightColor0 * _Color; 
            }
 
            // higher priority: outline
            if (dot(viewDirection, normalDirection) 
               < lerp(_UnlitOutlineThickness, _LitOutlineThickness, 
               max(0.0, dot(normalDirection, lightDirection))))
            {
               fragmentColor = _LightColor0 * _OutlineColor; 
            }
 
            // highest priority: highlights
            if (dot(normalDirection, lightDirection) > 0.0 
               // light source on the right side?
               && attenuation *  pow(max(0.0, dot(
               reflect(-lightDirection, normalDirection), 
               viewDirection)), _Shininess) > 0.5) 
               // more than half highlight intensity? 
            {
               fragmentColor = _SpecColor.a * _LightColor0 * _SpecColor + (1.0 - _SpecColor.a) * fragmentColor;
            }
 
 			float3 detailMask = tex2D(_DetailMaskTex, input.uv.xy).rgb;
 			fragmentColor.rgb += (detailMask.rgb*_DetailColor.rgb);
 			 
            return float4(fragmentColor, 1.0);
         }
 
         ENDCG
      }
 
      Pass {      
         Tags { "LightMode" = "ForwardAdd" } 
            // pass for additional light sources
         Blend SrcAlpha OneMinusSrcAlpha 
            // blend specular highlights over framebuffer
 
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
         uniform float4 _LightColor0; 
            // color of light source (from "Lighting.cginc")
 
         // User-specified properties
         uniform float4 _Color; 
         uniform float4 _UnlitColor;
         uniform float _DiffuseThreshold;
         uniform float4 _OutlineColor;
         uniform float _LitOutlineThickness;
         uniform float _UnlitOutlineThickness;
         uniform float4 _SpecColor; 
         uniform float _Shininess;
         uniform float4 _DetailColor;
	 
		 sampler2D _DetailMaskTex;
		  
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float4 uv : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posWorld : TEXCOORD0;
            float3 normalDir : TEXCOORD1;
			float4 uv : TEXCOORD2;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject; 
               // multiplication with unity_Scale.w is unnecessary 
               // because we normalize transformed vectors
 
            output.posWorld = mul(modelMatrix, input.vertex);
            output.normalDir = normalize( mul(float4(input.normal, 0.0), modelMatrixInverse));
            output.pos = UnityObjectToClipPos(input.vertex);
            output.uv = input.uv;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float3 normalDirection = normalize(input.normalDir);
 
            float3 viewDirection = normalize(_WorldSpaceCameraPos - input.posWorld);
            float3 lightDirection;
            float attenuation;
 
            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1.0; // no attenuation
               lightDirection = 
                  normalize(_WorldSpaceLightPos0);
            } 
            else // point or spot light
            {
               float3 vertexToLightSource = _WorldSpaceLightPos0 - input.posWorld;
               float distance = length(vertexToLightSource);
               attenuation = 1.0 / distance; // linear attenuation 
               lightDirection = normalize(vertexToLightSource);
            }
 
            float4 fragmentColor = float4(0.0, 0.0, 0.0, 0.0);
            if (dot(normalDirection, lightDirection) > 0.0 
               // light source on the right side?
               && attenuation *  pow(max(0.0, dot(
               reflect(-lightDirection, normalDirection), 
               viewDirection)), _Shininess) > 0.5) 
               // more than half highlight intensity? 
            {
               fragmentColor = 
                  float4(_LightColor0.rgb, 1.0) * _SpecColor;
            }
  			
 			float3 detailMask = tex2D(_DetailMaskTex, input.uv.xy).rgb;
 			fragmentColor.rgb += (detailMask.rgb*_DetailColor.rgb);
 			
            return fragmentColor;
         }
 
         ENDCG
      }
   } 
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Specular"
}