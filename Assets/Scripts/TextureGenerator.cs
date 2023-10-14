using UnityEngine;

public class TextureGenerator
{
    public Texture2D GenerateTexture(Color[] colorArr, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixels(colorArr);
        texture.Apply();

        return texture;
    }
}
