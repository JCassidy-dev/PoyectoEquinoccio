using UnityEngine;

public class BossController : MonoBehaviour
{

    private BossMovement BossMovement;
    private BossStats Stats;

    void Start()
    {
        BossMovement = GetComponent<BossMovement>();
        Stats = GetComponent<BossStats>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Heroe"))
        {
            BossMovement.rb.linearVelocity = Vector2.zero;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SwordCollider"))
        {
            int damage = collision.gameObject.GetComponent<Sword>().getAttack();
            Stats.TakeDamage(damage);
        }
    }


}

