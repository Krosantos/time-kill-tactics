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
		liminal.AbilityOne = liminal.AbilityTwo = "";
		liminal.MoveType = "Climb";
        liminal.GetTargets = liminal.OnMove = liminal.Attack = liminal.OnTurnStart = liminal.OnTurnEnd = liminal.OnDeath = liminal.OnAttacked = liminal.Tags = new string[1];
        Units.Add(liminal);

        var blap = liminal;
		blap.Name = "Decaying Thrall";
		blap.SpriteReference = "NEC_DecayingThrall";
		blap.MoveType = "Standard";
		blap.Strength = blap.Health = 1;
		blap.Speed = 2;
		blap.X = 3;
		blap.Y = 2;
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
    public string[] GetTargets, OnMove, Attack, OnTurnStart, OnTurnEnd, OnDeath, OnAttacked, Tags;
    public string Name, SpriteReference, MoveType, AbilityOne, AbilityTwo;

    public override string ToString(){
		return JsonUtility.ToJson(this);
	}
}