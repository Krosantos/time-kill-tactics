using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// This combines the existing EventSystem (which handles stuff like clicking) with turns, and selecting multiple objects.
public class TurnManager : EventSystem {
    public static List<Tile> MovableTiles;
    public static List<Unit> AttackableUnits;
    public static TurnManager Active;

    public static Tile SelectedTile;
    public static Unit SelectedUnit;

    new public void Awake()
    {
        base.Awake();
        Active = this;
        MovableTiles = new List<Tile>();
        AttackableUnits = new List<Unit>();
    }

    public static void ColorTiles()
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

    public static void Clear()
    {
        foreach (var tile in MovableTiles)
        {
            tile.Color = Color.white;
        }
        foreach (var unit in AttackableUnits)
        {
            unit.Tile.Color = Color.white;
        }
        SelectedTile = null;
        SelectedUnit = null;
        MovableTiles = new List<Tile>();
        AttackableUnits = new List<Unit>();
        Active.SetSelectedGameObject(null);
    }

    public static bool EligibleToMoveTo(Tile tile)
    {
        return MovableTiles.Contains(tile);
    }

    public static bool EligibleToAttack(Unit unit)
    {
        return AttackableUnits.Contains(unit);
    }
}
