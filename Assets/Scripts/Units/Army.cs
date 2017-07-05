using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Army {

	public string Name;
	public List<SerializedUnit> Units;
	public List<String> Spells;

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
        liminal.Attacks[0] = "Standard";
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
		var result = JsonUtility.ToJson(this, true);
		return result;
	}
}