using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Army {

	public string Name;
	public List<SerializedUnit> Units;

	public Army(){
		Units = new List<SerializedUnit>();
        var liminal = new SerializedUnit()
        {
            X = 2,
            Y = 2,
            Health = 4,
            Speed = 2,
            Strength = 2,
            Name = "Venerated Necrolith",
            SpriteReference = "NEC_VeneratedNecrolith"
        };
        liminal.AbilityOne = liminal.AbilityTwo = "";
		liminal.MoveType = "Climb";
        liminal.TargetType = "Melee";
        liminal.OnMoves = liminal.Attacks = liminal.OnTurnStarts = liminal.OnTurnEnds = liminal.OnDeaths = liminal.OnKills = liminal.OnAttackeds = liminal.Tags = new string[1];
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