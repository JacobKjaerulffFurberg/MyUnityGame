using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	internal Rigidbody2D rb;

	void Start () {
		/*
		this.gameObject.AddComponent<Rigidbody2D> ();
		this.rb = this.gameObject.GetComponent<Rigidbody2D> ();
		this.rb.isKinematic = true;

		int childCount = this.transform.childCount;

		for (int i = 0; i < childCount; i++) {
			Transform t = this.transform.GetChild (i);

			t.gameObject.AddComponent<HingeJoint2D> ();

			HingeJoint2D joint = t.gameObject.GetComponent<HingeJoint2D> ();

			joint.enableCollision = true;

			joint.connectedBody = 
				i == 0 ? this.rb :
				this.transform.GetChild (i - 1).GetComponent<Rigidbody2D> ();

			//joint.connectedBody.freezeRotation = true; 

			//joint.useMotor = true;

		}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	
	}
}
