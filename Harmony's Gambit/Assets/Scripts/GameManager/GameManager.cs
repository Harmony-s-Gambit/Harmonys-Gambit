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

    public bool isRedPlayerPlaying = false;
    public bool isBluePlayerPlaying = false;

    public int whichDoorHasRedPlayer = -1;
    public int whichDoorHasBluePlayer = -1;

    public bool isDebugMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(players.Count == 0)
        {
            return;
        }
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
                    //���� �Ѹ��� ����
                    redPlayer.m_Animator.SetTrigger("stun");
                    bluePlayer.m_Animator.SetTrigger("stun");
                }
                if (isRedValid && isBlueValid)
                {
                    isRedValid = false; isBlueValid = false;
                    GameObject redNextDest = redPlayer.GetNextDest();
                    GameObject blueNextDest = bluePlayer.GetNextDest();
                    if (redNextDest == blueNextDest || (redNextDest == bluePlayer.currentBlock && blueNextDest == redPlayer.currentBlock))
                    {
                        //����ĭ���� Ȥ�� �پ��ִ� ���¿��� ���� �浹
                        redPlayer.m_Animator.SetTrigger("crash");
                        bluePlayer.m_Animator.SetTrigger("crash");
                        isStunned = true;
                        redPlayer.isMovedThisTurn = true;
                        bluePlayer.isMovedThisTurn = true;
                    }
                    else
                    {
                        List<GameObject> tempEnemies = new List<GameObject>();
                        //��������
                        for (int i = 0; i < 2; i++)
                        {
                            Player tempPlayer = players[i].GetComponent<Player>();
                            tempEnemies.AddRange(tempPlayer.weapon.targetEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color));

                            //�����϶�
                            for (int j = 0; j < tempEnemies.Count; j++)
                            {
                                tempEnemies[j].GetComponent<Enemy>().MoveManage();
                            }
                            tempEnemies.Clear();

                            //�����϶�
                            tempPlayer.weapon.selectEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color);
                            if(tempPlayer.weapon.GetSelectorCount() > 0)
                            {
                                tempPlayer.m_Animator.SetTrigger("attack");
                                tempPlayer.weapon.attackEnemies(1);
                            }
                            //�̵��϶�
                            else
                            {
                                tempPlayer.MoveManage();
                            }
                            tempPlayer.weapon.ClearSelector();

                        }                          
                    }
                }
            }

            //enemy move
            foreach (GameObject enemy in enemies)
            {
                Enemy currentEnemy = enemy.GetComponent<Enemy>();
                if (currentEnemy.isMovedThisTurn)
                {
                    continue;
                }
                else
                {
                    currentEnemy.weapon.selectEnemies(currentEnemy.direction, currentEnemy.x, currentEnemy.y, currentEnemy.color);
                    if (currentEnemy.weapon.GetSelectorCount() > 0)
                    {
                        Debug.Log($"{currentEnemy.x}, {currentEnemy.y}");
                        currentEnemy.weapon.attackEnemies(1);
                    }
                    //�̵��϶�
                    else
                    {
                        Debug.Log("move");
                        currentEnemy.weapon.ClearSelector();
                        currentEnemy.MoveManage();
                    }
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
