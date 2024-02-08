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
    Stack<GridSlotInfo> dashAttackRange = new Stack<GridSlotInfo>();
    public DIRECTION dashDirection = DIRECTION.DOWN;
    private bool dontMove = false;
    public override void Start()
    {
        base.Start();
        pattern = new DIRECTION[1];
        pattern[0] = DIRECTION.STAY;
        //���߿� 3x3(�ڽ� �ֺ�����) �����ϴ� ���⸦ ���� �̴ϴ�. ���ٰ� ����
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
                target = GameObject.Find("redPlayer(Clone)");
            }
            else
            {
                target = GameObject.Find("bluePlayer(Clone)");
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
                }
                else
                {
                    dashDirection = DIRECTION.UP;
                }
                dashChargeTurn = 2;
                direction = DIRECTION.STAY;
                return GameObject.Find(x + "_" + y);
                }
            else if (target.GetComponent<Player>().y == this.y)
                {
                if(target.GetComponent<Player>().x > this.x)
                {
                    dashDirection = DIRECTION.RIGHT;
                }
                else
                {
                    dashDirection = DIRECTION.LEFT;
                }
                dashChargeTurn = 2;
                direction = DIRECTION.STAY;
                return GameObject.Find(x + "_" + y);
            }
        }
        if(dashChargeTurn != 0)
        {
            Debug.Log(dashChargeTurn + "_" + "charging");
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
            Debug.Log("Touched");
            target = GameObject.Find("redPlayer(Clone)");
        }
        else
        {
            Debug.Log("Touched");
            target = GameObject.Find("bluePlayer(Clone)");
        }
    }
    public override void specialAttack()
    {
        if (dashCount > 4 && dashChargeTurn <= 0)
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
            dashAttackRange.Push(movementPoint.GetComponent<GridSlotInfo>());
            dashAttackRange.Push(GameObject.Find((x + movement[1]) + "_" + (y + movement[0])).GetComponent<GridSlotInfo>());
            dashAttackRange.Push(GameObject.Find((x - movement[1]) + "_" + (y - movement[0])).GetComponent<GridSlotInfo>());
            for (int i = 1; i < 7; i++)
            {
                movementPoint = GameObject.Find((x + movement[0] * i) + "_" + (y + movement[1] * i));
                if (movementPoint.GetComponent<GridSlotInfo>().occupyingCharacter == null && movementPoint.GetComponent<GridSlotInfo>().blockType != BLOCKTYPE.WALL)
                {
                    movingDistance = i;
                    dashAttackRange.Push(movementPoint.GetComponent<GridSlotInfo>());
                    dashAttackRange.Push(GameObject.Find((x + movement[1] + movement[0] * i) + "_" + (y + movement[0] + movement[1] * i)).GetComponent<GridSlotInfo>());
                    dashAttackRange.Push(GameObject.Find((x - movement[1] + movement[0] * i) + "_" + (y - movement[0] + movement[1] * i)).GetComponent<GridSlotInfo>());
                }
                else
                {
                    break;
                }
            }
            dashAttackRange.Push(GameObject.Find((x + movement[1] + movement[0] * (movingDistance + 1)) + "_" + (y + movement[0] + movement[1] * (movingDistance + 1))).GetComponent<GridSlotInfo>());
            dashAttackRange.Push(GameObject.Find((x - movement[1] + movement[0] * (movingDistance + 1)) + "_" + (y + movement[0] - movement[1] * (movingDistance + 1))).GetComponent<GridSlotInfo>());
            dashAttackRange.Push(GameObject.Find((x + movement[0] * (movingDistance + 1)) + "_" + (y + movement[1] * (movingDistance + 1))).GetComponent<GridSlotInfo>());


            while (dashAttackRange.Count != 0)
            {
                GridSlotInfo temp = dashAttackRange.Pop();
                Debug.Log("AttackRange" + " " + temp.x + "_" + temp.y);
                try
                {
                    if (temp.occupyingCharacter.tag == "Player")
                    {
                        temp.occupyingCharacter.GetComponent<Player>().HP -= 1;
                    }
                }
                catch (Exception e) { }
            }

            x = x + movingDistance * movement[0];
            y = y + movingDistance * movement[1];
            Move(GameObject.Find(x + "_" + y));



            dashCount = 0;
        }
    }
}
