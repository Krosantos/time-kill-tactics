using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITurnable
{
	public int Team;
	public List<ITurnable> TurnAssets;
    public bool IsEnemy;
	public List<Unit> Units;
	public TextAsset Army;
    public UnitBuilder UnitBuilder;
	public GameObject UnitPrefab;
    public static Player Me;

	public void Awake(){
		UnitBuilder = new UnitBuilder(Team, this);
		TurnAssets = new List<ITurnable>();
		Units = new List<Unit>();
	}

	public void Start(){
        // This little block has to change once we go networked.
        if (IsEnemy) TurnManager.Active.Enemy = this;
        else
        {
            TurnManager.Active.Player = this;
            Me = this;
        }
		var army = JsonUtility.FromJson<Army>(Army.text);
		foreach(var unitString in army.Units){
			var unit = UnitBuilder.ConstructUnit(UnitPrefab, unitString);
			Units.Add(unit);
			TurnAssets.Add(unit);
		}
	}

	public void TurnStart()
	{
		foreach(var turnable in TurnAssets){
			turnable.TurnStart();
		}
	}

    public void TurnEnd()
    {
        foreach(var turnable in TurnAssets){
			turnable.TurnEnd();
		}
    }
}
