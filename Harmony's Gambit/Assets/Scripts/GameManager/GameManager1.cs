using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class GameManager1 : MonoBehaviour
{
    //saved enemies and players
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();

    //targeting redplayer, blueplayer
    public Player rp, bp;

    //boolean to check whether player is available to move
    public bool isRedValid = false;
    public bool isBlueValid = false;
    public bool isStunned = false;
    public bool rhythm = false;

    public int whichDoorHasRedPlayer = -1;
    public int whichDoorHasBluePlayer = -1;

    //use for debugging
    public bool isDebugMode = false;

    IEnumerator Turn(bool pMove)
    {

        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count == 0) return;

        rp = players[0].GetComponent<Player>();
        bp = players[0].GetComponent<Player>();

        if (rhythm) {
            //when both Red and Blue succeeded to hit the ryhthmn
            if (isRedValid && isBlueValid)
            {
                //플레이어 이동 경로 저장
                //같은 경로일 경우 충돌 애니메이션 + 효과음
                rp.m_Animator.SetTrigger("crash");
                bp.m_Animator.SetTrigger("crash");
                isStunned = true;
            }
            //When only one of Red or Blue succeeded to hit the rythmn
            else if (isRedValid ^ isBlueValid)
            {
                rp.m_Animator.SetTrigger("stun");
                bp.m_Animator.SetTrigger("stun");
            }
            else { 
            
            }
        }

        //player move
        //enemy move
    }
}
