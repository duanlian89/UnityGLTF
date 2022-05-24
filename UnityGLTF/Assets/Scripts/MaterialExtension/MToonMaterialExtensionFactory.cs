using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using GLTF.Extensions;

public class MToonMaterialExtensionFactory : MaterialExtensionFactory
{
	public const string Extension_Name = "VRM/MToon";

	#region property name

	public const string _Cutoff = "_Cutoff";

	public const string _Color = "_Color";

	public const string _ColorOL = "_ColorOL";

	public const string _ShadeColor = "_ShadeColor";

	public const string _MainTex = "_MainTex";

	public const string _MainTex2 = "_MainTex2";

	public const string _ShadeTexture = "_ShadeTexture";

	public const string _BumpScale = "_BumpScale";

	public const string _BumpMap = "_BumpMap";

	public const string _ReceiveShadowRate = "_ReceiveShadowRate";

	public const string _ReceiveShadowTexture = "_ReceiveShadowTexture";

	public const string _ShadingGradeRate = "_ShadingGradeRate";

	public const string _ShadingGradeTexture = "_ShadingGradeTexture";

	public const string _ShadeShift = "_ShadeShift";

	public const string _ShadeToony = "_ShadeToony";

	public const string _LightColorAttenuation = "_LightColorAttenuation";

	public const string _IndirectLightIntensity = "_IndirectLightIntensity";

	public const string _RimColor = "_RimColor";

	public const string _RimTexture = "_RimTexture";

	public const string _RimLightingMix = "_RimLightingMix";

	public const string _RimFresnelPower = "_RimFresnelPower";

	public const string _RimLift = "_RimLift";

	public const string _SphereAdd = "_SphereAdd";

	public const string _EmissionColor = "_EmissionColor";

	public const string _EmissionMap = "_EmissionMap";

	public const string _OutlineWidthTexture = "_OutlineWidthTexture";

	public const string _OutlineWidth = "_OutlineWidth";

	public const string _OutlineScaledMaxDistance = "_OutlineScaledMaxDistance";

	public const string _OutlineColor = "_OutlineColor";

	public const string _OutlineLightingMix = "_OutlineLightingMix";

	public const string _UvAnimMaskTexture = "_UvAnimMaskTexture";

	public const string _UvAnimScrollX = "_UvAnimScrollX";

	public const string _UvAnimScrollY = "_UvAnimScrollX";

	public const string _UvAnimRotation = "_UvAnimScrollX";

	#endregion

	public MToonMaterialExtensionFactory()
	{
		ExtensionName = Extension_Name;

		FloatProperties = new string[] { _Cutoff, _UvAnimRotation, _BumpScale , _ReceiveShadowRate ,
		_ShadingGradeRate , _ShadeShift,    _ShadeShift,_ShadeToony,_LightColorAttenuation,
		_IndirectLightIntensity,_RimLightingMix,_RimFresnelPower    ,_RimLift,_OutlineWidth,_OutlineScaledMaxDistance,
		_OutlineLightingMix,_UvAnimScrollX,_UvAnimScrollY,_UvAnimRotation   };

		ColorProperties = new string[] { _Color, _ColorOL, _ShadeColor, _RimColor, _EmissionColor, _OutlineColor };

		TextureProperties = new string[] { _MainTex, _MainTex2, _ShadeTexture , _BumpMap ,
		_ReceiveShadowTexture, _ShadingGradeTexture ,_RimTexture,_SphereAdd,_EmissionMap,
		_OutlineWidthTexture,_UvAnimMaskTexture };
	}

	// 从extensionToken读出属性，初始化 MToonMaterialExtension
	public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
	{
		MToonMaterialExtension ext = new MToonMaterialExtension();
		ext.Deserialize(root, extensionToken);
		return ext;
	}

	public override IExtension GetExtension()
	{
		MToonMaterialExtension ext = new MToonMaterialExtension();
		return ext;
	}
}
