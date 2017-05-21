using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Army {

	public string Name;
	public List<SerializedUnit> Units;

	public Army(){
		Units = new List<SerializedUnit>();
        var liminal = new SerializedUnit();
        liminal.X = 2;
        liminal.Y = 2;
        liminal.Health = 4;
        liminal.Speed = 2;
        liminal.Strength = 2;
        liminal.Name = "Venerated Necrolith";
        liminal.SpriteReference = "NEC_VeneratedNecrolith";
        liminal.GetMoves = liminal.GetTargets = liminal.Move = liminal.Attack = liminal.OnTurnStart = liminal.OnTurnEnd = liminal.OnDeath = liminal.OnAttacked = liminal.AbilityOne = liminal.AbilityTwo = liminal.Tags = new string[1];
        Units.Add(liminal);

        var blap = liminal;
        Units.Add(blap);
	}

	public override string ToString(){
		foreach(var unit in Units){
			Debug.Log(JsonUtility.ToJson(unit));
		}
		var result = JsonUtility.ToJson(this, true);
		Debug.Log(result);
		return result;
	}
}

[Serializable]
public struct SerializedUnit{
	public int X, Y, Health, Strength, Speed;
    public string[] GetMoves, GetTargets, Move, Attack, OnTurnStart, OnTurnEnd, OnDeath, OnAttacked, AbilityOne, AbilityTwo, Tags;
    public string Name, SpriteReference;

    public override string ToString(){
		return JsonUtility.ToJson(this);
	}
}