using UnityEngine;

//Builds units from JSON
public class UnitBuilder {

    public int Team;

    public static string SerializeUnit(Unit unit){
        return JsonUtility.ToJson(unit);
    }
    
    public static Unit ConstructUnit(GameObject unitPrefab, SerializedUnit serializedUnit){
        var result = GameObject.Instantiate(unitPrefab, new Vector3(), Quaternion.identity);
        var unit = result.GetComponent<Unit>();

        // The brunt of stat and ability assignment happens in here.
        serializedUnit.OverwriteUnit(unit);
        unit.SyncUi();
        // Get sprite? Get facing and team?
        return unit;
    }

}
