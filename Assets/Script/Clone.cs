using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Clone : MonoBehaviour {
	bool sameDirection;
	float axis;
	float speed = 4.0f;
	protected SkeletonAnimation skel;
	protected string cur_animation = "";
	bool wait = false;
	bool wakeup = false;
	public bool isActive = false;

	// Use this for initialization
	void Start () {
		if (transform.rotation.eulerAngles.y == GameManager.gameManager.selectedObj.transform.rotation.eulerAngles.y)
			sameDirection = true;
		else
			sameDirection = false;
		skel = GetComponentInChildren<SkeletonAnimation>();
		shutDown ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			if (!wakeup) {
				Wake ();
				wakeup = true;
			}
			cloneMove ();
		}
	}

	void cloneMove(){
		axis = sameDirection?Input.GetAxisRaw ("Horizontal"):-Input.GetAxisRaw("Horizontal");
		if (axis > 0) {
			transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, -180, 0);
		} else if (axis < 0) {
			transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		var vexPos = new Vector3
			(axis, Input.GetAxis("Vertical"), 0);
		gameObject.transform.position
		+= vexPos * speed * Time.deltaTime;
		if(vexPos != Vector3.zero)
		{
			setAnimation(0, "Run", true, 1f);
		}
		else
		{
			setAnimation(0, "Idle", true, 1f);
		}
	}

	protected void setAnimation(int track, string name, bool loop, float speed)
	{
		if (cur_animation == name) {
			return;
		} else {
			skel.state.SetAnimation (track, name, loop).timeScale = speed;
			cur_animation = name;
		}
	}

	void Wake()
	{
		StartCoroutine(Wake_cor());
	}
	void shutDown()
	{
		StartCoroutine(sleep_cor());
	}

	IEnumerator Wake_cor()
	{
		setAnimation(0, "Wake", false, 1f);
		while (skel.AnimationName == "Wake")
		{
			yield return null;
		}
	}

	IEnumerator sleep_cor()
	{
		setAnimation(0, "ShutDown", false, 1f);
		while(skel.AnimationName == "ShutDown")
		{
			yield return null;
		}
	}

}
