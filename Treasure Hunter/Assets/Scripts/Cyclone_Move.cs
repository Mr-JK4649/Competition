/****************************
 
 プレイヤーを飛ばす処理(風の渦に使う)
 * 各パブリックの座標変数には各座標に飛ぶ勢いを入れる
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone_Move : MonoBehaviour
{

    public Vector3 dodgeVec;        //ドッジ回避用のベクトル3


    //ベクトルを設定する関数
    public void setDodgeVec(float x,float y,float z) {
        dodgeVec = new Vector3(x, y, z);
    }

    //ベクトルを初期化する関数
    public void InitDodgeVec() {
        dodgeVec = Vector3.zero;
    }

    //何かのオブジェクトに接触したら(Trriger)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(dodgeVec, ForceMode.Impulse);
            Destroy(this.gameObject, 0.2f);
        }
    }

    //触れなかったオブジェクトは1秒後に自動的に削除
    private void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }
}
