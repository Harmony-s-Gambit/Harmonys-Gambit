using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    public bool takenDamage = false; //���� �������׼��� �ߺ� ������ ������������ �־����ϴ�
    // Start is called before the first frame update
    protected override void Start()
    {
        
        GameObject.Find("GameManager").GetComponent<GameManager>().players.Add(gameObject);
        weapon = new Fist();
        weapon.Start();
        weapon.playerWeapon = true;
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
    }

    public override bool MoveManage()
    {
        if (isMovedThisTurn)
        {
            return false;
        }
        GameObject nextDest = GetNextDest();
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
