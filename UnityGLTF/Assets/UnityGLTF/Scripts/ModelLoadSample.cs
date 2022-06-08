using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

namespace CKUnityGLTF
{

	public class ModelLoadSample : MonoBehaviour
	{
		//public string glbPath = @"C:\WorkSpace\ForkUnityGLTF\UnityGLTF\www\M_Upper_Ugc_016_01_01.glb";
		public string glbPath = @"http://127.0.0.1:8080/M_Upper_Ugc_016_01_01.glb";

		public GameObject go1;
		public string config1;
		public GameObject go2;
		public string config2;

		public UnityGLTF.Cache.AssetCache assetCache;

		void Start()
		{
			Load();
		}

		async void Load()
		{
			//HttpWebRequest request = WebRequest.Create(glbPath) as HttpWebRequest;
			//request.MediaType = "GET";
			//HttpWebResponse respone = await request.GetResponseAsync() as HttpWebResponse;
			//Stream stream = respone.GetResponseStream();
			//Debug.Log(stream.CanSeek);

			//await ModelLoader.LoadStream(glbPath);
			var importer = ModelLoader.GetImporter(glbPath);
			await importer.Load();
			go1 = importer.CreatedObject;
			config1 = importer.ConfigJson;

			await importer.Load();
			go2 = importer.CreatedObject;
			config2 = importer.ConfigJson;

			assetCache = importer.AssetCache;

			//importer.Dispose();
			//importer = null;
		}



		async void Load1()
		{
			string glbName = "";
			byte[] bytes = null;

			var importer = ModelLoader.GetImporter(glbName, bytes);

			await importer.Load((go, config) =>
			{
				go1 = go;
				config1 = config;
			});

			await importer.Load();
			go2 = importer.CreatedObject;
			config2 = importer.ConfigJson;

			importer.Dispose();
			importer = null;
		}

		public void Do()
		{
			Debug.Log(go1 == go2);

			var isEqual = go1.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture
				== go2.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture;
			Debug.Log("mainTexture: " + isEqual);

			isEqual = go1.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[0]
				== go2.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[0];
			Debug.Log("material: " + isEqual);

			//go1.AddComponent<MeshFilter>().sharedMesh = assetCache.MeshCache[0].LoadedMesh;
			//go2.AddComponent<MeshFilter>().sharedMesh = assetCache.MeshCache[0].LoadedMesh;
			isEqual = go1.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh
				== go2.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
			Debug.Log("sharedMesh: " + isEqual);


			isEqual = go1.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture
				== assetCache.ImageCache[0];
			Debug.Log(isEqual);

			isEqual = assetCache.TextureCache[0].Texture == assetCache.ImageCache[0];
			Debug.Log(isEqual);
		}

		public void After()
		{
			//Texture tex = assetCache.TextureCache[0].Texture;
			//UnityEngine.Object.DestroyImmediate(tex, true);

			var material = assetCache.MaterialCache[0].UnityMaterialWithVertexColor;
			UnityEngine.Object.DestroyImmediate(material, true);
			// await ModelLoader.LoadStream(glbPath);

			//await ModelLoader.LoadStream(glbPath);
		}

		public void Again()
		{
			bool b = Utils.DoesObjectWithInstanceIDExist(go1.GetInstanceID());

			var obj1 = Utils.FindObjectFromInstanceID(go1.GetInstanceID());
			var obj2 = Utils.ForceLoadFromInstanceID(go1.GetInstanceID());
			Debug.Log(obj1 == obj2);

			obj1 = Utils.FindObjectFromInstanceID(go1.GetInstanceID());
			obj2 = Utils.FindObjectFromInstanceID(go2.GetInstanceID());
			Debug.Log(obj1 == obj2);

			obj1 = Utils.ForceLoadFromInstanceID(go1.GetInstanceID());
			obj2 = Utils.ForceLoadFromInstanceID(go1.GetInstanceID());
			Debug.Log(obj1 == obj2);

			obj1 = Utils.ForceLoadFromInstanceID(go1.GetInstanceID());
			obj2 = Utils.ForceLoadFromInstanceID(go2.GetInstanceID());
			Debug.Log(obj1 == obj2);
		}
	}
}
