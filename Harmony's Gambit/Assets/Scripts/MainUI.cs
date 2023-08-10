using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    private GameManager _gameManager;

    private void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[0].SetActive(true);

        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Main_MapSelectButton()
    {
        panels[0].SetActive(false);
        panels[2].SetActive(true);
    }

    public void Main_OffsetSettingButton()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    public void OffsetSetting_MainButton()
    {
        panels[1].SetActive(false);
        panels[0].SetActive(true);
    }

    public void MapSelect_MainButton()
    {
        panels[2].SetActive(false);
        panels[0].SetActive(true);
    }

    public void GamePlay1Button() //게임 플레이 시 설정, 노트 생성 시작, 즉 게임 시작 버튼
    {
        NoteManager.instance.SetBGMValue("BGM1");
        GameStartSetting();
        panels[2].SetActive(false);
    }

    private void GameStartSetting()
    {
        _gameManager.isRedPlayerPlaying = true;
        _gameManager.isBluePlayerPlaying = true;
        _gameManager.whichDoorHasRedPlayer = -1;
        _gameManager.whichDoorHasBluePlayer = -1;
    }
}
