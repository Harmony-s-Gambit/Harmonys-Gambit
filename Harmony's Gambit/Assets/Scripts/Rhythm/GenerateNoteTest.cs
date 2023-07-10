using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNoteTest : MonoBehaviour
{
    int _bgmNum = 0;

    double _currentTime;
    bool isPlaying = false;

    private void Update()
    {
        if (_bgmNum == 1)
        {
            NoteManager.instance.GenerateNote("BGM1");
            _currentTime += Time.deltaTime;
            if (BGMTextReader.instance.BGMTextRead("BGM1")[2] < _currentTime && !isPlaying)
            {
                AudioManager.instance.PlayBGM("BGM1");
                isPlaying = true;
            }
        }
        else if (_bgmNum == 2)
        {
            NoteManager.instance.GenerateNote("Unity");
            _currentTime += Time.deltaTime;
            if (BGMTextReader.instance.BGMTextRead("Unity")[2] < _currentTime && !isPlaying)
            {
                AudioManager.instance.PlayBGM("Unity");
                isPlaying = true;
            }
        }
    }

    public void GamePlay1Button()
    {
        _bgmNum = 1;
        NoteManager.instance.bgmListindex = 10;
    }

    public void GamePlay2Button()
    {
        _bgmNum = 2;
        NoteManager.instance.bgmListindex = 10;
    }
}
