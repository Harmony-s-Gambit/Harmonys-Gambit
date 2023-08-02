using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateNoteTest : MonoBehaviour
{
    public static GenerateNoteTest instance;

    [SerializeField] Transform _tfCenterFrameP1;
    [SerializeField] Transform _tfCenterFrameP2;

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

    public void RecordingXPos(int player, float yPos)
    {
        if (player == 1)
        {
            xPosP1.Add(yPos);
        }
        else if (player == 2)
        {
            xPosP2.Add(yPos);
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
        
        if (xPosP1.Count != 0 && xPosP2.Count != 0)
        {
            _tfCenterFrameP1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 + p1Center, 0);
            _tfCenterFrameP2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 + p2Center, 0);
        }

        FindObjectOfType<TimingManager>().SetTimingBoxs(p1Center, p2Center);
    }
}
