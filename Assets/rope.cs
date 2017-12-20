
using UnityEngine;

public class rope : MonoBehaviour {

	public Rigidbody2D hook;


	public GameObject linkPrefab;

	public WeightScript weight;

	private EdgeCollider2D edgeCollider;

	public int links = 7;
	// Use this for initialization
	void Start () 
	{
		edgeCollider = GetComponent<EdgeCollider2D> ();
		GenerateRope ();
		
	}

	void GenerateRope()
	{
		Rigidbody2D previousRB = hook.GetComponent<Rigidbody2D>();
		edgeCollider.points = new Vector2[links];

		for (int i = 0; i < links; i++) 
		{
			GameObject link = Instantiate (linkPrefab, transform) as GameObject;
			Physics2D.IgnoreCollision(GetComponent<EdgeCollider2D>(), link.GetComponent<Collider2D>());
			Rigidbody2D tempRB = link.GetComponent<Rigidbody2D>();
			tempRB.centerOfMass = Vector2.zero;
			tempRB.inertia = 1.0f;

			HingeJoint2D joint = link.GetComponent<HingeJoint2D> ();
			joint.connectedBody = previousRB;

			previousRB = link.GetComponent<Rigidbody2D> ();
		}

		weight.ConnectRopeEnd (previousRB);
		//previousRB
	}

	void Update()
	{
		Vector2[] temp = new Vector2[links + 1];
		for (int i = 0; i < transform.childCount; i++) 
		{
			temp [i] = transform.GetChild(i).localPosition;

		}
		edgeCollider.points = temp;
	}
}
