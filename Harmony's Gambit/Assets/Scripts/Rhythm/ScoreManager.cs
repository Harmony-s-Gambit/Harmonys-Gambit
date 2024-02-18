using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private GameManager _gameManager;
    private PlayerManager _playerManager;

    public static ScoreManager instance;

    public bool rhythm;
    public int currentScore = 0;
    public float currentTime = 0;
    public double time = 0;
    public bool isTimeOver = false;
    public int totalScore = 0;
    private int beatListCount = 0;

    //Ÿ�̸�
    private Text timerText;
    private bool isTimerStarted = false; //ù ��Ʈ ��� �� Ÿ�̸� ����
    private double currentTimerTime = 0; //Ÿ�̸��� �ð�

    //��Ʈ ����
    private int[] noteScore = new int[] { 2000, 1500, 0, -1000, -1500, -3000 }; // ���ʴ�� Ÿ�ӿ��� ���� ��Ʈ 2�� 1�� 0��, Ÿ�� ���� ���� ��Ʈ 2�� 1�� 0�� �Է� �� ��� ����

    //�� óġ�� ����
    private int totalKillScore = 0;
    public int purpleMouseScore2 = 10000;
    public int purpleHyenaScore = 20000;

    //�޺� ����
    private int currentCombo = 0; //���� �޺�
    private int comboUnit = 10; //������ �� �޺��� ����
    private int maxCombo = 100; //�޺� ���� ������ ������ ��, �� ���ڸ�ŭ�� �޺� �ñ��� ������ ������. �������ʹ� ������ ����
    private int maxComboScore = 1000; //�޺��� �ִ� �� ���� ������ �ִ�ġ

    //��Ÿ ����
    private int stageClearScore = 100; //�������� Ŭ���� ���� ����
    private int stageFailScore = 0; //�������� ���� ����
    private int timeOverScore = 0; //Ÿ�� ���� ���� �� ����
    //Ŭ���� �� ������ ��Ʈ�� ���� 2���� ���� �߰� �ʿ�

    //���� ���
    private int twoNote = 0; //�� ��Ʈ �Է� ��
    private int oneNote = 0; //�� ��Ʈ �Է� ��
    private int zeroNote = 0; //��ģ ��Ʈ ��
    private int maximumCombo = 0; //���� ��� �̾��� �޺�
    private int killedMob = 0; //�� óġ ��

    //�޺� ui
    private GameObject comboEffect;
    private GameObject combo100Effect;

    private void Start()
    {
        currentScore = 0;
        currentTime = 0;
        currentCombo = 0;

        instance = this;
        _gameManager = FindObjectOfType<GameManager>();
        _playerManager = FindObjectOfType<PlayerManager>();

        
        try
        {
            comboEffect = GameObject.Find("combo_effect");
            combo100Effect = GameObject.Find("100combo_effect_0");
            comboEffect.SetActive(false);
            combo100Effect.SetActive(false);

            timerText = GameObject.Find("TimerText").GetComponent<Text>();
        }
        catch (System.Exception) { }
    }

    private void Update()
    {
        if (PlayAudio.instance._isMusiceStart)
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime > time && !isTimeOver)
        {
            isTimeOver = true;
            currentScore += timeOverScore;
            //AudioManager.instance.PlayBGM(NoteManager.instance.currentBgmNameAfterTimeOver);
        }
        
        if (rhythm)
        {
            //print(currentScore);
            //print(_gameManager.redPlayer.HP);
            //print(currentCombo);
            rhythm = false;
        }

        if (isTimerStarted)
        {
            currentTimerTime -= Time.deltaTime;
            timerText.text = WhatTImeInTimer(currentTimerTime);
        }
    }

    public void SetIsTimerStarted(bool _bool)
    {
        isTimerStarted = _bool;
    }

    private string WhatTImeInTimer(double _time)
    {
        int min = 0;
        
        for (int i = 0; _time >= 60; i++)
        {
            if (_time >= 60d)
            {
                min++;
                _time -= 60d;
            }
        }

        string minStr;

        if (min < 10)
        {
            minStr = "0" + min.ToString();
        }
        else
        {
            minStr = min.ToString();
        }

        if (_time < 0)
        {
            return "00:00";
        }
        else if (_time < 10)
        {
            return minStr + ":0" + ((int)_time).ToString();
        }
        else
        {
            return minStr + ":" + ((int)_time).ToString();
        }
    }

    public void TotalEnemyScore(List<GameObject> enemies)
    {
        totalKillScore = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains("purpleMouse"))
            {
                totalKillScore += purpleMouseScore2;
            }
            else if (enemies[i].name.Contains("purpleHyena"))
            {
                totalKillScore += purpleHyenaScore;
            }
        }
    }

    public int TotalComboScore(int beatListCount)
    {
        int totalComboScore = 0;
        int tmpComboScore = 1;

        while (beatListCount > comboUnit)
        {
            beatListCount -= comboUnit;
            totalComboScore += tmpComboScore * 100;
            
            if (tmpComboScore != 10)
            {
                tmpComboScore += 1;
            }
        }
        //print(totalComboScore);
        return totalComboScore;
    }

    public void GameStartSetting()
    {
        currentScore = 0;
        currentTime = 0;
        currentCombo = 0;
        twoNote = 0;
        oneNote = 0;
        zeroNote = 0;
        maximumCombo = 0;
        killedMob = 0;
        time = NoteManager.instance.time;
        totalScore = 0;
        isTimeOver = false;
        beatListCount = NoteManager.instance.currentBeatList.Count;
        currentTimerTime = NoteManager.instance.time;
        timerText.text = WhatTImeInTimer(currentTimerTime);
    }

    public void KillScore(int score)
    {
        currentScore += score;
        killedMob += 1;
    }

    public void ComboScore(bool p1, bool p2)
    {
        if (p1 && p2)
        {
            AudioManager.instance.PlaySFX("Perfect");
            currentCombo += 1;
            twoNote += 1;
        }
        else if (p1 || p2)
        {
            AudioManager.instance.PlaySFX("Good");
            comboEffect.SetActive(false);
            combo100Effect.SetActive(false);
            currentCombo = 0;
            oneNote += 1;
        }
        else
        {
            comboEffect.SetActive(false);
            combo100Effect.SetActive(false);
            currentCombo = 0;
            zeroNote += 1;
        }

        if (currentCombo % comboUnit == 0 && currentCombo != 0 && currentCombo < maxCombo)
        {
            //print(currentCombo);
            currentScore += currentCombo / comboUnit * 100;

            comboEffect.SetActive(false);
            comboEffect.SetActive(true);
            comboEffect.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "x" + currentCombo;
        }
        else if (currentCombo % comboUnit == 0 && currentCombo >= maxCombo)
        {
            currentScore += maxComboScore;
            comboEffect.SetActive(false);
            combo100Effect.SetActive(false);
            combo100Effect.SetActive(true);
        }

        if (currentCombo > maximumCombo)
        {
            maximumCombo = currentCombo;
        }
    }

    public void NoteScore(bool p1, bool p2)
    {
        if (currentTime < time)
        {
            if (p1 && p2)
            {
                currentScore += noteScore[0];
            }
            else if (p1 || p2)
            {
                currentScore += noteScore[1];
            }
            else
            {
                currentScore += noteScore[2];
            }
        }
        else
        {
            if (p1 && p2)
            {
                currentScore += noteScore[3];
            }
            else if (p1 || p2)
            {
                currentScore += noteScore[4];
            }
            else
            {
                currentScore += noteScore[5];
            }
        }
    }

    public void StageClearScore(int index)
    {
        if (index != -1)
        {
            currentScore += stageClearScore * (_gameManager.redPlayer.HP + _gameManager.bluePlayer.HP);
            
            if (!isTimeOver)
            {
                currentScore += (beatListCount - twoNote - oneNote - zeroNote) * noteScore[0];

                float num = beatListCount - (beatListCount % 10) - twoNote - oneNote - zeroNote;
                int numCeil = (int)(Mathf.Ceil(num / 10) * 10);
                currentScore += numCeil * 100;
            }
        }
        totalScore = currentScore;
        
        try
        {
            GameObject.Find("ScoreBoardCanvas").transform.GetChild(0).gameObject.SetActive(true); //���ھ� ���� �ѱ�
            //GameObject.Find("ScoreBoardCanvas").GetComponent<ScoreBoardCanvas>().TurnOnScoreBoard(totalScore, WhatRank(totalScore));
            if (_playerManager.GameOver)
            {
                GameObject.Find("ScoreBoardCanvas").GetComponent<ScoreBoardCanvas>().TurnOnScoreBoard(totalScore, WhatRank(totalScore), twoNote, oneNote, beatListCount - twoNote - oneNote, maximumCombo);
            }
            else
            {
                GameObject.Find("ScoreBoardCanvas").GetComponent<ScoreBoardCanvas>().TurnOnScoreBoard(totalScore, WhatRank(totalScore), beatListCount - oneNote - zeroNote, oneNote, zeroNote, maximumCombo);
            }
        }
        catch (System.Exception) { }
    }

    public void StageFailScore()
    {
        currentScore += stageFailScore;
    }

    private string WhatRank(int score)
    {
        float scoreRatio = (float)score / (beatListCount * noteScore[0] + totalKillScore);
        //print(beatListCount * noteScore[0] + 800 + totalKillScore + TotalComboScore(beatListCount));
        if (score >= (beatListCount * noteScore[0] + 800 + totalKillScore + TotalComboScore(beatListCount)))
        {
            return "SS";
        }
        else if (scoreRatio > 1f)
        {
            return "S";
        }
        else if (scoreRatio > 0.8f)
        {
            return "A";
        }
        else if (scoreRatio > 0.6f)
        {
            return "B";
        }
        else if (scoreRatio > 0.4f)
        {
            return "C";
        }
        else if (scoreRatio > 0.2f)
        {
            return "D";
        }
        else
        {
            return "F";
        }
    }
}
