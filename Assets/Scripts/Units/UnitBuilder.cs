using UnityEngine;

//Builds units from JSON
public class UnitBuilder {

    public int Team;

    public static string SerializeUnit(Unit unit){
        return JsonUtility.ToJson(unit);
    }
    
    public static GameObject ConstructUnit(GameObject unitPrefab, string serialized){
        var result = GameObject.Instantiate(unitPrefab, new Vector3(), Quaternion.identity);
        var unit = result.GetComponent<Unit>();

        // The brunt of stat and ability assignment happens in here.
        var serializedUnit = JsonUtility.FromJson<SerializedUnit>(serialized);
        serializedUnit.OverwriteUnit(unit);
        // Get sprite? Get facing and team?
        return result;
    }

}
