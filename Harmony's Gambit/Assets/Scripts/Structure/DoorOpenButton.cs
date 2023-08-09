using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenButton : Structure
{
    public bool isPressed;
    public int doorOpenButtonIndex;

    public void SetIndex(int index)
    {
        doorOpenButtonIndex = index;
    }

    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter != null)
            {
                isPressed = true;
            }
        }
    }
}
