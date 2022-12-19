using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

namespace CKUnityGLTF
{
	public class VirtualStreamLoader : UnityGLTF.Loader.IDataLoader
	{
		public byte[] data;

		public async Task<Stream> LoadStreamAsync(string relativeFilePath)
		{
			var stream = new MemoryStream(data, 0, data.Length, false, true);
			await Task.Yield();
			return stream;
		}
	}
}
