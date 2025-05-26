using UnityEngine;

public class BanditController : MonoBehaviour
{
    private BanditMovement BanditMovement;
    private BanditStats Stats;

    void Start()
    {
        BanditMovement = GetComponent<BanditMovement>();
        Stats = GetComponent<BanditStats>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

 
            if (collision.gameObject.CompareTag("Heroe"))
            {
                BanditMovement.rb.linearVelocity = Vector2.zero;
            }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SwordCollider"))
        {
            int damage = collision.gameObject.GetComponent<Sword>().getAttack();
            Stats.TakeDamage( damage );
        }
    }


}
