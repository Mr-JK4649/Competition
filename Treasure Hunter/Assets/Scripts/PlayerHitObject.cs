using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerHitObject : MonoBehaviour
{
    private PlayerMoveSystem plms;                  //プレイヤーの移動を司るスクリプト
    private GameManager gm;                         //ゲームの進行などを司るスクリプト

    [SerializeField] bool blockHitFlg = false;      //障害物に当たるかのフラグ
    
    [SerializeField] GameObject bom;                //爆発

    

    private void Start()
    {

        plms = this.GetComponent<PlayerMoveSystem>();
        gm = GameObject.Find("GameSystem").GetComponent<GameManager>();
        
    }

    private void Update()
    {
        ////障害物に当たった時の処理
        //if (deathFlg)
        //{
        //    if(blockHitFlg)
        //        plms.Wiz_RB.velocity = new Vector3(0f, -9.81f, 0f);

        //}
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (blockHitFlg)
        {
            switch (other.gameObject.tag) {
                case "Block":
                    if (gm.aaa <= 0)
                    {
                        //Instantiate(bom, this.gameObject.transform);
                        plms.accelCount = 0;
                        plms.RunSpeed = 0f;
                        gm.GameOver();
                    }
                    break;

                case "Wall":
                    Debug.Log("壁に衝突");
                    break;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        switch (other.gameObject.tag)
        {
            case "Coin":
                //other.gameObject.SetActive(false);
                //Destroy(other.gameObject);
                //sco += coinsScore;
                //coinCount += 1;
                //Score.text = sco.ToString("0000000") + " 点\n" + coinCount.ToString("000") + " 枚";
                //CoinBar.value += 0.1f;
                break;

            case "CheckPoint":
                plms.lastCheckPoint = other.gameObject.transform.position;
                plms.lastVec = plms.autoRunVec;
                break;

            case "Goal":
                gm.GameClearText.enabled = true;
                Destroy(GameObject.Find("Wizard"));
                break;

            case "Curve":
                plms.autoRunVec = other.gameObject.name;
                break;

            case "correct":
                GameObject.Find("LanePanel").transform.position = other.gameObject.transform.position;
                plms.Wiz_TF.position = plms.currentLane.transform.position;
                break;
        }


    }
    
}
