using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorOpenButton_Simultaneous : Structure
{
    public bool isPressed;
    public int doorOpenButtonIndex;
    public int doorOpenButtonIndex2 = 0;

    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if(currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == null)
            {
                if (isPressed)
                {
                    isPressed = false;
                }
            }
            else if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player") || currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player2"))
            {
                isPressed = true;
            }
            else
            {
                //if (doorOpenButtonIndex2 == 0)
                //{
                //    isPressed = false;
                //}
                isPressed = false;
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
