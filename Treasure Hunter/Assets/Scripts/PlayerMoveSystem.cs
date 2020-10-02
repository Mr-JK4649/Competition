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

    public float LaneMoveSpeed = 0f;

    private void Start()
    {
        Wiz = this.gameObject;
        Wiz_TF = Wiz.GetComponent<Transform>();
        Wiz_RB = Wiz.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        float X_Move = Input.GetAxis("Horizontal") * LaneMoveSpeed;
        float Y_Move = Input.GetAxis("Vertical") * LaneMoveSpeed;
        //Wiz_RB.AddForce(new Vector3(X_Move, Y_Move, RunSpeed), ForceMode.Impulse);
        Wiz_RB.velocity = new Vector3(X_Move, Y_Move, RunSpeed);

    }

    
}
