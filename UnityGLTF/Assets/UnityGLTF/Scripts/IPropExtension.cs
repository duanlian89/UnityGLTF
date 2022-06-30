using GLTF.Schema;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CKUnityGLTF
{
	public interface IPropExtension : IExtension
	{
		void Deserialize(GLTFRoot root, JProperty extensionToken);
	}

	public interface IComponentExtension : IPropExtension
	{
		void SetComponentParam(UnityEngine.Component com);
	}

	public interface IByteComponentExtension : IComponentExtension
	{
		byte[] SerializeToByte();
	}

	public abstract class ComponentExtensionFactory : ExtensionFactory
	{
		public abstract IComponentExtension ConstructExtension(Component component);
	}

	public abstract class MaterialExtensionFactory : ExtensionFactory
	{
		public string[] FloatProperties = new string[] { };

		public string[] ColorProperties = new string[] { };

		public string[] TextureProperties = new string[] { };

		public static string shaderKeywords = "shaderKeywords";

		public abstract IExtension ConstructExtension();

	}
}
