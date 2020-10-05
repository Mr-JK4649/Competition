using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpiral : MonoBehaviour
{
    public Cyclone_Move cyc_m;  // 渦のシステムのscript

    public GameObject ShotPoint;
    public GameObject Spiral;

    Vector3 SpiralEulerAngles;
    float x = 0;
    float y = 0;
    float z = 0;

    public float DodgeSpeed;

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
            SpiralShot();
            cyc_m.z = DodgeSpeed;
        }

        //左クリック＆シフト
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("左クリック＆シフト");
            x = 0f;
            y = 0f;
            z = 0f;
            SpiralEulerAngles = new Vector3(x, y, z);
            SpiralShot();
            cyc_m.z = DodgeSpeed;
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
                SpiralShot();
                cyc_m.x = -DodgeSpeed;
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
            SpiralShot();
            cyc_m.y = -DodgeSpeed;
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
                SpiralShot();
                cyc_m.x = DodgeSpeed;
            }
        }

    }

    public void SpiralShot()
    {
            Vector3 SpiralPos = ShotPoint.transform.position;
            GameObject newSpiral = Instantiate(Spiral, SpiralPos, Quaternion.Euler(SpiralEulerAngles));
        Vector3 dir = ShotPoint.transform.eulerAngles = SpiralEulerAngles;
        newSpiral.name = Spiral.name;
            Destroy(newSpiral, 0.8f);
    }
}
