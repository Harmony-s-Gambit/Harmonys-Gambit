using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    public Player redPlayer;
    public Player bluePlayer;
    public bool isRedValid = false;
    public bool isBlueValid = false;
    public bool isStunned = false;
    public bool rhythm = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        redPlayer = players[0].GetComponent<Player>();
        bluePlayer = players[1].GetComponent<Player>();
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
                if (isRedValid ^ isBlueValid)
                {
                    //���� �Ѹ� ����
                }
                if (isRedValid && isBlueValid)
                {
                    isRedValid = false; isBlueValid = false;
                    GameObject redNextDest = redPlayer.GetNextDest();
                    GameObject blueNextDest = bluePlayer.GetNextDest();
                    if (redNextDest == blueNextDest || (redNextDest == bluePlayer.currentBlock && blueNextDest == redPlayer.currentBlock))
                    {
                        //����ĭ���� Ȥ�� �پ��ִ� ���¿��� ���� �浹
                        isStunned = true;
                        redPlayer.isMovedThisTurn = true;
                        bluePlayer.isMovedThisTurn = true;
                    }
                    else
                    {
                        //��������

                        //�����϶�

                        //�̵��϶�
                        redPlayer.MoveManage();
                        bluePlayer.MoveManage();
                    }
                }
            }   
        
            //enemy move
            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<Enemy>().isMovedThisTurn)
                {

                }
                else
                {
                    //��������

                    //������ ��

                    //�̵��� ��
                    Debug.Log("Here");
                    enemy.GetComponent<Enemy>().MoveManage();
                }
            }
        }

        //isMovedThisTurn = false �� �ʱ�ȭ
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
