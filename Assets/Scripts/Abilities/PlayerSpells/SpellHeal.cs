using System.Collections.Generic;

public class SpellHeal : PlayerSpell
{
    public SpellHeal(Player player)
    {
        Player = player;
        SpriteReference = "PS_Heal";
        Name = "Heal";
        Text = "Heal any unit for 2 HP.";
        Ammo = MaxAmmo = 3;
        Cooldown = 1;
        CooldownCounter = 0;
        HasAmmo = HasCooldown = true;
        Player.MaxMana += 3;
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
        foreach (var unit in Player.Units)
        {
            result.Add(unit.Tile);
        }
        return result;
    }

    public override void Cast(List<Tile> targets)
    {
        var target = targets[0];
        if (IsDisabled() || target.Unit == null) return;
        target.Unit.Health += 2;
        if (target.Unit.Health > target.Unit.MaxHealth) target.Unit.Health = target.Unit.MaxHealth;
        target.Unit.SyncUi();
        Ammo--;
        CooldownCounter = Cooldown;
    }
}