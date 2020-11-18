using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GoodCurveScripts : MonoBehaviour
{
    GameObject cm1;
    GameObject cm2;


    private PlayerMoveSystem plms;
    public bool flg = false;
    private float size;
    private string objName;
    private float pl_oriSpd;
    


    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        size = this.GetComponent<BoxCollider>().size.y;
        objName = this.name;
        pl_oriSpd = plms.playerOriginSpeed;
        //plms.autoRunVec = this.name;

        //以下カメラ操作
        cm1 = GameObject.Find("CM vcam1");
        cm2 = GameObject.Find("CM vcam2");
    }

    private void OnTriggerEnter(Collider other)
    {
        int priry1 = cm1.GetComponent<CinemachineVirtualCamera>().Priority;
        int priry2 = cm2.GetComponent<CinemachineVirtualCamera>().Priority;

            if (priry1 >= priry2)
            {
                cm2.GetComponent<CinemachineVirtualCamera>().Priority = cm1.GetComponent<CinemachineVirtualCamera>().Priority + 1;
                Debug.Log("カメラ２");
            }

            if (priry2 >= priry1)
            {
                cm1.GetComponent<CinemachineVirtualCamera>().Priority = cm2.GetComponent<CinemachineVirtualCamera>().Priority + 1;
                Debug.Log("カメラ１");
            }

        flg = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (Mathf.Abs(plms.runSpd.x) < 5) plms.runSpd.x = 0;
        if (Mathf.Abs(plms.runSpd.y) < 5) plms.runSpd.y = 0;
        if (Mathf.Abs(plms.runSpd.z) < 5) plms.runSpd.z = 0;
        if (Mathf.Abs(plms.runSpd.x) > pl_oriSpd / 2) plms.runSpd.x = plms.RunSpeed * (objName == "left" ? -1 : 1);
        if (Mathf.Abs(plms.runSpd.z) > pl_oriSpd / 2) plms.runSpd.z = plms.RunSpeed * (objName == "back" ? -1 : 1);
        plms.autoRunVec = this.name;
        flg = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") CalcPlayerSpd(this.name);
        Debug.Log("ステイ");
    }

    void CalcPlayerSpd(string thisname){

        float diff = (plms.runSpd.z / (pl_oriSpd * plms.accelForce)) * 10;

        switch (thisname) {
            case "front":
            case "back":
                plms.runSpd.x = Mathf.MoveTowards(plms.runSpd.x, 0f, diff);
                plms.runSpd.z += thisname == "front" ? diff : -diff;
                break;

            case "right":
            case "left":
                plms.runSpd.z = Mathf.MoveTowards(plms.runSpd.z, 0f, diff);
                plms.runSpd.x += thisname == "right" ? diff : -diff;
                break;
        }

        
    }
}
