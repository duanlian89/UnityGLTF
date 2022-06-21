using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKUnityGLTF;
using UnityGLTF;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

namespace CKUnityGLTF
{
	public class StandardRoughnessMaterialExtensionFactory : MaterialExtensionFactory
	{
		public const string Extension_Name = "Shader Graphs/StandardRoughness";

		#region property name

		public const string _Diffuse = "_Diffuse";
		public const string _Normal = "_Normal";
		public const string _Height = "_Height";
		public const string _Roughness = "_Roughness";
		public const string _Emission = "_Emission";
		public const string _Metallic = "_Metallic";

		#endregion

		public StandardRoughnessMaterialExtensionFactory()
		{
			ExtensionName = Extension_Name;

			TextureProperties = new string[] { _Diffuse, _Normal, _Height, _Roughness, _Emission, _Metallic };
		}

		// 从extensionToken读出属性，初始化 MaterialExtension
		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			StandardRoughnessMaterialExtension ext = new StandardRoughnessMaterialExtension();
			ext.Deserialize(root, extensionToken);
			return ext;
		}

		public override IExtension ConstructExtension()
		{
			StandardRoughnessMaterialExtension ext = new StandardRoughnessMaterialExtension();
			return ext;
		}
	}
}
