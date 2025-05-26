using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform player;
    public float radioAccion = 15f;
    public float speed = 4f;
    public float radioAtaque = 4f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public float distanceToPlayer;
    public float moving;
    public BossStats stats;

    private Animator animator;
    public float Horizontal;
    public BossAttack attack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = rb.GetComponent<BossStats>();
        animator = GetComponent<Animator>();
        attack = GetComponent<BossAttack>();


    }


    void Update()
    {
        if (stats.isAlive )
        {

            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            Horizontal = Input.GetAxisRaw("Horizontal") * speed;

            animator.SetBool("running", Horizontal != 0);
            if (distanceToPlayer < radioAtaque)
            {
                attack.AttackHeroe();
            }
            if (distanceToPlayer < radioAccion)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                movement = new Vector2(direction.x, 0);

            }
            else
            {
                movement = Vector2.zero;
            }

            
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Heroe"))
        {
            collision.gameObject.GetComponent<HeroeController>().ApplyDamage(rb.position.x, 1);
        }
    }
}
