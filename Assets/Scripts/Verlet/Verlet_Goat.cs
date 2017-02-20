using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verlet_Goat : MonoBehaviour {
	public GameObject Head;
	public GameObject Body;
	public GameObject Leg1;
	public GameObject Leg2;

	// Use this for initialization
	void Start () {
		//get child gameobject
		Head = transform.FindChild ("Head").gameObject;
		Body = transform.FindChild ("Body").gameObject;
		Leg1 = transform.FindChild ("Leg1").gameObject;
		Leg2 = transform.FindChild ("Leg2").gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.Translate (new Vector2(0,1) * Time.deltaTime);
	}
}
