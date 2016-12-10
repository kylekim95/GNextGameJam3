using UnityEngine;
using Spine;
using Spine.Unity;
using System.Collections;

/// <summary>
/// 모든 Robot에 붙이게 될 스크립트.
/// </summary>
public class RobotsBehavior : MonoBehaviour
{
    GameManager gameManager;
	protected SkeletonAnimation skel;
	protected string cur_animation = "";
	public float axis;
    public float speed = 10.0f;
	public bool wait = false;

	private bool _isActive = false;
    public bool isActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            if (value == true)
            {
                //만약, 값이 참이라면 내부 값을 바꿔주고 현재 오브젝트를 넣는다.
                gameManager.selectedObj = gameObject;
                _isActive = true;
               // ColorRed();

                //DisActive 해제시의 애니메이션 실행
                //Active된 이미지로 셋팅.
            }
            else
            {
                _isActive = false;
              //  ColorWhite();

                //Active 해제시의 애니메이션 실행,
                //DisActive된 상태의 이미지로 셋팅.
            }
        }
    }
		
    protected void Start()
    {
		skel = GetComponentInChildren<SkeletonAnimation>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		shutDown ();
    }
    

	public bool wakeup = false;
    protected void Update()
	{
		if (!wait) {
			if (_isActive) {
				if (!wakeup) {
					Wake ();
				}
				RobotMove ();
			}
		}
	}

    private void RobotMove()
    {
		axis = Input.GetAxis ("Horizontal");
		if (axis > 0)
		{
			transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		else if (axis < 0)
		{
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
		else if(wakeup)
		{
			setAnimation(0, "Idle", true, 1f);
		}
    }

	bool firstChange = true;	
    public void _RobotChange(GameObject changeObject)
    {
        //값이 들어있는 것이 확인되었다면
		if ((changeObject != gameObject) || firstChange) {
			isActive = false;
			shutDown ();
			wakeup = false;
			changeObject.GetComponent<RobotsBehavior> ().isActive = true;
			GameManager.gameManager.selectedObj = changeObject;
			firstChange = false;
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
		
	protected void setAnimation(int track, string name, bool loop, float speed)
	{
			if (cur_animation == name) {
				return;
			} else {
				skel.state.SetAnimation (track, name, loop).timeScale = speed;
				cur_animation = name;
			}
	}

	IEnumerator Wake_cor()
	{
		wait = true;
		setAnimation(0, "Wake", false, 1f);
		while (skel.AnimationName == "Wake")
		{
			yield return null;
		}
		wakeup = true;
		wait = false;
	}
		
	IEnumerator sleep_cor()
	{
		setAnimation(0, "ShutDown", false, 1f);
		while(skel.AnimationName == "ShutDown")
		{
			yield return null;
		}
	}

	void OnTriggerStay2D(Collider2D collision){
		if (collision.transform.tag == "RobotOff" || collision.transform.tag == "RobotOn") {
			Physics2D.IgnoreCollision (collision.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		}
	}
}
