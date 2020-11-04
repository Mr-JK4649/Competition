using System;
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

    [NonSerialized] public Text GameOverText;   //ゲームオーバーのテキスト
    [NonSerialized] public Text GameClearText;  //ゲームクリアのテキスト

    private Slider CoinBar;                          //コイン獲るたびに増えるバー
    private Text ScoreText;                          //[Score:]を表示するためのテキスト
    private Text Score;                              //点数を表示するテキスト
    private int sco;                                //スコア用の変数
    private int coinCount = 0;                      //コインの枚数
    [SerializeField] private int coinsScore;        //コイン一枚当たりのスコア
    [SerializeField] private bool isShowUi = false; //UIを見せるかどうかのやつ

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

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

        //以下ステージUI
        CoinBar = GameObject.Find("CoinChainGage").GetComponent<Slider>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        Score = GameObject.Find("Score").GetComponent<Text>();

        //ステージ中のUIを標準非表示に
        CoinBar.gameObject.SetActive(false);
        ScoreText.enabled = false;
        Score.enabled = false;
        if (isShowUi)
        {
            CoinBar.gameObject.SetActive(true);
            ScoreText.enabled = true;
            Score.enabled = true;
        }
    }

    private void Update()
    {
        currentFramerate = (int)(1f / Time.deltaTime);

        aaa = blackOutImage.color.a;

        blackOut.SetFloat("BlackOutAlphaValue", aaa);

        if (aaa == 1f)
        {
            Retry();
            blackOut.SetTrigger("EndBlackOut");
            blackOutFlg = true;
        }

        if (aaa == 0f && blackOutFlg)
        {
            blackOut.SetTrigger("FinishBlackOut");
            blackOutFlg = false;
            Debug.Log("ｕｎｎｋｏ");
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //    SceneManager.LoadScene("MainScene");
    }

    private void FixedUpdate()
    {
        if (CoinBar.value >= 1.0f)
        {
            CoinBar.value = 0f;
            coinsScore += 100;
        }
    }

    //ゲームオーバーした時の判定
    public void GameOver() {
        

        GameOverText.enabled = true;

        blackOut.SetTrigger("StartBlackOut");

    }


    //ゲームオーバー時にリトライ機能を追加
    public void Retry() {
        //SceneManager.LoadScene("MainScene");
        plms.RunSpeed = plms.playerOriginSpeed;
        GameOverText.enabled = false;
        pl.transform.position = plms.lastCheckPoint;
    }

    public void GetCoin() {
        sco += coinsScore;
        coinCount += 1;
        Score.text = sco.ToString("0000000") + " 点\n" + coinCount.ToString("000") + " 枚";
        CoinBar.value += 0.1f;
    }

}
