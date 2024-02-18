using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrefsManager : MonoBehaviour
{
    public static event Action<string> PlayerPrefsValueChanged; // PlayerPrefs 값이 변경될 때 호출될 이벤트

    // PlayerPrefs 값 설정
    public static void SetPlayerPrefsValue(string key, int value)
    {
        //PlayerPrefs.SetInt("hasSpear", 0);
        //PlayerPrefs.SetInt("hasSweeper", 0);
        //PlayerPrefs.SetInt("hasCollar", 0);
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save(); // 변경 사항 저장

        // 값이 변경되었음을 알리는 이벤트 호출
        PlayerPrefsValueChanged?.Invoke(key);
    }
}
