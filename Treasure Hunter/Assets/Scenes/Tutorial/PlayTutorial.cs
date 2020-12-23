using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayTutorial : MonoBehaviour
{

    //[SerializeField] private Image img;
    [SerializeField] private Image[] config;            //十字ボタンの画像
    [SerializeField] private bool timeStop = false;     //時間が止まってるか
    public bool skipTuto = false;                       //チュートリアルをスキップしたか

    [SerializeField] private GameObject veriWin;        //チュートリアルスキップ確認画面
    [SerializeField] private GameObject tuto;           //チュートリアルに必要なやつども

    [NonSerialized] public int tutoNum = 99;            //チュートリアルの番号


    [NonSerialized] public float kak;           //アニメーションにつかう
    private float timeD;                        //deltaTimeの値を保存する

    private void Start()
    {
        //Time.timeScale = 0;
        veriWin.SetActive(false);
    }

    private void Update()
    {
        //Ｋボタンを押したらスキップ確認
        if (Input.GetKeyDown(KeyCode.K) && !skipTuto)
        {
            veriWin.SetActive(true);
            Time.timeScale = 0;
        }

        //チュートリアルスキップを押してなければ
        if (!skipTuto && tutoNum != 99)
        {
            kak += timeD * 2f;
            kak %= 2;
            int kaku = (int)kak;
            ChangeImageTime(kaku * (tutoNum + 1));
            TutoSys();
        }
        else
        {
            kak = 0;
            timeD = Time.deltaTime;
            ChangeImageTime(99);
        }
    }




    //==================================== 以下　関数 =======================================

    //画像の切り替え
    void ChangeImageTime(int num) {
        for (int i = 0; i < 5; i++) {
            if (i == num) config[i].enabled = true;
            else config[i].enabled = false;
        }
    }

    //チュートリアル進行
    void TutoSys() {
        

        bool passTest = false;

        switch (tutoNum) {
            case 0:         //上に避ける
                if (Input.GetAxis("Vertical") > 0) passTest = true;
                break;
            case 1:         //左に避ける
                if (Input.GetAxis("Horizontal") < 0) passTest = true;
                break;
            case 2:         //右に避ける
                if (Input.GetAxis("Horizontal") > 0) passTest = true;
                break;
            case 3:         //下に避ける
                if (Input.GetAxis("Vertical") < 0) passTest = true;
                break;
            case 4:         //加速
                if(Input.GetButtonDown("Cont_A")) passTest = true;
                break;
            case 5:         //コイン
                passTest = true;
                break;
            case 6:         //コイン加速
                if (Input.GetButtonDown("Cont_A")) passTest = true;
                break;
        }

        

        if (passTest) {
            Time.timeScale = 1;
        }

    }






    //スキップＹＥＳ
    public void SkipTutoFunc(){
        skipTuto = true;
        tuto.SetActive(false);
        DeleteVeriWindow();
    }

    //スキップ確認ウィンドウを消し、時間を進める
    public void DeleteVeriWindow() {
        veriWin.SetActive(false);
        Time.timeScale = 1;
    }
}
