using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutoStart : MonoBehaviour
{
    enum tutoLevel { 
        UP,
        LEFT,
        RIGHT,
        DOWN,
        ACCEL,
        COIN,
        COINACCEL,
        ENDTUTORIAL
    }

    public PlayTutorial plTu;

    private void OnTriggerEnter(Collider other)
    {
        if (!plTu.skipTuto)
        {
            switch (other.name)
            {
                case "Tuto0":   ToTuto(other, (int)tutoLevel.LEFT);     break;

                case "Tuto1":   ToTuto(other, (int)tutoLevel.UP);       break;

                case "Tuto2":   ToTuto(other, (int)tutoLevel.RIGHT);    break;

                case "Tuto3":   ToTuto(other, (int)tutoLevel.DOWN);     break;

                case "Tuto4":   ToTuto(other, (int)tutoLevel.ACCEL);    break;

                case "Tuto5":   ToTuto(other, (int)tutoLevel.COIN);     break;

                case "Tuto6":   ToTuto(other, (int)tutoLevel.COINACCEL);break;

                case "Tuto7":   plTu.skipTuto = true; plTu.tutoNum = 99;break;
            }
        }
    }



    //================================ 以下　関数 =====================================

    void ToTuto(Collider col,int num) {
        plTu.kak = 0.9f;
        plTu.tutoNum = num;
        Time.timeScale = 0;
        col.GetComponent<BoxCollider>().enabled = false;
    }
}
