using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Enemy
{
    public override void Start()
    {
        base.Start();

        pattern = new DIRECTION[4] {
            DIRECTION.LEFT,
            DIRECTION.STAY,
            DIRECTION.RIGHT,
            DIRECTION.STAY,
        };
        direction = pattern[0];
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        weapon.equiper = gameObject;

        killScore = ScoreManager.instance.purpleMouseScore2;
    }
}
