using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class EnemyAI : MonoBehaviour {
    public GameObject key;

	public SkeletonAnimation skel;
	public string cur_animation = "";

	float moveSpeed = 5.0f;
	bool cont = true;
	bool withinAttackRad = false;

	// Use this for initialization
	void Start () {
        skel = GetComponentInChildren<SkeletonAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
		while (cont) {
			if (!withinAttackRad) {
				transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
				setAnimation(0,"Run",true,1f);
			}
			else
				attack ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == GameManager.gameManager.selectedObj) {
			withinAttackRad = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == GameManager.gameManager.selectedObj) {
			withinAttackRad = false;
		}
	}

	void attack(){
		setAnimation(0,"Attack1",false,1f);
	}

	public void setAnimation(int track, string name, bool loop, float speed)
	{
		if (cur_animation == name) {
			return;
		} else {
			skel.state.SetAnimation (track, name, loop).timeScale = speed;
			cur_animation = name;
		}
	}
}
