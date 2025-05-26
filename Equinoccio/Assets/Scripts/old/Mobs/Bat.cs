using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private int speed = 2;
    public Transform[] puntosMovimiento;
    private float distanciaMinima = 0.2f;
    private int random;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        random = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Girar();

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[random].position, 
            speed * Time.deltaTime);
        Girar();
        if (Vector2.Distance(transform.position, puntosMovimiento[random].position) < distanciaMinima)
        {
            random = Random.Range(0, puntosMovimiento.Length);
            Girar();
        }
    }

    void Girar()
    {
        if (transform.position.x < puntosMovimiento[random].position.x)
        {
            spriteRenderer.flipX = false;
        } else
        {
            spriteRenderer.flipX = true;
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hiro"))
        {
            collision.gameObject.GetComponent<MainCharController>().ApplyDamage(rb.position.x, 5);
            rb.linearVelocity = Vector2.zero; 
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Spear"))
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Hit");
        GetComponent<Collider2D>().enabled = false;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        StartCoroutine(DieEnd());
    }

    IEnumerator DieEnd()
    {
        yield return new WaitForSeconds(3f);
        animator.enabled = false;
        Destroy(gameObject);
    }
    
}
