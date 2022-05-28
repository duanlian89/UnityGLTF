using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;
using GLTF.Extensions;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

public class ConfigJsonExtensionFactory : ExtensionFactory
{
	public const string Extension_Name = "ConfigJson";

	public ConfigJsonExtensionFactory()
	{
		this.ExtensionName = Extension_Name;
	}

	public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
	{
		ConfigJsonExtension extension = new ConfigJsonExtension();
		if (extensionToken.Value!= null)
		{
			string configJson = extensionToken.Value.ToString();
			extension.configJson = configJson;
		}
		return extension;
	}
}
