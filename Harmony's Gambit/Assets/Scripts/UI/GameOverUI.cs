using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void GameOver_MainButton()
    {
        SceneManager.LoadScene("Integrated");
        Destroy(GameObject.Find("Managers"));
        Destroy(GameObject.Find("MainCanvas"));
        Destroy(GameObject.Find("ScoreBoardCanvas"));
    }
}
