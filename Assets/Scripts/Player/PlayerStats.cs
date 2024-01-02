using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle,
        healthBar;

    private float currentHealth;

    private GameManager GM;

    private HeathBar hb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hb = healthBar.GetComponent<HeathBar>();
        hb.SetMaxHealth(maxHealth);
        //GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        hb.SetHealth(currentHealth);

        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        //GM.Respawn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dianguc"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("NextLv"))
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            StartCoroutine(Deplay(3));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    IEnumerator Deplay(int x)
    {
        yield return new WaitForSeconds(x);
    }
}
