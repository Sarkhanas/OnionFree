using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;

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
    
    
    /*public GameObject portalIcon;*/

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        levelNum = int.Parse(level.text);
        floorNum = int.Parse(floor.text);
    }

    // Update is called once per frame
    void Update()
    {
        
        healthDisplay.text = health.ToString();

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if(moveInput.x == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);

        if (!facingRight && moveInput.x>0)
            Flip();
        else if(facingRight && moveInput.x<0)
            Flip();

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
        health += healthValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
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
