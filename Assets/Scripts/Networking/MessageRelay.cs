using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRelay : MonoBehaviour {

    public int lemon;
    
	public void RelayMessage(BaseMessage msg){
        Debug.Log(msg.Buffer.Decode());
    }

    public void Test(){

    }
}
