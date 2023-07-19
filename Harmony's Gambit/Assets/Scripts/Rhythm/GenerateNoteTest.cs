using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNoteTest : MonoBehaviour
{
    double _currentTime = 0d;

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
    }
}
