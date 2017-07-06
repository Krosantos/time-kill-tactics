using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager Active;
    public bool PlayerActive;
    public Player Player;
    public Player Enemy;
    public Text PlayerName, EnemyName, PlayerMana, EnemyMana;
    public Image PlayerManaRing, EnemyManaRing;
    public GameObject SpellTabPrefab;

    public void Awake()
    {
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

    public void Update()
    {
        _updateMana(Player, PlayerMana, PlayerManaRing);
        _updateMana(Enemy, EnemyMana, EnemyManaRing);
    }

    private void _updateMana(Player player, Text manaText, Image manaRing)
    {
        manaText.text = "Mana\r\n" + player.Mana + "/" + player.MaxMana;
        manaRing.fillAmount = player.MaxMana == 0 ? 0f : (float)player.Mana / (float)player.MaxMana;
    }

    public void CheckForVictory()
    {
        // ...I should probably do this server-side only.
        if (Player.Units.Count <= 0)
        {
            Debug.Log("Enemy Wins!");
            Application.Quit();
        }
        if (Enemy.Units.Count <= 0)
        {
            Debug.Log("Player Wins!");
            Application.Quit();
        }
    }

    public GameObject createSpellTab(GameObject parent, PlayerSpell spell)
    {
        var newTab = Instantiate(SpellTabPrefab, new Vector3(), Quaternion.identity);
        newTab.transform.SetParent(parent.transform, false);

        // Set positioning.
        var rectTransform = newTab.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector3(50f, 50f);
        }

        // Load up the relevant spell.
        var spellUi = newTab.GetComponent<SpellUi>();
        if (spellUi != null)
        {
            spellUi.Spell = spell;
            // Set the display icon, and relevant cooldown/mana/ammo information.

        }

        return newTab;
    }

}
