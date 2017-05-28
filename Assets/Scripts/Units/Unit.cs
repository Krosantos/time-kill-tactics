using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, ITurnable, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public Player Player;
    public int Cost, MaxHealth, BaseStrength, BaseSpeed, Health, Strength, Speed;
    public bool HasMoved, HasAttacked;
    public Tile Tile;
    public Text AttackText, HpText;
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
    public delegate List<Tile> GetMovesDel(Unit self);
    public delegate List<Unit> GetTargetsDel(Unit self, bool targetAllies, bool targetEnemies);
    public delegate void MoveDel(Unit self, Tile tile);
    public delegate void AttackDel(Unit self, Unit victim);
    public delegate void OnTurnStartDel(Unit self);
    public delegate void OnTurnEndDel(Unit self);
    public delegate void OnDeathDel(Unit self, Unit killer);
    public delegate void OnKillDel(Unit self, Unit victim);
    public delegate void OnAttackedDel(Unit self, Unit attacker, int damage);
    public delegate void AbilityOneDel(Unit self, Unit target = null);
    public delegate void AbilityTwoDel(Unit self, Unit target = null);

    public GetMovesDel GetMoves;
    public GetTargetsDel GetTargets;
    public MoveDel Move;
    public AttackDel Attack;
    public OnTurnStartDel OnTurnStart;
    public OnTurnEndDel OnTurnEnd;
    public OnDeathDel OnDeath;
    public OnKillDel OnKill;
    public OnAttackedDel OnAttacked;
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
        if (!TurnManager.Active.alreadySelecting || !HasMoved || !HasAttacked) {
            if(TurnManager.SelectedUnit == this){
                TurnManager.Clear();
            }
            else if (TurnManager.EligibleToAttack(this))
            {
                var attacker = TurnManager.SelectedUnit;
                attacker.Attack(attacker, this);
                if (Health <= 0)
                {
                    Health = 0;
                    OnKill(attacker, this);
                    OnDeath(this, attacker);
                }
                this.SyncUi();
                attacker.SyncUi();
                attacker.HasAttacked = true;
                TurnManager.Clear();
            }
            else
            {
                TurnManager.Clear();
                TurnManager.Active.SetSelectedGameObject(gameObject);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        TurnManager.SelectedUnit = this;
        if(!HasMoved)TurnManager.MovableTiles = GetMoves(this);
        if(!HasAttacked) TurnManager.AttackableUnits = GetTargets(this, false, true);
        TurnManager.ColorTiles();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
    }
}
