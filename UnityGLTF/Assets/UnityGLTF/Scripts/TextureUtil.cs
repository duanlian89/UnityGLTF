using System.Reflection;
using System;
using UnityEngine;

public class TextureUtil
{
	// https://blog.csdn.net/weixin_42358083/article/details/126723445
	public static Texture2D ResizeTexture(Texture2D source, int width, int height)
	{
		if (source != null)
		{
			bool sRGB = ActiveTextureColorSpace(source) == ColorSpace.Gamma;
			// 创建临时的RenderTexture
			RenderTexture renderTex = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.Default,
				!sRGB ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.sRGB);
			//RenderTextureReadWrite.Linear);

			// 复制source的纹理到RenderTexture里
			Graphics.Blit(source, renderTex);
			// 开启当前RenderTexture激活状态
			RenderTexture previous = RenderTexture.active;
			RenderTexture.active = renderTex;
			// 创建修改后的纹理，保持与源纹理相同压缩格式
			Texture2D resizedTexture = new Texture2D(width, height, source.format, false, !sRGB);
			// 读取像素到创建的纹理中
			resizedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			// 应用修改到GPU上
			resizedTexture.Apply();
			// 停止当前RenderTexture工作
			RenderTexture.active = previous;
			// 释放内存
			RenderTexture.ReleaseTemporary(renderTex);
			return resizedTexture;
		}
		else
		{
			return null;
		}
	}

	public static ColorSpace ActiveTextureColorSpace(Texture texture)
	{
		ColorSpace colorSpace = ColorSpace.Gamma;
		try
		{
			PropertyInfo propertyInfo = texture.GetType().GetProperty("activeTextureColorSpace", BindingFlags.Instance | BindingFlags.NonPublic);
			var obj = propertyInfo.GetValue(texture);//(texture.GetType().GetMember("activeTextureColorSpace", BindingFlags.NonPublic | BindingFlags.Instance)[0] as PropertyInfo).GetValue(texture);
			colorSpace = (ColorSpace)obj;
			Debug.Log(string.Format("Name:{0} ColorSpace:{1} ", texture.name, obj.ToString()));
		}
		catch (Exception e)
		{
			Debug.LogWarning(string.Format("ActiveTextureColorSpace {0} Found Exception: {1}", texture.name, e.Message));
		}
		return colorSpace;
	}


	public static void ScaleTexture(Texture2D src, Texture2D dst)
	{
		int width = dst.width;
		int height = dst.height;
		Color[] rpixels = dst.GetPixels(0);
		float incX = (1.0f / (float)width);
		float incY = (1.0f / (float)height);
		for (int px = 0, len = rpixels.Length; px < len; ++px)
		{
			rpixels[px] = src.GetPixelBilinear(incX * ((float)px % width), incY * ((float)Mathf.Floor(px / width)));
		}
		dst.SetPixels(rpixels, 0);
		dst.Apply();
	}
}
