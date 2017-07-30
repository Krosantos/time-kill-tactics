﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRelay : MonoBehaviour
{

    public int lemon;

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
                if (classified.IsValid) classified.HandleMessage(this);
                break;
            case "ATCK":
                classified = new AttackMessage(decoded);
                break;
            case "SPEL":
                classified = null;
                break;
            case "SYNC":
                classified = null;
                break;
            case "DISC":
                classified = null;
                break;
            case "BEAT":
                classified = null;
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
