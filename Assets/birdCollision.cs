using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdCollision : MonoBehaviour {
	
	public GameObject birdDeath;
	private Animator ani;
	// Use this for initialization
	void Start()
	{
		ani = GetComponent<Animator> ();
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			Debug.Log ("hit!");
		}
		ani.SetBool ("isHit", true);
		StartCoroutine (waitSeconds(2));
		Instantiate (birdDeath, transform);
	}

	IEnumerator waitSeconds (int seconds)
	{
		yield return new WaitForSeconds(seconds);
		ani.SetBool ("isHit", false);
	}
}
