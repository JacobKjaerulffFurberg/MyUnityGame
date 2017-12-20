using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {


	public Text nameText;
	public Animator animator;
	public Text dialogueText;
	private Queue<string> sentences;
	// Use this for initialization
	void Awake () 
	{
		sentences = new Queue<string> ();	
	}

	public void StartDialogue(Dialogue dialogue)
	{
		
		nameText.text = dialogue.name;
		animator.SetBool ("isOpen", true);

		sentences.Clear ();

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}

		string sentence = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
	}

	void EndDialogue()
	{
		animator.SetBool ("isOpen", false);
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) 
		{
			if (letter == '-') {
				StartCoroutine (wait (3));
			} else {
				dialogueText.text += letter;
				yield return null;
			}
		}
	}
	IEnumerator wait(int seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
}
