using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour {

	private Rigidbody2D RB;
	// Use this for initialization
	void Start () {
		RB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 pos = transform.position;
		Vector2 v = RB.velocity;
		float angle = Vector2.Angle (pos, v);
		transform.rotation = Quaternion.AngleAxis(angle, new Vector3 (0f, 0f, -1f));
		transform.localScale -= new Vector3(0.004F, 0.004F, 0.004F);
		if (transform.localScale.magnitude < 0.1)
		{
			Destroy (this.gameObject);
		}
	}
}
