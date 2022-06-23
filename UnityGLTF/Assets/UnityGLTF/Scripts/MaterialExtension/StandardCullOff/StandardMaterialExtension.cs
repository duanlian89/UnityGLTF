using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKUnityGLTF;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using GLTF.Extensions;
using UnityGLTF.Extensions;

namespace CKUnityGLTF
{
	public class StandardMaterialExtension : IPropExtension
	{
		#region properties and default value
		public string[] shaderKeywords = new string[] { };

		public Color _Color = Color.white;
		public static readonly UnityEngine.Color _Color_Default = UnityEngine.Color.white;

		public TextureInfo _MainTex;
		public static readonly TextureInfo _MainTex_Default = null;

		public float _Cutoff = 0.0f;
		public static readonly float _Cutoff_Default = 0.5f;

		public float _Glossiness = 0.0f;
		public static readonly float _Glossiness_Default = 0.5f;

		public float _GlossMapScale = 0.0f;
		public static readonly float _GlossMapScale_Default = 1.0f;

		public float _SmoothnessTextureChannel = 0.0f;
		public static readonly float _SmoothnessTextureChannel_Default = 0.0f;

		public float _Metallic = 0.0f;
		public static readonly float _Metallic_Default = 0.0f;

		public TextureInfo _MetallicGlossMap;
		public static readonly TextureInfo _MetallicGlossMap_Default = null;

		public float _SpecularHighlights = 0.0f;
		public static readonly float _SpecularHighlights_Default = 1.0f;

		public float _GlossyReflections = 0.0f;
		public static readonly float _GlossyReflections_Default = 1.0f;

		public float _BumpScale = 1.0f;
		public static readonly float _BumpScale_Default = 1.0f;

		public TextureInfo _BumpMap;
		public static readonly TextureInfo _BumpMap_Default = null;

		public float _Parallax = 0.0f;
		public static readonly float _Parallax_Default = 0.02f;

		public TextureInfo _ParallaxMap;
		public static readonly TextureInfo _ParallaxMap_Default = null;

		public float _OcclusionStrength = 0.0f;
		public static readonly float _OcclusionStrength_Default = 1.0f;

		public TextureInfo _OcclusionMap;
		public static readonly TextureInfo _OcclusionMap_Default = null;

		public Color _EmissionColor;
		public static readonly Color _EmissionColor_Default = Color.black;

		public TextureInfo _EmissionMap;
		public static readonly TextureInfo _EmissionMap_Default;

		public TextureInfo _DetailMask;
		public static readonly TextureInfo _DetailMask_Default;

		public TextureInfo _DetailAlbedoMap;
		public static readonly TextureInfo _DetailAlbedoMap_Default = null;

		public float _DetailNormalMapScale;
		public static readonly float _DetailNormalMapScale_Default = 1.0f;

		public TextureInfo _DetailNormalMap;
		public static readonly TextureInfo _DetailNormalMap_Default = null;

		public float _UVSec;
		public static readonly float _UVSec_Default = 0.0f;

		public float _Mode = 1.0f;
		public static readonly float _Mode_Default = 0.0f;

		public float _SrcBlend = 1.0f;
		public static readonly float _SrcBlend_Default = 1.0f;

		public float _DstBlend = 0.0f;
		public static readonly float _DstBlend_Default = 0.0f;

		public float _ZWrite = 1.0f;
		public static readonly float _ZWrite_Default = 1.0f;
		#endregion

		public StandardMaterialExtension()
		{

		}

		public IExtension Clone(GLTFRoot root)
		{
			return new StandardMaterialExtension();
		}


		// properties to json
		// 用于导出时序列化属性名和属性值
		public JProperty Serialize()
		{
			JObject ext = new JObject();

			if (_Color != _Color_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._Color, new JArray(_Color.r, _Color.g, _Color.b, _Color.a)));
			}

			if (_MainTex != null)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._MainTex, new JObject(new JProperty(TextureInfo.INDEX, _MainTex.Index.Id))));
			}

			if (_Cutoff != _Cutoff_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._Cutoff, _Cutoff));
			}

			if (_Glossiness != _Glossiness_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._Glossiness, _Glossiness));
			}


			if (_GlossMapScale != _GlossMapScale_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._GlossMapScale, _GlossMapScale));
			}

			if (_BumpScale != _BumpScale_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._BumpScale, _BumpScale));
			}

			if (_BumpMap != null)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._BumpMap, new JObject(new JProperty(TextureInfo.INDEX, _BumpMap.Index.Id))));
			}

			if (_SmoothnessTextureChannel != _SmoothnessTextureChannel_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._SmoothnessTextureChannel, _SmoothnessTextureChannel));
			}

			if (_Metallic != _Metallic_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._Metallic, _Metallic));
			}

			if (_SpecularHighlights != _SpecularHighlights_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._SpecularHighlights, _SpecularHighlights));
			}

			if (_MetallicGlossMap != _MetallicGlossMap_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._MetallicGlossMap, new JObject(new JProperty(TextureInfo.INDEX, _MetallicGlossMap.Index.Id))));
			}


			if (_GlossyReflections != _GlossyReflections_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._GlossyReflections, _GlossyReflections));
			}

			if (_Parallax != _Parallax_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._Parallax, _Parallax));
			}

			if (_ParallaxMap != _ParallaxMap_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._ParallaxMap, new JObject(new JProperty(TextureInfo.INDEX, _ParallaxMap.Index.Id))));
			}

			if (_OcclusionStrength != _OcclusionStrength_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._OcclusionStrength, _OcclusionStrength));
			}

			if (_OcclusionMap != _OcclusionMap_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._OcclusionMap, new JObject(new JProperty(TextureInfo.INDEX, _OcclusionMap.Index.Id))));
			}

			if (_DetailMask != _DetailMask_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._DetailMask, new JObject(new JProperty(TextureInfo.INDEX, _DetailMask.Index.Id))));
			}

			if (_DetailAlbedoMap != _DetailAlbedoMap_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._DetailAlbedoMap, new JObject(new JProperty(TextureInfo.INDEX, _DetailAlbedoMap.Index.Id))));
			}

			if (_DetailNormalMapScale != _DetailNormalMapScale_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._DetailNormalMapScale, _DetailNormalMapScale));
			}

			if (_EmissionColor != _EmissionColor_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._EmissionColor, new JArray(_EmissionColor.r, _EmissionColor.g, _EmissionColor.b, _EmissionColor.a)));
			}

			if (_EmissionMap != null)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._EmissionMap, new JObject(new JProperty(TextureInfo.INDEX, _EmissionMap.Index.Id))));
			}

			if (_DetailNormalMap != _DetailNormalMap_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._DetailNormalMap, new JObject(new JProperty(TextureInfo.INDEX, _DetailNormalMap.Index.Id))));
			}

			if (_UVSec != _UVSec_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._UVSec, _UVSec));
			}

			if (_Mode != _Mode_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._Mode, _Mode));
			}

			if (_SrcBlend != _SrcBlend_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._SrcBlend, _SrcBlend));
			}

			if (_DstBlend != _DstBlend_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._DstBlend, _DstBlend));
			}

			if (_ZWrite != _ZWrite_Default)
			{
				ext.Add(new JProperty(StandardMaterialExtensionFactory._ZWrite, _ZWrite));
			}

			if (shaderKeywords.Length > 0)
			{
				System.Text.StringBuilder str = new System.Text.StringBuilder();
				for (int i = 0; i < shaderKeywords.Length; i++)
				{
					if (i > 0)
					{
						//分割符可根据需要自行修改
						str.Append(",");
					}
					str.Append(shaderKeywords[i]);
				}
				string keywords = str.ToString();
				ext.Add(new JProperty(StandardMaterialExtensionFactory.shaderKeywords, keywords));
			}

			return new JProperty(StandardMaterialExtensionFactory.Extension_Name, ext);
		}

		// json to MaterialExtension
		public void Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			JToken token = extensionToken.Value[StandardMaterialExtensionFactory._Cutoff];
			_Cutoff = token != null ? (float)token.DeserializeAsDouble() : _Cutoff_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._Color];
			_Color = token != null ? token.DeserializeAsColor().ToUnityColorRaw() : _Color_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._MainTex];
			_MainTex = token != null ? token.DeserializeAsTexture(root) : _MainTex_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._Glossiness];
			_Glossiness = token != null ? (float)token.DeserializeAsDouble() : _Glossiness_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._GlossMapScale];
			_GlossMapScale = token != null ? (float)token.DeserializeAsDouble() : _GlossMapScale_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._SmoothnessTextureChannel];
			_SmoothnessTextureChannel = token != null ? (float)token.DeserializeAsDouble() : _SmoothnessTextureChannel_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._Metallic];
			_Metallic = token != null ? (float)token.DeserializeAsDouble() : _Metallic_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._MetallicGlossMap];
			_MetallicGlossMap = token != null ? token.DeserializeAsTexture(root) : _MetallicGlossMap_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._SpecularHighlights];
			_SpecularHighlights = token != null ? (float)token.DeserializeAsDouble() : _SpecularHighlights_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._GlossyReflections];
			_GlossyReflections = token != null ? (float)token.DeserializeAsDouble() : _GlossyReflections_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._BumpScale];
			_BumpScale = token != null ? (float)token.DeserializeAsDouble() : _BumpScale_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._BumpMap];
			_BumpMap = token != null ? token.DeserializeAsTexture(root) : _BumpMap_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._Parallax];
			_Parallax = token != null ? (float)token.DeserializeAsDouble() : _Parallax_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._ParallaxMap];
			_ParallaxMap = token != null ? token.DeserializeAsTexture(root) : _ParallaxMap_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._OcclusionStrength];
			_OcclusionStrength = token != null ? (float)token.DeserializeAsDouble() : _OcclusionStrength_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._OcclusionStrength];
			_OcclusionMap = token != null ? token.DeserializeAsTexture(root) : _OcclusionMap_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._EmissionColor];
			_EmissionColor = token != null ? token.DeserializeAsColor().ToUnityColorRaw() : _EmissionColor_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._EmissionMap];
			_EmissionMap = token != null ? token.DeserializeAsTexture(root) : _EmissionMap_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._DetailMask];
			_DetailMask = token != null ? token.DeserializeAsTexture(root) : _DetailMask_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._DetailMask];
			_DetailAlbedoMap = token != null ? token.DeserializeAsTexture(root) : _DetailNormalMap_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._DetailMask];
			_DetailNormalMapScale = token != null ? (float)token.DeserializeAsDouble() : _DetailNormalMapScale_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._DetailNormalMap];
			_DetailNormalMap = token != null ? token.DeserializeAsTexture(root) : _DetailAlbedoMap_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._UVSec];
			_UVSec = token != null ? (float)token.DeserializeAsDouble() : _UVSec_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._Mode];
			_Mode = token != null ? (float)token.DeserializeAsDouble() : _Mode_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._SrcBlend];
			_SrcBlend = token != null ? (float)token.DeserializeAsDouble() : _SrcBlend_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._DstBlend];
			_DstBlend = token != null ? (float)token.DeserializeAsDouble() : _DstBlend_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory._ZWrite];
			_ZWrite = token != null ? (float)token.DeserializeAsDouble() : _ZWrite_Default;

			token = extensionToken.Value[StandardMaterialExtensionFactory.shaderKeywords];
			string[] reps = new string[] { "\\", "[", "]", "\"" };
			foreach (string s in reps)
			{
				token = token.ToString().Replace(s, "");
			}
			shaderKeywords = token != null ? token.ToString().Split(',') : shaderKeywords;
		}
	}
}
