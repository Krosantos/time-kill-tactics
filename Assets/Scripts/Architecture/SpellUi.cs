using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellUi : MonoBehaviour, IPointerClickHandler
{
    public PlayerSpell Spell;
    public Image SpellIcon, Disabled;
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
            Disabled.fillAmount = 0f;
            return;
        }
        var percentage = (float)Spell.CooldownCounter / (float)Spell.Cooldown;
        Disabled.fillAmount = percentage;
        CooldownText.text = Spell.CooldownCounter == 0 ? "" : Spell.CooldownCounter.ToString();
    }

    private void _setMana()
    {
        ManaCost.text = Spell.HasCost ? Spell.Cost.ToString() : "";
    }

    private void _setAmmo()
    {
        for (var x = 0; x < AmmoDots.Length; x++)
        {
            Debug.Log((x + 1 <= Spell.Ammo) ? "Full" : "Empty");
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
