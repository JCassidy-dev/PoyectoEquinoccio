using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStats : MonoBehaviour
{
    
    public Boolean isAlive;
    public float lifeMax = 5f;
    public float damage = 1f;
    public float life;
    public Animator animator;
    void Start()
    {
        isAlive = true;
        life = lifeMax;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {

        life -= damage;
        animator.SetTrigger("hit");
        if (life <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        isAlive = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DieEnd());
    }

    IEnumerator DieEnd()
    {
        yield return new WaitForSeconds(2f);
        animator.enabled = false; 
        Destroy(gameObject);
        SceneManager.LoadScene("win");
    }
}
