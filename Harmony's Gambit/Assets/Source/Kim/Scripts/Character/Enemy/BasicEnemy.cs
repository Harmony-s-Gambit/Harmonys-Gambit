using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyStat
{
    bool moveRight = false;
    bool MoveTurn = false;
    bool Attack = false;
    // Start is called before the first frame update

    public void MovementCheck()
    {
        if (MoveTurn)
        {
            if (moveRight)
            {

            }
        }
    }

    public void AttackCheck()
    {

    }
}
