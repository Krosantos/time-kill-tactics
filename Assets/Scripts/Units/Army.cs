using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Army {

	public string Name;
	public List<SerializedUnit> Units;

	public Army(){
		Units = new List<SerializedUnit>();
	}

	public override string ToString(){
		foreach(var unit in Units){
			Debug.Log(JsonUtility.ToJson(unit));
		}
		var result = JsonUtility.ToJson(this);
		Debug.Log(result);
		return result;
	}
}

[Serializable]
public struct SerializedUnit{
	Unit Unit;
	int X, Y;

	public SerializedUnit(Unit unit){
		Unit = unit;
		var tile = unit.Tile;
		X = tile.X;
		Y = tile.Y;
	}

	public override string ToString(){
		return JsonUtility.ToJson(this);
	}
}