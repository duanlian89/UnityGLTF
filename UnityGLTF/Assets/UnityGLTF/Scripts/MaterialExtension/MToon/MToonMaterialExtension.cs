using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using GLTF.Extensions;
using UnityGLTF.Extensions;

namespace CKUnityGLTF
{
	public class MToonMaterialExtension : IPropExtension
	{
		#region properties and default value
		public int renderQueue;

		public string[] shaderKeywords = new string[] { };

		public float _Cutoff = 0.0f;
		public static readonly float _Cutoff_Default = 0.5f;

		public Color _Color = Color.white;
		public static readonly UnityEngine.Color _Color_Default = UnityEngine.Color.white;

		public Color _LinearLitColor = new Color(0.5f, 0.5f, 0.5f, 1);
		public static readonly Color _LinearLitColor_Default = new Color(0.5f, 0.5f, 0.5f, 1);

		public Color _ColorOL = Color.white;
		public static readonly Color _ColorOL_Default = new Color(1, 1, 1, 0);

		public Color _ShadeColor = Color.white;
		public static readonly Color _ShadeColor_Default = new Color(0.97f, 0.81f, 0.86f, 1);

		public TextureInfo _MainTex;
		public static readonly TextureInfo _MainTex_Default = null;

		public TextureInfo _MainTex2;
		public static readonly TextureInfo _MainTex2_Default = null;

		public TextureInfo _ShadeTexture;
		public static readonly TextureInfo _ShadeTexture_Default = null;

		public float _BumpScale = 1.0f;
		public static readonly float _BumpScale_Default = 1.0f;

		public TextureInfo _BumpMap;
		public static readonly TextureInfo _BumpMap_Default = null;

		public TextureInfo _SpecTexture;
		public static readonly TextureInfo _SpecTexture_Default = null;

		public float _ReceiveShadowRate = 1.0f;
		public static readonly float _ReceiveShadowRate_Default = 1.0f;

		public TextureInfo _ReceiveShadowTexture;
		public static readonly TextureInfo _ReceiveShadowTexture_Default = null;

		public float _ShadingGradeRate = 1.0f;
		public static readonly float _ShadingGradeRate_Default = 1.0f;

		public TextureInfo _ShadingGradeTexture;
		public static readonly TextureInfo _ShadingGradeTexture_Default = null;

		public float _ShadeShift = 0;
		public static readonly float _ShadeShift_Default = 0;

		public float _ShadeToony = 0.9f;
		public static readonly float _ShadeToony_Default = 0.9f;

		public float _LightColorAttenuation = 0;
		public static readonly float _LightColorAttenuation_Default = 0;

		public float _IndirectLightIntensity = 0.1f;
		public static readonly float _IndirectLightIntensity_Default = 0.1f;

		public Color _RimColor;
		public static readonly Color _RimColor_Default = Color.white;

		public TextureInfo _RimTexture;
		public static readonly TextureInfo _RimTexture_Default = null;

		public float _RimLightingMix = 0;
		public static readonly float _RimLightingMix_Default = 0;

		public float _RimFresnelPower = 1;
		public static readonly float _RimFresnelPower_Default = 1;

		public float _RimLift = 0;
		public static readonly float _RimLift_Default = 0;

		public TextureInfo _SphereAdd;
		public static readonly TextureInfo _SphereAdd_Default;

		public Color _EmissionColor;
		public static readonly Color _EmissionColor_Default = Color.black;

		public TextureInfo _EmissionMap;
		public static readonly TextureInfo _EmissionMap_Default;

		public TextureInfo _OutlineWidthTexture;
		public static readonly TextureInfo _OutlineWidthTexture_Default;

		public float _OutlineWidth = 0.5f;
		public static readonly float _OutlineWidth_Default = 0.5f;

		public float _OutlineScaledMaxDistance = 1;
		public static readonly float _OutlineScaledMaxDistance_Default = 1;

		public Color _OutlineColor;
		public static readonly Color _OutlineColor_Default = new Color(0, 0, 0, 1);

		public float _OutlineLightingMix = 1;
		public static readonly float _OutlineLightingMix_Default = 1;

		public TextureInfo _UvAnimMaskTexture;
		public static readonly TextureInfo _UvAnimMaskTexture_Default;

		public float _UvAnimScrollX = 0;
		public static readonly float _UvAnimScrollX_Default = 0;

		public float _UvAnimScrollY = 0;
		public static readonly float _UvAnimScrollY_Default = 0;

		public float _UvAnimRotation = 0;
		public static readonly float _UvAnimRotation_Default = 0;

		public float _MToonVersion = 34;
		public static readonly float _MToonVersion_Default = 34;

		public float _DebugMode = 0.0f;
		public static readonly float _DebugMode_Default = 0.0f;

		public float _BlendMode = 0.0f;
		public static readonly float _BlendMode_Default = 0.0f;

		public float _OutlineWidthMode = 0.0f;
		public static readonly float _OutlineWidthMode_Default = 0.0f;

		public float _OutlineColorMode = 0.0f;
		public static readonly float _OutlineColorMode_Default = 0.0f;

		public float _CullMode = 2.0f;
		public static readonly float _CullMode_Default = 2.0f;

		public float _OutlineCullMode = 1.0f;
		public static readonly float _OutlineCullMode_Default = 1.0f;

		public float _SrcBlend = 1.0f;
		public static readonly float _SrcBlend_Default = 1.0f;

		public float _DstBlend = 0.0f;
		public static readonly float _DstBlend_Default = 0.0f;

		public float _ZWrite = 1.0f;
		public static readonly float _ZWrite_Default = 1.0f;

		public float _AlphaToMask = 0.0f;
		public static readonly float _AlphaToMask_Default = 0.0f;

		public float _SpecMulti = 0.0f;
		public static readonly float _SpecMulti_Default = 0.0f;

		public float _SpecRange = 1.0f;
		public static readonly float _SpecRange_Default = 1.0f;

		public float _SpecSoftness = 0.0f;
		public static readonly float _SpecSoftness_Default = 0.0f;

		public TextureInfo _PatternTexture;
		public static readonly TextureInfo _PatternTexture_Default;

		public TextureInfo _SocksTexture;
		public static readonly TextureInfo _SocksTexture_Default;

		public Color _SpecColor;
		public static readonly Color _SpecColor_Default = Color.white;

		public float _RimSoftness = 0.0f;
		public static readonly float _RimSoftness_Default = 0.0f;

		public float _RimWeight = 0.0f;
		public static readonly float _RimWeight_Default = 1.0f;

		public float _RimSphereWeight = 0.0f;
		public static readonly float _RimSphereWeight_Default = 0.0f;

		public int _IsGrayTex = 1;
		public static readonly int _IsGrayTex_Default = 1;

		#endregion

		public MToonMaterialExtension()
		{

		}

		public virtual IExtension Clone(GLTFRoot root)
		{
			return new MToonMaterialExtension();
		}


		// properties to json
		// 用于导出时序列化属性名和属性值
		public virtual JProperty Serialize()
		{
			JObject ext = new JObject();

			if (_SpecMulti != _SpecMulti_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._SpecMulti, _SpecMulti));
			}

			if (_SpecRange != _SpecRange_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._SpecRange, _SpecRange));
			}

			if (_SpecSoftness != _SpecSoftness_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._SpecSoftness, _SpecSoftness));
			}

			if (_Cutoff != _Cutoff_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._Cutoff, _Cutoff));
			}

			if (_Color != _Color_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._Color, new JArray(_Color.r, _Color.g, _Color.b, _Color.a)));
			}

			if (_ColorOL != _ColorOL_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ColorOL, new JArray(_ColorOL.r, _ColorOL.g, _ColorOL.b, _ColorOL.a)));
			}

			if (_ShadeColor != _ShadeColor_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ShadeColor, new JArray(_ShadeColor.r, _ShadeColor.g, _ShadeColor.b, _ShadeColor.a)));
			}

			if (_MainTex != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._MainTex, new JObject(new JProperty(TextureInfo.INDEX, _MainTex.Index.Id))));
			}

			if (_MainTex2 != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._MainTex2, new JObject(new JProperty(TextureInfo.INDEX, _MainTex2.Index.Id))));
			}


			if (_ShadeTexture != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ShadeTexture, new JObject(new JProperty(TextureInfo.INDEX, _ShadeTexture.Index.Id))));
			}

			if (_BumpScale != _BumpScale_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._BumpScale, _BumpScale));
			}

			if (_BumpMap != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._BumpMap, new JObject(new JProperty(TextureInfo.INDEX, _BumpMap.Index.Id))));
			}

			if (_ReceiveShadowRate != _ReceiveShadowRate_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ReceiveShadowRate, _ReceiveShadowRate));
			}

			if (_ReceiveShadowTexture != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ReceiveShadowTexture, new JObject(new JProperty(TextureInfo.INDEX, _ReceiveShadowTexture.Index.Id))));
			}

			if (_ShadingGradeRate != _ShadingGradeRate_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ShadingGradeRate, _ShadingGradeRate));
			}

			if (_ShadingGradeTexture != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ShadingGradeTexture, new JObject(new JProperty(TextureInfo.INDEX, _ShadingGradeTexture.Index.Id))));
			}


			if (_ShadeShift != _ShadeShift_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ShadeShift, _ShadeShift));
			}

			if (_ShadeToony != _ShadeToony_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ShadeToony, _ShadeToony));
			}

			if (_LightColorAttenuation != _LightColorAttenuation_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._LightColorAttenuation, _LightColorAttenuation));
			}

			if (_IndirectLightIntensity != _IndirectLightIntensity_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._IndirectLightIntensity, _IndirectLightIntensity));
			}

			if (_RimColor != _RimColor_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._RimColor, new JArray(_RimColor.r, _RimColor.g, _RimColor.b, _RimColor.a)));
			}

			if (_RimTexture != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._RimTexture, new JObject(new JProperty(TextureInfo.INDEX, _RimTexture.Index.Id))));
			}

			if (_RimLightingMix != _RimLightingMix_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._RimLightingMix, _RimLightingMix));
			}

			if (_RimFresnelPower != _RimFresnelPower_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._RimFresnelPower, _RimFresnelPower));
			}

			if (_RimLift != _RimLift_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._RimLift, _RimLift));
			}

			if (_SphereAdd != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._SphereAdd, new JObject(new JProperty(TextureInfo.INDEX, _SphereAdd.Index.Id))));
			}


			if (_EmissionColor != _EmissionColor_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._EmissionColor, new JArray(_EmissionColor.r, _EmissionColor.g, _EmissionColor.b, _EmissionColor.a)));
			}

			if (_EmissionMap != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._EmissionMap, new JObject(new JProperty(TextureInfo.INDEX, _EmissionMap.Index.Id))));
			}

			if (_OutlineWidthTexture != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineWidthTexture, new JObject(new JProperty(TextureInfo.INDEX, _OutlineWidthTexture.Index.Id))));
			}

			if (_OutlineWidth != _OutlineWidth_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineWidth, _OutlineWidth));
			}

			if (_OutlineScaledMaxDistance != _OutlineScaledMaxDistance_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineScaledMaxDistance, _OutlineScaledMaxDistance));
			}

			if (_OutlineColor != _OutlineColor_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineColor, new JArray(_OutlineColor.r, _OutlineColor.g, _OutlineColor.b, _OutlineColor.a)));
			}

			if (_OutlineLightingMix != _OutlineLightingMix_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineLightingMix, _OutlineLightingMix));
			}

			if (_UvAnimMaskTexture != null)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._UvAnimMaskTexture, new JObject(new JProperty(TextureInfo.INDEX, _UvAnimMaskTexture.Index.Id))));
			}

			if (_UvAnimScrollX != _UvAnimScrollX_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._UvAnimScrollX, _UvAnimScrollX));
			}

			if (_UvAnimScrollY != _UvAnimScrollY_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._UvAnimScrollY, _UvAnimScrollY));
			}

			if (_UvAnimRotation != _UvAnimRotation_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._UvAnimRotation, _UvAnimRotation));
			}

			if (_MToonVersion != _MToonVersion_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._MToonVersion, _MToonVersion));
			}

			if (_DebugMode != _DebugMode_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._DebugMode, _DebugMode));
			}

			if (_BlendMode != _BlendMode_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._BlendMode, _BlendMode));
			}

			if (_OutlineWidthMode != _OutlineWidthMode_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineWidthMode, _OutlineWidthMode));
			}

			if (_OutlineColorMode != _OutlineColorMode_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineColorMode, _OutlineColorMode));
			}

			if (_CullMode != _CullMode_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._CullMode, _CullMode));
			}

			if (_OutlineCullMode != _OutlineCullMode_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._OutlineCullMode, _OutlineCullMode));
			}

			if (_SrcBlend != _SrcBlend_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._SrcBlend, _SrcBlend));
			}

			if (_DstBlend != _DstBlend_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._DstBlend, _DstBlend));
			}

			if (_ZWrite != _ZWrite_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._ZWrite, _ZWrite));
			}

			if (_AlphaToMask != _AlphaToMask_Default)
			{
				ext.Add(new JProperty(MToonMaterialExtensionFactory._AlphaToMask, _AlphaToMask));
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
				ext.Add(new JProperty(MToonMaterialExtensionFactory.shaderKeywords, keywords));
			}

			ext.Add(new JProperty(MToonMaterialExtensionFactory.renderQueue, renderQueue));

			if (_LinearLitColor != _LinearLitColor_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._LinearLitColor, new JArray(_LinearLitColor.r, _LinearLitColor.g, _LinearLitColor.b, _LinearLitColor.a)));
			}

			if (_SpecTexture != _SpecTexture_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._SpecTexture, new JObject(new JProperty(TextureInfo.INDEX, _SpecTexture.Index.Id))));
			}

			if (_IsGrayTex != _IsGrayTex_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._IsGrayTex, _IsGrayTex));
			}

			return new JProperty(MToonMaterialExtensionFactory.Extension_Name, ext);
		}

		// json to MToonMaterialExtension
		public virtual void Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			//MToonMaterialExtension ext = new MToonMaterialExtension();

			JToken token = extensionToken.Value[MToonMaterialExtensionFactory._Cutoff];
			_Cutoff = token != null ? (float)token.DeserializeAsDouble() : _Cutoff_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SpecMulti];
			_SpecMulti = token != null ? (float)token.DeserializeAsDouble() : _SpecMulti_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SpecRange];
			_SpecRange = token != null ? (float)token.DeserializeAsDouble() : _SpecRange_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SpecSoftness];
			_SpecSoftness = token != null ? (float)token.DeserializeAsDouble() : _SpecSoftness_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._Color];
			_Color = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _Color_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ColorOL];
			_ColorOL = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _ColorOL_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ShadeColor];
			_ShadeColor = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _ShadeColor_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._MainTex];
			_MainTex = token != null ? token.DeserializeAsTexture(root) : _MainTex_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._MainTex2];
			_MainTex2 = token != null ? token.DeserializeAsTexture(root) : _MainTex2_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ShadeTexture];
			_ShadeTexture = token != null ? token.DeserializeAsTexture(root) : _ShadeTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._BumpScale];
			_BumpScale = token != null ? (float)token.DeserializeAsDouble() : _BumpScale_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._BumpMap];
			_BumpMap = token != null ? token.DeserializeAsTexture(root) : _BumpMap_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ReceiveShadowRate];
			_ReceiveShadowRate = token != null ? (float)token.DeserializeAsDouble() : _ReceiveShadowRate_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ReceiveShadowTexture];
			_ReceiveShadowTexture = token != null ? token.DeserializeAsTexture(root) : _ReceiveShadowTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ShadingGradeRate];
			_ShadingGradeRate = token != null ? (float)token.DeserializeAsDouble() : _ShadingGradeRate_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ShadingGradeTexture];
			_ShadingGradeTexture = token != null ? token.DeserializeAsTexture(root) : _ShadingGradeTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ShadeShift];
			_ShadeShift = token != null ? (float)token.DeserializeAsDouble() : _ShadeShift_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ShadeToony];
			_ShadeToony = token != null ? (float)token.DeserializeAsDouble() : _ShadeToony_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._LightColorAttenuation];
			_LightColorAttenuation = token != null ? (float)token.DeserializeAsDouble() : _LightColorAttenuation_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._IndirectLightIntensity];
			_IndirectLightIntensity = token != null ? (float)token.DeserializeAsDouble() : _IndirectLightIntensity_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimColor];
			_RimColor = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _RimColor_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimTexture];
			_RimTexture = token != null ? token.DeserializeAsTexture(root) : _RimTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimLightingMix];
			_RimLightingMix = token != null ? (float)token.DeserializeAsDouble() : _RimLightingMix_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimFresnelPower];
			_RimFresnelPower = token != null ? (float)token.DeserializeAsDouble() : _RimFresnelPower_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimLift];
			_RimLift = token != null ? (float)token.DeserializeAsDouble() : _RimLift_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SphereAdd];
			_SphereAdd = token != null ? token.DeserializeAsTexture(root) : _SphereAdd_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._EmissionColor];
			_EmissionColor = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _EmissionColor_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._EmissionMap];
			_EmissionMap = token != null ? token.DeserializeAsTexture(root) : _EmissionMap_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineWidthTexture];
			_OutlineWidthTexture = token != null ? token.DeserializeAsTexture(root) : _OutlineWidthTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineWidth];
			_OutlineWidth = token != null ? (float)token.DeserializeAsDouble() : _OutlineWidth_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineScaledMaxDistance];
			_OutlineScaledMaxDistance = token != null ? (float)token.DeserializeAsDouble() : _OutlineScaledMaxDistance_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineColor];
			_OutlineColor = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _OutlineColor_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineLightingMix];
			_OutlineLightingMix = token != null ? (float)token.DeserializeAsDouble() : _OutlineLightingMix_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._UvAnimMaskTexture];
			_UvAnimMaskTexture = token != null ? token.DeserializeAsTexture(root) : _UvAnimMaskTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._UvAnimScrollX];
			_UvAnimScrollX = token != null ? (float)token.DeserializeAsDouble() : _UvAnimScrollX_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._UvAnimScrollY];
			_UvAnimScrollY = token != null ? (float)token.DeserializeAsDouble() : _UvAnimScrollY_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._UvAnimRotation];
			_UvAnimRotation = token != null ? (float)token.DeserializeAsDouble() : _UvAnimRotation_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._MToonVersion];
			_MToonVersion = token != null ? (float)token.DeserializeAsDouble() : _MToonVersion_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._DebugMode];
			_DebugMode = token != null ? (float)token.DeserializeAsDouble() : _DebugMode_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._BlendMode];
			_BlendMode = token != null ? (float)token.DeserializeAsDouble() : _BlendMode_Default;


			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineWidthMode];
			_OutlineWidthMode = token != null ? (float)token.DeserializeAsDouble() : _OutlineWidthMode_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineColorMode];
			_OutlineColorMode = token != null ? (float)token.DeserializeAsDouble() : _OutlineColorMode_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._CullMode];
			_CullMode = token != null ? (float)token.DeserializeAsDouble() : _CullMode_Default;


			token = extensionToken.Value[MToonMaterialExtensionFactory._OutlineCullMode];
			_OutlineCullMode = token != null ? (float)token.DeserializeAsDouble() : _OutlineCullMode_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SrcBlend];
			_SrcBlend = token != null ? (float)token.DeserializeAsDouble() : _SrcBlend_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._DstBlend];
			_DstBlend = token != null ? (float)token.DeserializeAsDouble() : _DstBlend_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._ZWrite];
			_ZWrite = token != null ? (float)token.DeserializeAsDouble() : _ZWrite_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._AlphaToMask];
			_AlphaToMask = token != null ? (float)token.DeserializeAsDouble() : _AlphaToMask_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory.shaderKeywords];
			if (token != null)
			{
				string[] reps = new string[] { "\\", "[", "]", "\"" };
				foreach (string s in reps)
				{
					token = token.ToString().Replace(s, "");
				}
			}
			shaderKeywords = token != null ? token.ToString().Split(',') : shaderKeywords;

			token = extensionToken.Value[MToonMaterialExtensionFactory.renderQueue];
			renderQueue = token != null ? token.DeserializeAsInt() : renderQueue;


			token = extensionToken.Value[MToonMaterialExtensionFactory._LinearLitColor];
			_LinearLitColor = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _LinearLitColor_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SpecTexture];
			_SpecTexture = token != null ? token.DeserializeAsTexture(root) : _SpecTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._PatternTexture];
			_PatternTexture = token != null ? token.DeserializeAsTexture(root) : _PatternTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SocksTexture];
			_SocksTexture = token != null ? token.DeserializeAsTexture(root) : _SocksTexture_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._SpecColor];
			_SpecColor = token != null ? token.DeserializeAsColor().ToUnityColorLinear() : _SpecColor_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimSoftness];
			_RimSoftness = token != null ? (float)token.DeserializeAsDouble() : _RimSoftness_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimWeight];
			_RimWeight = token != null ? (float)token.DeserializeAsDouble() : _RimWeight_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._RimSphereWeight];
			_RimSphereWeight = token != null ? (float)token.DeserializeAsDouble() : _RimSphereWeight_Default;

			token = extensionToken.Value[MToonMaterialExtensionFactory._IsGrayTex];
			_IsGrayTex = token != null ? token.DeserializeAsInt() : _IsGrayTex_Default;
		}
	}
}
