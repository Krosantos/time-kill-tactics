using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlayerSpell : ITurnable
{
    public string SpriteReference, Name, Text;
    public int Cost, Cooldown, Ammo, MaxAmmo, CooldownCounter, Index, Targets;
    public bool HasCost, HasCooldown, HasAmmo;
    public Player Player;
    public abstract void TurnStart();
    public abstract void TurnEnd();
    public abstract void Cast(List<Tile> targets);
    public abstract List<Tile> GetValidTargets();

    public bool IsDisabled()
    {
        if (Player == null) return true;
        if (!Player.IsActive) return true;
        if (HasCost && Player.Mana < Cost) return true;
        if (HasCooldown && CooldownCounter != 0) return true;
        if (HasAmmo && Ammo <= 0) return true;
        return false;
    }

    public static PlayerSpell ConstructSpell(string input, Player player)
    {
        switch (input)
        {
            case "Heal":
                return new SpellHeal(player);
            case "Kill":
                return new SpellKill(player);
            default:
                return null;
        }
    }
}