using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePanelFollow : MonoBehaviour
{

    private Transform pl;
    private PlayerMoveSystem plms;

    private void Start()
    {
        pl = GameObject.Find("Wizard").GetComponent<Transform>();
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
    }

    private void Update()
    {

        switch(plms.autoRunVec) {
            case "front":
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                this.gameObject.GetComponent<Transform>().position = new Vector3(this.transform.position.x, this.transform.position.y, pl.position.z);
                break;

            case "right":
                this.transform.localRotation = Quaternion.Euler(0, 90, 0);
                this.gameObject.GetComponent<Transform>().position = new Vector3(pl.position.x, this.transform.position.y, this.transform.position.z);
                break;

            case "left":
                this.transform.localRotation = Quaternion.Euler(0, -90, 0);
                this.gameObject.GetComponent<Transform>().position = new Vector3(pl.position.x, this.transform.position.y, this.transform.position.z);
                break;

            case "back":
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
                this.gameObject.GetComponent<Transform>().position = new Vector3(this.transform.position.x, this.transform.position.y, pl.position.z);
                break;
        }
        
    }


}
