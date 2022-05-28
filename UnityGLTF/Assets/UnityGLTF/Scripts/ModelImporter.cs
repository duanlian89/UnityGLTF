using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using UnityGLTF.Cache;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;
using GLTF.Extensions;
using System;
using UnityGLTF;

namespace CKUnityGLTF
{
	public class ModelImporter : GLTFSceneImporter
	{
		public UnityGLTF.Cache.AssetCache AssetCache
		{
			get { return _assetCache; }
		}

		public ModelImporter(string gltfFileName, ImportOptions options)
			: base(gltfFileName, options)
		{
			GLTFMaterial.RegisterExtension(new MToonMaterialExtensionFactory());
			GLTFMaterial.RegisterExtension(new ConfigJsonExtensionFactory());
		}

		protected override async Task ConstructScene(GLTFScene scene, bool showSceneObj, CancellationToken cancellationToken)
		{
			GameObject sceneObj = null;

			try
			{
				Transform[] nodeTransforms = new Transform[scene.Nodes.Count];
				for (int i = 0; i < scene.Nodes.Count; ++i)
				{
					NodeId node = scene.Nodes[i];
					sceneObj = await GetNode(node.Id, cancellationToken);
					//nodeObj.transform.SetParent(sceneObj.transform, false);
					nodeTransforms[i] = sceneObj.transform;
				}

				if (_gltfRoot.Animations != null && _gltfRoot.Animations.Count > 0)
				{
					// create the AnimationClip that will contain animation data
					Animation animation = sceneObj.AddComponent<Animation>();
					for (int i = 0; i < _gltfRoot.Animations.Count; ++i)
					{
						AnimationClip clip = await ConstructClip(sceneObj.transform, i, cancellationToken);

						clip.wrapMode = UnityEngine.WrapMode.Loop;

						animation.AddClip(clip, clip.name);
						if (i == 0)
						{
							animation.clip = clip;
						}
					}
				}

				CreatedObject = sceneObj;
				ConfigJson = (_gltfRoot.Extensions[ConfigJsonExtensionFactory.Extension_Name] as ConfigJsonExtension).configJson;
			}
			catch (Exception ex)
			{
				// If some failure occured during loading, clean up the scene
				GameObject.DestroyImmediate(sceneObj);
				CreatedObject = null;

				if (ex is OutOfMemoryException)
				{
					Resources.UnloadUnusedAssets();
				}

				throw;
			}
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

			if (def.Extensions != null)
			{
				foreach (var ext in def.Extensions)
				{
					MaterialExtensionFactory factory = GLTFMaterial.TryGetExtension(ext.Key) as MaterialExtensionFactory;
					mapper.Material = await ConstructMToonMaterial(factory, def.Extensions[ext.Key]);
				}
			}

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

		private async Task<Material> ConstructMToonMaterial(MaterialExtensionFactory factory, IExtension extension)
		{
			Shader shader = Shader.Find(factory.ExtensionName);
			var material = new Material(shader);

			System.Type t = extension.GetType();

			for (int i = 0; i < factory.FloatProperties.Length; i++)
			{
				string prop = factory.FloatProperties[i];
				material.SetFloat(prop, (float)t.GetField(prop).GetValue(extension));
			}

			for (int i = 0; i < factory.ColorProperties.Length; i++)
			{
				string prop = factory.ColorProperties[i];
				Color c = (Color)t.GetField(prop).GetValue(extension);
				material.SetColor(prop, c);
			}

			for (int i = 0; i < factory.TextureProperties.Length; i++)
			{
				string prop = factory.TextureProperties[i];
				System.Object obj = t.GetField(prop).GetValue(extension);
				if (obj != null)
				{
					TextureId textureId = (obj as TextureInfo).Index;
					await ConstructTexture(textureId.Value, textureId.Id, !KeepCPUCopyOfTexture, false);
					Texture tex = _assetCache.TextureCache[textureId.Id].Texture;
					material.SetTexture(prop, tex);
				}
			}

			return material;
		}

		protected override async Task ConstructNode(Node node, int nodeIndex, CancellationToken cancellationToken)
		{
			await base.ConstructNode(node, nodeIndex, cancellationToken);

			if (node.Extensions != null)
			{
				foreach (var ext in node.Extensions)
				{
					System.Type t = System.Type.GetType(ext.Key);
					Component component = _assetCache.NodeCache[nodeIndex].AddComponent(t);
					(ext.Value as IComponentExtension).SetComponentParam(component);
				}
			}
		}
	}
}
