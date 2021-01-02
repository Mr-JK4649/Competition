using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSelect : MonoBehaviour
{
    [SerializeField] private Text[] image;
    [SerializeField] private Image[] stg;

    private void Update()
    {
        string th = this.GetComponent<Text>().text;

        if (image[0].text == th) stg[0].enabled = true;
        else stg[0].enabled = false;

        if (image[1].text == th) stg[1].enabled = true;
        else stg[1].enabled = false;

        if (image[2].text == th) stg[2].enabled = true;
        else stg[2].enabled = false;
    }
}
