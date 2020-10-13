using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public PlayerMoveSystem pms;            //プレイヤーのスクリプト
    public bool FixedCamera = false;        //カメラを定点化するやつ
    private bool FixedCameraFlg = false;    //カメラ固定スペース用のフラグ

    private void Update()
    {
        Transform pl = GameObject.Find("Wizard").GetComponent<Transform>();

        if (FixedCamera){
            
            this.gameObject.GetComponent<Transform>().position = new Vector3(pms.Origin_Pos.x, pms.Origin_Pos.y + 4.5f, pl.position.z - 20.5f);
        }
        else {
            this.gameObject.GetComponent<Transform>().position = new Vector3(pl.position.x, pl.position.y + 1.5f, pl.position.z - 5.5f);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            if (FixedCameraFlg == false) FixedCamera = !FixedCamera;
            FixedCameraFlg = true;
        }
        else {
            FixedCameraFlg = false;
        }
    }
}
