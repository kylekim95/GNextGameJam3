using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public Transform target;
	public CameraBounds cb;
	float widthOfGameObject;
	float heightOfGameObject;

	public float bh;
	public float bw;

	public float lerpT;

	void Start(){
		cb = new CameraBounds (new Vector2(target.position.x,target.position.y),bw,bh);
		widthOfGameObject = target.GetComponent<Collider2D> ().bounds.size.x;
		heightOfGameObject = target.GetComponent<Collider2D> ().bounds.size.y;
	}

	void LateUpdate(){
		target = GameManager.gameManager.selectedObj.GetComponent<Transform> ();
		if (target.position.x - widthOfGameObject / 2 < cb.left) {
			cb.left = target.position.x - widthOfGameObject / 2;
			cb.right = cb.left + bw;
			cb.center = new Vector2 ((cb.left + cb.right) / 2,cb.center.y);
		} else if (target.position.x + widthOfGameObject / 2 > cb.right) {
			cb.right = target.position.x + widthOfGameObject / 2;
			cb.left = cb.right - bw;
			cb.center = new Vector2 ((cb.left + cb.right) / 2,cb.center.y);
		}

		if (target.position.y - heightOfGameObject / 2 < cb.bottom) {
			cb.bottom = target.position.y - heightOfGameObject / 2;
			cb.top = cb.bottom + bh;
			cb.center = new Vector2 (cb.center.x, (cb.top + cb.bottom) / 2);
		} else if (target.position.y + heightOfGameObject / 2 > cb.top) {
			cb.top = target.position.y + heightOfGameObject / 2;
			cb.bottom = cb.top - bh;
			cb.center = new Vector2 (cb.center.x, (cb.top + cb.bottom) / 2);
		}
		transform.position = Vector3.Lerp (transform.position, new Vector3(cb.center.x,cb.center.y,transform.position.z), lerpT);
	}

	public struct CameraBounds{
		public Vector2 center;
		public float left, right;
		public float top, bottom;

		public CameraBounds(Vector2 targetLoc,float _boundsWidth,float _boundsHeight){
			center = targetLoc;
			left = targetLoc.x - _boundsWidth/2;
			right = targetLoc.x + _boundsWidth/2;
			top = targetLoc.y + _boundsHeight/2;
			bottom = targetLoc.y + _boundsHeight/2;
		}
	}
}
