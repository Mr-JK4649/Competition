using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardStartButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        //SceneManager.LoadScene("MainScene");
        FadeManager.Instance.LoadScene("HardStageEdit", 1.0f);
    }
}
