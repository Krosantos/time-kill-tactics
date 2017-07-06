using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITurnable
{
	public int Team;
    public string Name;
    public List<ITurnable> TurnAssets;
    public bool IsEnemy, IsActive;
	public List<Unit> Units;
	public List<PlayerSpell> Spells;
	public TextAsset Army;
    public UnitBuilder UnitBuilder;
	public GameObject UnitPrefab;
	public int Mana, MaxMana;
    public static Player Me;

    public void Awake(){
		UnitBuilder = new UnitBuilder(Team, this);
		TurnAssets = new List<ITurnable>();
		Units = new List<Unit>();
		Spells = new List<PlayerSpell>();
	}

	public void Start(){
        // This little block has to change once we go networked.
		var army = JsonUtility.FromJson<Army>(Army.text);
		foreach(var unitString in army.Units){
			var unit = UnitBuilder.ConstructUnit(UnitPrefab, unitString);
			Units.Add(unit);
			TurnAssets.Add(unit);
		}

		// Add Spells. They nest under each other in the UI for ease of procedural generation.
        var currentSpellTab = gameObject;
        foreach(var spellString in army.Spells){
			var spell = PlayerSpell.ConstructSpell(spellString, this);
			if(spell != null){
				Spells.Add(spell);
				TurnAssets.Add(spell);
                currentSpellTab = UiManager.Active.createSpellTab(currentSpellTab, spell);
            }
		}
	}

	public void TurnStart()
	{
        IsActive = true;
        foreach(var turnable in TurnAssets){
			turnable.TurnStart();
		}
        Mana++;
		if(Mana > MaxMana) Mana = MaxMana;
    }

    public void TurnEnd()
    {
        IsActive = false;
        foreach(var turnable in TurnAssets){
			turnable.TurnEnd();
		}
    }
}
