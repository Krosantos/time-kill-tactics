﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITurnable
{
    public int Team;
    public string Name;
    public List<ITurnable> TurnAssets;
    public bool IsEnemy, IsActive;
    public List<Unit> Units;
    public PlayerSpell[] Spells;
    public UnitBuilder UnitBuilder;
    public GameObject UnitPrefab;
    public int Mana, MaxMana;
    public static Dictionary<int, Player> PlayersByTeam;
    public static Player Me, Enemy;

    public void Awake()
    {
        if(PlayersByTeam == null) PlayersByTeam = new Dictionary<int, Player>();
        UnitBuilder = new UnitBuilder(this);
        if(IsEnemy) Enemy = this;
        else Me = this;
        TurnAssets = new List<ITurnable>();
        Units = new List<Unit>();
        Spells = new PlayerSpell[0];
    }

    public void LoadArmy(Army army)
    {
        // This little block has to change once we go networked.
        foreach (var unitString in army.Units)
        {
            var unit = UnitBuilder.ConstructUnit(UnitPrefab, unitString);
            Units.Add(unit);
            TurnAssets.Add(unit);
        }

        // Add Spells.
        var parent = gameObject;
        Spells = new PlayerSpell[army.Spells.Count];
        for (var x = 0; x < army.Spells.Count;x++){
            var spell = PlayerSpell.ConstructSpell(army.Spells[x], this);
            if(spell != null){
                spell.Index = x;
                Spells[x] = spell;
                TurnAssets.Add(spell);
                UiManager.Active.createSpellTab(parent, x, spell);
            }
        }
    }

    public void TurnStart()
    {
        IsActive = true;
        foreach (var turnable in TurnAssets)
        {
            turnable.TurnStart();
        }
        Mana++;
        if (Mana > MaxMana) Mana = MaxMana;
    }

    public void TurnEnd()
    {
        IsActive = false;
        foreach (var turnable in TurnAssets)
        {
            turnable.TurnEnd();
        }
    }
}
