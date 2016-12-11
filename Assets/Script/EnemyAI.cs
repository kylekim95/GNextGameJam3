using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	Vector2 curPos;
	public float patrolAreaRadius;
	public enum stateOfEnemy{patrolling,pursuing,attacking};
	public stateOfEnemy state;
	bool patrollingTo;
	float sightRange;
	float attackRange = 1;
	GameObject target;
	float moveSpeed = 3.0f;
	float patrolPointAX;
	float patrolPointBX;

	// Use this for initialization
	void Start () {
		curPos = transform.position;
		state = stateOfEnemy.patrolling;
		patrollingTo = true;
		sightRange = patrolAreaRadius / 2;
		patrolPointAX = curPos.x + patrolAreaRadius/2;
		patrolPointBX = curPos.x - patrolAreaRadius/2;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == stateOfEnemy.patrolling) {
			//enemy not within sight patrol area
			print(patrollingTo);
			if (patrollingTo) { //pointA
				transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
				print (patrolPointAX);
				print (patrolPointBX);
				if (transform.position.x > patrolPointAX)
					patrollingTo = false;
			} else { //pointB
				transform.Translate (Vector2.left * moveSpeed * Time.deltaTime);
				if (transform.position.x < patrolPointBX)
					patrollingTo = true;
			}
		} else if (state == stateOfEnemy.pursuing) {
			//pursue
			if (Vector2.Distance (target.transform.position, transform.position) <= attackRange) {
				state = stateOfEnemy.attacking;
			} else {
				transform.Translate ((target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
				if (Vector2.Distance (target.transform.position, transform.position) > patrolAreaRadius)
					state = stateOfEnemy.patrolling;
			}
		} else {
			//attack the target
			attack(2);
		}
	}

	public void attack(int damage){
		target.GetComponent<RobotsBehavior>().receiveDamage(damage);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == GameManager.gameManager.selectedObj) {
			state = stateOfEnemy.pursuing;
			target = other.gameObject;
		}
	}
}
