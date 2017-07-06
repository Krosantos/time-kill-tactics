using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellUi : MonoBehaviour, IPointerClickHandler, ISelectHandler
{
    public PlayerSpell Spell;

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickManager.Clear();
        ClickManager.Active.SetSelectedGameObject(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        ClickManager.SelectedSpell = Spell;
        ClickManager.SpellableTiles = Spell.GetValidTargets();
        ClickManager.ColorTiles();
    }

    void Update()
    {

    }
}
