using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collision : MonoBehaviour {

	BoxCollider2D eraserCollider;
	public GameObject lineDrawer;
	void Start()
	{
		eraserCollider = GetComponent<BoxCollider2D> ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		int tempLayerMask = 1 << 8; //Layer 8 - ground

		if (col.gameObject.tag == "Ground") 
		{
			//Destroy (col.gameObject);
			GameObject line = col.gameObject;
		 	LineRenderer LR = line.GetComponent<LineRenderer> ();
			
			Vector3[] positions = new Vector3[LR.positionCount];

			LR.GetPositions (positions);

			List<Vector3> list = new List<Vector3>(positions);

			List<Vector3> toRemove = new List<Vector3> ();

			bool split = false;
			int splitIndex = 0;

			Debug.Log (col.gameObject.name);
			foreach (Vector3 position in list) 
			{
				if(GetComponent<BoxCollider2D>().bounds.Contains(position))
				{
					Debug.Log ("Remove: " + position);
					toRemove.Add (position);
					int index = list.IndexOf (position);
					if (index != list.Count && index != 0) {
						split = true;
						if (index > splitIndex) {
							splitIndex = index;
						}
					} else 
					{
						split = false;
					}
				}
			}
			Vector3[] first = new Vector3[splitIndex];
			int indexCount = 0;
			if (splitIndex > 0) {
				foreach (Vector3 element in list)
				{
					first [indexCount] = element;
					indexCount++;
					if (indexCount == splitIndex) {
						break;
					}
				}
				Debug.Log ("First split: " + indexCount);
				foreach (Vector3 element in toRemove) {
					list.Remove (element);
				}

				Vector3[] array = list.ToArray (); 
				Debug.Log ("Current list: " + list.Count + "\t to remove objects: " + toRemove.Count);

				if (!split) {
					
					LR.GetComponent<drawScript> ().changePositions (array);
				} else {
					
					Vector3[] last = new Vector3[list.Count + toRemove.Count - splitIndex];
					int count = 0;


					for (int i = splitIndex; i < array.Length-1; i++) 
					{
						last [i - splitIndex] = array [i];
						count++;
					}
					Debug.Log ("split Objects: " + count);
					GameObject LD = Instantiate (lineDrawer) as GameObject;
					LD.transform.parent = this.transform.parent;
					LD.GetComponent<drawScript> ().changePositions (first);
					LR.GetComponent<drawScript> ().changePositions (last);
				}
			}
			//int count = col.gameObject.GetComponent<drawScript> ().count;
			this.transform.GetComponentInParent<Mouse_Control> ().	drawLimit += split == false ? toRemove.Count: toRemove.Count +1;
			Destroy (col.gameObject);
		}
	}
}
