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

    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = Direction * Speed;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {/*
            HeroeStats enemy = other.gameObject.GetComponent<HeroeStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        */
        }
        else if (other.gameObject.CompareTag("floor"))
        {
            DestroyBullet();
        }
        DestroyBullet();
    }
}
