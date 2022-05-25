using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ModelLoader
{
	public async Task<GameObject> Load(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		var sceneImporter = new UnityGLTF.ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions);
		sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
		await sceneImporter.LoadSceneAsync();
		GameObject go = sceneImporter.CreatedObject;
		return go;
	}
}
