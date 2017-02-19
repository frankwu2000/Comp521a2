using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
	//private float BornTime;
	public bool wind_trigger
	public bool Ball_hit_slope;
	private float Barrel_angle;
	private Vector2 Init_velocity;
	public Vector2 Velocity;
	public Vector2 Position;
	private float Gravity;
	private float Air_Resis;
	private float[] Wind;
	private float CurrentWind;
	private float nextActionTime;
	public float period;


	// Use this for initialization
	void Start () {
		//wind trigger : use wind or not
		wind_trigger = false;
		//set up the time the ball is created
		//BornTime = 0;
		//set up boolean ball hit slope
		Ball_hit_slope = false;
		//set up time check for wind, 0.5 sec 
		nextActionTime = 0f;
		period = 0.5f;
		//set up winds
		Wind = new float[] {-0.025f,-0.01f,0.05f,0.15f};
		//set up gravity
		Gravity = -9.8f;
		//set up air resistance
		Air_Resis = -1.5f;

		//Cannon1
		GameObject Cannon1 = GameObject.Find("cannon1");
		Cannon_script Cannon_script = Cannon1.GetComponent<Cannon_script> ();
		//Cannon2
	//	GameObject Cannon2 = GameObject.Find("cannon2");
	//	Cannon_script Cannon2_script = Cannon2.GetComponent<Cannon2_script> ();

		//get Barrel angle
		Barrel_angle = Cannon_script.BarrelAngle.eulerAngles.z;
		//convert angle to Radian
		//Barrel_angle = 55f;
		Barrel_angle = Barrel_angle/180*Mathf.PI;
		//set up initial velocity
		Init_velocity = new Vector2 (Mathf.Cos(Barrel_angle),Mathf.Sin(Barrel_angle));
		Velocity = Init_velocity * 9f;
		//Velocity.x *= 1.5f;


	}
	
	// Update is called once per frame
	void Update () {
		
		//every 0.5 second change wind
		//randomly choose one from four winds
		if(Time.time>nextActionTime){
			nextActionTime += period;
			CurrentWind = Wind[Random.Range(0,Wind.Length-1)];
			//Debug.Log (CurrentWind + "time: " + Time.time);
		}

		if (!Ball_hit_slope) {
			//-----------------------------wind resistance--------------------------------------------
			if(wind_trigger){
				Velocity.x = Velocity.x + CurrentWind;
			}
			//----------------------------------------------------------------------------------------

			//calculate new velocity every frame according to gravity and wind-resistance
			Velocity.y = Velocity.y + Gravity * Time.deltaTime;
			//air resistance
			Velocity.x = Velocity.x + Air_Resis * Time.deltaTime;
			//moving cannon ball
			gameObject.transform.Translate (Velocity * Time.deltaTime);
			//get cannon ball position
			Position = gameObject.transform.position;

		}else {
			//calculate velocity after bouncing
			//bouncing coefficient is 0.55
			Velocity.y = -0.55f*Velocity.y+Gravity * Time.deltaTime;
			//air resistance
			Velocity.x = -0.55f*(Velocity.x);
			//moving cannon ball
			//gameObject.transform.Translate (Velocity * Time.deltaTime);
			//get cannon ball position
			Position = gameObject.transform.position;
			//after bouncing change back to normal motion
			Ball_hit_slope = false;
		}


	}
}
