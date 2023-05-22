using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using GLTF.Extensions;
using UnityGLTF.Extensions;
using CKUnityGLTF;

namespace CKUnityGLTF
{
	public class MToonClothingMaterialExtension : MToonMaterialExtension
	{
		#region properties and default value

		public TextureInfo _Mask1;
		public static readonly TextureInfo _Mask1_Default = null;

		#endregion

		public MToonClothingMaterialExtension()
		{
		}

		public override IExtension Clone(GLTFRoot root)
		{
			return new MToonClothingMaterialExtension();
		}

		override public JProperty Serialize()
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
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._Cutoff, _Cutoff));
			}

			if (_Color != _Color_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._Color, new JArray(_Color.r, _Color.g, _Color.b, _Color.a)));
			}

			if (_ColorOL != _ColorOL_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ColorOL, new JArray(_ColorOL.r, _ColorOL.g, _ColorOL.b, _ColorOL.a)));
			}

			if (_ShadeColor != _ShadeColor_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ShadeColor, new JArray(_ShadeColor.r, _ShadeColor.g, _ShadeColor.b, _ShadeColor.a)));
			}

			if (_MainTex != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._MainTex, new JObject(new JProperty(TextureInfo.INDEX, _MainTex.Index.Id))));
			}

			if (_MainTex2 != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._MainTex2, new JObject(new JProperty(TextureInfo.INDEX, _MainTex2.Index.Id))));
			}

			if (_Mask1 != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._Mask1, new JObject(new JProperty(TextureInfo.INDEX, _Mask1.Index.Id))));
			}

			if (_ShadeTexture != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ShadeTexture, new JObject(new JProperty(TextureInfo.INDEX, _ShadeTexture.Index.Id))));
			}

			if (_BumpScale != _BumpScale_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._BumpScale, _BumpScale));
			}

			if (_BumpMap != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._BumpMap, new JObject(new JProperty(TextureInfo.INDEX, _BumpMap.Index.Id))));
			}

			if (_ReceiveShadowRate != _ReceiveShadowRate_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ReceiveShadowRate, _ReceiveShadowRate));
			}

			if (_ReceiveShadowTexture != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ReceiveShadowTexture, new JObject(new JProperty(TextureInfo.INDEX, _ReceiveShadowTexture.Index.Id))));
			}

			if (_ShadingGradeRate != _ShadingGradeRate_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ShadingGradeRate, _ShadingGradeRate));
			}

			if (_ShadingGradeTexture != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ShadingGradeTexture, new JObject(new JProperty(TextureInfo.INDEX, _ShadingGradeTexture.Index.Id))));
			}


			if (_ShadeShift != _ShadeShift_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ShadeShift, _ShadeShift));
			}

			if (_ShadeToony != _ShadeToony_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ShadeToony, _ShadeToony));
			}

			if (_LightColorAttenuation != _LightColorAttenuation_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._LightColorAttenuation, _LightColorAttenuation));
			}

			if (_IndirectLightIntensity != _IndirectLightIntensity_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._IndirectLightIntensity, _IndirectLightIntensity));
			}

			if (_RimColor != _RimColor_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimColor, new JArray(_RimColor.r, _RimColor.g, _RimColor.b, _RimColor.a)));
			}

			if (_RimTexture != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimTexture, new JObject(new JProperty(TextureInfo.INDEX, _RimTexture.Index.Id))));
			}

			if (_RimLightingMix != _RimLightingMix_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimLightingMix, _RimLightingMix));
			}

			if (_RimFresnelPower != _RimFresnelPower_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimFresnelPower, _RimFresnelPower));
			}

			if (_RimLift != _RimLift_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimLift, _RimLift));
			}

			if (_SphereAdd != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._SphereAdd, new JObject(new JProperty(TextureInfo.INDEX, _SphereAdd.Index.Id))));
			}


			if (_EmissionColor != _EmissionColor_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._EmissionColor, new JArray(_EmissionColor.r, _EmissionColor.g, _EmissionColor.b, _EmissionColor.a)));
			}

			if (_EmissionMap != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._EmissionMap, new JObject(new JProperty(TextureInfo.INDEX, _EmissionMap.Index.Id))));
			}

			if (_OutlineWidthTexture != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineWidthTexture, new JObject(new JProperty(TextureInfo.INDEX, _OutlineWidthTexture.Index.Id))));
			}

			if (_OutlineWidth != _OutlineWidth_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineWidth, _OutlineWidth));
			}

			if (_OutlineScaledMaxDistance != _OutlineScaledMaxDistance_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineScaledMaxDistance, _OutlineScaledMaxDistance));
			}

			if (_OutlineColor != _OutlineColor_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineColor, new JArray(_OutlineColor.r, _OutlineColor.g, _OutlineColor.b, _OutlineColor.a)));
			}

			if (_OutlineLightingMix != _OutlineLightingMix_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineLightingMix, _OutlineLightingMix));
			}

			if (_UvAnimMaskTexture != null)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._UvAnimMaskTexture, new JObject(new JProperty(TextureInfo.INDEX, _UvAnimMaskTexture.Index.Id))));
			}

			if (_UvAnimScrollX != _UvAnimScrollX_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._UvAnimScrollX, _UvAnimScrollX));
			}

			if (_UvAnimScrollY != _UvAnimScrollY_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._UvAnimScrollY, _UvAnimScrollY));
			}

			if (_UvAnimRotation != _UvAnimRotation_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._UvAnimRotation, _UvAnimRotation));
			}

			if (_MToonVersion != _MToonVersion_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._MToonVersion, _MToonVersion));
			}

			if (_DebugMode != _DebugMode_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._DebugMode, _DebugMode));
			}

			if (_BlendMode != _BlendMode_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._BlendMode, _BlendMode));
			}

			if (_OutlineWidthMode != _OutlineWidthMode_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineWidthMode, _OutlineWidthMode));
			}

			if (_OutlineColorMode != _OutlineColorMode_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineColorMode, _OutlineColorMode));
			}

			if (_CullMode != _CullMode_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._CullMode, _CullMode));
			}

			if (_OutlineCullMode != _OutlineCullMode_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._OutlineCullMode, _OutlineCullMode));
			}

			if (_SrcBlend != _SrcBlend_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._SrcBlend, _SrcBlend));
			}

			if (_DstBlend != _DstBlend_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._DstBlend, _DstBlend));
			}

			if (_ZWrite != _ZWrite_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._ZWrite, _ZWrite));
			}

			if (_AlphaToMask != _AlphaToMask_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._AlphaToMask, _AlphaToMask));
			}

			if (_LinearLitColor != _LinearLitColor_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._LinearLitColor, new JArray(_LinearLitColor.r, _LinearLitColor.g, _LinearLitColor.b, _LinearLitColor.a)));
			}

			if (_SpecTexture != _SpecTexture_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._SpecTexture, new JObject(new JProperty(TextureInfo.INDEX, _SpecTexture.Index.Id))));
			}

			if (_PatternTexture != _PatternTexture_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._PatternTexture, new JObject(new JProperty(TextureInfo.INDEX, _PatternTexture.Index.Id))));
			}

			if (_SocksTexture != _SocksTexture_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._SocksTexture, new JObject(new JProperty(TextureInfo.INDEX, _SocksTexture.Index.Id))));
			}

			if (_SpecColor != _SpecColor_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._SpecColor, new JArray(_SpecColor.r, _SpecColor.g, _SpecColor.b, _SpecColor.a)));
			}

			if (_RimSoftness != _RimSoftness_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimSoftness, _RimSoftness));
			}

			if (_RimWeight != _RimWeight_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimWeight, _RimWeight));
			}

			if (_RimSphereWeight != _RimSphereWeight_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._RimSphereWeight, _RimSphereWeight));
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

			if (_IsGrayTex != _IsGrayTex_Default)
			{
				ext.Add(new JProperty(MToonClothingMaterialExtensionFactory._IsGrayTex, _IsGrayTex));
			}

			return new JProperty(MToonClothingMaterialExtensionFactory.Extension_Name, ext);
		}

		public override void Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			base.Deserialize(root, extensionToken);

			JToken token = extensionToken.Value[MToonClothingMaterialExtensionFactory._Mask1];
			_Mask1 = token != null ? token.DeserializeAsTexture(root) : _Mask1_Default;
		}
	}
}
