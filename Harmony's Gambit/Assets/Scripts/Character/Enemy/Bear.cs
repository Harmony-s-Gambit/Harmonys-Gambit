using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    GameObject target;
    Player targetP;
    Enemy thisEnemy;
    void Start()
    {
        base.Start();
        if (color == COLOR.PURPLE)
        {
            Player rp = GameObject.Find("redPlayer").GetComponent<Player>();
            Player bp = GameObject.Find("bluePlayer").GetComponent<Player>();
            if (rp.x + rp.y < bp.x + bp.y)
            {
                target = GameObject.Find("redPlayer");
            }
            else
            {
                target = GameObject.Find("bluePlayer");
            }
        }
        else if (color == COLOR.BLUE)
        {
            target = GameObject.Find("redPlayer");
        }
        else if (color == COLOR.RED)
        {
            target = GameObject.Find("bluePlayer");
        }
        pattern = new DIRECTION[4] {
            DIRECTION.RIGHT,
            DIRECTION.STAY,
            DIRECTION.STAY,
            DIRECTION.STAY
        };
        direction = pattern[0];
        //���߿� 3x3(�ڽ� �ֺ�����) �����ϴ� ���⸦ ���� �̴ϴ�. ���ٰ� ����
        weapon = new Fist();
        weapon.Start();
        targetP = target.GetComponent<Player>();
        thisEnemy = target.GetComponent<Enemy>();
    }
    //���� ������ �� �μ��� �ٴҰ��̶� �̷��� ��������ϴ�
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

    public void specialDirection()
    {
        if (_directionIdx == 0)
        {
            pattern[0] = toPlayer();
        }
    }
}
