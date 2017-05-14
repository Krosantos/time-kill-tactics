using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Move {
	public static void Standard(Unit unit){
        var moveableTiles = unit.GetMovableTiles();
        foreach(var tile in moveableTiles)
        {
            tile.GetComponent<SpriteRenderer>().color = Color.blue;
        }
	}
	public static void Climb(Unit unit){

	}
	public static void Flying(Unit unit){
		
	}
	public static void Teleport(Unit unit){
		
	}
}
