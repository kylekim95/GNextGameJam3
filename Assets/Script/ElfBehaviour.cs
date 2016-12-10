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
        print(canClimb);
        print(grounded);
        if (onLadder)
        {
            if (Input.GetKeyDown(KeyCode.F) && !canClimb && grounded)
            {
                skel.state.ClearTrack(0);
                canClimb = true;
            }
        }
        if (!canClimb)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if(wakeup == false)
        {
            base.Update();
        }
        else if (wakeup == true)
        {
            if (canClimb)
            {
                if (onLadder)
                {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    ladder();
                    if (Input.GetAxisRaw("Vertical") != 0)
                    {
                        skel.state.TimeScale = 1;
                        
                        GetComponent<Rigidbody2D>().transform.Translate(new Vector2(0, climbSpeed * Input.GetAxisRaw("Vertical")));

                    }
                    else
                    {
                        skel.state.TimeScale = 0;
                    }
                }
            }
            else if (holding)
            {
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 2f, GetComponent<Rigidbody2D>().velocity.y);
                    push_walk();
                }
                else
                {
                    push_idle();
                }
            }
            else
            {
                base.Update();
                if (isActive && Input.GetKeyDown(KeyCode.Space) && grounded)
                {
                    jump();

                }
                if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0, gravity * Time.deltaTime);
                }
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
            canClimb = false;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Medium")) {
			holding = true;
		}
		if (col.gameObject.layer == LayerMask.NameToLayer ("Ladder")) {
            onLadder = true;
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
		setAnimation(0, "Ladder", true, 1f);
	}

	void jumpingup(){
		setAnimation(0, "Jump_Up", true, 1f);
	}

	void jumpingdown(){
		setAnimation(0, "Jump_Down", false, 1f);
	}
}
