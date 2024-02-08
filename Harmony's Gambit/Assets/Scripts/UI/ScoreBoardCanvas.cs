using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

[System.Serializable]
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

    private List<Text> scoreRankingNameTexts = new List<Text>();
    private List<Image> scoreRankingImages = new List<Image>();
    private List<Text> scoreRankingScoreTexts = new List<Text>();

    private void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        for (int i = 0; i < 8; i++)
        {
            scoreRankingNameTexts.Add(this.transform.GetChild(1).GetChild(3).GetChild(i).GetComponent<Text>());
            scoreRankingImages.Add(this.transform.GetChild(1).GetChild(4).GetChild(i).GetComponent<Image>());
            scoreRankingScoreTexts.Add(this.transform.GetChild(1).GetChild(5).GetChild(i).GetComponent<Text>());
        }

        //LoadRanking(); //�׽�Ʈ��
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

        LoadRanking();
    }

    public void RankingBoard_ScoreBoard()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    //json ����

    RankingData rankingData = new RankingData();

    public void TurnOnSaveRankingBoard()
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        
        if (rank == "SS")
        {
            this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load("Images/UI/SS", typeof(Sprite)) as Sprite;
        }
        else if (rank == "S")
        {
            this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load("Images/UI/S", typeof(Sprite)) as Sprite;
        }
        else if (rank == "A")
        {
            this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load("Images/UI/A", typeof(Sprite)) as Sprite;
        }
        else if (rank == "B")
        {
            this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load("Images/UI/B", typeof(Sprite)) as Sprite;
        }
        else if (rank == "C")
        {
            this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load("Images/UI/C", typeof(Sprite)) as Sprite;
        }
        else if (rank == "D")
        {
            this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load("Images/UI/D", typeof(Sprite)) as Sprite;
        }
        else
        {
            this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load("Images/UI/F", typeof(Sprite)) as Sprite;
        }

        this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = totalScore.ToString();
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
        string filePath = Path.Combine(Application.dataPath, "Ranking", StageInfo.instance.GetStageName(), "ScoreData" + rankingData.name + ".txt");

        var file = File.CreateText(filePath);
        file.Close();

        StreamWriter sw = new StreamWriter(filePath);

        sw.WriteLine(jsonData);
        sw.Flush();
        sw.Close();

        LoadRanking();
    }

    //json �ҷ�����

    private void LoadRanking()
    {
        //string filePath = Path.Combine(Application.dataPath, "Ranking", "Stage1"); //�׽�Ʈ��
        //this.gameObject.transform.GetChild(1).gameObject.SetActive(true); //�׽�Ʈ��
        string filePath = Path.Combine(Application.dataPath, "Ranking", StageInfo.instance.GetStageName());

        DirectoryInfo di = new DirectoryInfo(filePath);
        
        List<RankingData> rankDatas = new List<RankingData>();

        foreach (FileInfo file in di.GetFiles())
        {
            if (file.Name.Contains(".txt") && !file.Name.Contains(".meta"))
            {
                string value = "";
                StreamReader reader = new StreamReader(file.ToString());
                value = reader.ReadToEnd();
                reader.Close();

                rankDatas.Add(JsonConvert.DeserializeObject<RankingData>(value));

                //Debug.Log($"{rankData.name}, {rankData.score}, {rankData.rank}");
            }
        }

        List<RankingData> rankDatas_sorted = new List<RankingData>();
        int maxScore = 0;
        int maxScoreIndex = 0;
        
        for (int j = 0; j < 8; j++)
        {
            if (rankDatas.Count > 0)
            {
                for (int i = 0; i < rankDatas.Count; i++)
                {
                    if (rankDatas[i].score > maxScore)
                    {
                        maxScore = rankDatas[i].score;
                        maxScoreIndex = i;
                    }
                }

                rankDatas_sorted.Add(rankDatas[maxScoreIndex]);
                rankDatas.RemoveAt(maxScoreIndex);
                maxScore = 0;
            }
        }

        for (int i = 0; i < rankDatas_sorted.Count; i++)
        {
            scoreRankingNameTexts[i].text = rankDatas_sorted[i].name;
            scoreRankingScoreTexts[i].text = rankDatas_sorted[i].score.ToString();

            if (rankDatas_sorted[i].rank == "SS")
            {
                scoreRankingImages[i].sprite = Resources.Load("Images/UI/SS", typeof(Sprite)) as Sprite;
            }
            else if (rankDatas_sorted[i].rank == "S")
            {
                scoreRankingImages[i].sprite = Resources.Load("Images/UI/S", typeof(Sprite)) as Sprite;
            }
            else if (rankDatas_sorted[i].rank == "A")
            {
                scoreRankingImages[i].sprite = Resources.Load("Images/UI/A", typeof(Sprite)) as Sprite;
            }
            else if (rankDatas_sorted[i].rank == "B")
            {
                scoreRankingImages[i].sprite = Resources.Load("Images/UI/B", typeof(Sprite)) as Sprite;
            }
            else if (rankDatas_sorted[i].rank == "C")
            {
                scoreRankingImages[i].sprite = Resources.Load("Images/UI/C", typeof(Sprite)) as Sprite;
            }
            else if (rankDatas_sorted[i].rank == "D")
            {
                scoreRankingImages[i].sprite = Resources.Load("Images/UI/D", typeof(Sprite)) as Sprite;
            }
            else
            {
                scoreRankingImages[i].sprite = Resources.Load("Images/UI/F", typeof(Sprite)) as Sprite;
            }

            Color col = scoreRankingImages[i].color;
            col.a = 1f;
            scoreRankingImages[i].color = col;
        }
    }
}

