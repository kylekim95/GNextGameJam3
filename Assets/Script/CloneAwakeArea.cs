using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAwakeArea : MonoBehaviour {
	public bool awesome_clones;
	void OnTriggerEnter2D(Collider2D other){
		print ("hi");
		if (other.gameObject.layer == LayerMask.NameToLayer("Robot")) {
			awesome_clones = true;
		}
	}
}
