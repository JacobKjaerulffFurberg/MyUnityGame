using UnityEngine;
using System.Collections;

public class startUp : MonoBehaviour {

	public Vector2 startPoint = new Vector2 (-5f, 3.2f);
	public int ballAmount = 10;
	public GameObject ball = null;
	public GameObject parentObject = null;
	public GameObject ballHolder;
	public GameObject eventSystem;
	public GameObject buttonUI;
	public GameObject textHolder;

	void Start () 
	{
		//Create balls
		//spawnBalls();
		createCanvas();
		textHolder.GetComponent<DialogueTrigger>().TriggerDialogue ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.K))
			spawnBalls ();
	}

	public void spawnBalls()
	{
		//Vector2 point = new Vector2 (startPoint.x, startPoint.y);
		GameObject temp = Instantiate (ball, ballHolder.transform.position, Quaternion.identity) as GameObject;
		temp.transform.parent = parentObject.transform;

		/*for (float i = 0; i < ballAmount; i++) 
		{
			Vector2 point = new Vector2 (startPoint.x + i, startPoint.y);
			for (float j = 0; j < ballAmount*2; j++)
			{
				point = new Vector2 (startPoint.x + i / 5, startPoint.y + j / 5);
				GameObject temp = Instantiate (ball, point, Quaternion.identity) as GameObject;
				temp.transform.parent = parentObject.transform;
			}
		}*/
	}

	public void createCanvas()
	{
		Instantiate (eventSystem);
		GameObject temp = Instantiate (buttonUI) as GameObject;
		Debug.Log (temp.name);

		int childCount = temp.transform.childCount;

		for (int i = 0; i < childCount; i++) {
			Transform child = temp.transform.GetChild (i);
			if (child.name != "Gravity Controller") 
			{
				child.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (delegate {
					SwitchButtonHandler (child.name);
				});
			}
		}
		
	}

	public void SwitchButtonHandler(string name)
	{
		Debug.Log (name);
		if (name == "Exit") {
			Application.LoadLevel (0);
		}
		if (name == "Restart") {
			Application.LoadLevel (Application.loadedLevel);
		}
		if (name == "SpawnBalls") 
		{
			spawnBalls ();
		} else {
		
			Mouse_Control script = this.GetComponent<Mouse_Control> ();

			script.onClick (name);
		}
	}
}
