using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObj : MonoBehaviour
{

    public PlayerMoveSystem playermove;
    [SerializeField] private float backgroundSpeed;


    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = -backgroundSpeed * playermove.GetPlayerVelocity();
    }
}
