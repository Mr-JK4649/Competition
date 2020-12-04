using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class FadeOut : MonoBehaviour
{
    public float startDistance = 30;
    private float hiddenDistance = 25;
    private GameObject[] v_camera = new GameObject[4];

    private int coolTime = 90;
    private int time = 0;

    private void Start()
    {
        v_camera[0] = GameObject.Find("FollowCamera");
        v_camera[1] = GameObject.Find("FollowCameraR");
        v_camera[2] = GameObject.Find("FollowCameraL");
        v_camera[3] = GameObject.Find("FollowCameraB");
    }

    private void FixedUpdate()
    {
        int i;  // 今使っているカメラの添え字番号を記憶
        const int CAMERA_MAX = 4;   // カメラの最大数

        for(i=0;i<CAMERA_MAX;i++)
        {
            if(v_camera[i].GetComponent<Camera>().enabled == true)  // カメラを使用していたら
            {
                break;
            }
        }

        //V_cameraとオブジェクトの距離を変数dに入れる
        var d = Vector3.Distance(v_camera[i].transform.position, transform.position);

        //透明度をいじるための変数格納
        var color = this.GetComponent<Renderer>().material.color;

        if (--time <= 0)
        {
            //ある程度距離が縮まったら完全透明化
            if (d <= hiddenDistance)
            {
                Debug.Log("穂高！");
                color.a = 0.2f;
                time = coolTime;
            }
            else if (d <= startDistance)        //距離が一定以下になったら透明化開始
            {
                color.a = (d - hiddenDistance) / (startDistance - hiddenDistance) + 0.2f;
            }
            else                                //透明ではない
            {
                color.a = 1.0f;
            }
        }

        //変更した処理を反映する
        this.GetComponent<Renderer>().material.color = color;
    }
}
