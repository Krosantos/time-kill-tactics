using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// This combines the existing EventSystem (which handles stuff like clicking) with turns, and selecting multiple objects.
// This is the hotseat/single-player version. We'll make a networked version later.
public class TurnManager : EventSystem {
    public static List<Tile> MovableTiles;
    public static List<Unit> AttackableUnits;
    public static List<Unit> UnitsInRange;
    public static List<Tile> SpellableTiles;
    public bool PlayerActive;
    public Player Player;
    public Player Enemy;
    public static TurnManager Active;
    public static Tile SelectedTile;
    public static Unit SelectedUnit;

    new public void Awake()
    {
        base.Awake();
        Active = this;
        MovableTiles = new List<Tile>();
        AttackableUnits = new List<Unit>();
        UnitsInRange = new List<Unit>();
        SpellableTiles = new List<Tile>();
    }

    public void EndTurn()
    {
        if (PlayerActive)
        {
            Player.TurnEnd();
            Enemy.TurnStart();
        }
        else
        {
            Enemy.TurnEnd();
            Player.TurnStart();
        }
        PlayerActive = !PlayerActive;        
    }

    public void CheckForVictory()
    {
        // ...I should probably do this server-side only.
        //Debug.Log(Player.Units.Count);
        //Debug.Log(Enemy.Units.Count);
        if(Player.Units.Count <= 0){
            Debug.Log("Enemy Wins!");
            Application.Quit();
        }
        if(Enemy.Units.Count <= 0){
            Debug.Log("Player Wins!");
            Application.Quit();
        }
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
        SelectedTile = null;
        SelectedUnit = null;
        MovableTiles = new List<Tile>();
        AttackableUnits = new List<Unit>();
        UnitsInRange = new List<Unit>();
        SpellableTiles = new List<Tile>();
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

    public static bool EligibleToCast(Tile target)
    {
        return SpellableTiles.Contains(target);
    }
}
