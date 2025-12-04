using System.Linq;
using UnityEngine;

public static class SpriteUtil
{
    public static Sprite GetSpriteFromTexture(this string path, string spriteName)
    {
        var sprites = Resources.LoadAll<Sprite>(path);
        return sprites.FirstOrDefault(s => s.name == spriteName);
    }
}