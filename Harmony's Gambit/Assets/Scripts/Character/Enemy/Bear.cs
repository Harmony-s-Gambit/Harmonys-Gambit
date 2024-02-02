using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    GameObject target;
    Player targetP;
    Enemy thisEnemy;
    public override void Start()
    {
        base.Start();
        if (color == COLOR.PURPLE)
        {
            Player rp = GameObject.Find("redPlayer(Clone)").GetComponent<Player>();
            Player bp = GameObject.Find("bluePlayer(Clone)").GetComponent<Player>();
            if (rp.x + rp.y < bp.x + bp.y)
            {
                target = GameObject.Find("redPlayer(Clone)");
            }
            else
            {
                target = GameObject.Find("bluePlayer(Clone)");
            }
        }
        else if (color == COLOR.BLUE)
        {
<<<<<<< Updated upstream
=======
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
>>>>>>> Stashed changes
            target = GameObject.Find("redPlayer(Clone)");
        }
        else if (color == COLOR.RED)
        {
            target = GameObject.Find("bluePlayer(Clone)");
        }
        pattern = new DIRECTION[4] {
            DIRECTION.RIGHT,
            DIRECTION.STAY,
            DIRECTION.STAY,
            DIRECTION.STAY
        };
        direction = pattern[0];
        //나중에 3x3(자신 주변으로) 공격하는 무기를 넣을 겁니다. 요기다가 적용
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        targetP = target.GetComponent<Player>();
        thisEnemy = target.GetComponent<Enemy>();
    }
    //곰은 벽같은 거 부수고 다닐것이라서 이렇게 만들었습니다
    DIRECTION toPlayer()
    {
<<<<<<< Updated upstream
        if (Mathf.Abs(targetP.x - thisEnemy.x) > Mathf.Abs(targetP.y - thisEnemy.y))
        {
            if(targetP.y > thisEnemy.y)
            {
                return DIRECTION.UP;
            }
            else
            {
                return DIRECTION.DOWN;
            }
        }
        else
        {
            if(targetP.x > thisEnemy.x)
            {
                return DIRECTION.RIGHT;
            }
            else
            {
                return DIRECTION.LEFT;
            }
        }
    }

    public void SpecialDirection()
    {
        if (_directionIdx == 0)
        {
            pattern[0] = toPlayer();
        }
=======
        int[] movement = new int[2];
        switch (dashDirection)
        {
            case DIRECTION.DOWN:
                {
                    movement[0] = 0;
                    movement[1] = 1;
                    break;
                }
            case DIRECTION.UP:
                {
                    movement[0] = 0;
                    movement[1] = -1;
                    break;
                }
            case DIRECTION.LEFT:
                {
                    movement[0] = -1;
                    movement[1] = 0;
                    break;
                }
            case DIRECTION.RIGHT:
                {
                    movement[0] = 1;
                    movement[1] = 0;
                    break;
                }
        }
        for(int i = 0; i < 7; i++)
        {
            GameObject movingPoint = GameObject.Find((x + movement[0]*i) + "_" + (y + movement[1]*i));
            if(movingPoint.tag == "Wall") { }
        }
        dashCount = 0;
>>>>>>> Stashed changes
    }
}
