using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoadSample : MonoBehaviour
{
	public string glbPath = @"C:/WorkSpace/ForkUnityGLTF/UnityGLTF/www/sample.glb";

	void Start()
	{
		Load();
	}

	async void Load()
	{
		// await ModelLoader.LoadStream(glbPath);

		await ModelLoader.LoadStream(glbPath, (go, e) =>
		{
			if (e != null)
				Debug.Log(e.ToString());
			else
				Debug.Log(go.name);
		});
	}
}
