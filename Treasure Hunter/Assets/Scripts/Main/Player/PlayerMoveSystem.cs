using System;
using System.Collections;
using UnityEngine;

public class PlayerMoveSystem : MonoBehaviour
{
    private GameManager gm;

    public float RunSpeed = 0f;                     //プレイヤーの自動飛行の速度
    public Vector3 runSpd;                          //移動速度Vector3版
    [NonSerialized] public GameObject Wiz;          //プレイヤーオブジェクトを入れる
    [NonSerialized] public Transform Wiz_TF;        //プレイヤーのトランスフォーム
    [NonSerialized] public Rigidbody Wiz_RB;        //プレイヤーのRigitbody
    

    public GameObject[] laneObj;                                //レーンオブジェクトの位置
    public Vector3[] lanePos = new Vector3[4];                  //移動先レーンの位置
    public GameObject currentLane;                              //現在のレーン
    public AnimationCurve dodgeSpeed;                           //うずの移動速度
    public float dodgeTime;                                     //ドッジ回避に掛かる時間
    [NonSerialized] public Vector3 Origin_Pos;                  //プレイヤーの初期値
    [NonSerialized] public float playerOriginSpeed;             //プレイヤーの元の速度保存用
    public float accelForce;                                    //加速の倍率
    public int accelTime = 0;                                   //加速の時間
    [NonSerialized]public int accelCount;                       //加速カウント

    

    public Vector3 lastCheckPoint;                              //チェックポイントの座標
    [NonSerialized] public Vector3 lastRunSpd;                  //チェックポイント通過時の速度
    [NonSerialized] public string lastVec;                      //チェックポイント通過時の進行ベクトル

    private Vector3 latestPos;          //前回のPosition
    private Vector3 diff;               //距離
    private float mgni;                 //距離の前のやつ(軽量化用)

    public string autoRunVec = "front"; //進んでる方向

    [NonSerialized] public bool isCoroutine = false;   //コルーチン再生中
    [NonSerialized] public bool stopCoro = false;

    public bool curveFlg = false;       //カーブ中

    //SoundMgr soundMgr;                  // SE再生用
    private void Start()
    {
        //soundMgr = GameObject.Find("BgmManager").GetComponent<SoundMgr>();  // SE再生用のコンポーネントの取得

        //if(check.checkFlg == true)
        //{
        //    this.GetComponent<Transform>().position = check.CheckPointTransForm().position;
        //}
        Wiz = this.gameObject;                      //プレイヤーオブジェクトを入れる
        //if (check.CheckPointFlg() == true) Wiz.GetComponent<Transform>().position = check.CheckPointTransForm().position;   
        Wiz_TF = Wiz.GetComponent<Transform>();     //プレイヤーのTransFormを入れる
        Wiz_RB = Wiz.GetComponent<Rigidbody>();     //プレイヤーのRigitBodyを入れる

        currentLane = laneObj[4];
        Origin_Pos = Wiz_TF.position;               //プレイヤーの初期位置を設定
        lastCheckPoint = Wiz_TF.position;
        lastRunSpd = runSpd;
        playerOriginSpeed = RunSpeed;               //プレイヤーの速度を保存

        gm = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        
        AutoRun();      //座標の更新ver2

        AccelAutoRun(); //加速カウントが0では無ければ

        LookAtFront();  //移動先をむく
        
    }

    /*========================= 関　　　数 =========================*/

    //自動移動
    void AutoRun() {
        if (curveFlg == false)
        {
            if (RunSpeed != playerOriginSpeed)
            { //ダッシュ時のみ速度反映
                switch (autoRunVec)
                {
                    case "front":
                        runSpd.z = RunSpeed;
                        break;
                    case "right":
                        runSpd.x = RunSpeed;
                        break;
                    case "left":
                        runSpd.x = -RunSpeed;
                        break;
                    case "back":
                        runSpd.z = -RunSpeed;
                        break;
                }
            }
            Wiz_RB.velocity = runSpd;
        }
    }

    //加速移動
    void AccelAutoRun() {
        if (accelCount > 0)
        {
            //soundMgr.PlaySE(SoundMgr.SE_TYPE.ACTION, 2);                // SE再生

            GetComponent<BoxCollider>().size = new Vector3(50, 50, 5);
            accelCount--;

            //時間が経過したらプレイヤーの速度を徐々に初期化
            if (accelCount <= 0)
            {
                StartCoroutine("SlowInitSpeed");
                accelCount = 0;
            }

        }
    }

    //進行方向をむく関数
    void LookAtFront() {

        //前回からどこに進んだかをベクトルで取得
        diff = transform.position - latestPos;   

        //進んだ距離
        mgni = diff.magnitude;

        //ベクトルの大きさが0.01以上の時に向きを変える処理をする
        if (diff.magnitude >= 0.01f && !isCoroutine)
            transform.rotation = Quaternion.LookRotation(new Vector3(diff.x, diff.y, diff.z));

        //前回のPositionの更新
        latestPos = transform.position;  
    }

    //移動先のレーンを格納
    public void setNaighborDistination()
    {
        int current = 0;
        while (current < 9)
        {
            if (currentLane.name == laneObj[current].name) break;
            current++;
        }


        //上のレーン
        if (current / 3 > 0) lanePos[0] = laneObj[current - 3].transform.position;
        else lanePos[0] = Vector3.zero;

        //左のレーン
        if (current % 3 > 0) lanePos[1] = laneObj[current - 1].transform.position;
        else lanePos[1] = Vector3.zero;

        //右のレーン
        if (current % 3 < 2) lanePos[2] = laneObj[current + 1].transform.position;
        else lanePos[2] = Vector3.zero;

        //下のレーン
        if (current / 3 < 2) lanePos[3] = laneObj[current + 3].transform.position;
        else lanePos[3] = Vector3.zero;


    }

    /*========================= コルーチン =========================*/

    //レーン移動のやつ
    public IEnumerator Mover(Vector3 pos1, Vector3 pos2, AnimationCurve ac, float time)
    {
        float timer = 0.0f;
        isCoroutine = true;
        while (timer <= time)
        {

            if (stopCoro) timer = time;

            Vector3 pos = Vector3.Lerp(pos1, pos2, ac.Evaluate(timer / time));
            switch (autoRunVec) {
                case "front":
                case "back":
                    transform.position = new Vector3(pos.x, pos.y, Wiz_TF.position.z);
                    break;

                case "right":
                case "left":
                    transform.position = new Vector3(Wiz_TF.position.x, pos.y, pos.z);
                    break;
            }


            
            timer += Time.deltaTime;
            yield return null;
        }

        isCoroutine = false;
        stopCoro = false;
    }

    //徐々に減速
    private IEnumerator SlowInitSpeed() {

        while (RunSpeed > playerOriginSpeed) {
            float diff = 60f / gm.currentFramerate * 0.4f;
            RunSpeed -= diff;

            yield return null;
        }

        RunSpeed = playerOriginSpeed;
        GetComponent<BoxCollider>().size = new Vector3(10, 10, 5);
        
    }

    /*========================= 移動　以外 =========================*/

    // プレイヤーの加速度を返す
    public Vector3 GetPlayerVelocity()
    {
        return Wiz_RB.velocity;
    }

    //リトライ時の初期化
    public void RetrySpeedReset()
    {
        if (runSpd.x > 0) runSpd.x = 50;
        if (runSpd.y > 0) runSpd.y = 50;
        if (runSpd.z > 0) runSpd.z = 50;
        if (runSpd.x < 0) runSpd.x = -50;
        if (runSpd.y < 0) runSpd.y = -50;
        if (runSpd.z < 0) runSpd.z = -50;
    }

    
}
