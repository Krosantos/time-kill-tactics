using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapSerializer : MonoBehaviour
{

    public GameObject TilePrefab, FillerPrefab;
    public TextAsset MapFile;
    public SpriteDict[] TileDict, FillerDict;

    public void Awake()
    {
        if (MapFile == null) return;
        var serializedMap = JsonUtility.FromJson<SerializedMap>(MapFile.text);
        DeserializeMap(serializedMap);
    }

    public static SerializedMap SerializeMap(List<Tile> tileList, string name)
    {
        var result = new SerializedMap(tileList, name);
        Debug.Log(result.ToString());
        return result;
    }

    public void DeserializeMap(SerializedMap serializedMap)
    {
        Tile.AllTiles = new List<Tile>();
        foreach (var raw in serializedMap.Tiles)
        {
            var prefab = Instantiate(TilePrefab, new Vector3(), Quaternion.identity);
            var tile = prefab.GetComponent<Tile>();
            JsonUtility.FromJsonOverwrite(raw, tile);
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
}

[Serializable]
public struct SerializedMap
{
    public List<string> Tiles;
    public string Name;

    public SerializedMap(List<Tile> tiles, string name)
    {
        Name = name;
        Tiles = new List<string>();
        foreach (var tile in tiles)
        {
            Tiles.Add(JsonUtility.ToJson(tile));
        }
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}