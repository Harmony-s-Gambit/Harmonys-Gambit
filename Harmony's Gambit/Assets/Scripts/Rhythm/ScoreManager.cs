using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private GameManager _gameManager;

    public static ScoreManager instance;

    public bool rhythm;
    private int currentScore = 0;
    private float currentTime = 0;
    private double time = 0;
    private bool isTimeOver = false;

    //노트 점수
    private int[] noteScore = new int[] { 2, 1, 0, -1, -2, -3 }; // 차례대로 타임오버 이전 노트 2개 1개 0개, 타임 오버 이후 노트 2개 1개 0개 입력 시 얻는 점수

    //적 처치시 점수
    public int mouseScore = 10;
    public int hienaScore = 30;

    //콤보 점수
    private int currentCombo = 0; //현재 콤보
    private int comboUnit = 10; //점수를 줄 콤보의 단위
    private int maxCombo = 100; //콤보 점수 증가의 마지막 수, 이 숫자만큼의 콤보 시까지 점수가 증가함. 다음부터는 점수가 유지
    private int maxComboScore = 10; //콤보로 주는 한 번의 점수의 최대치

    //기타 점수
    private int stageClearScore = 10; //스테이지 클리어 점수 배율
    private int stageFailScore = 0; //스테이지 실패 점수
    private int timeOverScore = -10; //타임 오버 진입 시 점수
    //클리어 시 나머지 노트에 대해 2점씩 점수 추가 필요

    //점수 기록
    private int twoNote = 0; //두 노트 입력 수
    private int oneNote = 0; //한 노트 입력 수
    private int zeroNote = 0; //놓친 노트 수
    private int maximumCombo = 0; //가장 길게 이어진 콤보
    private int killedMob = 0; //적 처치 수

    private void Start()
    {
        currentScore = 0;
        currentTime = 0;
        currentCombo = 0;

        instance = this;
        _gameManager = FindObjectOfType<GameManager>();

        
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
        }
        
        if (rhythm)
        {
            //print(currentScore);
            //print(_gameManager.redPlayer.HP);
            //print(currentCombo);
            rhythm = false;
        }
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
            currentCombo += 1;
            twoNote += 1;
        }
        else if (p1 || p2)
        {
            oneNote += 1;
        }
        else
        {
            currentCombo = 0;
            zeroNote += 1;
        }

        if (currentCombo % comboUnit == 0 && currentCombo != 0 && currentCombo <= maxCombo)
        {
            //print(currentCombo);
            currentScore += currentCombo / comboUnit;
        }
        else if (currentCombo % comboUnit == 0 && currentCombo > maxCombo)
        {
            currentScore += maxComboScore;
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
        currentScore += stageClearScore * (_gameManager.redPlayer.HP + _gameManager.bluePlayer.HP);
        currentScore += (NoteManager.instance.currentBeatList.Count - twoNote - oneNote - zeroNote) * 2;

        if (index == 0)
        {
            GameObject.Find("ScoreBoardCanvas").transform.GetChild(0).gameObject.SetActive(true); //스코어 보드 켜기
        }
    }

    public void StageFailScore()
    {
        currentScore += stageFailScore;
    }
}
