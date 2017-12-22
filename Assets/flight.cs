using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flight : MonoBehaviour {

	// Use this for initialization
	Vector2 startPosition;
	float horizontalCounter = 1;
	float verticalCounter = 1;
	public int scale = 10;
	public int count = 0;

	public float waveLength = 10f;
	public float verticalSpeed = 0.1f;
	public float horizontalSpeed = 0.2f;

	public float flightLength = 50f;
	void Start () 
	{
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		count += (int) horizontalCounter;
		if (count > waveLength || count < 0) {
			horizontalCounter *= -1;
		}
		if (Mathf.Abs (transform.position.x - startPosition.x) > flightLength) {
			verticalCounter *= -1;
			transform.Rotate (new Vector3 (0f, 180f, 0f));
		}
		Vector2 temp = new Vector3((verticalSpeed * verticalCounter), (horizontalCounter/scale * horizontalSpeed), 0f) + transform.position;
		transform.position = temp;
	}
}
