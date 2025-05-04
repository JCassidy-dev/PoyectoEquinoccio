
using UnityEngine;

public class MainCharMove : MonoBehaviour 
{
    public float speed;
    public Rigidbody2D rb;
    private Vector2 smoothTimeRef;
    public float smoothTime;
    public float looking;
    public float moveDirection;
    Animator anim;
    MainCharController controller;
    public float coyoteTimeRun; 
    private float runTimer;
    public bool grounded;
    public bool wallNearLeft;
    public bool wallNearRight;
    bool canJump;
    bool fly;
    
    public float jumpForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        controller = GetComponent<MainCharController>();
    }

    private void Start()
    {
        speed = 5.5f;
        smoothTimeRef = Vector2.zero;
        smoothTime = 0.01f;
        looking = controller.xDirection;
        coyoteTimeRun = 0.1f;
        runTimer = 0f;
        jumpForce = 1800f;
    }
    public void SetDirection(float Direction, bool isPressed)
    {
        if (isPressed)
        {
            moveDirection = Direction;
        } else
        {
            moveDirection = 0;
        }
    }  
    private void Update()
    {
        looking = controller.xDirection;

        if (looking != 0)
        {
            anim.SetBool("running", true);
            runTimer = coyoteTimeRun; 
            if (looking < 0.0f)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (looking > 0.0f)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            if (runTimer > 0f)
            {
                runTimer -= Time.deltaTime;
                anim.SetBool("running", true); 
            }
            else
            {
                anim.SetBool("running", false);
            }
        }
        grounded = controller.grounded;
        wallNearLeft = controller.wallNearLeft;
        wallNearRight = controller.wallNearRight;
         
      
    }
    public void JumpPress()
    {
        if (grounded || wallNearRight || wallNearLeft)
        {
            anim.SetBool("falling", false);
            canJump = true;
            fly = false;
            if (canJump)
            {
                Jump();
                canJump = false;
                fly = true;
            }
            else if (fly)
            {
                Jump();
                fly = false;
            }
            anim.SetBool("falling", true);
        }
        else
        {
            canJump = false;
        }

    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        anim.SetTrigger("jump");
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref smoothTimeRef, smoothTime);     
    }
  
}
