using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bear : Enemy
{

    System.Random r = new System.Random();
    public GameObject target;
    Enemy thisEnemy;
    int dashCount = 0;
    int dashChargeTurn = 0;
    bool dash = false;
    Stack<GridSlotInfo> dashAttackRange = new Stack<GridSlotInfo>();
    public DIRECTION dashDirection = DIRECTION.DOWN;
    private bool dontMove = false;

    //곰이 죽었을 때 한 번만 실행하기 위한 변수
    private bool onceDie = false;

    public override void Start()
    {
        base.Start();
        
        killScore = ScoreManager.instance.purpleBearScore;

        gameObject.transform.Find("DashRoute").gameObject.SetActive(false);
        pattern = new DIRECTION[1];
        pattern[0] = DIRECTION.STAY;
        //나중에 3x3(자신 주변으로) 공격하는 무기를 넣을 겁니다. 요기다가 적용
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        weapon.equiper = gameObject;
        //thisEnemy = target.GetComponent<Enemy>();

        for(int i = 0; i < 3; i++)
        {
            int k = r.Next(0, 2);
            if (k == 0)
            {
                barrier.Push(COLOR.RED);
            }
            else
            {
                barrier.Push(COLOR.BLUE);
            }
        }
        if(barrier.Peek() == COLOR.RED)
        {
            target = GameObject.Find("redPlayer(Clone)");
        }
        else
        {
            target = GameObject.Find("bluePlayer(Clone)");
        }

    }

    private new void Update() //곰이 죽었을 때 실행하기 위한 업데이트, 더 좋게 수정가능하면 수정하셔도 됩니다.
    {
        if (HP < 1)
        {
            if (!onceDie)
            {
                onceDie = true;
                ScoreManager.instance.StageClearScore(1);
                FindObjectOfType<PlayerManager>().GameClear = true;
                AudioManager.instance.PlaySFX("Clear");
                FindObjectOfType<GameManager>().isGameStart = false;
            }
        }
    }

    public override GameObject GetNextDest()
    {
        
        if(barrier.Count != 0)
        {
            if(barrier.Peek() == COLOR.RED)
            {
                target = GameObject.Find("bluePlayer(Clone)");
            }
            else
            {
                target = GameObject.Find("redPlayer(Clone)");
            }
        }

        if (dontMove)
        {
            dontMove = false;
            direction = DIRECTION.STAY;
            return GameObject.Find(x + "_" + y);
        }

        if (dashCount > 4 && dashChargeTurn <= 0)
        {
            
            if(target.GetComponent<Player>().x == this.x)
                {
                m_Animator.Play("Charge");
                if (target.GetComponent<Player>().y > this.y)
                {

                    dashDirection = DIRECTION.DOWN;
                    direction = DIRECTION.DOWN;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position.x; v.y = gameObject.transform.position.y +200; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    gameObject.transform.Find("DashRoute").transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));

                }
                else
                {
                    dashDirection = DIRECTION.UP;
                    direction = DIRECTION.UP;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position.x; v.y = gameObject.transform.position.y -200; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    gameObject.transform.Find("DashRoute").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

                }
                dashChargeTurn = 4;
                direction = DIRECTION.STAY;
                dash = true;
                return GameObject.Find(x + "_" + y);
                }
            else if (target.GetComponent<Player>().y == this.y)
                {
                m_Animator.Play("Charge");
                if (target.GetComponent<Player>().x > this.x)
                {
                    dashDirection = DIRECTION.RIGHT;
                    direction = DIRECTION.RIGHT;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position.x + 200; v.y = gameObject.transform.position.y; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    gameObject.transform.Find("DashRoute").transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
                    v= new Vector3(1.5f, 1.5f, 1);
                    gameObject.transform.localScale = v;
                }
                else
                {
                    dashDirection = DIRECTION.LEFT;
                    direction = DIRECTION.LEFT;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position .x - 200; v.y = gameObject.transform.position.y; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    gameObject.transform.Find("DashRoute").transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                    v = new Vector3(-1.5f, 1.5f, 1);
                    gameObject.transform.localScale = v;
                }
                dashChargeTurn = 4;
                direction = DIRECTION.STAY;
                dash = true;
                return GameObject.Find(x + "_" + y);
            }
        }
        if(dash)
        {
            direction = DIRECTION.STAY;
            dashChargeTurn--;
            return GameObject.Find(x + "_" + y);
        }

        GridSlotInfo g = GameObject.Find((x + 1) + "_" + y).GetComponent<GridSlotInfo>();
        direction = DIRECTION.RIGHT;
        GridSlotInfo t = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();
        Vector3 v2 = new Vector3(1.5f, 1.5f, 1);
        gameObject.transform.localScale = v2;

        if (target.GetComponent<Player>().color == COLOR.BLUE)
        {
            
            if (g.blueDistance > t.blueDistance)
            {
                g = t;
                direction = DIRECTION.LEFT;
                v2 = new Vector3(-1.5f, 1.5f, 1);
                gameObject.transform.localScale = v2;
            }
            t = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
            if (g.blueDistance > t.blueDistance)
            {
                g = t;
                direction = DIRECTION.UP;
            }
            t = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
            if (g.blueDistance > t.blueDistance)
            {
                g = t;
                direction = DIRECTION.DOWN;
            }
        }
        else
        {
            if (g.redDistance > t.redDistance)
            {
                g = t;
                direction = DIRECTION.LEFT;
                v2 = new Vector3(-1.5f, 1.5f, 1);
                gameObject.transform.localScale = v2;
            }
            t = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
            if (g.redDistance > t.redDistance)
            {
                g = t;
                direction = DIRECTION.UP;
            }
            t = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
            if (g.redDistance > t.redDistance)
            {
                g = t;
                direction = DIRECTION.DOWN;
            }
        }
        dashCount++;
        dontMove = true;
        return GameObject.Find(g.x + "_" + g.y);
    }

    public override void changeTarget(COLOR c)
    {
        if (c == COLOR.RED)
        {
            target = GameObject.Find("redPlayer(Clone)");
        }
        else
        {
            target = GameObject.Find("bluePlayer(Clone)");
        }
    }
    public override void specialAttack()
    {
        if (dashChargeTurn <= 0 && dash)
        {
            int movingDistance = 0;
            int[] movement = new int[2];
            switch (dashDirection)
            {
                case DIRECTION.UP:
                    {
                        movement[0] = 0;
                        movement[1] = -1;
                        break;
                    }
                case DIRECTION.DOWN:
                    {
                        movement[0] = 0;
                        movement[1] = 1;
                        break;
                    }
                case DIRECTION.RIGHT:
                    {
                        movement[0] = 1;
                        movement[1] = 0;
                        break;
                    }
                case DIRECTION.LEFT:
                    {
                        movement[0] = -1;
                        movement[1] = 0;
                        break;
                    }
            }

            GameObject movementPoint = GameObject.Find((x) + "_" + (y));
            GameObject forThrifting = movementPoint;

            dashAttackRange.Push(movementPoint.GetComponent<GridSlotInfo>());
            try
            {
                dashAttackRange.Push(GameObject.Find((x + movement[1]) + "_" + (y + movement[0])).GetComponent<GridSlotInfo>());
            }catch(Exception e) { }
            try
            {
                dashAttackRange.Push(GameObject.Find((x - movement[1]) + "_" + (y - movement[0])).GetComponent<GridSlotInfo>());
            }catch(Exception e) { }

            for (int i = 1; i < 7; i++)
            {
                movementPoint = GameObject.Find((x + movement[0] * i) + "_" + (y + movement[1] * i));
                if (movementPoint.GetComponent<GridSlotInfo>().occupyingCharacter == null && movementPoint.GetComponent<GridSlotInfo>().blockType != BLOCKTYPE.WALL)
                {
                    movingDistance = i;
                    dashAttackRange.Push(movementPoint.GetComponent<GridSlotInfo>());
                    try
                    {
                        dashAttackRange.Push(GameObject.Find((x + movement[1] + movement[0] * i) + "_" + (y + movement[0] + movement[1] * i)).GetComponent<GridSlotInfo>());
                    }
                    catch (Exception e) { }
                    try
                    {
                        dashAttackRange.Push(GameObject.Find((x - movement[1] + movement[0] * i) + "_" + (y - movement[0] + movement[1] * i)).GetComponent<GridSlotInfo>());
                    }
                    catch (Exception e) { }
                }
                else
                {
                    break;
                }
            }
            try
            {
                dashAttackRange.Push(GameObject.Find((x + movement[1] + movement[0] * (movingDistance + 1)) + "_" + (y + movement[0] + movement[1] * (movingDistance + 1))).GetComponent<GridSlotInfo>());
            }
            catch (Exception e) { }
            try
            {
                dashAttackRange.Push(GameObject.Find((x - movement[1] + movement[0] * (movingDistance + 1)) + "_" + (y + movement[0] - movement[1] * (movingDistance + 1))).GetComponent<GridSlotInfo>());
            }
            catch (Exception e) { }
            try
            {
                dashAttackRange.Push(GameObject.Find((x + movement[0] * (movingDistance + 1)) + "_" + (y + movement[1] * (movingDistance + 1))).GetComponent<GridSlotInfo>());
            }
            catch (Exception e) { }

            while (dashAttackRange.Count != 0)
            {
                GridSlotInfo temp = dashAttackRange.Pop();
                Debug.Log("AttackRange" + " " + temp.x + "_" + temp.y);
                try
                {
                    if (temp.occupyingCharacter.tag == "Player" || temp.occupyingCharacter.tag == "Player2")
                    {
                        temp.occupyingCharacter.GetComponent<Player>().HP -= 1;
                    }
                }
                catch (Exception e) { }
            }

            x = x + movingDistance * movement[0];
            y = y + movingDistance * movement[1];


            Move(GameObject.Find(x + "_" + y));


            dash = false;
            dashCount = 0;
            dashAttackRange.Clear();
            gameObject.transform.Find("DashRoute").gameObject.SetActive(false);
            m_Animator.Play("Pattern_Rush");
        }
    }
}
