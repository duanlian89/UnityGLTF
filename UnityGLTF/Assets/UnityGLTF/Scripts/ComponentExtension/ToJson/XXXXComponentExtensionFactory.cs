using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

namespace UnityGLTF
{
	public class XXXXComponentExtensionFactory : ComponentExtensionFactory
	{
		public const string Extension_Name = "UnityGLTF.XXXX";
		#region property name

		public const string _Cutoff = "_Cutoff";

		#endregion
		public XXXXComponentExtensionFactory()
		{
			ExtensionName = Extension_Name;
		}

		// [import] 从extensionToken读出属性，初始化 MToonMaterialExtension
		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			XXXXComponentExtension ext = new XXXXComponentExtension();
			ext.Deserialize(root, extensionToken);
			return ext;
		}

		// [export]
		public override IComponentExtension ConstructExtension(Component component)
		{
			IComponentExtension extension = new XXXXComponentExtension(component as XXXX);
			return extension;
		}
	}
}
