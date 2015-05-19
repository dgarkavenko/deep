using UnityEngine;
using System.Collections.Generic;
using SocketIO;


public class NetworkController : MonoBehaviour {


    public SocketIOComponent socket;

    public static List<string> _logs = new List<string>();

	// Use this for initialization
	void Start () {
        socket.On("open", OnOpen);
        socket.On("close", OnClose);
        socket.On("error", OnError);
        socket.On("alive", OnAlive);
	}

    private void OnAlive(SocketIOEvent obj)
    {
        Debug.Log("alive");
    }

    public static void Log(string log)
    {
        _logs.Add(log);

        if (_logs.Count > 20)        
            _logs.RemoveAt(0);
        
    }
	
	// Update is called once per frame
	void OnGUI () {

        var log = "";

        foreach (var item in _logs)        
            log += item + "\n";
        

        GUI.Label(new Rect(0, 0, 1000, 1000), log);
	}

    public void OnOpen(SocketIOEvent e)
    {
        Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void OnError(SocketIOEvent e)
    {
        Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void OnClose(SocketIOEvent e)
    {
        Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }
}
