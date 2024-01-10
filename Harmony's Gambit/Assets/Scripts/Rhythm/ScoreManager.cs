using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private GameManager _gameManager;

    public static ScoreManager instance;

    public bool rhythm;
    public int currentScore = 0;
    public float currentTime = 0;
    public double time = 0;
    private bool isTimeOver = false;

    //노트 점수
    private int[] noteScore = new int[] { 2, 1, 0, -1, -2, -3 }; // 차례대로 타임오버 이전 노트 2개 1개 0개, 타임 오버 이후 노트 2개 1개 0개 입력 시 얻는 점수

    //적 처치시 점수
    public int mouseScore = 10;
    public int hienaScore = 30;

    //콤보 점수
    public int currentCombo = 0; //현재 콤보
    private int comboUnit = 10; //점수를 줄 콤보의 단위
    private int maxCombo = 100; //콤보 점수 증가의 마지막 수, 이 숫자만큼의 콤보 시까지 점수가 증가함. 다음부터는 점수가 유지
    private int maxComboScore = 10; //콤보로 주는 한 번의 점수의 최대치

    //기타 점수
    private int stageClearScore = 10; //스테이지 클리어 점수 배율
    private int stageFailScore = 0; //스테이지 실패 점수
    private int timeOverScore = -10; //타임 오버 진입 시 점수

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

    public void GetScore(int score)
    {
        currentScore += score;
    }

    public void ComboScore(bool p1, bool p2)
    {
        if (p1 && p2)
        {
            currentCombo += 1;
        }
        else
        {
            currentCombo = 0;
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
    }

    public void StageFailScore()
    {
        currentScore += stageFailScore;
    }
}
