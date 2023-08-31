using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hiena : Enemy
{
    Dictionary<GameObject, int> MovementRoute;
    GameObject target;
    private bool dontMove =false;
    void Start()
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

    //DIRECTION toPlayer()
    //{
    //    MovementRoute.Clear();
    //    int upD = -1, leftD = -1, downD = -1, rightD = -1, temp = 0 ;
    //    int tarX = target.GetComponent<Player>().x;
    //    int tarY = target.GetComponent<Player>().y;
    //    SearchEnemy(tarX, tarY, 0);
    //    if (MovementRoute.ContainsKey(GameObject.Find((x + 1) + "_" + y)))
    //    {
    //        rightD = MovementRoute[GameObject.Find((x + 1) + "_" + y)];
    //    }
    //    if (MovementRoute.ContainsKey(GameObject.Find((x - 1) + "_" + y)))
    //    {
    //        leftD = MovementRoute[GameObject.Find((x - 1) + "_" + y)];
    //    }
    //    if (MovementRoute.ContainsKey(GameObject.Find(x + "_" + (y+1))))
    //    {
    //        upD = MovementRoute[GameObject.Find((x + "_" + (y+1)))];
    //    }
    //    if (MovementRoute.ContainsKey(GameObject.Find(x + "_" + (y - 1))))
    //    {
    //        downD = MovementRoute[GameObject.Find((x + "_" + (y - 1)))];
    //    }
    //    temp = Mathf.Min(upD, downD, leftD, rightD);
    //    if (temp == upD)
    //        return DIRECTION.UP;
    //    else if (temp == downD)
    //        return DIRECTION.DOWN;
    //    else if (temp == rightD)
    //        return DIRECTION.RIGHT;
    //    else if (temp == leftD)
    //        return DIRECTION.LEFT;
    //    else
    //        return DIRECTION.STAY;
    //}

    //public void specialDirection()
    //{
    //    if(_directionIdx == 0)
    //    {
    //        pattern[0] = toPlayer();
    //    }
    //}


    //void SearchEnemy(int GameObjectX, int GameObjectY, int distance)
    //{
    //    if(MovementRoute.ContainsKey(GameObject.Find(GameObjectX + "_" +GameObjectY)))
    //    {
    //        if(MovementRoute[GameObject.Find(GameObjectX + "_" + GameObjectY)] > distance)
    //        {
    //            MovementRoute[GameObject.Find(GameObjectX + "_" + GameObjectY)] = distance;
    //        }
    //    }
    //    else
    //    {
    //        MovementRoute.Add(GameObject.Find(GameObjectX + "_" + GameObjectY), distance);
    //        if(GameObject.Find((GameObjectX + 1) + "_" + GameObjectY).tag != "Wall")
    //        {
    //            SearchEnemy(GameObjectX + 1, GameObjectY, distance + 1);
    //        }
    //        if (GameObject.Find((GameObjectX - 1) + "_" + GameObjectY).tag != "Wall")
    //        {
    //            SearchEnemy(GameObjectX - 1, GameObjectY, distance + 1);
    //        }
    //        if (GameObject.Find((GameObjectX ) + "_" + (GameObjectY + 1)).tag != "Wall")
    //        {
    //            SearchEnemy(GameObjectX, GameObjectY + 1, distance + 1);
    //        }
    //        if (GameObject.Find((GameObjectX) + "_" + (GameObjectY - 1)).tag != "Wall")
    //        {
    //            SearchEnemy(GameObjectX, GameObjectY - 1, distance + 1);
    //        }
    //    }
    //}

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
