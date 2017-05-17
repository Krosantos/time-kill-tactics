using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Builds units from JSON
public class UnitBuilder : MonoBehaviour {
    
    public static string SerializeUnit(Unit unit){
        return JsonUtility.ToJson(unit);
    }

    // Add in delegate building bit.
    public static GameObject ConstructUnit(GameObject unitPrefab, string serializedUnit){
        var result = Instantiate(unitPrefab, new Vector3(), Quaternion.identity);
        var unit = result.GetComponent<Unit>();
        JsonUtility.FromJsonOverwrite(serializedUnit, unit);
        new Climb().Apply(unit);
        unit.GetTargets += Target.Melee;
        return result;
    }

}
