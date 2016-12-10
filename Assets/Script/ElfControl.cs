using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

[RequireComponent(typeof(RobotsBehavior))]
public class ElfControl : MonoBehaviour {

    bool hold = false;
    string cur_animation = "";
    SkeletonAnimation skel;
	// Use this for initialization
	void Start () {
        skel = GetComponentInChildren<SkeletonAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hold)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                setAnimation(1, "Push_Walk", true, 1f);
            }else
            {
                setAnimation(1, "Push_Idle", true, 1f);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Medium"))
        {
            hold = true;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Medium"))
        {
            skel.state.ClearTrack(1);
            hold = false;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {

        }
    }
    protected void setAnimation(int track, string name, bool loop, float speed)
    {
        if (cur_animation == name)
        {
            return;
        }
        else
        {
            skel.state.SetAnimation(track, name, loop).timeScale = speed;
            cur_animation = name;
        }
    }
}
