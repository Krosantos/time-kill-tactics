using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRelay : MonoBehaviour {

    public int lemon;
    
	public void RelayMessage(RawMessage msg){
        Debug.Log(msg.Buffer.Decode());
    }
}
