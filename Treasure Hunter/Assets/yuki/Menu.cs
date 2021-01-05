using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI; // UIコンポーネントの使用
using KanKikuchi.AudioManager;

public class Menu : MonoBehaviour
{
	[SerializeField] EventSystem eventSystem;

	[SerializeField] Text text;
	GameObject selectedObj;

	Button stage1;
	Button stage2;
	Button stage3;

	private int SETime = 0;
	private bool SEflg = false;

	void Start()
	{
		// ボタンコンポーネントの取得
		stage1 = GameObject.Find("/Canvas/STAGE1").GetComponent<Button>();
		stage2 = GameObject.Find("/Canvas/STAGE2").GetComponent<Button>();
		stage3 = GameObject.Find("/Canvas/STAGE3").GetComponent<Button>();

		// 最初に選択状態にしたいボタンの設定
		stage1.Select();
	}

    void Update()
	{
        //if(stage1.transform.localScale.x > 1.0f)
        //      {
        //	SEManager.Instance.Play(SEPath.CURSOR5);
        //}
        //if (stage2.transform.localScale.x > 1.0f)
        //{
        //	SEManager.Instance.Play(SEPath.CURSOR5);
        //}
        //if (stage3.transform.localScale.x > 1.0f)
        //{
        //	SEManager.Instance.Play(SEPath.CURSOR5);
        //}


        if (Input.GetAxis("Vertical") != 0f && SEflg == false)
        {
            SEflg = true;
            SEManager.Instance.Play(SEPath.CURSOR5);
        }

        if (SEflg == true && Input.GetAxis("Vertical") == 0f)
        {
			SEflg = false;
        }
        //     if (Input.GetAxis("Vertical") != 0f && SEflg == false)
        //     {
        //         SEflg = true;
        //         SEManager.Instance.Play(SEPath.CURSOR5);
        //     }

        //     if (SEflg == true)
        //     {
        //if(SETime++ > 120)
        //         {
        //	SETime = 0;
        //	SEflg = false;
        //         }
        //     }

        // TryCatch文でNull回避
        try
		{
			// 子供のコンポーネントにアクセスしたいのでいったん変数に格納
			selectedObj = eventSystem.currentSelectedGameObject.gameObject;
			// ボタンのTextコンポーネントからtextデータを取得
			//text.text = selectedObj.GetComponentInChildren<Text>().text;

			if(selectedObj.GetComponentInChildren<Text>().text == "STAGE 1")
            {
				text.text = "EASY";
			}
			else if(selectedObj.GetComponentInChildren<Text>().text == "STAGE 2")
			{
				text.text = "NORMAL";
			}
			else if (selectedObj.GetComponentInChildren<Text>().text == "STAGE 3")
			{
				text.text = "HARD";
			}
		}
		// 例外処理的なやつ
		catch (NullReferenceException ex)
		{
			// なにも選択されない場合に
			text.text = "NULL";
		}
	}

}