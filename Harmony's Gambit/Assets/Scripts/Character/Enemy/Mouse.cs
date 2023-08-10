using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Enemy
{
    void Start()
    {
        base.Start();
        pattern = new DIRECTION[4] {
            DIRECTION.LEFT,
            DIRECTION.STAY,
            DIRECTION.RIGHT,
            DIRECTION.STAY,
        };
        direction = pattern[0];
        weapon = new Fist();
        weapon.Start();
    }
}
