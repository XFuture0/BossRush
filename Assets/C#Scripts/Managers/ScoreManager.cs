using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : SingleTons<ScoreManager>
{
    public Text ScoreText;
    public GameObject GetScoreImage;
    public Transform GetScoreBox;
    private int LastScore;
    private int[] ScoreCount = new int[8];
    public void StartGetScore()
    {
        GameManager.Instance.PlayerData.ScoreCount = 0;
        RefreshScore();
    }
    public void EndGetScore()
    {
        CancelInvoke();
    }
    public void AddScore(int score,AttackType type)
    {
        LastScore = GameManager.Instance.PlayerData.ScoreCount;
        var NewGetScore = Instantiate(GetScoreImage, GetScoreBox);
        NewGetScore.GetComponent<RectTransform>().anchoredPosition = new Vector2(-173f, 100f);
        AddScoreText(type,NewGetScore.gameObject.GetComponent<Text>(),score);
        StartCoroutine(OnAddScore(score));
    }
    private IEnumerator OnAddScore(int Score)
    {
        for(int i = 0;i < Score; i++)
        {
            GameManager.Instance.PlayerData.ScoreCount += 1;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void AddScoreText(AttackType type,Text NewGetScoreText,int Score)
    {
        switch (type)
        {
            case AttackType.HitBoss:
                NewGetScoreText.text = "Уќжа + " + Score.ToString();
                break;
            case AttackType.HitBoss_Critical:
                NewGetScoreText.text = "БЉЛї + " + Score.ToString();
                break;
        }
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
