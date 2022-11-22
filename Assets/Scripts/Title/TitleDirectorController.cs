﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameStatus;

/// <summary>
/// タイトル画面の操作
/// </summary>
public class TitleDirectorController : MonoBehaviour
{
    [SerializeField]
    GameStatus gameStatus;

    [SerializeField]
    Text resultGamehighScoreText;


    /// <summary>
    ///　スタートボタンを押したら実行する
    /// </summary>
    public void GameStart() => SceneManager.LoadScene("GameMain");

    /// <summary>
    ///　解説MENUに進む
    /// </summary>
    public void DescriptionStart() => SceneManager.LoadScene("DescriptionScene");

    /// <summary>
    ///　RANKING Buttonを押したら実行する
    /// </summary>
    public void RankingStart() => UnityEngine.SceneManagement.SceneManager.LoadScene("Ranking");

    /// <summary>
    ///  ゲーム終了ボタンを押したら実行する
    /// </summary>
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://www.yahoo.co.jp/");
#else
        Application.Quit();
#endif
    }

    void Start() 
    {
        Screen.SetResolution(400, 710, false, 60);
        gameStatus.Load();

        //gameStatus.rankings.Sort((a, b) => b.Score - a.Score);//ダミー用
        Ranking ranking;

        if (gameStatus.rankings.Count != 0)
        {

            ranking = gameStatus.rankings[0];

            resultGamehighScoreText.text = "HighScore: " + ranking.Score.ToString();
        }
        else
        {
            resultGamehighScoreText.text = "HighScore : none memory";
        }
    }

}
