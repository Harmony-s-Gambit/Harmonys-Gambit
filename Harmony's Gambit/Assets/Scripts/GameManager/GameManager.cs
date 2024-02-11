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
    public List<GameObject> items = new List<GameObject>();

    public Player redPlayer;
    public Player bluePlayer;

    public bool isRedValid = false;
    public bool isBlueValid = false;
    public bool isStunned = false;
    public bool rhythm = false;
    public bool isGameStart = false;

    public bool isRedPlayerPlaying = false;
    public bool isBluePlayerPlaying = false;

    public int whichDoorHasRedPlayer = -1;
    public int whichDoorHasBluePlayer = -1;

    public bool isDebugMode = false;

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

    public void SetStart()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        ScoreManager.instance.TotalEnemyScore(enemies);
    }


    IEnumerator MainManager()
    {
        //Attack or Move
        if (!isBluePlayerPlaying)
        {
            isBlueValid = true;
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().changeTarget(COLOR.RED);
            }
        }
        if (!isRedPlayerPlaying)
        {
            isRedValid = true;
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().changeTarget(COLOR.BLUE);
            }
        }
        if (rhythm)
        {
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
                    redPlayer.m_Animator.Play("stun", -1, 0);
                    bluePlayer.m_Animator.Play("stun", -1, 0);
                }
                if (isRedValid && isBlueValid)
                {

                    //플레이어 움직임 설정순서

                    if(isRedPlayerPlaying && isBluePlayerPlaying)
                    {
                        redPlayer.weapon.targetEnemies(redPlayer.direction, redPlayer.x, redPlayer.y, COLOR.RED);
                        bluePlayer.weapon.targetEnemies(bluePlayer.direction, bluePlayer.x, bluePlayer.y, COLOR.BLUE);
                        if(redPlayer.weapon.Selector.Count != 0)
                        {
                            redPlayer.weapon.Attack = true;
                            redPlayer.weapon.attackEnemies(1);
                            redPlayer.weapon.ClearSelector();
                            redPlayer.isMovedThisTurn = true;
                        }

                        if(bluePlayer.weapon.Selector.Count != 0)
                        {
                            bluePlayer.weapon.Attack = true;
                            bluePlayer.weapon.attackEnemies(1);
                            bluePlayer.weapon.ClearSelector();
                            bluePlayer.isMovedThisTurn = true;
                        }
                    }
                    else if(isRedPlayerPlaying)
                    {
                        redPlayer.weapon.targetEnemies(redPlayer.direction, redPlayer.x, redPlayer.y, COLOR.RED);
                        if(redPlayer.weapon.Selector.Count != 0)
                        {
                            redPlayer.weapon.Attack = true;
                            redPlayer.weapon.attackEnemies(1);
                            redPlayer.weapon.ClearSelector();
                            redPlayer.isMovedThisTurn = true;
                        }
                    }
                    else if (isBluePlayerPlaying)
                    {
                        bluePlayer.weapon.targetEnemies(bluePlayer.direction, bluePlayer.x, bluePlayer.y, COLOR.BLUE);
                        if (bluePlayer.weapon.Selector.Count != 0)
                        {
                            bluePlayer.weapon.Attack = true;
                            bluePlayer.weapon.attackEnemies(1);
                            bluePlayer.weapon.ClearSelector();
                            bluePlayer.isMovedThisTurn = true;
                        }
                    }


                    //공격 불가능시 이동범위 위치 찾기
                    GameObject redDest, blueDest;
                    redDest = redPlayer.GetNextDest();
                    blueDest = bluePlayer.GetNextDest();

                    if (!redPlayer.isMovedThisTurn && !bluePlayer.isMovedThisTurn)
                    {
                        if(redDest != blueDest) {
                            redPlayer.MoveManage();
                            bluePlayer.MoveManage();
                        }
                        else
                        {
                            redPlayer.Crashed(redDest, redPlayer.transform.position);
                            bluePlayer.Crashed(blueDest, bluePlayer.transform.position);
                        }
                    }else if (!redPlayer.isMovedThisTurn)
                    {
                        redPlayer.MoveManage();
                    }else if (!bluePlayer.isMovedThisTurn)
                    {
                        bluePlayer.MoveManage();
                    }


                }
            }

            //적 움직임 설정 순서

            //공격범위 설정
            //공격 가능시 공격


            foreach (GameObject enemy in enemies)
            {
                Enemy tempEnemy = enemy.GetComponent<Enemy>();
                tempEnemy.weapon.targetEnemies(tempEnemy.direction, tempEnemy.x, tempEnemy.y, COLOR.PURPLE);
                if (tempEnemy.weapon.Selector.Count != 0)
                {
                    tempEnemy.weapon.Attack = true;
                    tempEnemy.weapon.attackEnemies(1);
                    tempEnemy.isMovedThisTurn = true;
                }
                tempEnemy.weapon.ClearSelector();
                if (!tempEnemy.isMovedThisTurn)
                {
                    tempEnemy.MoveManage();
                }
            }

            //공격 불가능시 이동범위 위치 찾기

            

            //이동 불가능 감지
            //이동 처리

            //enemy move

            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().specialAttack();
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
        yield return new WaitForFixedUpdate();
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
        
        redCheck.Add(redStart, true);
        blueCheck.Add(blueStart, true);
        if (isRedPlayerPlaying)
        {
            redDijSlots.Enqueue(GameObject.Find((redStart.x + 1) + "_" + redStart.y).GetComponent<GridSlotInfo>());
            redDijSlots.Enqueue(GameObject.Find((redStart.x - 1) + "_" + redStart.y).GetComponent<GridSlotInfo>());
            redDijSlots.Enqueue(GameObject.Find(redStart.x + "_" + (redStart.y + 1)).GetComponent<GridSlotInfo>());
            redDijSlots.Enqueue(GameObject.Find(redStart.x + "_" + (redStart.y - 1)).GetComponent<GridSlotInfo>());

            GameObject.Find((redStart.x + 1) + "_" + redStart.y).GetComponent<GridSlotInfo>().redDistance = 1;
            GameObject.Find((redStart.x - 1) + "_" + redStart.y).GetComponent<GridSlotInfo>().redDistance = 1;
            GameObject.Find(redStart.x + "_" + (redStart.y + 1)).GetComponent<GridSlotInfo>().redDistance = 1;
            GameObject.Find(redStart.x + "_" + (redStart.y - 1)).GetComponent<GridSlotInfo>().redDistance = 1;
        }

        if (isBluePlayerPlaying)
        {
            blueDijSlots.Enqueue(GameObject.Find((blueStart.x + 1) + "_" + blueStart.y).GetComponent<GridSlotInfo>());
            blueDijSlots.Enqueue(GameObject.Find((blueStart.x - 1) + "_" + blueStart.y).GetComponent<GridSlotInfo>());
            blueDijSlots.Enqueue(GameObject.Find(blueStart.x + "_" + (blueStart.y + 1)).GetComponent<GridSlotInfo>());
            blueDijSlots.Enqueue(GameObject.Find(blueStart.x + "_" + (blueStart.y - 1)).GetComponent<GridSlotInfo>());

            GameObject.Find((blueStart.x + 1) + "_" + blueStart.y).GetComponent<GridSlotInfo>().blueDistance = 1;
            GameObject.Find((blueStart.x - 1) + "_" + blueStart.y).GetComponent<GridSlotInfo>().blueDistance = 1;
            GameObject.Find(blueStart.x + "_" + (blueStart.y - 1)).GetComponent<GridSlotInfo>().blueDistance = 1;
            GameObject.Find(blueStart.x + "_" + (blueStart.y + 1)).GetComponent<GridSlotInfo>().blueDistance = 1;
        }

        while (redDijSlots.Count != 0)
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
                    if (!redCheck.ContainsKey(g))
                    {
                        redDij.Enqueue(g);
                        if (GameObject.Find((x + 1) + "_" + y).tag != "Wall") g.redDistance = temp.redDistance + 1;
                    }
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
                    if (!redCheck.ContainsKey(g))
                    {
                        redDij.Enqueue(g);
                        if (GameObject.Find(x  + "_" + (y+1)).tag != "Wall") g.redDistance = temp.redDistance + 1;
                    }
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();
                    if (!redCheck.ContainsKey(g))
                    {
                        redDij.Enqueue(g);
                        if (GameObject.Find((x - 1) + "_" + y).tag != "Wall") g.redDistance = temp.redDistance + 1;
                    }
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y-1)).GetComponent<GridSlotInfo>();
                    if (!redCheck.ContainsKey(g))
                    {
                        redDij.Enqueue(g);
                        if (GameObject.Find(x  + "_" + (y-1)).tag != "Wall") g.redDistance = temp.redDistance + 1;
                    }
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
                    if (!blueCheck.ContainsKey(g))
                    {
                        blueDij.Enqueue(g);
                        if (GameObject.Find((x + 1) + "_" + y).tag != "Wall") g.blueDistance = temp.blueDistance + 1;
                    }
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
                    if (!blueCheck.ContainsKey(g))
                    {
                        blueDij.Enqueue(g);
                        if (GameObject.Find(x + "_" + (y+1)).tag != "Wall") g.blueDistance = temp.blueDistance + 1;
                    }
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();
                    if (!blueCheck.ContainsKey(g))
                    {
                        blueDij.Enqueue(g);
                        if (GameObject.Find((x - 1) + "_" + y).tag != "Wall") g.blueDistance = temp.blueDistance + 1;
                    }
                }
                catch (Exception) { }

                try
                {
                    GridSlotInfo g = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
                    if (!blueCheck.ContainsKey(g))
                    {
                        blueDij.Enqueue(g);
                        if (GameObject.Find(x+ "_" + (y-1)).tag != "Wall") g.blueDistance = temp.blueDistance + 1;
                    }
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




