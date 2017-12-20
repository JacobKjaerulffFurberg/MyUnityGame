using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

	public float speed = 4f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer ("Ball"))
		{
			Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D> ();

			rb.AddForce (Vector2.up * speed);

		}
	}
}
