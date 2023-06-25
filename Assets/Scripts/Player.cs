using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CircleCollider2D controller;

    public float speed;
    public int health;
    public int armor = 0;
    public int maxHealth;

    public int upPoints = 0;

    public Text healthDisplay;
    public Text level;
    public Text floor;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;

    private bool facingRight = true;
    private bool keyButtonPushed;
    private int levelNum;
    private int floorNum;
    public GameObject mainRoom;
    public GameObject Room;
    public GameObject deadScreen;
    public GameObject HUD;


    [Header("Key")]
    public GameObject keyIcon;

    [Header("Controls")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    private bool isControlsChanged = false;

    public RoomVariants RoomVariants
    {
        get => default;
        set
        {
        }
    }

    public PauseMenu PauseMenu
    {
        get => default;
        set
        {
        }
    }

    public DeadEnd DeadEnd
    {
        get => default;
        set
        {
        }
    }


    /*public GameObject portalIcon;*/

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("maxHealth") <= maxHealth)
        {
            PlayerPrefs.SetInt("maxHealth", maxHealth);
        }

        health = maxHealth;

        controller = GetComponent<CircleCollider2D>();

        if (PlayerPrefs.GetInt("OptionsChanged") == 0)
            isControlsChanged = false;
        else
            isControlsChanged = true;

        if (PlayerPrefs.GetInt("armor") == PlayerPrefs.GetInt("defArmor"))
            armor = PlayerPrefs.GetInt("defArmor");
        else
            armor = PlayerPrefs.GetInt("armor");

        upPoints = PlayerPrefs.GetInt("points") != 0 || PlayerPrefs.GetInt("points") != null ? PlayerPrefs.GetInt("points") : 0;


        PlayerPrefs.SetInt("defDamage", 1);

        if (PlayerPrefs.GetInt("damage") <= PlayerPrefs.GetInt("defDamage"))
            PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("defDamage"));

        PlayerPrefs.SetInt("defUp", (int)KeyCode.W);
        PlayerPrefs.SetInt("defDown", (int)KeyCode.S);
        PlayerPrefs.SetInt("defLeft", (int)KeyCode.A);
        PlayerPrefs.SetInt("defRight", (int)KeyCode.D);

        if (!isControlsChanged)
        {
            PlayerPrefs.SetInt("up", PlayerPrefs.GetInt("defUp"));
            PlayerPrefs.SetInt("down", PlayerPrefs.GetInt("defDown"));
            PlayerPrefs.SetInt("left", PlayerPrefs.GetInt("defLeft"));
            PlayerPrefs.SetInt("right", PlayerPrefs.GetInt("defRight"));
        }

        up = (KeyCode)PlayerPrefs.GetInt("up");
        down = (KeyCode)PlayerPrefs.GetInt("down");
        left = (KeyCode)PlayerPrefs.GetInt("left");
        right = (KeyCode)PlayerPrefs.GetInt("right");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        levelNum = int.Parse(level.text);
        floorNum = int.Parse(floor.text);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        healthDisplay.text = health.ToString();

        if (controller.isActiveAndEnabled)
        {
            int hor;
            int ver;

            if (Input.GetKey(up))
            {
                hor = 1;
            } else if (Input.GetKey(down))
            {
                hor = -1;
            } else
            {
                hor = 0;
            }

            if (Input.GetKey(right))
            {
                ver = 1;
            }
            else if (Input.GetKey(left))
            {
                ver = -1;
            }
            else
            {
                ver = 0;
            }

            moveInput = new Vector2(ver, hor);
            moveVelocity = moveInput.normalized * speed;

            if(moveInput.x == 0)
            anim.SetBool("isRunning", false);
            else
                anim.SetBool("isRunning", true);

            if (!facingRight && moveInput.x>0)
                Flip();
            else if(facingRight && moveInput.x<0)
                Flip();
        }

       

        if (health <= 0)
        {
            deadScreen.SetActive(true);
            HUD.SetActive(false);
        }            
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void ChangeHealth(int healthValue)
    {
        
        if (healthValue < 0)
        {
            if (healthValue * -1 - armor <= 0)
                health += 0;
            else
                health += (healthValue + armor);
            Debug.Log("Отнимаем здоровье: " + healthValue);
        }
        else
        {
            health += healthValue;
            Debug.Log("Добавляем здоровье: " + healthValue);
        }

        //health += healthValue + (healthValue < 0 ? (healthValue >= armor ? armor : healthValue) : 0);

        if (health > maxHealth)
            health = maxHealth;
    }

    public void ChangePoints(int points)
    {
        upPoints += points;
        PlayerPrefs.SetInt("points", upPoints);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Heal"))
        {
            if (health < maxHealth)
            {
                ChangeHealth(3);
                Destroy(other.gameObject);
            }
        }
    }

    public void onKeyButtonDown()
    {
        keyButtonPushed = !keyButtonPushed;
    }

    /*public void onPortalBurronDown()
    {
        portalButtonPushed = !portalButtonPushed;
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {        
        if (other.CompareTag("Door") && keyButtonPushed && keyIcon.activeInHierarchy)
        {
            keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonPushed = false;
        }

        if (other.CompareTag("Portal"))
        {
            if (int.Parse(floor.text) < 3)
            {
                floorNum++;
                floor.text = floorNum.ToString();
            }
            else
            {
                levelNum++;
                level.text = levelNum.ToString();
                floorNum = 1;
                floor.text = floorNum.ToString();
            }

            if (level.text != "3")
            {
                GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
                foreach(var room in rooms)
                {
                    Destroy(room);
                }
                Instantiate(Room, Camera.main.GetComponent<Camera>().transform.position, Quaternion.identity);
                Instantiate(mainRoom, Camera.main.GetComponent<Camera>().transform.position, Quaternion.identity);                
                gameObject.transform.position = Camera.main.GetComponent<Camera>().transform.position;
            }
        }
    }
}
