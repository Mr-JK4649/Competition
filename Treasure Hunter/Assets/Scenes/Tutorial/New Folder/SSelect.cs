using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSelect : MonoBehaviour
{
    [SerializeField] private Text[] image;
    [SerializeField] private Image[] stg;

    [SerializeField] private string[] name = new string[3];

    private void Update()
    {
        string th = this.GetComponent<Text>().text;

        if (th == name[0]) stg[0].enabled = true;
        else stg[0].enabled = false;

        if (th == name[1]) stg[1].enabled = true;
        else stg[1].enabled = false;

        if (th == name[2]) stg[2].enabled = true;
        else stg[2].enabled = false;
    }
}
