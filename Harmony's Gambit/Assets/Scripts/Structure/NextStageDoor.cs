using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageDoor : Structure
{
    private PlayerManager _playerManager;

    public int nextStageDoorIndex;

    private void Start()
    {
        base.Start();
        _playerManager = FindObjectOfType<PlayerManager>();
    }

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
                    ActiveNextStage(nextStageDoorIndex);
                }
            }
        }
    }

    private void ActiveNextStage(int index)
    {
        switch (index)
        {
            case 0:
                //SceneManager.LoadScene("BossStage1"); //스코어보드 보고, 버튼눌러서 다음 씬 이동
                ScoreManager.instance.StageClearScore(nextStageDoorIndex);
                _playerManager.GameClear = true;
                SceneManager.LoadScene("Clear");
                break;
            case 1:
                SceneManager.LoadScene("Clear");
                break;
        }
    }
}
