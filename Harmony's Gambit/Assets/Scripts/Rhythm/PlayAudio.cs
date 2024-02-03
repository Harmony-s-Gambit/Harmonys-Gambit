using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour
{
    public static PlayAudio instance;

    public bool _isMusiceStart = false;
    private double _currentTime = 0d;
    public bool _isGameStart = false;

    private void Start()
    {
        instance = this;
    }

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
        if (collision.CompareTag("NoteIn") && !ScoreManager.instance.isTimeOver)
        {
            _currentTime = 0;
            _isGameStart = true;
            if (NoteManager.instance.currentBGM == "Offset")
            {
                AudioManager.instance.PlaySFX("Clap");
            }
        }
    }
}
