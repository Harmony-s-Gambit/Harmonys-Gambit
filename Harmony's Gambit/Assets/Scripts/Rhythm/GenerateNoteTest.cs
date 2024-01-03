using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateNoteTest : MonoBehaviour
{
    public static GenerateNoteTest instance;

    [SerializeField] Transform _tfCenterFrameP1;
    [SerializeField] Transform _tfCenterFrameP2;

    private MainUI _mainUI;
    private GameManager _gameManager;

    private void Start()
    {
        _mainUI = FindObjectOfType<MainUI>();
        _gameManager = FindObjectOfType<GameManager>();
        instance = this;
    }

    private void FixedUpdate()
    {
        if (NoteManager.instance.currentBGM != "") //BGM 이름 설정 시 노트 생성 시작
        {
            NoteManager.instance.GenerateNote();
        }
    }

    public void OffsetStartButton()
    {
        _gameManager.isBluePlayerPlaying = true;
        _gameManager.isRedPlayerPlaying = true;
        _mainUI.ControllButton(0);
        _mainUI.ControllButton(2);
        NoteManager.instance.SetBGMValue("Offset");
        SettingOffset();
        StartCoroutine(TurnOnButton());
    }

    IEnumerator TurnOnButton()
    {
        yield return new WaitForSeconds(7f);
        _mainUI.ControllButton(1);
        _mainUI.ControllButton(3);
        _gameManager.isBluePlayerPlaying = false;
        _gameManager.isRedPlayerPlaying = false;
        OffsetTestButton();
    }

    //오프셋 세팅 관련
    private List<float> xPosP1 = new List<float>();
    private List<float> xPosP2 = new List<float>();

    private void SettingOffset()
    {
        xPosP1.Clear();
        xPosP2.Clear();
    }

    public void RecordingXPos(int player, float xPos)
    {
        if (player == 1)
        {
            xPosP1.Add(xPos);
        }
        else if (player == 2)
        {
            xPosP2.Add(xPos);
        }
    }

    public void OffsetTestButton()
    {
        float p1Center = 0;
        float p2Center = 0;

        for (int i = 0; i < xPosP1.Count; i++)
        {
            p1Center += xPosP1[i];
        }
        for (int i = 0; i < xPosP2.Count; i++)
        {
            p2Center += xPosP2[i];
        }

        try
        {
            p1Center /= xPosP1.Count;
        }
        catch (Exception)
        {
            p1Center = 0;
        }

        try
        {
            p2Center /= xPosP2.Count;
        }
        catch (Exception)
        {
            p2Center = 0;
        }
        
        if (xPosP1.Count != 0 && xPosP2.Count != 0)
        {
            _tfCenterFrameP1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 - p1Center, 30);
            _tfCenterFrameP2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 - p2Center, 30);
            FindObjectOfType<TimingManager>().SetTimingBoxs(p2Center, p1Center);
        }
        else
        {
            _tfCenterFrameP1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 30);
            _tfCenterFrameP2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 30);
            FindObjectOfType<TimingManager>().SetTimingBoxs(0, 0);
        }
    }
}
