using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialMessage
{

    private string _type;
    private int _packetCount;
    private bool _receivedFinal, _isReady;
    private Dictionary<int, string> _packets;

    public PartialMessage()
    {
        _packets = new Dictionary<int, string>();
        _receivedFinal = _isReady = false;
    }

    public bool AppendPacket(string raw)
    {
        // Each packet looks like --> Type|ID|Order|Final?|Body
        // These have lengths of  -->    4|18|    5|     1| 996
        var split = raw.Split('|');
        if (_type == null) _type = split[0];
        var order = int.Parse(split[2]);
        var body = "";
        for (var x = 4; x < split.Length; x++)
        {
            if (x != 4) body += "|";
            body += split[x];
        }
        if (!_packets.ContainsKey(order)) _packets[order] = body;
        if (split[3] == "1")
        {
            _receivedFinal = true;
            _packetCount = order + 1;
        }
        _isReady = _calculateReady();
        return _isReady;
    }

    private bool _calculateReady()
    {
        if (!_receivedFinal) return false;
        var result = true;
        for (var x = 0; x < _packetCount; x++)
        {
            Debug.Log($"Checking for packet {x + 1} of {_packetCount}");
            if (!_packets.ContainsKey(x))
            {
                Debug.Log("It ain't there =(");
                result = false;
                break;
            }
        }
        return result;
    }

    public BaseMessage FinalizeMessage()
    {
        if (!_isReady) return null;

        var compiled = _type + "|";
        for (var x = 0; x < _packetCount; x++)
        {
            compiled += _packets[x];
        }
        switch (_type)
        {
            case "MOVE":
                return new MoveMessage(compiled);
            case "ATCK":
                return new AttackMessage(compiled);
            case "SPEL":
                return new SpellMessage(compiled);
            case "TURN":
                return new TurnMessage(compiled);
            case "SYNC":
                return null;
            case "DISC":
                return null;
            case "BEAT":
                return new HeartBeatMessage(compiled);
            case "VICT":
                return null;
            case "FIND":
                return new FindGameMessage(compiled);
            case "ARMY":
                return new ArmyMessage(compiled);
            case "MAPP":
                return new MapMessage(compiled);
            default:
                Debug.Log("DEFAULT");
                Debug.Log(compiled);
                return null;
        }
    }

}
