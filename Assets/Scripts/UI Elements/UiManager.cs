using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager Active;
    public Player Player;
    public Player Enemy;
    public Text PlayerName, EnemyName, PlayerMana, EnemyMana;
    public Image PlayerManaRing, EnemyManaRing;
    public GameObject SpellTabUp, SpellTabDown, SpellAmmoDot;
    public GameObject UnitHover, SpellHover, TileHover;

    public void Awake()
    {
        Active = this;
        PlayerName.text = Player.Name;
        EnemyName.text = Enemy.Name;
    }

    public void EndTurn()
    {
       if(Player.Me.IsActive) WebClient.Send(new TurnMessage(Player.Me.Team));
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

    public GameObject createSpellTab(GameObject parent, int index, PlayerSpell spell)
    {
        var isFlipped = spell.Player.IsEnemy;
        var newTab = Instantiate(isFlipped ? SpellTabDown : SpellTabUp, new Vector3(), Quaternion.identity);
        newTab.transform.SetParent(parent.transform, false);

        // Set positioning.
        var rectTransform = newTab.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = isFlipped ? new Vector3((-150f * (index + 1) + 62.5f), -50f) : new Vector3((150f * (index + 1) - 62.5f), 50f);
        }

        // Load up the relevant spell.
        var spellUi = newTab.GetComponent<SpellUi>();
        if (spellUi != null)
        {
            spellUi.Spell = spell;
            // Set the display icon, and relevant cooldown/mana/ammo information.
            var resourceString = "Sprites/Spells/" + spell.SpriteReference;
            var toLoad = Resources.Load<Sprite>(resourceString);
            spellUi.SpellIcon.sprite = toLoad;

            // If the spell uses Ammo, set up that indicator.
            if (spell.HasAmmo)
            {
                spellUi.AmmoDots = new Image[spell.MaxAmmo];
                for (var x = 0; x < spell.MaxAmmo; x++)
                {
                    var newDot = Instantiate(SpellAmmoDot, new Vector3(), Quaternion.identity);
                    newDot.transform.SetParent(spellUi.transform, false);
                    var dotTransform = newDot.GetComponent<RectTransform>();
                    if (dotTransform != null)
                    {
                        // The positioning will be slightly different depending on which side of the screen this loads on.
                        if (isFlipped)
                        {
                            dotTransform.anchoredPosition = new Vector3(0f, (-25f * (x + 1)));
                            dotTransform.anchorMax = new Vector2(0f, 1f);
                            dotTransform.anchorMin = new Vector2(0f, 1f);
                        }
                        else
                        {
                            dotTransform.anchoredPosition = new Vector3(0f, (25f * (x + 1)));
                            dotTransform.anchorMax = new Vector2(0f, 0f);
                            dotTransform.anchorMin = new Vector2(0f, 0f);
                        }
                    }

                    var dotSprite = newDot.GetComponent<Image>();
                    spellUi.AmmoDots[x] = dotSprite;
                }
            }
            else
            {
                spellUi.AmmoDots = new Image[0];
            }
        }

        return newTab;
    }

}
