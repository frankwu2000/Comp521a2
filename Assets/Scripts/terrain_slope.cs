using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrain_slope : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = mf.mesh;

		//vertices of mesh
		int mid_vertices = 7;
		Vector2[] vertices2D = new Vector2[4+mid_vertices+mid_vertices];
		vertices2D [0] = new Vector2 (-3f, 2); //0, start point of first slope
		vertices2D [mid_vertices+1] = new Vector2(-1f,6); //1, end point of first slope
		vertices2D [mid_vertices+2] = new Vector2(1f,6); //2, first point of second slope
		vertices2D [vertices2D.Length-1] = new Vector2(3f,2); //2, end point of second slope

		//mid vertices on first slope
		Vector2[] mid_vertices_up = Mid_Bisection_Rec(vertices2D [0],vertices2D [mid_vertices+1]);
		for(int i =0;i<mid_vertices_up.Length;i++){
			vertices2D [i + 1] = mid_vertices_up [i];
		}

		//mid vertices on second slope
		Vector2[] mid_vertices_down = Mid_Bisection_Rec(vertices2D [mid_vertices+2],vertices2D [vertices2D.Length-1]);
		for(int i =0;i<mid_vertices_down.Length;i++){
			vertices2D [i + mid_vertices+3] = mid_vertices_down [i];
		}

		//vertices [1] = Mid_Bisection (vertices [0],vertices [vertices.Length-2]);


		//triangulate
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		//UV
		Vector2[] uvs = new Vector2[vertices2D.Length];
		for(int i =0 ; i<vertices2D.Length;i++){
			uvs[i] = vertices2D[i];
		}

		//convert 2d Vertices to 3d
		Vector3[] vertices3d = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices2D.Length; i++) {
			vertices3d[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		//map to mesh
		mesh.Clear();
		mesh.vertices = vertices3d;
		mesh.triangles = indices;
		mesh.uv = uvs;
		mesh.RecalculateNormals();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//midpoint bisection
	//given two points v1 v2, return the point on the normal of v1-v2 
	Vector2 Mid_Bisection(Vector2 v1, Vector2 v2){
		
		//get vector from v1 to v2
		Vector2 v = v2 - v1;
		v.Normalize ();
		//random distance for the midpoint
		float distance = v.magnitude;
		float mid_distance = Random.Range(0.1f,distance/16);

		//get perpendicular vector 
		//by 1/2 chance goes up or down
		Vector2 mid_vec;
		if (Random.Range (0.0f, 1.0f) <= 0.5f) {
			mid_vec = new Vector2 (-v.y, v.x); //up
		} else {
			mid_vec = new Vector2 (v.y, -v.x); //down	
		}


		//get midpoint 
		Vector2 midpoint = new Vector2 ((v1.x+v2.x)/2,(v1.y+v2.y)/2);
		Vector2 final_point = new Vector2 (midpoint.x,midpoint.y);
		final_point.x += mid_vec.x * mid_distance;
		final_point.y += mid_vec.y * mid_distance;

		//return the final point
		return final_point;
	}

	//recursive midpoint bisection
	Vector2[] Mid_Bisection_Rec(Vector2 v1, Vector2 v2 ){
		//get all the new vertices

		Vector2 v3 = Mid_Bisection (v1, v2);

		Vector2 v4 = Mid_Bisection (v1, v3);
		Vector2 v5 = Mid_Bisection (v3, v2);

		Vector2 v6 = Mid_Bisection (v1, v4);
		Vector2 v7 = Mid_Bisection (v4, v3);
		Vector2 v8 = Mid_Bisection (v3, v5);
		Vector2 v9 = Mid_Bisection (v5, v2);
		//put all vertices into array and return

		Vector2[] final_vertices = new Vector2[]{
			v6, v4, v7, v3, v8, v5, v9
		};
//		Vector2[] final_vertices = new Vector2[]{
//			v4, v3, v5
//		};

		return final_vertices;	
	}
}
