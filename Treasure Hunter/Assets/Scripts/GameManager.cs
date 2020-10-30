using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Animator blackOut;
    private Image blackOutImage;
    private bool blackOutFlg = false;

    public float aaa;

    private GameObject pl;
    private PlayerMoveSystem plms;

    [NonSerialized] public Text GameOverText;
    [NonSerialized] public Text GameClearText;

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

    //ゲームオーバーした時の判定
    public void GameOver() {
        //GameObject gameoverText = new GameObject("GameOverText");
        //gameoverText.transform.parent = GameObject.Find("InStageUI").transform;
        //gameoverText.transform.localPosition = Vector3.zero;
        //gameoverText.AddComponent<Text>();
        //Text gmt = gameoverText.GetComponent<Text>();
        //gmt.horizontalOverflow = HorizontalWrapMode.Overflow;
        //gmt.verticalOverflow = VerticalWrapMode.Overflow;
        //gmt.text = "GameOver\n[r] key to Retry";
        //gmt.fontSize = 40;
        //gmt.alignment = TextAnchor.MiddleCenter;
        //gmt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        //gmt.color = new Color(255f, 0f, 0f);

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

    
 
}
