using System.Linq;
using UnityEngine;

public static class SpriteUtil
{
    public static Sprite GetSpriteFromTexture(string texturePath, string spriteName)
    {
        var sprites = Resources.LoadAll<Sprite>(texturePath);
        return sprites.FirstOrDefault(s => s.name == spriteName);
    }
}