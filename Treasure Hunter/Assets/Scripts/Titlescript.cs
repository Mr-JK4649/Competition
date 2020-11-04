using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlescript : MonoBehaviour
{

    // メモ：今のところ強制起動
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainScene");//mainシーンをロードする
        }
    }
}
