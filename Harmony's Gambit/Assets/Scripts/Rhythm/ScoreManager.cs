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

    //��Ʈ ����
    private int[] noteScore = new int[] { 2, 1, 0, -1, -2, -3 }; // ���ʴ�� Ÿ�ӿ��� ���� ��Ʈ 2�� 1�� 0��, Ÿ�� ���� ���� ��Ʈ 2�� 1�� 0�� �Է� �� ��� ����

    //�� óġ�� ����
    public int mouseScore = 10;
    public int hienaScore = 30;

    //�޺� ����
    private int currentCombo = 0; //���� �޺�
    private int comboUnit = 10; //������ �� �޺��� ����
    private int maxCombo = 100; //�޺� ���� ������ ������ ��, �� ���ڸ�ŭ�� �޺� �ñ��� ������ ������. �������ʹ� ������ ����
    private int maxComboScore = 10; //�޺��� �ִ� �� ���� ������ �ִ�ġ

    //��Ÿ ����
    private int stageClearScore = 10; //�������� Ŭ���� ���� ����
    private int stageFailScore = 0; //�������� ���� ����
    private int timeOverScore = -10; //Ÿ�� ���� ���� �� ����
    //Ŭ���� �� ������ ��Ʈ�� ���� 2���� ���� �߰� �ʿ�

    //���� ���
    private int twoNote = 0; //�� ��Ʈ �Է� ��
    private int oneNote = 0; //�� ��Ʈ �Է� ��
    private int zeroNote = 0; //��ģ ��Ʈ ��
    private int maximumCombo = 0; //���� ��� �̾��� �޺�
    private int killedMob = 0; //�� óġ ��

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
            GameObject.Find("ScoreBoardCanvas").transform.GetChild(0).gameObject.SetActive(true); //���ھ� ���� �ѱ�
        }
    }

    public void StageFailScore()
    {
        currentScore += stageFailScore;
    }
}
