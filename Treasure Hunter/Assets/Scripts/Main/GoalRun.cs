using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRun : MonoBehaviour
{
    public PlayerMoveSystem PlayerMove;
    public GameObject stopObject;
    public bool goalStop = false;
    private Vector3 point;
    float StartPlayer_z;

    void Start()
    {
        StartPlayer_z = GameObject.Find("Wizard").transform.position.z;
        point = stopObject.GetComponent<Transform>().position;
    }

    private void FixedUpdate()
    {
        float Player_z = GameObject.Find("Wizard").transform.position.z;
        if (goalStop == false)
        {
            this.GetComponent<Rigidbody>().velocity = PlayerMove.GetPlayerVelocity();
        }
        else
        {
            this.GetComponent<Transform>().position = new Vector3(point.x, this.GetComponent<Transform>().position.y, point.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GoalStop")
        {
            goalStop = true;
            Debug.Log("ヒット");
            //this.enabled = false;
        }
    }
    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "GoalStop")
        {
            //goalStop = true;
            this.enabled = false;
        }
    }*/
}
