using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;
	public bool firstChoice = true;

    void Awake()
    {
		if (selectedObj == null) {
			selectedObj = new GameObject ();
			selectedObj.transform.position = new Vector3 (0, 0, 0);
		}
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
  	
    //현재 움직이고 있는 로봇을 받는다.
    public GameObject selectedObj;
		
    public void RobotChange(GameObject changeObject)
    {
		//selectedObj.GetComponent<RobotsBehavior> ().wait = true;
        selectedObj.GetComponent<RobotsBehavior>()._RobotChange(changeObject);
		//selectedObj.GetComponent<RobotsBehavior> ().wait = false;
    }
    public GameObject GetSelectObject()
    {
        return selectedObj;
    }
}
