using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{

	public float lookSpeedH = 2f;
	public float lookSpeedV = 2f;
	public float zoomSpeed = 2f;
	public float dragSpeed = 6f;

	private float yaw = 0f;
	private float pitch = 0f;

	void Update ()
	{
		//Look around with Right Mouse

		//drag camera around with Middle Mouse
		if (Input.GetMouseButton(1))
		{
			transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed,   -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);

		}

		//Zoom in and out with Mouse Wheel
		Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		//transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
	}
}