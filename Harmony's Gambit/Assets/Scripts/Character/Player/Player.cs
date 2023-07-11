using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject;
        GameObject.Find("GameManager").GetComponent<GameManager>().players.Add(character);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetXY(int px, int py)
    {
        x = px; y = py;
        currentBlock = GameObject.Find(x + "_" + y);
        currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = character;
        character.transform.position = currentBlock.transform.position;
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
        nextDest.GetComponent<GridSlotInfo>().occupyingCharacter = character;
        currentBlock = nextDest;
        character.transform.position = currentBlock.transform.position;
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
                return false;
            }
        }
    }
}
