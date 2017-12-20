using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceBall : MonoBehaviour {


	public float speed = 25f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnCollisionStay2D (Collision2D col)
	{
		Vector2 direction = findDirectionOfNearestPoint (col.transform.position);
		if (col.transform.tag == "Player") 
		{
			Debug.Log ("forced: " + col.transform.name);
			Vector2 force = direction * speed;
			col.transform.GetComponent<Rigidbody2D> ().AddForce (force);
		}
	}

	Vector2 findDirectionOfNearestPoint(Vector2 position)
	{
		Vector2[] points = GetComponent<EdgeCollider2D> ().points;

		float shortestDistance = 20f;
		int closestPointIndex = 0;
		for (int i = 0; i < points.Length; i++)
		{
			if (Vector2.Distance (points[i], position) > shortestDistance) {
				shortestDistance = Vector2.Distance (points[i], position);
				closestPointIndex = i;
			}	
		}
		if (closestPointIndex == points.Length) {
			closestPointIndex -= 1;
		}
		Vector2 direction = points [closestPointIndex] - points [closestPointIndex + 1];
		return direction;
	}

}
