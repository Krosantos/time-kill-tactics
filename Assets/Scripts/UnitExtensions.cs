using UnityEngine;

public static class UnitExtensions {

    public static Vector3 GetPosition(this Unit unit)
    {
        var x = unit.Tile.X;
        var y = unit.Tile.Y;
        var z = unit.Tile.Height;
        return new Vector3(x + (y % 2) * 0.5f, (0.75f * y + 0.35f * z)+0.25f);
    }

}
