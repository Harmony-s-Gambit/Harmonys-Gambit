using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

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
        if (players.Count == 0)
        {
            return;
        }
        redPlayer = players[0].GetComponent<Player>();
        bluePlayer = players[1].GetComponent<Player>();
        //Attack or Move
        if(!isBluePlayerPlaying)
        {
            isBlueValid = true;
        }
        if(!isRedPlayerPlaying)
        {
            isRedValid = true;
        }
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
                        AudioManager.instance.PlaySFX("Crash");
                        isStunned = true;
                        if (redNextDest == blueNextDest)
                        {
                            redPlayer.Crashed(redNextDest, redPlayer.transform.position);
                            bluePlayer.Crashed(blueNextDest, bluePlayer.transform.position);
                        }
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
                            if (tempPlayer.weapon.GetSelectorCount() > 0)
                            {
                                tempPlayer.m_Animator.SetTrigger("attack");
                                AudioManager.instance.PlaySFX("PlayerAttackEnemy");

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
                        currentEnemy.m_Animator.SetTrigger("attack");
                        currentEnemy.weapon.attackEnemies(1);
                    }
                    //�̵��϶�
                    else
                    {
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
    }
    //���� ������ ����

    //structure ������ �ߵ�
     //Hiena Targeting AI proposal
        public void resetCheck(int x, int y)
        {
            GridSlotInfo temp = GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>();
            if (temp.redDistanceCheck || temp.blueDistanceCheck)
            {
                temp.redDistance = -1;
                temp.blueDistance = -1;
                temp.redDistanceCheck = false;
                temp.blueDistanceCheck = false;
                try
                {
                    resetCheck(x + 1, y);
                }
                catch (Exception e) { }
                try
                {
                    resetCheck(x - 1, y);
                }
                catch (Exception e) { }
                try
                {
                    resetCheck(x, y + 1);
                }
                catch (Exception e) { }
                try
                {
                    resetCheck(x, y - 1);
                }
                catch (Exception e) { }
            }
        }
    
        public void RedDistance(int x, int y, int n)
        {
            GameObject tempObject = GameObject.Find(x + "_" + y);
            GridSlotInfo temp = tempObject.GetComponent<GridSlotInfo>();
            if (!temp.redDistanceCheck)
            {
            if (tempObject.tag != "Wall")
            {
                temp.redDistanceCheck = true;
                temp.redDistance = n;
                try
                {
                    RedDistance(x + 1, y, n + 1);
                }
                catch (Exception e) { }
                try
                {
                    RedDistance(x - 1, y, n + 1);
                }
                catch (Exception e) { }
                try
                {
                    RedDistance(x, y + 1, n + 1);
                }
                catch (Exception e) { }
                try
                {
                    RedDistance(x, y - 1, n + 1);
                }
                catch (Exception e) { }
            }
            }
        }

        public void BlueDistance(int x, int y, int n)
        {
            GameObject tempObject = GameObject.Find(x + "_" + y);
            GridSlotInfo temp = tempObject.GetComponent<GridSlotInfo>();
            if (!temp.blueDistanceCheck)
            {
                if (tempObject.tag != "Wall")
                {
                    temp.blueDistanceCheck = true;
                    temp.blueDistance = n;
                    try
                    {
                        BlueDistance(x + 1, y, n + 1);
                    }
                    catch (Exception e) { }
                    try
                    {
                        BlueDistance(x - 1, y, n + 1);
                    }
                    catch (Exception e) { }
                    try
                    {
                        BlueDistance(x, y + 1, n + 1);
                    }
                    catch (Exception e) { }
                    try
                    {
                        BlueDistance(x, y - 1, n + 1);
                    }
                    catch (Exception e) { }
                }
            }
        }
}


