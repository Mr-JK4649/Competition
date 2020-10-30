using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShootSpiral : MonoBehaviour
{

    public PlayerMoveSystem plms;       //プレイヤーの移動を司るスクリプト

    public GameObject ShotPoint;        // 渦を出す位置
    public GameObject Spiral;           // 渦のオブジェクト
    private string spiralName;          //渦の名前

    Vector3 SpiralEulerAngles;          //渦を出す角度
    float x = 0;
    float y = 0;
    float z = 0;

    public float DodgeSpeed;            // キャラクターのドッジ移動量

    public float hori, ver;             //十字キー/スティックの方向
    private bool cont_A;                //Aキーを押すやつ
    private float oldHori, oldVer;      //前のフレームの傾き

    void Update()
    {

        InputProcess();
        plms.setNaighborDistination();

        if (GameObject.FindWithTag("Spiral") == null)
        {

            //縦方向のうず移動
            if (ver > 0f && cont_A == true && plms.lanePos[0] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = 0f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "UpSpiral";
                SpiralShot();
            }
            else if (ver < 0f && cont_A == true && plms.lanePos[3] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = 0f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "DownSpiral";
                SpiralShot();
            }
            else if (hori < 0 && cont_A == true && plms.lanePos[1] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = -90f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "LeftSpiral";
                SpiralShot();
            }
            else if (hori > 0 && cont_A == true && plms.lanePos[2] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = 90f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "RightSpiral";
                SpiralShot();
            }
            else if (cont_A == true)
            {
                x = 90f;
                y = 0f;
                z = 0f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "AccelSpiral";
                SpiralShot();
            }

        }
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

    public void SpiralShot()
    {
        Vector3 SpiralPos = ShotPoint.transform.position;
        GameObject newSpiral = Instantiate(Spiral, SpiralPos, Quaternion.Euler(SpiralEulerAngles));
        newSpiral.name = spiralName;
    }

    void InputProcess() {
        hori = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        cont_A = Input.GetButtonDown("Cont_A") | Input.GetMouseButtonDown(0);
    }
}
