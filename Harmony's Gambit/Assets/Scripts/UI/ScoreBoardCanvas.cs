using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

class RankingData
{
    public string name;
    public string rank;
    public int score;
}

public class ScoreBoardCanvas : MonoBehaviour
{
    public int totalScore;
    public string rank;

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
        rankingData.name = GameObject.Find("NameInputField").GetComponent<InputField>().text;
        rankingData.score = totalScore;
        rankingData.rank = rank;

        string jsonData = JsonUtility.ToJson(rankingData);
        string filePath = Path.Combine(Application.dataPath, "Ranking", "ScoreData" + rankingData.name + ".txt");

        var file = File.CreateText(filePath);
        file.Close();

        StreamWriter sw = new StreamWriter(filePath);

        sw.WriteLine(jsonData);
        sw.Flush();
        sw.Close();
    }
}

