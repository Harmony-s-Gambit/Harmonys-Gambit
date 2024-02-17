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
    public override void Start()
    {
        base.Start();
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
                if(target.GetComponent<Player>().y > this.y)
                {
                    dashDirection = DIRECTION.DOWN;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position.x; v.y = gameObject.transform.position.y -200; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    Quaternion w = new Quaternion();
                    w.x = 0; w.y = 0; w.z = 90;
                    gameObject.transform.Find("DashRoute").transform.rotation = w;
                }
                else
                {
                    dashDirection = DIRECTION.UP;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position.x; v.y = gameObject.transform.position.y +200; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    Quaternion w = new Quaternion();
                    w.x = 0; w.y = 0; w.z = -90;
                    gameObject.transform.Find("DashRoute").transform.rotation = w;
                }
                dashChargeTurn = 4;
                direction = DIRECTION.STAY;
                dash = true;
                return GameObject.Find(x + "_" + y);
                }
            else if (target.GetComponent<Player>().y == this.y)
                {
                if(target.GetComponent<Player>().x > this.x)
                {
                    dashDirection = DIRECTION.RIGHT;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position.x + 200; v.y = gameObject.transform.position.y; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    Quaternion w = new Quaternion();
                    w.x = 0; w.y = 0; w.z = 180;
                    gameObject.transform.Find("DashRoute").transform.rotation = w;
                }
                else
                {
                    dashDirection = DIRECTION.LEFT;
                    gameObject.transform.Find("DashRoute").gameObject.SetActive(true);
                    Vector3 v = new Vector3();
                    v.x = gameObject.transform.position .x - 200; v.y = gameObject.transform.position.y; v.z = gameObject.transform.position.z;
                    gameObject.transform.Find("DashRoute").position = v;
                    Quaternion w = new Quaternion();
                    w.x = 0; w.y = 0; w.z = 0;
                    gameObject.transform.Find("DashRoute").transform.rotation = w;
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

        if (target.GetComponent<Player>().color == COLOR.BLUE)
        {
            if (g.blueDistance > t.blueDistance)
            {
                g = t;
                direction = DIRECTION.LEFT;
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
        }
    }
}
