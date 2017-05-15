using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Move {
    
    public static List<Tile> Standard(Unit unit){
        return unit.GetMovableTiles();
	}
	public static List<Tile> Climb(Unit unit){
        return unit.GetMovableTiles(99);
	}
	public static List<Tile> Flying(Unit unit){
        return unit.GetMovableTiles(99, true);
	}
	public static List<Tile> Teleport(Unit unit){
        return unit.GetMovableTiles(99, true, true);
	}
}
