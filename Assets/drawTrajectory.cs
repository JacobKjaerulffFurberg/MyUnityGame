using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawTrajectory : MonoBehaviour {

	private LineRenderer lr;
	// Use this for initialization
	void Start () 
	{
		lr = GetComponent<LineRenderer> ();
		lr.enabled = true;
		lr.positionCount = 100;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 velocity = GetComponent<Rigidbody2D> ().velocity;
		Vector2 pos = transform.position;

		int count = lr.positionCount;

		for (int i = 0; i < count; i++) 
		{
			lr.SetPosition(i, pos);
			Debug.Log (Physics2D.gravity);
			velocity += Physics2D.gravity * Time.fixedDeltaTime;
			pos += velocity * Time.fixedDeltaTime;  
		}
		
	}
}
