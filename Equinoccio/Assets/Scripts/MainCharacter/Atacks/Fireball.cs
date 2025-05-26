using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float Speed;
    public AudioClip Sound;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Speed = 3f;

    }
     
    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = Direction * Speed;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
        transform.localScale = new Vector3(direction.x, 1.0f, 1.0f);
    }

    public void DestroyFireball()
    {
       
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        /*if (trigger.gameObject.CompareTag("enemy"))
        {
            HeroeStats enemy = trigger.gameObject.GetComponent<HeroeStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
       
        }
        */
        
        DestroyFireball();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            Debug.Log("Toque fin");
            DestroyFireball();
        }
        DestroyFireball();
    }

    
}
