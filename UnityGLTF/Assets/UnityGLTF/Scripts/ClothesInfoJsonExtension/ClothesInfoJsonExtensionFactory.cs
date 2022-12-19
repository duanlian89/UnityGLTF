using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;
using GLTF.Extensions;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

namespace CKUnityGLTF
{
	public class ClothesInfoJsonExtensionFactory : ExtensionFactory
	{
		public const string Extension_Name = "ClothesInfoJson";

		public ClothesInfoJsonExtensionFactory()
		{
			this.ExtensionName = Extension_Name;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			ClothesInfoJsonExtension extension = new ClothesInfoJsonExtension();
			if (extensionToken.Value != null)
			{
				string configJson = extensionToken.Value.ToString();
				extension.clothesInfoJson = configJson;
			}
			return extension;
		}
	}
}
