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

    //콤보 ui
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

        comboEffect = GameObject.Find("combo_effect");
        combo100Effect = GameObject.Find("100combo_effect_0");
        comboEffect.SetActive(false);
        combo100Effect.SetActive(false);
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
            if (_playerManager.GameClear)
            {
                totalScore = currentScore;
            }
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
        totalScore = 0;
        isTimeOver = false;
        beatListCount = NoteManager.instance.currentBeatList.Count;
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
            currentScore += currentCombo / comboUnit;

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
        currentScore += stageClearScore * (_gameManager.redPlayer.HP + _gameManager.bluePlayer.HP);
        currentScore += (beatListCount - twoNote - oneNote - zeroNote) * 2;

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
