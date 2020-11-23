using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoraPoint : MonoBehaviour
{
 
    private void FixedUpdate()
    {
        this.GetComponent<Transform>().position = GameObject.Find("Wizard").transform.position;
    }
}
