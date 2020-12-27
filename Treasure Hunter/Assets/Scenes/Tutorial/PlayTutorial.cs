using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayTutorial : MonoBehaviour
{

    //[SerializeField] private Image img;
    [SerializeField] private Image[] config;            //十字ボタンの画像
    [SerializeField] private Image[] button;            //Ａぼったん
    [SerializeField] private bool timeStop = false;     //時間が止まってるか
    public bool skipTuto = false;                       //チュートリアルをスキップしたか
    [SerializeField] private Animator hold;             //ホールド指示のアニメーション
    [SerializeField] private Text[] tutoText;           //モードを表す

    [SerializeField] private GameObject veriWin;        //チュートリアルスキップ確認画面
    [SerializeField] private GameObject tuto;           //チュートリアルに必要なやつども

    public int tutoNum = 99;            //チュートリアルの番号


    public float kak;                           //アニメーションにつかう
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
            tutoNum = 99;
            ChangeImageTime(99);
        }

        if (skipTuto) {
            tutoText[0].text = "チュートリアル解除：";
            tutoText[1].text = "　自由操作可能";
            tutoText[1].color = Color.green;
        }

    }




    //==================================== 以下　関数 =======================================

    //画像の切り替え
    void ChangeImageTime(int num) {
        if (tutoNum != 4 && tutoNum != 6)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == num) config[i].enabled = true;
                else config[i].enabled = false;
                button[0].enabled = false;
                button[1].enabled = false;
                button[2].enabled = false;
            }
        }
        else{
            for (int i = 0; i < 2; i++)
            {
                if (i == (int)kak) button[i].enabled = true;
                else button[i].enabled = false;
            }
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
                hold.SetBool("AnimStart", true);
                if(button[2].enabled == false)button[2].enabled = true;
                if (Input.GetButtonDown("Cont_A")) passTest = true;
                break;
        }

        

        if (passTest) {
            Time.timeScale = 1;
            hold.SetBool("AnimStart", false);
            button[2].enabled = false;
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
