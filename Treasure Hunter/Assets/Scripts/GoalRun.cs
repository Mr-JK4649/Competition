using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRun : MonoBehaviour
{
    public PlayerMoveSystem PlayerMove;
    float StartPlayer_z;

    void Start()
    {
        StartPlayer_z = GameObject.Find("Wizard").transform.position.z;
    }

    void Update()
    {
        float Player_z = GameObject.Find("Wizard").transform.position.z;
        this.GetComponent<Rigidbody>().velocity = PlayerMove.GetPlayerVelocity();
    }
}
