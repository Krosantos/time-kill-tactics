using System;
using System.Net;
using System.Text;

public class RawMessage  {

    public byte[] Buffer;

	public RawMessage(){
        Buffer = new byte[256];
    }

}
