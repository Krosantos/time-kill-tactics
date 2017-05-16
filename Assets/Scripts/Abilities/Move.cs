using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move {

    public abstract List<Tile> Target(Unit unit);
    public abstract void Execute(Unit unit, Tile tile);

    public virtual void Apply(Unit unit)
    {
        unit.GetMoves += Target;
        unit.Move += Execute;
    }

	public static List<Tile> Flying(Unit unit){
        return unit.GetMovableTiles(99, true);
	}
	public static List<Tile> Teleport(Unit unit){
        return unit.GetMovableTiles(99, true, true);
	}
}

public class MoveStandard : Move
{
    public override List<Tile> Target(Unit unit)
    {
        return unit.GetMovableTiles();
    }

    public override void Execute(Unit unit, Tile tile)
    {
        var path = unit.Tile.AStarPath(tile, unit);
        // Start Animation
        // For each tile in path, move unit to that tile.
        unit.HasMoved = true;
        unit.Tile.Unit = null;
        unit.Tile = tile;
        unit.Tile.Unit = unit;
        unit.transform.position = unit.GetPosition();
        // End Animation
    }
}

public class Climb : Move
{
    public override List<Tile> Target(Unit unit)
    {
        return unit.GetMovableTiles(99);
    }

    public override void Execute(Unit unit, Tile tile)
    {
        var path = unit.Tile.AStarPath(tile, unit, 99);
        // Start Animation
        // For each tile in path, move unit to that tile.
        unit.HasMoved = true;
        unit.Tile.Unit = null;
        unit.Tile = tile;
        unit.Tile.Unit = unit;
        unit.transform.position = unit.GetPosition();
        // End Animation
    }
}