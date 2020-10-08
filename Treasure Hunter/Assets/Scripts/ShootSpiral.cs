using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShootSpiral : MonoBehaviour
{

    public PlayerMoveSystem plms;

    public GameObject ShotPoint;        // 渦を出す位置
    public GameObject Spiral;           // 渦のオブジェクト
    private string spiralName;          //渦の名前

    Vector3 SpiralEulerAngles;          //渦を出す角度
    float x = 0;
    float y = 0;
    float z = 0;

    public float DodgeSpeed;            // キャラクターのドッジ移動量

    void Update()
    {

        ////マウスホイールクリック
        //if (Input.GetMouseButtonDown(2))
        //{
        //    Debug.Log("マウスホイールクリック");
        //    x = 90f;
        //    y = 0f;
        //    z = 0f;
        //    SpiralEulerAngles = new Vector3(x, y, z);
        //    spiralName = "AccelSpiral";
        //    SpiralShot();
        //}

        ////左クリック＆シフト
        //if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        //{
        //    if (plms.lanePos[0] != Vector3.zero)
        //    {
        //        Debug.Log("左クリック＆シフト");
        //        x = 0f;
        //        y = 0f;
        //        z = 0f;
        //        SpiralEulerAngles = new Vector3(x, y, z);
        //        spiralName = "UpSpiral";
        //        SpiralShot();
        //    }
        //}
        //else
        //{
        //    //左クリック
        //    if (Input.GetMouseButtonDown(0) && plms.lanePos[1] != Vector3.zero)
        //    {
        //        Debug.Log("左クリック");
        //        Vector3 dir = ShotPoint.transform.up;
        //        x = 0f;
        //        y = 0f;
        //        z = -90f;
        //        SpiralEulerAngles = new Vector3(x, y, z);
        //        spiralName = "LeftSpiral";
        //        SpiralShot();
        //    }
        //}
        ////右クリック＆シフト
        //if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        //{
        //    if (plms.lanePos[3] != Vector3.zero)
        //    {
        //        Debug.Log("右クリック＆シフト");
        //        x = 0f;
        //        y = 0f;
        //        z = 0f;
        //        SpiralEulerAngles = new Vector3(x, y, z);
        //        spiralName = "DownSpiral";
        //        SpiralShot();
        //    }
        //}
        //else
        //{
        //    //右クリック
        //    if (Input.GetMouseButtonDown(1) && plms.lanePos[2] != Vector3.zero)
        //    {
        //        Debug.Log("右クリック");
        //        x = 0f;
        //        y = 0f;
        //        z = 90f;
        //        SpiralEulerAngles = new Vector3(x, y, z);
        //        spiralName = "RightSpiral";
        //        SpiralShot();
                
        //    }
        //}


            if (Input.GetKey(KeyCode.W) && Input.GetMouseButtonDown(0) && plms.lanePos[0] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = 0f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "UpSpiral";
                SpiralShot();
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(0) && plms.lanePos[1] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = -90f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "LeftSpiral";
                SpiralShot();
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetMouseButtonDown(0) && plms.lanePos[2] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = 90f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "RightSpiral";
                SpiralShot();
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0) && plms.lanePos[3] != Vector3.zero)
            {
                x = 0f;
                y = 0f;
                z = 0f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "DownSpiral";
                SpiralShot();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                x = 90f;
                y = 0f;
                z = 0f;
                SpiralEulerAngles = new Vector3(x, y, z);
                spiralName = "AccelSpiral";
                SpiralShot();
            }

    }

    public void SpiralShot()
    {
        Vector3 SpiralPos = ShotPoint.transform.position;
        GameObject newSpiral = Instantiate(Spiral, SpiralPos, Quaternion.Euler(SpiralEulerAngles));
        newSpiral.name = spiralName;
    }
}
