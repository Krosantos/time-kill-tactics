using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITurnable
{
	public int Team;
    public string Name;
    public List<ITurnable> TurnAssets;
    public bool IsEnemy;
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
		foreach(var spellString in army.Spells){
			var spell = PlayerSpell.ConstructSpell(spellString);
			if(spell != null){
				spell.Player = this;
				Spells.Add(spell);
				TurnAssets.Add(spell);
			}
		}
	}

	public void TurnStart()
	{
		foreach(var turnable in TurnAssets){
			turnable.TurnStart();
		}
        Mana++;
		if(Mana > MaxMana) Mana = MaxMana;
    }

    public void TurnEnd()
    {
        foreach(var turnable in TurnAssets){
			turnable.TurnEnd();
		}
    }
}
