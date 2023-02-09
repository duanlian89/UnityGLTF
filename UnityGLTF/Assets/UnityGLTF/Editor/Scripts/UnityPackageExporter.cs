using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UnityPackageExporter
{
#if UNITY_EDITOR
	[MenuItem("Tools/UnityPackageExporter/Export UnityGLTF")]
	private static void ExportLobby()
	{
		var path = EditorUtility.SaveFilePanel("Save unitypackage", "", "UnityGLTF", "unitypackage");
		if (path == "")
		{
			return;
		}

		string[] assetPaths = new string[]
		{
				"Assets/UnityGLTF/Editor/Scripts/PbrShaderGUI.cs",
				"Assets/UnityGLTF/Editor/UnityGLTFEditor.asmdef",
				"Assets/UnityGLTF/CKUnityGLTF.asmdef",
				"Assets/UnityGLTF/Runtime",
				"Assets/UnityGLTF/Scripts/ConfigJsonExtension",
				"Assets/UnityGLTF/Scripts/MaterialExtension",
				"Assets/UnityGLTF/Scripts/MeshFilterAndMeshColliderExtension",
				"Assets/UnityGLTF/Scripts/ClothesInfoJsonExtension",
				"Assets/UnityGLTF/Scripts/Server",
				"Assets/UnityGLTF/Scripts/IPropExtension.cs",
				"Assets/UnityGLTF/Scripts/ModelExporter.cs",
				"Assets/UnityGLTF/Scripts/ModelImporter.cs",
				"Assets/UnityGLTF/Scripts/ModelLoader.cs",
				"Assets/UnityGLTF/Scripts/ModelLoadSample.cs",
				"Assets/UnityGLTF/Scripts/MyGLTFRoot.cs",
				"Assets/UnityGLTF/Scripts/Utils.cs",
				"Assets/UnityGLTF/Scripts/VirtualStreamLoader.cs",
				"Assets/UnityGLTF/Scripts/TextureUtil.cs"
		};

		//var allPaths = AssetDatabase.GetDependencies(assetPaths);

		AssetDatabase.ExportPackage(assetPaths, path, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse /*| ExportPackageOptions.IncludeDependencies*/);
	}
#endif
}

