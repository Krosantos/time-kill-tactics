using System;
using System.Net;
using UnityEngine;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Collections.Generic;

public class WebClient : MonoBehaviour
{

    public Socket Socket;
    public Byte[] Buffer;
    // Use this for initialization
    void Start()
    {
        Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        Socket.BeginConnect("127.0.0.1", 3000, new AsyncCallback(OnConnect), Socket);
    }

    void OnConnect(IAsyncResult ar)
    {
        Debug.Log("I AM CONNECT");
        Socket.Send("lemon".Encode());
    }

    // Update is called once per frame
    void Update()
    {
        var msg = new RawMessage();
        Socket.BeginReceive(msg.Buffer, 0, 256, SocketFlags.None, new AsyncCallback(OnReceive), msg);
    }

    void OnReceive(IAsyncResult ar)
    {
        var length = Socket.EndReceive(ar);
        if (length > 0)
        {
            var msg = (RawMessage)ar.AsyncState;
            Debug.Log(msg.Buffer.Decode());
        }
    }
}
