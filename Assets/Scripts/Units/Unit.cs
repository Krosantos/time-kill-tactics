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
    public SerializedUnit SerializedUnit;
    public Sprite Sprite
    {
        get
        {
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        set
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = value;
            var previousCollider = gameObject.GetComponent<PolygonCollider2D>();
            if (previousCollider != null) Destroy(previousCollider);
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }
    // public Anisomething something

    public int Team;
    public delegate List<Tile> GetMovesDel(Unit self);
    public delegate List<Unit> GetTargetsDel(Unit self, bool targetAllies, bool targetEnemies, bool getAllInRange);
    public delegate void MoveDel(Unit self, Tile tile);
    public delegate void AttackDel(Unit self, Unit victim);
    public delegate void OnTurnStartDel(Unit self);
    public delegate void OnTurnEndDel(Unit self);
    public delegate void OnDeathDel(Unit self, Unit killer);
    public delegate void OnKillDel(Unit self, Unit victim);
    public delegate void OnAttackedDel(Unit attacker, Unit self, int damage);
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
        if (ClickManager.EligibleToCast(Tile))
        {
            ClickManager.SelectedSpell.Cast(Tile);
            ClickManager.Clear();
        }
        else if (!ClickManager.Active.alreadySelecting || !HasMoved || !HasAttacked)
        {
            if (ClickManager.SelectedUnit == this)
            {
                ClickManager.Clear();
            }
            else if (ClickManager.EligibleToAttack(this))
            {
                var attacker = ClickManager.SelectedUnit;
                attacker.Attack(attacker, this);
                if (Health <= 0)
                {
                    Health = 0;
                    OnKill(attacker, this);
                    OnDeath(this, attacker);
                    this.CleanlyDestroy();
                }
                this.SyncUi();
                attacker.SyncUi();
                ClickManager.Clear();
            }
            else if (Player.IsActive == true)
            {
                ClickManager.Clear();
                ClickManager.Active.SetSelectedGameObject(gameObject);
            }
        }
    }

    public void OnMouseOver(){
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        ClickManager.SelectedUnit = this;
        if (!HasMoved) ClickManager.MovableTiles = GetMoves(this);
        if (!HasAttacked) ClickManager.AttackableUnits = GetTargets(this, false, true, false);
        if (!HasMoved && !HasAttacked) ClickManager.UnitsInRange = GetTargets(this, false, true, true);
        ClickManager.ColorTiles();
    }

    public void OnDeselect(BaseEventData eventData)
    {

    }
}
