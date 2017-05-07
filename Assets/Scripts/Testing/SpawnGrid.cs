using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour {
	public GameObject TilePrefab;
	public int X,Y;

	public void Awake(){
		Tile.AllTiles = new List<Tile>();
		Tile.TileMap = new Tile[X,Y];
		for(var x = 0;x<X;x++){
			for(var y = 0;y<Y;y++){
				var obj = Instantiate(TilePrefab, getTransformCoords(x,y), Quaternion.identity);
				var tile = obj.GetComponent<Tile>();
				obj.name = x+","+y;
				obj.transform.parent = this.transform;
				Tile.AllTiles.Add(tile);
				Tile.TileMap[x,y] = tile;
				tile.X = x;
				tile.Y = y;
			}
		}
	}

	public Vector3 getTransformCoords(int x, int y){
		var resX = x*0.88f;
		var resY = y*0.75f;
		if(y%2 != 0){
			resX -= 0.44f;
		}
		return new Vector3(resX, resY, 0f);
	}
}
