using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class PlayerMoveSystem : MonoBehaviour
{

    public float RunSpeed = 0f;     //プレイヤーの自動飛行の速度
    private GameObject Wiz;          //プレイヤーオブジェクトを入れる
    private Transform Wiz_TF;        //プレイヤーのトランスフォーム
    private Rigidbody Wiz_RB;        //プレイヤーのRigitbody

    public float LaneMoveSpeed = 0f;            //プレイヤーのレーン移動速度
    public GameObject[] laneObj;                //レーンオブジェクトの位置
    public Vector3[] lanePos = new Vector3[4];  //移動先レーンの位置
    public GameObject currentLane;              //現在のレーン
    public int SpiralMoveNum = -1;              //うずの識別番号
    public AnimationCurve dodgeSpeed;           //うずの移動速度
    public Vector3 Origin_Pos;                 //プレイヤーの初期値
    private Vector3 Move;                       //プレイヤーの微調整

    public bool blockHitFlg = false;    //障害物に当たるかのフラグ
    private bool deathFlg = false;      //プレイヤーが障害物に当たったかの判定
    public GameObject bom;              //爆発
    

    private void Start()
    {
        Wiz = this.gameObject;                      //プレイヤーオブジェクトを入れる
        Wiz_TF = Wiz.GetComponent<Transform>();     //プレイヤーのTransFormを入れる
        Wiz_RB = Wiz.GetComponent<Rigidbody>();     //プレイヤーのRigitBodyを入れる

        Origin_Pos = Wiz_TF.position;               //プレイヤーの初期位置を設定
    }

    private void Update()
    {

        //レーン移動の処理
        LaneMove();

        //お試しうずの移動
        setNaighborDistination();
        if (SpiralMoveNum != -1) {
            Wiz_TF.position = new Vector3(lanePos[SpiralMoveNum].x, lanePos[SpiralMoveNum].y, Wiz_TF.position.z);
            SpiralMoveNum = -1;
        }
            

        //プレイヤーの座標更新
        Wiz_RB.velocity = new Vector3(Move.x, Move.y, RunSpeed);
        
        //障害物に当たった時の処理
        if(deathFlg && blockHitFlg)
            Wiz_RB.velocity = new Vector3(0f, -9.81f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (blockHitFlg)
        {
            Instantiate(bom, this.gameObject.transform);
            RunSpeed = -0.5f;
            deathFlg = true;
        }
    }

    //ＷＡＳＤキーの移動
    void LaneMove() {

        Move = Vector3.zero;

        if (Wiz_TF.position.x - 0.5f > Origin_Pos.x - 4.5f &&
            Wiz_TF.position.x + 0.5f < Origin_Pos.x + 4.5f)
        {
            Move.x = Input.GetAxis("Horizontal") * LaneMoveSpeed;
        }
        else {
            Move.x = Input.GetAxis("Horizontal") * -LaneMoveSpeed * 5;
        }

        if (Wiz_TF.position.y - 0.5f > Origin_Pos.y - 4.5f &&
            Wiz_TF.position.y + 0.5f < Origin_Pos.y + 4.5f)
        {
            Move.y = Input.GetAxis("Vertical") * LaneMoveSpeed;
        }
        else {
            Move.y = Input.GetAxis("Vertical") * -LaneMoveSpeed * 5;
        }

    }

    //移動先のレーンを格納
    void setNaighborDistination() {
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
    //void DodgeMovement() {
    //    Vector3.Lerp(lanePos[SpiralMoveNum], Wiz_TF.position, );
    //}

    //IEnumerator SpiralMovement() { 
        
    //    Vector3.Lerp(lanePos[SpiralMoveNum], Wiz_TF.position,)

    //}
}
