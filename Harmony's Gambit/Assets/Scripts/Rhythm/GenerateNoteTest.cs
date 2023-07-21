using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateNoteTest : MonoBehaviour
{
    public static GenerateNoteTest instance;

    [SerializeField] Transform _tfNoteAppearP1;
    [SerializeField] Transform _tfNoteAppearP2;

    private void Start()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (NoteManager.instance.currentBGM != "") //BGM 이름 설정 시 노트 생성 시작
        {
            NoteManager.instance.GenerateNote();
        }
    }

    public void GamePlay1Button() //게임 플레이 시 설정, 노트 생성 시작, 즉 게임 시작 버튼
    {
        NoteManager.instance.SetBGMValue("BGM1");
    }

    public void OffsetStartButton()
    {
        NoteManager.instance.SetBGMValue("Offset");
        SettingOffset();
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

        p1Center /= xPosP1.Count;
        p2Center /= xPosP2.Count;

        print(p1Center);
        print(p2Center);

        _tfNoteAppearP1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000 - p1Center, 0);
        _tfNoteAppearP2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000 - p2Center, 0);
    }
}
