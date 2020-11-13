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

    //以下ゴリラカメラ用
    GameObject camf;
    GameObject camr;
    GameObject caml;
    GameObject camb;
    
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


        ////以下カメラ用
        camf = GameObject.Find("FollowCamera");
        camr = GameObject.Find("FollowCameraR");
        caml = GameObject.Find("FollowCameraL");
        camb = GameObject.Find("FollowCameraB");
    }

    private void Update()
    {
        currentFramerate = (int)(1f / Time.deltaTime);      //現在のフレームレート近似値

        aaa = blackOutImage.color.a;                        //暗転用画像のアルファ値

        blackOut.SetFloat("BlackOutAlphaValue", aaa);       //暗転アニメーション開始

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
            aaa = 0f;
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //    SceneManager.LoadScene("MainScene");


        switch (plms.autoRunVec) {

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

        if(aaa == 0f)
            blackOut.SetTrigger("StartBlackOut");

    }


    //ゲームオーバー時にリトライ機能を追加
    public void Retry() {
        //SceneManager.LoadScene("MainScene");
        plms.RunSpeed = plms.playerOriginSpeed;
        GameOverText.enabled = false;
        pl.transform.position = plms.lastCheckPoint;
        plms.autoRunVec = plms.lastVec;
        GameObject.Find("LanePanel").transform.position = plms.lastCheckPoint;
    }

    public void GetCoin() {
        sco += coinsScore;
        coinCount += 1;
        Score.text = sco.ToString("0000000") + " 点\n" + coinCount.ToString("000") + " 枚";
        CoinBar.value += 0.1f;
    }

    //カメラ4つのオンオフ切り替え
    void CameraOnOff(int f,int r,int l,int b) {

        if (f == 1) camf.SetActive(true);
        else camf.SetActive(false);

        if (r == 1) camr.SetActive(true);
        else camr.SetActive(false);

        if (l == 1) caml.SetActive(true);
        else caml.SetActive(false);

        if (b == 1) camb.SetActive(true);
        else camb.SetActive(false);

    }
}
