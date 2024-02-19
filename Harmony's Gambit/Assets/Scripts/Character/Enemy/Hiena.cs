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
    private bool isSleep = true;
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
        if (isSleep)
        {
            if(color == COLOR.PURPLE)
            {
                if(GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().redDistance <= 4)
                {
                    target = GameObject.Find("redPlayer(Clone)");
                    isSleep = false;
                }else if(GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().blueDistance <= 4)
                {
                    target = GameObject.Find("bluePlayer(Clone)");
                    isSleep = false;
                }
                else
                {
                    return GameObject.Find(x + "_" + y);
                }
            }else if(color == COLOR.RED)
            {
                if(GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().blueDistance <= 4 || GameObject.Find(x+"_"+y).GetComponent<GridSlotInfo>().redDistance <=4)
                {
                    target = GameObject.Find("bluePlayer(Clone)");
                    isSleep = false;
                }
            }else if(color == COLOR.BLUE)
            {
                if (GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().blueDistance <= 4 || GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().redDistance <= 4)
                {
                    target = GameObject.Find("redPlayer(Clone)");
                    isSleep = false;
                }
            }

        }

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
        if(direction == DIRECTION.RIGHT)
        {
            Vector3 v = new Vector3(0.1f, 0.1f, 1);
            gameObject.transform.localScale = v;
        }else if(direction == DIRECTION.LEFT)
        {
            Vector3 v = new Vector3(-0.1f, 0.1f, 1);
            gameObject.transform.localScale = v;
        }
        return GameObject.Find(g.x + "_" + g.y);
    }

    public override void changeTarget(COLOR c) {
        if (c == COLOR.RED)
        {
            target = GameObject.Find("redPlayer(Clone)");
        }
        else
        {
            target = GameObject.Find("bluePlayer(Clone)");
        }
    }
    public void sepcialAttack()
    {
    }
}
