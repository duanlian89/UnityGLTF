using GLTF.Extensions;
using GLTF.Schema;
using Newtonsoft.Json.Linq;

namespace UnityGLTF
{
	public class XXXXComponentExtension2 : IComponentExtension
	{
		#region property
		public float _Cutoff = 0.0f;
		public static readonly float _Cutoff_Default = 0.5f;
		#endregion

		public XXXXComponentExtension2()
		{
		}

		//TODO:[export] 记录其他属性值
		public XXXXComponentExtension2(XXXX2 x)
		{
			_Cutoff = x._Cutoff;
		}

		public IExtension Clone(GLTFRoot root)
		{
			return new XXXXComponentExtension2();
		}

		//TODO: [export] 将其他有变化的属性值序列化到json
		public JProperty Serialize()
		{
			JObject obj = new JObject();

			if (_Cutoff != _Cutoff_Default)
			{
				obj.Add(new JProperty("_Cutoff", _Cutoff));
			}
			return new JProperty(XXXXComponentExtensionFactory2.Extension_Name, obj);
		}

		//TODO:[import] 读取导出时，被序列化的、有变化的那些属性
		public void Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			JToken token = extensionToken.Value[XXXXComponentExtensionFactory2._Cutoff];
			_Cutoff = token != null ? (float)token.DeserializeAsDouble() : _Cutoff_Default;
		}

		//TODO:[import] 导入时、构建节点时，设置脚本的属性值
		public void SetComponentParam(UnityEngine.Component com)
		{
			(com as XXXX2)._Cutoff = _Cutoff;
		}
	}
}
