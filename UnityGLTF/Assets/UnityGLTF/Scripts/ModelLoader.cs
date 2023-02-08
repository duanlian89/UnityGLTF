using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using System.Threading.Tasks;
using UnityGLTF;
using CKUnityGLTF;
using GLTF.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ModelLoader
{
	public class Result
	{
		public GameObject model;
		public string configJson;
	}

	static ModelLoader()
	{
		GLTFMaterial.RegisterExtension(new MToonMaterialExtensionFactory());
		GLTFMaterial.RegisterExtension(new MToonClothingMaterialExtensionFactory());
		GLTFMaterial.RegisterExtension(new StandardMaterialExtensionFactory());
		GLTFMaterial.RegisterExtension(new StandardRoughnessMaterialExtensionFactory());
		GLTFMaterial.RegisterExtension(new ConfigJsonExtensionFactory());
		Node.RegisterExtension(new MeshFilterAndMeshColliderExtensionFactory());
	}

	public static ModelImporter GetImporter(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions);
		sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;

		//sceneImporter.ConfigJson = ParseConfigJson(System.IO.File.ReadAllBytes(absoluteStreamingPath));

		//await sceneImporter.LoadSceneAsync();
		return sceneImporter;
	}

	public static ModelImporter GetImporter(string fileName, byte[] data, float scaleFactor = 0.5f)
	{
		//string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		ImportOptionsExtension importOptions = new ImportOptionsExtension();
		importOptions.scaleFactor = scaleFactor;
		importOptions.DataLoader = new VirtualStreamLoader() { data = data };

		ModelImporter sceneImporter = new ModelImporter(fileName, importOptions);
		sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;

		//sceneImporter.ConfigJson = ParseConfigJson(data);

		return sceneImporter;

		//await sceneImporter.LoadSceneAsync(-1, true, (go, e) =>
		//{
		//	onLoadComplete.Invoke(go, sceneImporter.ConfigJson, e);
		//});
	}

	/*
	private static string ParseConfigJson(byte[] data)
	{
		string regStr = "\"ConfigJson\".*\"}\"";
		string replaceStr = "ConfigJson\":\"";

		string dataStr = System.Text.UTF8Encoding.UTF8.GetString(data);
		System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regStr);
		var match = regex.Match(dataStr);
		if (match.Success)
		{
			string cfgJsonString = match.Value.Replace(replaceStr, "");
			var obj = JsonConvert.DeserializeObject(cfgJsonString, new JsonSerializerSettings()
			{
				CheckAdditionalContent = false,
			});
			return obj.ToString();
		}
		else
		{
			Debug.LogException(new Exception("can not get ConfigJson from data!"));
			return "";
		}
	}
	*/
}
