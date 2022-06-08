using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKUnityGLTF
{
	public class Utils
	{
		public static UnityEngine.Object FindObjectFromInstanceID(int iid)
		{
			return (UnityEngine.Object)typeof(UnityEngine.Object)
					.GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
					.Invoke(null, new object[] { iid });

		}

		public static bool DoesObjectWithInstanceIDExist(int iid)
		{
			return (bool)typeof(UnityEngine.Object)
					.GetMethod("DoesObjectWithInstanceIDExist", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
					.Invoke(null, new object[] { iid });

		}

		public static UnityEngine.Object ForceLoadFromInstanceID(int iid)
		{
			return (UnityEngine.Object)typeof(UnityEngine.Object)
					.GetMethod("ForceLoadFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
					.Invoke(null, new object[] { iid });

		}
	}
}
