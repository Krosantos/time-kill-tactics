using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, ITurnEndable, IPointerClickHandler, ISelectHandler, IDeselectHandler
{

    public int MaxHealth, BaseStrength, BaseSpeed, Health, Strength, Speed;
    public bool HasMoved, HasAttacked;
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
    //False for left, true for right.
    public bool Facing;
    public delegate void MoveDel(Unit self);
    public delegate void AttackDel(Unit self, Unit victim);
    public delegate void OnTurnDel(Unit self);
    public delegate void OnDeathDel(Unit self, Unit killer);
    public delegate void OnAttackedDel(Unit self, Unit attacker, int damage);
    public delegate void OnAttackDel(Unit self, Unit victim);
    public delegate void AbilityOneDel(Unit self, Unit target = null);
    public delegate void AbilityTwoDel(Unit self, Unit target = null);

    public MoveDel Move;
    public AttackDel Attack;
    public OnTurnDel OnTurn;
    public OnDeathDel OnDeath;
    public OnAttackedDel OnAttacked;
    public OnAttackDel OnAttack;
    public AbilityOneDel AbilityOne;
    public AbilityTwoDel AbilityTwo;

    public List<string> Tags;

    public void OnTurnEnd()
    {
        HasMoved = false;
        HasAttacked = false;
        OnTurn(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnSelect(BaseEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        throw new NotImplementedException();
    }
}
