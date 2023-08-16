using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenButton_Simultaneous : Structure
{
    public bool isPressed;
    public int doorOpenButtonIndex;
    public int doorOpenButtonIndex2 = 0;

    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter != null)
            {
                isPressed = true;
            }
            else
            {
                if (doorOpenButtonIndex2 == 0)
                {
                    isPressed = false;
                }
            }
        }
    }

    public void SetIndex(int index)
    {
        doorOpenButtonIndex = index;
    }

    public void SetIndex2(int index)
    {
        doorOpenButtonIndex2 = index;
    }
}
