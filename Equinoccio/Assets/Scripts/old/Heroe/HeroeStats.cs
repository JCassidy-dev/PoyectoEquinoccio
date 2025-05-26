using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroeStats : MonoBehaviour
{
   
    public float health;  
    public float maxHealth =5f;
    Animator animator;
    public bool isAlive;


     void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    public void Update()
    {
       if (health <= 0)
        {
            Die();
        }

    }

    
    // Recibir daño
    public void TakeDamage(int damage)
    {

        health -= damage;
        animator.SetTrigger("Damage");
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        animator.SetTrigger("Death");
        StartCoroutine(death());
        Debug.Log("El jugador ha muerto");
        SceneManager.LoadScene("gameOver");
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(3f);
    }


    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floorDeath"))
        {
            health = 0;
        }
    }

    
}


