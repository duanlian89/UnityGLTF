using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKUnityGLTF;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

namespace CKUnityGLTF
{
	public class StandardMaterialExtensionFactory : MaterialExtensionFactory
	{
		public const string Extension_Name = "Standard";

		#region property name

		public const string _Color = "_Color";
		public const string _MainTex = "_MainTex";
		public const string _Cutoff = "_Cutoff";
		public const string _Glossiness = "_Glossiness";
		public const string _GlossMapScale = "_GlossMapScale";
		public const string _SmoothnessTextureChannel = "_SmoothnessTextureChannel";
		public const string _Metallic = "_Metallic";
		public const string _MetallicGlossMap = "_MetallicGlossMap";
		public const string _SpecularHighlights = "_SpecularHighlights";
		public const string _GlossyReflections = "_GlossyReflections";
		public const string _BumpScale = "_BumpScale";
		public const string _BumpMap = "_BumpMap";
		public const string _Parallax = "_Parallax";
		public const string _ParallaxMap = "_ParallaxMap";
		public const string _OcclusionStrength = "_OcclusionStrength";
		public const string _OcclusionMap = "_OcclusionMap";
		public const string _EmissionColor = "_EmissionColor";
		public const string _EmissionMap = "_EmissionMap";
		public const string _DetailMask = "_DetailMask";
		public const string _DetailAlbedoMap = "_DetailAlbedoMap";
		public const string _DetailNormalMapScale = "_DetailNormalMapScale";
		public const string _DetailNormalMap = "_DetailNormalMap";
		public const string _UVSec = "_UVSec";
		public const string _Mode = "_Mode";
		public const string _SrcBlend = "_SrcBlend";
		public const string _DstBlend = "_DstBlend";
		public const string _ZWrite = "_ZWrite";

		#endregion

		public StandardMaterialExtensionFactory()
		{
			ExtensionName = Extension_Name;

			FloatProperties = new string[] { _Cutoff, _Glossiness, _BumpScale , _GlossMapScale , _SmoothnessTextureChannel ,
			_Metallic, _SpecularHighlights,_GlossyReflections,_Parallax, _OcclusionStrength,
			_DetailNormalMapScale,_SrcBlend ,_DstBlend ,_ZWrite,_UVSec ,
			_Mode };

			ColorProperties = new string[] { _Color, _EmissionColor };

			TextureProperties = new string[] { _MainTex, _BumpMap ,
		_ParallaxMap, _OcclusionMap ,_EmissionMap,_DetailMask,_MetallicGlossMap,
		_DetailAlbedoMap,_DetailNormalMap };
		}

		// 从extensionToken读出属性，初始化 MaterialExtension
		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			StandardMaterialExtension ext = new StandardMaterialExtension();
			ext.Deserialize(root, extensionToken);
			return ext;
		}

		public override IExtension ConstructExtension()
		{
			StandardMaterialExtension ext = new StandardMaterialExtension();
			return ext;
		}
	}
}
