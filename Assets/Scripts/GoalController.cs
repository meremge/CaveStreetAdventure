﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.SocialPlatforms.Impl;
//using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    #region//インスペクターで設定する

    [SerializeField]
    GameManagement GameManagement;

    #endregion

    //#region//プライベート変数  bool

    //bool gameClear = false; //ゲームクリアーしたら操作を無効にする

    //#endregion


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            {
                GameManagement.GameClear();
            }
        }
    }
}
