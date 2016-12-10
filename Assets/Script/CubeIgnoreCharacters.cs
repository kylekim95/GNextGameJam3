using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIgnoreCharacters : MonoBehaviour {
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.transform.tag == "Santa") {
			Physics2D.IgnoreCollision (other.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		}
	}
}
