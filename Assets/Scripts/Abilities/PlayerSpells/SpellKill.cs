using System.Collections.Generic;

public class SpellKill : PlayerSpell
{
    public SpellKill(Player player)
    {
        Player = player;
        SpriteReference = "PS_Heal";
        Name = "Kill";
        Text = "Kill any unit. Lol, OP.";
        Cooldown = 3;
        CooldownCounter = 1;
        Cost = 2;
        HasCost = HasCooldown = true;
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
        foreach (var unit in UiManager.Active.Enemy.Units)
        {
            result.Add(unit.Tile);
        }
        return result;
    }

    public override void Cast(Tile tile)
    {
        if (IsDisabled() || tile.Unit == null) return;
        tile.Unit.OnAttacked(null, tile.Unit, 5);
        Player.Mana -= Cost;
        CooldownCounter = Cooldown;
        if (tile.Unit.Health <= 0)
        {
            tile.Unit.Health = 0;
            tile.Unit.OnDeath(tile.Unit, null);
            tile.Unit.CleanlyDestroy();
        }
        tile.Unit.SyncUi();
    }
}