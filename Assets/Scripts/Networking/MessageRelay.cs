using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRelay : MonoBehaviour
{

    public static Dictionary<string, PartialMessage> MessageDict;

    public void ProcessMessage(BaseMessage packet)
    {
        var decoded = packet.Buffer.Decode();
        if (decoded.Split('|').Length < 2)
        {
            Debug.Log(decoded);
        }
        var id = decoded.Split('|')[1];
        if (!MessageDict.ContainsKey(id)) MessageDict[id] = new PartialMessage();
        if (MessageDict[id].AppendPacket(decoded))
        {
            var finalized = MessageDict[id].FinalizeMessage();
            ExecuteMessage(finalized, id);
        }
    }

    public void ExecuteMessage(BaseMessage msg, string id)
    {
        if (msg != null && msg.IsValid)
        {
            msg.Execute(this);
            MessageDict.Remove(id);
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
