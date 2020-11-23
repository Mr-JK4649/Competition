using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class FadeOut : MonoBehaviour
{

    public float startDistance = 70;
    private float hiddenDistance = 25;
    private GameObject v_camera;

    private int coolTime = 90;
    private int time = 0;

    private void Start()
    {
        v_camera = GameObject.Find("Wizard");
    }

    private void FixedUpdate()
    {
        //V_cameraとオブジェクトの距離を変数dに入れる
        var d = Vector3.Distance(v_camera.transform.position, transform.position);

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
