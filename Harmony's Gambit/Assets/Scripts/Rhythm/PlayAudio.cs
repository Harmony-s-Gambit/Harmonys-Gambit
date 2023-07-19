using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private bool _isMusiceStart = false;
    private double _currentTime = 0d;
    private bool _isGameStart = false;
    private int _NoteNum = 0;

    private void FixedUpdate()
    {
        if (_isGameStart)
        {
            if (NoteManager.instance.delay > 0)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime > NoteManager.instance.delay)
                {
                    if (!_isMusiceStart)
                    {
                        AudioManager.instance.PlayBGM(NoteManager.instance.currentBGM);
                        _isMusiceStart = true;

                    }
                }
            }
            else
            {
                if (!_isMusiceStart)
                {
                    AudioManager.instance.PlayBGM(NoteManager.instance.currentBGM, NoteManager.instance.delay * -1);
                    _isMusiceStart = true;

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteP1") || collision.CompareTag("NoteP2"))
        {
            if (_NoteNum == 0) //noteNum 초기화 필요
            {
                _NoteNum++;
            }
            else if (_NoteNum == 1)
            {
                _currentTime = 0;
                _isGameStart = true;
                _NoteNum++;
            }
        }
    }
}
