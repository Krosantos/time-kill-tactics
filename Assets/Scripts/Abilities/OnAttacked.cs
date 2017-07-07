using UnityEngine;

public class OnAttacked : MonoBehaviour {

    public static void Apply(Unit unit, string ability)
    {
        switch (ability)
        {
            case "Counter":
                break;
            case "Deflect":
                break;
            case "Standard":
                unit.OnAttacked += Standard;
                break;
            default:
                break;
        }
    }
    
    public static void Standard(Unit attacker, Unit victim, int damage)
    {
        victim.Health -= damage;
    }
}
