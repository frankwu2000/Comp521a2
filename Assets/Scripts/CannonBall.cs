using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
	private float Barrel_angle;
	private Vector2 Init_velocity;
	private Vector2 Velocity;
	private float Gravity;
	private float[] Wind;
	private float CurrentWind;


	// Use this for initialization
	void Start () {
		//set up winds
		Wind = new float[] {-0.025f,-0.0f,0.1f,0.5f};
		//set up cannon initial position
		gameObject.transform.position = new Vector3 (-8.1f,2.2f,0);
		//set up gravity
		Gravity = -9.8f;
		//get Barrel angle
		GameObject Cannon1 = GameObject.Find("cannon1");
		Cannon_script Cannon_script = Cannon1.GetComponent<Cannon_script> ();
		Barrel_angle = Cannon_script.BarrelAngle.eulerAngles.z;
		//convert angle to Radian
		Barrel_angle = Barrel_angle/180*Mathf.PI;
		//set up initial velocity
		Init_velocity = new Vector2 (Mathf.Cos(Barrel_angle),Mathf.Sin(Barrel_angle));
		Velocity = Init_velocity * 11f;
		//Velocity.x *= 1.5f;



	}
	
	// Update is called once per frame
	void Update () {
		
		//every 0.5 second change wind
		//randomly choose one from four winds
		if(Time.fixedTime%0.5f==0){
			CurrentWind = Wind[Random.Range(0,Wind.Length-1)];
		}

//-----------------------------wind resistance--------------------------------------------
		//Velocity.x = Velocity.x+CurrentWind;
//----------------------------------------------------------------------------------------

		//calculate new velocity every frame according to gravity and wind-resistance
		Velocity.y = Velocity.y + Gravity * Time.deltaTime;
		//moving cannon ball
		gameObject.transform.Translate( Velocity * Time.deltaTime);

	}
}
