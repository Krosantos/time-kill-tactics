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
            default:
                break;
        }
    }
}
