using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMessage
{
    public bool IsValid;
    public byte[] Buffer = new byte[256];
    public abstract void Execute(MessageRelay relay);
}

public class RawMessage : BaseMessage
{
    public RawMessage()
    {
        IsValid = false;
    }
    public override void Execute(MessageRelay relay) { }
}

public class MoveMessage : BaseMessage
{
    public Vector2 From, To;
    public MoveMessage(string raw)
    {
        try
        {
            var split = raw.Split('|');
            From = new Vector2(int.Parse(split[1]), int.Parse(split[2]));
            To = new Vector2(int.Parse(split[3]), int.Parse(split[4]));
            IsValid = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            IsValid = false;
        }
    }

    public MoveMessage(Unit unit, Tile tile)
    {
        var rawString = $"MOVE|{unit.Tile.X}|{unit.Tile.Y}|{tile.X}|{tile.Y}";
        Buffer = rawString.Encode();
        IsValid = true;
    }

    public override void Execute(MessageRelay relay)
    {
        var unit = relay.GetUnitByCoords(From);
        var tile = relay.GetTileByCoords(To);
        unit.Move(unit, tile);
    }
}

public class AttackMessage : BaseMessage
{
    public Vector2 From, To;
    public AttackMessage(string raw)
    {
        Buffer = raw.Encode();
        try
        {
            var split = raw.Split('|');
            From = new Vector2(int.Parse(split[1]), int.Parse(split[2]));
            To = new Vector2(int.Parse(split[3]), int.Parse(split[4]));
            IsValid = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            IsValid = false;
        }
    }

    public AttackMessage(Unit attacker, Unit target)
    {
        var rawString = $"ATCK|{attacker.Tile.X}|{attacker.Tile.Y}|{target.Tile.X}|{target.Tile.Y}";
        Buffer = rawString.Encode();
        IsValid = true;
    }

    public override void Execute(MessageRelay relay)
    {
        var attacker = relay.GetUnitByCoords(From);
        var target = relay.GetUnitByCoords(To);
        attacker.Attack(attacker, target);
        if (target.Health <= 0)
        {
            target.Health = 0;
            attacker.OnKill(attacker, target);
            target.OnDeath(target, attacker);
            target.CleanlyDestroy();
        }
        target.SyncUi();
        attacker.SyncUi();
        ClickManager.Clear();
    }
}

public class SpellMessage : BaseMessage
{
    public int PlayerTeam, SpellIndex;
    public List<Vector2> Targets;

    public SpellMessage(string raw)
    {
        Targets = new List<Vector2>();
        Buffer = raw.Encode();
        try
        {
            var split = raw.Split('|');
            PlayerTeam = int.Parse(split[1]);
            SpellIndex = int.Parse(split[2]);
            for (var x = 3; x < split.Length; x += 2)
            {
                var newVect = new Vector2(int.Parse(split[x]), int.Parse(split[x + 1]));
                Targets.Add(newVect);
            }
            IsValid = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            IsValid = false;
        }
    }

    public SpellMessage(int team, int spellIndex, List<Tile> targets)
    {
        PlayerTeam = team;
        SpellIndex = spellIndex;
        Targets = targets.Select(t => new Vector2(t.X, t.Y)).ToList();
        var rawString = $"SPEL|{PlayerTeam}|{SpellIndex}";
        foreach (var target in Targets)
        {
            rawString += $"|{target.x}|{target.y}";
        }
        Buffer = rawString.Encode();
    }

    public override void Execute(MessageRelay relay)
    {
        var targets = Targets.Select(x => relay.GetTileByCoords(x)).ToList();
        var spell = Player.PlayersByTeam[PlayerTeam].Spells[SpellIndex];
        spell.Cast(targets);
    }
}

public class TurnMessage : BaseMessage
{
    int PlayerTeam;

    public TurnMessage(string raw)
    {
        PlayerTeam = int.Parse(raw.Split('|')[1]);
        IsValid = true;
        Buffer = raw.Encode();
    }

    public TurnMessage(int team)
    {
        PlayerTeam = team;
        var rawString = $"TURN|{team}";
        IsValid = true;
        Buffer = rawString.Encode();
    }

    public override void Execute(MessageRelay relay)
    {
        Debug.Log(PlayerTeam);
        var endingPlayer = Player.PlayersByTeam[PlayerTeam];
        var startingPlayer = (Player.PlayersByTeam.Count > PlayerTeam + 1) ? Player.PlayersByTeam[PlayerTeam + 1] : Player.PlayersByTeam[0];
        endingPlayer.IsActive = false;
        endingPlayer.TurnEnd();
        startingPlayer.IsActive = true;
        startingPlayer.TurnStart();
        Debug.Log(startingPlayer.Name);
    }
}

public class HeartBeatMessage : BaseMessage
{
    public override void Execute(MessageRelay relay) { }
}

public class ResyncMessage : BaseMessage
{
    public override void Execute(MessageRelay relay) { }
}

public class DisconnectMessage : BaseMessage
{
    public override void Execute(MessageRelay relay) { }
}
