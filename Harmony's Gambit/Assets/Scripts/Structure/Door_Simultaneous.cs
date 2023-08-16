using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Simultaneous : Structure
{
    public int doorIndex;
    private bool isDoorOpened = false;
    private int doorOpenTurn;
    private bool doorOpenTurnOnce = true;

    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if (_structureManager.TryOpenDoor_Simultaneous(doorIndex))
            {
                currentBlock.GetComponent<GridSlotInfo>().blockType = BLOCKTYPE.GROUND;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                isDoorOpened = true;
                doorOpenTurn = 0;
            }

            if (isDoorOpened && doorOpenTurnOnce)
            {
                doorOpenTurnOnce = false;
                doorOpenTurn++;
                if (doorOpenTurn >= 3)
                {
                    doorOpenTurn = 0;
                    currentBlock.GetComponent<GridSlotInfo>().blockType = BLOCKTYPE.WALL;
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    isDoorOpened = false;
                }
            }
        }
        else
        {
            doorOpenTurnOnce = true;
        }
    }

    public override void SetXY(int px, int py)
    {
        base.SetXY(px, py);
        currentBlock.GetComponent<GridSlotInfo>().blockType = BLOCKTYPE.WALL;
    }

    public void SetIndex(int index)
    {
        doorIndex = index;
    }
}
