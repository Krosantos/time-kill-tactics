using System.Collections.Generic;

public class SpellHeal : PlayerSpell
{
    public SpellHeal(Player player)
    {
        Player = player;
        SpriteReference = "PS_Heal";
        Name = "Heal";
        Text = "Heal any unit for 2 HP.";
        Ammo = 5;
        Cooldown = 1;
        CooldownCounter = 0;
        HasAmmo = HasCooldown = true;
        Player.MaxMana += 3;
    }

    public override void TurnEnd()
    {
        CooldownCounter--;
        if (CooldownCounter < 0) CooldownCounter = 0;
    }

    public override List<Tile> GetValidTargets()
    {
        var result = new List<Tile>();
        if (IsDisabled()) return result;
        foreach (var unit in Player.Units)
        {
            result.Add(unit.Tile);
        }
        return result;
    }

    public override void Cast(Tile tile)
    {
        if (IsDisabled() || tile.Unit == null) return;
        tile.Unit.Health += 2;
        if (tile.Unit.Health > tile.Unit.MaxHealth) tile.Unit.Health = tile.Unit.MaxHealth;
        tile.Unit.SyncUi();
        Ammo--;
        CooldownCounter = Cooldown;
    }
}