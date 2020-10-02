using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMoveSystem : MonoBehaviour
{

    public float RunSpeed = 0f;     //プレイヤーの自動飛行の速度
    public GameObject Wiz;          //プレイヤーオブジェクトを入れる
    public Transform Wiz_TF;        //プレイヤーのトランスフォーム
    public Rigidbody Wiz_RB;        //プレイヤーのRigitbody

    public float LaneMoveSpeed = 0f;    //プレイヤーのレーン移動速度
    private Vector3 Origin_Pos;         //プレイヤーの初期値 

    private void Start()
    {
        Wiz = this.gameObject;                      //プレイヤーオブジェクトを入れる
        Wiz_TF = Wiz.GetComponent<Transform>();     //プレイヤーのTransFormを入れる
        Wiz_RB = Wiz.GetComponent<Rigidbody>();     //プレイヤーのRigitBodyを入れる

        Origin_Pos = Wiz_TF.position;               //プレイヤーの初期位置を設定
    }

    private void Update()
    {
        
        //if(Wiz_TF.position.x - 0.5f > Origin_Pos.x - 4.5f || )
        float X_Move = Input.GetAxis("Horizontal") * LaneMoveSpeed;
        float Y_Move = Input.GetAxis("Vertical") * LaneMoveSpeed;
        Wiz_RB.velocity = new Vector3(X_Move, Y_Move, RunSpeed);
            

    }

    
}
