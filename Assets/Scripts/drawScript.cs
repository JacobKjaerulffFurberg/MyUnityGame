using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawScript : MonoBehaviour
{

	[SerializeField]
	protected LineRenderer m_LineRenderer;
	[SerializeField]
	protected bool m_AddCollider = false;
	[SerializeField]
	protected EdgeCollider2D m_EdgeCollider2D;
	[SerializeField]
	protected Camera m_Camera;
	protected List<Vector2> m_Points;

	public int count = 0;
	public int limit;

	private bool freeze = false;

	private int drawLimit = 0;

	public virtual LineRenderer lineRenderer
	{
		get
		{
			return m_LineRenderer;
		}
	}

	public virtual bool addCollider
	{
		get
		{
			return m_AddCollider;
		}
	}

	public virtual EdgeCollider2D edgeCollider2D
	{
		get
		{
			return m_EdgeCollider2D;
		}
	}

	public virtual List<Vector2> points
	{
		get
		{
			return m_Points;
		}
	}

	void Start()
	{
		drawLimit = this.transform.parent.GetComponent<Mouse_Control> ().drawLimit;
	}

	protected virtual void Awake ()
	{
		/*if (GetComponentInParent<Mouse_Control> () != null) 
		{*/
			//limit = this.transform.parent.GetComponent<Mouse_Control>().drawLimit;
		//}
		if ( m_LineRenderer == null )
		{
			Debug.LogWarning ( "DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer." );
			CreateDefaultLineRenderer ();
		}
		if ( m_EdgeCollider2D == null && m_AddCollider )
		{
			Debug.LogWarning ( "DrawLine: Edge Collider 2D not assigned, Adding and Using default Edge Collider 2D." );
			CreateDefaultEdgeCollider2D ();
		}
		if ( m_Camera == null ) {
			m_Camera = Camera.main;
		}
		m_Points = new List<Vector2> ();
	}

	protected virtual void Update ()
	{
		
		if ( Input.GetMouseButtonUp ( 0 ) )
		{
			freeze = true;
			//Reset ();
		}
		if (count > drawLimit)
		{
			freeze = true;
		}
		if ( Input.GetMouseButton ( 0 )  && !freeze)
		{
			Vector2 mousePosition = m_Camera.ScreenToWorldPoint ( Input.mousePosition );
			if ( !m_Points.Contains ( mousePosition ) )
			{
				m_Points.Add ( mousePosition );
				count++;
				this.transform.parent.GetComponent<Mouse_Control>().drawLimit--;
				//Debug.Log (count);
				m_LineRenderer.positionCount = m_Points.Count;
				m_LineRenderer.SetPosition ( m_LineRenderer.positionCount - 1, mousePosition );
				if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1) {
					m_EdgeCollider2D.points = m_Points.ToArray ();

					Vector2[] temp = m_EdgeCollider2D.points;
					Vector2 offset = new Vector2 (1.5f, -1.5f);
					for (int i = 0; i < m_EdgeCollider2D.pointCount; i++) {
						temp [i] += offset;
					}

					m_EdgeCollider2D.points = temp;
				} else {
					//m_EdgeCollider2D.pointCount = 2;
					Vector2[] array = new Vector2[2];
					array [0] = Vector2.zero;
					array [1] = Vector2.zero;
					m_EdgeCollider2D.points = array;
				}
			}

		}

		if (freeze && m_Points.Count < 2) {
			Destroy (this.gameObject);
		}
	}


	protected virtual void Reset ()
	{
		if ( m_LineRenderer != null )
		{
			m_LineRenderer.positionCount = 0;
		}
		if ( m_Points != null )
		{
			m_Points.Clear ();
		}
		if ( m_EdgeCollider2D != null && m_AddCollider) // maybe add this:  && m_AddCollider 
		{
			m_EdgeCollider2D.Reset ();
		}
	}

	protected virtual void CreateDefaultLineRenderer ()
	{
		m_LineRenderer = gameObject.AddComponent<LineRenderer> ();
		m_LineRenderer.positionCount = 0;
		m_LineRenderer.material = new Material ( Shader.Find ( "Particles/Additive" ) );
		m_LineRenderer.startColor = Color.white;
		m_LineRenderer.endColor = Color.white;
		m_LineRenderer.startWidth = 0.2f;
		m_LineRenderer.endWidth = 0.2f;
		m_LineRenderer.useWorldSpace = true;
	}
		

	protected virtual void CreateDefaultEdgeCollider2D ()
	{
		m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D> ();
		m_EdgeCollider2D.offset = (new Vector2 (0.15f, -0.075f));
		m_EdgeCollider2D.edgeRadius = 0.1f;
	}

	public void changePositions(Vector3[] positions)
	{
		freeze = true;
		if (positions.Length < 2) 
		{
			Destroy (this.gameObject);
		}
		m_LineRenderer.positionCount = positions.Length;
		m_LineRenderer.SetPositions (positions);
		if ( m_EdgeCollider2D != null)
		{
			
			Debug.Log("positions: " + positions + "\t count: " + positions.Length);
			m_EdgeCollider2D.points = Vector3toVector2(positions);

			Vector2[] temp = m_EdgeCollider2D.points;
			Vector2 offset = new Vector2 (1.5f, -1.5f);
			for (int i = 0; i < m_EdgeCollider2D.pointCount; i++)
			{
				temp[i] += offset;
			}
			Debug.Log ("New Positions");
			foreach (Vector2 element in temp)
			{
				Debug.Log (element);
			}
			Debug.Log ("End");
			m_EdgeCollider2D.points = temp;
		}
	}

	Vector2[] Vector3toVector2(Vector3[] points)
	{
		Vector2[] temp = new Vector2[points.Length];
		for (int i = 0; i < points.Length; i++)
		{
			temp[i] = new Vector2(points[i].x, points[i].y);
		}
		return temp;
	}

}