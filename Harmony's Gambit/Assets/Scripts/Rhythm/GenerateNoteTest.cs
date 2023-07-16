using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNoteTest : MonoBehaviour
{
    private void Update()
    {
        if (NoteManager.instance.currentBGM != "") //BGM 이름 설정 시 노트 생성 시작
        {
            NoteManager.instance.GenerateNote();
        }
    }

    public void GamePlay1Button() //게임 플레이 시 설정, 노트 생성 시작, 즉 게임 시작 버튼
    {
        NoteManager.instance.SetBGMValue("BGM1");
        NoteManager.instance.currentBGM = "BGM1";
        NoteManager.instance.bgmListindex = 0;
    }
}
