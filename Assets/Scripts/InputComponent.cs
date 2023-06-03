using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputComponent : MonoBehaviour
{
	[SerializeField] private Text _buttonText; // текст кнопки
	[SerializeField] private string _defaultKeyName; // ключ/имя вызова
	[SerializeField] private KeyCode _defaultKeyCode; // клавиша ключа

	public KeyCode keyCode { get; set; }

	public bool isUp;
	public bool isDown;
	public bool isLeft;
	public bool isRight;

	private IEnumerator coroutine;
	private string tmpKey;

	public Text buttonText
	{
		get { return _buttonText; }
	}

	public string defaultKeyName
	{
		get { return _defaultKeyName; }
	}

	public KeyCode defaultKeyCode
	{
		get { return _defaultKeyCode; }
	}

	public void ButtonSetKey() // событие кнопки, для перехода в режим ожидания
	{
		tmpKey = _buttonText.text;
		_buttonText.text = "???";
		coroutine = Wait();
		StartCoroutine(coroutine);
	}

	// ждем, когда игрок нажмет какую-нибудь клавишу, для привязки
	// если будет нажата клавиша 'Escape', то отмена
	IEnumerator Wait()
	{
		while (true)
		{
			yield return null;

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_buttonText.text = tmpKey;
				StopCoroutine(coroutine);
			}

			foreach (KeyCode k in KeyCode.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(k) && !Input.GetKeyDown(KeyCode.Escape))
				{
					keyCode = k;
					if (isUp) PlayerPrefs.SetInt("up", (int)k);
					if (isDown) PlayerPrefs.SetInt("down", (int)k);
					if (isLeft) PlayerPrefs.SetInt("left", (int)k);
					if (isRight) PlayerPrefs.SetInt("right", (int)k);
					PlayerPrefs.SetInt("OptionsChanged", 1);
					_buttonText.text = k.ToString();
					StopCoroutine(coroutine);
				}
			}
		}
	}
}
