using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updraft : MonoBehaviour {

	public float height = 20f;
	public float updraftForce = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//float width = transform.localScale.x * this.GetComponent<Renderer>().bounds.size.x;
		//Vector3 bottomLeft = new Vector3(transform.position.x- width / 2, transform.position.y, transform.position.z);
		Vector2 bottomLeft = transform.GetChild(0).transform.position; //leftSide
		Vector2 bottomRight = transform.GetChild(1).transform.position; //rightSide

		float width = Vector3.Distance (bottomLeft, bottomRight);
		Vector2 topRight = new Vector2 (bottomRight.x, bottomRight.y + height);

		Collider2D[] colliders = selectAllObjectsInRect(bottomLeft, topRight);

		//Vector3 bottomRight = new Vector3(transform.position.x + width / 2, transform.position.y, transform.position.z);
		Vector2 topLeft = new Vector2 (bottomLeft.x, bottomLeft.y + height);

		//Debug.Log (transform.GetChild(0).name + ": " + (bottomLeft) + "\t" + transform.GetChild(1).name + ": " + (bottomRight));

		Debug.DrawLine (bottomLeft, bottomRight, Color.green);
		Debug.DrawLine (bottomRight, topRight, Color.green);
		Debug.DrawLine (topRight, topLeft, Color.green);
		Debug.DrawLine (topLeft, bottomLeft, Color.green);

		foreach (Collider2D col in colliders) {

			Vector3 force = new Vector3 (0f, 20f + updraftForce, 0f);
			col.GetComponent<Rigidbody2D>().AddForce (force);
			
		}
			

	}

	Collider2D[] selectAllObjectsInRect(Vector3 start, Vector3 end)
	{
		float x = start.x + (end.x - start.x) / 2;
		float y = start.y + (end.y - start.y) / 2;

		Vector2 middle = new Vector2 (x, y);

		//Debug.Log (middle + "\t" + (start - end));
		//Debug.DrawRay (start, start-end, Color.green);
		/*
		Rect selectionBox = new Rect(Mathf.Min(start.x, end.x), 
Screen.height - Mathf.Max(start.y, end.y), Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
		*/

		//CHANGE TO NOT SELECT ALL OBJECTS
		int tempLayerMask = 1 << 10; //Layer 10 - ball
		return Physics2D.OverlapAreaAll(start, end, tempLayerMask);
		//return Physics2D.OverlapBoxAll(middle,  new Vector2(Mathf.Abs(start.x- end.x), Mathf.Abs(start.y-end.y)), 0f, LayerMask.NameToLayer("Ground"));
	}
}
