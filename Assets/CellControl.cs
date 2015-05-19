using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CellControl : MonoBehaviour {

	// Use this for initialization

    public Player player;

	public JellySprite Jelly;
	private float _rad2Directions;
	void Start ()
	{	
		Jelly = GetComponent<JellySprite>();
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

	// Update is called once per frame
	void FixedUpdate ()
	{

		if (Math.Abs(VisualRadius - Radius) > 0.01f)
		{
			VisualRadius = Mathf.SmoothDamp(VisualRadius, Radius, ref vel, GrowTime);
			Jelly.Scale(VisualRadius);
		}
		

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Feed(Random.Range(1,Radius) * 2);
			//jelly.Scale(Radius);
		}


        var dir = player.GetInput.normalized;

		float dirInt = Mathf.Atan2(dir.y, dir.x) * _rad2Directions;
		if (dirInt < 0) dirInt += NumberOfReferencePoints;

		for (int i = -NumberOfAdjustedPoints; i < NumberOfAdjustedPoints + 1; i++)
		{
			var f = (int)dirInt + i;
			if (f < 0) f = f + NumberOfReferencePoints;
			if (f > NumberOfReferencePoints - 1) f -= NumberOfReferencePoints;
			Jelly.AddForce(dir * MS, 1 + f);
		}
	}



    internal Vector2 GetPosition()
    {
        return Jelly.ReferencePoints[0].transform.position;
    }
}
