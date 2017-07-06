using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellUi : MonoBehaviour, IPointerClickHandler
{
    public PlayerSpell Spell;
    public Image SpellIcon;
    public Button Button;
    public Sprite Up, Down, UpClick, DownClick;

    public void Update()
    {
        Button.interactable = !Spell.IsDisabled();
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
