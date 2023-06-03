using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputComponent : MonoBehaviour
{
	[SerializeField] private Text _buttonText; // ����� ������
	[SerializeField] private string _defaultKeyName; // ����/��� ������
	[SerializeField] private KeyCode _defaultKeyCode; // ������� �����

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

	public void ButtonSetKey() // ������� ������, ��� �������� � ����� ��������
	{
		tmpKey = _buttonText.text;
		_buttonText.text = "???";
		coroutine = Wait();
		StartCoroutine(coroutine);
	}

	// ����, ����� ����� ������ �����-������ �������, ��� ��������
	// ���� ����� ������ ������� 'Escape', �� ������
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
