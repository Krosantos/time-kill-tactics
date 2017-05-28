using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKill : MonoBehaviour
{

    public static void Apply(Unit unit, string ability)
    {
        switch (ability)
        {
            case "Highlander":
                break;
            case "Skeletons":
                break;
            case "Standard":
                unit.OnKill += Standard;
                break;
            default:
                break;
        }
    }

    public static void Standard(Unit killer, Unit victim)
    {
        //Nothing.
    }
}
