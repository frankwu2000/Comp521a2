using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_cannon : MonoBehaviour {
	private int counter =0;
	private Cannon_script cannon1;
	private Cannon2_script cannon2;

	// Use this for initialization
	void Start () {
		cannon1 = gameObject.GetComponentInChildren<Cannon_script>();
		cannon2 = gameObject.GetComponentInChildren<Cannon2_script>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			counter++;
			Debug.Log (cannon1.Cannon1_switch);

		}
		if (counter % 2 == 0) {
			cannon1.Cannon1_switch = true;
			cannon2.Cannon2_switch = false;
		} else {
			cannon2.Cannon2_switch = true;
			cannon1.Cannon1_switch = false;
		}
	}
}
