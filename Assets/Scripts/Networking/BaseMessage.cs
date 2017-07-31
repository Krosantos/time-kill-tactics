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
    public abstract void HandleMessage(MessageRelay relay);
}

public class RawMessage : BaseMessage
{
    public RawMessage()
    {
        IsValid = false;
    }
    public override void HandleMessage(MessageRelay relay) { }
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
    }

    public override void HandleMessage(MessageRelay relay)
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
    }

    public override void HandleMessage(MessageRelay relay)
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

public class HeartBeatMessage : BaseMessage
{
    public override void HandleMessage(MessageRelay relay) { }
}

public class ResyncMessage : BaseMessage
{
    public override void HandleMessage(MessageRelay relay) { }
}

public class DisconnectMessage : BaseMessage
{
    public override void HandleMessage(MessageRelay relay) { }
}
