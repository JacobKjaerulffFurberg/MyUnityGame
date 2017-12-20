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
		Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		/*lr.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
		lr.material.SetTextureScale("_MainTex", new Vector2(newPosition.magnitude, 1f));*/
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
			velocity += Physics2D.gravity * Time.fixedDeltaTime;
			pos += velocity * Time.fixedDeltaTime;  
		}
		
	}
}
