using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cont_A"))
        {
            SceneManager.LoadScene("MainScene");//mainシーンをロードする
        }
    }
}
