using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public static UiManager Active;
    public bool PlayerActive;
    public Player Player;
    public Player Enemy;
    public Text PlayerName, EnemyName, PlayerMana, EnemyMana;
    public Image PlayerManaRing, EnemyManaRing;

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

    public void Update(){
        _updateMana(Player, PlayerMana, PlayerManaRing);
        _updateMana(Enemy, EnemyMana, EnemyManaRing);
    }

    private void _updateMana(Player player, Text manaText, Image manaRing){
        manaText.text = "Mana\r\n"+player.Mana+"/"+player.MaxMana;
        Debug.Log("Pre: " + manaRing.fillAmount);
        manaRing.fillAmount = player.MaxMana == 0 ? 0f : (float)player.Mana / (float)player.MaxMana;
        Debug.Log("Post: " + manaRing.fillAmount);
    }

    public void CheckForVictory()
    {
        // ...I should probably do this server-side only.
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
