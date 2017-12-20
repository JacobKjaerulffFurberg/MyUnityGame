using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	void Start()
	{
		foreach (string str in dialogue.sentences)
		{
			Debug.Log(str);
		}

		Debug.Log (dialogue + "     " + dialogue.name);
	}

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
	}
}
