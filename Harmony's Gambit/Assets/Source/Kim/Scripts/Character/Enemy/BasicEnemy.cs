using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyStat
{
    bool moveRight = false;
    bool MoveTurn = false;
    bool Attack = false;
    // Start is called before the first frame update
    public override void Spawn(int x, int y)
    {
        xPos = x;
        yPos = y;
        Debug.Log(xPos + " " + yPos);
        GameObject current = GameObject.Find(xPos + "_" + yPos);
        current.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
        gameObject.transform.position = current.transform.position;
        red = true;
        blue = true;
    }
    

    public override void MovementCheck()
    {
        Debug.Log(HP);
        if (MoveTurn)
        {
            GameObject current = GameObject.Find(xPos + "_" + yPos);
            if (moveRight)
            {
                GameObject target = GameObject.Find(xPos + 1 + "_" + yPos);
                if(target.tag != "Wall")
                {
                    if(target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                    {
                        current.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                        target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                        gameObject.transform.Translate(1, 0, 0);
                        xPos += 1;
                        moveRight = false;
                    }
                    else
                    {
                        Attack = true;
                    }
                }
            }
            else
            {
                GameObject target = GameObject.Find(xPos - 1 + "_" + yPos);
                if (target.tag != "Wall")
                {
                    if (target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                    {
                        current.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                        target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                        gameObject.transform.Translate(-1, 0, 0);
                        xPos -= 1;
                        moveRight = true;
                    }
                    else
                    {
                        Attack = true;
                    }
                }
            }
            MoveTurn = false;
        }
        else
        {
            MoveTurn = true;
        }
    }

    public override void AttackCheck()
    {
        if (Attack)
        {
            if (moveRight)
            {
                GameObject PM = GameObject.Find("PlayerManager");
                GameObject target = GameObject.Find((xPos + 1) + "_" + yPos);
                if (target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                {
                    GameObject current = GameObject.Find(xPos + "_" + yPos);
                    current.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                    xPos += 1;
                    moveRight = false;
                    gameObject.transform.Translate(1, 0, 0);
                }
                else if(target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Player")
                {
                    PM.GetComponent<PlayerManager>().P1_HP -= 1;
                }else if(target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Player2")
                {
                    PM.GetComponent<PlayerManager>().P2_HP -= 1;
                }
            }else
            {
                GameObject PM = GameObject.Find("PlayerManager");
                GameObject target = GameObject.Find((xPos - 1) + "_" + yPos);
                if (target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                {
                    GameObject current = GameObject.Find(xPos + "_" + yPos);
                    current.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                    xPos -= 1;
                    moveRight = true;
                    gameObject.transform.Translate(-1, 0, 0);
                }
                else if (target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Player")
                {
                    PM.GetComponent<PlayerManager>().P1_HP -= 1;
                }
                else if (target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Player2")
                {
                    PM.GetComponent<PlayerManager>().P2_HP -= 1;
                }
            }
            Attack = false;
            
            
        }
    }
}
