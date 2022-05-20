using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

public class MToonMaterialExtension : IExtension
{
	//TODO: 进一步整理成 floatProperty,colorProperty ...
	public float _Cutoff = 0.0f;
	public static readonly float _Cutoff_Default = 0.5f;

	public Color _Color = Color.white;
	public static readonly Color _Color_Default = Color.white;

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

	public MToonMaterialExtension()
	{

	}

	public IExtension Clone(GLTFRoot root)
	{
		return new MToonMaterialExtension();
	}

	public JProperty Serialize()
	{
		JObject ext = new JObject();

		// TODO: ext 添加非默认值的属性
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

		return new JProperty(MToonMaterialExtensionFactory.Extension_Name, ext);
	}
}
