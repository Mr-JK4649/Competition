using UnityEngine;
using System;
using Cinemachine;



public class GreatCurveScripts : MonoBehaviour
{
    private enum CameraType
    {
        FRONT,
        BACK,
        RIGHT,
        LEFT,
        MAX
    }

    private enum CurveVector
    {
        front,
        right,
        left,
        back
    };

    [SerializeField] private CurveVector curveVector;   //曲がる方向を選んで
    private PlayerMoveSystem plms;                      //プレイヤーの動きを司るやつ
    [SerializeField] private GameObject curveMid;       //カーブトンネルの中心
    [SerializeField] private GameObject curveEnd;       //カーブトンネルの出口
    private Vector3 curveEnd_pos;                       //カーブの終わり位置
    private Vector3 curveMid_pos;                       //カーブの中間位置  
    private Vector3 endPos;                             //プレイヤーの行き先

    [NonSerialized, Tooltip("Curve Speed")]                            
    public float curveSpeed;                                            //曲がる速度

    [NonSerialized]                                                     
    private Vector3 diff;                                               //距離を求めるのに使う

    [NonSerialized, Tooltip("Distance to Curve's End")]                
    public float magni;                                                 //ゴールまでの距離

    [NonSerialized, Tooltip("Original Distance")]                      
    public float originMagni;                                           //ゴールまでの最大距離

    [NonSerialized, Tooltip("Percentage of Distance to Curve's End")]  
    public float perDistanceToEnd;                                      //ゴールまでの割合


    //以下ゴリラカメラ様変数
    GameObject cm1;
    GameObject cm2;
    GameObject cm3;

    private GameObject[] MainCamera = new GameObject[4];
    private Camera[] cam = new Camera[4];
    private float[] mainCameraRotatebuf_Y = new float[4];
    private int onCamraBuf;                                             // 起動していたカメラを保存しておく変数

    private void Awake()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        curveMid_pos = curveMid.transform.position;
        curveEnd_pos = curveEnd.transform.position;

        CameraInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            plms.curveFlg = true;
            CalcMagnitude();
            originMagni = magni;

            VirtualCameraOn();
            CameraSwitching();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            plms.curveFlg = false;
            plms.autoRunVec = curveVector.ToString();

            //出た時に速度リセット
            switch (curveVector)
            {
                case CurveVector.front:
                case CurveVector.back:
                    plms.runSpd = new Vector3(0f, 0f, this.name == "back" ? -plms.RunSpeed : plms.RunSpeed);
                    break;

                case CurveVector.right:
                case CurveVector.left:
                    plms.runSpd = new Vector3(this.name == "left" ? -plms.RunSpeed : plms.RunSpeed, 0f, 0f);
                    break;
            }

            plms.StartCoroutine("SlowInitSpeed");


            VirtualCameraOff();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            curveSpeed = 1.5f * (plms.RunSpeed / plms.playerOriginSpeed);

            CalcMagnitude();

            //plms.Wiz_TF.position = Vector3.MoveTowards(plms.Wiz_TF.position, curveEnd_pos, curveSpeed);
            //plms.Wiz_TF.position = Vector3.MoveTowards(plms.Wiz_TF.position, curveMid_pos, curveSpeed);

            endPos = Vector3.Lerp(curveMid_pos, curveEnd_pos, (1f - perDistanceToEnd));
            plms.Wiz_TF.position = Vector3.MoveTowards(plms.Wiz_TF.position, endPos, curveSpeed);
        }
    }

    void CalcMagnitude() {
        diff = curveEnd_pos - plms.Wiz_TF.position;
        magni = diff.magnitude;
        perDistanceToEnd = magni / originMagni;
    }




    /*========================= カメラの関数 ==========================*/

    void CameraInit() {
        onCamraBuf = (int)CameraType.FRONT;
        //以下カメラ操作
        MainCamera[(int)CameraType.FRONT] = GameObject.Find("FollowCamera");    // メインカメラの情報
        MainCamera[(int)CameraType.BACK] = GameObject.Find("FollowCameraB");    // メインカメラBの情報
        MainCamera[(int)CameraType.LEFT] = GameObject.Find("FollowCameraL");    // メインカメラLの情報
        MainCamera[(int)CameraType.RIGHT] = GameObject.Find("FollowCameraR");    // メインカメラRの情報

        cam[(int)CameraType.FRONT] = MainCamera[(int)CameraType.FRONT].GetComponent<Camera>();    // メインカメラの情報
        cam[(int)CameraType.BACK] = MainCamera[(int)CameraType.BACK].GetComponent<Camera>();    // メインカメラBの情報
        cam[(int)CameraType.LEFT] = MainCamera[(int)CameraType.LEFT].GetComponent<Camera>();    // メインカメラLの情報
        cam[(int)CameraType.RIGHT] = MainCamera[(int)CameraType.RIGHT].GetComponent<Camera>();    // メインカメラRの情報

        mainCameraRotatebuf_Y[(int)CameraType.FRONT] = 0.0f;   // カメラのトランスフォームをバックアップ
        mainCameraRotatebuf_Y[(int)CameraType.BACK] = 180.0f;     // カメラのトランスフォームをバックアップ
        mainCameraRotatebuf_Y[(int)CameraType.LEFT] = -90.0f;     // カメラのトランスフォームをバックアップ
        mainCameraRotatebuf_Y[(int)CameraType.RIGHT] = 90.0f;   // カメラのトランスフォームをバックアップ


        cm1 = GameObject.Find("CM vcam1");
        cm2 = GameObject.Find("CM vcam2");
        cm3 = GameObject.Find("CM vcam3");
    }

    void CameraSwitching()
    {

        string curVec = curveVector.ToString();

        cm1.GetComponent<CinemachineVirtualCamera>().Priority = curVec == "front" ? 1 : 0;
        cm2.GetComponent<CinemachineVirtualCamera>().Priority = curVec == "right" ? 1 : 0;
        cm3.GetComponent<CinemachineVirtualCamera>().Priority = curVec == "left" ? 1 : 0;
        //cm4.GetComponent<CinemachineVirtualCamera>().Priority = curVec == "back" ? 1 : 0;




    }

    void VirtualCameraOn()
    {

        for (int i = 0; i < (int)CameraType.MAX; i++)
        {
            if (cam[i].enabled == true)
            {
                onCamraBuf = i;
                MainCamera[i].GetComponent<CinemachineBrain>().enabled = true;
            }
        }

    }

    void VirtualCameraOff()
    {


        for (int i = 0; i < (int)CameraType.MAX; i++)
        {
            if (cam[i].enabled == true)
            {
                onCamraBuf = i;
                MainCamera[i].GetComponent<CinemachineBrain>().enabled = false;
            }
        }
        SetBufCameraTrans();
    }

    void SetBufCameraTrans()
    {
        for (int i = 0; i < (int)CameraType.MAX; i++)
        {

            MainCamera[i].transform.rotation = Quaternion.Euler(MainCamera[i].transform.rotation.x, mainCameraRotatebuf_Y[i], MainCamera[i].transform.rotation.z);



        }
    }
}
