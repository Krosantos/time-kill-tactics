using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// This combines the existing EventSystem (which handles stuff like clicking) with turns, and selecting multiple objects.
// This is the hotseat/single-player version. We'll make a networked version later.
public class ClickManager : EventSystem {
    public static List<Tile> MovableTiles;
    public static List<Unit> AttackableUnits;
    public static List<Unit> UnitsInRange;
    public static List<Tile> SpellableTiles, SpellTargets;
    public static ClickManager Active;
    public static Tile SelectedTile;
    public static Unit SelectedUnit;
    public static PlayerSpell SelectedSpell;

    new public void Awake()
    {
        base.Awake();
        Active = this;
        MovableTiles = new List<Tile>();
        AttackableUnits = new List<Unit>();
        UnitsInRange = new List<Unit>();
        SpellableTiles = new List<Tile>();
        SpellTargets = new List<Tile>();
    }

    public static void ColorTiles()
    {
        foreach(var tile in MovableTiles)
        {
            tile.ToggleGrey(true);
            tile.Color = new Color(0.5f,0.5f,0.5f);
        }
        foreach(var unit in AttackableUnits)
        {
            unit.Tile.ToggleGrey(true);
            unit.Tile.Color = new Color(0.45f, 0.27f, 0.27f);
        }
        foreach(var unit in UnitsInRange)
        {
            unit.Tile.ToggleGrey(true);
            unit.Tile.Color = new Color(0.45f, 0.27f, 0.27f);
        }
        foreach(var tile in SpellableTiles)
        {
            tile.ToggleGrey(true);
            tile.Color = new Color(1f,0.952f,0.682f);
        }
        foreach(var tile in SpellTargets)
        {
            tile.ToggleGrey(true);
            tile.Color = new Color(0.952f,1f,0.682f);
        }
    }

    public static void Clear()
    {
        foreach (var tile in MovableTiles)
        {
            tile.ToggleGrey(false);
            tile.Color = Color.white;
        }
        foreach (var unit in AttackableUnits)
        {
            unit.Tile.ToggleGrey(false);
            unit.Tile.Color = Color.white;
        }
        foreach (var unit in UnitsInRange)
        {
            unit.Tile.ToggleGrey(false);
            unit.Tile.Color = Color.white;
        }
        foreach(var tile in SpellableTiles)
        {
            tile.ToggleGrey(false);
            tile.Color = Color.white;
        }
        foreach(var tile in SpellTargets)
        {
            tile.ToggleGrey(false);
            tile.Color = Color.white;
        }
        SelectedTile = null;
        SelectedUnit = null;
        SelectedSpell = null;
        MovableTiles = new List<Tile>();
        AttackableUnits = new List<Unit>();
        UnitsInRange = new List<Unit>();
        SpellableTiles = new List<Tile>();
        SpellTargets = new List<Tile>();
        if (!Active.alreadySelecting) Active.SetSelectedGameObject(null);
    }

    public static bool EligibleToMoveTo(Tile tile)
    {
        return SelectedUnit != null && MovableTiles.Contains(tile);
    }

    public static bool EligibleToAttack(Unit unit)
    {
        return SelectedUnit != null && AttackableUnits.Contains(unit);
    }

    public static bool EligibleToCast(Tile target)
    {
        return SelectedSpell != null && SpellableTiles.Contains(target) && !SpellTargets.Contains(target);
    }
}
