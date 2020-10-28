using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    

    [SerializeField] private int SpinNum;       //回転数
    [SerializeField] private float SpinTime;    //回転にかかる時間

    private Quaternion originAngle;
    private PlayerMoveSystem plms;
    private bool spinFlg = true;

    private void Start()
    {
        originAngle = this.gameObject.transform.rotation;
        plms = this.GetComponent<PlayerMoveSystem>();
    }

    private void Update()
    {
        if (plms.accelCount > 0)
        {
            if(spinFlg)
                StartCoroutine("AccelRotate");
            spinFlg = false;
        }

    }

    IEnumerator AccelRotate()
    {
        float aps = 360 * SpinNum / SpinTime;
        float timer = 0.0f;
        while (timer <= SpinTime)
        {

            this.gameObject.transform.Rotate(0f,aps * Time.deltaTime,0f);

            timer += Time.deltaTime;
            yield return null;
        }

        this.transform.rotation = originAngle;
        spinFlg = true;
    }

}