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

    [SerializeField] private Slider coinCountBar;
    [SerializeField] private Slider clearTimeBar;
    [SerializeField] private Slider stageClearPer;


    private void Start()
    {
        //cointCount = (float)PlayerPrefs.GetInt("Coin", 0);
        //score = (float)PlayerPrefs.GetInt("Score", 0);
        //stageNum = PlayerPrefs.GetInt("StageNum", 0);

        StartCoroutine("CoinGageIncrease");
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
    }
}
