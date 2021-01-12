using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class Resultscripts : MonoBehaviour
{
    readonly int[] coinMax = { 500, 800, 1300 };
    readonly float[] clearTimeLine = { 300f, 400f, 500f };

    //ステージクリア時に渡されるやつ
    public float cointCount;
    public float clearTime;
    public float coinScore;
    public int stageNum = 0;
    public Text[] Pers;             //パーセンテージ
    public Text[] PerNums;          //ゲージ内部の数値
    public Text addScorePer;        //スコアのボーナス加算
    public Animator movePerNum;     //ボーナスのためだけにつくったアニメーション
    public Animator movePerNum2;     //ボーナスのためだけにつくったアニメーション2
    
    public float speed;

    private bool buttonPushFlg;//ボタン押すのを許可する奴～～
    private Text ScoreText;
    private Color ScoreColor;
    private Image Rank_S;
    private Image Rank_A;
    private Image Rank_B;
    private Image Rank_C;
    private Color Rank_Color;//色変更させるために使いまわす
    [SerializeField] private Slider coinCountBar;
    [SerializeField] private Slider clearTimeBar;
    [SerializeField] private Slider stageClearPer;


    private void Start()
    {
        cointCount = (float)PlayerPrefs.GetInt("Coin", 0);
        coinScore = (float)PlayerPrefs.GetInt("Score", 0);
        stageNum = PlayerPrefs.GetInt("StageNum", 0);
        clearTime = PlayerPrefs.GetInt("clearTime", 0);

        buttonPushFlg = false;

        StartCoroutine("CoinGageIncrease");
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        Rank_S = GameObject.Find("StageEvaluation_S(Image)").GetComponent<Image>();
        Rank_A = GameObject.Find("StageEvaluation_A(Image)").GetComponent<Image>();
        Rank_B = GameObject.Find("StageEvaluation_B(Image)").GetComponent<Image>();
        Rank_C = GameObject.Find("StageEvaluation_C(Image)").GetComponent<Image>();

        ScoreColor = ScoreText.color;
        ScoreColor.a = 0;
        ScoreText.color = ScoreColor;

        //透明化
        Rank_Color = Rank_S.color;
        Rank_Color.a = 0;
        Rank_S.color = Rank_Color;

        Rank_Color = Rank_A.color;
        Rank_Color.a = 0;
        Rank_A.color = Rank_Color;

        Rank_Color = Rank_B.color;
        Rank_Color.a = 0;
        Rank_B.color = Rank_Color;

        Rank_Color = Rank_C.color;
        Rank_Color.a = 0;
        Rank_C.color = Rank_Color;

        //ゲージの中の数値
        PerNums[0].text = 0.ToString("000") + "/" + coinMax[stageNum].ToString("000") + " coins";
        PerNums[1].text = 0.ToString("000") + "/" + clearTimeLine[stageNum].ToString("000") + "seconds";

        //ボーナス加算テキスト
        addScorePer.enabled = false;
    }

    void FixedUpdate()
    {

        //タイトルに戻る処理
        if(buttonPushFlg == true)
        ToTitle();
    }


    void ToTitle() {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cont_A"))
        {
            SceneManager.LoadScene("SelectScene");//mainシーンをロードする
        }
    }

    private IEnumerator CoinGageIncrease() {

        float num = 0;
        float max = cointCount;
        float gageMax = (float)coinMax[stageNum];


        if(gageMax < cointCount)
        {
            gageMax = cointCount;
        }
        while (num < max) {
            num += speed;

            coinCountBar.value = (float)num / gageMax;
            Pers[0].text = (coinCountBar.value*100).ToString("00") + "%";
            PerNums[0].text = num.ToString("000") + "/" + coinMax[stageNum].ToString("000") + " coins";
            SEManager.Instance.Play(SEPath.BEEP1);

            yield return null;
        }

        coinCountBar.value = max / gageMax;


        StartCoroutine("ClearTimeGageIncrease");
        
    }

    private IEnumerator ClearTimeGageIncrease()
    {

        float num = 0;
        float max = clearTimeLine[stageNum] - clearTime;
        float gageMax = clearTimeLine[stageNum];

        if (max >= 0)
        {
            max = 300;
        }
        if (max < 0)
        {
            max = 300 - (max * -1 / (clearTimeLine[stageNum]/100));
        }
        while (num < max)
        {
            num += speed;

            clearTimeBar.value = num / gageMax;
            Pers[1].text = (clearTimeBar.value * 100).ToString("00") + "%";
            PerNums[1].text = num.ToString("000") + "/" + clearTimeLine[stageNum].ToString("000") + "seconds";
            SEManager.Instance.Play(SEPath.BEEP1);

            yield return null;
        }

        clearTimeBar.value = max / gageMax;


        StartCoroutine("ClearPerGageIncrease");

    }

    private IEnumerator ClearPerGageIncrease()
    {

        float num = 0;
        float max = (coinCountBar.value + clearTimeBar.value) * 100 / 2;
        float gageMax = 100;

        //if (clearTimeLine[stageNum] - clearTime >= 0) max = (cointCount + clearTimeLine[stageNum]) / 2;
        //if (clearTimeLine[stageNum] - clearTime < 0)
        //{
        //    max = (cointCount + 300-((clearTimeLine[stageNum] - clearTime) * -1 / (clearTimeLine[stageNum] / 100)))/2;
        //}
        while (num < max) {

            num += speed*0.2f;

            stageClearPer.value = num / gageMax;
            Pers[2].text = (stageClearPer.value * 100).ToString("00") + "%";
            addScorePer.text = "+ " + (stageClearPer.value * 100).ToString("00") + "%";
            SEManager.Instance.Play(SEPath.BEEP1);

            yield return null;
        }

        stageClearPer.value = max / gageMax;

        StartCoroutine("ClearScoreIncrease");
    }

    //スコアの処理
    private IEnumerator ClearScoreIncrease()
    {
        float num;
        const float maxframe = 100;
        float totalscore;

        totalscore = coinScore;

        ScoreText.text = totalscore.ToString("00000000");
        num = speed / maxframe;
        while(ScoreColor.a != 1)
        {
            ScoreColor.a += num;
            if (ScoreColor.a >= 1) ScoreColor.a = 1;
            ScoreText.color = ScoreColor;
            yield return null;
        }

        //StartCoroutine("ClearStageEvaluation");
        movePerNum.SetTrigger("AnimStart");
    }
    //ステージ評価の処理
    private IEnumerator ClearStageEvaluation()
    {
        float num;
        const float maxframe = 500;
        Color ColorKeep;


        num = speed / maxframe;

        if(stageClearPer.value >= 0.9) ColorKeep = Rank_S.color;
        else if(stageClearPer.value >= 0.7) ColorKeep = Rank_A.color;
        else if (stageClearPer.value >= 0.5)ColorKeep = Rank_B.color;
        else ColorKeep = Rank_C.color;
        

        while ( ColorKeep.a != 1)
        {
            ColorKeep.a += num;

            if (ColorKeep.a >= 1) ColorKeep.a = 1;
            if      (stageClearPer.value >= 0.9)  Rank_S.color = ColorKeep;
            else if (stageClearPer.value >= 0.7)  Rank_A.color = ColorKeep;
            else if (stageClearPer.value >= 0.5)  Rank_B.color = ColorKeep;
            else    Rank_C.color = ColorKeep;

            
            yield return null;


        }
        buttonPushFlg = true;
    }

    //スコア計算
    float ScoreCalculation()
    {
        /***計算方法***/
        //10万点を最高点数と置いて、時間以内にゴールできない場合、60ｆ/1000点　で点数を減らす

        float timefream = clearTimeLine[stageNum] - clearTime;
        float timescore = 0;
        const float MAXSCORE = 100000.0F;

        if (timefream >= 0) timescore = MAXSCORE;
        if (timefream <  0)
        {
            timescore = MAXSCORE + timefream / 10 * 1000;
            if (timescore <= 0) timescore = 0;
        }
        return timescore + coinScore ;

        
    }

    public void EndAnimFunc() {
        addScorePer.enabled = true;
        ScoreText.enabled = false;
        movePerNum2.SetTrigger("AnimStart2");
        //StartCoroutine("ClearStageEvaluation");
    }
}
