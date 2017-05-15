using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITurnable
{
	public int Team;
	public List<ITurnable> TurnAssets;
	public List<Unit> Units;

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
