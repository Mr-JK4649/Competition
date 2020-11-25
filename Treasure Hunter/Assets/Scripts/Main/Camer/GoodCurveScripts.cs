using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GoodCurveScripts : MonoBehaviour
{
    GameObject cm1;
    GameObject cm2;
    GameObject cm3;


    private GameObject[] MainCamera = new GameObject[4];
    private Camera[] cam = new Camera[4];
    [SerializeField] private float[] mainCameraRotatebuf_Y = new float[4];
    [SerializeField] private int onCamraBuf; // 起動していたカメラを保存しておく変数
    private PlayerMoveSystem plms;
    private float size;
    private string objName;
    private float pl_oriSpd;
    
    public enum CameraType{
        FRONT,
        BACK,
        RIGHT,
        LEFT,
        MAX
    }

    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        size = this.GetComponent<BoxCollider>().size.y / 2;
        objName = this.name;
        //plms.autoRunVec = this.name;
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

        //mainCameraRotatebuf[(int)CameraType.FRONT] = MainCamera[(int)CameraType.FRONT].GetComponent<Transform>();   // カメラのトランスフォームをバックアップ
        //mainCameraRotatebuf[(int)CameraType.BACK] = MainCamera[(int)CameraType.BACK].GetComponent<Transform>();     // カメラのトランスフォームをバックアップ
        //mainCameraRotatebuf[(int)CameraType.LEFT] = MainCamera[(int)CameraType.LEFT].GetComponent<Transform>();     // カメラのトランスフォームをバックアップ
        //mainCameraRotatebuf[(int)CameraType.RIGHT] = MainCamera[(int)CameraType.RIGHT].GetComponent<Transform>();   // カメラのトランスフォームをバックアップ

        cm1 = GameObject.Find("CM vcam1");
        cm2 = GameObject.Find("CM vcam2");
        cm3 = GameObject.Find("CM vcam3");

    }

    private void OnTriggerEnter(Collider other)
    {

        //int priry1 = cm1.GetComponent<CinemachineVirtualCamera>().Priority;
        //int priry2 = cm2.GetComponent<CinemachineVirtualCamera>().Priority;

        //if (priry1 >= priry2)
        //{
        //    cm2.GetComponent<CinemachineVirtualCamera>().Priority = cm1.GetComponent<CinemachineVirtualCamera>().Priority + 1;
        //    Debug.Log("カメラ２");
        //}

        //if (priry2 >= priry1)
        //{
        //    cm1.GetComponent<CinemachineVirtualCamera>().Priority = cm2.GetComponent<CinemachineVirtualCamera>().Priority + 1;
        //    Debug.Log("カメラ１");
        //}
       
        VirtualCameraOn();
        CameraSwitching();

        pl_oriSpd = plms.playerOriginSpeed;
        plms.curveFlg = true;

    }

    private void OnTriggerExit(Collider other)
    {
        //if (Mathf.Abs(plms.runSpd.x) < pl_oriSpd / 2) plms.runSpd.x = 0;
        //if (Mathf.Abs(plms.runSpd.y) < pl_oriSpd / 2) plms.runSpd.y = 0;
        //if (Mathf.Abs(plms.runSpd.z) < pl_oriSpd / 2) plms.runSpd.z = 0;
        //if (Mathf.Abs(plms.runSpd.x) > pl_oriSpd / 2) plms.runSpd.x = plms.RunSpeed * (objName == "left" ? -1 : 1);
        //if (Mathf.Abs(plms.runSpd.z) > pl_oriSpd / 2) plms.runSpd.z = plms.RunSpeed * (objName == "back" ? -1 : 1);

        switch (this.name)
        {
            case "front":
            case "back":
                SetRunSpeedExit(new Vector3(0f, 0f, this.name == "back" ? -plms.RunSpeed : plms.RunSpeed));
                break;

            case "right":
            case "left":
                SetRunSpeedExit(new Vector3(this.name == "left" ? -plms.RunSpeed : plms.RunSpeed, 0f, 0f));
                break;
        }

        plms.autoRunVec = this.name;
        plms.curveFlg = false;
        VirtualCameraOff();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") CalcPlayerSpd(this.name,(this.transform.position - other.transform.position));
        Debug.Log("ステイ");
    }


    void CalcPlayerSpd(string thisname,Vector3 dis){

        //float diff = (plms.runSpd.z / (plms.RunSpeed * plms.accelForce)) * 10;
        float diff = (plms.runSpd.x + plms.runSpd.z) / pl_oriSpd;

        if (plms.RunSpeed != pl_oriSpd) diff *= 5;

        switch (thisname) {
            case "front":
            case "back":

                plms.runSpd.x = Mathf.MoveTowards(plms.runSpd.x, 0f, diff);
                plms.runSpd.z += thisname == "front" ? diff : -diff;
                break;

            case "right":
            case "left":
                float mana = plms.RunSpeed / pl_oriSpd;

                //plms.runSpd.z = Mathf.MoveTowards(plms.runSpd.z, 0f, 1f * mana);
                //plms.runSpd.x += thisname == "right" ? diff : -diff
                plms.runSpd.z = plms.RunSpeed * (dis.z / size);
                plms.runSpd.x = plms.RunSpeed - plms.runSpd.z;
                
                break;
        }

        
    }

    void SetRunSpeedExit(Vector3 rs) {

        plms.runSpd = rs;

    }

    //カメラの切り替え
    void CameraSwitching()
    {

        cm1.GetComponent<CinemachineVirtualCamera>().Priority = objName == "front" ? 1 : 0;
        cm2.GetComponent<CinemachineVirtualCamera>().Priority = objName == "right" ? 1 : 0;
        cm3.GetComponent<CinemachineVirtualCamera>().Priority = objName == "left" ? 1 : 0;
        //cm4.GetComponent<CinemachineVirtualCamera>().Priority = objName == "back" ? 1 : 0;




    }

    void VirtualCameraOn()
    {
        
        for(int i=0;i<(int)CameraType.MAX;i++)
        {
            if(cam[i].enabled == true)
            {
                onCamraBuf = i;
                MainCamera[i].GetComponent<CinemachineBrain>().enabled = true;
            }
        }
        
    }

    void VirtualCameraOff()
    {
        
        
        MainCamera[onCamraBuf].GetComponent<CinemachineBrain>().enabled = false;
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


