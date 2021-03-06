﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private Animator blackOut;                  //画面暗転用のアニメーター
    private Image blackOutImage;                //画面暗転用のImage
    private bool blackOutFlg = false;           //画面暗転用のフラグ

    public float aaa;                           //Imageのアルファ値を入れる変数

    private GameObject pl;                      //プレイヤー
    private PlayerMoveSystem plms;              //プレイヤーの移動を司るスクリプト
    private PlayerHitObject plho;               // PlayerHitObjctスクリプト
    private EndCurveCameraReset eccr;
    private TimerScript ts;                     //Timerスクリプト

    [NonSerialized] public Text GameOverText;   //ゲームオーバーのテキスト
    [NonSerialized] public Text GameClearText;  //ゲームクリアのテキスト

    private Slider CoinBar;                          //コイン獲るたびに増えるバー
    private Text ScoreText;                          //[Score:]を表示するためのテキスト
    private Text Score;                              //点数を表示するテキスト
    private int sco;                                //スコア用の変数
    private Text CoinText;                          //コイン用のテキスト
    private int coinCount = 0;                      //コインの枚数
    [SerializeField] private int coinsScore;        //コイン一枚当たりのスコア
    [SerializeField] private bool isShowUi = false; //UIを見せるかどうかのやつ
    private int coinSave = 0;                       //中間地点の記録用コイン変数
    private int scoreSave = 0;                      //中間地点の記録用スコア変数

 
    //以下ゴリラカメラ用
    Camera camf;
    Camera camr;
    Camera caml;
    Camera camb;

    [SerializeField,Tooltip("ゲームのFPS値を設定するやつね")] 
    private int framerate;

    [Tooltip("現在のFPS値の近似値のやつね")]
    public int currentFramerate;

    private void Start()
    {
        Application.targetFrameRate = framerate;
        GameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        GameOverText.enabled = false;
        
        GameClearText = GameObject.Find("GameClearText").GetComponent<Text>();
        GameClearText.enabled = false;

        blackOut = GameObject.Find("BlackoutImage").GetComponent<Animator>();
        blackOutImage = GameObject.Find("BlackoutImage").GetComponent<Image>();
        pl = GameObject.Find("Wizard");
        plms = pl.GetComponent<PlayerMoveSystem>();
        plho = pl.GetComponent<PlayerHitObject>();
        eccr = GameObject.Find("CameraSet").GetComponent<EndCurveCameraReset>();
        ts = GameObject.Find("TimeBox").GetComponent<TimerScript>();

        //以下ステージUI
        CoinBar = GameObject.Find("CoinChainGage").GetComponent<Slider>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        Score = GameObject.Find("Score").GetComponent<Text>();
        CoinText = GameObject.Find("CoinText").GetComponent<Text>();

        //ステージ中のUIを標準非表示に
        CoinBar.gameObject.SetActive(false);
        ScoreText.enabled = false;
        Score.enabled = false;
        CoinText.enabled = false;
        
        if (isShowUi)
        {
            CoinBar.gameObject.SetActive(true);
            ScoreText.enabled = true;
            Score.enabled = true;
            CoinText.enabled = true;
        }

        ////以下カメラ用
        camf = GameObject.Find("FollowCamera").GetComponent<Camera>();
        camr = GameObject.Find("FollowCameraR").GetComponent<Camera>();
        caml = GameObject.Find("FollowCameraL").GetComponent<Camera>();
        camb = GameObject.Find("FollowCameraB").GetComponent<Camera>();
    }

    private void Update()
    {
        //ポーズする

        if (GameObject.Find("TutoSys"))
        {
            if (Input.GetButtonDown("Pause") && GameObject.Find("TutoSys").GetComponent<PlayTutorial>().tutoNum == 99)
                Time.timeScale = 1 - Time.timeScale;
        }
        else
        {
            if (Input.GetButtonDown("Pause"))
                Time.timeScale = 1 - Time.timeScale;
        }

    }

    private void FixedUpdate()
    {

        currentFramerate = (int)(1f / Time.deltaTime);      //現在のフレームレート近似値

        aaa = blackOutImage.color.a;                        //暗転用画像のアルファ値
        
        blackOut.SetFloat("BlackOutAlphaValue", aaa);       //暗転アニメーション開始

        if (aaa == 1f && blackOutFlg == false)
        {
            Retry();
            blackOut.SetTrigger("EndBlackOut");
            CoinLoad();
            blackOutFlg = true;
        }

        if (aaa == 0 && blackOutFlg == true)
        {
            blackOut.SetTrigger("FinishBlackOut");
            blackOutFlg = false;
            Debug.Log("ｕｎｎｋｏ");
            aaa = 0f;
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //    SceneManager.LoadScene("MainScene");

        if (plms.curveFlg == false)
        {
            switch (plms.autoRunVec)
            {

                case "front":
                    CameraOnOff(1, 0, 0, 0);
                    break;
                case "right":
                    CameraOnOff(0, 1, 0, 0);
                    break;
                case "left":
                    CameraOnOff(0, 0, 1, 0);
                    break;
                case "back":
                    CameraOnOff(0, 0, 0, 1);
                    break;
            }
        }


        if (CoinBar.value >= 1.0f)
        {
            CoinBar.value = 0f;
            coinsScore += 100;
        }

        if (GameClearText.enabled == true)
        {
            //tr.SetValue(coinCount, 0, sco);
            //SceneManager.LoadScene("ResultScene");
        }

        eccr.CameraPosReset();
    }

    //ゲームオーバーした時の判定
    public void GameOver() {
        

        GameOverText.enabled = true;

        if(aaa == 0f)
            blackOut.SetTrigger("StartBlackOut");

    }

    //ゲームオーバー時にリトライ機能を追加
    public void Retry() {

        plms.RunSpeed = plms.playerOriginSpeed;
        plms.runSpd = plms.lastRunSpd;
        plms.RetrySpeedReset();
        GameOverText.enabled = false;
        pl.transform.position = plms.lastCheckPoint;
        plms.autoRunVec = plms.lastVec;
        plho.GetPriority();
        GameObject.Find("LanePanel").transform.position = plms.lastCheckPoint;
    }
    
    //コイン
    public void GetCoin() {
        sco += coinsScore;
        coinCount += 1;
        //Score.text = sco.ToString("0000000") + " 点\n" + coinCount.ToString("000") + " 枚";
        Score.text = sco.ToString("000000");
        CoinText.text = "× " + coinCount.ToString("000")+" ";
        CoinBar.value += 0.1f;
    }

    //カメラ4つのオンオフ切り替え
    void CameraOnOff(int f,int r,int l,int b) {

        if (f == 1) { camf.enabled = true; }
        else { camf.enabled = false; }

        if (r == 1) { camr.enabled = true; }
        else { camr.enabled = false; }

        if (l == 1) { caml.enabled = true; }
        else { caml.enabled = false; }

        if (b == 1) { camb.enabled = true; }
        else { camb.enabled = false; }

    }

    //ゲームデータをセーブ
    public void GameDataSave() {
        PlayerPrefs.SetInt("Coin", coinCount);
        PlayerPrefs.SetInt("Score", sco);
        PlayerPrefs.SetInt("StageNum", /*StageNum*/1);
        //PlayerPrefs.SetFloat("clearTime;", ts.CountTime);
        PlayerPrefs.Save();
    }
    //中間地点侵入時、コインの状態を記録
    public void CoinSave()
    {
        coinSave = coinCount;
        scoreSave = sco;
    }
    //ゲームオーバー時、コインの状態を上書きする
    public void CoinLoad()
    {
        //変数を上書き
        coinCount = coinSave;
        sco = scoreSave;
        //テキストを一瞬読んで上書き
        //Score.text = sco.ToString("0000000") + " 点\n" + coinCount.ToString("000") + " 枚";
        Score.text = sco.ToString("000000");
        CoinText.text = "× " + coinCount.ToString("000") + " 枚";
    }

}