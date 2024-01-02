using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lv5controller : MonoBehaviour
{

    int maxHealth = 100;
    int currentHealth;
    public HeathBar healthBar;
    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;
    private PlayerController PC;
    private PlayerStats PS;

    public string tag1 = "Player";
    public string tag2 = "Enemy";
    private float timeToIgnoreCollision = 1f;
    private float ignoreCollisionTimer = 0f;
    private bool check = false;


    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        PC = GetComponent<PlayerController>();
        PS = GetComponent<PlayerStats>();
        ignoreCollisionTimer = timeToIgnoreCollision;
    }

    public void Update()
    {
        if (check)
        {
            ignoreCollisionTimer -= Time.deltaTime;
            IgnoreCollision();
            
            if (ignoreCollisionTimer <= 0f)
            {
                
                EnableCollision();
                check = false;
               
            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            takeHP(5);
        }
        if (collision.gameObject.CompareTag("ItemHP"))
        {
            Destroy(collision.gameObject);
            takeHP(20);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            takeDamageHP(20);
            
            check = true;
            ignoreCollisionTimer = timeToIgnoreCollision;
            int direction;

            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            PC.Knockback(direction);
        }
    }

    public void takeHP(int damage)
    {
        currentHealth += damage;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void takeDamageHP(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <=0)
        {
            Die();

        }
        healthBar.SetHealth(currentHealth);
    }
    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        //GM.Respawn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);

    }
    void IgnoreCollision()
    {
        GameObject[] objectsWithTag1 = GameObject.FindGameObjectsWithTag(tag1);

        GameObject[] objectsWithTag2 = GameObject.FindGameObjectsWithTag(tag2);

        foreach (var obj1 in objectsWithTag1)
        {
            foreach (var obj2 in objectsWithTag2)
            {
                Physics2D.IgnoreCollision(obj1.GetComponent<Collider2D>(), obj2.GetComponent<Collider2D>(), true);
            }
        }
    }

    void EnableCollision()
    {
        GameObject[] objectsWithTag1 = GameObject.FindGameObjectsWithTag(tag1);

        GameObject[] objectsWithTag2 = GameObject.FindGameObjectsWithTag(tag2);

        foreach (var obj1 in objectsWithTag1)
        {
            foreach (var obj2 in objectsWithTag2)
            {
                Physics2D.IgnoreCollision(obj1.GetComponent<Collider2D>(), obj2.GetComponent<Collider2D>(), false);
            }
        }
    }
}
