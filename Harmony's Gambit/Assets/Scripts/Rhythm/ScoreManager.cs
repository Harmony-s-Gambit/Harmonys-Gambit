using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public bool rhythm;
    public int currentScore = 0;
    public float currentTime = 0;
    public double time = 0;

    //노트 점수
    private int[] noteScore = new int[] { 2, 1, 0, -1, -2, -3 }; // 차례대로 타임오버 이전 노트 2개 1개 0개, 타임 오버 이후 노트 2개 1개 0개 입력 시 얻는 점수

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (PlayAudio.instance._isMusiceStart)
        {
            currentTime += Time.deltaTime;
        }
        print(currentScore);
    }

    public void GetScore(int score)
    {
        currentScore += score;
    }

    public void ComboScore()
    {

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
}
