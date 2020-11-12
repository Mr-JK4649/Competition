using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    //Coinをいい感じに回す奴
    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0));
    }
}
