using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameBootScripts : MonoBehaviour
{
    private PlayerMoveSystem plms;
    private CinemachineVirtualCamera vc1;
    private CinemachineVirtualCamera vc2;

    // Start is called before the first frame update
    void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();

        //カメラの初期化
        CameraInit();

    }


    void CameraInit() {
        vc1 = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        vc2 = GameObject.Find("CM vcam2").GetComponent<CinemachineVirtualCamera>();

        vc1.Follow = GameObject.Find("WizardCameraPoint").transform;
        vc1.LookAt = GameObject.Find("Wizard").transform;

        vc2.Follow = GameObject.Find("WizardCameraPoint").transform;
        vc2.LookAt = GameObject.Find("WizardCameraPoint").transform;
    }

}
