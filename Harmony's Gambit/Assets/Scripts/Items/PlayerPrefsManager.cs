using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrefsManager : MonoBehaviour
{
    public static event Action<string> PlayerPrefsValueChanged; // PlayerPrefs ���� ����� �� ȣ��� �̺�Ʈ

    // PlayerPrefs �� ����
    public static void SetPlayerPrefsValue(string key, int value)
    {
        //PlayerPrefs.SetInt("hasSpear", 0);
        //PlayerPrefs.SetInt("hasSweeper", 0);
        //PlayerPrefs.SetInt("hasCollar", 0);
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save(); // ���� ���� ����

        // ���� ����Ǿ����� �˸��� �̺�Ʈ ȣ��
        PlayerPrefsValueChanged?.Invoke(key);
    }
}
