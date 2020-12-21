using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cont_B"))
        {
            //SceneManager.LoadScene("TitleScene");//mainシーンをロードする
            FadeManager.Instance.LoadScene("TitleScene", 1.0f);
        }
    }

    public void OnClickStartButton()
    {
        //SceneManager.LoadScene("MainScene");
        FadeManager.Instance.LoadScene("MainScene", 1.0f);
    }
}
