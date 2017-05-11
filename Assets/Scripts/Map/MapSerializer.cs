using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapSerializer : MonoBehaviour {

    public GameObject TilePrefab;
    public TextAsset Input;

    public static string SerializeMap(List<Tile> tileList)
    {
        var result = "";
        foreach(var tile in tileList)
        {
            result += JsonUtility.ToJson(tile)+",";
        }
        result = result.Substring(0, result.Length - 1);
        return result;
    }

    public static void DeserializeMap(string serializedMap)
    {
        var split = serializedMap.Split(',');
        Tile.AllTiles = new List<Tile>();
        foreach(var raw in split)
        {
            var tile = JsonUtility.FromJson<Tile>(raw);
            Tile.AllTiles.Add(tile);
        }
    }
}