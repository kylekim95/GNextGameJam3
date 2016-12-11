using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


public class HandleScript : MonoBehaviour {
    public GameObject shelf;
    bool inTrigger = false;
    string cur_animation = "";
    public SkeletonAnimation anim;
    // Use this for initialization
    // Update is called once per frame

    private void Start()
    {
        setAnimation(0, "Off", true, 1f);
    }
    
	void Update () {
        
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F) && shelf != null)
            {
                setAnimation(0, "On", false, 1f);
                Destroy(shelf);
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Robot"))
        {
            inTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Robot"))
        {
            inTrigger = false;
        }
    }
    void setAnimation(int track, string name, bool loop, float speed)
    {
        if (cur_animation == name)
        {
            return;
        }
        else
        {
            anim.state.SetAnimation(0, name, true).TimeScale = speed;
            cur_animation = name;
        }
    }
}
