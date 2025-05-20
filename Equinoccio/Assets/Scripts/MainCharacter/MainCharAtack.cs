using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCharAtack : MonoBehaviour
{
    public bool canAtackMelee;
    public bool canAtackRngd;
    public float lastAtackTimeMelee;
    public float lastAtackTimeRngd;
    public float cooldownMeleeAtk;
    public float cooldownRngdAtk;
    public Animator animator;
    public GameObject Fireball;
    public Rigidbody2D rb;
    public MainCharController controller;
    public float dashDuration;
    public float dashSpeed;
    private Vector2 smoothTimeRef;
    public float smoothTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<MainCharController>();
    }

    private void Start()
    {
        canAtackMelee = true;
        canAtackRngd = true;
        cooldownMeleeAtk = 0.6f;
        cooldownRngdAtk = 0.5f;
        lastAtackTimeMelee = Time.time;
        lastAtackTimeRngd = Time.time;
        dashDuration = 0.1f;
        dashSpeed = 700f;
    }

    private void Update()
    {
        if (Time.time > lastAtackTimeMelee + cooldownMeleeAtk)
        {
            canAtackMelee = true;
        }
        if (Time.time > lastAtackTimeRngd + cooldownRngdAtk)
        {
            canAtackRngd = true;
        }
    }

    public void Attack(string atack)
    {
        switch (atack)
        {
            case "AtkM":
                    if (canAtackMelee)
                    {
                        animator.SetTrigger("meleeAtack");
                        lastAtackTimeMelee = Time.time;
                        canAtackMelee = false;
                    } 
                break;
            case "AtkSp":
                    if (canAtackMelee)
                    {
                        animator.SetTrigger("spearAtack");
                        StartCoroutine(Dashear());
                    lastAtackTimeMelee = Time.time;
                        canAtackMelee = false;
                    }
                break;
            case "AtkRg":
                if (canAtackRngd)
                {
                    Shoot();
                    animator.SetTrigger("rangedAtack");
                    lastAtackTimeRngd = Time.time;
                    canAtackRngd = false;
                }
                break;
        }
    }
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject fireball = Instantiate(Fireball, transform.position + direction * 0.1f, Quaternion.identity);
        fireball.GetComponent<Fireball>().SetDirection(direction);
    }
    private IEnumerator Dashear()
    {
      
        float inputX = controller.looking;
        if (inputX == 0) inputX = transform.localScale.x; // Usa la dirección en la que está mirando
        Vector2 dashDirection = new Vector2(inputX, 0).normalized;

        // Desactiva gravedad (opcional)
        float originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        // Aplica velocidad de dash
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, (dashDirection * dashSpeed), ref smoothTimeRef, smoothTime);

        // Espera duración del dash
        yield return new WaitForSeconds(dashDuration);


        rb.gravityScale = originalGravityScale;
        rb.linearVelocity = Vector2.zero;

    }

}
