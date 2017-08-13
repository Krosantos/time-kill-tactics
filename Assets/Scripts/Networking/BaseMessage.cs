using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMessage
{
    public bool IsValid;
    public byte[] Buffer = new byte[1028];
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
        // If we send over something wild (which we do to begin the match), start player one.
        if (!Player.PlayersByTeam.ContainsKey(PlayerTeam))
        {
            var startingPlayer = Player.PlayersByTeam[0];
            startingPlayer.IsActive = true;
            startingPlayer.TurnStart();
        }
        else
        {
            var endingPlayer = Player.PlayersByTeam[PlayerTeam];
            var startingPlayer = (Player.PlayersByTeam.Count > PlayerTeam + 1) ? Player.PlayersByTeam[PlayerTeam + 1] : Player.PlayersByTeam[0];
            endingPlayer.IsActive = false;
            endingPlayer.TurnEnd();
            startingPlayer.IsActive = true;
            startingPlayer.TurnStart();
        }
    }
}

public class HeartBeatMessage : BaseMessage
{

    public HeartBeatMessage(string raw)
    {
        Buffer = raw.Encode();
        IsValid = true;
    }

    public HeartBeatMessage()
    {
        var rawString = "BEAT|";
        Buffer = rawString.Encode();
        IsValid = true;
    }

    public override void Execute(MessageRelay relay)
    {
        WebClient.Active.Status = Status.Connected;
    }
}

public class ResyncMessage : BaseMessage
{
    public override void Execute(MessageRelay relay) { }
}

public class DisconnectMessage : BaseMessage
{
    public override void Execute(MessageRelay relay) { }
}

public class FindGameMessage : BaseMessage
{
    // We'll put in whatever we need for matchmaking eventually. We'll add another
    // variable which the server will use to tell us which player we are (player or enemy).

    public int AssignedTeam, EnemyTeam;

    public FindGameMessage(string raw)
    {
        var split = raw.Split('|');
        AssignedTeam = int.Parse(split[1]);
        EnemyTeam = int.Parse(split[2]);
        Buffer = raw.Encode();
        IsValid = true;
    }

    public FindGameMessage(int team = 0, int enemy = 0)
    {
        var rawString = $"FIND|{team}|{enemy}";
        AssignedTeam = team;
        EnemyTeam = enemy;
        Buffer = rawString.Encode();
        IsValid = true;
    }

    public override void Execute(MessageRelay relay)
    {
        Player.PlayersByTeam[AssignedTeam] = Player.Me;
        Player.PlayersByTeam[EnemyTeam] = Player.Enemy;
    }
}

public class ArmyMessage : BaseMessage
{
    public Army Army;
    public int Team;

    public ArmyMessage(string raw)
    {
        var split = raw.Split('|');
        Team = int.Parse(split[1]);

        // For now, gonna play it safe on the off chance the army contains | characters.
        var components = new string[split.Length - 2];
        for (var x = 2; x < split.Length; x++) components[x - 2] = split[x];
        var armyJson = string.Join("|", components);
        Debug.Log($"The Army Message is {raw.Length} characters long");
        Army = JsonUtility.FromJson<Army>(armyJson);
        Buffer = raw.Encode();
        IsValid = true;
    }

    public ArmyMessage(Army army, int team)
    {
        Army = army;
        Team = team;
        var armyJson = JsonUtility.ToJson(army, false);
        var rawString = $"ARMY|{team}|{armyJson}";
        Buffer = rawString.Encode();
        IsValid = true;
    }

    public override void Execute(MessageRelay relay)
    {
        var player = Player.PlayersByTeam[Team];
        player.LoadArmy(Army);
    }
}

public class MapMessage : BaseMessage
{
    public Map Map;

    public MapMessage(string raw)
    {
        var split = raw.Split('|');

        // For now, gonna play it safe on the off chance the army contains | characters.
        var components = new string[split.Length - 1];
        for (var x = 1; x < split.Length; x++) components[x - 1] = split[x];
        var mapJson = string.Join("|", components);
        Debug.Log($"The Map Message is {raw.Length} characters long");
        Debug.Log(mapJson);
        Debug.Log(raw);
        Map = JsonUtility.FromJson<Map>(mapJson);
        Buffer = raw.Encode();
        IsValid = true;
    }

    public MapMessage(Map map)
    {
        Map = map;
        var mapJson = JsonUtility.ToJson(map);
        var rawString = $"MAPP|{mapJson}";
        Buffer = rawString.Encode();
        IsValid = true;
    }

    public override void Execute(MessageRelay relay)
    {
        MapSerializer.Active.WipeMap();
        MapSerializer.Active.DeserializeMap(Map);
    }
}
