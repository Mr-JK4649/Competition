using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAutoRun : MonoBehaviour
{

    public float RunSpeed = 0f;
    public GameObject Wiz;          //プレイヤーオブジェクトを入れる
    public Transform Wiz_TF;        //プレイヤーのトランスフォーム
    public Rigidbody Wiz_RB;        //プレイヤーのRigitbody

    private void Start()
    {
        Wiz = GameObject.Find("Wizard");
        Wiz_TF = Wiz.GetComponent<Transform>();
        Wiz_RB = Wiz.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Wiz_RB.AddForce(new Vector3(0f, 0f, RunSpeed), ForceMode.Impulse);
        Debug.DrawRay(Wiz_TF.position, new Vector3(Wiz_TF.position.x, -50f, Wiz_TF.position.z),new Color(255f,0f,0f));
        
    }
}
