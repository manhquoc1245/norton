using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 3f;
    private float dirX = 0f;

    private SpriteRenderer sprite;

    private float maxHealth = 100f;
    [SerializeField] private GameObject hitParticles;

    private float currentHealth;
    private Rigidbody rb;
    private PlayerController pc;

    BoxCollider2D boxCollider;
    Vector2 currentColliderOffset;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        transform.gameObject.SetActive(true);

        boxCollider = GetComponent<BoxCollider2D>();

        currentColliderOffset = boxCollider.offset;

    }
    private void Update()
    {
        
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

        dirX = transform.position.x;

        if (currentWaypointIndex==0)
        {
            sprite.flipX = true;
            boxCollider.offset = new Vector2(-currentColliderOffset.x, currentColliderOffset.y);


        }
        else if (currentWaypointIndex == 1)
        {
            sprite.flipX = false;
            boxCollider.offset = new Vector2(currentColliderOffset.x, currentColliderOffset.y);
        }

    }
    private void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;

        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (currentHealth <= 0.0)
        {
            Die();

        }

    }

    private void Die()
    {
        transform.gameObject.SetActive (false);
        Destroy(gameObject);
    }

}
