using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrain_base : MonoBehaviour {
	public Mesh mesh;
	public MeshFilter mf;
	// Use this for initialization
	void Start () {
		//making terrain mesh
		mf = GetComponent<MeshFilter> ();
		mesh = mf.mesh;

		//vertices
		Vector2[] vertices2D = new Vector2[]{
			//front face
			new Vector2(-9f,0), //0
			new Vector2(-9f,2), //1
			new Vector2(9f,2), //2
			new Vector2(9f,0)  //3

		};

		//triangulate
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		//UV
		Vector2[] uvs = new Vector2[vertices2D.Length];
		for(int i =0 ; i<vertices2D.Length;i++){
			uvs[i] = vertices2D[i];
		}

		//convert 2d Vertices to 3d
		Vector3[] vertices3D = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices2D.Length; i++) {
			vertices3D[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		//map to mesh
		mesh.Clear();
		mesh.vertices = vertices3D;
		mesh.triangles = indices;
		mesh.uv = uvs;
		mesh.RecalculateNormals();

		//add mesh collider
		PolygonCollider2D pol_col = gameObject.AddComponent<PolygonCollider2D>();
		pol_col.points = vertices2D;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
