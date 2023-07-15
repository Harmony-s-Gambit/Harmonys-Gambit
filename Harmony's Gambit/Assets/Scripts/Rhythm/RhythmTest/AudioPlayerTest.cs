using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayerTest : MonoBehaviour
{
    [SerializeField] InputField _bgmNameInputField;

    public void PlayBGMButton()
    {
        AudioManager.instance.PlayBGM(_bgmNameInputField.text);
    }

    public void PlaySFXButton()
    {
        AudioManager.instance.PlaySFX(_bgmNameInputField.text);
    }

    public void StopBGMButton()
    {
        AudioManager.instance.StopBGM();
    }
}
