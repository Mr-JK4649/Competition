using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePanelFollow : MonoBehaviour
{

    private void Update()
    {
        Transform pl = GameObject.Find("Wizard").GetComponent<Transform>();
        this.gameObject.GetComponent<Transform>().position = new Vector3(this.transform.position.x, this.transform.position.y, pl.position.z);
    }
}
