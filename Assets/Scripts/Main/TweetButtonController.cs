﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GameStatus;
/// <summary>
/// ツイッター共有の制御
/// </summary>
public class TweetButtonController : MonoBehaviour
{ 
    [SerializeField]
    GameStatus gameStatus;

    [SerializeField]
    Text inputName;

    [SerializeField]
    PlayerLifeManagement playerLifeManagement;

    [SerializeField]
    GameObject gameManagement;

   /// <summary>
   /// 「つぶやく」ボタンを押したときの処理
   /// </summary>
    public void OnClickTweetButton()
    {
        string nameValue = inputName.text;

        string scoreText = playerLifeManagement.Score.ToString();

        string text;

        Ranking ranking;

        if (nameValue == "") nameValue = "匿名";

        if (gameStatus.rankings.Count != 0)
        {
            ranking = gameStatus.rankings[0];
            if (playerLifeManagement.Score >= ranking.Score)
            {
                text = $"{nameValue}さんの今回の記録は『{scoreText}』点でした! \n" +
            $"ハイスコアーは、{nameValue}さんの『{scoreText}』点です \n" +
            $"挑戦者求む!!\n"
        + "https://www.google.com/‎\n";
            }
            else
            {
                text = $"{nameValue}さんの今回の記録は『{scoreText}』点でした! \n" +
            $"ハイスコアーは、{ranking.Name}さんの『{ranking.Score}』点です \n" +
            $"挑戦者求む!!\n"
        + "https://www.google.com/‎\n";
            }
        }
        else
        {
            text = $"{nameValue}さんの今回の記録は『{scoreText}』点でした! \n" +
            $"ハイスコアーは、{nameValue}さんの『{scoreText}』点です \n" +
            $"挑戦者求む!!\n"
        + "https://www.google.com/‎\n";
        }

        string[] hashtags = { "2ゲーム", "Unity" };

        Application.OpenURL($"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(text)}&hashtags={UnityWebRequest.EscapeURL(string.Join(",", hashtags))}");
    }
}