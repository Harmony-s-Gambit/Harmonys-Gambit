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
    public int twoNote, oneNote, zeroNote, maxCombo;

    [SerializeField] private Text[] scoreTexts;
    [SerializeField] private List<GameObject> scoreBoardThings = new List<GameObject>();

    private MainUI _mainUI;

    private List<Text> scoreRankingNameTexts = new List<Text>();
    private List<Image> scoreRankingImages = new List<Image>();
    private List<Text> scoreRankingScoreTexts = new List<Text>();

    private void Start()
    {
        _mainUI = FindObjectOfType<MainUI>();

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        for (int i = 0; i < 8; i++)
        {
            scoreRankingNameTexts.Add(this.transform.GetChild(1).GetChild(3).GetChild(i).GetComponent<Text>());
            scoreRankingImages.Add(this.transform.GetChild(1).GetChild(4).GetChild(i).GetComponent<Image>());
            scoreRankingScoreTexts.Add(this.transform.GetChild(1).GetChild(5).GetChild(i).GetComponent<Text>());
        }

        //LoadRanking(); //테스트용
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
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        
        if (StageInfo.instance.GetStageName().Contains("Stage1_1_"))
        {
            if (GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GameClear)
            {
                StartCoroutine(NextStageButtonDelay());
            }
        }

        LoadRanking();
    }

    IEnumerator NextStageButtonDelay()
    {
        yield return new WaitForSeconds(2f);
        this.transform.GetChild(1).transform.GetChild(6).gameObject.SetActive(true);
    }

    public void RankingBoard_ScoreBoard()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void TurnOnScoreBoard(int _totalScore, string _rank, int _twoNote, int _oneNote, int _zeroNote, int _maxCombo)
    {
        totalScore = _totalScore;
        rank = _rank;
        if (rank == "SS")
        {
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else if (rank == "S")
        {
            this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else if (rank == "A")
        {
            this.gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        else if (rank == "B")
        {
            this.gameObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }
        else if (rank == "C")
        {
            this.gameObject.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        }
        else if (rank == "D")
        {
            this.gameObject.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
        }

        for (int i = 0; i < scoreBoardThings.Count; i++)
        {
            scoreBoardThings[i].SetActive(true);
        }

        StartCoroutine(TextAnimation(scoreTexts[0], _twoNote));
        StartCoroutine(TextAnimation(scoreTexts[1], _oneNote));
        StartCoroutine(TextAnimation(scoreTexts[2], _zeroNote));
        StartCoroutine(TextAnimation(scoreTexts[3], _maxCombo));
        StartCoroutine(TextAnimation(scoreTexts[4], _totalScore));
    }

    IEnumerator TextAnimation(Text txt, int num)
    {
        if (num < 1000)
        {
            for (int i = 0; i <= num; i++)
            {
                txt.text = i.ToString();
                yield return null;
            }
            txt.text = num.ToString();
        }
        else
        {
            for (int i = 0; i <= num; i += 500)
            {
                txt.text = i.ToString();
                yield return null;
            }
            txt.text = num.ToString();
        }
    }

    public void LoadingNextStageButton()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        if (StageInfo.instance.GetStageName().Contains("Stage1_1_"))
        {
            StageInfo.instance.SetStageName("BossStage1");
        }

        SceneManager.LoadScene("LoadingScene");
        Destroy(GameObject.Find("Managers"));
        Destroy(GameObject.Find("MainCanvas"));
        Destroy(GameObject.Find("ScoreBoardCanvas"));
        //GameOver_MainButton();
    }

    //json 저장

    RankingData rankingData = new RankingData();

    public void TurnOnSaveRankingBoard()
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        
        if (rank == "SS")
        {
            this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Images/UI/SS", typeof(Sprite)) as Sprite;
        }
        else if (rank == "S")
        {
            this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Images/UI/S", typeof(Sprite)) as Sprite;
        }
        else if (rank == "A")
        {
            this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Images/UI/A", typeof(Sprite)) as Sprite;
        }
        else if (rank == "B")
        {
            this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Images/UI/B", typeof(Sprite)) as Sprite;
        }
        else if (rank == "C")
        {
            this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Images/UI/C", typeof(Sprite)) as Sprite;
        }
        else if (rank == "D")
        {
            this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Images/UI/D", typeof(Sprite)) as Sprite;
        }
        else
        {
            this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Images/UI/F", typeof(Sprite)) as Sprite;
        }

        this.gameObject.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = totalScore.ToString();
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

    //json 불러오기

    private void LoadRanking()
    {
        //string filePath = Path.Combine(Application.dataPath, "Ranking", "Stage1"); //테스트용
        //this.gameObject.transform.GetChild(1).gameObject.SetActive(true); //테스트용

        List<RankingData> rankDatas = new List<RankingData>();

        if (StageInfo.instance.GetStageName().Contains("Stage1_1")) 
        {
            for (int i = 1; i < 4; i++) //나중에 스테이지 인포에 각 스테이지가 몇 개 있는지 저장한 후 사용
            {
                string filePath = Path.Combine(Application.dataPath, "Ranking", "Stage1_1_" + i.ToString());

                DirectoryInfo di = new DirectoryInfo(filePath);

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
            }
        }
        else
        {
            string filePath = Path.Combine(Application.dataPath, "Ranking", StageInfo.instance.GetStageName());

            DirectoryInfo di = new DirectoryInfo(filePath);

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

