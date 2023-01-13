using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float stopTime;
    public float startStopTime;

    private Animator anim;
    private Player player;
    private AddRoom room;

    public GameObject destroyEffect;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float speed;
    public int health;
    public int damage;

    [HideInInspector] public bool playerNotInRoom;
    [HideInInspector] public bool isBoss;

    private bool stopped;

    public Player Player
    {
        get => default;
        set
        {
        }
    }

    public AddRoom AddRoom
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        room = GetComponentInParent<AddRoom>();
        stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerNotInRoom)
        {
            if (stopTime <= 0)
                stopped = false;
            else
            {
                stopped = true;
                stopTime -= Time.deltaTime;
            }
        }
        else
        {
            stopped = true;
        }

        if (room == null)
        {
            Debug.Log("Room is Null");//Попробуй присваивать комнату волкам при спавне
        }

        if (health <= 0)
        {
            room.enemies.Remove(gameObject);
            if (isBoss)
                room.isBossDefeated = true;
            Destroy(gameObject);
        }

        if (player.transform.position.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
        if(!stopped)
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
