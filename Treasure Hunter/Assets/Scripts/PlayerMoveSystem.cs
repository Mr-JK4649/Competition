using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class PlayerMoveSystem : MonoBehaviour
{
    private GameManager gm;

    public float RunSpeed = 0f;      //プレイヤーの自動飛行の速度
    [NonSerialized] public GameObject Wiz;          //プレイヤーオブジェクトを入れる
    [NonSerialized] public Transform Wiz_TF;        //プレイヤーのトランスフォーム
    [NonSerialized] public Rigidbody Wiz_RB;        //プレイヤーのRigitbody
    private CheckPoint check;
    
    public float LaneMoveSpeed = 0f;                            //プレイヤーのレーン移動速度
    public GameObject[] laneObj;                                //レーンオブジェクトの位置
    [NonSerialized] public Vector3[] lanePos = new Vector3[4];  //移動先レーンの位置
    [NonSerialized] public GameObject currentLane;              //現在のレーン
    public AnimationCurve dodgeSpeed;                           //うずの移動速度
    public float dodgeTime;                                     //ドッジ回避に掛かる時間
    [NonSerialized] public Vector3 Origin_Pos;                  //プレイヤーの初期値
    private Vector3 Move;                                       //プレイヤーの微調整
    [NonSerialized] public float playerOriginSpeed;             //プレイヤーの元の速度保存用
    public float accelForce;                                    //加速の倍率
    public int accelTime = 0;                                   //加速の時間
    [NonSerialized]public int accelCount;                       //加速カウント

    public Vector3 lastCheckPoint;

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

    private void Update()
    {

        //レーン移動の処理
        LaneMove();

        //プレイヤーの座標更新
        Wiz_RB.velocity = new Vector3(Move.x, Move.y, RunSpeed);

        if (Input.GetKeyDown(KeyCode.Z) && lastCheckPoint != Vector3.zero)
            this.transform.position = lastCheckPoint;

    }

    private void FixedUpdate()
    {

        //加速カウントが0では無ければ
        if (accelCount > 0)
        {

            accelCount--;

            //時間が経過したらプレイヤーの速度を徐々に初期化
            if (accelCount <= 0)
            {
                StartCoroutine("SlowInitSpeed");
                accelCount = 0;
            }

        }
    }


    // プレイヤーの加速度を返す
    public Vector3 GetPlayerVelocity() 
    {
        return Wiz_RB.velocity;
    }

    //ＷＡＳＤキーの移動
    void LaneMove() {

        Move = Vector3.zero;

        if (Wiz_TF.position.x - 0.5f > Origin_Pos.x - 13.5f &&
            Wiz_TF.position.x + 0.5f < Origin_Pos.x + 13.5f)
        {
            Move.x = Input.GetAxis("Horizontal") * LaneMoveSpeed;
        }
        else {
            Move.x = Input.GetAxis("Horizontal") * -LaneMoveSpeed * 5;
        }

        if (Wiz_TF.position.y - 0.5f > Origin_Pos.y - 13.5f &&
            Wiz_TF.position.y + 0.5f < Origin_Pos.y + 13.5f)
        {
            Move.y = Input.GetAxis("Vertical") * LaneMoveSpeed;
        }
        else {
            Move.y = Input.GetAxis("Vertical") * -LaneMoveSpeed * 5;
        }

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
        while (timer <= time)
        {

            Vector3 pos = Vector3.Lerp(pos1, pos2, ac.Evaluate(timer / time));
            transform.position = new Vector3(pos.x,pos.y,Wiz_TF.position.z);
            

            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SlowInitSpeed() {

        while (RunSpeed > playerOriginSpeed) {
            float diff = 60f / gm.currentFramerate * 0.4f;
            RunSpeed -= diff;

            yield return null;
        }

        RunSpeed = playerOriginSpeed;

        
    }
}
