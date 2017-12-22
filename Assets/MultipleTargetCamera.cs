using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour {

	public List<Transform> targets;

	public bool activated = true;
	public Vector3 offset;

	private Vector3 velocity;
	public float smoothTime = .5f;

	public float minZoom = 40f;

	public float maxZoom = 10f;

	public float zoomLimiter = 50f;
	private Camera cam;
	void Start()
	{
		cam = GetComponent<Camera> ();
		targets = new List<Transform> ();
	}
	void LateUpdate()
	{
		if (targets.Count > 0) {
			Move ();
			Zoom ();
		}
	}

	void Zoom()
	{
		float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance () / zoomLimiter);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
	}

	float GetGreatestDistance()
	{
		var bounds = new Bounds (targets [0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++) {
			bounds.Encapsulate (targets [i].position);
		}
		return bounds.size.x;
	}
	void Move()
	{
		if (targets.Count > 0 && activated) {
			Vector3 centerPoint = GetCenterPoint ();

			Vector3 newPosition = centerPoint + offset;

			//Camera.main.orthographicSize = newPosition.z;
			newPosition.z = -10f;

			transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref velocity, smoothTime);
		}
	}
	public void addTarget (Transform trans)
	{
		targets.Add (trans);
	}
	Vector3 GetCenterPoint()
	{
		if (targets.Count == 1) {
			return targets [0].position;
		}

		var bounds = new Bounds (targets [0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++) {
			bounds.Encapsulate (targets [i].position);
		}

		return bounds.center;
	}
}
