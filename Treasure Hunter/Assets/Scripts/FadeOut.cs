using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class FadeOut : MonoBehaviour
{

    public float startDistance = 70;
    public float hiddenDistance = 2;
    private GameObject v_camera;

    private void Start()
    {
        v_camera = GameObject.Find("CM vcam1");
    }
    private void Update()
    {
        var d = Vector3.Distance(v_camera.transform.position, transform.position);
        
        var color = this.GetComponent<Renderer>().material.color;
        
        if(d <= hiddenDistance)
        {
            color.a = 0.0f;
        }
        else if(d <= startDistance)
        {
            color.a = (d - hiddenDistance) / (startDistance - hiddenDistance);

        }
        else
        {
            color.a = 1.0f;
        }
        this.GetComponent<Renderer>().material.color = color;
    }
}
