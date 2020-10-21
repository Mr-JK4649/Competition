using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotateAngle;
    private Quaternion originAngle;

    private void Start()
    {
        originAngle = this.gameObject.transform.rotation;
    }

    private void Update()
    {
        if (this.GetComponent<PlayerMoveSystem>().accelCount > this.GetComponent<PlayerMoveSystem>().accelTime / 10)
        {
            this.gameObject.transform.Rotate(rotateAngle);
        }
        else if (this.GetComponent<PlayerMoveSystem>().accelCount > 0)
        {
            if(this.gameObject.transform.rotation != originAngle)
                this.gameObject.transform.Rotate(-rotateAngle);
        }

    }
}
