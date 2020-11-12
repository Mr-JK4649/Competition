using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObj : MonoBehaviour
{

    public PlayerMoveSystem playermove;
    [SerializeField] private float backgroundSpeed;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = -backgroundSpeed * playermove.GetPlayerVelocity();
    }
}
