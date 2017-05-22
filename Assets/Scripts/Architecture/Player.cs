using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITurnable
{
	public int Team;
	public List<ITurnable> TurnAssets;
	public List<Unit> Units;
	public TextAsset Army;
    public UnitBuilder UnitBuilder;
	public GameObject UnitPrefab;

	public void Awake(){
		UnitBuilder = new UnitBuilder();
		UnitBuilder.Team = Team;
		TurnAssets = new List<ITurnable>();
		Units = new List<Unit>();
	}

	public void Start(){
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
