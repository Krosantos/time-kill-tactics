using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : MonoBehaviour {

    public GameObject UnitPrefab, EnemyPrefab;
    public MapSerializer Serializer;
    public Vector2[] AllyPositions, EnemyPositions;
    public TextAsset MapJson;

    public void Awake()
    {
        Serializer.DeserializeMap(MapJson.text);
        foreach(var pos in AllyPositions)
        {
            var result = Instantiate(UnitPrefab, new Vector3(), Quaternion.identity);
            var unit = result.GetComponent<Unit>();
            unit.Tile = Tile.TileMap[(int)pos.x, (int)pos.y];
            unit.Tile.Unit = unit;
            new MoveStandard().Apply(unit);
            unit.GetTargets += Target.Melee;
            unit.GetPosition();
        }
        foreach (var pos in EnemyPositions)
        {
            var result = Instantiate(EnemyPrefab, new Vector3(), Quaternion.identity);
            var unit = result.GetComponent<Unit>();
            unit.Tile = Tile.TileMap[(int)pos.x, (int)pos.y];
            unit.Tile.Unit = unit;
            new MoveStandard().Apply(unit);
            unit.GetTargets += Target.Melee;
            unit.transform.rotation = new Quaternion(0f, -180f, 0f, 0f);
            unit.GetPosition();
        }
    }
}
