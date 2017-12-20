using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoftBodyFX : MonoBehaviour {
	Mesh mesh;
	private List<Vector3> vertices;
	private List<Vector3> original_vertices;
	Vector3[] normals;
	int[] triangles;
	MeshCollider meshCollider;
	Vector2[] uvs;
	//SkinnedMeshRenderer SMrenderer;
	public Material mat;
	private MeshFilter meshFilt;
	Vector2 worldpoint;
	Rigidbody rig;
	Vector2 lastspeed;
	Vector2 oldpos;
	// Use this for initialization

	void Start () 
	{
		meshFilt = GetComponent<MeshFilter>();
		mesh = GetComponent<MeshFilter>().mesh;
		mesh.MarkDynamic();
		vertices = new List<Vector3>();
		vertices.AddRange(mesh.vertices);
		original_vertices = new List<Vector3>();
		original_vertices.AddRange(mesh.vertices);
		normals = mesh.normals;
		triangles = mesh.triangles;
		meshCollider = GetComponent<MeshCollider>();
		uvs = new Vector2[vertices.Count];
		//SMrenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
		//SMrenderer.material = mat;
		//SMrenderer.sharedMesh = mesh;
		meshCollider.sharedMesh = mesh;
		rig=GetComponent<Rigidbody>();

	}

	void Update()
	{


	}
	void OnCollisionEnter2D(Collision2D col)
	{

		ContactPoint2D[] contacts = col.contacts;
		mesh = meshFilt.mesh;

		if(col.relativeVelocity.magnitude+rig.angularVelocity.magnitude>2.5f)
		{
			foreach (ContactPoint2D c in contacts)
			{

				for (int i = 0; i < vertices.Count; i++)
				{
					worldpoint = transform.TransformPoint(vertices[i]);
					//Debug.Log(vertices[i] - transform.position);
					if (Vector3.Distance(c.point, worldpoint) < 0.2f)
					{
						Vector3 deform;
						float dirX = vertices[i].x - c.normal.x;
						float dirY = vertices[i].y - c.normal.x;
						float dirZ = vertices[i].z - c.normal.x;

						deform = vertices[i] - new Vector3(dirX*0.02f, dirY*0.02f, dirZ*0.02f);

						if (Vector3.Distance(deform, transform.position) < Vector3.Distance(vertices[i],transform.position))
						{
							//Debug.Log(Vector3.Distance(deform, transform.position)+" "+Vector3.Distance(vertices[i], transform.position));
							vertices[i]= deform;
							deform = new Vector3(0f, 0f, 0f);
						}
						else
						{

							//Debug.Log("cannot deform");
						}


					}

				}

			}//for each collision point


			meshFilt.mesh.vertices = vertices.ToArray();
			meshFilt.mesh.RecalculateNormals();
			meshFilt.mesh.RecalculateBounds();
			meshCollider.sharedMesh = mesh;

			//SMrenderer.sharedMesh = mesh;


		}

		lastspeed = rig.velocity;





	}
}