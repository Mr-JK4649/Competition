using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GoodCurveScripts : MonoBehaviour
{
    GameObject cm1;
    GameObject cm2;

    

    private PlayerMoveSystem plms;
    private float size;
    private string objName;
    private float pl_oriSpd;


    private void Start()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        size = this.GetComponent<BoxCollider>().size.y / 2;
        objName = this.name;
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
}
