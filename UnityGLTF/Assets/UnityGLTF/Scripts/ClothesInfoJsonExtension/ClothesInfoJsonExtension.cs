
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;


namespace CKUnityGLTF
{
	public class ClothesInfoJsonExtension : IExtension
	{
		public string clothesInfoJson = "";

		public JProperty Serialize()
		{
			JProperty p = new JProperty(ClothesInfoJsonExtensionFactory.Extension_Name, clothesInfoJson);
			return p;
		}

		public IExtension Clone(GLTFRoot root)
		{
			return new ClothesInfoJsonExtension();
		}
	}
}
