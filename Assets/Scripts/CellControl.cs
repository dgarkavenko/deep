using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class CellControl : NetworkBehaviour
{

	// Use this for initialization

	private JellySprite jelly;
	private float _rad2Directions;
	void Start ()
	{	
		jelly = GetComponent<JellySprite>();
		jelly.ManualStart(GetComponent<JellySpriteReferencePoint>());
		_rad2Directions = 0.5f / Mathf.PI*NumberOfReferencePoints;

	}

	public float MS;
	public int NumberOfReferencePoints = 8;
	public int NumberOfAdjustedPoints = 1;

	public float Radius = 1;
	public float VisualRadius = 1;

	private void Feed(float foodRadius)
	{
		Radius = (float)Math.Sqrt(Radius * Radius + foodRadius * foodRadius);
	
	}

	private float vel;
	public float GrowTime = 1;
	public Rigidbody2D Body;

	void Update()
	{
		
		if(!isLocalPlayer) return;

		var h = Input.GetAxisRaw("Horizontal");
		var v = Input.GetAxisRaw("Vertical");
		var dir = (new Vector2(h, v)).normalized;

		float dirInt = Mathf.Atan2(dir.y, dir.x) * _rad2Directions;

		Body.AddForce(dir * MS);
		return;

		if (dirInt < 0) dirInt += NumberOfReferencePoints;
		for (int i = -NumberOfAdjustedPoints; i < NumberOfAdjustedPoints + 1; i++)
		{
			var f = (int)dirInt + i;
			if (f < 0) f = f + NumberOfReferencePoints;
			if (f > NumberOfReferencePoints - 1) f -= NumberOfReferencePoints;
			jelly.CmdAddForce(dir * MS, 1 + f);
			
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Math.Abs(VisualRadius - Radius) > 0.01f)
		{
			VisualRadius = Mathf.SmoothDamp(VisualRadius, Radius, ref vel, GrowTime);
			jelly.Scale(VisualRadius);
		}

	}


}
