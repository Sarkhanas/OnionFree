using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class options : MonoBehaviour
{

    public GameObject upBtn;
    public GameObject downBtn;
    public GameObject leftBtn;
    public GameObject rightBtn;

    public GameObject resetButton;

    private void Start()
    {
        upBtn.GetComponentInChildren<Text>().text = ((KeyCode)PlayerPrefs.GetInt("up")).ToString(); 
        downBtn.GetComponentInChildren<Text>().text = ((KeyCode)PlayerPrefs.GetInt("down")).ToString(); 
        leftBtn.GetComponentInChildren<Text>().text = ((KeyCode)PlayerPrefs.GetInt("left")).ToString(); 
        rightBtn.GetComponentInChildren<Text>().text = ((KeyCode)PlayerPrefs.GetInt("right")).ToString(); 
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("OptionsChanged") == 1)
        {
            resetButton.SetActive(true);
        } else
        {
            resetButton.SetActive(false);
        }
    }

    public void resetOptions()
    {
        PlayerPrefs.SetInt("up", PlayerPrefs.GetInt("defUp"));
        upBtn.GetComponentInChildren<Text>().text = "W";
        PlayerPrefs.SetInt("down", PlayerPrefs.GetInt("defDown"));
        downBtn.GetComponentInChildren<Text>().text = "S";
        PlayerPrefs.SetInt("left", PlayerPrefs.GetInt("defLeft"));
        leftBtn.GetComponentInChildren<Text>().text = "A";
        PlayerPrefs.SetInt("right", PlayerPrefs.GetInt("defRight"));
        rightBtn.GetComponentInChildren<Text>().text = "D";
        PlayerPrefs.SetInt("OptionsChanged", 0);
    }
}
