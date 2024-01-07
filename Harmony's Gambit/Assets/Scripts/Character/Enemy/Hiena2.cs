using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Hiena2 : Enemy
{
    Dictionary<GameObject, int> MovementRoute;
    GameObject target;
    private bool dontMove = false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (color == COLOR.PURPLE)
        {
            Player rp = GameObject.Find("redPlayer(Clone)").GetComponent<Player>();
            Player bp = GameObject.Find("bluePlayer(Clone)").GetComponent<Player>();
            if(rp.x + rp.y < bp.x + bp.y)
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

    public class XYD {
        public int x, y;
        public DIRECTION dir;

        public XYD(int x, int y, DIRECTION dir) {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }
    }

}
