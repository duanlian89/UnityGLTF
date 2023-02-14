using UnityEngine;

public class TextureUtil
{
	// https://blog.csdn.net/weixin_42358083/article/details/126723445
	public static Texture2D ResizeTexture(Texture2D source, int width, int height)
	{
		if (source != null)
		{
			RenderTexture renderTex = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
			Graphics.Blit(source, renderTex);

			Texture2D resizedTexture = new Texture2D(width, height, source.format, false);
			resizedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			resizedTexture.Apply();

			RenderTexture.ReleaseTemporary(renderTex);
			return resizedTexture;
		}
		else
		{
			return null;
		}
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
