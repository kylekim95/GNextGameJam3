using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    bool inDoor = false;
    public Transform to_Door;
    public Transform key;
    bool haveKey = false;
	// Use this for initialization
	void Start () {
		if(key != null)
        {
            haveKey = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (key != null && haveKey)
        { 
        }
        else
        {
            if (inDoor)
            {
                if (Input.GetKeyDown(KeyCode.W) && to_Door != null)
                {
                    GameManager.gameManager.selectedObj.transform.position = to_Door.transform.position + new Vector3(0, 1f, 0);
                }
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "RobotOn" || collision.transform.tag == "RobotOff")
            inDoor = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "RobotOn" || collision.transform.tag=="RobotOff")
        {
            inDoor = false;
        }
    }
}
