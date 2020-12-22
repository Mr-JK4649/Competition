using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Resultscripts : MonoBehaviour
{
    readonly int[] coinMax = { 500, 800, 1300 };
    readonly float[] clearTimeLine = { 300f, 400f, 500f };

    //ステージクリア時に渡されるやつ
    public float cointCount;
    public float clearTime;
    public float score;
    public int stageNum = 0;
    
    public float speed;

    private Text ScoreText;
    private Color ScoreColor;
    public Image Rank_S;
    public Image Rank_A;
    public Image Rank_B;
    public Image Rank_C;
    private Color Rank_Color;//色変更させるために使いまわす
    [SerializeField] private Slider coinCountBar;
    [SerializeField] private Slider clearTimeBar;
    [SerializeField] private Slider stageClearPer;


    private void Start()
    {
        cointCount = (float)PlayerPrefs.GetInt("Coin", 0);
        score = (float)PlayerPrefs.GetInt("Score", 0);
        stageNum = PlayerPrefs.GetInt("StageNum", 0);
        clearTime = PlayerPrefs.GetInt("clearTime", 0);

        StartCoroutine("CoinGageIncrease");
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        //Rank_S = GameObject.Find("StageEvaluation_S(Image)").GetComponent<Image>();
        //Rank_A = GameObject.Find("StageEvaluation_A(Image)").GetComponent<Image>();
        //Rank_B = GameObject.Find("StageEvaluation_B(Image)").GetComponent<Image>();
        //Rank_C = GameObject.Find("StageEvaluation_C(Image)").GetComponent<Image>();

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


    }

    void FixedUpdate()
    {

        //タイトルに戻る処理
        ToTitle();
    }


    void ToTitle() {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cont_A"))
        {
            SceneManager.LoadScene("TitleScene");//mainシーンをロードする
        }
    }

    private IEnumerator CoinGageIncrease() {

        float num = 0;
        float max = cointCount;
        float gageMax = (float)coinMax[stageNum];

        while (num < max) {
            num += speed;

            coinCountBar.value = (float)num / gageMax;

            yield return null;
        }

        coinCountBar.value = max / gageMax;


        StartCoroutine("ClearTimeGageIncrease");
        
    }

    private IEnumerator ClearTimeGageIncrease()
    {

        float num = 0;
        float max = clearTimeLine[stageNum] - clearTime;
        float gageMax = max + 100f;

        while (num < max)
        {
            num += speed;

            clearTimeBar.value = num / gageMax;

            yield return null;
        }

        clearTimeBar.value = max / gageMax;


        StartCoroutine("ClearPerGageIncrease");

    }

    private IEnumerator ClearPerGageIncrease()
    {

        float num = 0;
        float max = (cointCount + clearTime) / 2;
        float gageMax = (coinMax[stageNum] + clearTimeLine[stageNum]) / 2;

        while (num < max) {

            num += speed;

            stageClearPer.value = num / gageMax;

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

        ScoreText.text = score.ToString("00000000");
        num = speed / maxframe;
        while(ScoreColor.a != 1)
        {
            ScoreColor.a += num;
            if (ScoreColor.a >= 1) ScoreColor.a = 1;
            ScoreText.color = ScoreColor;
            yield return null;
        }

        StartCoroutine("ClearStageEvaluation");
    }
    //ステージ評価の処理
    private IEnumerator ClearStageEvaluation()
    {
        float num;
        const float maxframe = 500;
        Color ColorKeep;


        num = speed / maxframe;

        if(stageClearPer.value >= 0.75) ColorKeep = Rank_S.color;
        else if(stageClearPer.value >= 0.5) ColorKeep = Rank_A.color;
        else if (stageClearPer.value >= 0.25)ColorKeep = Rank_B.color;
        else ColorKeep = Rank_C.color;
        

        while ( ColorKeep.a != 1)
        {
            ColorKeep.a += num;

            if (ColorKeep.a >= 1) ColorKeep.a = 1;
            if      (stageClearPer.value >= 0.75)  Rank_S.color = ColorKeep;
            else if (stageClearPer.value >= 0.50)  Rank_A.color = ColorKeep;
            else if (stageClearPer.value >= 0.25)  Rank_B.color = ColorKeep;
            else    Rank_C.color = ColorKeep;

            yield return null;

        }
    }
}
