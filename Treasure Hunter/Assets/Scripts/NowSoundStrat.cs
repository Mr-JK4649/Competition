using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NowSoundStrat : MonoBehaviour
{

    private void Start()
    {
        //シングルトンのためのコード
        
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleScene":
                BgmManager.Instance.Play("Hold_Up");
                break;

            case "MainScene":
                BgmManager.Instance.Play("inner_flame");

                break;

            case "ResultScene":
                BgmManager.Instance.Play("Sparkle");
                break;
        }
    }
}
