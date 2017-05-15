using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// This combines the existing EventSystem (which handles stuff like clicking) with turns, and selecting multiple objects.
public class TurnManager : EventSystem {
    public List<Tile> MovableTiles;
    public List<Unit> AttackableUnits;
    public static TurnManager Active;

    public Tile SelectedTile
    {
        get
        {
            return currentSelectedGameObject.GetComponent<Tile>();
        }
    }
    public Unit SelectedUnit
    {
        get
        {
            return currentSelectedGameObject.GetComponent<Unit>();
        }
    }

    new public void Awake()
    {
        base.Awake();
        Active = this;
    }

    public void ColorTiles()
    {
        foreach(var tile in MovableTiles)
        {
            tile.Color = new Color(0.33f,0.33f,0.33f);
        }
        foreach(var unit in AttackableUnits)
        {
            unit.Tile.Color = new Color(0.45f, 0.27f, 0.27f);
        }
    }

    public void Clear()
    {
        foreach (var tile in MovableTiles)
        {
            tile.Color = Color.white;
        }
        foreach (var unit in AttackableUnits)
        {
            unit.Tile.Color = Color.white;
        }
        MovableTiles = new List<Tile>();
        AttackableUnits = new List<Unit>();
    }

    public bool EligibleToMoveTo(Tile tile)
    {
        Debug.Log("MovableTiles Count: " + MovableTiles.Count);
        foreach(var t in MovableTiles)
        {
            //Debug.Log(tile.name + " == " + t.name + "?");
        }
        return MovableTiles.Contains(tile);
    }

    public bool EligibleToAttack(Unit unit)
    {
        return AttackableUnits.Contains(unit);
    }
}
