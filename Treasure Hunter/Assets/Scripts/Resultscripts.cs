using UnityEngine;
using UnityEngine.SceneManagement;
public class Resultscripts : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cont_A"))
        {
            SceneManager.LoadScene("TitleScene");//mainシーンをロードする
        }
    }
}
