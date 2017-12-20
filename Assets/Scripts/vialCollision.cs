using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vialCollision : MonoBehaviour {

	public int dropletAmount = 8;
	public GameObject drop;
	private Vector3 offset;
	public float speed = 50f;
	public float explosionRadius = 5f;
	public float explosionForce = 10f;

	public GameObject vialExplosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer ("Ball")) 
		{
			Explode ();
		}
	}

	void Explode()
	{
		GameObject scriptObj = GameObject.Find ("Scripts");
		//Vector2 direction = new Vector2 (5f, 5f) * speed;
		int tempLayerMask = 1 << LayerMask.NameToLayer("Ball"); 

		Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, explosionRadius, tempLayerMask);

		foreach (Collider2D element in colliders) 
		{
			Vector2 diff = GetComponent<BoxCollider2D>().offset - new Vector2 (element.transform.position.x, element.transform.position.y);
			Rigidbody2D temp = element.transform.GetComponent<Rigidbody2D> ();
			temp.AddForce (diff * (explosionForce/Vector2.Distance(transform.position, element.transform.position)));
		}

		for (int i = 0; i < dropletAmount; i++) 
		{
			float degree = 360/dropletAmount * i;
			float radians = degree * (Mathf.PI / 180);
			Vector2 degreeVector = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
			offset = degreeVector;
			GameObject temp = Instantiate (drop, transform.position + offset, Quaternion.identity) as GameObject;
			temp.transform.parent = scriptObj.transform;
			Rigidbody2D RB = temp.GetComponent<Rigidbody2D> ();
			RB.AddForce (degreeVector*speed);

		}
		Destroy (this.gameObject);
		Instantiate (vialExplosion, transform.position, Quaternion.identity);
	}
}
