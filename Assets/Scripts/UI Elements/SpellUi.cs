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
        _setCooldown();
        _setMana();
        _setAmmo();
        if (!Spell.Player.IsActive && (!Spell.HasCooldown || Spell.CooldownCounter == 0)) _useFullDisable();
    }

    private void _useFullDisable()
    {
        Disabled.fillAmount = 1f;
    }

    private void _setCooldown()
    {
        if (!Spell.HasCooldown)
        {
            CooldownText.text = "";
            return;
        }
        Disabled.fillAmount = (float)Spell.CooldownCounter / (float)Spell.Cooldown;
        CooldownText.text = Spell.CooldownCounter == 0 ? "" : Spell.CooldownCounter.ToString();
    }

    private void _setMana()
    {
        if (Spell.HasCost)
        {
            CostRing.color = Color.white;
            ManaCost.text = Spell.Cost.ToString();
            if (Spell.Player.Mana < Spell.Cost)
            {
                _useFullDisable();
                ManaCost.color = Color.red;
            }
            else
            {
                ManaCost.color = Color.white;
            }
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
        if (Spell.HasAmmo && Spell.Ammo == 0) _useFullDisable();
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
