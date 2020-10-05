/****************************
 
 プレイヤーを飛ばす処理(風の渦に使う)
 * 各パブリックの座標変数には各座標に飛ぶ勢いを入れる
 *****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone_Move : MonoBehaviour
{
    public float x, y, z;
    void Cyclone_Init()
    {
        x = 0f;
        y = 0f;
        z = 0f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z), ForceMode.Impulse);
            Cyclone_Init();
        }
    }
}
