using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using UnityGLTF.Cache;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using UnityGLTF;
using GLTF;
using GLTF.Utilities;
using System.IO;

namespace CKUnityGLTF
{
	public class ModelImporter : GLTFSceneImporter
	{
		internal UnityGLTF.Cache.AssetCache AssetCache
		{
			get { return _assetCache; }
		}

		public ModelImporter(string gltfFileName, ImportOptions options)
			: base(gltfFileName, options)
		{

		}

		string configJson;
		/// <summary>
		/// info.json
		/// </summary>
		public string ConfigJson
		{
			get
			{
				if (string.IsNullOrEmpty(configJson))
				{
					GetConfigJson();
				}

				return configJson;
			}

			set
			{
				configJson = value;
			}
		}

		private void GetConfigJson()
		{
			_gltfStream.Stream = _options.DataLoader.LoadStreamAsync(_gltfFileName).GetAwaiter().GetResult();
			_gltfStream.StartPosition = 0;
			GLTFParser.ParseJson(_gltfStream.Stream, out _gltfRoot, _gltfStream.StartPosition);

			if (_gltfRoot != null && _gltfRoot.Extensions != null && _gltfRoot.Extensions.Count > 0 && _gltfRoot.Extensions[ConfigJsonExtensionFactory.Extension_Name] != null)
			{
				ConfigJsonExtension extension = _gltfRoot.Extensions[ConfigJsonExtensionFactory.Extension_Name] as ConfigJsonExtension;
				if (extension != null)
					configJson = extension.configJson;
			}
		}

		// 创建GameObject
		public async Task Load()
		{
			await base.LoadSceneAsync(-1, true, (go, e) =>
			{
				if (e != null)
					Debug.LogException(e.SourceException);

				_assetCache.NodeCache = new GameObject[_gltfRoot.Nodes?.Count ?? 0];
			});
		}

		public async Task Load(Action<GameObject, string> onLoadComplete)
		{
			await base.LoadSceneAsync(-1, true, (go, e) =>
			{
				_assetCache.NodeCache = new GameObject[_gltfRoot.Nodes?.Count ?? 0];

				if (e != null)
					Debug.LogException(e.SourceException);
				else
				{
					onLoadComplete(this.CreatedObject, this.ConfigJson);
				}
			});
		}

		//public async Task<Texture2D> GetTexture2DByIndexAsync(int index)
		//{
		//	await ConstructTexture(_gltfRoot.Textures[index], index, false, true);
		//	Texture2D texture2D = _assetCache.TextureCache[index].Texture as Texture2D;
		//	return texture2D;
		//}

		/// <summary>
		/// 根据名字获取对应的贴图
		/// </summary>
		public Texture2D GetTexture2DByName(string name)
		{
			try
			{
				_gltfStream.Stream = _options.DataLoader.LoadStreamAsync(_gltfFileName).GetAwaiter().GetResult();
				_gltfStream.StartPosition = 0;
				GLTFParser.ParseJson(_gltfStream.Stream, out _gltfRoot, _gltfStream.StartPosition);

				for (int i = 0; i < _gltfRoot.Textures.Count; i++)
				{
					if (_gltfRoot.Textures[i].Name == name)
					{
						return GetTexture2DByIndex(i);
					}
				}

				return null;
			}
			catch (Exception e)
			{
				Debug.LogWarning(string.Format("Found Exception Whene GetTexture2DByName,{0}{1}{2}", e.Message, Environment.NewLine, e.StackTrace));
				return null;
			}
		}

		/// <summary>
		/// 根据索引获取对应的贴图
		/// </summary>
		public Texture2D GetTexture2DByIndex(int index)
		{
			try
			{
				_gltfStream.Stream = _options.DataLoader.LoadStreamAsync(_gltfFileName).GetAwaiter().GetResult();
				_gltfStream.StartPosition = 0;
				GLTFParser.ParseJson(_gltfStream.Stream, out _gltfRoot, _gltfStream.StartPosition);

				if (_gltfRoot != null)
				{
					_gltfStream.StartPosition = 0;

					var tor = _gltfRoot.Meshes[0].Primitives[0].Attributes.GetEnumerator();
					tor.MoveNext();
					int bufferIndex = tor.Current.Value.Value.BufferView.Value.Buffer.Id;

					GLTFParser.SeekToBinaryChunk(_gltfStream.Stream, bufferIndex, _gltfStream.StartPosition);  // sets stream to correct start position
					BufferCacheData bufferContents = new BufferCacheData
					{
						Stream = _gltfStream.Stream,
						ChunkOffset = (uint)_gltfStream.Stream.Position
					};

					var image = _gltfRoot.Images[_gltfRoot.Textures[index].Source.Id];
					var bufferView = image.BufferView.Value;
					var data = new byte[bufferView.ByteLength];
					bufferContents.Stream.Position = bufferView.ByteOffset + bufferContents.ChunkOffset;
					Stream stream = new SubStream(bufferContents.Stream, 0, data.Length);
					byte[] buffer = new byte[stream.Length];
					stream.Read(buffer, 0, (int)stream.Length);
					stream.Dispose();

					Texture2D texture = new Texture2D(0, 0, TextureFormat.RGBA32, false, false);
					texture.name = string.IsNullOrEmpty(image.Name) ? _gltfRoot.Textures[index].Name : image.Name;
					texture.LoadImage(buffer, false);
					texture.Apply();

					buffer = null;

					return texture;
				}

				return null;
			}
			catch (Exception e)
			{
				Debug.LogWarning(string.Format("Found Exception Whene GetTexture2DByIndex,{0}{1}{2}", e.Message, Environment.NewLine, e.StackTrace));
				return null;
			}
		}

		/// <summary>
		/// 根据GameObjext获取其在Cache中的索引
		/// </summary>
		public int GetGameObjectIndex(GameObject gameObject)
		{
			return _assetCache.NodeCache.ToList().IndexOf(gameObject);
		}

		/// <summary>
		/// 根据索引获取Cache中对应位置的GameObjext
		/// </summary>
		public GameObject GetGameObject(int index)
		{
			if (index < _assetCache.NodeCache.Length)
				return _assetCache.NodeCache[index];
			else
			{
				Debug.LogException(new IndexOutOfRangeException(string.Format("index:{0},_assetCache.NodeCache.Length:{1}", index, _assetCache.NodeCache.Length)));
				return null;
			}
		}

		protected override async Task ConstructScene(GLTFScene scene, bool showSceneObj, CancellationToken cancellationToken)
		{
			GameObject sceneObj = null;

			try
			{
				//Transform[] nodeTransforms = new Transform[scene.Nodes.Count];
				for (int i = 0; i < scene.Nodes.Count; ++i)
				{
					NodeId node = scene.Nodes[i];
					sceneObj = await GetNode(node.Id, cancellationToken);
					//nodeObj.transform.SetParent(sceneObj.transform, false);
					//nodeTransforms[i] = sceneObj.transform;
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

				//node's component
				for (int i = 0; i < _gltfRoot.Nodes.Count; ++i)
				{
					Node node = _gltfRoot.Nodes[i];
					sceneObj = await GetNode(i, cancellationToken);
					//nodeObj.transform.SetParent(sceneObj.transform, false);
					//nodeTransforms[i] = sceneObj.transform;

					if (node.Extensions != null)
					{
						var extensions = node.Extensions.ToList().
							FindAll(e => { return e.Value is IComponentExtension; });

						extensions.ForEach(ext =>
						{
							System.Type t = System.Type.GetType(ext.Key);
							if (t == null)
							{
								string key = ext.Key;
								string[] sps = key.Split('.');
								t = System.Reflection.Assembly.Load(sps[0]).GetType(key);
							}

							if (t != null)
							{
								Component component = sceneObj.AddComponent(t);// _assetCache.NodeCache[i].AddComponent(t);
								(ext.Value as IComponentExtension).SetComponentParam(component);
							}
							else
							{
								Debug.LogException(new Exception(string.Format("can't get type by extension's key:{0}", ext.Key)));
							}
						});
					}
				}
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

			if (def.Extensions != null && def.Extensions.Count > 0)
			{
				var tor = def.Extensions.GetEnumerator();
				while (tor.MoveNext())
				{
					var current = tor.Current;
					string Extension_Name = current.Key;
					IExtension extension = current.Value;

					MaterialExtensionFactory factory = GLTFProperty.TryGetExtension(current.Key) as MaterialExtensionFactory;

					if (factory != null)
					{
						var t = extension.GetType();
						for (int j = 0; j < factory.TextureProperties.Length; j++)
						{
							System.Reflection.FieldInfo fileInfo = t.GetField(factory.TextureProperties[j]);
							if (fileInfo != null && fileInfo.GetValue(extension) != null)
							{
								TextureId textureId = (fileInfo.GetValue(extension) as TextureInfo).Index;
								tasks.Add(ConstructImageBuffer(textureId.Value, textureId.Id));
							}
						}
					}
				}
			}

			return Task.WhenAll(tasks);
		}

		protected override async Task<IUniformMap> ConstructMaterial(GLTFMaterial def, int materialIndex)
		{
			if (materialIndex >= 0 && materialIndex < _assetCache.MaterialCache.Length && _assetCache.MaterialCache[materialIndex] == null)
			{
				IUniformMap mapper = await base.ConstructMaterial(def, materialIndex);
				//TODO:记录原有的 mapper.Material，在新的创建完成后，销毁原材质
				if (def.Extensions != null)
				{
					foreach (var ext in def.Extensions)
					{
						MaterialExtensionFactory factory = GLTFMaterial.TryGetExtension(ext.Key) as MaterialExtensionFactory;

						if (factory != null && Shader.Find(factory.ExtensionName) != null)
							mapper.Material = await ConstructMaterial(factory, def.Extensions[ext.Key]);
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

			return null;
		}

		private async Task<Material> ConstructMaterial(MaterialExtensionFactory factory, IExtension extension)
		{
			Shader shader = Shader.Find(factory.ExtensionName);
			var material = new Material(shader);

			System.Type t = extension.GetType();

			for (int i = 0; i < factory.FloatProperties.Length; i++)
			{
				string prop = factory.FloatProperties[i];
				if (t.GetField(prop) != null)
				{
					material.SetFloat(prop, (float)t.GetField(prop).GetValue(extension));
				}
				else
				{
					Debug.Log(string.Format("can not get Field by name:{0}", prop));
				}
			}

			for (int i = 0; i < factory.ColorProperties.Length; i++)
			{
				string prop = factory.ColorProperties[i];
				if (t.GetField(prop) != null)
				{
					Color c = (Color)t.GetField(prop).GetValue(extension);
					material.SetColor(prop, c);
				}
				else
				{
					Debug.Log(string.Format("can not get Field by name:{0}", prop));
				}
			}

			for (int i = 0; i < factory.TextureProperties.Length; i++)
			{
				string prop = factory.TextureProperties[i];
				if (t.GetField(prop) != null && t.GetField(prop).GetValue(extension) != null)
				{
					System.Object obj = t.GetField(prop).GetValue(extension);
					TextureId textureId = (obj as TextureInfo).Index;
					await ConstructTexture(textureId.Value, textureId.Id, !KeepCPUCopyOfTexture, false);
					Texture tex = _assetCache.TextureCache[textureId.Id].Texture;
					material.SetTexture(prop, tex);
				}
				else
				{
					Debug.Log(string.Format("can not get Field by name:{0}", prop));
				}
			}

			if (factory.ExtensionName == StandardMaterialExtensionFactory.Extension_Name)
				material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;

			if (t.GetField(MaterialExtensionFactory.shaderKeywords) != null)
			{
				System.Object shaderKeywords = t.GetField(MaterialExtensionFactory.shaderKeywords).GetValue(extension);
				material.shaderKeywords = shaderKeywords as String[];
			}
			else
			{
				Debug.Log(string.Format("can not get Field by name:{0}", MaterialExtensionFactory.shaderKeywords));
			}

			if (t.GetField(MaterialExtensionFactory.renderQueue) != null)
			{
				System.Object renderQueue = t.GetField(MaterialExtensionFactory.renderQueue).GetValue(extension);
				material.renderQueue = (int)renderQueue;
			}
			else
			{
				Debug.Log(string.Format("can not get Field by name:{0}", MaterialExtensionFactory.renderQueue));
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
					MeshFilterAndMeshColliderExtension extension = ext.Value as MeshFilterAndMeshColliderExtension;
					if (extension != null)
					{
						var mesh = extension.Mesh.Value;
						await ConstructMesh(mesh, extension.Mesh.Id, cancellationToken);
						var unityMesh = _assetCache.MeshCache[extension.Mesh.Id].LoadedMesh;

						// TODO: weight ??
						//var morphTargets = mesh.Primitives[0].Targets;
						//var weights = node.Weights ?? mesh.Weights ??
						//	(morphTargets != null ? new List<double>(morphTargets.Select(mt => 0.0)) : null);
						//if (weights != null)
						//{
						_assetCache.NodeCache[nodeIndex].AddComponent<MeshFilter>().sharedMesh = unityMesh;
						_assetCache.NodeCache[nodeIndex].AddComponent<MeshCollider>().sharedMesh = unityMesh;
						//}
					}
				}
			}
		}

		protected override async Task ConstructBufferData(Node node, CancellationToken cancellationToken)
		{
			await base.ConstructBufferData(node, cancellationToken);

			if (node.Extensions != null)
			{
				var extensions = node.Extensions.Values.ToList().
					FindAll(e => { return e is MeshFilterAndMeshColliderExtension; });

				extensions.ForEach(async (e) =>
				{
					MeshId mesh = (e as MeshFilterAndMeshColliderExtension).Mesh;
					if (mesh != null)
					{
						if (mesh.Value.Primitives != null)
						{
							await ConstructMeshAttributes(mesh.Value, mesh);
						}
					}
				});
			}
		}

		public void Dispose(bool destory = false)
		{
			if (destory)
			{
				for (int i = 0; i < _assetCache.MeshCache.Length; i++)
				{
					_assetCache.MeshCache[i]?.Dispose();
					_assetCache.MeshCache[i] = null;
				}

				// Destroy the cached textures
				for (int i = 0; i < _assetCache.TextureCache.Length; i++)
				{
					_assetCache.TextureCache[i]?.Dispose();
					_assetCache.TextureCache[i] = null;
				}

				// Destroy the cached materials
				for (int i = 0; i < _assetCache.MaterialCache.Length; i++)
				{
					_assetCache.MaterialCache[i]?.Dispose();
					_assetCache.MaterialCache[i] = null;
				}

				// Destroy the cached images
				for (int i = 0; i < _assetCache.ImageCache.Length; i++)
				{
					if (_assetCache.ImageCache[i] != null)
					{
						UnityEngine.Object.Destroy(_assetCache.ImageCache[i]);
						_assetCache.ImageCache[i] = null;
					}
				}
			}

			base.Dispose();
		}
	}
}
