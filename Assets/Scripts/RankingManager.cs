﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static GameStatus;
//using static System.Net.Mime.MediaTypeNames;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    Text displayField;

    [SerializeField]
    GameStatus gameStatus;

    void Start()
    {
        displayField.text = "";
        ShowRanking();
    }

    public void OnClickRankingPush() => ShowRanking();

    void ShowRanking()
    {

        gameStatus.rankings.Sort((a, b) => b.Score - a.Score);

        foreach (var ranking in gameStatus.rankings)
        {
            displayField.text += $"{ranking.Name}さん\nScore：{ranking.Score}\t{ranking.Timer}\n\n";
        }
    }

    public void RoadButton() => gameStatus.Load();

    public void SaveButton() => gameStatus.Save();

    public void ClearPanel() => gameStatus.rankings.Clear();

    public void TitleBackButton() => UnityEngine.SceneManagement.SceneManager.LoadScene("TitleMenuScene");
}
