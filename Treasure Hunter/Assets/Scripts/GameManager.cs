using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [NonSerialized]public Text GameOverText;
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
    }

    private void Update()
    {
        currentFramerate = (int)(1f / Time.deltaTime);
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
    }


    //ゲームオーバー時にリトライ機能を追加
    public void Retry() {
        SceneManager.LoadScene("MainScene");
    }

    
 
}
