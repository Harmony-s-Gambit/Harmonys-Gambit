using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hiena2 : Enemy
{
    Dictionary<GameObject, int> MovementRoute;
    public GameObject target;
    private bool dontMove = false;
    public override void Start()
    {
        killScore = ScoreManager.instance.hienaScore;

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
        pattern = new DIRECTION[2] {
            DIRECTION.RIGHT,
            DIRECTION.STAY
        };
        direction = pattern[0];
        weapon = gameObject.AddComponent<Fist>();
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
            return GameObject.Find(x + "_" + y);
        }
        GridSlotInfo g = GameObject.Find((x + 1) + "_" + y).GetComponent<GridSlotInfo>();
        GridSlotInfo t = GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>();

        if (target.GetComponent<Player>().color == COLOR.BLUE) {
            if (g.blueDistance > t.blueDistance)
            {
                g = t;
            }
            t = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
            if (g.blueDistance > t.blueDistance)
            {
                g = t;
            }
            t = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
            if (g.blueDistance > t.blueDistance)
            {
                g = t;
            }
        }
        else
        {
            if (g.redDistance > t.redDistance)
            {
                g = t;
            }
            t = GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>();
            if (g.redDistance > t.redDistance)
            {
                g = t;
            }
            t = GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>();
            if (g.redDistance > t.redDistance)
            {
                g = t;
            }
        }

        return GameObject.Find(g.x + "_" + g.y);
    }

    public void targetChange() { }

}
