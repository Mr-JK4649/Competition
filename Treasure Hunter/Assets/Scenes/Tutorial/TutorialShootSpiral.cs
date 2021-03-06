﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class TutorialShootSpiral : MonoBehaviour
{
    public PlayerMoveSystem plms;       //プレイヤーの移動を司るスクリプト
    public PlayTutorial pltu;           //チュートリアル
    private Animator animator;          //アニメーション
    private bool animeFlg = true;      //アニメーションを繰り返さないフラグ

    public GameObject ShotPoint;        // 渦を出す位置
    public GameObject Spiral;           // 渦のオブジェクト
    private string spiralName;          //渦の名前

    Vector3 SpiralEulerAngles;          //渦を出す角度

    public float DodgeSpeed;            // キャラクターのドッジ移動量

    public float hori, ver;             //十字キー/スティックの方向
    //private bool cont_A;                //Aキーを押すやつ
    private float oldHori, oldVer;      //前のフレームの傾き


    private void Start()
    {
        animator = GameObject.Find("Player02").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        InputProcess();

        plms.setNaighborDistination();

        if(pltu.tutoNum != 99 || pltu.skipTuto){
            if (GameObject.FindWithTag("Spiral") == null && plms.curveFlg == false && plms.clearFlg == false)
            {



                if (ver > 0f && ver != oldVer && plms.lanePos[0] != Vector3.zero)        //上
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, 0f);
                    spiralName = "UpSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE1);
                    pltu.tutoNum = 99;
                }
                else if (ver < 0f && ver != oldVer && plms.lanePos[3] != Vector3.zero)   //下
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, 180f);
                    spiralName = "DownSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE1);
                    pltu.tutoNum = 99;
                }
                else if (hori < 0 && hori != oldHori && plms.lanePos[1] != Vector3.zero)   //左
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, 90f);
                    spiralName = "LeftSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE1);
                    pltu.tutoNum = 99;
                }
                else if (hori > 0 && hori != oldHori && plms.lanePos[2] != Vector3.zero)   //右
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, -90f);
                    spiralName = "RightSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE1);
                    pltu.tutoNum = 99;
                }
            }

            if (Input.GetButton("Cont_A") || Input.GetMouseButton(0) && plms.curveFlg == false && plms.clearFlg == false)
            {
                //加速渦のSE
                //var currentSENames = SEManager.Instance.GetCurrentAudioNames();
                //if (!currentSENames.Contains("MSFX_CHRONO_GALE_WIND"))
                //{
                //    SEManager.Instance.Play(SEPath.MSFX_CHRONO_GALE_WIND);
                //}

                plms.accelCount = plms.accelTime;
                plms.RunSpeed = plms.playerOriginSpeed * plms.accelForce;
                pltu.tutoNum = 99;
                if (animeFlg) animator.SetTrigger("Spiral_UP");
                animeFlg = false;
            }
            else
            {
                animeFlg = true;
            }
        }

        oldHori = hori;
        oldVer = ver;
    }

    //渦を出す処理
    public void SpiralShot()
    {
        if (plms.isCoroutine) plms.stopCoro = true;
        Vector3 SpiralPos = ShotPoint.transform.position;
        GameObject newSpiral = Instantiate(Spiral, SpiralPos, Quaternion.Euler(SpiralEulerAngles));
        newSpiral.name = spiralName;
    }

    //入力の処理
    void InputProcess()
    {
        hori = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    //進む方向によって渦の角度を変える処理
    void CorrectionSpiralAngles()
    {
        switch (plms.autoRunVec)
        {
            case "front": break;
            case "left": SpiralEulerAngles.y += 90f; break;
            case "right": SpiralEulerAngles.y -= 90f; break;
            case "back": SpiralEulerAngles.y += 180f; break;
        }
    }
}
