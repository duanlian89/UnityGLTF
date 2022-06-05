using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;


namespace CKUnityGLTF
{
	public class ConfigJsonExtension : IExtension
	{
		public string configJson = "";

		public JProperty Serialize()
		{
			JProperty p = new JProperty(ConfigJsonExtensionFactory.Extension_Name, configJson);
			return p;
		}

		public IExtension Clone(GLTFRoot root)
		{
			return new ConfigJsonExtension();
		}
	}
}
