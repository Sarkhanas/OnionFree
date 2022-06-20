using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float stopTime;
    public float startStopTime;

    private Animator anim;
    private Player player;

    public GameObject destroyEffect;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float speed;
    public float normalSpeed;
    public int health;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTime <= 0)
            speed = normalSpeed;
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0)
            Destroy(gameObject);

        if (player.transform.position.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("attack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyAttack()
    {
        Instantiate(destroyEffect, player.transform.position, Quaternion.identity);
        player.ChangeHealth(-damage);
        timeBtwAttack = startTimeBtwAttack;
    }
}
