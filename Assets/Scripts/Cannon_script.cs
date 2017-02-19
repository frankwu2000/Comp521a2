using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_script : MonoBehaviour {
	public bool Cannon1_switch;
	public Quaternion BarrelAngle;
	public GameObject cannon_ball;
	public Vector2 Cannon_Position;
	private MeshFilter mf;
	private bool up;
	// Use this for initialization
	void Start () {
		//set up cannon toggle
		Cannon1_switch = true;
		//making cannon barrel mesh
		mf = GetComponent<MeshFilter> ();
		Mesh mesh = mf.mesh;

		// Create Vector2 vertices
		Vector2[] vertices2D = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,0.4f),
			new Vector2(1,0.4f),
			new Vector2(1,0),
		};

		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		mesh.vertices = vertices;
		mesh.triangles = indices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		//transfer cannon
		mf.transform.position = new Vector2 (-8.2f,2.2f);

		//create wheel
		GameObject wheel = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		wheel.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
		wheel.transform.position = new Vector3 (-8.2f,2.2f);
		//get cannon position
		Cannon_Position = mf.transform.position;

		//wheel rotation boolean
		up = true;


	}
	
	// Update is called once per frame
	void Update () {
		
		//cannon will elevate up and down
		if (mf.transform.rotation.z > 0.45f) {
			up = false;
		}
		if (mf.transform.rotation.z < 0.0f) {
			up = true;
		}

		if (up) {
			mf.transform.Rotate (new Vector3(0,0,15f)*Time.deltaTime);
		}

		if (!up) {
			mf.transform.Rotate (new Vector3(0,0,-15f)*Time.deltaTime);
		}
		//cannon ball
		if (Input.GetKeyDown (KeyCode.Space) && Cannon1_switch) {
			//get realtime cannon angle
			BarrelAngle = mf.transform.rotation;
			Instantiate (cannon_ball,Cannon_Position,BarrelAngle);
		}



	}
}
