using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTurn : MonoBehaviour {

    public static void Apply(Unit unit, string ability, bool onStart)
    {
        switch (ability)
        {
            case "Grow":
                break;
            case "Wither":
                break;
            case "Standard":
                if (onStart) unit.OnTurnStart += Standard;
                else unit.OnTurnEnd += Standard;
                break;
            default:
                break;
        }
    }

    public static void Standard(Unit self)
    {

    }
}
