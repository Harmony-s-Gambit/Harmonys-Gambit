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
    }
}
