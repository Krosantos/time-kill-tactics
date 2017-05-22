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
        liminal.OnMoves = liminal.Attacks = liminal.OnTurnStarts = liminal.OnTurnEnds = liminal.OnDeaths = liminal.OnAttackeds = liminal.Tags = new string[1];
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
    public string[] OnMoves, Attacks, OnTurnStarts, OnTurnEnds, OnDeaths, OnAttackeds, Tags;
    public string Name, SpriteReference, MoveType, TargetType, AbilityOne, AbilityTwo;

    public override string ToString(){
		return JsonUtility.ToJson(this);
	}

    public void OverwriteUnit(Unit unit)
    {
        // Place the unit where it should be. (Maybe)
        if(X < Tile.TileMap.GetLength(0) && Y < Tile.TileMap.GetLength(1))
        {
            var possibleTile = Tile.TileMap[X, Y];
            if(possibleTile.Unit == null)
            {
                possibleTile.Unit = unit;
                unit.Tile = possibleTile;
            }
        }

        // Add its movement.
        switch (MoveType)
        {
            case "Climb":
                new Climb().Apply(unit);
                break;
            case "Teleport":
                new Teleport().Apply(unit);
                break;
            case "Flying":
                new Flying().Apply(unit);
                break;
            default:
                new MoveStandard().Apply(unit);
                break;
        }

        // Add attack targeting, and abilities
        Target.Apply(unit, TargetType);
        Abilities.Apply(unit, AbilityOne);
        Abilities.Apply(unit, AbilityTwo);

        // Add all the OnX abilities
        foreach(var ability in OnMoves)
        {
            OnMove.Apply(unit, ability);
        }
        foreach (var ability in Attacks)
        {
            Attack.Apply(unit, ability);
        }
        foreach (var ability in OnTurnStarts)
        {
            OnTurn.Apply(unit, ability, true);
        }
        foreach (var ability in OnTurnEnds)
        {
            OnTurn.Apply(unit, ability, false);
        }
        foreach (var ability in OnDeaths)
        {
            OnDeath.Apply(unit, ability);
        }
        foreach (var ability in OnAttackeds)
        {
            OnAttacked.Apply(unit, ability);
        }

        // Plink on the individual stats.
        unit.Health = unit.MaxHealth = Health;
        unit.Strength = unit.BaseStrength = Strength;
        unit.Speed = unit.BaseSpeed = Speed;

        // Add tags. Sprite and facing will be handled by the UnitBuilder, along with team.
        unit.Tags = new List<string>();
        foreach(var tag in Tags)
        {
            unit.Tags.Add(tag);
        }
    }
}