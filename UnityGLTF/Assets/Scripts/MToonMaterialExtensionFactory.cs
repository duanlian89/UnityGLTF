using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using GLTF.Extensions;

public class MToonMaterialExtensionFactory : ExtensionFactory
{
	#region property name
	public const string Extension_Name = "Mtoon";

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
	}

	public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
	{
		// 从extensionToken读出属性，初始化 MToonMaterialExtension
		MToonMaterialExtension ext = new MToonMaterialExtension();

		JToken v = extensionToken.Value["FOR TEST"];
		int a = v.DeserializeAsInt();
		ext._Cutoff = (float)extensionToken.Value[_Cutoff].DeserializeAsDouble();

		var c = extensionToken.Value[_Color].DeserializeAsColor();
		ext._Color = new Color(c.R,c.G,c.B,c.A);

		c = extensionToken.Value[_ColorOL].DeserializeAsColor();
		ext._ColorOL = new Color(c.R, c.G, c.B, c.A);

		c = extensionToken.Value[_ShadeColor].DeserializeAsColor();
		ext._ShadeColor = new Color(c.R, c.G, c.B, c.A);

		ext._MainTex = extensionToken.Value[MToonMaterialExtensionFactory._MainTex].DeserializeAsTexture(root);
		ext._MainTex2 = extensionToken.Value[MToonMaterialExtensionFactory._MainTex2].DeserializeAsTexture(root);
		ext._ShadeTexture = extensionToken.Value[MToonMaterialExtensionFactory._ShadeTexture].DeserializeAsTexture(root);
		ext._BumpScale = (float)extensionToken.Value[_BumpScale].DeserializeAsDouble();
		ext._BumpMap = extensionToken.Value[MToonMaterialExtensionFactory._BumpMap].DeserializeAsTexture(root);
		ext._ReceiveShadowRate = (float)extensionToken.Value[_ReceiveShadowRate].DeserializeAsDouble();
		ext._ReceiveShadowTexture = extensionToken.Value[MToonMaterialExtensionFactory._ReceiveShadowTexture].DeserializeAsTexture(root);
		ext._ShadingGradeRate = (float)extensionToken.Value[_ShadingGradeRate].DeserializeAsDouble();
		ext._ShadingGradeTexture = extensionToken.Value[MToonMaterialExtensionFactory._ShadingGradeTexture].DeserializeAsTexture(root);
		ext._ShadeShift = (float)extensionToken.Value[_ShadeShift].DeserializeAsDouble();
		ext._ShadeToony = (float)extensionToken.Value[_ShadeToony].DeserializeAsDouble();
		ext._LightColorAttenuation = (float)extensionToken.Value[_LightColorAttenuation].DeserializeAsDouble();
		ext._IndirectLightIntensity = (float)extensionToken.Value[_IndirectLightIntensity].DeserializeAsDouble();

		c = extensionToken.Value[_RimColor].DeserializeAsColor();
		ext._RimColor = new Color(c.R, c.G, c.B, c.A);

		ext._RimTexture = extensionToken.Value[MToonMaterialExtensionFactory._RimTexture].DeserializeAsTexture(root);
		ext._RimLightingMix = (float)extensionToken.Value[_RimLightingMix].DeserializeAsDouble();
		ext._RimFresnelPower = (float)extensionToken.Value[_RimFresnelPower].DeserializeAsDouble();
		ext._RimLift = (float)extensionToken.Value[_RimLift].DeserializeAsDouble();
		ext._SphereAdd = extensionToken.Value[MToonMaterialExtensionFactory._SphereAdd].DeserializeAsTexture(root);

		c = extensionToken.Value[_EmissionColor].DeserializeAsColor();
		ext._EmissionColor = new Color(c.R, c.G, c.B, c.A);

		ext._EmissionMap = extensionToken.Value[MToonMaterialExtensionFactory._EmissionMap].DeserializeAsTexture(root);
		ext._OutlineWidthTexture = extensionToken.Value[MToonMaterialExtensionFactory._OutlineWidthTexture].DeserializeAsTexture(root);
		ext._OutlineWidth = (float)extensionToken.Value[_OutlineWidth].DeserializeAsDouble();
		ext._OutlineScaledMaxDistance = (float)extensionToken.Value[_OutlineScaledMaxDistance].DeserializeAsDouble();

		c = extensionToken.Value[_OutlineColor].DeserializeAsColor();
		ext._OutlineColor = new Color(c.R, c.G, c.B, c.A);

		ext._OutlineLightingMix = (float)extensionToken.Value[_OutlineLightingMix].DeserializeAsDouble();
		ext._UvAnimMaskTexture = extensionToken.Value[MToonMaterialExtensionFactory._UvAnimMaskTexture].DeserializeAsTexture(root);
		ext._UvAnimScrollX = (float)extensionToken.Value[_UvAnimScrollX].DeserializeAsDouble();
		ext._UvAnimScrollY = (float)extensionToken.Value[_UvAnimScrollY].DeserializeAsDouble();
		ext._UvAnimRotation = (float)extensionToken.Value[_UvAnimRotation].DeserializeAsDouble();
		return ext;
	}
}
