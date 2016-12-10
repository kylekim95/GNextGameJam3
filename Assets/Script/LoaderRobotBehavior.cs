using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderRobotBehavior : RobotsBehavior {
	public LayerMask liftable;
	public float liftrange = 5;
	bool lifting = false;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.transform.tag == "RobotOff" || other.transform.tag == "RobotOn") {
			Physics2D.IgnoreCollision (other.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		}
		if (Input.GetKeyDown (KeyCode.Mouse0) && other.gameObject.layer == LayerMask.NameToLayer("Medium") && !lifting) {
			other.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
			other.gameObject.GetComponent<Transform> ().transform.Translate(new Vector3(0,4,0));
			Transform[] children = transform.GetComponentsInChildren<Transform> ();
			foreach(Transform t in children){
				if (t.name == "Load") {
					other.transform.SetParent (t.transform);
				}
			}
			lifting = true;
			LoadOn ();
		}
		if (Input.GetKeyDown (KeyCode.Mouse1) && lifting) {
			other.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 9.81f;
			other.transform.SetParent (null);
			lifting = false;
			LoadOff ();
		}
	}

	void LoadOff(){
		setAnimation(1, "Load_Off", false, 1f);
	}

	void LoadOn(){
		setAnimation(1, "Load_On", true, 1f);
	}
}
