using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public static void Apply(Unit unit, string targetType)
    {
        switch (targetType)
        {
            case "Ranged":
                break;
            case "Spear":
                break;
            default:
                unit.GetTargets += Melee;
                break;
        }
    }

    public static List<Unit> Melee(Unit unit, bool targetAllies = false, bool targetEnemies = true, bool getAllInRange = false)
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
        if (getAllInRange)
        {
            foreach (var tile in ClickManager.MovableTiles)
            {
                foreach (var neighbour in tile.Neighbours)
                {
                    if (neighbour.Unit != null)
                    {
                        if (neighbour.Unit.Team != unit.Team && targetEnemies) result.Add(neighbour.Unit);
                        if (neighbour.Unit.Team == unit.Team && targetAllies) result.Add(neighbour.Unit);
                    }
                }
            }
        }
        return result;
    }
}
