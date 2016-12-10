using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfBehaviour : RobotsBehavior {
	public float jumpheight = 4f;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	float gravity;
	float jumpVelocity;

	public bool grounded = false;
	float climbSpeed = 0.1f;
	public bool holding;
	bool canClimb;
	bool onLadder = false;

	void Start(){
		base.Start ();
		gravity = -(2 * jumpheight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
	}

	void Update(){
		base.Update ();
		if (canClimb) {
			if (Input.GetAxisRaw ("Vertical") != 0) {
				onLadder = true;
				skel.state.TimeScale = 1;
				GetComponent<Rigidbody2D> ().gravityScale = 0;
				GetComponent<Rigidbody2D> ().transform.Translate(new Vector2 (0, climbSpeed * Input.GetAxisRaw("Vertical")));
				ladder ();
			} else if(onLadder) {
				skel.state.TimeScale = 0;
			}
		}
		else if(holding) {
			float input = Input.GetAxisRaw ("Horizontal");
			if (input != 0) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (input * 2f,GetComponent<Rigidbody2D>().velocity.y);
				push_walk ();
			} else {
				push_idle ();
			}
		}
		else {
			
			if (isActive && Input.GetKeyDown (KeyCode.Space) && grounded) {
				jump ();
				jumpingup ();
			}
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > 0) {
				gameObject.GetComponent<Rigidbody2D> ().velocity += new Vector2 (0, gravity * Time.deltaTime);
			} 
		}
	}

	void jump(){
		gameObject.GetComponent<Rigidbody2D> ().velocity += new Vector2 (0,jumpVelocity);
		grounded = false;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer("Ground") || col.gameObject.layer == LayerMask.NameToLayer("Medium")) {
			grounded = true;
			onLadder = false;
			skel.state.ClearTrack (1);
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Medium")) {
			holding = true;
		}
		if (col.gameObject.layer == LayerMask.NameToLayer ("Ladder")) {
			canClimb = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Medium")) {
			holding = false;
		}
		if (col.gameObject.layer == LayerMask.NameToLayer ("Ladder")) {
			canClimb = false;
			onLadder = false;
		}
	}

	void push_idle(){
		setAnimation(0, "Push_Idle", true, 1f);
	}

	void push_walk(){
		setAnimation(0, "Push_Walk", true, 1f);
	}

	void ladder(){
		setAnimation(2, "Ladder", true, 1f);
	}

	void jumpingup(){
		setAnimation(1, "Jump_Up", true, 1f);
	}

	void jumpingdown(){
		setAnimation(0, "Jump_Down", false, 1f);
	}
}
