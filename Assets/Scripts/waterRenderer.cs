using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRenderer : MonoBehaviour 
{
	float[] xpositions;
	float[] ypositions;
	float[] velocities;
	float[] accelerations;
	LineRenderer Body;
	GameObject[] meshobjects;
	Mesh[] meshes;
	GameObject[] colliders;
	const float springconstant = 0.02f;
	const float damping = 0.04f;
	const float spread = 0.05f;
	const float z = -1f;

	float baseheight;
	float left;
	float bottom;

	public GameObject splash;
	public Material mat;
	public GameObject watermesh;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnWater(float Left, float Width, float Top, float Bottom)
	{
		int edgecount = Mathf.RoundToInt (Width) * 5;
		int nodecount = edgecount + 1;
		Body = gameObject.AddComponent<LineRenderer>();
		Body.material = mat;
		Body.material.renderQueue = 1000;
		Body.SetVertexCount(nodecount);
		Body.SetWidth(0.1f, 0.1f);
		xpositions = new float[nodecount];
		ypositions = new float[nodecount];
		velocities = new float[nodecount];
		accelerations = new float[nodecount];

		meshobjects = new GameObject[edgecount];
		meshes = new Mesh[edgecount];
		colliders = new GameObject[edgecount];

		baseheight = Top;
		bottom = Bottom;
		left = Left;
		for (int i = 0; i < nodecount; i++)
		{
			ypositions[i] = Top;
			xpositions[i] = Left + Width * i / edgecount;
			accelerations[i] = 0;
			velocities[i] = 0;
			Body.SetPosition(i, new Vector3(xpositions[i], ypositions[i], z));
		}

		for (int i = 0; i < edgecount; i++) {
			meshes [i] = new Mesh ();
			Vector3[] Vertices = new Vector3[4];
			Vertices [0] = new Vector3 (xpositions [i], ypositions [i], z);
			Vertices [1] = new Vector3 (xpositions [i + 1], ypositions [i + 1], z);
			Vertices [2] = new Vector3 (xpositions [i], bottom, z);
			Vertices [3] = new Vector3 (xpositions [i + 1], bottom, z);
		}
	}
}
