using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Map
{
    public List<SerializedTile> Tiles;
    public string Name;

    public Map(List<Tile> tiles, string name)
    {
        Name = name;
        Tiles = new List<SerializedTile>();
        foreach (var tile in tiles)
        {
            Tiles.Add(new SerializedTile(tile));
        }
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
