using UnityEngine;

//Builds units from JSON
public class UnitBuilder {

    public Player Player;

    public UnitBuilder(Player player)
    {
        Player = player;
    }

    public static string SerializeUnit(Unit unit){
        return JsonUtility.ToJson(unit);
    }

    public Sprite GetSprite(string spriteReference)
    {
        var toFind = "Sprites/Units/" + spriteReference;
        return Resources.Load<Sprite>(toFind);
    }

    public Unit ConstructUnit(GameObject unitPrefab, SerializedUnit serializedUnit){
        var result = GameObject.Instantiate(unitPrefab, new Vector3(), Quaternion.identity);
        var unit = result.GetComponentInChildren<Unit>();
        // Units start getting unpacked at 2, 13 and 9, 13
        // The brunt of stat and ability assignment happens in here.
        serializedUnit.OverwriteUnit(unit);
        unit.SerializedUnit = serializedUnit;
        unit.Sprite = GetSprite(serializedUnit.SpriteReference);
        unit.Player = Player;
        if(Player.Team == 1)unit.transform.Rotate(new Vector3(0f, 180f, 0f));
        unit.SyncUi();
        return unit;
    }

}
