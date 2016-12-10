using UnityEngine;
using System.Collections;

/// <summary>
/// 전체적인 Raycast를 담당할 스크립트
/// </summary>
public class RaycastManager : MonoBehaviour
{
    public static RaycastManager raycastManager;
    public static RaycastManager getRaycastManager()
    {
        return raycastManager;
    }
    
    //OverlapC해주는 범위를 설정 해 줄 필요가 있다.


    //RaycastHit2D 소환에 넣어 줄 벡터.
    private Vector2 mousePosition;
    //Ray의 Hit 값을 받아 올 변수
    private RaycastHit2D rayHit;

    //근처 범위에 있는 도형 리스트
    public Collider2D[] colliderList;
    //찾는 오브젝트와의 거리 제한.
    public float findDistance = 10.0f;


    void Start()
    {
        raycastManager = this;
    }
    void Update()
    {
        _Raycast2DHit();
    }


    /// <summary>
    /// Overlap로 체크
    /// </summary>
    public void checkOverlap(GameObject gameObject)
    {
        _checkOverlap(gameObject);
    }
    private void _checkOverlap(GameObject gameObject)
    {
        colliderList = Physics2D.OverlapCircleAll
            ((Vector2)gameObject.transform.position, findDistance);

    //    PrintList();
    }
    private void PrintList()
    {
        foreach (Collider2D colRe in colliderList)
            Debug.Log(">> : " + colRe.name);
    }

    /// <summary>
    /// Ray 된 Object를 반환
    /// </summary>
    /// <returns></returns>
    public GameObject GetRayHitObject()
    {
        return rayHit.collider.gameObject;
    }
    /// <summary>
    /// ImsiPos에 마우스 값을 넣어준다.
    /// </summary>
    private void vector2MousePosition()
    {
        mousePosition = new Vector2
            (Camera.main.ScreenToWorldPoint
            (Input.mousePosition).x,
            (Camera.main.ScreenToWorldPoint
            (Input.mousePosition).y)
        );
    }

    /// <summary>
    /// Ray를 쏴서 값을 받아온다.
    /// </summary>
    ///
    public void Raycast2DHit()
    {
        _Raycast2DHit();
    }
    private void _Raycast2DHit()
    {
        vector2MousePosition();

        if (Input.GetMouseButton(0))
        {
            //raycast Hit 2D에 마우스 값을 넣어준다.
            rayHit = Physics2D.Raycast(mousePosition, Vector2.zero);

            //받아온 값이 있을 경우
            if (rayHit)
            {
                if (rayHit.collider.gameObject.layer == 8)
                {
                    //체크받은 값을 배열에 저장
                    checkOverlap(rayHit.collider.gameObject);
                    //현재 오브젝트가 범위 안에 있는가?
                    ColCheck(GameManager.gameManager.GetSelectObject());
                }
            }
            //else Debug.Log("$$ ERROR :: It is NULL");
        }
    }


    public bool ColCheck(GameObject thisObject)
    {
        bool check = false;

        foreach (Collider2D colRe in colliderList)
        {
            if (thisObject == colRe.gameObject)
            {
                //Debug.Log("YEEEEEEEEEEEEEE");

                GameManager.gameManager.RobotChange(rayHit.collider.gameObject);
                check = true;
            }
            else
            {
               // Debug.Log("ㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠ");
            }
        }
        return check;
    }

    public Collider2D[] getColliderList()
    {
        return colliderList;
    }
}
