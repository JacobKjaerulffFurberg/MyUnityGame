using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{

	public float lookSpeedH = 2f;
	public float lookSpeedV = 2f;
	public float zoomSpeed = 2f;
	public float dragSpeed = 6f;
	public bool playerLock = false;

	private GameObject target;


	void Update ()
	{
		//Look around with Right Mouse
		if (playerLock) 
		{
			/*
			transform.position.z = target.position.z - distance;
			transform.position.y = target.position.y;
			transform.position.x = target.position.x;
			*/
		} 
		else 
		{
			//drag camera around with Middle Mouse
			if (Input.GetMouseButton (1)) {
				transform.Translate (-Input.GetAxisRaw ("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * dragSpeed, 0);

			}
		}
		//Zoom in and out with Mouse Wheel
		Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		//transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
	
		}
}