using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageDoor : Structure
{
    public int nextStageDoorIndex;

    public void SetIndex(int index)
    {
        nextStageDoorIndex = index;
    }

    private void Update()
    {
        if (_structureManager.rhythm)
        {
            if (_gameManager.isRedPlayerPlaying || _gameManager.isBluePlayerPlaying)
            {
                if ((_gameManager.whichDoorHasRedPlayer == -1 && _gameManager.whichDoorHasBluePlayer == -1) || (_gameManager.whichDoorHasRedPlayer == nextStageDoorIndex || _gameManager.whichDoorHasBluePlayer == nextStageDoorIndex))
                {
                    if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player"))
                    {
                        _gameManager.isRedPlayerPlaying = false;
                        _gameManager.whichDoorHasRedPlayer = nextStageDoorIndex;

                        try
                        {
                            GameObject.FindGameObjectWithTag("Player").SetActive(false);
                        }
                        catch (System.NullReferenceException) { }
                    }
                    else if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player2"))
                    {
                        _gameManager.isBluePlayerPlaying = false;
                        _gameManager.whichDoorHasBluePlayer = nextStageDoorIndex;

                        try
                        {
                            GameObject.FindGameObjectWithTag("Player2").SetActive(false);
                        }
                        catch (System.NullReferenceException) { }
                    }

                    if (!_gameManager.isRedPlayerPlaying && !_gameManager.isBluePlayerPlaying)
                    {
                        ActiveNextStage(_gameManager.whichDoorHasRedPlayer);
                    }
                }
            }
        }
    }

    private void ActiveNextStage(int index)
    {
        _gameManager.whichDoorHasRedPlayer = -1;
        _gameManager.whichDoorHasBluePlayer = -1;

        switch (index)
        {
            case 1:
                print("1번 문 실행");
                break;
            case 2:
                print("2번 문 실행");
                break;
        }
    }
}
