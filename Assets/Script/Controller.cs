using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	Rigidbody2D rb;
	bool isActive;
	float movespeed = 6;
	Vector2 velocity;

	Vector2 curVelocity; 
	public float smoothDuration;

	// Use this for initialization
	void Start () {
		isActive = false;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			//take input
			velocity = (new Vector2(Input.GetAxisRaw("Horizontal"),0).normalized) * 6;
			if (Input.GetKeyDown (KeyCode.Space)) {
				rb.velocity += (Vector2.up * 5);
			}
		} else {
			//sleep
			if (Input.GetKeyDown (KeyCode.Space)) {
				wake();
			}
		}
	}

	void FixedUpdate(){
		rb.velocity = new Vector2(velocity.x,rb.velocity.y);
	}

	void wake(){
		isActive = true;
	}
}
