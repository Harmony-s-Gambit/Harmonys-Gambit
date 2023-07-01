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

    [SerializeField] Sound[] _sfx;
    [SerializeField] Sound[] _bgm;

    [SerializeField] AudioSource _bgmPlayer;
    [SerializeField] AudioSource[] _sfxPlayer;

    private void Start()
    {
        instance = this;
    }

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < _bgm.Length; i++)
        {
            if (p_bgmName == _bgm[i]._name)
            {
                _bgmPlayer.clip = _bgm[i]._clip;
                _bgmPlayer.Play();
                return;
            }
        }
        print("�ش� �̸��� ��������� �����ϴ�.");
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
                print("��� ����� �÷��̾ ������Դϴ�.");
                return;
            }
        }
        print("�ش� �̸��� ȿ������ �����ϴ�.");
        return;
    }
}