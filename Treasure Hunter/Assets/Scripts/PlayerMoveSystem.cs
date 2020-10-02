using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class PlayerMoveSystem : MonoBehaviour
{

    public float RunSpeed = 0f;     //プレイヤーの自動飛行の速度
    public GameObject Wiz;          //プレイヤーオブジェクトを入れる
    public Transform Wiz_TF;        //プレイヤーのトランスフォーム
    public Rigidbody Wiz_RB;        //プレイヤーのRigitbody

    public float LaneMoveSpeed = 0f;    //プレイヤーのレーン移動速度
    public Vector3 Origin_Pos;         //プレイヤーの初期値 
    public Vector3 Move;               //プレイヤーの微調整

    public GameObject spiral;

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

        //プレイヤーの座標更新
        Wiz_RB.velocity = new Vector3(Move.x, Move.y, RunSpeed);
        
    }

    void LaneMove() {

        Move = Vector3.zero;

        if (Wiz_TF.position.x - 0.5f > Origin_Pos.x - 4.5f &&
            Wiz_TF.position.x + 0.5f < Origin_Pos.x + 4.5f)
        {
            Move.x = Input.GetAxis("Horizontal") * LaneMoveSpeed;
        }
        else {
            Move.x = Input.GetAxis("Horizontal") * -LaneMoveSpeed * 2;
        }

        if (Wiz_TF.position.y - 0.5f > Origin_Pos.y - 4.5f &&
            Wiz_TF.position.y + 0.5f < Origin_Pos.y + 4.5f)
        {
            Move.y = Input.GetAxis("Vertical") * LaneMoveSpeed;
        }
        else {
            Move.y = Input.GetAxis("Vertical") * -LaneMoveSpeed * 2;
        }

    }

}
