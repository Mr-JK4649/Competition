using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {

            var color = other.GetComponent<Renderer>().material.color;

            color.a = 1.0f;

            other.GetComponent<Renderer>().material.color = color;
        }
    }
}
