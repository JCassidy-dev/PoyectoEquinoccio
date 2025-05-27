using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PoaklController : MonoBehaviour
{
    public Transform player; // asigna esto desde el inspector o por código
    public float speed = 3f;
    public Rigidbody2D rb;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public SpriteRenderer spriteRenderer;
    private bool isChasing = false;
    private bool isTackling = false;
    public Animator anim;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(MoveTowardPlayerForTime(5f));

    }

    private void Update()
    {
        
    }

    IEnumerator MoveTowardPlayerForTime(float duration)
    {
        isChasing = true;
        float elapsed = 0f;
        anim.SetBool("running", true);
        while (elapsed < duration && isChasing)
        {
            if (player != null)
            {

                // Calcular dirección solo en el eje X
                float directionX = Mathf.Sign(player.position.x - transform.position.x);
                if (directionX != 0)
                    if (transform.position.x < player.position.x)
                    {
                        spriteRenderer.flipX = false;
                    }
                    else
                    {
                        spriteRenderer.flipX = true;
                    }
                // Mantener velocidad vertical (para que no flote)
                rb.linearVelocity = new Vector2(directionX * speed, rb.linearVelocity.y);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("running", false);
        // Detener movimiento después de 5 segundos
        rb.linearVelocity = Vector2.zero;
        isChasing = false;
    }

    public void StopChasing()
    {
        isChasing = false;
        rb.linearVelocity = Vector2.zero;
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // resetear velocidad vertical
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    public void Tackle()
    {
        if (player == null) return;

        
        float directionX = Mathf.Sign(player.position.x - transform.position.x);

        StartCoroutine(MoveInDirectionForSeconds(directionX, 3f));
    }

    private IEnumerator MoveInDirectionForSeconds(float directionX, float duration)
    {
        isTackling = true;
        float elapsed = 0f;
        anim.SetBool("stirke", true);
        while (elapsed < duration)
        {
            // Mover siempre en la misma dirección X fija
            rb.linearVelocity = new Vector2(directionX * speed*3, rb.linearVelocity.y);

            elapsed += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("strike", false);
        // Detener movimiento al finalizar el placaje
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        isTackling = false;
    }
}
