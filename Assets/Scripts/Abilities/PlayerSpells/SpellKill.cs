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
        Targets = 1;
    }

    public override void TurnStart()
    {
        CooldownCounter--;
        if (CooldownCounter < 0) CooldownCounter = 0;
    }

    public override void TurnEnd() { }

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

    public override void Cast(List<Tile> targets)
    {
        var target = targets[0];
        if (IsDisabled() || target.Unit == null) return;
        target.Unit.OnAttacked(null, target.Unit, 5);
        Player.Mana -= Cost;
        CooldownCounter = Cooldown;
        if (target.Unit.Health <= 0)
        {
            target.Unit.Health = 0;
            target.Unit.OnDeath(target.Unit, null);
            target.Unit.CleanlyDestroy();
        }
        target.Unit.SyncUi();
    }
}