using System;
using System.Text;
using UnityEngine;

public static class MiscExtensions
{

    public static Sprite GetSpriteFromDict(this Terrain terrain, SpriteDict[] dict)
    {
        foreach (var pair in dict)
        {
            if (pair.Name == terrain.ToString())
            {
                return pair.Sprite;
            }
        }
        return dict[0].Sprite;
    }

    public static Byte[] Encode(this string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static string Decode(this byte[] input)
    {
        return Encoding.UTF8.GetString(input);
    }

    public static void DestroyChildren(this Transform self)
    {
        var children = self.childCount;
        for (var x = 0; x < children; x++)
        {
            var child = self.GetChild(x);
            if(child.childCount > 0) DestroyChildren(child);
            GameObject.Destroy(child.gameObject);
        }
    }
}
