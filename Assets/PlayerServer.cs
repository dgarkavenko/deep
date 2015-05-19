using UnityEngine;
using System.Collections;
using SocketIO;


public class PlayerServer : Player {


	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("input2server", UpdateInput);
	}
	
    private void UpdateInput(SocketIOEvent obj)
    {        
        PlayerInput = obj.data.ToVector2();
        NetworkController.Log(PlayerInput.ToString());
    }

    void Update()
    {
        socket.Emit("position", Cell.GetPosition().ToJSONObject());
    }

}
