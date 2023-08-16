using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class Door_Basic : Structure
{
    public int doorIndex;

    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if (_structureManager.TryOpenDoor_Basic(doorIndex))
            {
                currentBlock.GetComponent<GridSlotInfo>().blockType = BLOCKTYPE.GROUND;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
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
