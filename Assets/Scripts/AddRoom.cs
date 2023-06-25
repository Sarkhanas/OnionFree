using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Walls")]
    public GameObject[] walls;
    public GameObject door;

    [Header("Walls")]
    public GameObject portal;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public GameObject healthPotion;
    public Transform[] enemySpawner;

    [HideInInspector] public List<GameObject> enemies;
    [HideInInspector] public bool isBossRoom;
    [HideInInspector] public bool isBossDefeated;
    [HideInInspector] public bool isLastRoom;

    private RoomVariants variants;
    private bool spawned;
    private bool wallsDestroyed;

    public RoomVariants RoomVariants
    {
        get => default;
        set
        {
        }
    }

    public Player Player
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

    private void Awake()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }

    private void Start()
    {
        variants.rooms.Add(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !spawned)
        {
            spawned = true;

            if (!isBossRoom && gameObject.name != "MainRoom" && gameObject.name != "MainRoom(Clone)")
            {
                foreach (Transform spawner in enemySpawner)
                {
                    int rand = Random.Range(0, 10);//до 11
                    if (rand < 9)
                    {
                        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                        GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                        enemy.transform.SetParent(gameObject.transform);                        
                        enemies.Add(enemy);
                    }
                    else if (rand == 9)
                    {
                        Instantiate(healthPotion, spawner.position, Quaternion.identity);
                    }
                    /*else if (rand == 10)
                    {
                        Instantiate(shield, spawner.position, Quaternion.identity);
                    }*/
                }
            }
            else if (isBossRoom && gameObject.name != "MainRoom" && gameObject.name != "MainRoom(Clone)")
            {
                int rand = Random.Range(0, enemySpawner.Length);

                GameObject enemy = Instantiate(enemyTypes[0], enemySpawner[rand].position, Quaternion.identity) as GameObject;

                enemy.GetComponent<Enemy>().isBoss = true;
                enemy.GetComponent<Enemy>().health *= 2;
                enemy.GetComponent<Enemy>().speed = 1;
                enemy.GetComponent<Enemy>().damage = 2;
                enemy.GetComponent<Enemy>().isBoss = true;
                enemy.transform.localScale = new Vector3((float)(gameObject.transform.localScale.x * 0.5), (float)(gameObject.transform.localScale.y * 0.5));

                enemy.transform.SetParent(gameObject.transform);
                enemies.Add(enemy);
            }
            else
            {
                DestroyWalls();
            }
             
            StartCoroutine(CheckEnemies());
        } else if (other.CompareTag("Player") && spawned)
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = false;
            }
        }
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        if (isBossRoom && isBossDefeated || isLastRoom)
            portal.SetActive(true);
        DestroyWalls();
    }

    public void DestroyWalls()
    {
        foreach(GameObject wall in walls)
        {
            if(wall != null && wall.transform.childCount != 0)
            {
                Destroy(wall);
            }
        }
        wallsDestroyed = true;        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(wallsDestroyed && other.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Room"))
        {
            Destroy(other.gameObject);
            Debug.Log("Комната уничтожена");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spawned)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = true;
            }
        }
    }
}
