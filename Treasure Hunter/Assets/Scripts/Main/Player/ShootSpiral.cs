using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using KanKikuchi.AudioManager;

public class ShootSpiral : MonoBehaviour
{

    public PlayerMoveSystem plms;       //プレイヤーの移動を司るスクリプト
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

        //version1.3
        {
            if (GameObject.FindWithTag("Spiral") == null && plms.curveFlg == false)
            {



                if (ver > 0f && ver != oldVer && plms.lanePos[0] != Vector3.zero)        //上
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, 0f);
                    spiralName = "UpSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE);
                }
                else if (ver < 0f && ver != oldVer && plms.lanePos[3] != Vector3.zero)   //下
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, 180f);
                    spiralName = "DownSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE);
                }
                else if (hori < 0 && hori != oldHori && plms.lanePos[1] != Vector3.zero)   //左
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, 90f);
                    spiralName = "LeftSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE);
                }
                else if (hori > 0 && hori != oldHori && plms.lanePos[2] != Vector3.zero)   //右
                {
                    SpiralEulerAngles = new Vector3(0f, 0f, -90f);
                    spiralName = "RightSpiral";
                    CorrectionSpiralAngles();
                    SpiralShot();
                    SEManager.Instance.Play(SEPath.MSFX_EXPLOSION_2_EXPLODE);
                }
                //else if (cont_A == true)
                //{
                //    SpiralEulerAngles = new Vector3(90f, 0f, 0f);
                //    spiralName = "AccelSpiral";
                //    CorrectionSpiralAngles();
                //    SpiralShot();
                //}
            }

            if (Input.GetButton("Cont_A") || Input.GetMouseButton(0) && plms.curveFlg == false)
            {
                //加速渦のSE
                //var currentSENames = SEManager.Instance.GetCurrentAudioNames();
                //if (!currentSENames.Contains("MSFX_CHRONO_GALE_WIND"))
                //{
                //    SEManager.Instance.Play(SEPath.MSFX_CHRONO_GALE_WIND);
                //}

                plms.accelCount = plms.accelTime;
                plms.RunSpeed = plms.playerOriginSpeed * plms.accelForce;
                if(animeFlg)animator.SetTrigger("Spiral_UP");
                animeFlg = false;
            }
            else {
                animeFlg = true;
            }
        }

        //version1.2
        {
            //if (GameObject.FindWithTag("Spiral") == null)
            //{

            //    //縦方向のうず移動
            //    if (ver > 0f && cont_A == true && plms.lanePos[0] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = 0f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "UpSpiral";
            //        SpiralShot();
            //    }
            //    else if (ver < 0f && cont_A == true && plms.lanePos[3] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = 0f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "DownSpiral";
            //        SpiralShot();
            //    }
            //    else if (hori < 0 && cont_A == true && plms.lanePos[1] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = -90f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "LeftSpiral";
            //        SpiralShot();
            //    }
            //    else if (hori > 0 && cont_A == true && plms.lanePos[2] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = 90f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "RightSpiral";
            //        SpiralShot();
            //    }
            //    else if (cont_A == true)
            //    {
            //        x = 90f;
            //        y = 0f;
            //        z = 0f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "AccelSpiral";
            //        SpiralShot();
            //    }

            //}
        }

        //version1.0
        {
            //if (GameObject.FindWithTag("Spiral") == null)
            //{

            //    if ((Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") == 1) &&
            //        (Input.GetMouseButtonDown(0) || Input.GetButton("Cont_A") == true)
            //        && plms.lanePos[0] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = 0f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "UpSpiral";
            //        SpiralShot();
            //    }
            //    else if ((Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") == -1) &&
            //        (Input.GetMouseButtonDown(0) || Input.GetButton("Cont_A") == true) &&
            //        plms.lanePos[1] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = -90f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "LeftSpiral";
            //        SpiralShot();
            //    }
            //    else if ((Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") == 1) &&
            //        (Input.GetMouseButtonDown(0) || Input.GetButton("Cont_A") == true) &&
            //        plms.lanePos[2] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = 90f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "RightSpiral";
            //        SpiralShot();
            //    }
            //    else if ( (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") == -1) &&
            //              (Input.GetMouseButtonDown(0) || Input.GetButton("Cont_A") == true) &&
            //              plms.lanePos[3] != Vector3.zero)
            //    {
            //        x = 0f;
            //        y = 0f;
            //        z = 0f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "DownSpiral";
            //        SpiralShot();
            //    }
            //    else if (Input.GetButton("Cont_A") == true)
            //    {
            //        x = 90f;
            //        y = 0f;
            //        z = 0f;
            //        SpiralEulerAngles = new Vector3(x, y, z);
            //        spiralName = "AccelSpiral";
            //        SpiralShot();
            //    }

            //}
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
    void InputProcess() {
        hori = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    //進む方向によって渦の角度を変える処理
    void CorrectionSpiralAngles() {
        switch (plms.autoRunVec)
        {
            case "front":                                   break;
            case "left":    SpiralEulerAngles.y += 90f;     break;
            case "right":   SpiralEulerAngles.y -= 90f;     break;
            case "back":    SpiralEulerAngles.y += 180f;    break;
        }
    }
}
