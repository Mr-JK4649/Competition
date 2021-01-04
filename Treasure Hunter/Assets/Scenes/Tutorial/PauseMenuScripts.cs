using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScripts : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;       //ポーズメニューまとめたやつ

    [SerializeField] private Image[] _pauseSelect = new Image[2];
    [SerializeField] private Image[] _pauseTitle = new Image[2];

    private Text openMenuText;

    public bool isMenu = false;
    public int menuSelectNum = 0;      //メニュー選択のやつ

    private void Start()
    {
        pauseMenu.SetActive(false);
        openMenuText = GameObject.Find("OpenMenuText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && Time.timeScale > 0) isMenu = true;

        if (isMenu)
        {

            if (Time.timeScale < 1 && !pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                openMenuText.color = new Color(1, 1, 1, 1);
                openMenuText.text = "STARTボタン：CLOSE";
                menuSelectNum = 0;
            }
            if (Time.timeScale > 0 && pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                openMenuText.color = new Color(1, 1, 1, 0.8f);
                openMenuText.text = "STARTボタン： MENU";
                isMenu = false;
            }

            if (pauseMenu.activeSelf)
            {
                if (Input.GetAxis("Vertical") > 0f) menuSelectNum = 0;
                if (Input.GetAxis("Vertical") < 0f) menuSelectNum = 1;


                if (menuSelectNum == 0)
                {
                    _pauseSelect[0].enabled = true;
                    _pauseSelect[1].enabled = false;
                    _pauseTitle[0].enabled = true;
                    _pauseTitle[1].enabled = false;
                }
                else
                {
                    _pauseSelect[0].enabled = false;
                    _pauseSelect[1].enabled = true;
                    _pauseTitle[0].enabled = false;
                    _pauseTitle[1].enabled = true;
                }

                if (Input.GetButtonDown("Cont_A"))
                {
                    switch (menuSelectNum)
                    {
                        case 0:
                            SceneManager.LoadScene("SelectScene");
                            break;
                        case 1:
                            SceneManager.LoadScene("TitleScene");
                            break;
                    }
                    Time.timeScale = 1;
                }
            }

        }

    }

    

}
