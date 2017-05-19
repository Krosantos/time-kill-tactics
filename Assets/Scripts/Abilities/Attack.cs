using UnityEngine;

public class Attack : MonoBehaviour {

    public static void Standard(Unit attacker, Unit victim)
    {
        victim.Health -= attacker.Strength;
        attacker.HasMoved = true;
    }
}
