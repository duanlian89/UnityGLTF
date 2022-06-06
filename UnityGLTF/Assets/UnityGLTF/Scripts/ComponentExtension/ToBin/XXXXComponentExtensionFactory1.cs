using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using UnityGLTF;

namespace CKUnityGLTF
{
	public class XXXXComponentExtensionFactory1 : ComponentExtensionFactory
	{
		ModelExporter exporter;
		ModelImporter importer;
		public const string Extension_Name = "CKUnityGLTF.XXXX";
		#region property name
		public const string _Cutoff = "_Cutoff";

		#endregion
		public XXXXComponentExtensionFactory1(GLTFSceneExporter exporter, GLTFSceneImporter importer)
		{
			this.exporter = exporter as ModelExporter;
			this.importer = importer as ModelImporter;

			ExtensionName = Extension_Name;
		}

		//[import]从extensionToken读出属性，初始化 MToonMaterialExtension
		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			XXXXComponentExtension1 ext = new XXXXComponentExtension1(importer);
			ext.Deserialize(root, extensionToken);

			return ext;
		}

		//[export]
		public override IComponentExtension ConstructExtension(Component component)
		{
			IByteComponentExtension ext = new XXXXComponentExtension1(exporter, component as XXXX);

			ModelExporter.AlignToBoundary(exporter.BufferWriter.BaseStream, 0x00);
			uint byteOffset = ModelExporter.CalculateAlignment((uint)exporter.BufferWriter.BaseStream.Position, 4);

			byte[] bytes = ext.SerializeToByte();
			exporter.BufferWriter.Write(bytes);

			//var byteLength = exporter.BufferWriter.BaseStream.Position - byteOffset;

			//byteLength = exporter.AppendToBufferMultiplyOf4(byteOffset, byteLength);
			//(ext as XXXXComponentExtension1).BufferView = exporter.ExportBufferView((uint)byteOffset, (uint)byteLength);

			uint byteLength = ModelExporter.CalculateAlignment((uint)exporter.BufferWriter.BaseStream.Position - byteOffset, 4);
			(ext as XXXXComponentExtension1).BufferView = exporter.ExportBufferView(byteOffset, (uint)byteLength);

			return ext;
		}
	}
}
