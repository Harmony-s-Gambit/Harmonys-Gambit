using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Simultaneous : Structure
{
    public int doorIndex;
    private bool isDoorOpened = false;
    private int doorOpenTurn;
    private bool doorOpenTurnOnce = true;

    protected void Start()
    {
        base.Start();

    }
    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if (_structureManager.TryOpenDoor_Simultaneous(doorIndex) && !isDoorOpened)
            {
                currentBlock.GetComponent<GridSlotInfo>().blockType = BLOCKTYPE.GROUND;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                isDoorOpened = true;
                doorOpenTurn = 0;
                AudioManager.instance.PlaySFX("DoorOpenButton");
            }

            if (isDoorOpened && doorOpenTurnOnce)
            {
                doorOpenTurnOnce = false;
                doorOpenTurn++;
                if (doorOpenTurn >= 4)
                {
                    doorOpenTurn = 0;
                    currentBlock.GetComponent<GridSlotInfo>().blockType = BLOCKTYPE.WALL;
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    isDoorOpened = false;
                    AudioManager.instance.PlaySFX("DoorClose");
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
