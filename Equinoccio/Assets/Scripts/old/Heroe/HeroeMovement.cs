using System.Collections;
using UnityEngine;



public class HeroeMovement : MonoBehaviour
{
    public float JumpForce = 180f; 
    private Rigidbody2D Rigidbd2d;
    public float Horizontal;
    public float Velocidad = 5f;
    private bool CanJump;
    private bool Flying;
    private Animator Animator;
    private Vector2 DampVelocity = Vector2.zero;  
    public float SmoothTime = 0.1f; 
    private float GroundCheckDistance = 0.1f;
    public bool takeDamage = false;
    int directionDamage = 0;
    public float PlayerXSituation;
    public int knockbackStrength = 40;
    public float tiempoPW = 5f;
    public float speedReset = 5f;
    public HeroeStats stats;
    void Start()
    {
        Rigidbd2d = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        stats = GetComponent<HeroeStats>();
    }

    public void Update()
    {
        if (stats.isAlive)
        {
        Horizontal = Input.GetAxisRaw("Horizontal") * Velocidad;

                Animator.SetBool("running", Horizontal != 0);
                PlayerXSituation = transform.localScale.x;

                if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance);
                if (hit.collider != null)
                {
                    Animator.SetBool("Falling", false);
                    CanJump = true;
                    Flying = false;
                }
                else
                {
                    CanJump = false;
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (CanJump)
                    {
                        Jump();
                        CanJump = false; 
                        Flying = true;
                    }
                    else if (Flying)
                    {
                        Jump();
                        Flying = false; 
                    }
                    Animator.SetBool("Falling", true);
                }
        
        }
    }

    private void Jump()
    {
        Rigidbd2d.AddForce(Vector2.up * JumpForce);
        Animator.SetTrigger("Jump");
    }

    private void FixedUpdate()
    {
        if (stats.isAlive)
        {
            Vector2 targetVelocity = new Vector2(Horizontal, Rigidbd2d.linearVelocity.y);
                    if (takeDamage)
                    {
            
                        Vector2 knockbackForce = new Vector2(directionDamage * knockbackStrength, Rigidbd2d.linearVelocity.y);
                        Rigidbd2d.linearVelocity = knockbackForce;
                        takeDamage = false;
                        takeDamage = false;
                    }
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance);
                    Rigidbd2d.linearVelocity = Vector2.SmoothDamp(Rigidbd2d.linearVelocity, targetVelocity, ref DampVelocity, SmoothTime);
        }

        
        
    }

    public void SetDamage( int direction)
    {
        takeDamage = true;
        directionDamage = direction;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("fruit"))
        {
            collision.gameObject.GetComponent<fruit>().Die();
            StartCoroutine(pwUpSpeed());
        }
    }


    public IEnumerator pwUpSpeed()
    {
        Velocidad = Velocidad * 1.4f;
        yield return new WaitForSeconds(tiempoPW);
        Velocidad = speedReset;
    }
}


