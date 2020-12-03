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
    private Animator animator;

    private float time = 0f;
    private bool flg = true;

    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        Destroy(this.gameObject, 3.0f);
        animator = GameObject.Find("Player02").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (time >= plms.dodgeTime / 5)
        {
            this.gameObject.tag = "Untagged";
            this.transform.GetChild(0).tag = "Untagged";
        }
        time += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            if (flg)
            {

                

                switch (this.gameObject.name)
                {

                    case "UpSpiral": spiralNum = 0; animator.PlayInFixedTime("SpiralShot_Up", 0); break;      //上にドッジ

                    case "LeftSpiral": spiralNum = 1; animator.PlayInFixedTime("SpiralShot_L", 0); break;      //左にドッジ

                    case "RightSpiral": spiralNum = 2; animator.PlayInFixedTime("SpiralShot_R", 0); break;      //右にドッジ

                    case "DownSpiral": spiralNum = 3; animator.PlayInFixedTime("SpiralShot_Down", 0); break;      //下にドッジ

                }

                flg = false;
            }

            
            

            //移動のフラグが立ったら移動コルーチン起動
            if (spiralNum != -1){
                plms.StartCoroutine(plms.Mover(plms.Wiz_TF.position, plms.lanePos[spiralNum], plms.dodgeSpeed, plms.dodgeTime));
                spiralNum = -1;
            }
            
        }
    }



}
