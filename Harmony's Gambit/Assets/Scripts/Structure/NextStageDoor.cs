using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player"))
                    {
                        if (_gameManager.isRedPlayerPlaying)
                        {
                            GameObject.FindGameObjectWithTag("Player").SetActive(false);
                            currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                        }

                        _gameManager.isRedPlayerPlaying = false;
                    }
                    else if (currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter == GameObject.FindGameObjectWithTag("Player2"))
                    {
                        if (_gameManager.isBluePlayerPlaying)
                        {
                            GameObject.FindGameObjectWithTag("Player2").SetActive(false);
                            currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                        }

                        _gameManager.isBluePlayerPlaying = false;
                    }

                    if (!_gameManager.isRedPlayerPlaying && !_gameManager.isBluePlayerPlaying)
                    {
                        ActiveNextStage();
                    }
            }
        }
    }

    private void ActiveNextStage()
    {
        SceneManager.LoadScene("Clear");
    }
}
