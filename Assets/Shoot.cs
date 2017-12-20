using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public float speed;
	public GameObject bullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			ShootBullet();
		}
		
	}

	public void ShootBullet()
	{
		GameObject temp = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
		Vector2 direction = transform.up;
		temp.GetComponent<Rigidbody2D> ().AddForce (direction * speed);
	}


}
