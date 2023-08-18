using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiena : Enemy
{
    GameObject target;
    void Start()
    {
        base.Start();
        if(color == COLOR.PURPLE)
        {
            Player rp = GameObject.Find("redPlayer").GetComponent<Player>();
            Player bp = GameObject.Find("bluePlayer").GetComponent<Player>();
            if(rp.x + rp.y < bp.x + bp.y)
            {
                target = GameObject.Find("redPlayer");
            }
            else
            {
                target = GameObject.Find("bluePlayer");
            }
        }
        else if(color == COLOR.BLUE)
        {
            target = GameObject.Find("redPlayer");
        }
        else if(color == COLOR.RED)
        {
            target = GameObject.Find("bluePlayer");
        }
        pattern = new DIRECTION[2] {
            DIRECTION.RIGHT,
            DIRECTION.STAY
        };
        direction = pattern[0];
        weapon = new Fist();
        weapon.Start();
    }

    DIRECTION toPlayer()
    {
        if (true)
        {

        }
        return DIRECTION.DOWN;
    }

    public void specialDirection()
    {
        if(_directionIdx == 0)
        {
            pattern[0] = toPlayer();
        }
    }
}
