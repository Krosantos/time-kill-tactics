using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellUi : MonoBehaviour, IPointerClickHandler
{
    public PlayerSpell Spell;
    public Image SpellIcon, Disabled, CostRing;
    public Text ManaCost, CooldownText;
    public Button Button;
    public Sprite AmmoFull, AmmoEmpty;
    public Image[] AmmoDots;

    public void FixedUpdate()
    {
        Button.interactable = !Spell.IsDisabled();
        if (Spell.IsDisabled()) Disabled.fillAmount = 1f;
        _setCooldown();
        _setMana();
        _setAmmo();
    }

    private void _setCooldown()
    {
        if (!Spell.HasCooldown)
        {
            CooldownText.text = "";
            return;
        }
        Disabled.fillAmount = (Spell.IsDisabled() && Spell.CooldownCounter == 0) ? 1f : (float)Spell.CooldownCounter / (float)Spell.Cooldown;
        CooldownText.text = Spell.CooldownCounter == 0 ? "" : Spell.CooldownCounter.ToString();
    }

    private void _setMana()
    {
        if (Spell.HasCost)
        {
            CostRing.color = Color.white;
            ManaCost.text = Spell.Cost.ToString();
            ManaCost.color = Spell.Player.Mana >= Spell.Cost ? Color.white : Color.red;
        }
        else
        {
            CostRing.color = Color.clear;
            ManaCost.text = "";
        }
    }

    private void _setAmmo()
    {
        for (var x = 0; x < AmmoDots.Length; x++)
        {
            AmmoDots[x].sprite = (x + 1 <= Spell.Ammo) ? AmmoFull : AmmoEmpty;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ClickManager.Active.alreadySelecting)
        {
            if (ClickManager.SelectedSpell == Spell)
            {
                ClickManager.Clear();
            }
            else if (Spell.Player.IsActive)
            {
                ClickManager.Clear();
                ClickManager.Active.SetSelectedGameObject(gameObject);
                ClickManager.SelectedSpell = Spell;
                ClickManager.SpellableTiles = Spell.GetValidTargets();
                ClickManager.ColorTiles();
            }
        }
    }
}
