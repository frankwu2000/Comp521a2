using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCollision : MonoBehaviour {
	public Vector2 old_position;
	public Vector2 col_velocity;
	private Vector2 col_position;
	// Use this for initialization
	void Start () {
		//store old position;
		old_position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		//get velocity and position of the cannon
		CannonBall BallScript = gameObject.GetComponent<CannonBall>();
		col_velocity = BallScript.Velocity;
		col_velocity.Normalize ();
		col_position = BallScript.Position;


//		//if the ball velocity is smaller then threshold , set to zero
//		if(Mathf.Abs(col_velocity.x) < 0.1f && Mathf.Abs(col_velocity.y) <0.1f){
//			//BallScript.ball_stop = true;
//		}

		//if the ball is not moving for 4 seconds, destroy
		if (old_position != col_position) {
			old_position = col_position;
		} else {
			StartCoroutine (ExecuteAfterTime (4));
		}


		//if ball goes outside border, destroy it
		if(col_position.x <-9 || col_position.x >9 || col_position.y < 0 || col_position.y>9){
			Destroy(gameObject);
		}
		//doing raycast for collision detection 
		RaycastHit2D hit = Physics2D.Raycast (col_position, col_velocity, 0.22f);
		//if ball hit plain, destroy
		if (hit.collider.gameObject.tag == "terrain_plain") {
			Destroy(gameObject);
		}
		//if ball hit mountain , handling bouncing in the CannonBall script
		if (hit.collider.gameObject.tag == "terrain_slope") {
			BallScript.Ball_hit_slope = true;
		}


	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		Destroy (gameObject);
	}
}
