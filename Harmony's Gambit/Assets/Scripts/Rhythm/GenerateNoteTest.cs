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
        if (NoteManager.instance.currentBGM != "") //BGM �̸� ���� �� ��Ʈ ���� ����
        {
            NoteManager.instance.GenerateNote();
        }
    }

    public void GamePlay1Button() //���� �÷��� �� ����, ��Ʈ ���� ����, �� ���� ���� ��ư
    {
        NoteManager.instance.SetBGMValue("BGM1");
    }

    public void OffsetStartButton()
    {
        NoteManager.instance.SetBGMValue("Offset");
        SettingOffset();
    }

    //������ ���� ����
    private List<float> yPosP1 = new List<float>();
    private List<float> yPosP2 = new List<float>();

    private void SettingOffset()
    {
        yPosP1.Clear();
        yPosP2.Clear();
    }

    public void RecordingXPos(int player, float yPos)
    {
        if (player == 1)
        {
            yPosP1.Add(yPos);
        }
        else if (player == 2)
        {
            yPosP2.Add(yPos);
        }
    }

    public void OffsetTestButton()
    {
        float p1Center = 0;
        float p2Center = 0;

        for (int i = 0; i < yPosP1.Count; i++)
        {
            p1Center += yPosP1[i];
        }
        for (int i = 0; i < yPosP2.Count; i++)
        {
            p2Center += yPosP2[i];
        }

        p1Center /= yPosP1.Count;
        p2Center /= yPosP2.Count;

        _tfNoteAppearP1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200 - p1Center);
        _tfNoteAppearP2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200 - p2Center);
    }
}
