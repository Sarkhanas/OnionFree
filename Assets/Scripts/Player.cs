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

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;

    private bool facingRight = true;
    private bool keyButtonPushed;

    [Header("Key")]
    public GameObject keyIcon;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Door") && keyButtonPushed && keyIcon.activeInHierarchy)
        {
            keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonPushed = false;
        }
    }
}
