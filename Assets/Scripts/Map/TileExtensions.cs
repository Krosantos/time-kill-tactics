using System;
using UnityEngine;

public static class TileExtensions
{
    public static void UpdateEditorSprite(this Tile tile, GameObject fillerPrefab, SpriteDict[] spriteDict, SpriteDict[] fillerDict)
    {
        tile.GetComponent<SpriteRenderer>().sortingOrder = tile.Height + tile.Y * -1;
        tile.transform.position = GetTransformFromCoords(tile.X, tile.Y, tile.Height);
        tile.DrawDown(fillerPrefab, fillerDict);
        tile.Sprite = tile.Terrain.GetSpriteFromDict(spriteDict);
    }

    private static void DrawDown(this Tile tile, GameObject fillerPrefab, SpriteDict[] fillerDict)
    {
        for (var x = 0; x < tile.transform.childCount; x++)
        {
            GameObject.Destroy(tile.transform.GetChild(x).gameObject);
        }
        if (tile.DontDraw) return;
        for (var z = tile.Height; z > 0; z--)
        {
            var filler = GameObject.Instantiate(fillerPrefab, GetTransformFromCoords(tile.X, tile.Y, z - 1), Quaternion.identity);
            filler.transform.parent = tile.transform;
            var renderer = filler.GetComponent<SpriteRenderer>();
            renderer.sortingOrder = (z - 1) + tile.Y * -1;
            renderer.sprite = tile.Terrain.GetSpriteFromDict(fillerDict);
        }
    }

    public static void ToggleGrey(this Tile tile, bool shouldGrey)
    {
        var renderer = tile.GetComponent<SpriteRenderer>();
        if (renderer == null) return;
        var toLoad = shouldGrey ? "MAT_GreyScale" : "MAT_Standard";
        renderer.material = Resources.Load<Material>(toLoad);
    }

    private static Vector3 GetTransformFromCoords(int x, int y, int z)
    {
        return new Vector3(x + (y % 2) * 0.5f, 0.75f * y + 0.35f * z);
    }
}

[Serializable]
public struct SpriteDict
{
    public string Name;
    public Sprite Sprite;
}

public enum Terrain
{
    Grass,
    Dirt,
    Rock,
    Water,
    Snow,
    Ice,
    Sand,
    Lava
}
