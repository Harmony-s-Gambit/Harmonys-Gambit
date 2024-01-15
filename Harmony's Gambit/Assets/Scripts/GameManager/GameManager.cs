using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


public struct dijSet
{
    public Queue<GridSlotInfo> dijSlot;
    public Dictionary<GridSlotInfo, bool> dijDic;
}

public class GameManager : MonoBehaviour
{
    private PlayerManager _playerManager;

    /*
    public Queue<GridSlotInfo> redDijSlots = new Queue<GridSlotInfo>();
    public Queue<GridSlotInfo> blueDijSlots = new Queue<GridSlotInfo>();
    */
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

    private void Start()
    {
        StartCoroutine(startDelay());
    }

    void Update()
    {
        if (players.Count == 0)
        {
            return;
        }
        redPlayer = players[0].GetComponent<Player>();
        bluePlayer = players[1].GetComponent<Player>();
        
        
        if (isRedPlayerPlaying || isBluePlayerPlaying)
        {
            if (!_playerManager.GameOver)
            {
                StartCoroutine(MainManager());
            }
        }
    }

    IEnumerator startDelay()
    {
        yield return null;
        _playerManager = FindObjectOfType<PlayerManager>();
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
            /*
            GameObject curRed = GameObject.Find(redPlayer.x + "_" + redPlayer.y);
            GameObject curBlue = GameObject.Find(bluePlayer.x + "_" + bluePlayer.y);
            curRed.GetComponent<GridSlotInfo>().redDistance = 0;
            curBlue.GetComponent<GridSlotInfo>().blueDistance = 0;
            redDijSlots.Enqueue(curRed.GetComponent<GridSlotInfo>());
            blueDijSlots.Enqueue(curBlue.GetComponent<GridSlotInfo>());
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

            */
            GridSlotInfo curRed = GameObject.Find(redPlayer.x + "_" + redPlayer.y).GetComponent<GridSlotInfo>();
            GridSlotInfo curBlue = GameObject.Find(bluePlayer.x + "_" + bluePlayer.y).GetComponent<GridSlotInfo>();

            Distance(curRed, curBlue);
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
                        for (int i = 0; i < 2; i++)
                        {
                            Player tempPlayer = players[i].GetComponent<Player>();
                            tempEnemies.AddRange(tempPlayer.weapon.targetEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color));

                            for (int j = 0; j < tempEnemies.Count; j++)
                            {
                                tempEnemies[j].GetComponent<Enemy>().MoveManage();
                            }
                            tempEnemies.Clear();

                            tempPlayer.weapon.selectEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color);
                            if (tempPlayer.weapon.GetSelectorCount() > 0)
                            {
                                tempPlayer.m_Animator.SetTrigger("attack");
                                AudioManager.instance.PlaySFX("PlayerAttackEnemy");

                                tempPlayer.weapon.attackEnemies(1);
                            }
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
        yield return new WaitForFixedUpdate();
        resetCheck(redPlayer.x, redPlayer.y);
        //yield return new WaitWhile(testfunc);
    }

    public void Distance(GridSlotInfo redStart, GridSlotInfo blueStart)
    {
        Queue<GridSlotInfo> redDijSlots = new Queue<GridSlotInfo>();
        Queue<GridSlotInfo> blueDijSlots = new Queue<GridSlotInfo>();

        Dictionary<GridSlotInfo, bool> redCheck = new Dictionary<GridSlotInfo, bool>();
        Dictionary<GridSlotInfo, bool> blueCheck = new Dictionary<GridSlotInfo, bool>();

        redStart.redDistance = 0;
        blueStart.blueDistance = 0;

        redDijSlots.Enqueue(redStart);
        blueDijSlots.Enqueue(blueStart);

        while(redDijSlots.Count != 0)
        {
            dijSet n = RD(redDijSlots, redCheck);
            redDijSlots = n.dijSlot;
            redCheck = n.dijDic;
        }
        while(blueDijSlots.Count != 0)
        {
            dijSet n = BD(blueDijSlots, blueCheck);
            blueDijSlots = n.dijSlot;
            blueCheck = n.dijDic;
        }

    }

    public dijSet RD(Queue<GridSlotInfo> redDij, Dictionary<GridSlotInfo, bool> redCheck)
    {
        GridSlotInfo temp = redDij.Dequeue();
        int x = temp.x;
        int y = temp.y;
        if (!redCheck.ContainsKey(temp))
        {
            redCheck.Add(temp, true);
            if (GameObject.Find(x + "_" + y).tag != "Wall")
            {
                try
                {
                    GridSlotInfo g = GameObject.Find((x + 1) + "_" + y).GetComponent<GridSlotInfo>();
                    redDij.Enqueue(g);
                    g.redDistance = temp.redDistance + 1;
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
                    redDij.Enqueue(g);
                    g.redDistance = temp.redDistance + 1;
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();
                    redDij.Enqueue(g);
                    g.redDistance = temp.redDistance + 1;
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y-1)).GetComponent<GridSlotInfo>();
                    redDij.Enqueue(g);
                    g.redDistance = temp.redDistance + 1;
                }
                catch (Exception) { }
            }
            else temp.redDistance = 100000;
        }
        dijSet n;
        n.dijSlot = redDij;
        n.dijDic = redCheck;
        return n;
        
    }

    public dijSet BD(Queue<GridSlotInfo> blueDij, Dictionary<GridSlotInfo, bool> blueCheck)
    {
        GridSlotInfo temp = blueDij.Dequeue();
        int x = temp.x;
        int y = temp.y;
        if (!blueCheck.ContainsKey(temp))
        {
            blueCheck.Add(temp, true);
            if (GameObject.Find(x + "_" + y).tag != "Wall")
            {
                try
                {
                    GridSlotInfo g = GameObject.Find((x + 1) + "_" + y).GetComponent<GridSlotInfo>();
                    blueDij.Enqueue(g);
                    g.blueDistance = temp.blueDistance + 1;
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
                    blueDij.Enqueue(g);
                    g.blueDistance = temp.blueDistance + 1;
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();
                    blueDij.Enqueue(g);
                    g.blueDistance = temp.blueDistance + 1;
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
                    blueDij.Enqueue(g);
                    g.blueDistance = temp.blueDistance + 1;
                }
                catch (Exception) { }

            }
            else temp.blueDistance = 100000;
        }
        dijSet n;
        n.dijSlot = blueDij;
        n.dijDic = blueCheck;
        return n;
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
                catch (Exception) { }
                try
                {
                    resetCheck(x - 1, y);
                }
                catch (Exception) { }
                try
                {
                    resetCheck(x, y + 1);
                }
                catch (Exception) { }
                try
                {
                    resetCheck(x, y - 1);
                }
                catch (Exception) { }
            }
        }
    /*
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
    */
        
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
/*
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
        }*//*
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




