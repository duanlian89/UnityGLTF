using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class ModelLoadSample : MonoBehaviour
{
	//public string glbPath = @"C:\WorkSpace\ForkUnityGLTF\UnityGLTF\www\M_Upper_Ugc_016_01_01.glb";
	public string glbPath = @"http://127.0.0.1:8080/M_Upper_Ugc_016_01_01.glb";
	public GameObject go1;
	public GameObject go2;
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
		var importer = await ModelLoader.LoadI(glbPath);
		go1 = importer.CreatedObject;
		importer._assetCache.NodeCache = new GameObject[importer._gltfRoot.Nodes?.Count ?? 0];
		await importer.LoadSceneAsync();
		go2 = importer.CreatedObject;
		//importer._assetCache.NodeCache = new GameObject[importer._gltfRoot.Nodes?.Count ?? 0];
		assetCache = importer._assetCache;
	}

	public void Do()
	{
		Debug.Log(go1 == go2);
		var isEqual = go1.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture
			== go2.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture;
		Debug.Log(isEqual);

		isEqual = go1.GetComponentInChildren<SkinnedMeshRenderer>().material
			== go2.GetComponentInChildren<SkinnedMeshRenderer>().material;
		Debug.Log("material: " + isEqual);

		//go1.AddComponent<MeshFilter>().sharedMesh = assetCache.MeshCache[0].LoadedMesh;
		//go2.AddComponent<MeshFilter>().sharedMesh = assetCache.MeshCache[0].LoadedMesh;
		isEqual = go1.GetComponentInChildren<MeshFilter>().sharedMesh
			== go2.GetComponentInChildren<MeshFilter>().sharedMesh;
		Debug.Log("sharedMesh: " + isEqual);


		isEqual = go1.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture
			== assetCache.ImageCache[0];
		Debug.Log(isEqual);

		isEqual = assetCache.TextureCache[0].Texture == assetCache.ImageCache[0];
		Debug.Log(isEqual);
	}

	public void After()
	{
		Texture tex = assetCache.TextureCache[0].Texture;
		UnityEngine.Object.DestroyImmediate(tex, true);

		// await ModelLoader.LoadStream(glbPath);

		//await ModelLoader.LoadStream(glbPath);
	}
}
