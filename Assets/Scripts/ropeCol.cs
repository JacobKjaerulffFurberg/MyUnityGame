using UnityEngine;
using System.Collections;

public class ropeCol : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision)
	{

		/*
		if (collision.gameObject.layer == LayerMask.NameToLayer("Rope")) 
		{
			Debug.Log ("Ignored");
			Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}*/
	}
}
