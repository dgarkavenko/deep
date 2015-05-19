using UnityEngine;
using System.Collections;
using SocketIO;


public class Player : MonoBehaviour {

    public int id;
    public Vector2 PlayerInput;

  

    public CellControl Cell;

    protected SocketIOComponent socket;

    public void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("position2client", UpdatePosition);

    }

    private void UpdatePosition(SocketIOEvent obj)
    {
        Cell.Jelly.ReferencePoints[0].transform.position = obj.data.ToVector2();        
    }

    void Update()
    {
        var _input = new Vector2(0,0);
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
        //if (_input.magnitude > 0) socket.Emit("input", _input.ToJSONObject());
        PlayerInput = _input;
    }

 

   
}
