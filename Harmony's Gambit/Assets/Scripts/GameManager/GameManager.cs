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
        //Attack or Move
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
                        List<GameObject> tempEnemies = new List<GameObject>();
                        //��������
                        for(int i = 0; i < 2; i++)
                        {
                            Player tempPlayer = players[i].GetComponent<Player>();   
                            tempEnemies.AddRange(tempPlayer.weapon.targetEnemies(tempPlayer.direction,tempPlayer.x,tempPlayer.y,tempPlayer.color));
                        }
                        //�����϶�
                        for(int i = 0; i < tempEnemies.Count; i++)
                        {
                            if (tempEnemies[i].GetComponent<Enemy>().MoveManage())
                            {
                                tempEnemies[i].GetComponent<Enemy>().Move(tempEnemies[i].GetComponent<Enemy>().GetNextDest());
                            }
                        }
                        tempEnemies.Clear();
                        for(int i = 0; i < 2; i++)
                        {
                            Player tempPlayer = players[i].GetComponent<Player>();
                            tempPlayer.weapon.selectEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color);
                            tempPlayer.weapon.attackEnemies(1);
                        }
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

                    //�ȿ� ������ ����

                    //������ �̵�
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

        //���� ������ ����

        //structure ������ �ߵ�

    }
}
