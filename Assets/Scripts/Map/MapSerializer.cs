using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapSerializer : MonoBehaviour
{
    public static MapSerializer Active;
    public GameObject TilePrefab, FillerPrefab;
    public SpriteDict[] TileDict, FillerDict;

    public void Awake()
    {
        Active = this;
    }

    public static Map SerializeMap(List<Tile> tileList, string name)
    {

        var result = new Map(tileList, name);
        Debug.Log($"Writing {tileList.Count} tiles!");
        return result;
    }

    public void DeserializeMap(Map serializedMap)
    {
        Tile.AllTiles = new List<Tile>();
        foreach (var raw in serializedMap.Tiles)
        {
            var prefab = Instantiate(TilePrefab, new Vector3(), Quaternion.identity);
            var tile = prefab.GetComponent<Tile>();
            raw.OverwriteTile(tile);
            Tile.AllTiles.Add(tile);
            tile.UpdateEditorSprite(FillerPrefab, TileDict, FillerDict);
            prefab.name = tile.X + "," + tile.Y;
            prefab.transform.parent = transform;
        }

        int maxX = 0;
        int maxY = 0;

        CameraDrag.MinX = Tile.AllTiles.Min(t => t.transform.position.x);
        CameraDrag.MinY = Tile.AllTiles.Min(t => t.transform.position.y);
        CameraDrag.MaxX = Tile.AllTiles.Max(t => t.transform.position.x);
        CameraDrag.MaxY = Tile.AllTiles.Max(t => t.transform.position.y);

        foreach (var tile in Tile.AllTiles)
        {
            if (tile.X > maxX) maxX = tile.X;
            if (tile.Y > maxY) maxY = tile.Y;
        }
        Tile.TileMap = new Tile[maxX + 1, maxY + 1];
        foreach (var tile in Tile.AllTiles)
        {
            Tile.TileMap[tile.X, tile.Y] = tile;
        }
    }

    public void WipeMap()
    {
        if (Tile.AllTiles == null) return;
        foreach (var tile in Tile.AllTiles)
        {
            if (tile.Unit) tile.Unit.Tile = null;
        }
        transform.DestroyChildren();
    }
}