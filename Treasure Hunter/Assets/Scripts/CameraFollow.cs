﻿using MeshUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CameraFollow : MonoBehaviour
{

    public PlayerMoveSystem pms;            //プレイヤーのスクリプト
    public bool FixedCamera = false;        //カメラを定点化するやつ
    private bool FixedCameraFlg = false;    //カメラ固定スペース用のフラグ

    //カメラの位置を調整用
    private GameObject player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用

    Transform pl;

    //カメラ速度減衰用の変数
    private float distance;   // カメラとプレイヤーとの距離
    public float height = 2.0f;     // 高さ


    public float attenRate = 5.0f;  // 減衰比率

    void Start()
    {
        

        //unitychanの情報を取得
        pl = GameObject.Find("Wizard").GetComponent<Transform>();

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - pl.position;

        distance = transform.position.z - pms.GetComponent<Transform>().position.z;

    }

    private void Update()
    {

        if (pms.accelCount >= 1)
        {
            var pos = pms.GetComponent<Transform>().position + new Vector3(0.0f, height, -distance);    // 本来到着しているカメラの位置
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * attenRate);     // Lerp減衰
        }
        else
        {
            if (pms.accelCount == 0 && pms.RunSpeed != pms.playerOriginSpeed)
            {
                transform.position = Vector3.Lerp(transform.position, pl.position + offset, 1f - (pms.RunSpeed - pms.playerOriginSpeed) / 100f);
                
            }
            else
            {
                //新しいトランスフォームの値を代入する
                transform.position = pl.position + offset;
                Debug.Log("入ってる！！");
            }
        }
        



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
