using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalToResult : MonoBehaviour
{
    public void ToResult() {
        GameObject.Find("GameSystem").GetComponent<GameManager>().GameDataSave();
        SceneManager.LoadScene("ResultScene");
    }
    
}
