using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using GLTF.Extensions;

namespace CKUnityGLTF
{

	public class MeshFilterAndMeshColliderExtensionFactory : ExtensionFactory
	{
		public const string Extension_Name = "MeshFilterAndMeshCollider";

		public MeshFilterAndMeshColliderExtensionFactory()
		{
			ExtensionName = Extension_Name;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			MeshId meshId = new MeshId();
			meshId.Root = root;
			JToken token = extensionToken.Value["mesh"];
			meshId.Id = token.DeserializeAsInt();

			return new MeshFilterAndMeshColliderExtension()
			{
				Mesh = meshId
			};
		}
	}
}
