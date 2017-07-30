using System;
using System.Net;
using System.Text;
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

    public override void HandleMessage(MessageRelay relay)
    {

    }
}

public class AttackMessage : BaseMessage
{
    public Vector2 From, To;
    public AttackMessage(string raw)
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

    public override void HandleMessage(MessageRelay relay)
    {

    }
}
