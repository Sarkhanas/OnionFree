using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{

    public GameObject[] first;
    public GameObject[] second;
    public GameObject[] third;
    public GameObject[] fourth;
    public Text points;

    private bool[] HP_Up;
    private bool[] Armor_Up;
    private bool[] DMG_Up;

    public Upgrade Upgrade1
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("HP_1", PlayerPrefs.GetInt("maxHealth") == 10 ? 1 : PlayerPrefs.GetInt("HP_1"));
        PlayerPrefs.SetInt("HP_2", PlayerPrefs.GetInt("maxHealth") <= 13 ? 1 : PlayerPrefs.GetInt("HP_2"));
        PlayerPrefs.SetInt("HP_3", PlayerPrefs.GetInt("maxHealth") <= 16 ? 1 : PlayerPrefs.GetInt("HP_3"));
        PlayerPrefs.SetInt("HP_4", PlayerPrefs.GetInt("maxHealth") <= 19 ? 1 : PlayerPrefs.GetInt("HP_4"));

        PlayerPrefs.SetInt("ARM_1", PlayerPrefs.GetInt("armor") == 0 ? 1 : PlayerPrefs.GetInt("ARM_1"));
        PlayerPrefs.SetInt("ARM_2", PlayerPrefs.GetInt("armor") <= 1 ? 1 : PlayerPrefs.GetInt("ARM_2"));
        PlayerPrefs.SetInt("ARM_3", PlayerPrefs.GetInt("armor") <= 2 ? 1 : PlayerPrefs.GetInt("ARM_3"));
        PlayerPrefs.SetInt("ARM_4", PlayerPrefs.GetInt("armor") <= 3 ? 1 : PlayerPrefs.GetInt("ARM_4"));

        PlayerPrefs.SetInt("DMG_1", PlayerPrefs.GetInt("damage") == 1 ? 1 : PlayerPrefs.GetInt("DMG_1"));
        PlayerPrefs.SetInt("DMG_2", PlayerPrefs.GetInt("damage") <= 2 ? 1 : PlayerPrefs.GetInt("DMG_2"));
        PlayerPrefs.SetInt("DMG_3", PlayerPrefs.GetInt("damage") <= 3 ? 1 : PlayerPrefs.GetInt("DMG_3"));
        PlayerPrefs.SetInt("DMG_4", PlayerPrefs.GetInt("damage") <= 4 ? 1 : PlayerPrefs.GetInt("DMG_4"));

        HP_Up = new bool[]
        {
            PlayerPrefs.GetInt("HP_1") == 0 ? false : (PlayerPrefs.GetInt("HP_1") == 1 ? true : false),
            PlayerPrefs.GetInt("HP_2") == 0 ? false : (PlayerPrefs.GetInt("HP_2") == 1 ? true : false),
            PlayerPrefs.GetInt("HP_3") == 0 ? false : (PlayerPrefs.GetInt("HP_3") == 1 ? true : false),
            PlayerPrefs.GetInt("HP_4") == 0 ? false : (PlayerPrefs.GetInt("HP_4") == 1 ? true : false)
        };

        Armor_Up = new bool[]
        {
            PlayerPrefs.GetInt("ARM_1") == 0 ? false : (PlayerPrefs.GetInt("ARM_1") == 1 ? true : false),
            PlayerPrefs.GetInt("ARM_2") == 0 ? false : (PlayerPrefs.GetInt("ARM_2") == 1 ? true : false),
            PlayerPrefs.GetInt("ARM_3") == 0 ? false : (PlayerPrefs.GetInt("ARM_3") == 1 ? true : false),
            PlayerPrefs.GetInt("ARM_4") == 0 ? false : (PlayerPrefs.GetInt("ARM_4") == 1 ? true : false)
        };

        DMG_Up = new bool[]
        {
            PlayerPrefs.GetInt("DMG_1") == 0 ? false : (PlayerPrefs.GetInt("DMG_1") == 1 ? true : false),
            PlayerPrefs.GetInt("DMG_2") == 0 ? false : (PlayerPrefs.GetInt("DMG_2") == 1 ? true : false),
            PlayerPrefs.GetInt("DMG_3") == 0 ? false : (PlayerPrefs.GetInt("DMG_3") == 1 ? true : false),
            PlayerPrefs.GetInt("DMG_4") == 0 ? false : (PlayerPrefs.GetInt("DMG_4") == 1 ? true : false)
        };

        points.text = "Очки: " + PlayerPrefs.GetInt("points");

        first[0].SetActive(HP_Up[0]);
        first[1].SetActive(Armor_Up[0]);
        first[2].SetActive(DMG_Up[0]);

        second[0].SetActive(HP_Up[1]);
        second[1].SetActive(Armor_Up[1]);
        second[2].SetActive(DMG_Up[1]);

        third[0].SetActive(HP_Up[2]);
        third[1].SetActive(Armor_Up[2]);
        third[2].SetActive(DMG_Up[2]);

        fourth[0].SetActive(HP_Up[3]);
        fourth[1].SetActive(Armor_Up[3]);
        fourth[2].SetActive(DMG_Up[3]);

        if (PlayerPrefs.GetInt("points") >= 100)
            for (int i = 0; i < first.Length; i++)
            {
                first[i].SetActive(true);
            }
        else
            for (int i = 0; i < first.Length; i++)
            {
                first[i].SetActive(false);
            }

        if (PlayerPrefs.GetInt("points") >= 200)
            for (int i = 0; i < second.Length; i++)
            {
                second[i].SetActive(true);
            }
        else
            for (int i = 0; i < second.Length; i++)
            {
                second[i].SetActive(false);
            }

        if (PlayerPrefs.GetInt("points") >= 300)
            for (int i = 0; i < third.Length; i++)
            {
                third[i].SetActive(true);
            }
        else
            for (int i = 0; i < third.Length; i++)
            {
                third[i].SetActive(false);
            }

        if (PlayerPrefs.GetInt("points") >= 350)
            for (int i = 0; i < fourth.Length; i++)
            {
                fourth[i].SetActive(true);
            }
        else
            for (int i = 0; i < fourth.Length; i++)
            {
                fourth[i].SetActive(false);
            }
    }

    private void Update()
    {
        points.text = "Очки: " + PlayerPrefs.GetInt("points");

        if (PlayerPrefs.GetInt("points") >= 100)
        {
            for (int i = 0; i < first.Length; i++)
            {
                first[i].SetActive(true);
            }

            first[0].SetActive(HP_Up[0]);
            first[1].SetActive(Armor_Up[0]);
            first[2].SetActive(DMG_Up[0]);
        }
        else
            for (int i = 0; i < first.Length; i++)
            {
                first[i].SetActive(false);
            }

        if (PlayerPrefs.GetInt("points") >= 200)
        {
            for (int i = 0; i < second.Length; i++)
            {
                second[i].SetActive(true);
            }

            second[0].SetActive(HP_Up[1]);
            second[1].SetActive(Armor_Up[1]);
            second[2].SetActive(DMG_Up[1]);
        }
        else
            for (int i = 0; i < second.Length; i++)
            {
                second[i].SetActive(false);
            }

        if (PlayerPrefs.GetInt("points") >= 300)
        {
            for (int i = 0; i < third.Length; i++)
            {
                third[i].SetActive(true);
            }

            third[0].SetActive(HP_Up[2]);
            third[1].SetActive(Armor_Up[2]);
            third[2].SetActive(DMG_Up[2]);
        }
        else
            for (int i = 0; i < third.Length; i++)
            {
                third[i].SetActive(false);
            }

        if (PlayerPrefs.GetInt("points") >= 350)
        {
            for (int i = 0; i < fourth.Length; i++)
            {
                fourth[i].SetActive(true);
            }

            fourth[0].SetActive(HP_Up[3]);
            fourth[1].SetActive(Armor_Up[3]);
            fourth[2].SetActive(DMG_Up[3]);
        }
        else
            for (int i = 0; i < fourth.Length; i++)
            {
                fourth[i].SetActive(false);
            }

        

        
    }

    public void addHP_1()
    {
        PlayerPrefs.SetInt("HP_1", 0);
        PlayerPrefs.SetInt("maxHealth", PlayerPrefs.GetInt("maxHealth") + 3);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 100);
        /*first[0].SetActive(false);*/
    }
    public void addHP_2()
    {
        PlayerPrefs.SetInt("HP_2", 0);
        PlayerPrefs.SetInt("maxHealth", PlayerPrefs.GetInt("maxHealth") + 3);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 200);
    }
    public void addHP_3()
    {
        PlayerPrefs.SetInt("HP_3", 0);
        PlayerPrefs.SetInt("maxHealth", PlayerPrefs.GetInt("maxHealth") + 3);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 300);
    }
    public void addHP_4()
    {
        PlayerPrefs.SetInt("HP_4", 0);
        PlayerPrefs.SetInt("maxHealth", PlayerPrefs.GetInt("maxHealth") + 3);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 350);
    }

    public void addARM_1()
    {
        PlayerPrefs.SetInt("ARM_1", 0);
        PlayerPrefs.SetInt("armor", PlayerPrefs.GetInt("armor") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 100);
    }
    public void addARM_2()
    {
        PlayerPrefs.SetInt("ARM_2", 0);
        PlayerPrefs.SetInt("armor", PlayerPrefs.GetInt("armor") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 200);
    }
    public void addARM_3()
    {
        PlayerPrefs.SetInt("ARM_3", 0);
        PlayerPrefs.SetInt("armor", PlayerPrefs.GetInt("armor") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 300);
    }
    public void addARM_4()
    {
        PlayerPrefs.SetInt("ARM_4", 0);
        PlayerPrefs.SetInt("armor", PlayerPrefs.GetInt("armor") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 350);
    }

    public void addDMG_1() 
    {
        PlayerPrefs.SetInt("DMG_1", 0);
        PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("damage") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 100);
    }
    public void addDMG_2()
    {
        PlayerPrefs.SetInt("DMG_2", 0);
        PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("damage") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 200);
    }
    public void addDMG_3()
    {
        PlayerPrefs.SetInt("DMG_3", 0);
        PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("damage") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 300);
    }
    public void addDMG_4()
    {
        PlayerPrefs.SetInt("DMG_4", 0);
        PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("damage") + 1);
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") - 350);
    }

    public void NextBtn()
    {
        SceneManager.LoadScene(0);
    }
}
