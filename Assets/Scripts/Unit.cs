using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int MaxHealth, BaseStrength, BaseSpeed, Health, Strength, Speed;
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
    public delegate void Move(Unit self);
    public delegate void Attack(Unit self, Unit victim);
    public delegate void OnTurn(Unit self);
    public delegate void OnDeath(Unit self, Unit killer);
    public delegate void OnAttacked(Unit self, Unit attacker, int damage);
    public delegate void OnAttack(Unit self, Unit victim);
    public delegate void AbilityOne(Unit self, Unit target = null);
    public delegate void AbilityTwo(Unit self, Unit target = null);

    public List<string> Tags;
}
