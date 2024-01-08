using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hiena : Enemy
{
    Dictionary<GameObject, int> MovementRoute;
    public GameObject target;
    private bool dontMove =false;
    public override void Start()
    {
        base.Start();
        if(color == COLOR.PURPLE)
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
        else if(color == COLOR.BLUE)
        {
            target = GameObject.Find("redPlayer(Clone)");
        }
        else if(color == COLOR.RED)
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
        if(dontMove)
        {
            dontMove = false;
            return GameObject.Find(x + "_" + y);
        }
        int tarX = target.GetComponent<Player>().x;
        int tarY = target.GetComponent<Player>().y;

        if(Mathf.Abs(tarX - x) + Mathf.Abs(tarY - y) >= 10) 
        {
            return GameObject.Find(x+ "_" + y);
        }

        Queue<XYD> xydQ = new Queue<XYD>();
        if (GameObject.Find((x - 1) + "_" + y).GetComponent<GridSlotInfo>().blockType != BLOCKTYPE.WALL)
        {
            xydQ.Enqueue(new XYD(x - 1, y, DIRECTION.LEFT));
        }
        if (GameObject.Find((x + 1) + "_" + y).GetComponent<GridSlotInfo>().blockType != BLOCKTYPE.WALL)
        {
            xydQ.Enqueue(new XYD(x + 1, y, DIRECTION.RIGHT));
        }
        if (GameObject.Find(x + "_" + (y + 1)).GetComponent<GridSlotInfo>().blockType != BLOCKTYPE.WALL)
        {
            xydQ.Enqueue(new XYD(x, y + 1, DIRECTION.UP));
        }
        if (GameObject.Find(x + "_" + (y - 1)).GetComponent<GridSlotInfo>().blockType != BLOCKTYPE.WALL)
        {
            xydQ.Enqueue(new XYD(x, y - 1, DIRECTION.DOWN));
        }

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };
        while(xydQ.Count > 0)
        {
            XYD cur = xydQ.Peek();
            xydQ.Dequeue();

            int nextX, nextY;
            for(int i = 0; i < 4; i++)
            {
                nextX = cur.x + dx[i];
                nextY = cur.y + dy[i];
                if(nextX == tarX && nextY == tarY)
                {
                    xydQ.Clear();
                    dontMove = true;
                    direction = cur.dir;
                    switch (cur.dir)
                    {
                        case DIRECTION.LEFT:
                            return GameObject.Find(x - 1 + "_" + y);
                        case DIRECTION.RIGHT:
                            return GameObject.Find(x + 1 + "_" + y);
                        case DIRECTION.UP:
                            return GameObject.Find(x + "_" + (y+1));
                        case DIRECTION.DOWN:
                            return GameObject.Find(x + "_" + (y-1));
                    }
                }
                if (Mathf.Abs(nextX-x) + Mathf.Abs(nextY - y)> Mathf.Abs(cur.x-x) + Mathf.Abs(cur.y - y)
                    && Mathf.Abs(nextX - x) + Mathf.Abs(nextY - y) <10)
                {
                    if (GameObject.Find(nextX + "_" + nextY).GetComponent<GridSlotInfo>().blockType != BLOCKTYPE.WALL)
                    {
                        xydQ.Enqueue(new XYD(nextX, nextY, cur.dir));
                    }
                }
                
            }
        }

        return GameObject.Find(x+"_" + y);
    }
      

}
