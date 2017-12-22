using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour {

	public float count = 0;
	public float countLimit = 100;
	void Update () 
	{
		count++;
		if (count == countLimit) {
			count = 0;
			GetComponentInChildren<Animator> ().SetTrigger ("blink");
		}
	}
}
