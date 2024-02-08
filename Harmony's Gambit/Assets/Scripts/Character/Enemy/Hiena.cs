using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Hiena : Enemy
{
    Dictionary<GameObject, int> MovementRoute;
    public GameObject target;
    private bool dontMove = false;
    public override void Start()
    {
        killScore = ScoreManager.instance.purpleHyenaScore;

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
        pattern = new DIRECTION[1] {
            DIRECTION.STAY
        };
        direction = pattern[0];
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        weapon.equiper = gameObject;
    }

    public class XYD
    {
        public int x, y;
        public DIRECTION dir;

        public XYD(int x, int y, DIRECTION dir)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }
    }

    public override GameObject GetNextDest()
    {
        if (dontMove)
        {
            dontMove = false;
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
        dontMove = true;
        return GameObject.Find(g.x + "_" + g.y);
    }

    public override void changeTarget(COLOR c) {
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
    public void sepcialAttack()
    {
        Debug.Log("Hello");
    }
}
