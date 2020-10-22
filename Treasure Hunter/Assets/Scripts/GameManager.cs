using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //ゲームオーバーした時の判定
    public void GameOver() {
        GameObject gameoverText = new GameObject("GameOverText");
        gameoverText.transform.parent = GameObject.Find("InStageUI").transform;
        gameoverText.transform.localPosition = Vector3.zero;
        gameoverText.AddComponent<Text>();
        Text gmt = gameoverText.GetComponent<Text>();
        gmt.horizontalOverflow = HorizontalWrapMode.Overflow;
        gmt.verticalOverflow = VerticalWrapMode.Overflow;
        gmt.text = "GameOver\n[r] key to Retry";
        gmt.fontSize = 40;
        gmt.alignment = TextAnchor.MiddleCenter;
        gmt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        gmt.color = new Color(255f, 0f, 0f);
    }

    public void Retry() {
        SceneManager.LoadScene("MainScene");
    }

}
