using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public static List<Unit> Melee(Unit unit, bool targetAllies = false, bool targetEnemies = true)
    {
        var result = new List<Unit>();
        foreach(var neighbour in unit.Tile.Neighbours)
        {
            if(neighbour.Unit != null)
            {
                if (neighbour.Unit.Team != unit.Team && targetEnemies) result.Add(neighbour.Unit);
                if (neighbour.Unit.Team == unit.Team && targetAllies) result.Add(neighbour.Unit);
            }
        }
        return result;
    }
}
