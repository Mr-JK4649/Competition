using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    private Text  TimerText;         //タイマー用のテキスト
    private float CountTime;         //タイマー用の変数
    private int   Min;               //分をカウントする用の変数
    private float Sec;               //秒+ミリ秒をカウントする用の変数
    void Start()
    {
        //タイマーのテキストオブジェクト探す
        TimerText = GameObject.Find("TimerText").GetComponent<Text>();
        //タイマーの値初期化
        CountTime = 0;
        Min = 0;
    }

    void FixedUpdate()
    {
        const int NUM = 60;//秒→分に切り替わる固定値

        //60秒で一周
        if(Sec >= NUM) Min++;
        Sec = CountTime - NUM * Min;

        //表示部分
        //99分59秒以上になったらカウントさせないように処理(タイマー処理の前に書くと問題が発生するのでここで飛ばす）
        if(Min == 99 && Sec >= 59.9)
        {
            TimerText.text = "99:59.99";
            return;
        }
        TimerText.text = Min.ToString("00")+":"+Sec.ToString("00.00");



        //タイマー処理
        CountTime += Time.deltaTime;

    }
}
