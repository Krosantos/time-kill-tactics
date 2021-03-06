﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour {

    public static void Apply(Unit unit, string ability)
    {
        switch (ability)
        {
            case "Explode":
                break;
            case "Rebirth":
                break;
            case "Standard":
                unit.OnDeath += Standard;
                break;
            default:
                break;
        }
    }

    public static void Standard(Unit unit, Unit attacker)
    {
        //Unit plays death animation.
    }
}
