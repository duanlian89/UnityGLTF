using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoadSample : MonoBehaviour
{
	public string glbPath = @"C:/WorkSpace/ForkUnityGLTF/UnityGLTF/www/sample.glb";

	async void Start()
	{
		await new ModelLoader().Load(glbPath);
	}
}
