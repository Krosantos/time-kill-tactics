using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneEditor : MonoBehaviour
{
    public string SavePath, MapName, ArmyName;
    public GameObject TilePrefab;
    public GameObject FillerPrefab;
    public SpriteDict[] SpriteDictionary, FillerDictionary;
    public int X, Y;

    private List<Unit> AllUnits
    {
        get
        {
            var result = new List<Unit>();

            var gameObjs = GameObject.FindGameObjectsWithTag("Unit");
            foreach (var obj in gameObjs)
            {
                var unit = obj.GetComponent<Unit>();
                if (unit != null) result.Add(unit);
            }
            return result;
        }
    }

    public void Awake()
    {
        Tile.AllTiles = new List<Tile>();
        Tile.TileMap = new Tile[X + 1, Y];
        Tile.Filler = FillerPrefab;
        for (var y = 0; y < Y; y++)
        {

            var rowX = X;
            if (y % 2 == 0) rowX++;

            for (var x = 0; x < rowX; x++)
            {
                var obj = Instantiate(TilePrefab, new Vector3(), Quaternion.identity);
                var tile = obj.GetComponent<Tile>();
                obj.name = x + "," + y;
                obj.transform.parent = transform;
                Tile.AllTiles.Add(tile);
                Tile.TileMap[x, y] = tile;
                tile.Passable = true;
                tile.X = x;
                tile.Y = y;
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Writing Map!");
            var serialized = MapSerializer.SerializeMap(Tile.AllTiles, MapName);
            File.WriteAllText(SavePath + "/" + MapName + ".json", serialized.ToString());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Writing Units!");
            var army = new Army();
            File.WriteAllText(SavePath + "/" + ArmyName + ".json", army.ToString());
        }
        foreach (var tile in Tile.AllTiles)
        {
            tile.UpdateEditorSprite(FillerPrefab, SpriteDictionary, FillerDictionary);
        }
        foreach (var unit in AllUnits)
        {
            unit.SyncUi();
        }
    }
}
