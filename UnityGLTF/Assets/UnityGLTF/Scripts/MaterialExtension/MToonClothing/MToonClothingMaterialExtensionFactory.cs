using GLTF.Schema;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace CKUnityGLTF
{
	public class MToonClothingMaterialExtensionFactory : MToonMaterialExtensionFactory
	{
		new public const string Extension_Name = "VRM/MToonClothing";

		public const string _Mask1 = "_Mask1";

		public MToonClothingMaterialExtensionFactory()
			: base()
		{
			ExtensionName = Extension_Name;

			List<string> _TextureProperties = TextureProperties.ToList();
			_TextureProperties.Add(_Mask1);
			TextureProperties = _TextureProperties.ToArray();
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			MToonClothingMaterialExtension ext = new MToonClothingMaterialExtension();
			ext.Deserialize(root, extensionToken);
			return ext;
		}

		public override IExtension ConstructExtension()
		{
			MToonClothingMaterialExtension ext = new MToonClothingMaterialExtension();
			return ext;
		}
	}
}
