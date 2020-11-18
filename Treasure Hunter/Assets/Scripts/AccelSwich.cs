using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelSwich : MonoBehaviour
{

    public GameObject sl;

    //加速してる状態で集中線を描画する
    private void FixedUpdate()
    {
        if (GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>().accelCount > 0)
        {
            sl.SetActive(true);
        }
        else
        {
            sl.SetActive(false);
        }
    }

}
