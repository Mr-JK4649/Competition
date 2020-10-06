using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpiral : MonoBehaviour
{
    public Cyclone_Move cyc_m;          // 渦のシステムのscript
    public PlayerMoveSystem move_sys;   // キャラクターの移動に関するやつ

    public GameObject ShotPoint;        // 渦を出す位置
    public GameObject Spiral;           // 渦のオブジェクト

    Vector3 SpiralEulerAngles;          //渦を出す角度
    float x = 0;
    float y = 0;
    float z = 0;

    public float DodgeSpeed;            // キャラクターのドッジ移動量

    void Update()
    {

        //マウスホイールクリック
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("マウスホイールクリック");
            x = 90f;
            y = 0f;
            z = 0f;
            SpiralEulerAngles = new Vector3(x, y, z);
            cyc_m.setDodgeVec(0f, 0f, DodgeSpeed);
            SpiralShot();
        }

        //左クリック＆シフト
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("左クリック＆シフト");
            x = 0f;
            y = 0f;
            z = 0f;
            SpiralEulerAngles = new Vector3(x, y, z);
            cyc_m.setDodgeVec(0f, DodgeSpeed, 0f);
            SpiralShot();
        }
        else
        {
            //左クリック
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("左クリック");
                Vector3 dir = ShotPoint.transform.up;
                x = 0f;
                y = 0f;
                z = -90f;
                SpiralEulerAngles = new Vector3(x, y, z);
                cyc_m.setDodgeVec(-DodgeSpeed, 0f, 0f);
                SpiralShot();
            }
        }
            //右クリック＆シフト
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("右クリック＆シフト");
            x = 0f;
            y = 0f;
            z = 0f;
            SpiralEulerAngles = new Vector3(x, y, z);
            cyc_m.setDodgeVec(0f, -DodgeSpeed, 0f);
            SpiralShot();
        }
        else
        {
            //右クリック
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("右クリック");
                x = 0f;
                y = 0f;
                z = 90f;
                SpiralEulerAngles = new Vector3(x, y, z);
                cyc_m.setDodgeVec(DodgeSpeed, 0f, 0f);
                SpiralShot();
                
            }
        }

    }

    public void SpiralShot()
    {
        Vector3 SpiralPos = ShotPoint.transform.position;
        GameObject newSpiral = Instantiate(Spiral, SpiralPos, Quaternion.Euler(SpiralEulerAngles));
        newSpiral.name = Spiral.name;
    }
}
