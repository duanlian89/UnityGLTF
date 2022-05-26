using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using System.Threading.Tasks;
using UnityGLTF;

public class ModelLoader
{
	public static async Task<GameObject> Load(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		using (var sceneImporter = new UnityGLTF.ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync();
			GameObject go = sceneImporter.CreatedObject;
			return go;
		}
	}

	public static async Task Load(string absoluteStreamingPath, Action<GameObject, ExceptionDispatchInfo> onLoadComplete)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.FileLoader(directoryPath);

		using (var sceneImporter = new UnityGLTF.ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync(-1, true, onLoadComplete);
			GameObject go = sceneImporter.CreatedObject;
		}
	}

	public static async Task<GameObject> LoadStream(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.UnityWebRequestLoader(directoryPath);

		using (var sceneImporter = new UnityGLTF.ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync();
			GameObject go = sceneImporter.CreatedObject;
			return go;
		}
	}

	public static async Task LoadStream(string absoluteStreamingPath, Action<GameObject, ExceptionDispatchInfo> onLoadComplete)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.UnityWebRequestLoader(directoryPath);

		using (var sceneImporter = new UnityGLTF.ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync(-1, true, onLoadComplete);
			GameObject go = sceneImporter.CreatedObject;
		}
	}

	public static async Task<GameObject> LoadHttpStream(string absoluteStreamingPath)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.UnityWebRequestLoader(directoryPath);

		using (var sceneImporter = new UnityGLTF.ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync();
			GameObject go = sceneImporter.CreatedObject;
			return go;
		}
	}

	public static async Task LoadHttpStream(string absoluteStreamingPath, Action<GameObject, ExceptionDispatchInfo> onLoadComplete)
	{
		string directoryPath = URIHelper.GetDirectoryName(absoluteStreamingPath);

		UnityGLTF.ImportOptions importOptions = new UnityGLTF.ImportOptions();
		importOptions.DataLoader = new UnityGLTF.Loader.UnityWebRequestLoader(directoryPath);

		using (var sceneImporter = new UnityGLTF.ModelImporter(System.IO.Path.GetFileName(absoluteStreamingPath), importOptions))
		{
			sceneImporter.Collider = UnityGLTF.GLTFSceneImporter.ColliderType.None;
			await sceneImporter.LoadSceneAsync();
			GameObject go = sceneImporter.CreatedObject;
		}
	}
}
