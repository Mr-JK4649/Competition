using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{

    [SerializeField] private float camOrgpos_X; 
    [SerializeField] private float camOrgpos_Y;
    // Start is called before the first frame update
    void Start()
    {
        camOrgpos_X = this.GetComponent<Transform>().position.x;
        camOrgpos_Y = this.GetComponent<Transform>().position.y;
    }

    void FixedUpdate()
    {
        //nemuti
        //this.GetComponent<Transform>().position = new Vector2(camOrgpos_X, camOrgpos_Y);
        this.GetComponent<Transform>().position = new Vector3(camOrgpos_X, camOrgpos_Y, 0.0f);
    }
}
