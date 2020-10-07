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
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == ("Block"))
        {
            //this.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            int priry1 = cm1.GetComponent<CinemachineVirtualCamera>().Priority;
            int priry2 = cm2.GetComponent<CinemachineVirtualCamera>().Priority;

            if(priry1 >= priry2)
            {
                cm2.GetComponent<CinemachineVirtualCamera>().Priority = cm1.GetComponent<CinemachineVirtualCamera>().Priority + 1;
            }
            

        }
    }
}
