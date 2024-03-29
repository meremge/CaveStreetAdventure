﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static GameStatus;
//using UnityEngine.UIElements;
//using static UnityEditor.Timeline.TimelinePlaybackControls;


/// <summary>
/// ゲーム全体を制御する
/// </summary>
public class GameManagement : MonoBehaviour
{
    #region//インスペクターで設定する

    [SerializeField]
    GameObject resultRSButton;

    [SerializeField]
    GameObject scoreUI;

    [SerializeField]
    GameObject buttonController;

    [SerializeField]
    GameObject[] enemys;

    [SerializeField]
    GameObject[] warps;

    [SerializeField]
    GameObject[] downLifts;

    [SerializeField]
    GameObject winUI;

    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    Text resultGameScoreText;

    [SerializeField]
    Text resultGamehighScoreText;

    [SerializeField]
    GameObject resultScoreText;

    [SerializeField]
    GameStatus gameStatus;

    [SerializeField]
    PlayerLifeManagement playerLifeManagement;

    [SerializeField]
    Text resultTimer;

    [SerializeField]
    TimerController timerController;

    [SerializeField] GameObject pausePanel;

    [SerializeField] GameObject titleBackButton;

    [SerializeField] Button pauseButton;

    [SerializeField] Button resumeButton;

    [SerializeField] Text inputName;

    public int InputName
    {
        get => inputNum; 
        set
        {
            if (value > 3)
            {
                inputNum = value;
            }
            
        }
    }

    #endregion

    #region//インスペクターで設定する サウンドSE
    public AudioSource SoundSE;
    public AudioClip WinSE;
    public AudioClip GameOverSE;
    private int inputNum;

    #endregion



    void Start()
    {
        titleBackButton.SetActive(false);
        resultRSButton.SetActive(false);
        resultScoreText.SetActive(false);
        resultTimer.enabled = false;
        gameOverUI.SetActive(false);
        winUI.SetActive(true);
        pausePanel.SetActive(false);// 最初は非表示
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
    }

    /// <summary>
    /// ゲームを一時停止する
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0; // Time.timeScaleで時間の流れの速さを決める。0だと時間が停止する
        pausePanel.SetActive(true);
        titleBackButton.SetActive(true);
    }

    /// <summary>
    /// ゲームを再開する
    /// </summary>
    private void Resume()
    {
        Time.timeScale = 1; // また時間が流れるようにする
        pausePanel.SetActive(false);
        titleBackButton.SetActive(false);
    }

    /// <summary>
    /// タイトルボタン実行する
    /// </summary>
    public void TitleBackBottan()
    {
        print("Title");
        Resume();
        SceneManager.LoadScene("TitleMenuScene");
    }

    //　RANKING Buttonを押したら実行する
    public void RankingStart() => UnityEngine.SceneManagement.SceneManager.LoadScene("Ranking");

    public void RankingNameButton()
    {
        AddRanking();
        gameStatus.Save();
    }

    private void AddRanking()
    {
        Debug.Log("RankB");

        string nameValue = inputName.text;

        //// 入力された文字の文字数をコンソール画面に表示する
        //Debug.Log(nameValue.Length);

        ////８文字まで入力できる。それ以上は入力できない
        //if (nameValue.Length < 9)
        //{
        //    nameValue = inputName.text;
        //    Debug.Log(nameValue);
        //}

        string timer = resultTimer.text;

        Ranking rank = new Ranking(nameValue, playerLifeManagement.Score, timer);
        gameStatus.rankings.Add(rank);
    }

    /// <summary>
    /// ゲームオーバー実行
    /// </summary>
    public void GameOver()
    {
        SoundSE.PlayOneShot(GameOverSE);

        titleBackButton.SetActive(true);

        resultScoreText.SetActive(true);

        winUI.SetActive(false);

        gameOverUI.SetActive(true);

        resultRSButton.SetActive(true);

        resultTimer.enabled = true;

        timerController.enabled = false;

        enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<EnemyRoundTripAct>().enabled = false; // Enemyのコンポーネントを止める。
        }

        enemys = GameObject.FindGameObjectsWithTag("Enemy2");

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<LoopMoveEnemy>().enabled = false; // Enemyのコンポーネントを止める。
        }

        enemys = GameObject.FindGameObjectsWithTag("Enemy3");

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<EnemyDownMove>().enabled = false; // Enemyのコンポーネントを止める。
            Destroy(enemy.GetComponent<Rigidbody2D>());// EnemyのRigidbodyを止める。
        }

        warps = GameObject.FindGameObjectsWithTag("Warp");
        foreach (var warp in warps)
        {
            warp.SetActive(false); // Warpを止める。
        }

        downLifts = GameObject.FindGameObjectsWithTag("Lift");

        foreach (var downLift in downLifts)
        {
            downLift.GetComponent<LiftDownMove>().enabled = false; // Liftのコンポーネントを止める。
            Destroy(downLift.GetComponent<Rigidbody2D>()); // LiftのRigidbodyを止める。
        }

        scoreUI.SetActive(false);

        buttonController.SetActive(false);

        resultGameScoreText.text = "Score: " + playerLifeManagement.Score.ToString();
        resultGamehighScoreText.text = "High Score: " + playerLifeManagement.highScore.ToString();

        gameStatus.highScore = "High Score: " + playerLifeManagement.highScore.ToString();
    }

    /// <summary>
    /// ゲームクリア実行
    /// </summary>
    public void GameClear()
    {
        SoundSE.PlayOneShot(WinSE);

        titleBackButton.SetActive(true);

        resultScoreText.SetActive(true);

        winUI.SetActive(true);

        gameOverUI.SetActive(false);

        resultRSButton.SetActive(true);

        buttonController.SetActive(false);

        scoreUI.SetActive(false);

        resultTimer.enabled = true;

        timerController.enabled = false;

        enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<EnemyRoundTripAct>().enabled = false; // Enemyのコンポーネントを止める。
        }

        enemys = GameObject.FindGameObjectsWithTag("Enemy2");

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<LoopMoveEnemy>().enabled = false; // Enemyのコンポーネントを止める。
        }

        warps = GameObject.FindGameObjectsWithTag("Warp");
        foreach (var warp in warps)
        {
            warp.SetActive(false); // Warpのコンポーネントを止める。
        }

        downLifts = GameObject.FindGameObjectsWithTag("Lift");

        foreach (var downLift in downLifts)
        {
            downLift.GetComponent<LiftDownMove>().enabled = false; // Liftのコンポーネントを止める。
            Destroy(downLift.GetComponent<Rigidbody2D>()); // Liftのコンポーネントを止める。
        }

        resultGameScoreText.text = "Score: " + playerLifeManagement.Score.ToString();
        resultGamehighScoreText.text = "High Score: " + playerLifeManagement.highScore.ToString();


        gameStatus.highScore = "High Score: " + playerLifeManagement.highScore.ToString();
    }
}
