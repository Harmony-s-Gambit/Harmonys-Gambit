using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    System.Random r = new System.Random();
    public GameObject target;
    Enemy thisEnemy;
    int dashCount = 0;
    int dashChargeTurn = 0;
    DIRECTION dashDirection = DIRECTION.DOWN;
    private bool dontMove = false;
    public override void Start()
    {
        base.Start();
        //나중에 3x3(자신 주변으로) 공격하는 무기를 넣을 겁니다. 요기다가 적용
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        weapon.equiper = gameObject;
        thisEnemy = target.GetComponent<Enemy>();

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

        if (dashCount > 4)
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
            direction = DIRECTION.STAY;
            return GameObject.Find(x + "_" + y);
        }

        if(dashChargeTurn == 0 && dashCount > 4)
        {
            //dash
            dashCount = 0;
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
}
