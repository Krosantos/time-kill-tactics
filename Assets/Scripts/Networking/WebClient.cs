using System;
using System.Net;
using UnityEngine;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

public class WebClient : MonoBehaviour
{
    public string IpAddress;
    public int Port, Heartbeat;
    public Status Status;
    private MessageRelay _messageRelay;
    private Socket _socket;
    private object _lock;
    private float _timeSinceLastMessage;
    private List<BaseMessage> _activeQueue, _passiveQueue;

    // Reconnect?

    // If initial connection fails?

    void Awake()
    {
        _messageRelay = gameObject.AddComponent<MessageRelay>();
        Connect();
    }

    public void Connect()
    {
        _lock = new object();
        _activeQueue = new List<BaseMessage>();
        _passiveQueue = new List<BaseMessage>();
        _timeSinceLastMessage = 0f;
        _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        _socket.BeginConnect(IpAddress, Port, new AsyncCallback(OnConnect), _socket);
        Status = Status.Connecting;
    }

    void OnConnect(IAsyncResult ar)
    {
        Debug.Log(_socket.Connected);
        Debug.Log("I AM CONNECT");
        Status = Status.Connected;
        _socket.Send("OPEN THE FLOODGATES".Encode());
        Receive();
    }

    public void Send(BaseMessage msg)
    {
        if (Status != Status.Connected)
        {
            Debug.Log("WebClient isn't connected! Couldn't send message!");
        }
        else
        {
            _socket.Send(msg.Buffer);
        }
    }

    void Update()
    {
        // Digest the message backlog.
        _timeSinceLastMessage += Time.deltaTime;
        if (_timeSinceLastMessage > Heartbeat) Send(new BaseMessage());
        // Swap out the queues. The lock prevents the constant Receive from altering them as this happens.
        lock (_lock)
        {
            var holding = _passiveQueue;
            _passiveQueue = _activeQueue;
            _activeQueue = holding;
        }
        foreach (var msg in _activeQueue)
        {
            _messageRelay.RelayMessage(msg);
        }
        _activeQueue.Clear();
    }

    // You can think of each call of Receive() as a a window of opportunity to collect a single message.
    // In the callback, we'll call Receive() again, so as to re-prime the socket for incoming messages.
    void Receive()
    {
        var msg = new BaseMessage();
        _socket.BeginReceive(msg.Buffer, 0, 256, SocketFlags.None, new AsyncCallback(OnReceive), msg);
    }

    void OnReceive(IAsyncResult ar)
    {
        var length = _socket.EndReceive(ar);
        if (length > 0)
        {
            _timeSinceLastMessage = 0f;
            var msg = (BaseMessage)ar.AsyncState;
            lock (_lock) _passiveQueue.Add(msg);
        }
        Receive();
    }

    // Close gracefully on quit
    void OnApplicationQuit()
    {
        if (Status == Status.Connected) Disconnect();
    }

    void Disconnect()
    {
        _socket.Close();
        Status = Status.Idle;
    }
}

public enum Status
{
    Connected,
    Connecting,
    Idle,
    Error
}
