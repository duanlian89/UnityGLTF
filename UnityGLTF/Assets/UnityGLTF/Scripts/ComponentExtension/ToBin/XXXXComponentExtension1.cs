using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Extensions;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

namespace UnityGLTF
{
	public class XXXXComponentExtension1 : IByteComponentExtension
	{
		ModelImporter importer;
		ModelExporter exporter;
		#region property
		public float _Cutoff = 0.0f;
		public static readonly float _Cutoff_Default = 0.5f;
		#endregion

		public BufferViewId BufferView;

		public XXXXComponentExtension1()
		{
		}

		public XXXXComponentExtension1(GLTFSceneImporter importer)
		{
			this.importer = importer as ModelImporter;
		}

		public XXXXComponentExtension1(GLTFSceneExporter exporter, XXXX component)
		{
			this.exporter = exporter as ModelExporter;

			//TODO:[export]记录属性值
			_Cutoff = component._Cutoff;
		}

		public IExtension Clone(GLTFRoot root)
		{
			return new XXXXComponentExtension1();
		}

		//TODO:[export] 将属性值转换成byte[],以便存储。
		public byte[] SerializeToByte()
		{
			JObject obj = new JObject();

			if (_Cutoff != _Cutoff_Default)
			{
				obj.Add(new JProperty("_Cutoff", _Cutoff));
			}
			return System.Text.UTF8Encoding.UTF8.GetBytes(obj.ToString());
		}

		//[export] 序列化到 bin
		public JProperty Serialize()
		{
			JObject obj = new JObject();

			if (_Cutoff != _Cutoff_Default)
			{
				obj.Add(new JProperty("bufferView", BufferView.Id));
			}
			return new JProperty(XXXXComponentExtensionFactory.Extension_Name, obj);
		}

		//[import]
		public void Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			JToken token = extensionToken.Value["bufferView"];

			BufferView = new BufferViewId()
			{
				Id = token.DeserializeAsInt(),
				Root = root,
			};
		}

		//[import]给脚本设置属性值
		public void SetComponentParam(Component com)
		{
			BufferView bufferView = BufferView.Root.BufferViews[BufferView.Id];

			var data = new byte[bufferView.ByteLength];

			UnityGLTF.Cache.BufferCacheData bufferContents = importer.AssetCache.BufferCache[BufferView.Value.Buffer.Id];
			bufferContents.Stream.Position = bufferView.ByteOffset + bufferContents.ChunkOffset;
			bufferContents.Stream.Read(data, 0, data.Length);

			string s = System.Text.UTF8Encoding.UTF8.GetString(data);

			var setting = new JsonLoadSettings()
			{
				CommentHandling = CommentHandling.Ignore,
				LineInfoHandling = LineInfoHandling.Ignore,
			};

			JObject jobj = JObject.Parse(s, setting);

			//TODO:给脚本设置其他属性
			JToken token = jobj[XXXXComponentExtensionFactory1._Cutoff];
			_Cutoff = token != null ? (float)token.DeserializeAsDouble() : XXXXComponentExtension1._Cutoff_Default;
			(com as XXXX)._Cutoff = _Cutoff;
		}
	}
}
