using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI; // UIコンポーネントの使用

public class Menu : MonoBehaviour
{
	[SerializeField] EventSystem eventSystem;

	[SerializeField] Text text;
	GameObject selectedObj;

	Button stage1;
	Button stage2;
	Button stage3;

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
		// TryCatch文でNull回避
		try
		{
			// 子供のコンポーネントにアクセスしたいのでいったん変数に格納
			selectedObj = eventSystem.currentSelectedGameObject.gameObject;
			// ボタンのTextコンポーネントからtextデータを取得
			text.text = selectedObj.GetComponentInChildren<Text>().text;
		}
		// 例外処理的なやつ
		catch (NullReferenceException ex)
		{
			// なにも選択されない場合に
			text.text = "Unknown";
		}
	}
}