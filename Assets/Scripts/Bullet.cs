using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;

    [SerializeField] bool enemyBullet;

    public Gun Gun
    {
        get => default;
        set
        {
        }
    }

    public Enemy Enemy
    {
        get => default;
        set
        {
        }
    }

    private void Start()
    {
        damage = PlayerPrefs.GetInt("damage");
        Invoke("DestroyBullet", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy"))
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);

            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
                hitInfo.collider.GetComponent<Player>().ChangeHealth(-damage);

            DestroyBullet();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void DestroyBullet()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
