/****************************
 
 プレイヤーを飛ばす処理(風の渦に使う)
 * 各パブリックの座標変数には各座標に飛ぶ勢いを入れる
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone_Move : MonoBehaviour
{

    private PlayerMoveSystem plms;
    private int spiralNum = -1;

    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        Destroy(this.gameObject, plms.dodgeTime);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            

            switch (this.gameObject.name)
            {

                case "UpSpiral":
                    spiralNum = 0;
                    break;

                case "LeftSpiral":
                    spiralNum = 1;
                    break;

                case "RightSpiral":
                    spiralNum = 2;
                    break;

                case "DownSpiral":
                    spiralNum = 3;
                    break;

                case "AccelSpiral":
                    plms.accelTime = 60;
                    plms.RunSpeed = plms.playerOriginSpeed * plms.accelForce;
                    break;

            }

            if(spiralNum != -1)
                plms.StartCoroutine(plms.Mover(plms.Wiz_TF.position, plms.lanePos[spiralNum], plms.dodgeSpeed, plms.dodgeTime));
            
        }
    }



}
