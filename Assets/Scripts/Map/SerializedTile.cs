using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedTile {

    public int X, Y, Height, Terrain;
    public bool DontDraw, Passable, Blocking;

	public SerializedTile(Tile tile){
        X = tile.X;
        Y = tile.Y;
        Height = tile.Height;
        Terrain = (int)tile.Terrain;
        DontDraw = tile.DontDraw;
        Passable = tile.Passable;
        Blocking = tile.Blocking;
    }

	public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }

	public void OverwriteTile(Tile tile){
        tile.X = X;
        tile.Y = Y;
        tile.Height = Height;
        tile.DontDraw = DontDraw;
        tile.Passable = Passable;
        tile.Blocking = Blocking;
        tile.Terrain = (Terrain)Terrain;
    }
}
