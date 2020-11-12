using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoraPoint : MonoBehaviour
{

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().position = GameObject.Find("Wizard").transform.position;
    }
}
