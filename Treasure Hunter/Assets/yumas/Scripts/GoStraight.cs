using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoStraight : MonoBehaviour
{

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, 100f), ForceMode.Impulse);
        }
    }
}
