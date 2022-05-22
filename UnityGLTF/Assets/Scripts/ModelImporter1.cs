using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;
using UnityEditor;
using GLTF.Schema;
using System.Threading.Tasks;
using UnityGLTF.Cache;

public class ModelImporter1 : GLTFSceneImporter
{
	public ModelImporter1(string gltfFileName, ImportOptions options)
		: base(gltfFileName, options)
	{
	}
	protected override Task ConstructMaterialImageBuffers(GLTFMaterial def)
	{
		base.ConstructMaterialImageBuffers(def);

		var tasks = new List<Task>();

		const string Extension_Name = MToonMaterialExtensionFactory.Extension_Name;
		if (def.Extensions != null && def.Extensions.ContainsKey(Extension_Name))
		{
			var ext = (MToonMaterialExtension)def.Extensions[Extension_Name];
			if (ext._MainTex != null)
			{
				var textureId = ext._MainTex.Index;
				tasks.Add(ConstructImageBuffer(textureId.Value, textureId.Id));
			}

			if (ext._ShadeTexture != null)
			{
				var textureId = ext._ShadeTexture.Index;
				tasks.Add(ConstructImageBuffer(textureId.Value, textureId.Id));
			}
		}

		return Task.WhenAll(tasks);
	}
	protected override async Task<IUniformMap> ConstructMaterial(GLTFMaterial def, int materialIndex)
	{
		IUniformMap mapper = await base.ConstructMaterial(def, materialIndex);

		MToonMaterialExtension ext = def.Extensions[MToonMaterialExtensionFactory.Extension_Name] as MToonMaterialExtension;
		Shader shader = Shader.Find("VRM/MToon");
		mapper.Material = new Material(shader);

		if (ext._MainTex != null)
		{
			// TODO: 也搞一个 mapper 去设置 material 参数 ， 完成其他参数的设置，和 ModelExpoter 导出材质差不对
			TextureId textureId = ext._MainTex.Index;
			await ConstructTexture(textureId.Value, textureId.Id, !KeepCPUCopyOfTexture, false);
			Texture tex = _assetCache.TextureCache[textureId.Id].Texture;
			mapper.Material.SetTexture(MToonMaterialExtensionFactory._MainTex, tex);
		}

		if (ext._ShadeTexture != null)
		{
			// TODO: 也搞一个 mapper 去设置 material 参数
			TextureId textureId = ext._ShadeTexture.Index;
			await ConstructTexture(textureId.Value, textureId.Id, !KeepCPUCopyOfTexture, false);
			Texture tex = _assetCache.TextureCache[textureId.Id].Texture;
			mapper.Material.SetTexture(MToonMaterialExtensionFactory._ShadeTexture, tex);
		}

		mapper.Material.SetColor(MToonMaterialExtensionFactory._RimColor, ext._RimColor);

		var vertColorMapper = mapper.Clone();
		vertColorMapper.VertexColorsEnabled = true;

		MaterialCacheData materialWrapper = new MaterialCacheData
		{
			UnityMaterial = mapper.Material,
			UnityMaterialWithVertexColor = vertColorMapper.Material,
			GLTFMaterial = def
		};

		if (materialIndex >= 0)
		{
			_assetCache.MaterialCache[materialIndex] = materialWrapper;
		}
		else
		{
			_defaultLoadedMaterial = materialWrapper;
		}

		return mapper;
	}
}
