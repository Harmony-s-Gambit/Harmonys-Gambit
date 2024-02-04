using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTest : MonoBehaviour
{
    Text currentScoreText, plusScoreText, totalScoreText;
    private int previousScore;

    void Start()
    {
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<Text>();
        plusScoreText = GameObject.Find("PlusScoreText").GetComponent<Text>();
        totalScoreText = GameObject.Find("TotalScoreText").GetComponent<Text>();
    }

    void Update()
    {
        try
        {
            if (ScoreManager.instance.rhythm)
            {
                plusScoreText.text = "+" + (ScoreManager.instance.currentScore - previousScore).ToString();
                currentScoreText.text = ScoreManager.instance.currentScore.ToString();
                previousScore = ScoreManager.instance.currentScore;
                totalScoreText.text = ScoreManager.instance.totalScore.ToString();
            }
        }
        catch (System.Exception)
        {

        }
    }
}
