using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPositioningSystem : MonoBehaviour
{
    public PlayerMoveSystem plms;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Lane":
                plms.currentLane = other.gameObject;
                break;
        }
    }


}
