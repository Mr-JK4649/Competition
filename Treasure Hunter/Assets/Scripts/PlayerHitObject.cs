using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerHitObject : MonoBehaviour
{
    private PlayerMoveSystem plms;                  //プレイヤーの移動を司るスクリプト
    private GameManager gm;                         //ゲームの進行などを司るスクリプト

    private bool deathFlg = false;                  //プレイヤーが障害物に当たったかの判定

    [SerializeField] bool blockHitFlg = false;      //障害物に当たるかのフラグ
    
    [SerializeField] GameObject bom;                //爆発

    public Slider CoinBar;                          //コイン獲るたびに増えるバー
    public Text ScoreText;                          //[Score:]を表示するためのテキスト
    public Text Score;                              //点数を表示するテキスト
    private int sco;                                //スコア用の変数
    private int coinCount = 0;                      //コインの枚数
    [SerializeField] private int coinsScore;        //コイン一枚当たりのスコア
    [SerializeField] private bool isShowUi = false; //UIを見せるかどうかのやつ

    private void Start()
    {
        plms = this.GetComponent<PlayerMoveSystem>();
        gm = GameObject.Find("GameSystem").GetComponent<GameManager>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        //ステージ中のUIを標準非表示に
        CoinBar.gameObject.SetActive(false);
        ScoreText.enabled = false;
        Score.enabled = false;
        if (isShowUi) {
            CoinBar.gameObject.SetActive(true);
            ScoreText.enabled = true;
            Score.enabled = true;
        }
    }

    private void Update()
    {
        //障害物に当たった時の処理
        if (deathFlg)
        {
            if(blockHitFlg)
                plms.Wiz_RB.velocity = new Vector3(0f, -9.81f, 0f);

            if (Input.GetKeyDown(KeyCode.R)) {
                gm.Retry();
            }
        }
    }

    private void FixedUpdate()
    {
        if (CoinBar.value >= 1.0f)
        {
            CoinBar.value = 0f;
            coinsScore += 100;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (blockHitFlg)
        {
            Instantiate(bom, this.gameObject.transform);
            plms.RunSpeed = -0.5f;
            deathFlg = true;
            gm.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        switch (other.gameObject.tag)
        {
            case "Coin":
                other.gameObject.SetActive(false);
                Destroy(other.gameObject);
                sco += coinsScore;
                coinCount += 1;
                Score.text = sco.ToString("0000000") + " 点\n" + coinCount.ToString("000") + " 枚";
                CoinBar.value += 0.1f;
                break;
        }
    }
}
