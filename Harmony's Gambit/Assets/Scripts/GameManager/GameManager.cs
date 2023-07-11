using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private Player redPlayer;
    private Player bluePlayer;
    public bool isRedValid = false;
    public bool isBlueValid = false;
    public bool isStunned = false;
    public bool rhythm = false;
    // Start is called before the first frame update
    void Start()
    {
        bluePlayer = players[0].GetComponent<Player>();
        redPlayer = players[1].GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rhythm)
        {
            //player Move
            rhythm = false;
            if (isStunned)
            {
                isStunned = false;
            }
            else
            {
                if(isRedValid ^ isBlueValid)
                {
                    //둘중 한명만 성공
                }
                if (isRedValid && isBlueValid)
                {
                    isRedValid = false; isBlueValid = false;
                    GameObject redNextDest = redPlayer.GetNextDest();
                    GameObject blueNextDest = bluePlayer.GetNextDest();
                    if (redNextDest == blueNextDest || (redNextDest == bluePlayer.currentBlock && blueNextDest == redPlayer.currentBlock))
                    {
                        //같은칸으로 혹은 붙어있는 상태에서 서로 충돌
                        isStunned = true;
                    }
                    else
                    {
                        //공격판정

                        //공격일때

                        //이동일때
                        redPlayer.MoveManage();
                    }
                }
            }   
        
            //enemy move

        }

        //isMovedThisTurn = false 로 초기화
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().isMovedThisTurn = false;
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().isMovedThisTurn = false;
        }

    }
}
