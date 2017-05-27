using UnityEngine;

//Builds units from JSON
public class UnitBuilder {

    public int Team;

    public static string SerializeUnit(Unit unit){
        return JsonUtility.ToJson(unit);
    }
    
    public Unit ConstructUnit(GameObject unitPrefab, SerializedUnit serializedUnit){
        var result = GameObject.Instantiate(unitPrefab, new Vector3(), Quaternion.identity);
        var unit = result.GetComponentInChildren<Unit>();

        // The brunt of stat and ability assignment happens in here.
        serializedUnit.OverwriteUnit(unit);
        unit.SyncUi();
        // Get sprite? Get facing and team?
        unit.Team = Team;
        if(unit.Team != 0)unit.transform.Rotate(new Vector3(0f, 180f, 0f));
        return unit;
    }

}
