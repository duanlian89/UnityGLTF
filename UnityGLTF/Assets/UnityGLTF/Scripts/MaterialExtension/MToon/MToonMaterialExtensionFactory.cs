﻿using GLTF.Schema;
using Newtonsoft.Json.Linq;

namespace CKUnityGLTF
{
	public class MToonMaterialExtensionFactory : MaterialExtensionFactory
	{
		public const string Extension_Name = "VRM/MToon";

		#region property name

		public const string _Cutoff = "_Cutoff";

		public const string _Color = "_Color";

		public const string _LinearLitColor = "_LinearLitColor";
		public const string _SpecTexture = "_SpecTexture";

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

		public const string _MToonVersion = "_MToonVersion";

		public const string _DebugMode = "_DebugMode";

		public const string _BlendMode = "_BlendMode";

		public const string _OutlineWidthMode = "_OutlineWidthMode";

		public const string _OutlineColorMode = "_OutlineColorMode";

		public const string _CullMode = "_CullMode";

		public const string _OutlineCullMode = "_OutlineCullMode";

		public const string _SrcBlend = "_SrcBlend";

		public const string _DstBlend = "_DstBlend";

		public const string _ZWrite = "_ZWrite";

		public const string _AlphaToMask = "_AlphaToMask";

		public const string _SpecMulti = "_SpecMulti";

		public const string _SpecRange = "_SpecRange";

		public const string _SpecSoftness = "_SpecSoftness";

		#endregion

		public MToonMaterialExtensionFactory()
		{
			ExtensionName = Extension_Name;

			FloatProperties = new string[] { _Cutoff, _UvAnimRotation, _BumpScale , _ReceiveShadowRate ,
		_ShadingGradeRate , _ShadeShift,    _ShadeShift,_ShadeToony,_LightColorAttenuation,
		_IndirectLightIntensity,_RimLightingMix,_RimFresnelPower    ,_RimLift,_OutlineWidth,_OutlineScaledMaxDistance,
		_OutlineLightingMix,_UvAnimScrollX,_UvAnimScrollY,_UvAnimRotation,_MToonVersion ,
		_DebugMode ,_BlendMode ,_OutlineWidthMode ,_OutlineColorMode ,_CullMode ,
		_OutlineCullMode,_SrcBlend ,_DstBlend ,_ZWrite,_AlphaToMask,_SpecMulti,_SpecRange ,_SpecSoftness };

			ColorProperties = new string[] { _Color, _ColorOL, _ShadeColor, _RimColor, _EmissionColor, _OutlineColor, _LinearLitColor };

			TextureProperties = new string[] { _MainTex, _MainTex2, _ShadeTexture , _BumpMap ,
		_ReceiveShadowTexture, _ShadingGradeTexture ,_RimTexture,_SphereAdd,_EmissionMap,
		_OutlineWidthTexture,_UvAnimMaskTexture,_SpecTexture};
		}

		// 从extensionToken读出属性，初始化 MToonMaterialExtension
		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			MToonMaterialExtension ext = new MToonMaterialExtension();
			ext.Deserialize(root, extensionToken);
			return ext;
		}

		public override IExtension ConstructExtension()
		{
			MToonMaterialExtension ext = new MToonMaterialExtension();
			return ext;
		}
	}
}
