using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using UnityGLTF;
using UnityGLTF.Cache;
using GLTF.Extensions;

public class XXXXComponentExtensionFactory1 : ComponentExtensionFactory
{
	ModelExporter exporter;
	ModelImporter1 importer;
	public const string Extension_Name = "Test.XXXX";
	#region property name
	public const string _Cutoff = "_Cutoff";

	#endregion
	public XXXXComponentExtensionFactory1(GLTFSceneExporter exporter, GLTFSceneImporter importer)
	{
		this.exporter = exporter as ModelExporter;
		this.importer = importer as ModelImporter1;

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
		IByteComponentExtension ext = new XXXXComponentExtension1(exporter, component as Test.XXXX);

		var byteOffset = exporter.BufferWriter.BaseStream.Position;

		byte[] bytes = ext.SerializeToByte();
		exporter.BufferWriter.Write(bytes);

		var byteLength = exporter.BufferWriter.BaseStream.Position - byteOffset;

		byteLength = exporter.AppendToBufferMultiplyOf4(byteOffset, byteLength);

		(ext as XXXXComponentExtension1).BufferView = exporter.ExportBufferView((uint)byteOffset, (uint)byteLength);
		return ext;
	}
}
