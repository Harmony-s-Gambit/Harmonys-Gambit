using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string _name;
    public AudioClip _clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private GameManager _gameManager;

    [SerializeField] Sound[] _sfx;
    [SerializeField] Sound[] _bgm;

    [SerializeField] AudioSource _bgmPlayer;
    [SerializeField] AudioSource[] _sfxPlayer;

    private bool isPaused = false;

    private void Start()
    {
        instance = this;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.isGameStart)
        {
            if (ScoreManager.instance.currentTime > ScoreManager.instance.time && !_bgmPlayer.isPlaying)
            {
                PlayBGM(NoteManager.instance.currentBgmNameAfterTimeOver);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameManager.isGameStart)
            {
                if (isPaused)
                {
                    _bgmPlayer.UnPause();
                    FindObjectOfType<NoteManager>().SetIsPaused(false);
                    FindObjectOfType<PlayerController>().SetIsPaused(false);

                    Note[] tmp_1 = FindObjectsOfType<Note>();
                    for (int i = 0; i < tmp_1.Length; i++)
                    {
                        tmp_1[i].SetNoteSpeed(500);
                    }

                    Note2[] tmp_2 = FindObjectsOfType<Note2>();
                    for (int i = 0; i < tmp_2.Length; i++)
                    {
                        tmp_2[i].SetNoteSpeed(500);
                    }

                    NoteIn[] tmp_3 = FindObjectsOfType<NoteIn>();
                    for (int i = 0; i < tmp_3.Length; i++)
                    {
                        tmp_3[i].SetNoteSpeed(500);
                    }
                }
                else
                {
                    _bgmPlayer.Pause();
                    FindObjectOfType<NoteManager>().SetIsPaused(true);
                    FindObjectOfType<PlayerController>().SetIsPaused(true);

                    Note[] tmp_1 = FindObjectsOfType<Note>();
                    for (int i = 0; i < tmp_1.Length; i++)
                    {
                        tmp_1[i].SetNoteSpeed(0);
                    }

                    Note2[] tmp_2 = FindObjectsOfType<Note2>();
                    for (int i = 0; i < tmp_2.Length; i++)
                    {
                        tmp_2[i].SetNoteSpeed(0);
                    }

                    NoteIn[] tmp_3 = FindObjectsOfType<NoteIn>();
                    for (int i = 0; i < tmp_3.Length; i++)
                    {
                        tmp_3[i].SetNoteSpeed(0);
                    }
                }
                isPaused = !isPaused;
            }
        }
    }

    public void PlayBGM(string p_bgmName, float delay = 0f)
    {
        if (p_bgmName == "Lobby")
        {
            _bgmPlayer.loop = true;
        }
        else
        {
            _bgmPlayer.loop = false;
        }

        for (int i = 0; i < _bgm.Length; i++)
        {
            if (p_bgmName == _bgm[i]._name)
            {
                _bgmPlayer.clip = _bgm[i]._clip;
                _bgmPlayer.time = delay;
                _bgmPlayer.Play();
                return;
            }
        }
        print("해당 이름의 배경음악이 없습니다.");
        return;
    }

    public void StopBGM()
    {
        _bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < _sfx.Length; i++)
        {
            if (p_sfxName == _sfx[i]._name)
            {
                for (int j = 0; j < _sfxPlayer.Length; j++)
                {
                    if (!_sfxPlayer[j].isPlaying)
                    {
                        _sfxPlayer[j].clip = _sfx[i]._clip;
                        _sfxPlayer[j].Play();
                        return;
                    }
                }
                print("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }
        print("해당 이름의 효과음이 없습니다.");
        return;
    }

    public void SetIsPaused(bool _bool)
    {
        isPaused = true;
    }
}