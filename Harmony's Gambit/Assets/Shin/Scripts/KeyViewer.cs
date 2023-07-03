using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyViewer : MonoBehaviour
{
    private string pressedKey = "";

    void Update()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                pressedKey = keyCode.ToString();
                break;
            }
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label) { fontSize = 100, alignment = TextAnchor.MiddleCenter };
        GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 + 300, 600, 200), pressedKey, style);
    }
}
