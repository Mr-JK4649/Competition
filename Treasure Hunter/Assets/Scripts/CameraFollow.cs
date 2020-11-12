using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public PlayerMoveSystem pms;            //プレイヤーのスクリプト
    public bool FixedCamera = false;        //カメラを定点化するやつ
    private bool FixedCameraFlg = false;    //カメラ固定スペース用のフラグ

    //カメラの位置を調整用
    private GameObject player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用

    Transform pl;

    void Start()
    {

        //unitychanの情報を取得
        pl = GameObject.Find("Wizard").GetComponent<Transform>();

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - pl.position;

    }

    private void Update()
    {

        //新しいトランスフォームの値を代入する
        transform.position = pl.position + offset;

        



        //Transform pl = GameObject.Find("Wizard").GetComponent<Transform>();

        //if (FixedCamera){

        //    this.gameObject.GetComponent<Transform>().position = new Vector3(pms.Origin_Pos.x, pms.Origin_Pos.y + 4.5f, pl.position.z - 20.5f);
        //}
        //else {
        //    this.gameObject.GetComponent<Transform>().position = new Vector3(pl.position.x, pl.position.y + 4.5f, pl.position.z - 20.5f);
        //}


        //if (Input.GetKey(KeyCode.Space))
        //{
        //    if (FixedCameraFlg == false) FixedCamera = !FixedCamera;
        //    FixedCameraFlg = true;
        //}
        //else {
        //    FixedCameraFlg = false;
        //}
    }
}
