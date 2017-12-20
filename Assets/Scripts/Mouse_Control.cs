
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Mouse_Control : MonoBehaviour {
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>

	public GameObject floor;

	//The position you clicked
	private Vector2 targetPosition;
	//That position relative to the players current position (what direction and how far did you click?)
	private Vector2 relativePosition;

	// 2 - Store the movement
	private Vector2 movement;
	public float speed = 10;
	public float drawDistance = 0.4f;

	public int drawLimit = 200000000;


	public GameObject parentFloor = null;

	private bool cameraFree = false;
	private bool pickedRotate = false;

	public float dragSpeed = 0.8f;
	private Vector3 dragOrigin;

	private bool drawActivated;
	public Material pickedFloor;
	public Material defaultMaterial;

	private bool eraser = false;

	public GameObject eraserObj = null;
	private GameObject cursor = null;

	public float cameraZoomSpeed = 5f;

	public float rotationForce = 5f;

	private bool multiPicking = false;

	private Collider2D[] pickedObjects = new Collider2D[1];

	public Vector3 startMultiPick = new Vector3(0f, 0f, 0f);

	public Vector3 endMultiPick = new Vector3(0f, 0f, 0f);

	private Vector2 mousePosInWorld;

	public GameObject lineDrawer;

	//public RectTransform inkBar;

	public Vector3 gravity;
	public Vector3 defaultGravity;
	public Material fastLineMaterial;

	[SerializeField]
	protected LineRenderer m_LineRenderer;

	void Start()
	{
		//lineDrawer.SetActive (false);
		defaultGravity = Physics.gravity;
		gravity = defaultGravity;
		CreateDefaultLineRenderer ();
		drawActivated = true;
	}
	void Update()
	{
		Physics2D.gravity = gravity;
		/*inkBar.sizeDelta = new Vector2(
			drawLimit, 
			inkBar.sizeDelta.y);
*/
		mousePosInWorld = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);

		//Zoom in / out camera
		//Camera.main.orthographicSize += Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * cameraZoomSpeed;

		//Camera control
		cameraControl ();

		pickObject ();

			//Debug.Log ("click");

			/// MULTI PICK!



			//Debug.DrawRay (startMultiPick, mousePosInWorld, Color.green);
		/*Debug.DrawLine (startMultiPick, new Vector2 (mousePosInWorld.x, startMultiPick.y), Color.green);

			Debug.DrawLine (startMultiPick, new Vector2 (startMultiPick.x, mousePosInWorld.y), Color.green);

			Debug.DrawLine (mousePosInWorld, new Vector2 (mousePosInWorld.x, startMultiPick.y), Color.green);
			Debug.DrawLine (mousePosInWorld, new Vector2 (startMultiPick.x, mousePosInWorld.y), Color.green);
*/
			Vector3[] rectPoints = {
				startMultiPick,
				new Vector2 (mousePosInWorld.x, startMultiPick.y),
				mousePosInWorld,
				new Vector2 (startMultiPick.x, mousePosInWorld.y)
			};

			m_LineRenderer.positionCount = 5;
			m_LineRenderer.SetPosition (0, startMultiPick);
			m_LineRenderer.SetPosition (1, new Vector2 (mousePosInWorld.x, startMultiPick.y));
			m_LineRenderer.SetPosition (2, mousePosInWorld);
			m_LineRenderer.SetPosition (3, new Vector2 (startMultiPick.x, mousePosInWorld.y));
			m_LineRenderer.SetPosition (4, startMultiPick);

			/*Vector3[] tempArray = 
			{startMultiPick,
				new Vector2 (mousePosInWorld.x, startMultiPick.y), 
				mousePosInWorld, 
				new Vector2 (startMultiPick.x, mousePosInWorld.y), 
				startMultiPick};
			m_LineRenderer.SetPositions (tempArray);

			m_LineRenderer.SetPositions(tempArray);*/


		pickAllObjects ();

		if (drawActivated && Input.GetMouseButtonDown(0)) 
		{
			GameObject temp = Instantiate (lineDrawer) as GameObject;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				temp.GetComponent<forceBall> ().enabled = true;
				temp.GetComponent<LineRenderer> ().startColor = Color.red;
				temp.GetComponent<LineRenderer> ().endColor = Color.red;
				temp.GetComponent<LineRenderer> ().material = fastLineMaterial; 
			}
			temp.transform.parent = transform; 
			temp.GetComponent<drawScript> ().limit = 200;
			//lineDrawer.SetActive (true);
		}


		// ERASER!!
		/*if (Input.GetKeyDown (KeyCode.X)
		{
			eraser = true;
			cursor = Instantiate (eraserObj, mousePosInWorld, Quaternion.identity) as GameObject; 
		}*/

		if (eraser && Input.GetMouseButtonDown(0)) 
		{
			cursor = Instantiate (eraserObj, mousePosInWorld, Quaternion.identity) as GameObject; 
			cursor.transform.parent = transform;
		}
		/*
		if (Input.GetKeyUp (KeyCode.X))
		{
			eraser = false;
			if (cursor != null) 
			{
				Destroy (cursor);
			}
		}*/

		///DRAW ACTIVATED?
		/*if (Input.GetKeyDown (KeyCode.J))
		{
			drawActivated = true;
		}
		if (Input.GetKeyUp (KeyCode.J))
		{
			drawActivated = false;
		}*/




		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.LeftShift)) 
		{
			pickedRotate = true;
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			
				foreach (Collider2D element in pickedObjects) 
				{
					if (element != null)
					{
						Debug.Log ("element");
						element.gameObject.GetComponent<Renderer> ().material = defaultMaterial;
					}
				}
				pickedObjects = new Collider2D[1];
				
				//Debug.Log ("stop");

		}

		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			pickedRotate = false;
		}
		//Draw

		if (eraser && Input.GetMouseButton(0)) 
		{
			if (cursor != null) {
				cursor.transform.position = mousePosInWorld;
			}
		}

		if (Input.GetMouseButtonUp(0) && cursor != null) {
			Destroy (cursor);
		}
		/*
		else if (drawActivated) 
		{
			float distance = Mathf.Sqrt(Mathf.Pow((position.x-mousePosInWorld.x),2) + Mathf.Pow((position.y - mousePosInWorld.y), 2));
			if (curly) 
			{
				if (distance >= drawDistance)
				{
					
					//float cosA = Mathf.Pow ((position.x - mousePosInWorld.x), 2) + Mathf.Pow (distance, 2) - Mathf.Pow (position.y - mousePosInWorld.y, 2) / 2 * (position.x - mousePosInWorld.x) * distance;
					//Debug.Log (Vector2.Angle (position, mousePosInWorld));
					float angle = Mathf.Atan2 (position.y - mousePosInWorld.y, position.x - mousePosInWorld.x) * 180 / Mathf.PI;
						
					GameObject temp = Instantiate (floor, position, Quaternion.Euler(new Vector3(0f, 0f, angle))) as GameObject;
					temp.transform.localScale = (new Vector2 (1.5f, 0.2f));
					temp.transform.parent = parentFloor.transform;
					//Debug.Log ("Draw start: " + position + "\tmousePos: " + mousePosInWorld + "\t Distance" + distance);
					position = mousePosInWorld;
				}
			}
		}*/

		//Drag




		if (pickedObjects != null) 
		{
			foreach (Collider2D element in pickedObjects) 
			{
				
				if (element != null) 
				{
					element.gameObject.GetComponent<Renderer> ().material = pickedFloor;
			
					if (!pickedRotate) 
					{
						//Vector2 distance = pickedObjDistFromMouse[System.Array.IndexOf (pickedObjects, element)];

						Vector2 distance = element.GetComponent<Variables> ().offset;
							
						/*if (element.GetComponent<LineRenderer> () != null)
						{
							m_LineRenderer.SetPosition (0, startMultiPick);
							m_LineRenderer.SetPosition (1, new Vector2 (mousePosInWorld.x, startMultiPick.y));
							m_LineRenderer.SetPosition (2, mousePosInWorld);
							m_LineRenderer.SetPosition (3, new Vector2 (startMultiPick.x, mousePosInWorld.y));
							m_LineRenderer.SetPosition (4, startMultiPick);

							LineRenderer LR = element.GetComponent<LineRenderer> ();
							Vector3[] tempArray = new Vector3[LR.positionCount];

							LR.GetPositions (tempArray);


							for (int i = 0; i < tempArray.Length; i++) 
							{
								tempArray [i] = tempArray[i] + new Vector3 (tempArray [i].x - mousePosInWorld.x, tempArray [i].y - mousePosInWorld.y, 0f);
							}

							LR.SetPositions (tempArray);
							
							Vector3[] tempArray = 
							{startMultiPick,
								new Vector2 (mousePosInWorld.x, startMultiPick.y), 
								mousePosInWorld, 
								new Vector2 (startMultiPick.x, mousePosInWorld.y), 
								startMultiPick};
							m_LineRenderer.SetPositions (tempArray);
						}*/
						element.transform.position = mousePosInWorld + distance;
					} else 
					{
						Vector3 angle = element.GetComponent<Variables> ().angle;
						//Vector3 rotation = element.transform.rotation.eulerAngles;
						element.transform.eulerAngles = angle - new Vector3 (0f, 0f, mousePosInWorld.y) * rotationForce;
					}
				}
			}
		}
			
	}

	GameObject ClickSelect()
	{
		//Converting Mouse Pos to 2D (vector2) World Pos
		Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		RaycastHit2D hit=Physics2D.Raycast(rayPos, Vector2.zero, 0f);

		if (hit && hit.transform.gameObject.layer == 8) //Is it the 8th layer: "ground"?
		{
			return hit.transform.gameObject;
		}
		else return null;
	}



	Collider2D[] selectAllObjectsInRect(Vector3 start, Vector3 end)
	{
		float x = start.x + (end.x - start.x) / 2;
		float y = start.y + (end.y - start.y) / 2;

		Vector2 middle = new Vector2 (x, y);

		Debug.Log (middle + "\t" + (start - end));
		Debug.DrawRay (start, start-end, Color.green);
		/*
		Rect selectionBox = new Rect(Mathf.Min(start.x, end.x), 
Screen.height - Mathf.Max(start.y, end.y), Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
		*/

		//CHANGE TO NOT SELECT ALL OBJECTS
		int tempLayerMask = 1 << 8; //Layer 8
		return Physics2D.OverlapAreaAll(start, end, tempLayerMask);
		//return Physics2D.OverlapBoxAll(middle,  new Vector2(Mathf.Abs(start.x- end.x), Mathf.Abs(start.y-end.y)), 0f, LayerMask.NameToLayer("Ground"));
	}


	void cameraControl()
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			//cameraFree = true;
		}
		if (Input.GetMouseButtonUp (1)) 
		{
			cameraFree = false;
		}

		if (cameraFree)
		{

			Camera.main.transform.position += new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed, -1f);
			Debug.Log (Input.GetAxisRaw ("Mouse X") + "\t" + Input.GetAxisRaw ("Mouse Y"));

		}
	}

	void pickObject()
	{
		if (multiPicking) 
		{
			if (Input.GetMouseButtonDown (0)) {  
				GameObject temp = ClickSelect ();
				if (temp != null) {
					foreach (Collider2D element in pickedObjects) {
						if (element != null) {
							Debug.Log ("element");
							element.gameObject.GetComponent<Renderer> ().material = defaultMaterial;
						}
					}
					pickedObjects = new Collider2D[1];
					//pickedObjDistFromMouse = new Vector2[1];
					pickedObjects [0] = temp.GetComponent<Collider2D> ();
					//pickedObjDistFromMouse [0] = new Vector2 (Mathf.Abs (pickedObjects [0].transform.position.x - mousePosInWorld.x), Mathf.Abs (pickedObjects [0].transform.position.y - mousePosInWorld.y));
					Variables script = pickedObjects [0].GetComponent<Variables> ();
					script.offset = new Vector2 (Mathf.Abs (pickedObjects [0].transform.position.x - mousePosInWorld.x), Mathf.Abs (pickedObjects [0].transform.position.y - mousePosInWorld.y));
				}

			}
		}
	}

	void pickAllObjects()
	{
		if (multiPicking && Input.GetMouseButtonDown(0)) 
		{
			startMultiPick = mousePosInWorld;
			m_LineRenderer.enabled = true;
		}

		if (multiPicking && Input.GetMouseButtonUp (0)) 
		{
			endMultiPick = mousePosInWorld; 
			multiPicking = false;
			pickedObjects = selectAllObjectsInRect (startMultiPick, mousePosInWorld);
			for (int i = 0; i < pickedObjects.Length; i++) 
			{
				Variables script = pickedObjects[i].GetComponent<Variables>();
				script.offset = new Vector2 (Mathf.Abs (pickedObjects [i].transform.position.x - mousePosInWorld.x), Mathf.Abs (pickedObjects [i].transform.position.y - mousePosInWorld.y));
				script.angle = pickedObjects [i].transform.eulerAngles;
				//pickedObjDistFromMouse [i] = new Vector2 (Mathf.Abs (pickedObjects [i].transform.position.x - mousePosInWorld.x), Mathf.Abs (pickedObjects [i].transform.position.y - mousePosInWorld.y));
			}
			Debug.Log (pickedObjects.Length + " selected!");
			m_LineRenderer.enabled = false;
		}
		/*
		if (Input.GetKeyDown (KeyCode.G))
		{
			m_LineRenderer.enabled = true;
			multiPicking = true;
			startMultiPick = mousePosInWorld;
		}

		if (Input.GetKeyUp (KeyCode.G))
		{
			endMultiPick = mousePosInWorld; 
			multiPicking = false;
			pickedObjects = selectAllObjectsInRect (startMultiPick, mousePosInWorld);
			for (int i = 0; i < pickedObjects.Length; i++) 
			{
				Variables script = pickedObjects[i].GetComponent<Variables>();
				script.offset = new Vector2 (Mathf.Abs (pickedObjects [i].transform.position.x - mousePosInWorld.x), Mathf.Abs (pickedObjects [i].transform.position.y - mousePosInWorld.y));
				script.angle = pickedObjects [i].transform.eulerAngles;
				//pickedObjDistFromMouse [i] = new Vector2 (Mathf.Abs (pickedObjects [i].transform.position.x - mousePosInWorld.x), Mathf.Abs (pickedObjects [i].transform.position.y - mousePosInWorld.y));
			}
			Debug.Log (pickedObjects.Length + " selected!");
			m_LineRenderer.enabled = false;
		}*/
	}

	protected virtual void CreateDefaultLineRenderer ()
	{
		m_LineRenderer = gameObject.AddComponent<LineRenderer> ();
		m_LineRenderer.enabled = false;

		m_LineRenderer.positionCount = 0;
		m_LineRenderer.material = new Material ( Shader.Find ( "Particles/Additive" ) );
		m_LineRenderer.startColor = Color.green;
		m_LineRenderer.endColor = Color.green;
		m_LineRenderer.startWidth = 0.05f;
		m_LineRenderer.endWidth = 0.05f;
		m_LineRenderer.useWorldSpace = true;
	}

	public void onClick(string buttonID)
	{
		Debug.Log (buttonID);
		reset ();

		switch (buttonID) {
		case "MultiPick":
			multiPicking = true;
			break;
		case "Eraser":
			eraser = true;
			break;
		case "Draw":
			drawActivated = true;
			break;
		default:
			break;
		}


	}

	void reset()
	{
		drawActivated = false;
		//lineDrawer.SetActive (false);

		eraser = false;
		if (cursor != null) {
			Destroy (cursor);
		}

		multiPicking = false;

	}

	public void setGravity()
	{
		GameObject scriptObj = GameObject.Find("Canvas/Gravity Controller");
		float amount = (scriptObj.GetComponent<Scrollbar> ().value - 0.5f) * -2;

		gravity = defaultGravity * amount;
		Debug.Log(amount +  "\t" + Physics.gravity);
	}
}

