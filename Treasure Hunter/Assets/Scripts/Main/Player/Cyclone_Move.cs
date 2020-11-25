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
        if (time >= plms.dodgeTime - 0.2f)
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

                    case "UpSpiral": spiralNum = 0; animator.SetTrigger("Spiral_UP"); break;      //上にドッジ

                    case "LeftSpiral": spiralNum = 1; animator.SetTrigger("Spiral_L"); break;      //左にドッジ

                    case "RightSpiral": spiralNum = 2; animator.SetTrigger("Spiral_R"); break;      //右にドッジ

                    case "DownSpiral": spiralNum = 3; animator.SetTrigger("Spiral_Down"); break;      //下にドッジ

                    case "AccelSpiral":                             //前方に加速
                        plms.accelCount = plms.accelTime;
                        if (plms.accelCount > 0)
                            plms.StopCoroutine("SlowInitSpeed");
                        plms.RunSpeed = plms.playerOriginSpeed * plms.accelForce;
                        animator.SetTrigger("Spiral_UP");
                        break;

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
