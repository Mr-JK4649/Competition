using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodCurveScripts : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") { 
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag == "Player") { 
            
        }
    }

    private void FixedUpdate()
    {
        
    }
}
