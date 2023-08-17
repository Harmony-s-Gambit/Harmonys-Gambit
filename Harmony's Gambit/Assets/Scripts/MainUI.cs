using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject[] buttons;
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

    public void GamePlay1Button() //���� �÷��� �� ����, ��Ʈ ���� ����, �� ���� ���� ��ư
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

    public void ControllButton(int num)
    {
        switch (num)
        {
            case 0:
                buttons[0].SetActive(false); //������ ���� ��ư ����
                break;
            case 1:
                buttons[0].SetActive(true);
                break;
            case 2:
                buttons[1].SetActive(false); //������-���� ��ư ����
                break;
            case 3:
                buttons[1].SetActive(true);
                break;
        }
    }
}
