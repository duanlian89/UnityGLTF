using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GLTF.Schema;
using System.IO;
using System.Text;
using UnityGLTF;

namespace CKUnityGLTF
{
	public class ModelExporter : GLTFSceneExporter
	{
		protected string gltfFileName;
		public System.IO.BinaryWriter BufferWriter
		{
			get { return _bufferWriter; }
		}
		static string RetrieveTexturePath(UnityEngine.Texture texture)
		{
			//return texture.name;
			return AssetDatabase.GetAssetPath(texture);
		}

		//var exportOptions = new ExportOptions { TexturePathRetriever = RetrieveTexturePath };
		public ModelExporter(Transform parent, string configJson) :
			base(new[] { parent }, new ExportOptions() { TexturePathRetriever = RetrieveTexturePath, ExportInactivePrimitives = true })
		{
			gltfFileName = parent.name;

			GLTFMaterial.RegisterExtension(new MToonMaterialExtensionFactory());
			GLTFMaterial.RegisterExtension(new ConfigJsonExtensionFactory());

			// 重写部分属性
			_root = new MyGLTFRoot
			{
				Extensions = new Dictionary<string, IExtension>(),
				Accessors = new List<Accessor>(),
				Asset = new Asset
				{
					Version = "2.0"
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

		public void Export(string path ,string gltfFileName = "")
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

			RecurGameObject(rootObjTransforms[0].gameObject); // TODO: 默认只有一个根节点

			scene.Nodes = new List<NodeId>(rootObjTransforms.Length);
			foreach (var transform in rootObjTransforms)
			{
				scene.Nodes.Add(ExportNode(transform));
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

			Node node = _root.Nodes[id.Id];

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

			return id;
		}

		// TODO: 应该是不需要了
		private void RecurGameObject(GameObject go)
		{
			(_root as MyGLTFRoot).gameObjects.Add(go);

			int childCount = go.transform.childCount;
			for (var i = 0; i < childCount; i++)
			{
				RecurGameObject(go.transform.GetChild(i).gameObject);
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
