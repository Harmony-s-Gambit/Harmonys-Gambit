using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenButton_Basic : Structure
{
    public bool isPressed;
    public int doorOpenButtonIndex;

    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player") || currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player2"))
            {
                isPressed = true;
            }
        }
    }

    public void SetIndex(int index)
    {
        doorOpenButtonIndex = index;
    }
}
