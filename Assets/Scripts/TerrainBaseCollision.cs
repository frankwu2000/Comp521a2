using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBaseCollision : MonoBehaviour {
	public Mesh mesh;
	//public MeshCollider meshcollider;
	// Use this for initialization
	void Start () {
		terrain_base base_script = gameObject.GetComponent<terrain_base> ();
		mesh = base_script.mf.mesh;

		transform.gameObject.AddComponent<MeshCollider>();
		transform.GetComponent<MeshCollider>().sharedMesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
