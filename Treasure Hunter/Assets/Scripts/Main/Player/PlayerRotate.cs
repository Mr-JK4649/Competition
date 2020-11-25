//using System.Collections;
//using System.Collections.Generic;
//using System.Security.Cryptography;
//using UnityEngine;

//public class PlayerRotate : MonoBehaviour
//{
    

//    [SerializeField] private int SpinNum;       //回転数
//    [SerializeField] private float SpinTime;    //回転にかかる時間

//    private Quaternion originAngle;
//    private PlayerMoveSystem plms;
//    private bool spinFlg = true;

//    private void Start()
//    {
//        originAngle = this.gameObject.transform.rotation;
//        plms = this.GetComponent<PlayerMoveSystem>();
//    }


//    private void FixedUpdate()
//    {
//        if (plms.accelCount > 0)
//        {
//            if (spinFlg)
//                StartCoroutine("AccelRotate");
//            spinFlg = false;
//        }


//        AccelAngleChangeLaneMove();


//    }

//    //加速中に回転する処理
//    IEnumerator AccelRotate()
//    {
//        float aps = 360 * SpinNum / SpinTime;
//        float timer = 0.0f;
//        while (timer <= SpinTime)
//        {

//            this.gameObject.transform.Rotate(0f,0f, aps * Time.deltaTime);

//            timer += Time.deltaTime;
//            yield return null;
//        }

//        this.transform.rotation = originAngle;
//        spinFlg = true;
//    }


//    //加速の向き別のレーン移動時プレイヤー角度変更の処理
//    void AccelAngleChangeLaneMove() {

//        switch (plms.autoRunVec) {
//            case "front":               //前方
//                if (plms.diff.x != 0f)
//                {
//                    float addAngle;
//                    addAngle = plms.diff.x * -300f;

//                    plms.transform.localRotation = Quaternion.Euler(
//                        plms.Wiz_TF.localRotation.x,
//                        plms.Wiz_TF.localRotation.y,
//                        plms.Wiz_TF.localRotation.z + addAngle
//                    );
//                }
//                break;

//            case "right":               //右方
//                if (plms.diff.z != 0f)
//                {
//                    float addAngle;
//                    addAngle = plms.diff.z * 300f;

//                    plms.transform.localRotation = Quaternion.Euler(
//                        plms.Wiz_TF.localRotation.x,
//                        plms.Wiz_TF.localRotation.y + 90f,
//                        plms.Wiz_TF.localRotation.z + addAngle
//                    );
//                }
//                break;
//        }

//    }

//}