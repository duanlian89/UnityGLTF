using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKUnityGLTF;
using UnityGLTF;
using GLTF.Schema;
using GLTF.Extensions;
using Newtonsoft.Json.Linq;

namespace CKUnityGLTF
{
	public class StandardRoughnessMaterialExtension : IPropExtension
	{
		// _Diffuse, _Normal, _Height, _Roughness, _Emission, _Metallic

		public TextureInfo _Diffuse;
		public static readonly TextureInfo _Diffuse_Default = null;

		public TextureInfo _Normal;
		public static readonly TextureInfo _Normal_Default = null;

		public TextureInfo _Height;
		public static readonly TextureInfo _Height_Default = null;

		public TextureInfo _Roughness;
		public static readonly TextureInfo _Roughness_Default = null;

		public TextureInfo _Emission;
		public static readonly TextureInfo _Emission_Default = null;

		public TextureInfo _Metallic;
		public static readonly TextureInfo _Metallic_Default = null;

		public IExtension Clone(GLTFRoot root)
		{
			return new StandardRoughnessMaterialExtension();
		}

		public JProperty Serialize()
		{
			JObject ext = new JObject();

			if (_Diffuse != _Diffuse_Default)
			{
				ext.Add(new JProperty(StandardRoughnessMaterialExtensionFactory._Diffuse, new JObject(new JProperty(TextureInfo.INDEX, _Diffuse.Index.Id))));
			}

			if (_Normal != _Normal_Default)
			{
				ext.Add(new JProperty(StandardRoughnessMaterialExtensionFactory._Normal, new JObject(new JProperty(TextureInfo.INDEX, _Normal.Index.Id))));
			}

			if (_Height != _Height_Default)
			{
				ext.Add(new JProperty(StandardRoughnessMaterialExtensionFactory._Height, new JObject(new JProperty(TextureInfo.INDEX, _Height.Index.Id))));
			}

			if (_Roughness != _Roughness_Default)
			{
				ext.Add(new JProperty(StandardRoughnessMaterialExtensionFactory._Roughness, new JObject(new JProperty(TextureInfo.INDEX, _Roughness.Index.Id))));
			}

			if (_Emission != _Emission_Default)
			{
				ext.Add(new JProperty(StandardRoughnessMaterialExtensionFactory._Emission, new JObject(new JProperty(TextureInfo.INDEX, _Emission.Index.Id))));
			}

			if (_Metallic != _Metallic_Default)
			{
				ext.Add(new JProperty(StandardRoughnessMaterialExtensionFactory._Metallic, new JObject(new JProperty(TextureInfo.INDEX, _Metallic.Index.Id))));
			}

			return new JProperty(StandardRoughnessMaterialExtensionFactory.Extension_Name, ext);
		}

		public void Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			JToken token = extensionToken.Value[StandardRoughnessMaterialExtensionFactory._Diffuse];
			_Diffuse = token != null ? token.DeserializeAsTexture(root) : _Diffuse_Default;

			token = extensionToken.Value[StandardRoughnessMaterialExtensionFactory._Normal];
			_Normal = token != null ? token.DeserializeAsTexture(root) : _Normal_Default;

			token = extensionToken.Value[StandardRoughnessMaterialExtensionFactory._Height];
			_Height = token != null ? token.DeserializeAsTexture(root) : _Height_Default;

			token = extensionToken.Value[StandardRoughnessMaterialExtensionFactory._Roughness];
			_Roughness = token != null ? token.DeserializeAsTexture(root) : _Roughness_Default;

			token = extensionToken.Value[StandardRoughnessMaterialExtensionFactory._Emission];
			_Emission = token != null ? token.DeserializeAsTexture(root) : _Emission_Default;

			token = extensionToken.Value[StandardRoughnessMaterialExtensionFactory._Metallic];
			_Metallic = token != null ? token.DeserializeAsTexture(root) : _Metallic_Default;
		}
	}
}
