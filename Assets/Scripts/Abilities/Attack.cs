﻿using UnityEngine;

public class Attack : MonoBehaviour {

    public static void Apply(Unit unit, string onAttack)
    {
        switch (onAttack)
        {
            case "Poison":
                break;
            case "Destroy":
                break;
            case "Standard":
                unit.Attack += Standard;
                break;
            default:
                break;
        }
    }

    public static void Standard(Unit attacker, Unit victim)
    {
        attacker.HasMoved = true;
        attacker.HasAttacked = true;
        victim.OnAttacked(attacker, victim, attacker.Strength);
    }
}
