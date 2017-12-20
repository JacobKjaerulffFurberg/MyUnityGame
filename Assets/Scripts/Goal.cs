using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour 
{

	// Use this for initialization
	public ParticleSystem victoryExplosion;
	private int points = 0;
	void Start () 
	{
	}

	void Update()
	{
		
	}
	void OnCollisionEnter2D (Collision2D col)
	{

		if (col.gameObject.layer == LayerMask.NameToLayer ("Ball")) 
		{
			ParticleSystem temp = Instantiate (victoryExplosion, col.gameObject.transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (col.gameObject);

			StartCoroutine(Example());
			//Application.LoadLevel (0); //Go to main menu


		}
	}
	IEnumerator Example()
	{
		yield return new WaitForSeconds(5);
		Application.LoadLevel (0); //Go to main menu
	}
}
