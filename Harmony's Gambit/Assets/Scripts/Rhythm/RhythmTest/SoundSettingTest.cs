using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingTest : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider, sfxSlider;
    [SerializeField] AudioSource _bgmPlayer;
    [SerializeField] AudioSource[] _sfxPlayer;

    private void Start()
    {
        bgmSlider.value = _bgmPlayer.volume;
        sfxSlider.value = _sfxPlayer[0].volume;
    }

    private void Update()
    {
        _bgmPlayer.volume = bgmSlider.value;

        for (int i = 0; i < _sfxPlayer.Length; i++)
        {
            _sfxPlayer[i].volume = sfxSlider.value;
        }
    }
}
