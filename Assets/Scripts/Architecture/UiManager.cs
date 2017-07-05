using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public static UiManager Active;
    public bool PlayerActive;
    public Player Player;
    public Player Enemy;
    public Text PlayerName, EnemyName;

    public void Awake(){
        Active = this;
        PlayerName.text = Player.Name;
        EnemyName.text = Enemy.Name;
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

}
