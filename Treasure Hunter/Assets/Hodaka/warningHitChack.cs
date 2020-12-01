using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warningHitChack : MonoBehaviour
{
    public Image warningImage;

    private void Start()
    {
        //初期状態を非表示にセット
        warningImage.enabled = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            Debug.Log("Hit"); // ログを表示する
            warningImage.enabled = true;//画像表示
        }

    }
    void OnTriggerExit(Collider other)
    {
        warningImage.enabled = false;//画像表示
    }
}
