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
                if (classified.IsValid) classified.Execute(this);
                break;
            case "ATCK":
                classified = new AttackMessage(decoded);
                if (classified.IsValid) classified.Execute(this);
                break;
            case "SPEL":
                classified = new SpellMessage(decoded);
                if (classified.IsValid) classified.Execute(this);
                break;
            case "TURN":
                classified = new TurnMessage(decoded);
                if(classified.IsValid) classified.Execute(this);
                break;
            case "SYNC":
                classified = null;
                if (classified.IsValid) classified.Execute(this);
                break;
            case "DISC":
                classified = null;
                break;
            case "BEAT":
                classified = null;
                if (classified.IsValid) classified.Execute(this);
                break;
            case "VICT":
                classified = null;
                if (classified.IsValid) classified.Execute(this);
                break;
            case "FIND":
                classified = new FindGameMessage(decoded);
                if (classified.IsValid) classified.Execute(this);
                break;
            default:
                classified = null;
                break;
        }
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
