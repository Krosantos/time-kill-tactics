using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRelay : MonoBehaviour
{

    public void ProcessMessage(BaseMessage msg)
    {
        Debug.Log(msg.Buffer.Decode());
        var decoded = msg.Buffer.Decode();
        var split = decoded.Split('|');
        BaseMessage classified;
        switch (split[0])
        {
            case "MOVE":
                classified = new MoveMessage(decoded);                
                break;
            case "ATCK":
                classified = new AttackMessage(decoded);                
                break;
            case "SPEL":
                classified = new SpellMessage(decoded);                
                break;
            case "TURN":
                classified = new TurnMessage(decoded);
                break;
            case "SYNC":
                classified = null;                
                break;
            case "DISC":
                classified = null;
                break;
            case "BEAT":
                classified = new HeartBeatMessage(decoded);                
                break;
            case "VICT":
                classified = null;                
                break;
            case "FIND":
                classified = new FindGameMessage(decoded);                
                break;
            case "ARMY":
                classified = new ArmyMessage(decoded);                
                break;
            case "MAPP":
                classified = new MapMessage(decoded);                
                break;
            default:
                classified = null;
                break;
        }
        if (classified != null && classified.IsValid) classified.Execute(this);
    }

    public Unit GetUnitByCoords(Vector2 coords)
    {
        var x = (int)coords.x;
        var y = (int)coords.y;
        if (Tile.TileMap.GetLength(0) > x && Tile.TileMap.GetLength(1) > y && x >= 0 && y >= 0)
        {

            var tile = Tile.TileMap[(int)coords.x, (int)coords.y];
            return tile.Unit;
        }
        return null;
    }

    public Tile GetTileByCoords(Vector2 coords)
    {
        var x = (int)coords.x;
        var y = (int)coords.y;
        if (Tile.TileMap.GetLength(0) > x && Tile.TileMap.GetLength(1) > y && x >= 0 && y >= 0)
        {

            return Tile.TileMap[(int)coords.x, (int)coords.y];
        }
        return null;
    }
}
