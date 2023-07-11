using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected DIRECTION[] pattern;
    protected int _directionIdx;

    // Start is called before the first frame update
    protected override void Start()
    {
        _directionIdx = 0;
        GameObject.Find("GameManager").GetComponent<GameManager>().enemies.Add(gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public override void SetXY(int px, int py)
    {
        x = px; y = py;
        currentBlock = GameObject.Find(x + "_" + y);
        currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
        gameObject.transform.position = currentBlock.transform.position;
    }

    public override GameObject GetNextDest()
    {
        GameObject destBlock = currentBlock;
        switch (direction)
        {
            case DIRECTION.UP:
                destBlock = GameObject.Find(x + "_" + (y + 1));
                break;
            case DIRECTION.LEFT:
                destBlock = GameObject.Find((x - 1) + "_" + y);
                break;
            case DIRECTION.DOWN:
                destBlock = GameObject.Find(x + "_" + (y - 1));
                break;
            case DIRECTION.RIGHT:
                destBlock = GameObject.Find((x + 1) + "_" + y);
                break;
            case DIRECTION.STAY:
                break;
            default:
                destBlock = currentBlock;
                break;

        }
        return destBlock;
    }

    public override void Move(GameObject nextDest)
    {
        isMovedThisTurn = true;
        currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = null;
        nextDest.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
        currentBlock = nextDest;
        gameObject.transform.position = currentBlock.transform.position;
        switch (direction)
        {
            case DIRECTION.UP:
                y = y + 1;
                break;
            case DIRECTION.LEFT:
                x = x - 1;
                break;
            case DIRECTION.DOWN:
                y = y - 1;
                break;
            case DIRECTION.RIGHT:
                x = x + 1;
                break;
            case DIRECTION.STAY:
                break;
            default:
                break;
        }
        _directionIdx = (_directionIdx + 1) % pattern.Length;
        direction = pattern[_directionIdx];
    }

    public override bool MoveManage()
    {
        GameObject nextDest = GetNextDest();
        if (isMovedThisTurn)
        {
            return false;
        }
        isMovedThisTurn = true;
        GameObject whosOnDest = nextDest.GetComponent<GridSlotInfo>().occupyingCharacter;
        if (nextDest.GetComponent<GridSlotInfo>().blockType == BLOCKTYPE.WALL)
        {
            return false;
        }
        else if (whosOnDest == null)
        {
            Move(nextDest);
            return true;
        }
        else
        {
            if (whosOnDest.GetComponent<Character>().MoveManage())
            {
                Move(nextDest);
                return true;
            }
            else
            {
                if(direction == DIRECTION.STAY)
                {
                    _directionIdx = (_directionIdx + 1) % pattern.Length;
                    direction = pattern[_directionIdx];
                }
                return false;
            }
        }
    }
}
