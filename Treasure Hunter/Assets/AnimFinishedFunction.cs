using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimFinishedFunction : MonoBehaviour
{
    public void ToResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
