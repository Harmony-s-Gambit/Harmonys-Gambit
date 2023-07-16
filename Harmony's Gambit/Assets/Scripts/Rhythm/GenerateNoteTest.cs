using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNoteTest : MonoBehaviour
{
    private void Update()
    {
        if (NoteManager.instance.currentBGM != "") //BGM �̸� ���� �� ��Ʈ ���� ����
        {
            NoteManager.instance.GenerateNote();
        }
    }

    public void GamePlay1Button() //���� �÷��� �� ����, ��Ʈ ���� ����, �� ���� ���� ��ư
    {
        NoteManager.instance.SetBGMValue("BGM1");
        NoteManager.instance.currentBGM = "BGM1";
        NoteManager.instance.bgmListindex = 0;
    }
}
