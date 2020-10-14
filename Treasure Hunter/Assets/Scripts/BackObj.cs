using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObj : MonoBehaviour
{

    public PlayerMoveSystem playermove;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = -4 * playermove.GetPlayerVelocity();
    }
}
