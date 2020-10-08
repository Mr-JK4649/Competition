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

    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        Destroy(this.gameObject, 1.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (this.gameObject.name) {
            case "UpSpiral":
                plms.SpiralMoveNum = 0;
                break;

            case "LeftSpiral":
                plms.SpiralMoveNum = 1;
                break;

            case "RightSpiral":
                plms.SpiralMoveNum = 2;
                break;

            case "DownSpiral":
                plms.SpiralMoveNum = 3;
                break;

            case "AccelSpiral":
                plms.SpiralMoveNum = 4;
                break;
        }

        Destroy(this.gameObject, 0.2f);
    }

}
