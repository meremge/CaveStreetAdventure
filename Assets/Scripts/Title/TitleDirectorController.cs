﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDirectorController : MonoBehaviour
{
    [SerializeField]
    GameStatus gameStatus;

    [SerializeField]
    Text resultGamehighScoreText;
 

    //　スタートボタンを押したら実行する
    public void GameStart()
    {
        print("a");
        SceneManager.LoadScene("GameMain");
    }

    //　RANKING Buttonを押したら実行する
    public void RankingStart()
    {
        print("b");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ranking");
    }

    //　ゲーム終了ボタンを押したら実行する
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

    // Use this for initialization
    void Start() 
    {
        Screen.SetResolution(400, 710, false, 60);
        gameStatus.Load();
        resultGamehighScoreText.text = "High Score: " + PlayerPrefs.GetInt(gameStatus.key).ToString();
    }

}
