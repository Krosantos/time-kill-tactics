using UnityEngine;

public static class MiscExtensions {

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
}
