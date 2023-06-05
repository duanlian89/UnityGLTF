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
	public class ImportOptionsExtension : ImportOptions
	{
		/// <summary>
		/// 纹理缩放比例
		/// </summary>
		public float scaleFactor = 1.0f;

		/// <summary>
		/// 纹理最大尺寸
		/// </summary>
		public Vector2 maxSize = Vector2.one * 4096 * 2;
	}

	public class ModelImporter : GLTFSceneImporter
	{
		internal UnityGLTF.Cache.AssetCache AssetCache
		{
			get { return _assetCache; }
		}

		/// <summary>
		/// 纹理缩放比例
		/// </summary>
		float scaleFactor = 1.0f;

		/// <summary>
		/// 纹理最大尺寸
		/// </summary>
		Vector2 maxSize = Vector2.one * 512.0f;

		public ModelImporter(string gltfFileName, ImportOptions options)
			: base(gltfFileName, options)
		{
			ImportOptionsExtension optionsExtension = options as ImportOptionsExtension;
			scaleFactor = (float)(optionsExtension?.scaleFactor ?? scaleFactor);
			maxSize = (Vector2)(optionsExtension?.maxSize ?? maxSize);
		}

		void ParseGltfRoot()
		{
			_gltfStream.Stream = _options.DataLoader.LoadStreamAsync(_gltfFileName).GetAwaiter().GetResult();
			_gltfStream.StartPosition = 0;
			GLTFParser.ParseJson(_gltfStream.Stream, out _gltfRoot, _gltfStream.StartPosition);
		}

		string configJson = "";
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
			ParseGltfRoot();

			IExtension _extension;
			if (_gltfRoot != null && _gltfRoot.Extensions != null
				&& _gltfRoot.Extensions.Count > 0
				&& _gltfRoot.Extensions.TryGetValue(ConfigJsonExtensionFactory.Extension_Name, out _extension)
				&& _extension != null)
			{
				ConfigJsonExtension extension = _extension as ConfigJsonExtension;
				if (extension != null)
					configJson = extension.configJson;
			}
		}

		string clothesInfoJson = "";
		/// <summary>
		/// ClothesInfo 结构的反序列
		/// </summary>
		public string ClothesInfoJson
		{
			get
			{
				if (string.IsNullOrEmpty(clothesInfoJson))
				{
					GetClothesInfoJson();
				}

				return clothesInfoJson;
			}
		}

		private void GetClothesInfoJson()
		{
			ParseGltfRoot();

			IExtension _extension;
			if (_gltfRoot != null && _gltfRoot.Extensions != null
				&& _gltfRoot.Extensions.Count > 0
				&& _gltfRoot.Extensions.TryGetValue(ClothesInfoJsonExtensionFactory.Extension_Name, out _extension)
				&& _extension != null)
			{
				ClothesInfoJsonExtension extension = _extension as ClothesInfoJsonExtension;
				if (extension != null)
					clothesInfoJson = extension.clothesInfoJson;
			}
		}

		string sceneName = "";
		/// <summary>
		/// 获取默认[第一个]场景[模型根节点]名字
		/// </summary>
		public string DefaultSceneName
		{
			get
			{
				if (string.IsNullOrEmpty(sceneName))
				{
					sceneName = GetSceneName(0);
				}
				return sceneName;
			}
		}

		/// <summary>
		/// 获取位于索引位置的场景[模型根节点]名字
		/// </summary>
		public string SceneName(int index)
		{
			return GetSceneName(index);
		}

		string GetSceneName(int index)
		{
			if (!(_gltfRoot != null && _gltfRoot.Scenes != null))
				ParseGltfRoot();

			return _gltfRoot.Scenes[index]?.Name ?? "";
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
						{
							mapper.Material = await ConstructMaterial(factory, def.Extensions[ext.Key]);
							mapper.Material.name = def.Name;
						}
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

			for (int i = 0; i < factory.IntProperties.Length; i++)
			{
				string prop = factory.IntProperties[i];
				if (t.GetField(prop) != null)
				{
					material.SetInt(prop, (int)t.GetField(prop).GetValue(extension));
				}
				else
				{
					Debug.Log(string.Format("can not get Field by name:{0}", prop));
				}
			}

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
						var meshFilter = _assetCache.NodeCache[nodeIndex].GetComponent<MeshFilter>();
						if (!meshFilter)
							meshFilter = _assetCache.NodeCache[nodeIndex].AddComponent<MeshFilter>();
						meshFilter.sharedMesh = unityMesh;

						var meshCollider = _assetCache.NodeCache[nodeIndex].GetComponent<MeshCollider>();
						if (!meshCollider)
							meshCollider = _assetCache.NodeCache[nodeIndex].AddComponent<MeshCollider>();
						meshCollider.sharedMesh = unityMesh;
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

		protected override async Task ConstructUnityTexture(Stream stream, bool markGpuOnly, bool isLinear, GLTFImage image, int imageCacheIndex)
		{
			await base.ConstructUnityTexture(stream, markGpuOnly, isLinear, image, imageCacheIndex);

			Texture2D originalTexture2d = _assetCache.ImageCache[imageCacheIndex];
			originalTexture2d.name = _gltfRoot.Textures[imageCacheIndex].Name;

			if (scaleFactor != 1.0f)
			{
				var scaleTexture2D = TextureUtil.ResizeTexture(originalTexture2d, (int)(originalTexture2d.width * scaleFactor), (int)(originalTexture2d.height * scaleFactor));
				if (scaleTexture2D)
				{
					scaleTexture2D.name = originalTexture2d.name;
					_assetCache.ImageCache[imageCacheIndex] = scaleTexture2D;
#if UNITY_EDITOR
					UnityEngine.Object.DestroyImmediate(originalTexture2d, true);
#else
					UnityEngine.Object.Destroy(originalTexture2d);
#endif
				}
			}

			originalTexture2d = _assetCache.ImageCache[imageCacheIndex];
			if (originalTexture2d.width > maxSize.x || originalTexture2d.height > maxSize.y)
			{
				var scaleTexture2D = TextureUtil.ResizeTexture(originalTexture2d, (int)MathF.Min(originalTexture2d.width, maxSize.x), (int)MathF.Min(originalTexture2d.height, maxSize.y));
				if (scaleTexture2D)
				{
					scaleTexture2D.name = originalTexture2d.name;
					_assetCache.ImageCache[imageCacheIndex] = scaleTexture2D;
#if UNITY_EDITOR
					UnityEngine.Object.DestroyImmediate(originalTexture2d, true);
#else
					UnityEngine.Object.Destroy(originalTexture2d);
#endif
				}
			}
		}

		// 重写此方法，使TextureCache和ImageCache存储同样的内容以减少占用内存。
		protected override async Task ConstructTexture(GLTFTexture texture, int textureIndex,
			bool markGpuOnly, bool isLinear)
		{
			if (_assetCache.TextureCache[textureIndex].Texture == null)
			{
				int sourceId = GetTextureSourceId(texture);
				GLTFImage image = _gltfRoot.Images[sourceId];
				await ConstructImage(image, sourceId, markGpuOnly, isLinear);

				var source = _assetCache.ImageCache[sourceId];
				FilterMode desiredFilterMode;
				TextureWrapMode desiredWrapModeS, desiredWrapModeT;

				if (texture.Sampler != null)
				{
					var sampler = texture.Sampler.Value;
					switch (sampler.MinFilter)
					{
						case MinFilterMode.Nearest:
						case MinFilterMode.NearestMipmapNearest:
						case MinFilterMode.NearestMipmapLinear:
							desiredFilterMode = FilterMode.Point;
							break;
						case MinFilterMode.Linear:
						case MinFilterMode.LinearMipmapNearest:
							desiredFilterMode = FilterMode.Bilinear;
							break;
						case MinFilterMode.LinearMipmapLinear:
							desiredFilterMode = FilterMode.Trilinear;
							break;
						default:
							Debug.LogWarning("Unsupported Sampler.MinFilter: " + sampler.MinFilter);
							desiredFilterMode = FilterMode.Trilinear;
							break;
					}

					TextureWrapMode UnityWrapMode(GLTF.Schema.WrapMode gltfWrapMode)
					{
						switch (gltfWrapMode)
						{
							case GLTF.Schema.WrapMode.ClampToEdge:
								return TextureWrapMode.Clamp;
							case GLTF.Schema.WrapMode.Repeat:
								return TextureWrapMode.Repeat;
							case GLTF.Schema.WrapMode.MirroredRepeat:
								return TextureWrapMode.Mirror;
							default:
								Debug.LogWarning("Unsupported Sampler.Wrap: " + gltfWrapMode);
								return TextureWrapMode.Repeat;
						}
					}

					desiredWrapModeS = UnityWrapMode(sampler.WrapS);
					desiredWrapModeT = UnityWrapMode(sampler.WrapT);
				}
				else
				{
					desiredFilterMode = FilterMode.Trilinear;
					desiredWrapModeS = TextureWrapMode.Repeat;
					desiredWrapModeT = TextureWrapMode.Repeat;
				}

				var matchSamplerState = source.filterMode == desiredFilterMode && source.wrapModeU == desiredWrapModeS && source.wrapModeV == desiredWrapModeT;
				//if (matchSamplerState || markGpuOnly)
				//{
				Debug.Assert(_assetCache.TextureCache[textureIndex].Texture == null, "Texture should not be reset to prevent memory leaks");
				_assetCache.TextureCache[textureIndex].Texture = source;
				source.filterMode = desiredFilterMode;
				source.wrapModeU = desiredWrapModeS;
				source.wrapModeV = desiredWrapModeT;

				if (!matchSamplerState)
				{
					Debug.LogWarning($"Ignoring sampler; filter mode: source {source.filterMode}, desired {desiredFilterMode}; wrap mode: source {source.wrapModeU}x{source.wrapModeV}, desired {desiredWrapModeS}x{desiredWrapModeT}");
				}
				//}
				//else
				//{
				//	var unityTexture = Object.Instantiate(source);
				//	unityTexture.name = string.IsNullOrEmpty(image.Name) ? Path.GetFileNameWithoutExtension(image.Uri) : image.Name;
				//	unityTexture.filterMode = desiredFilterMode;
				//	unityTexture.wrapModeU = desiredWrapModeS;
				//	unityTexture.wrapModeV = desiredWrapModeT;

				//	Debug.Assert(_assetCache.TextureCache[textureIndex].Texture == null, "Texture should not be reset to prevent memory leaks");
				//	_assetCache.TextureCache[textureIndex].Texture = unityTexture;
				//}
			}
		}
		public void Dispose(bool destory = false)
		{
			if (destory && _assetCache != null)
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
