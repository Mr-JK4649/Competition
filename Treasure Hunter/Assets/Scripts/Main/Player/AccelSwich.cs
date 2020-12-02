using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelSwich : MonoBehaviour
{

    public GameObject sl;               //集中線
    private PlayerMoveSystem plms;      //プレイヤーの移動とかを司るやつ
    private Camera fov;                 //fovを操作するやつ

    public float currentFov;

    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        fov = this.GetComponent<Camera>();
    }

    //加速してる状態で集中線を描画する
    private void FixedUpdate()
    {
        if (plms.accelCount > 0)
        {
            sl.SetActive(true);
            fov.fieldOfView = 150;
        }
        else
        {
            sl.SetActive(false);
            if (fov.fieldOfView > 120) fov.fieldOfView -= 1;
            if (fov.fieldOfView < 120) fov.fieldOfView = 120;
        }

        currentFov = fov.fieldOfView;
    }

}
