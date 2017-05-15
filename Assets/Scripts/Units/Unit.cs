using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, ITurnable, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public Player Player;
    public int MaxHealth, BaseStrength, BaseSpeed, Health, Strength, Speed;
    public bool HasMoved, HasAttacked;
    [NonSerialized]
    public Tile Tile;
    public Sprite Sprite
    {
        get
        {
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        set { gameObject.GetComponent<SpriteRenderer>().sprite = value; }
    }
    // public Anisomething something

    public int Team;
    public delegate List<Tile> MoveDel(Unit self);
    public delegate void AttackDel(Unit self, Unit victim);
    public delegate void OnTurnStartDel(Unit self);
    public delegate void OnTurnEndDel(Unit self);
    public delegate void OnDeathDel(Unit self, Unit killer);
    public delegate void OnAttackedDel(Unit self, Unit attacker, int damage);
    public delegate void OnAttackDel(Unit self, Unit victim);
    public delegate void AbilityOneDel(Unit self, Unit target = null);
    public delegate void AbilityTwoDel(Unit self, Unit target = null);

    public MoveDel Move;
    public AttackDel Attack;
    public OnTurnStartDel OnTurnStart;
    public OnTurnEndDel OnTurnEnd;
    public OnDeathDel OnDeath;
    public OnAttackedDel OnAttacked;
    public OnAttackDel OnAttack;
    public AbilityOneDel AbilityOne;
    public AbilityTwoDel AbilityTwo;

    public List<string> Tags;

    public void TurnStart()
    {
        HasMoved = false;
        HasAttacked = false;
        OnTurnStart(this);
    }

    public void TurnEnd()
    {
        HasMoved = true;
        HasAttacked = true;
        OnTurnEnd(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!TurnManager.Active.alreadySelecting) {
            if (TurnManager.Active.EligibleToAttack(this))
            {
                TurnManager.Active.SelectedUnit.Attack(TurnManager.Active.SelectedUnit,this);
            }
            else
            {
                TurnManager.Active.SetSelectedGameObject(gameObject);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Selecting Unit!");
        TurnManager.Active.MovableTiles = Move(this);
        TurnManager.Active.ColorTiles();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Deselecting Unit!");
        TurnManager.Active.Clear();
    }
}
