using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public int HP=3;
    public int x, y;
    public COLOR color;
    public DIRECTION direction;
    public GameObject player;
    public GameObject currentBlock;
    public bool isMovedThisTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        GameObject.Find("GameManager").GetComponent<GameManager>().players.Add(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetXY(int px, int py)
    {
        x = px; y = py;
        currentBlock = GameObject.Find(x + "_" + y);
        currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = player;
        player.transform.position = currentBlock.transform.position;
    }

    public GameObject GetNextDest()
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

    public void Move(GameObject nextDest)
    {
        isMovedThisTurn = true;
        currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = null;
        nextDest.GetComponent<GridSlotInfo>().occupyingCharacter = player;
        currentBlock = nextDest;
        player.transform.position = currentBlock.transform.position;
    }

    public bool MoveManage()
    {
        GameObject nextDest = GetNextDest();
        if (isMovedThisTurn)
        {
            return false;
        }
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

        }

        return true;
    }
    
}
