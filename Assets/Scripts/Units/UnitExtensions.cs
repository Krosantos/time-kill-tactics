﻿using UnityEngine;

public static class UnitExtensions
{

    public static void SyncUi(this Unit unit)
    {
        if ((unit.HasAttacked || unit.GetTargets(unit, false, true, false).Count == 0) && unit.HasMoved && unit.Player.IsActive) unit.ToggleGrey(true);
        unit.GetPosition();
        unit.SetText();
    }

    public static void ToggleGrey(this Unit unit, bool shouldGrey)
    {
        var renderer = unit.GetComponent<SpriteRenderer>();
        if (renderer == null) return;
        var toLoad = shouldGrey ? "MAT_GreyScale" : "MAT_Standard";
        renderer.material = Resources.Load<Material>(toLoad);
    }

    public static void GetPosition(this Unit unit)
    {
        if (unit.Tile == null) return;
        var x = unit.Tile.X;
        var y = unit.Tile.Y;
        var z = unit.Tile.Height;
        var pos = new Vector3(x + (y % 2) * 0.5f, (0.75f * y + 0.35f * z) + 0.25f);
        unit.GetComponent<SpriteRenderer>().sortingOrder = 1 + unit.Tile.Height + unit.Tile.Y * -1;
        unit.transform.parent.position = pos;
    }

    public static void SetText(this Unit unit)
    {
        unit.HpText.text = unit.Health.ToString();
        unit.AttackText.text = unit.Strength.ToString();

        if (unit.Health > unit.MaxHealth) unit.HpText.color = Color.green;
        else if (unit.Health < unit.MaxHealth) unit.HpText.color = Color.red;
        else unit.HpText.color = Color.white;

        if (unit.Strength > unit.BaseStrength) unit.AttackText.color = Color.green;
        else if (unit.Strength < unit.BaseStrength) unit.AttackText.color = Color.red;
        else unit.AttackText.color = Color.white;
    }

    public static void CleanlyDestroy(this Unit unit)
    {
        unit.Player.Units.Remove(unit);
        unit.Player.TurnAssets.Remove(unit);
        GameObject.Destroy(unit.transform.parent.gameObject);
        UiManager.Active.CheckForVictory();
    }
}
