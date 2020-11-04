using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InhaleCoin : MonoBehaviour
{
    private Transform pl;
    private bool inhale = false;

    private float offset = 0;

    private void Start()
    {
        pl = GameObject.Find("Wizard").transform;
    }

    void Update()
    {
        //offset += 0.01f;

        if (inhale) {
            Vector3.Lerp(transform.position, pl.position, offset);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") inhale = true;
    }
}
