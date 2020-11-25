using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatCameraScripts : MonoBehaviour
{
    private enum CameraVec { 
        front,
        right,
        left,
        back
    }

    [SerializeField] private CameraVec cameraVec;

    [SerializeField] private bool cameraFlg;

    private void Start()
    {
        CameraSwithing();
    }

    private void FixedUpdate()
    {
        CameraSwithing();
    }

    void CameraSwithing() {
        string vec = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>().autoRunVec;

        if (vec == cameraVec.ToString())
        {
            this.GetComponent<Camera>().enabled = true;
            this.transform.GetChild(1).GetComponent<Camera>().enabled = true;
        }
        else
        {
            this.GetComponent<Camera>().enabled = false;
            this.transform.GetChild(1).GetComponent<Camera>().enabled = false;
        }
    }
}
