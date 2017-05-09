using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnGrid : MonoBehaviour {
    public string SavePath, MapName;
    public GameObject TilePrefab;
    public GameObject FillerPrefab;
    public int X,Y;

	public void Awake(){
		Tile.AllTiles = new List<Tile>();
		Tile.TileMap = new Tile[X+1,Y];
        Tile.Filler = FillerPrefab;
		for(var y = 0;y<Y;y++){

            var rowX = X;
            if (y % 2 == 0) rowX++;

			for(var x = 0;x<rowX;x++){
				var obj = Instantiate(TilePrefab, new Vector3(), Quaternion.identity);
				var tile = obj.GetComponent<Tile>();
				obj.name = x+","+y;
				obj.transform.parent = transform;
				Tile.AllTiles.Add(tile);
				Tile.TileMap[x,y] = tile;
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
            Debug.Log("Writing!");
            var serialized = MapSerializer.SerializeMap(Tile.AllTiles);
            File.WriteAllText(SavePath + "/" + MapName + ".txt", serialized);
        }
    }
}
