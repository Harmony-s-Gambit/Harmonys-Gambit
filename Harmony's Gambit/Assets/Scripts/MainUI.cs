using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject[] buttons;
    private GameManager _gameManager;
    private SightManager _sightManager;
    private CameraMoving _cameraMoving;

    private void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[0].SetActive(true);

        _gameManager = FindObjectOfType<GameManager>();
        _sightManager = FindObjectOfType<SightManager>();
        _cameraMoving = FindObjectOfType<CameraMoving>();
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
        panels[0].SetActive(false);
        panels[3].SetActive(true);
        NoteManager.instance.SetBGMValue("BGM1");
        GameStartSetting();
        panels[2].SetActive(false);

        GridSlotInfo[] slots = FindObjectsOfType<GridSlotInfo>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateSightType();
        }

        StartCoroutine(TestDelay());
        _cameraMoving.FirstCam();
    }

    IEnumerator TestDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _sightManager.rhythm = true;
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
                buttons[0].SetActive(false); //오프셋 설정 버튼 끄기
                break;
            case 1:
                buttons[0].SetActive(true);
                break;
            case 2:
                buttons[1].SetActive(false); //오프셋-메인 버튼 끄기
                break;
            case 3:
                buttons[1].SetActive(true);
                break;
        }
    }
}
