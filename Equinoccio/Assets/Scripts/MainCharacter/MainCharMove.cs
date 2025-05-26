
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
    bool wallRide;
    bool canJump;
    bool fly;
    bool canTurn;
    public float jumpForceUp;
    public float jumpForceX;
    [SerializeField] float wallRideDownForce;
    bool takeDamage;
    int directionDamage = 0;
    public float PlayerXSituation;
    public int knockbackStrength = 40;

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
        jumpForceUp = 75f;
        canTurn = true;
        wallRide = false;
        wallRideDownForce = 0.5f;
        canJump = true;
        jumpForceX = 25000f;
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
        if (canTurn)
        {
            looking = controller.xDirection;
        }
        
       if (grounded && looking != 0)
{
    anim.SetBool("running", true);
    runTimer = coyoteTimeRun;

    if (looking < 0.0f)
        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    else if (looking > 0.0f)
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
}
else if (grounded)
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
        if (grounded || wallNearLeft || wallNearRight)
        {
            anim.SetBool("falling", false);
        } else
        {
            anim.SetBool("falling", true);
        }
        if (!grounded && wallNearLeft)
        {
            Debug.Log("Muro en la Izquierda");
            anim.SetBool("running", false);
            anim.SetBool("leftWall", true);
            wallRide = true;
            canTurn = false;
           
        }
        
        else if (!grounded && wallNearRight)
        {
            Debug.Log("Muro en la Derecha");
            anim.SetBool("running", false);
            anim.SetBool("rightWall", true);
            wallRide = true;
            canTurn = false;
           
        } else
        {
            canTurn = true;
            anim.SetBool("leftWall", false);
            anim.SetBool("rightWall", false);
            wallRide = false;
        }
    }
    public void JumpPress()
    {
        if (grounded || wallNearRight || wallNearLeft)
        {
             Jump();
            canJump = false;
                fly = true;
            anim.SetBool("falling", true);
        } 
        else if (fly)
        {
             Jump();
             fly = false;
            anim.SetBool("falling", true);
        }
       

    }
    private void Jump()
    {  
        if (wallRide)
        {          
            rb.linearVelocity = Vector2.zero;
            if (wallNearLeft)
            {
                
                anim.SetTrigger("jump");
                Vector2 jumpForceVec = new Vector2(jumpForceX, jumpForceUp);
                rb.AddForce(jumpForceVec, ForceMode2D.Impulse);
                
            }
            else if (wallNearRight)
            {
                
                anim.SetTrigger("jump");
                Vector2 jumpForceVec = new Vector2(-jumpForceX, jumpForceUp);
                rb.AddForce(jumpForceVec, ForceMode2D.Impulse);
              
            }
        }
        else
        {
            Vector2 currentVelocity = rb.linearVelocity;
            currentVelocity.y = 0;
            rb.linearVelocity = currentVelocity;
            Vector2 jumpForceVec = new Vector2(0, jumpForceUp);
            rb.AddForce(jumpForceVec, ForceMode2D.Impulse);
        }

        anim.SetTrigger("jump");
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref smoothTimeRef, smoothTime);     
        if(wallRide)
        {
            Vector2 wallDown = new Vector2(0f, -wallRideDownForce);
            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, wallDown, ref smoothTimeRef, smoothTime);
        }
        if (takeDamage)
        {

            Vector2 knockbackForce = new Vector2(directionDamage * knockbackStrength, rb.linearVelocity.y);
            rb.linearVelocity = knockbackForce;
            takeDamage = false;
            takeDamage = false;
        }
    }
    public void SetDamage(int direction)
    {
        takeDamage = true;
        directionDamage = direction;
    }

}
