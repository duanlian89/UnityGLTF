using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using System.Threading.Tasks;
using UnityGLTF;
using CKUnityGLTF;

public class ModelLoader
{
	public class Result
	{
		public GameObject model;
		public string configJson;
	}

	public static ModelImporter GetImporter(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions);
		sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
		//await sceneImporter.LoadSceneAsync();
		return sceneImporter;
	}

	public static ModelImporter GetImporter(string fileName, byte[] data)
	{
		//string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new VirtualStreamLoader() { data = data };

		ModelImporter sceneImporter = new ModelImporter(fileName, importOptions);
		sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
		return sceneImporter;

		//await sceneImporter.LoadSceneAsync(-1, true, (go, e) =>
		//{
		//	onLoadComplete.Invoke(go, sceneImporter.ConfigJson, e);
		//});
	}

	/*
	public static async Task<GLTFSceneImporter> LoadI(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions);
		sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
		await sceneImporter.LoadSceneAsync();
		return sceneImporter;
	}

	

	public static async Task<Result> Load(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		using (var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync();
			Result result = new Result()
			{
				model = sceneImporter.CreatedObject,
				configJson = sceneImporter.ConfigJson
			};
			return result;
		}
	}

	public static async Task Load(string absoluteStreamingPath, Action<GameObject, string, ExceptionDispatchInfo> onLoadComplete)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		using (var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync(-1, true, (go, e) =>
			{
				onLoadComplete.Invoke(go, sceneImporter.ConfigJson, e);
			});
		}
	}

	public static async Task<Result> LoadStream(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.UnityWebRequestLoader(directoryPath);

		using (var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync();
			var result = new Result()
			{
				model = sceneImporter.CreatedObject,
				configJson = sceneImporter.ConfigJson
			};
			return result;
		}
	}

	public static async Task LoadStream(string fileName, byte[] data, Action<GameObject, string, ExceptionDispatchInfo> onLoadComplete)
	{
		//string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new VirtualStreamLoader() { data = data };

		using (var sceneImporter = new ModelImporter(fileName, importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync(-1, true, (go, e) =>
			{
				onLoadComplete.Invoke(go, sceneImporter.ConfigJson, e);
			});
		}
	}

	public static async Task<Result> LoadHttpStream(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.WebRequestLoader(directoryPath);

		using (var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync();
			var result = new Result()
			{
				model = sceneImporter.CreatedObject,
				configJson = sceneImporter.ConfigJson
			};
			return result;
		}
	}

	public static async Task LoadHttpStream(string absoluteStreamingPath, Action<GameObject, string, ExceptionDispatchInfo> onLoadComplete)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.WebRequestLoader(directoryPath);

		using (var sceneImporter = new ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync(-1, true, (go, e) =>
			{
				onLoadComplete.Invoke(go, sceneImporter.ConfigJson, e);
			});
		}
	}
	*/
}
