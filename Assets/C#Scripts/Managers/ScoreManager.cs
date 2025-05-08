using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingleTons<ScoreManager>
{
    public Text ScoreText;
    private int LastScore;
    private int[] ScoreCount = new int[8];
    public void StartGetScore()
    {
        GameManager.Instance.PlayerData.ScoreCount = 0;
        InvokeRepeating("SecondAddScore",0,1);
    }
    public void GetScore()
    {
        InvokeRepeating("SecondAddScore", 0, 1);
    }
    public void EndGetScore()
    {
        CancelInvoke();
    }
    public void AddScore(int score)
    {
        GameManager.Instance.PlayerData.ScoreCount += score;
    }
    private void SecondAddScore()
    {
        LastScore = GameManager.Instance.PlayerData.ScoreCount;
        GameManager.Instance.PlayerData.ScoreCount += 1;
    }
    private void Update()
    {
        if(ScoreText.color != ColorManager.Instance.UpdateColor(1))
        {
            ScoreText.color = ColorManager.Instance.UpdateColor(1);
        }
        if(LastScore != GameManager.Instance.PlayerData.ScoreCount)
        {
            RefreshScore();
        }
    }
    private void RefreshScore()
    {
        ScoreText.text = "";
        var Temp = 10000000;
        var TempScore = GameManager.Instance.PlayerData.ScoreCount;
        for(int i = 0; i < 8; i++)
        {
            var newCount = TempScore / Temp;
            ScoreCount[i] = newCount;
            TempScore -= newCount * Temp;
            Temp /= 10;
        }
        for(int i = 0; i < 8; i++)
        {
            ScoreText.text += ScoreCount[i].ToString() + " ";
        }
    }
}
