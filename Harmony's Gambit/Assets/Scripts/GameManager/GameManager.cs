using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;




public class GameManager : MonoBehaviour
{
    public Queue<GridSlotInfo> redDijSlots = new Queue<GridSlotInfo>();
    public Queue<GridSlotInfo> blueDijSlots = new Queue<GridSlotInfo>();


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
        StartCoroutine(MainManager());
    }

    IEnumerator MainManager()
    {
        //Attack or Move
        if (!isBluePlayerPlaying)
        {
            isBlueValid = true;
        }
        if (!isRedPlayerPlaying)
        {
            isRedValid = true;
        }
        if (rhythm)
        {
            resetCheck(redPlayer.x, redPlayer.y);
            GameObject curRed = GameObject.Find(redPlayer.x + "_" + redPlayer.y);
            GameObject curBlue = GameObject.Find(bluePlayer.x + "_" + bluePlayer.y);
            curRed.GetComponent<GridSlotInfo>().redDistance = 0;
            curBlue.GetComponent<GridSlotInfo>().blueDistance = 0;
            RedDistance(redPlayer.x, redPlayer.y);
            BlueDistance(bluePlayer.x, bluePlayer.y);
            while (redDijSlots.Count != 0)
            {
                GridSlotInfo t = redDijSlots.Dequeue();
                RedDistance(t.x, t.y);
            }
            while (blueDijSlots.Count != 0)
            {
                GridSlotInfo t = blueDijSlots.Dequeue();
                BlueDistance(t.x, t.y);
            }
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
            Debug.Log(GameObject.Find("11_18").GetComponent<GridSlotInfo>().redDistance + " " + GameObject.Find("11_18").GetComponent<GridSlotInfo>().blueDistance);
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
        yield return new WaitWhile(testfunc);
    }

    bool testfunc() {
        if (redDijSlots.Count == 0 || blueDijSlots.Count == 0)
        {
            return true;
        }
        else return false;
    }

    //���� ������ ����

    //structure ������ �ߵ�
     //Hiena Targeting AI proposal
        public void resetCheck(int x, int y)
        {
            GridSlotInfo temp = GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>();
            if (temp.redDistanceCheck || temp.blueDistanceCheck)
            {
                temp.redDistance = 100000;
                temp.blueDistance = 100000;
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
    
        public void RedDistance(int x, int y)
        {
            GameObject tempObject = GameObject.Find(x + "_" + y);
            GridSlotInfo temp = tempObject.GetComponent<GridSlotInfo>();

        if (temp.redDistanceCheck) return;
        else {
            temp.redDistanceCheck = true;
            if (tempObject.tag != "Wall")
            {
                temp.redDistanceCheck = true;
                try
                {
                    GridSlotInfo g = GameObject.Find((x + 1) + "_" + y).GetComponent<GridSlotInfo>();
                    if (g.redDistance > temp.redDistance)
                    {
                        redDijSlots.Enqueue(g);
                        g.redDistance = temp.redDistance + 1;
                    }
                }
                catch (Exception e) { };

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
                    if (g.redDistance > temp.redDistance)
                    {
                        redDijSlots.Enqueue(g);
                        g.redDistance = temp.redDistance + 1;
                    }
                }
                catch (Exception e) { }
                try
                {
                    GridSlotInfo g = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();
                    if (g.redDistance > temp.redDistance + 1)
                    {
                        redDijSlots.Enqueue(g);
                        g.redDistance = temp.redDistance + 1;
                    }
                }
                catch (Exception e) { }
                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
                    if (g.redDistance > temp.redDistance + 1)
                    {
                        redDijSlots.Enqueue(g);
                        g.redDistance = temp.redDistance + 1;
                    }
                }catch(Exception e) { }
            }
        }

        
        /*if (!temp.redDistanceCheck)
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
        }*/
        }

        public void BlueDistance(int x, int y)
        {
        
        GameObject tempObject = GameObject.Find(x + "_" + y);
        GridSlotInfo temp = tempObject.GetComponent<GridSlotInfo>();

        if (temp.blueDistanceCheck) return;
        else
        {
            temp.blueDistanceCheck = true;
            if (tempObject.tag != "Wall")
            {
                temp.blueDistanceCheck = true;
                try
                {
                    GridSlotInfo g = GameObject.Find((x + 1) + "_" + y).GetComponent<GridSlotInfo>();
                    if (g.blueDistance > temp.blueDistance)
                    {
                        blueDijSlots.Enqueue(g);
                        g.blueDistance = temp.blueDistance + 1;
                    }
                }
                catch (Exception e) { };

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
                    if (g.blueDistance > temp.blueDistance)
                    {
                        blueDijSlots.Enqueue(g);
                        g.blueDistance = temp.blueDistance + 1;
                    }
                }
                catch (Exception e) { }
                try
                {
                    GridSlotInfo g = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();
                    if (g.blueDistance > temp.blueDistance + 1)
                    {
                        redDijSlots.Enqueue(g);
                        g.blueDistance = temp.blueDistance + 1;
                    }
                }
                catch (Exception e) { }
                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
                    if (g.blueDistance > temp.blueDistance + 1)
                    {
                        redDijSlots.Enqueue(g);
                        g.blueDistance = temp.blueDistance + 1;
                    }
                }
                catch (Exception e) { }
            }
        }/*
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
        }*/
    }
}


