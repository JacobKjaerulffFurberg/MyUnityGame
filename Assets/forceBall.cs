using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceBall : MonoBehaviour {


	public float speed = 25f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D col)
	{
		if (this.enabled == true) {
			Vector2 direction = findDirectionOfNearestPoint (col.transform.position);
			if (col.transform.tag == "Player") {
				Debug.Log ("forced: " + direction);
				Vector2 force = direction * speed;
				col.transform.GetComponent<Rigidbody2D> ().AddForce (force);
			}
		}
	}

	Vector2 findDirectionOfNearestPoint(Vector2 position)
	{
		Vector2[] points = GetComponent<EdgeCollider2D> ().points;

		float shortestDistance = 2000f;
		int closestPointIndex = 0;
		for (int i = 0; i < points.Length; i++)
		{
			if (Vector2.Distance (points[i], position) > shortestDistance) {
				shortestDistance = Vector2.Distance (points[i], position);
				closestPointIndex = i;
			}	
		}
		if (closestPointIndex == 0) {
			closestPointIndex = 1;
		}
		Vector2 direction = points [closestPointIndex] - points [closestPointIndex - 1];
		direction.Normalize ();
		return direction;
	}

}
