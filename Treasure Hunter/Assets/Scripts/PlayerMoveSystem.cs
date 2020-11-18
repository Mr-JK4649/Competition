using System;
using System.Collections;
using UnityEngine;

public class PlayerMoveSystem : MonoBehaviour
{
    private GameManager gm;

    public float RunSpeed = 0f;      //プレイヤーの自動飛行の速度
    public Vector3 runSpd;           //移動速度Vector3版
    [NonSerialized] public GameObject Wiz;          //プレイヤーオブジェクトを入れる
    [NonSerialized] public Transform Wiz_TF;        //プレイヤーのトランスフォーム
    [NonSerialized] public Rigidbody Wiz_RB;        //プレイヤーのRigitbody
    
    public float LaneMoveSpeed = 0f;                            //プレイヤーのレーン移動速度

    public GameObject[] laneObj;                                //レーンオブジェクトの位置
    public Vector3[] lanePos = new Vector3[4];                  //移動先レーンの位置
    public GameObject currentLane;                              //現在のレーン
    public AnimationCurve dodgeSpeed;                           //うずの移動速度
    public float dodgeTime;                                     //ドッジ回避に掛かる時間
    [NonSerialized] public Vector3 Origin_Pos;                  //プレイヤーの初期値
    private Vector3 Move;                                       //プレイヤーの微調整
    [NonSerialized] public float playerOriginSpeed;             //プレイヤーの元の速度保存用
    public float accelForce;                                    //加速の倍率
    public int accelTime = 0;                                   //加速の時間
    [NonSerialized]public int accelCount;                       //加速カウント

    

    public Vector3 lastCheckPoint;
    [NonSerialized] public string lastVec;                     //最後に通ったチェックポイントのベクトル

    private Vector3 latestPos;  //前回のPosition
    public Vector3 diff;        //距離
    public float mgni;          //距離の前のやつ(軽量化用)

    public string autoRunVec = "front"; //進んでる方向

    private bool isCoroutine = false;

    private void Start()
    {
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
        playerOriginSpeed = RunSpeed;               //プレイヤーの速度を保存

        gm = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }


    private void FixedUpdate()
    {

        //レーン移動の処理
        //LaneMove();

        //座標の更新
        {
            //switch (autoRunVec) {
            //    case "front":
            //        Wiz_RB.velocity = new Vector3(Move.x, Move.y, RunSpeed);
            //        break;

            //    case "left":
            //        Wiz_RB.velocity = new Vector3(-RunSpeed, Move.y, Move.x);
            //        break;

            //    case "right":
            //        Wiz_RB.velocity = new Vector3(RunSpeed, Move.y, Move.x);
            //        break;

            //    case "back":
            //        Wiz_RB.velocity = new Vector3(Move.x, Move.y, -RunSpeed);
            //        break;
            //}
        }

        //座標の更新ver2
        Wiz_RB.velocity = runSpd;

        //加速カウントが0では無ければ
        if (accelCount > 0)
        {
            GetComponent<BoxCollider>().size = new Vector3(100, 100, 5);
            accelCount--;

            //時間が経過したらプレイヤーの速度を徐々に初期化
            if (accelCount <= 0)
            {
                StartCoroutine("SlowInitSpeed");
                accelCount = 0;
            }

        }

        diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得

        mgni = diff.magnitude;

        //ベクトルの大きさが0.01以上の時に向きを変える処理をする
        if (diff.magnitude >= 0.01f && RunSpeed != playerOriginSpeed * accelForce && !isCoroutine)
        {
            Debug.Log("現在の向き : " + autoRunVec);
            transform.rotation = Quaternion.LookRotation(new Vector3(diff.x, diff.y, diff.z)); //向きを変更する
        }

        latestPos = transform.position;  //前回のPositionの更新
    }


    // プレイヤーの加速度を返す
    public Vector3 GetPlayerVelocity() 
    {
        return Wiz_RB.velocity;
    }

    //ＷＡＳＤキーの移動
    void LaneMove() {

        Move = Vector3.zero;

        int isAccel = RunSpeed > playerOriginSpeed ? 5 : 1;

        //if (Wiz_TF.position.x - 0.5f > Origin_Pos.x - 13.5f &&
        //    Wiz_TF.position.x + 0.5f < Origin_Pos.x + 13.5f)
        //{
        //    Move.x = Input.GetAxis("Horizontal") * LaneMoveSpeed * isAccel;
        //}
        //else {
        //    Move.x = Input.GetAxis("Horizontal") * -LaneMoveSpeed * 5 * isAccel;
        //}

        //if (Wiz_TF.position.y - 0.5f > Origin_Pos.y - 13.5f &&
        //    Wiz_TF.position.y + 0.5f < Origin_Pos.y + 13.5f)
        //{
        //    Move.y = Input.GetAxis("Vertical") * LaneMoveSpeed * isAccel;
        //}
        //else {
        //    Move.y = Input.GetAxis("Vertical") * -LaneMoveSpeed * 5 * isAccel;
        //}

        Move.x = Input.GetAxis("Horizontal") * LaneMoveSpeed * isAccel;
        Move.y = Input.GetAxis("Vertical") * LaneMoveSpeed * isAccel;

    }


    //移動先のレーンを格納
    public void setNaighborDistination() {
        int current = 0;
        while (current < 9) {
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

    //レーン移動のやつ
    public IEnumerator Mover(Vector3 pos1, Vector3 pos2, AnimationCurve ac, float time)
    {
        float timer = 0.0f;
        isCoroutine = true;
        while (timer <= time)
        {

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


            isCoroutine = false;
            timer += Time.deltaTime;
            yield return null;
        }
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
}
