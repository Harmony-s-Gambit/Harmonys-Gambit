using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class RankingData
{
    public string name;
    public string rank;
    public int score;
}

public class ScoreBoardCanvas : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void GameOver_MainButton()
    {
        SceneManager.LoadScene("Integrated");
        Destroy(GameObject.Find("Managers"));
        Destroy(GameObject.Find("MainCanvas"));
        Destroy(GameObject.Find("ScoreBoardCanvas"));
    }

    public void ScoreBoard_RankingBoard()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void RankingBoard_ScoreBoard()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    //json ¿˙¿Â

    RankingData rankingData = new RankingData();

    public void TurnOnSaveRankingBoard()
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void TurnOffSaveRankingBoard()
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void SaveRanking()
    {

    }
}

