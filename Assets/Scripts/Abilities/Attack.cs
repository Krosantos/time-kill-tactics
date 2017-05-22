using UnityEngine;

public class Attack : MonoBehaviour {

    public static void Apply(Unit unit, string onAttack)
    {
        switch (onAttack)
        {
            case "Poison":
                break;
            case "Destroy":
                break;
            default:
                unit.Attack += Standard;
                break;
        }
    }

    public static void Standard(Unit attacker, Unit victim)
    {
        victim.Health -= attacker.Strength;
        attacker.HasMoved = true;
    }
}
