using System.Collections.Generic;
using UnityEngine;

public class MapSerializer : MonoBehaviour {

    public GameObject TilePrefab, FillerPrefab;
    public SpriteDict[] TileDict, FillerDict; 
    public TextAsset Input;

    public void Awake()
    {
        DeserializeMap(Input.text);
    }

    public static string SerializeMap(List<Tile> tileList)
    {
        var result = "";
        foreach(var tile in tileList)
        {
            result += JsonUtility.ToJson(tile)+";";
        }
        result = result.Substring(0, result.Length - 1);
        return result;
    }

    public void DeserializeMap(string serializedMap)
    {
        var split = serializedMap.Split(';');
        Tile.AllTiles = new List<Tile>();
        foreach(var raw in split)
        {
            var prefab = Instantiate(TilePrefab, new Vector3(), Quaternion.identity);
            var tile = prefab.GetComponent<Tile>();
            Debug.Log(raw);
            JsonUtility.FromJsonOverwrite(raw, tile);
            Tile.AllTiles.Add(tile);
            tile.UpdateEditorSprite(FillerPrefab, TileDict, FillerDict);
        }
    }
}