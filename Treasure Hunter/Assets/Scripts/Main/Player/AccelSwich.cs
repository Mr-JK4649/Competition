using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelSwich : MonoBehaviour
{

    public ParticleSystem sl;           //集中線
    public GameObject accelSpriral;
    private PlayerMoveSystem plms;      //プレイヤーの移動とかを司るやつ
    private Camera fov;                 //fovを操作するやつ

    [Header("fovの最大値"),Range(101,150)]
    public float fovMax;

    [Header("fovの最小値"),Range(0,100)]
    public float fovMin;

    [Header("fovの現在値")]
    public float currentFov;            //現在のfov

    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        fov = this.GetComponent<Camera>();
    }

    //加速してる状態で集中線を描画する
    private void FixedUpdate()
    {
        var main = sl.main;
        float fovNum = fov.fieldOfView;

        if (plms.accelCount > 0)
        {
            sl.Play();
            accelSpriral.SetActive(true);
            if (fovNum < fovMax) fovNum += 3f;
            if (fovNum > fovMax) fovNum = fovMax;
            main.simulationSpeed = 3f;
        }
        else if (plms.RunSpeed > plms.playerOriginSpeed)
        {
            
            if (fovNum > fovMin) fovNum -= 0.2f;
            if (fovNum < fovMin) fovNum = fovMin;
            main.simulationSpeed = 2f;
        }
        else {
            //パーティクル
            sl.Stop();
            accelSpriral.SetActive(false);
        }

        //fov更新の反映と確認用変数への格納
        fov.fieldOfView = fovNum;
        currentFov = fov.fieldOfView;
        
    }

}
