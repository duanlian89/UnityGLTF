using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Threading;

namespace DIYAvatar
{
	public class DIYAssetBundleManager : UnityGLTF.Loader.IDataLoader
	{
		public async Task<Stream> LoadStreamAsync(string relativeFilePath)
		{
			return await StartRequest("", "", false);
		}

		private async Task<Stream> StartRequest(string abPath, string abName, bool isPersistend)
		{
			int count = 10;
			while (count > 0)
			{
				count--;
				using (UnityWebRequest request = UnityWebRequest.Get(abPath))
				{
					request.timeout = 100;
					await Task.Run(() => { request.SendWebRequest(); });
					if (request.result == UnityWebRequest.Result.Success)
					{
						var results = request.downloadHandler.data;
						var stream = new MemoryStream(results, 0, results.Length, false, true);
						return stream;
					}
				}
			}
			return null;
		}
	}
}

