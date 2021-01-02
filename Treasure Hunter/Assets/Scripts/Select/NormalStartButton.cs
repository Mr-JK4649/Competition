using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStartButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        //SceneManager.LoadScene("MainScene");
        FadeManager.Instance.LoadScene("NormalStageEdit", 1.0f);
    }
}
