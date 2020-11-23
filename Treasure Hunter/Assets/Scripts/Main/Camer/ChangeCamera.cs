using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    GameObject cm1;
    GameObject cm2;

    void Start()
    {
        cm1 = GameObject.Find("CM vcam1");
        cm2 = GameObject.Find("CM vcam2");
    }
    
    void Update()
    {  
        //this.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        int priry1 = cm1.GetComponent<CinemachineVirtualCamera>().Priority;
        int priry2 = cm2.GetComponent<CinemachineVirtualCamera>().Priority;
        if (GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>().accelCount > 0)
        {
          

            if(priry1 >= priry2)
            {
                cm2.GetComponent<CinemachineVirtualCamera>().Priority = cm1.GetComponent<CinemachineVirtualCamera>().Priority + 1;
                Debug.Log("カメラ２");
            }
            

        } 
        else
        {
            if (priry2 >= priry1)
            {
                cm1.GetComponent<CinemachineVirtualCamera>().Priority = cm2.GetComponent<CinemachineVirtualCamera>().Priority + 1;
                Debug.Log("カメラ１");
            }
        }
    }
}
