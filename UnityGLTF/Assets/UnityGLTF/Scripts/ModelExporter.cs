using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GLTF.Schema;
using System.IO;
using System.Linq;
using UnityGLTF;
using System.Reflection;
using UnityGLTF.Extensions;

namespace CKUnityGLTF
{
	public class ModelExporter : GLTFSceneExporter
	{
		protected string gltfFileName;

		private Dictionary<GameObject, int> exportedGameObjects; //TODO: 

		public System.IO.BinaryWriter BufferWriter
		{
			get { return _bufferWriter; }
		}
		static string RetrieveTexturePath(UnityEngine.Texture texture)
		{
			return texture.name;
			//return AssetDatabase.GetAssetPath(texture);
		}

		//var exportOptions = new ExportOptions { TexturePathRetriever = RetrieveTexturePath };
		public ModelExporter(Transform parent, string configJson) :
			base(new[] { parent }, new ExportOptions() { TexturePathRetriever = RetrieveTexturePath, ExportInactivePrimitives = true })
		{
			exportedGameObjects = new Dictionary<GameObject, int>();

			gltfFileName = parent.name;

			GLTFMaterial.RegisterExtension(new MToonMaterialExtensionFactory());
			GLTFMaterial.RegisterExtension(new ConfigJsonExtensionFactory());
			Node.RegisterExtension(new XXXXComponentExtensionFactory());
			Node.RegisterExtension(new XXXXComponentExtensionFactory2());
			Node.RegisterExtension(new MeshFilterAndMeshColliderExtensionFactory());

			// 重写部分属性
			_root = new MyGLTFRoot
			{
				Extensions = new Dictionary<string, IExtension>(),
				Accessors = new List<Accessor>(),
				Animations = new List<GLTFAnimation>(),
				Asset = new Asset
				{
					Version = "2.0",
					Generator = "UnityGLTF"
				},
				Buffers = new List<GLTFBuffer>(),
				BufferViews = new List<BufferView>(),
				Cameras = new List<GLTFCamera>(),
				Images = new List<GLTFImage>(),
				Materials = new List<GLTFMaterial>(),
				Meshes = new List<GLTFMesh>(),
				Nodes = new List<Node>(),
				Samplers = new List<Sampler>(),
				Scenes = new List<GLTFScene>(),
				Skins = new List<Skin>(),
				Textures = new List<GLTFTexture>(),

				gameObjects = new List<GameObject>()
			};

			_imageInfos = new List<ImageInfo>();
			_materials = new Dictionary<Material, int>();
			_textures = new List<Texture>();

			_buffer = new GLTFBuffer();
			_bufferId = new BufferId
			{
				Id = _root.Buffers.Count,
				Root = _root
			};
			_root.Buffers.Add(_buffer);

			var configJsonExtension = new ConfigJsonExtension() { configJson = configJson };
			_root.Extensions.Add(ConfigJsonExtensionFactory.Extension_Name, configJsonExtension);
		}

		/// <summary>
		/// 根据GameObjext获取其在Cache中的索引
		/// </summary>
		public int GetGameObjectIndex(GameObject gameObject)
		{
			return exportedGameObjects[gameObject];
		}

		/// <summary>
		/// 根据索引获取Cache中对应位置的GameObjext
		/// </summary>
		public GameObject GetGameObject(int index)
		{
			var tor = exportedGameObjects.GetEnumerator();
			while (tor.MoveNext())
			{
				if (tor.Current.Value == index)
					return tor.Current.Key;
			}

			Debug.LogException(new DirectoryNotFoundException(string.Format("index:{0},exportedGameObjects.Count:{1}", index, exportedGameObjects.Count)));
			return null;
		}


		public void Export(string path, string gltfFileName = "")
		{
			if (!string.IsNullOrEmpty(gltfFileName))
				this.gltfFileName = gltfFileName;

			//var path = EditorUtility.OpenFolderPanel("glTF Export Path", "", "");
			if (!string.IsNullOrEmpty(path))
			{
				base.SaveGLB(path, gltfFileName);
			}
		}

		public override void SaveGLB(string path, string fileName)
		{
			_shouldUseInternalBufferForImages = false;
			string fullPath = Path.Combine(path, Path.ChangeExtension(fileName, "glb"));

			using (FileStream glbFile = new FileStream(fullPath, FileMode.Create))
			{
				SaveGLBToStream(glbFile, fileName);
			}

			if (!_shouldUseInternalBufferForImages)
			{
				ExportImages(path);
			}
		}

		protected override SceneId ExportScene(string name, Transform[] rootObjTransforms)
		{
			var scene = new GLTFScene();

			if (ExportNames)
			{
				scene.Name = name;
			}

			scene.Nodes = new List<NodeId>(rootObjTransforms.Length);
			foreach (var transform in rootObjTransforms)
			{
				scene.Nodes.Add(ExportNode(transform));
			}

			foreach (var kv in exportedGameObjects)
			{
				ExportComponent(kv.Value, kv.Key.transform);
			}

			_root.Scenes.Add(scene);

			return new SceneId
			{
				Id = _root.Scenes.Count - 1,
				Root = _root
			};
		}

		protected override NodeId ExportNode(Transform nodeTransform)
		{
			NodeId id = base.ExportNode(nodeTransform);

			exportedGameObjects.Add(nodeTransform.gameObject, id.Id);

			//TODO: mesh filter & mesh collider
			MeshFilter meshFilter = nodeTransform.GetComponent<MeshFilter>();
			MeshCollider meshCollider = nodeTransform.GetComponent<MeshCollider>();
			if (meshFilter != null && meshCollider != null)
			{
				var node = _root.Nodes[id.Id];
				if (node.Extensions == null)
					node.Extensions = new Dictionary<string, IExtension>();

				GameObject[] primitives;
				FilterPrimitives(nodeTransform, out primitives);
				MeshId meshId = ExportMesh(nodeTransform.name, primitives);
				_primOwner[new PrimKey { Mesh = nodeTransform.GetComponent<MeshFilter>().sharedMesh }] = meshId;

				ExtensionFactory extFactory = Node.TryGetExtension(MeshFilterAndMeshColliderExtensionFactory.Extension_Name);
				node.Extensions.Add(extFactory.ExtensionName, new MeshFilterAndMeshColliderExtension() { Mesh = meshId });
			}
			return id;
		}

		private void FilterPrimitives(Transform transform, out GameObject[] primitives)
		{
			var childCount = transform.childCount;
			var prims = new List<GameObject>(childCount + 1);

			// add another primitive if the root object also has a mesh
			if (transform.gameObject.activeSelf || ExportDisabledGameObjects)
			{
				if (transform.GetComponent<MeshFilter>() != null && transform.GetComponent<MeshCollider>() != null)
				{
					prims.Add(transform.gameObject);
				}
			}

			primitives = prims.ToArray();
		}

		private MeshId ExportMesh(string name, GameObject[] primitives)
		{
			// check if this set of primitives is already a mesh
			MeshId existingMeshId = null;
			var key = new PrimKey();
			foreach (var prim in primitives)
			{
				var filter = prim.GetComponent<MeshFilter>();
				key.Mesh = filter.sharedMesh;

				MeshId tempMeshId;
				if (_primOwner.TryGetValue(key, out tempMeshId) && (existingMeshId == null || tempMeshId == existingMeshId))
				{
					existingMeshId = tempMeshId;
				}
				else
				{
					existingMeshId = null;
					break;
				}
			}

			// if so, return that mesh id
			if (existingMeshId != null)
			{
				return existingMeshId;
			}

			// if not, create new mesh and return its id
			var mesh = new GLTFMesh();

			if (ExportNames)
			{
				mesh.Name = name;
			}

			mesh.Primitives = new List<MeshPrimitive>(primitives.Length);
			foreach (var prim in primitives)
			{
				MeshPrimitive[] meshPrimitives = ExportPrimitive(prim, mesh);
				if (meshPrimitives != null)
				{
					mesh.Primitives.AddRange(meshPrimitives);
				}
			}

			var id = new MeshId
			{
				Id = _root.Meshes.Count,
				Root = _root
			};

			if (mesh.Primitives.Count > 0)
			{
				_root.Meshes.Add(mesh);
				return id;
			}

			return null;
		}

		private MeshPrimitive[] ExportPrimitive(GameObject gameObject, GLTFMesh mesh)
		{
			Mesh meshObj = null;
			SkinnedMeshRenderer smr = null;
			var filter = gameObject.GetComponent<MeshFilter>();
			if (filter)
			{
				meshObj = filter.sharedMesh;
			}

			if (!meshObj)
			{
				Debug.LogWarning($"MeshFilter.sharedMesh on GameObject:{gameObject.name} is missing, skipping", gameObject);
				return null;
			}

			var prims = new MeshPrimitive[meshObj.subMeshCount];
			List<MeshPrimitive> nonEmptyPrims = null;
			var vertices = meshObj.vertices;
			if (vertices.Length < 1)
			{
				Debug.LogWarning("MeshFilter does not contain any vertices, won't export: " + gameObject.name, gameObject);
				return null;
			}

			if (!_meshToPrims.ContainsKey(meshObj))
			{
				AccessorId aPosition = null, aNormal = null, aTangent = null, aTexcoord0 = null, aTexcoord1 = null, aColor0 = null;

				aPosition = ExportAccessor(SchemaExtensions.ConvertVector3CoordinateSpaceAndCopy(meshObj.vertices, SchemaExtensions.CoordinateSpaceConversionScale));

				if (meshObj.normals.Length != 0)
					aNormal = ExportAccessor(SchemaExtensions.ConvertVector3CoordinateSpaceAndCopy(meshObj.normals, SchemaExtensions.CoordinateSpaceConversionScale));

				if (meshObj.tangents.Length != 0)
					aTangent = ExportAccessor(SchemaExtensions.ConvertVector4CoordinateSpaceAndCopy(meshObj.tangents, SchemaExtensions.TangentSpaceConversionScale));

				if (meshObj.uv.Length != 0)
					aTexcoord0 = ExportAccessor(SchemaExtensions.FlipTexCoordArrayVAndCopy(meshObj.uv));

				if (meshObj.uv2.Length != 0)
					aTexcoord1 = ExportAccessor(SchemaExtensions.FlipTexCoordArrayVAndCopy(meshObj.uv2));

				if (settings.ExportVertexColors && meshObj.colors.Length != 0)
					aColor0 = ExportAccessor(QualitySettings.activeColorSpace == ColorSpace.Linear ? meshObj.colors : meshObj.colors.ToLinear());

				aPosition.Value.BufferView.Value.Target = BufferViewTarget.ArrayBuffer;
				if (aNormal != null) aNormal.Value.BufferView.Value.Target = BufferViewTarget.ArrayBuffer;
				if (aTangent != null) aTangent.Value.BufferView.Value.Target = BufferViewTarget.ArrayBuffer;
				if (aTexcoord0 != null) aTexcoord0.Value.BufferView.Value.Target = BufferViewTarget.ArrayBuffer;
				if (aTexcoord1 != null) aTexcoord1.Value.BufferView.Value.Target = BufferViewTarget.ArrayBuffer;
				if (aColor0 != null) aColor0.Value.BufferView.Value.Target = BufferViewTarget.ArrayBuffer;

				_meshToPrims.Add(meshObj, new MeshAccessors()
				{
					aPosition = aPosition,
					aNormal = aNormal,
					aTangent = aTangent,
					aTexcoord0 = aTexcoord0,
					aTexcoord1 = aTexcoord1,
					aColor0 = aColor0,
					subMeshPrimitives = new Dictionary<int, MeshPrimitive>()
				});
			}

			var accessors = _meshToPrims[meshObj];

			// walk submeshes and export the ones with non-null meshes
			for (int submesh = 0; submesh < meshObj.subMeshCount; submesh++)
			{
				if (!accessors.subMeshPrimitives.ContainsKey(submesh))
				{
					var primitive = new MeshPrimitive();

					var topology = meshObj.GetTopology(submesh);
					var indices = meshObj.GetIndices(submesh);
					if (topology == MeshTopology.Triangles) SchemaExtensions.FlipTriangleFaces(indices);

					primitive.Mode = GetDrawMode(topology);
					primitive.Indices = ExportAccessor(indices, true);
					primitive.Indices.Value.BufferView.Value.Target = BufferViewTarget.ElementArrayBuffer;

					primitive.Attributes = new Dictionary<string, AccessorId>();
					primitive.Attributes.Add(SemanticProperties.POSITION, accessors.aPosition);

					if (accessors.aNormal != null)
						primitive.Attributes.Add(SemanticProperties.NORMAL, accessors.aNormal);
					if (accessors.aTangent != null)
						primitive.Attributes.Add(SemanticProperties.TANGENT, accessors.aTangent);
					if (accessors.aTexcoord0 != null)
						primitive.Attributes.Add(SemanticProperties.TEXCOORD_0, accessors.aTexcoord0);
					if (accessors.aTexcoord1 != null)
						primitive.Attributes.Add(SemanticProperties.TEXCOORD_1, accessors.aTexcoord1);
					if (accessors.aColor0 != null)
						primitive.Attributes.Add(SemanticProperties.COLOR_0, accessors.aColor0);

					primitive.Material = null;

					ExportBlendShapes(smr, meshObj, submesh, primitive, mesh);

					accessors.subMeshPrimitives.Add(submesh, primitive);
				}

				var submeshPrimitive = accessors.subMeshPrimitives[submesh];
				prims[submesh] = new MeshPrimitive(submeshPrimitive, _root)
				{
					//Material = ExportMaterial(materialsObj[submesh]),
				};
			}

			//remove any prims that have empty triangles
			nonEmptyPrims = new List<MeshPrimitive>(prims);
			nonEmptyPrims.RemoveAll(EmptyPrimitive);
			prims = nonEmptyPrims.ToArray();

			return prims;
		}

		protected void ExportComponent(int index, Transform nodeTransform)
		{
			Node node = _root.Nodes[index];

			//export Component
			MonoBehaviour[] components = nodeTransform.GetComponents<MonoBehaviour>();
			if (components != null)
			{
				if (node.Extensions == null)
					node.Extensions = new Dictionary<string, IExtension>();

				foreach (var component in components)
				{
					string extensionName = component.GetType().ToString();
					ComponentExtensionFactory extensionFactory = Node.TryGetExtension(extensionName) as ComponentExtensionFactory;
					IPropExtension ext = null;
					if (extensionFactory != null)
					{
						ext = extensionFactory.ConstructExtension(component);
					}
					node.Extensions.Add(extensionName, ext);
				}
			}
		}

		public override MaterialId ExportMaterial(Material materialObj)
		{
			MaterialId id = base.ExportMaterial(materialObj);

			GLTFMaterial gltfMaterial = GetRoot().Materials[id.Id];
			MaterialExtensionFactory factory = GLTFMaterial.TryGetExtension(materialObj.shader.name) as MaterialExtensionFactory;

			if (gltfMaterial.Extensions == null) gltfMaterial.Extensions = new Dictionary<string, IExtension>();

			IPropExtension ext = factory.GetExtension() as IPropExtension;
			ExportMaterialExtension(ext, materialObj, factory);
			gltfMaterial.Extensions[MToonMaterialExtensionFactory.Extension_Name] = ext;

			return id;
		}

		private void ExportMaterialExtension(IPropExtension ext, Material materialObj, MaterialExtensionFactory factory)
		{
			System.Type t = ext.GetType();

			for (int i = 0; i < factory.FloatProperties.Length; i++)
			{
				string prop = factory.FloatProperties[i];
				if (materialObj.HasFloat(prop))
					t.GetField(prop).SetValue(ext, materialObj.GetFloat(prop));
			}

			for (int i = 0; i < factory.ColorProperties.Length; i++)
			{
				string prop = factory.ColorProperties[i];
				if (materialObj.HasColor(prop))
					t.GetField(prop).SetValue(ext, materialObj.GetColor(prop));
			}

			for (int i = 0; i < factory.TextureProperties.Length; i++)
			{
				string prop = factory.TextureProperties[i];
				if (materialObj.HasTexture(prop))
				{
					Texture Tex = materialObj.GetTexture(prop);
					if (Tex != null)
					{
						//if (Tex is Texture2D)
						//{
						TextureInfo textureInfo = ExportTextureInfo(Tex, TextureMapType.Main);
						t.GetField(prop).SetValue(ext, textureInfo);
						ExportTextureTransform(textureInfo, materialObj, MToonMaterialExtensionFactory._MainTex);
					}
					else
					{
						//Debug.LogErrorFormat("Can't export a {0} emissive texture in material {1}", Tex.GetType(), materialObj.name);
					}
				}
			}
		}


	}
}
