using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using UnityGLTF;


public class XXXXComponentExtensionFactory2 : ComponentExtensionFactory
{
	public const string Extension_Name = "Test.XXXX2";
	#region property name

	public const string _Cutoff = "_Cutoff";

	#endregion
	public XXXXComponentExtensionFactory2()
	{
		ExtensionName = Extension_Name;
	}

	// [import] 从extensionToken读出属性，初始化 MToonMaterialExtension
	public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
	{
		XXXXComponentExtension2 ext = new XXXXComponentExtension2();
		ext.Deserialize(root, extensionToken);
		return ext;
	}

	// [export]
	public override IComponentExtension ConstructExtension(Component component)
	{
		IComponentExtension extension = new XXXXComponentExtension2(component as Test.XXXX2);
		return extension;
	}
}
