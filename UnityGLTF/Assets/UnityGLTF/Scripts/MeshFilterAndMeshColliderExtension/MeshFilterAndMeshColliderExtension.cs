using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;
using GLTF.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CKUnityGLTF
{
	public class MeshFilterAndMeshColliderExtension : IExtension
	{
		/// <summary>
		/// The index of the mesh in this node's Extensions
		/// </summary>
		public MeshId Mesh;

		public JProperty Serialize()
		{
			JObject obj = new JObject();
			obj.Add("mesh", Mesh.Id);
			return new JProperty(MeshFilterAndMeshColliderExtensionFactory.Extension_Name, obj);
		}

		public IExtension Clone(GLTFRoot root)
		{
			MeshFilterAndMeshColliderExtension ext = new MeshFilterAndMeshColliderExtension();
			ext.Mesh = Mesh;
			return ext;
		}
	}
}
