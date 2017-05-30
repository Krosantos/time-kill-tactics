using UnityEngine;

public class Attack : MonoBehaviour {

    public static void Apply(Unit unit, string onAttack)
    {
        Debug.Log(onAttack);
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
        victim.Health -= attacker.Strength;
        attacker.HasMoved = true;
    }
}
