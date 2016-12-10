using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour {
	public static CloneManager cm;
	public Clone[] pool;
	public CloneAwakeArea area;

	// Use this for initialization
	void Awake () {
		cm = this;
	}
	
	void Update(){
		if (area.awesome_clones) {
			for (int i = 0; i < pool.Length; i++) {
				pool [i].isActive = true;
			}
		}
	}
}
