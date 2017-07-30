using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRelay : MonoBehaviour {

    public int lemon;
    
	public void ProcessMessage (BaseMessage msg){
        Debug.Log(msg.Buffer.Decode());
        var decoded = msg.Buffer.Decode();
        var split = decoded.Split('|');
        BaseMessage classified;
        switch(split[0]){
            case "MOVE":
                classified = new MoveMessage(decoded);
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

    public void Test(){

    }
}
