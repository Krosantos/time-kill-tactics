using UnityEngine;

public static class UnitExtensions {

    public static void GetPosition(this Unit unit)
    {
        var x = unit.Tile.X;
        var y = unit.Tile.Y;
        var z = unit.Tile.Height;
        var pos = new Vector3(x + (y % 2) * 0.5f, (0.75f * y + 0.35f * z)+0.25f);
        unit.transform.position = pos;
    }

}
