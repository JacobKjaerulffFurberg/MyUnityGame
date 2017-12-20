using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attraction : MonoBehaviour {

	// Use this for initialization
	private Collider2D gravity;

	public float StrengthOfAttraction;
	private List<Collider2D> triggerList = new List<Collider2D> ();

	void Start () 
	{
		gravity = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (Collider2D element in triggerList)
		{
			if (element.attachedRigidbody != null) 
			{
				GameObject planet = element.transform.gameObject;
				float magsqr;

				//offset is the distance to the planet
				Vector3 offset;

				//get offset between each planet and the player
				offset = planet.transform.position - transform.position;

				//My game is 2D, so  I set the offset on the Z axis to 0
				offset.z = 0;

				//Offset Squared:
				magsqr = offset.sqrMagnitude;

				//Check distance is more than 0 to prevent division by 0
				if (magsqr > 0.0001f) {
					//Create the gravity- make it realistic through division by the "magsqr" variable

					element.attachedRigidbody.AddForce ((StrengthOfAttraction * offset.normalized / magsqr) * element.attachedRigidbody.mass);
				}
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		
	
		if(!triggerList.Contains(other))
		{
			//add the object to the list
			triggerList.Add(other);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		triggerList.Remove (other);
	}

}
