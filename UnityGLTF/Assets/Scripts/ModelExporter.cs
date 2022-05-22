using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;
using UnityEditor;
using GLTF.Schema;

public class ModelExporter : GLTFSceneExporter
{
	public string name = "sample";
	static string RetrieveTexturePath(UnityEngine.Texture texture)
	{
		//return texture.name;
		return AssetDatabase.GetAssetPath(texture);
	}

	//var exportOptions = new ExportOptions { TexturePathRetriever = RetrieveTexturePath };
	public ModelExporter(Transform parent) :
		base(new[] { parent }, new ExportOptions() { TexturePathRetriever = RetrieveTexturePath, ExportInactivePrimitives = true })
	{
		GLTFMaterial.TryRegisterExtension(new MToonMaterialExtensionFactory());
	}

	public void Export()
	{
		var path = EditorUtility.OpenFolderPanel("glTF Export Path", "", ""); // TODO: 替换接口
		if (!string.IsNullOrEmpty(path))
		{
			SaveGLB(path, name);
		}
	}

	public override MaterialId ExportMaterial(Material materialObj)
	{
		MaterialId id = base.ExportMaterial(materialObj);

		// TODO: 针对不同是材质，搞个工场模式来完成任务，这样更灵活一些
		GLTFMaterial gltfMaterial = GetRoot().Materials[id.Id];

		if (gltfMaterial.Extensions == null) gltfMaterial.Extensions = new Dictionary<string, IExtension>();
		gltfMaterial.Extensions[MToonMaterialExtensionFactory.Extension_Name] = new MToonMaterialExtension();

		// TODO:  从 materialObj 中读取属性，Texture, 初始化 Extension
		// Iextension.Init(UnityEngine.Material)
		MToonMaterialExtension ext = gltfMaterial.Extensions[MToonMaterialExtensionFactory.Extension_Name] as MToonMaterialExtension;

		ext._Cutoff = materialObj.GetFloat(MToonMaterialExtensionFactory._Cutoff);
		ext._Color = materialObj.GetColor(MToonMaterialExtensionFactory._Color);
		ext._ColorOL = materialObj.GetColor(MToonMaterialExtensionFactory._ColorOL);
		ext._ShadeColor = materialObj.GetColor(MToonMaterialExtensionFactory._ShadeColor);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._MainTex))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._MainTex);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._MainTex = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._MainTex, materialObj, MToonMaterialExtensionFactory._MainTex);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		//public Texture2D _MainTex2;
		if (materialObj.HasProperty(MToonMaterialExtensionFactory._MainTex2))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._MainTex2);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._MainTex2 = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._MainTex2, materialObj, MToonMaterialExtensionFactory._MainTex2);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		//public Texture2D _ShadeTexture;
		if (materialObj.HasProperty(MToonMaterialExtensionFactory._ShadeTexture))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._ShadeTexture);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._ShadeTexture = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._ShadeTexture, materialObj, MToonMaterialExtensionFactory._ShadeTexture);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._BumpScale = materialObj.GetFloat(MToonMaterialExtensionFactory._BumpScale);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._BumpMap))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._BumpMap);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._BumpMap = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._BumpMap, materialObj, MToonMaterialExtensionFactory._BumpMap);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._ReceiveShadowRate = materialObj.GetFloat(MToonMaterialExtensionFactory._ReceiveShadowRate);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._ReceiveShadowTexture))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._ReceiveShadowTexture);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._ReceiveShadowTexture = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._ReceiveShadowTexture, materialObj, MToonMaterialExtensionFactory._ReceiveShadowTexture);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._ShadingGradeRate = materialObj.GetFloat(MToonMaterialExtensionFactory._ShadingGradeRate);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._ShadingGradeTexture))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._ShadingGradeTexture);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._ShadingGradeTexture = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._ShadingGradeTexture, materialObj, MToonMaterialExtensionFactory._ShadingGradeTexture);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._ShadeShift = materialObj.GetFloat(MToonMaterialExtensionFactory._ShadeShift);
		ext._ShadeToony = materialObj.GetFloat(MToonMaterialExtensionFactory._ShadeToony);
		ext._LightColorAttenuation = materialObj.GetFloat(MToonMaterialExtensionFactory._LightColorAttenuation);
		ext._IndirectLightIntensity = materialObj.GetFloat(MToonMaterialExtensionFactory._IndirectLightIntensity);
		ext._RimColor = materialObj.GetColor(MToonMaterialExtensionFactory._RimColor);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._RimTexture))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._RimTexture);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._RimTexture = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._RimTexture, materialObj, MToonMaterialExtensionFactory._RimTexture);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._RimLightingMix = materialObj.GetFloat(MToonMaterialExtensionFactory._RimLightingMix);
		ext._RimFresnelPower = materialObj.GetFloat(MToonMaterialExtensionFactory._RimFresnelPower);
		ext._RimLift = materialObj.GetFloat(MToonMaterialExtensionFactory._RimLift);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._SphereAdd))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._SphereAdd);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._SphereAdd = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._SphereAdd, materialObj, MToonMaterialExtensionFactory._SphereAdd);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._EmissionColor = materialObj.GetColor(MToonMaterialExtensionFactory._EmissionColor);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._EmissionMap))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._EmissionMap);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._EmissionMap = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._EmissionMap, materialObj, MToonMaterialExtensionFactory._EmissionMap);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._OutlineWidthTexture))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._OutlineWidthTexture);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._OutlineWidthTexture = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._OutlineWidthTexture, materialObj, MToonMaterialExtensionFactory._OutlineWidthTexture);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._OutlineWidth = materialObj.GetFloat(MToonMaterialExtensionFactory._OutlineWidth);
		ext._OutlineScaledMaxDistance = materialObj.GetFloat(MToonMaterialExtensionFactory._OutlineScaledMaxDistance);
		ext._OutlineColor = materialObj.GetColor(MToonMaterialExtensionFactory._OutlineColor);
		ext._OutlineLightingMix = materialObj.GetFloat(MToonMaterialExtensionFactory._OutlineLightingMix);

		if (materialObj.HasProperty(MToonMaterialExtensionFactory._UvAnimMaskTexture))
		{
			var Tex = materialObj.GetTexture(MToonMaterialExtensionFactory._UvAnimMaskTexture);
			if (Tex != null)
			{
				if (Tex is Texture2D)
				{
					ext._UvAnimMaskTexture = ExportTextureInfo(Tex, TextureMapType.Main);
					ExportTextureTransform(ext._UvAnimMaskTexture, materialObj, MToonMaterialExtensionFactory._UvAnimMaskTexture);
				}
				else
				{
					Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
				}
			}
		}

		ext._UvAnimScrollX = materialObj.GetFloat(MToonMaterialExtensionFactory._UvAnimScrollX);
		ext._UvAnimScrollY = materialObj.GetFloat(MToonMaterialExtensionFactory._UvAnimScrollY);
		ext._UvAnimRotation = materialObj.GetFloat(MToonMaterialExtensionFactory._UvAnimRotation);

		return id;
	}
}
